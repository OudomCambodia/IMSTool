using CrystalDecisions.CrystalReports.Engine;
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
    public partial class Latest_List_of_Insured_Members : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        string sql;
        public string UserName = "SICL";

        public Latest_List_of_Insured_Members()
        {
            InitializeComponent();
        }

        private void Latest_List_of_Insured_Members_Load(object sender, EventArgs e)
        {
            //txtPolicy_Leave(sender, e);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Query();
            }
            catch(Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void txtPolicy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Query();
        }

        private void Query()
        {
            try
            {
                string sub_class = "";
                if (txtPolicy.Text == string.Empty && txtProposal.Text == string.Empty) 
                {
                    Msgbox.Show("Please enter policy no or proposal no!");
                    return;
                }

                if (txtPolicy.Text != string.Empty)
                {
                   if (txtPolicy.Text.Length < 20)
                    {
                        Msgbox.Show("Invalid Policy No!");
                        return;
                    }
                   sub_class = txtPolicy.Text.Trim().ToUpper().Substring(7, 3);
                   
                }

                if (txtProposal.Text != string.Empty)
                {   
                    if (txtProposal.Text.Length < 15)
                    {
                        Msgbox.Show("Invalid Proposal No!");
                        return;
                    }
                    //P00517CYC100003
                    sub_class = txtProposal.Text.Trim().ToUpper().Substring(6, 3);
                  
                }



                sql = "";

                Cursor.Current = Cursors.WaitCursor;


                //  sql = "SELECT V.* from VIEW_EMC_LATEST_LIST V WHERE rownum<50001 and POLICY_NO='D/001/HBHP/17/100001'";

                //     sql = "SELECT V.* from VIEW_EMC_LATEST_LIST V WHERE rownum<50001 and POLICY_NO='" + txtPolicy.Text.Trim().ToUpper() + "'";
               
               
                string View = "";
                string Condition = "";
                ReportClass myDataReport = new ReportClass();
                if (sub_class == "GPA" || sub_class == "PAC")
                {
                    View = "VIEW_GPA_LATEST_LIST";
                    //myDataReport = new Reports.GPA_Latest_List();
                    if (UserName != "C-CLC") 
                        myDataReport = new Reports.GPA_Latest_List();
                    else myDataReport = new Reports.GPA_Latest_List_For_CLC();
                    Condition = "PRS_POLICY_NO";
                }
                else if (sub_class == "CYC" || sub_class == "VPC" || sub_class == "VCM")
                {
                    View = "VIEW_AUTO_LASTEST_LIST";
                    myDataReport = new Reports.Auto_Latest_List();
                    Condition = "PRS_POLICY_NO";
                }
                else if (sub_class == "HNS" || sub_class == "EMC" || sub_class == "BHP" || sub_class == "MED" || sub_class == "CVD")
                {
                    View = "VIEW_EMC_LATEST_LIST";
                    //myDataReport = new Reports.EMC_Latest_List();
                    if (UserName != "C-CLC")
                        myDataReport = new Reports.EMC_Latest_List();
                    else myDataReport = new Reports.EMC_Latest_List_For_CLC();
                    Condition = "POLICY_NO";
                }
                else
                {
                    Msgbox.Show("This Policy No is not the type of group policy!");
                    return;
                }

                if (txtPolicy.Text != string.Empty)
                {
                    sql = "SELECT V.* from " + View + " V WHERE rownum < 50001 and " + Condition + " = '" + txtPolicy.Text.Trim().ToUpper() + "'";
                }
                if (txtProposal.Text != string.Empty)
                {
                    sql = "SELECT V.* from " + View + " V WHERE rownum < 50001 and Proposal = '" + txtProposal.Text.Trim().ToUpper() + "'";
                }

                //  sql = "SELECT V.* from VIEW_GPA_LATEST_LIST V WHERE rownum<50001 and PRS_POLICY_NO='D/001/CGPA/17/100009'";

                //sql += " and Notified_Date >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS')";
                //sql += " and Notified_Date <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS')";
                // sql += " order by rownum, Notified_Date, PAYEE_NAME";

                dt = crud.ExecQuery(sql);

                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("Policy is incorrect or in cancelled status!");
                    return;
                }
                //  dtPlan = crud.ExecQuery(sqlPlan);

                //int num = 1;
                // for( int i = 0 ;i< dt.Rows.Count; i++)
                //{
                //    string a = dt.Rows[i]["INSURED_MEMBER"].ToString();
                //    string b = dt.Rows[i]["DEPENDENT"].ToString();
                //    if (a==b) 
                //    {
                //        dt.Rows[i]["No"] = num;
                //        num = num + 1;
                //    }
                //}

                //foreach (DataTable thisTable in dataset1.TableName)
                //{
                //    foreach (DataRow dr in thisTable.Rows)
                //    {
                //        if (dr["INSURED_MEMBER"] == dr["DEPENDENT"])
                //        {
                //            dr["No"] = " 1 ";
                //        }       
                //    }
                //}

                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (dr["INSURED_MEMBER"] == dr["DEPENDENT"]) 
                //    {
                //        dr["No"] = " 1 ";
                //    }                
                //}

                Cursor.Current = Cursors.WaitCursor;
                DataTable dtTempt = new DataTable();
                dtTempt = dt.Copy();
                DataSet dataReport = new DataSet();
                dataReport.Tables.Clear();
                dataReport.Tables.Add(dtTempt);
                //  Reports.ClaimPaidReportPayee myDataReport = new Reports.ClaimPaidReportPayee();
                //  Reports.EMC_Latest_List myDataReport = new Reports.EMC_Latest_List();

                myDataReport.SetDataSource(dataReport); 
                crystalReportViewer1.ReportSource = myDataReport;
                
                Cursor.Current = Cursors.AppStarting;
                //     dgClaimPaid.DataSource = dt;

                //for (int i = 0; i < dgClaimPaid.Columns.Count; i++)
                //    dgClaimPaid.Columns[i].Width = 150;

                //  for (int i = 0; i < fieldNames.Count(); i++)
                //    dgClaimPaid.Columns[i].HeaderText = fieldNames[i];

                //dgClaimPaid.Columns[0].Width = 50;
                //dgClaimPaid.Columns[2].Width = 60;
                //dgClaimPaid.Columns[5].Width = 50;
                //dgClaimPaid.Columns[7].Width = 120;
                //dgClaimPaid.Columns[8].Width = 120;
                //dgClaimPaid.Columns[1].DefaultCellStyle.Format = "dd/MMM/yyyy";
                //dgClaimPaid.Columns[9].DefaultCellStyle.Format = "dd/MMM/yyyy";

                Cursor.Current = Cursors.AppStarting;
                //   lblTotal.Text = "Total Record(s): " + dgClaimPaid.Rows.Count.ToString();

                //    if (dgClaimPaid.Rows.Count > 50000)
                {
                    //        Msgbox.Show("System allow to query only 50000 records,the result is exceeded there will be missing some records. Please contact IMS team to get full data.");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void txtProposal_TextChanged(object sender, EventArgs e)
        {
            txtPolicy.Text = string.Empty;
            txtProposal.Focus();

        }

        private void txtPolicy_TextChanged(object sender, EventArgs e)
        {
            txtProposal.Text = string.Empty;
            txtPolicy.Focus();
        }

        private void txtProposal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Query();
        }

        private void Latest_List_of_Insured_Members_Activated(object sender, EventArgs e)
        {
            txtProposal.Focus();
        }
    }
}
