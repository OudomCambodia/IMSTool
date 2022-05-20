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
    public partial class frmDCSMainReport : Form
    {
        public frmDCSMainReport()
        {
            InitializeComponent();
        }

        DBS11SqlCrud crud = new DBS11SqlCrud();
        DataTable dt = new DataTable();
        public string Team="";
        private void bnSearch_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dt = crud.LoadData("SELECT * from dbo.VIEW_MAIN_REPORT " +
            //    "where convert(datetime,CREATE_DATE,103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') " +
            //    "and convert(datetime,CREATE_DATE,103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59') ").Tables[0];
            //dgv.DataSource = dt;
            //Cursor.Current = Cursors.AppStarting;
            //if (dgv.RowCount <= 0)
            //    Msgbox.Show("No data found.");
            string main_rpt = "SELECT * from dbo.VIEW_MAIN_REPORT " +
                        "where convert(datetime,CREATE_DATE,103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') " +
                        "and convert(datetime,CREATE_DATE,103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59') ";
            string[] TeamSplit = Team.Split(',');
            if (!String.IsNullOrEmpty(Team) && frmAddDocument1.product.ContainsValue(TeamSplit[0]))
            {
                string ProType = "";
                bool check = false;
                foreach (string t in TeamSplit)
                {
                    foreach (KeyValuePair<string, string> entry in frmAddDocument1.product)
                    {
                        if (entry.Value == t)
                        {
                            check = true;
                            ProType += "'" + entry.Key + "',";
                        }
                    }
                }
                if (check)
                {
                    ProType = ProType.Remove(ProType.Length - 1);
                    main_rpt += " AND PRODUCT_TYPE IN (" + ProType + ")";
                    dt = crud.LoadData(main_rpt).Tables[0];
                    dgv.DataSource = dt;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            dgv.DataSource = null;
            dgv.Rows.Clear();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                //My_DataTable_Extensions.ExportToExcel(dt, "");
                My_DataTable_Extensions.ExportToExcelXML(dt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found.");
            }
        }

        private void frmDCSMainReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }
    }
}
