using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmAutoMonthlyReport : Form
    {
        CRUD crud = new CRUD();
        private DataTable result;

        public frmAutoMonthlyReport()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            result = new DataTable();
            if (cboreport.Text == "PREMIUM_REGISTER")
            {
                CRUD crud = new CRUD();

                DataTable dtCOM = new DataTable();
                try
                {
                    Cursor.Current = Cursors.WaitCursor;



                    string sql = "SELECT * FROM VIEW_AUTO_PREMIUM_REGISTER where";
                    sql += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00','YYYY/MM/DD HH24:MI:SS')";
                    sql += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59','YYYY/MM/DD HH24:MI:SS')";
                    result = crud.ExecQuery(sql);

                    //DataTable dtNUMmAX = new DataTable();
                    //string sqlNUMmAX = "SELECT MAX(NUMBER_OF_COMMISSION) FROM VIEW_PRE_REGISTER_BREAK_DOWN where";
                    //sqlNUMmAX += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";
                    //sqlNUMmAX += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";
                    //sqlNUMmAX += " and MAIN_CLASS = 'AUTOMOBILE'";

                    //dtNUMmAX = crud.ExecQuery(sqlNUMmAX);
                    //DataRow drMax = dtNUMmAX.Rows[0];
                    //int n = Convert.ToInt16(drMax[0]);
                    //if (n > 0)
                    //{
                    //    for (int i = 1; i <= n; i++)
                    //    {
                    //        DataColumn dcolColumnAgent = new DataColumn("Agent " + i, typeof(string));
                    //        DataColumn dcolColumnAgtName = new DataColumn("Agent_name " + i, typeof(string));
                    //        DataColumn dcolColumnComPer = new DataColumn("CMB_PERCENTAGE " + i, typeof(string));
                    //        DataColumn dcolColumnCom = new DataColumn("Com_amount " + i, typeof(string));
                    //        result.Columns.Add(dcolColumnAgent);
                    //        result.Columns.Add(dcolColumnAgtName);
                    //        result.Columns.Add(dcolColumnComPer);
                    //        result.Columns.Add(dcolColumnCom);
                    //    }
                    //    foreach (DataRow row in result.Rows)
                    //    {
                    //        int no = Convert.ToInt16(row["NUMBER_OF_COMMISSION"]);
                    //        if (no > 0)
                    //        {
                    //            string sqlComBre = "SELECT * FROM VIEW_PRE_COM_BREK_DOWN where DEB_DEB_NOTE_NO='" + row["DN_CN"] + "'";
                    //            dtCOM = crud.ExecQuery(sqlComBre);
                    //            int i = 0;
                    //            foreach (DataRow rowDetail in dtCOM.Rows)
                    //            {
                    //                i += 1;
                    //                row["Agent " + i] = rowDetail["AGENT"];
                    //                row["Agent_name " + i] = rowDetail["AGENT_NAME"];
                    //                row["CMB_PERCENTAGE " + i] = rowDetail["CMB_PERCENTAGE"];
                    //                row["Com_amount " + i] = rowDetail["COM_AMOUNT"];
                    //            }
                    //        }
                    //    }
                    //}

                    dgvResult.DataSource = result;
                    Cursor.Current = Cursors.AppStarting;
                }
                catch (Exception ex) { Msgbox.Show("Invalid Parameter \n" + ex.ToString()); }
            }

            else
            {
                
            }
        }

        private void frmAutoMonthlyReport_Load(object sender, EventArgs e)
        {
            cboreport.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvResult.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(result, "");
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
            dgvResult.DataSource = null;
            dgvResult.Rows.Clear();
        }

        private void dgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvResult);
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotalNum.Text = dgvResult.RowCount.ToString();
        }
    }
}
