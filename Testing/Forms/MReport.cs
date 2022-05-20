using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Testing.Forms
{
    public partial class MReport : Form
    {
        public string username;
        CRUD crud = new CRUD();
        private DataTable dt;
        public MReport()
        {
            InitializeComponent();
        }

        public void BindComboBox1()
        {
            DataRow dr;
            string SQLcombox = "SELECT PRO_CODE,PRO_DES,USER_C FROM MRPRC";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            //dr.ItemArray = new object[] {};
            //dtCombox.Rows.InsertAt(dr,0);
            cboprdcode.ValueMember = "PRO_DES";
            cboprdcode.DisplayMember = "PRO_CODE";
            cboprdcode.DataSource = dtCombox;
        }
        public void BindComboBox2()
        {
            DataRow dr;
            string SQLcombox = "SELECT REP_NAME,REP_DES,USER_C FROM REP_FIL";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            //dr.ItemArray = new object[] {};
            //dtCombox.Rows.InsertAt(dr,0);
            cboreport.ValueMember = "REP_NAME";
            cboreport.DisplayMember = "REP_NAME";
            cboreport.DataSource = dtCombox;
        }
        private void bnSearch_Click(object sender, EventArgs e)
        {

            dt = new DataTable();
            if (cboreport.Text == "PREMIUM_REGISTER BREAKDOWN")
            {
                CRUD crud = new CRUD();

                DataTable dtCOM = new DataTable();
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string sql = "SELECT * FROM VIEW_PRE_REGISTER_BREAK_DOWN where";
                    sql += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00','YYYY/MM/DD HH24:MI:SS')";
                    sql += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59','YYYY/MM/DD HH24:MI:SS')";
                    if(username != "ADMIN")
                    sql += "and SUB_CLASS = '" + cboprdcode.Text + "'";
                   

                    DataTable dtNUMmAX = new DataTable();
                    string sqlNUMmAX = "SELECT MAX(NUMBER_OF_COMMISSION) FROM VIEW_PRE_REGISTER_BREAK_DOWN where";
                    sqlNUMmAX += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00','YYYY/MM/DD HH24:MI:SS')";
                    sqlNUMmAX += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59','YYYY/MM/DD HH24:MI:SS')";

                    dtNUMmAX = crud.ExecQuery(sqlNUMmAX);
                    DataRow drMax = dtNUMmAX.Rows[0];
                    int n = Convert.ToInt16(drMax[0]);
                    dt = crud.ExecQuery(sql);
                    if (n > 0)
                    {
                        for (int i = 1; i <= n; i++)
                        {
                            DataColumn dcolColumnAgent = new DataColumn("Agent " + i, typeof(string));
                            DataColumn dcolColumnAgtName = new DataColumn("Agent_name " + i, typeof(string));
                            DataColumn dcolColumnComPer = new DataColumn("CMB_PERCENTAGE " + i, typeof(string));
                            DataColumn dcolColumnCom = new DataColumn("Com_amount " + i, typeof(string));
                            dt.Columns.Add(dcolColumnAgent);
                            dt.Columns.Add(dcolColumnAgtName);
                            dt.Columns.Add(dcolColumnComPer);
                            dt.Columns.Add(dcolColumnCom);
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            int no = Convert.ToInt16(row["NUMBER_OF_COMMISSION"]);
                            if (no > 0)
                            {
                                string sqlComBre = "SELECT * FROM VIEW_PRE_COM_BREK_DOWN where DEB_DEB_NOTE_NO='" + row["DN_CN"] + "'";
                                dtCOM = crud.ExecQuery(sqlComBre);
                                int i = 0;
                                foreach (DataRow rowDetail in dtCOM.Rows)
                                {
                                    i += 1;
                                    row["Agent " + i] = rowDetail["AGENT"];
                                    row["Agent_name " + i] = rowDetail["AGENT_NAME"];
                                    row["CMB_PERCENTAGE " + i] = rowDetail["CMB_PERCENTAGE"];
                                    row["Com_amount " + i] = rowDetail["COM_AMOUNT"];
                                }
                            }
                        }
                    }

                    dataGridView1.DataSource = dt;
                    Cursor.Current = Cursors.AppStarting;
                }
                catch (Exception ex) { Msgbox.Show("Invalid Parameter \n" + ex.ToString()); }
            }

            else
            {
                string cs = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;
                using (OracleConnection con = new OracleConnection(cs))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    con.Open();
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MPRT";

                    cmd.Parameters.Add("P_PRO_CODE", OracleDbType.NVarchar2).Value = cboprdcode.Text;
                    cmd.Parameters.Add("P_RE", OracleDbType.NVarchar2).Value = cboreport.Text;
                    //cmd.Parameters.Add("P_DATETO", OracleDbType.NVarchar2).Value = dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    //cmd.Parameters.Add("P_DATEFR", OracleDbType.NVarchar2).Value = dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    cmd.Parameters.Add("P_DATETO", OracleDbType.NVarchar2).Value = dtpTo.Value.ToString("yyyy/MM/dd")+" 23:59:59";
                    cmd.Parameters.Add("P_DATEFR", OracleDbType.NVarchar2).Value = dtpFrom.Value.ToString("yyyy/MM/dd")+" 00:00:00";
                    cmd.Parameters.Add("P_DEC", OracleDbType.NVarchar2).Value = cboprdcode.SelectedValue;
                    OracleParameter OUA = cmd.Parameters.Add("DATAOUTPUT", OracleDbType.RefCursor);
                    OUA.Value = DateTime.Now;
                    OUA.Direction = ParameterDirection.Output;

                    try
                    {
                        // DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());

                        dataGridView1.DataSource = dt;



                    }
                    catch (Exception ex)
                    {
                        Msgbox.Show(ex.Message + "\n" + ex.Source);
                    }

                    Cursor.Current = Cursors.AppStarting;
                }
            }
        }

        private void MReport_Load(object sender, EventArgs e)
        {
            BindComboBox1();
            BindComboBox2();
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(dt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dataGridView1);
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotalNum.Text = dataGridView1.RowCount.ToString();
        }


    }
}
