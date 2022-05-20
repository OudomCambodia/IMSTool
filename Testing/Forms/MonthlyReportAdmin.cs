using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class MonthlyReportAdmin : Form
    {
        
        CRUD crud = new CRUD();
        private DataTable dt;
        DataTable dt_textemail;
        private static string FileName = "";
        int port;
        int button;
        //string fpath;
        public MonthlyReportAdmin()
        {
            InitializeComponent();
        }

        private void MonthlyReportAdmin_Load(object sender, EventArgs e)
        {
            for (int i = 2015; i <= Convert.ToInt32(DateTime.Now.ToString("yyyy")); i++)
            {
                cbYear.Items.Add(i.ToString());
            }
            cbMonth.Text = DateTime.Now.ToString("MMMM");
            cbYear.Text = DateTime.Now.ToString("yyyy");
            ComboBox_Load();
            load_textemail(new string[] { DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString() });
        }
        public void ComboBox_Load()
        {
            DataTable dt_cb = new DataTable();
            dt_cb.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(string)), new DataColumn("Name", typeof(string)) });
            dt_cb.Rows.Add("N0001", "Nguon Noramy");
            dt_cb.Rows.Add("N0002", "Sopheakanitha");

            //Assign DataTable as DataSource.
            cbReportType.DataSource = dt_cb;
            cbReportType.DisplayMember = "Name";
            cbReportType.ValueMember = "Id";
            cbReportType.SelectedIndex = 0;
            //cbyear items
            
        }

        public void load_textemail(string[] replace_text)
        {
            try
            {

                dt_textemail = crud.ExecQuery("select * from user_monthly_report_details where report_type = '" + 0 + "'");
                if (dt_textemail.Rows.Count != 0)
                {
                    tbTO.Text = dt_textemail.Rows[0]["EMAIL_TO"].ToString();
                    tbCC.Text = dt_textemail.Rows[0]["EMAIL_CC"].ToString();
                    tbSubject.Text = dt_textemail.Rows[0]["EMAIL_SUBJ"].ToString().Replace("@report_month@", replace_text[0]).Replace("@report_year@", replace_text[1]).Replace("@newline@", "\r\n");
                    tbCONTENT.Text = dt_textemail.Rows[0]["EMAIL_CONTENT"].ToString().Replace("@report_month@", replace_text[0]).Replace("@report_year@", replace_text[1]).Replace("@newline@", "\r\n");
                }
                else
                {
                    Msgbox.Show("No Broker or Agent Name to send report please check database user_monthly_report_details!");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }


        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_textemail(new string[] { cbMonth.Text, cbYear.Text });
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_textemail(new string[] { cbMonth.Text, cbYear.Text });

        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_textemail(new string[] { cbMonth.Text, cbYear.Text });


        }

        private void bnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ConvertExcel();
                Cursor.Current = Cursor.Current = Cursors.AppStarting;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot send Message: " + ex.Message);
            }
        }

        private void ConvertExcel(string path = "")
        {
            CRUD crud = new CRUD();
            //string sql = "select * from USER_MONTHLY_REPORT_UMNP where ";
            string sql = re_sql(cbReportType.Text);
            if (rbMonth.Checked == true)
            {
                sql += "trim(ACC_MONTH) = '" +cbMonth.Text.ToUpper()+"-"+cbYear.Text.Substring(2,2)+ "'";
            }
            else
            {
                sql += "TRUNC(TRN_DATE) >= '" + dtpFrom.Value.ToString("dd-MMM-yyyy") + "' and TRUNC(TRN_DATE) <= '" + dtpTo.Value.ToString("dd-MMM-yyyy") + "'";
            }
            dt = new DataTable();
            Msgbox.Show(sql);
            dt = crud.ExecQuery(sql);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("No data found in this period.");
                return;
            }
            FileName = fname(cbReportType.Text);
          
            My_DataTable_Extensions.ExportToExcelXMLSharepoint(dt, FileName, "https://forteinsurancegroup.sharepoint.com/sites/forteinsurance/Shared Documents/MIS/Samnang/Monthly Report" + txtSavedPath.Text);

        }

        private string fname(string a)
        {
            string temp; 
            if (rbMonth.Checked == true)
                temp = "Business Achievement of " + a + cbMonth.Text.ToUpper() + "-" + cbYear.Text.Substring(2, 2);
            else
                temp = "Business Achievement of " + a + dtpFrom.Value.Month.ToString().ToUpper()+"-"+dtpTo.Value.Year.ToString() ;

            return temp;
        }
        private string re_sql(string a)
        {
            string sql="";
            if (a == "Nguon Noramy")
                sql = "select * from USER_MONTHLY_REPORT_UMNP where ";
            else if (a == "Sopheakanitha")
                sql = "select * from USER_MONTHLY_REPORT_JPN where ";

            return sql;
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth.Checked == true)
            {
                cbMonth.Enabled = true;
                cbYear.Enabled = true;
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
            else
            {
                cbMonth.Enabled = false;
                cbYear.Enabled = false;
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }

        }

        

    }
}
