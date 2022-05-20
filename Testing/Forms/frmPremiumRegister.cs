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
    public partial class frmPremiumRegister : Form
    {
        public frmPremiumRegister()
        {
            InitializeComponent();
        }
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        DataTable dtCOM = new DataTable();
      

        private void frmPremiumRegister_Load(object sender, EventArgs e)
        {
           // txtParameter.Text = "TO_DATE(TRN_DATE,'YYYY/MM/DD HH:MI:SS') >= TO_DATE('2018/01/01 00:00:00','YYYY/MM/DD HH24:MI:SS') AND TO_DATE(TRN_DATE ,'YYYY/MM/DD HH:MI:SS') <= TO_DATE('2018/01/31 23:59:59','YYYY/MM/DD HH24:MI:SS')";
     
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
             
                string sql = "SELECT * FROM VIEW_PRE_REGISTER_BREAK_DOWN where";
                sql += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";
                sql += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";

                if (txtParameter.Text != string.Empty) {

                    sql +=" "+ txtParameter.Text;
                }

                DataTable dtNUMmAX = new DataTable();
                string sqlNUMmAX = "SELECT MAX(NUMBER_OF_COMMISSION) FROM VIEW_PRE_REGISTER_BREAK_DOWN where";
                sqlNUMmAX += " TRN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";
                sqlNUMmAX += " and TRN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";

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
                        DataColumn dcolColumnCom = new DataColumn("Com_amount " + i, typeof(string));
                        dt.Columns.Add(dcolColumnAgent);
                        dt.Columns.Add(dcolColumnAgtName);
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
                                row["Com_amount " + i] = rowDetail["COM_AMOUNT"];
                            }
                        }
                    }
                }
                dataGridView.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    My_DataTable_Extensions.ExportToExcel(dt, "");
                }

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex) { Msgbox.Show("Invalid Parameter \n" + ex.ToString()); }
        }
    }
}
