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
            try
            {
                Cursor = Cursors.WaitCursor;

                var isBrokerTeam = false;
                var dtBrokerTeams = crud.LoadData("select [GROUP] from tbDOC_USER where USER_NAME = '" + frmLogIn.Usert.ToUpper() + "'").Tables[0];

                if (dtBrokerTeams.Rows.Count > 0)
                {
                    isBrokerTeam = dtBrokerTeams.Rows[0]["GROUP"].ToString().Equals("BROKERTEAM");
                }

                if (isBrokerTeam)
                {
                    var queryBuilder = new StringBuilder();
                    queryBuilder.Append("select * from view_main_report ")
                        .Append("where USER_CODE in ( ")
                        .AppendFormat("select [USER_CODE] from [DocumentControlDB].[dbo].tbDOC_USER where [USER_NAME] = '{0}' ", frmLogIn.Usert.ToUpper())
                        .Append("union all ")
                        .Append("SELECT [USER_CODE] ") 
                        .Append("FROM [DocumentControlDB].[dbo].[tbDOC_USER] ")
                        .Append("where [GROUP] = 'BROKERTEAM' ")
                        .AppendFormat("and Parent like (select isnull (PARENT, '') + USER_CODE + '.' as PARENT from [DocumentControlDB].[dbo].tbDOC_USER where [USER_NAME] = '{0}') + '%') ", frmLogIn.Usert.ToUpper())
                        .Append("and convert(datetime,CREATE_DATE,103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') ")
                        .Append("and convert(datetime,CREATE_DATE,103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59') ")
                        .Append("order by user_code");

                    dt = crud.LoadData(queryBuilder.ToString()).Tables[0];
                    dgv.DataSource = dt;
                }
                else
                {
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

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(ex.ToString());
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
