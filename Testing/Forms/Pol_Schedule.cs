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
    public partial class Pol_Schedule : Form
    {
        public string filename = "";
        public string Pro_code = "";
        public DataSet dataReport = new DataSet();
        //public Reports.CanCkoReport myDataReportCan = new Reports.CanCkoReport();
        public Reports.Pol_Schedule myDataReportPol = new Reports.Pol_Schedule();
        public Pol_Schedule()
        {
            InitializeComponent();
        }

        private void lbPolicyNo_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (txtPol.Text == "")
                {
                    Msgbox.Show("Please Make Sure your PolicyNo. is not empty!");
                    txtPol.Focus();
                }
                else
                {
                    Pro_code = txtPol.Text.Trim().ToUpper().Substring(7, 3);
                    filename = txtPol.Text.Replace("/", "-") + Pro_code + DateTime.Now.ToString("dd-MM-yyyy");
                    if (Pro_code == "PAC")
                    {
                        string sql, sql1, sql2, sql3,sql4 = "";
                        DataTable dt = new DataTable();

                        CRUD crud = new CRUD();

                        Cursor.Current = Cursors.WaitCursor;

                        sql = "SELECT * FROM VIEW_CUSTOMER where POL_POLICY_NO ='" + txtPol.Text + "'";

                        DataTable dt1 = new DataTable();
                        DataTable dt2 = new DataTable();
                        DataTable dt3 = new DataTable();
                        DataTable dt4 = new DataTable();
                        sql2 = "SELECT * FROM VIEWCLAUSEPOL where POL_POLICY_NO ='" + txtPol.Text + "'";
                        sql3 = "SELECT * FROM VIEWINFOPOL where POL_POLICY_NO ='" + txtPol.Text + "'";
                        sql1 = "SELECT * FROM VIEW_RISK_POL where POL_POLICY_NO ='" + txtPol.Text + "'";
                        sql4 = "SELECT * FROM VIEWTRPOLID where POL_POLICY_NO ='" + txtPol.Text + "'";
                        dt = crud.ExecQuery(sql);
                        DataTable dtTempt = new DataTable();
                        dtTempt = dt.Copy();

                        dataReport.Tables.Clear();
                        dataReport.Tables.Add(dtTempt);
                        dt1 = crud.ExecQuery(sql1);
                        dtTempt = dt1.Copy();
                        dataReport.Tables.Add(dtTempt);
                        dt2 = crud.ExecQuery(sql2);
                        dtTempt = dt2.Copy();
                        dataReport.Tables.Add(dtTempt);
                        dt3 = crud.ExecQuery(sql3);
                        dtTempt = dt3.Copy();
                        dataReport.Tables.Add(dtTempt);
                        dt4 = crud.ExecQuery(sql4);
                        dtTempt = dt4.Copy();
                        dataReport.Tables.Add(dtTempt);


                        myDataReportPol.SetDataSource(dataReport);
                        crystalReportViewer1.ReportSource = myDataReportPol;

                        Cursor.Current = Cursors.AppStarting;
                    }
                    else if (Pro_code == "CAN")
                    {
                        string sqlCan, sqlage  = "";
                        string sqlpayid = "";
                        DataTable dtcan = new DataTable();
                        DataTable dtage = new DataTable();
                        DataTable dtpayid = new DataTable();
                        CRUD crud = new CRUD();
                        Cursor.Current = Cursors.WaitCursor;

                        sqlCan = "SELECT * FROM VIEWCANPOL where POL_POLICY_NO ='" + txtPol.Text + "'";
                        sqlage = "SELECT * FROM VIEWAGEBAND where POL_POLICY_NO ='" + txtPol.Text + "'";
                        sqlpayid = "SELECT * FROM VIEWTRPOLID where POL_POLICY_NO ='" + txtPol.Text + "'";
                        
                        dataReport.Tables.Clear();
                        dtcan = crud.ExecQuery(sqlCan);
                        DataTable dtTempt = new DataTable();
                        dtTempt = dtcan.Copy();
                        dataReport.Tables.Add(dtTempt);
                        dtage = crud.ExecQuery(sqlage);
                        dtTempt = dtage.Copy();
                        dataReport.Tables.Add(dtTempt);
                        dtpayid = crud.ExecQuery(sqlpayid);
                        dtTempt = dtpayid.Copy();
                        dataReport.Tables.Add(dtTempt);

                        //if (dtcan.Rows.Count <= 1)
                        //{
                        //    myDataReportCan.SetDataSource(dataReport);
                        //    crystalReportViewer1.ReportSource = myDataReportCan;
                        //    Cursor.Current = Cursors.AppStarting;
                        //}
                        //else
                        //{
                        //    Msgbox.Show("it is invalid!!!");
                        //    clear();
                        //    return;
                        //}
                        

                    }
                    else
                    {
                        Msgbox.Show("Sorry,this policy  " + txtPol.Text + "  has not yet allowed to print !!!");
                        txtPol.Text = "";
                    }


                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
           
           
            
        }
        public void savepdf()
        {
            try
            {
               
                

                if (Pro_code == "CAN")
                {
                    //myDataReportCan.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"\\fipnhdbs11\Infoins_IMS_Upload_doc$\Pipay File\" + filename + ".pdf");
                }
                else if (Pro_code == "PAC")
                {
                    myDataReportPol.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"\\fipnhdbs11\Infoins_IMS_Upload_doc$\Pipay File\" + filename + ".pdf");
                }
            }
            catch(Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
           
           
            //System.Diagnostics.Process.Start(@"D:\" + filename + ".pdf");
            
        }

        private void btnMail_Click(object sender, EventArgs e)
        {

            if (txtPol.Text == "" || crystalReportViewer1.ReportSource == null)
           {
               Msgbox.Show("Data is empty, it could not send mail !!!!");
               return;
           }
           FrmSchSendMail email = new FrmSchSendMail();
           email.pol = this;
           email.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           clear();
        }
        public void clear()
        {
            txtPol.Text = "";
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.Refresh();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
