
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

namespace Testing
{
    public partial class frmSendEmail : Form
    {
        private static string FileName = "RI Pending as of " + DateTime.Now.ToString("dd-MM-yyyy");
        
        private DataTable dt;
        private bool Loaded = false;
        private string Content = "";
        int port;
        string HashPass = "Forte@2017";
        public frmSendEmail()
        {
            InitializeComponent();
        }

        private void bnSend_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to send the RI Pending email?", "Confirmation", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.No)
                    return;

                //string SavedPath = @"I:\Samnang\Monthly Report\RI Pending\" + FileName + ".xlsx";
                string SavedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + FileName + ".xlsx";
                Cursor.Current = Cursors.WaitCursor;

                if (File.Exists(SavedPath) == true)
                {
                    DialogResult dr2 = MessageBox.Show("The file already exists. Do you want to continue and resend the email?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dr2 == System.Windows.Forms.DialogResult.No)
                        return;
                }

                ConvertExcel(SavedPath);

                CRUD crud = new CRUD();
                DataTable tmptb = crud.ExecQuery("SELECT SMTP_CLIENT, PORT FROM USER_CLAIM_EMAIL_INFO");

                //first by initializing a new SmtpClient and pass the server ip/hostname to it
                //SmtpClient client = new SmtpClient("192.168.110.251");
                SmtpClient client = new SmtpClient(tmptb.Rows[0][0].ToString());

                //then initialize a mail message
                MailMessage message = new MailMessage();

                //lets set the from field to the from text field
                message.From = new MailAddress("support.infoins@forteinsurance.com");

                //and the to field
                //string[] tempt = dt.AsEnumerable().Where(ite => ite.Field<string>("SFC_DPT_CODE") != "UW" && ite.Field<string>("SFC_DPT_CODE") != "RGN").Select(ite => ite.Field<string>("EMAIL")).Distinct().ToArray();
                string[] tempt = tbTO.Text.ToString().Split(';');
                foreach (string str in tempt)
                    message.To.Add(str);

                //cc field
                string[] ccList = tbCC.Text.ToString().Split(';');
                foreach (string str in ccList)
                    message.CC.Add(str.Trim());

                //attached file
               
                Attachment att = new Attachment(SavedPath);
                message.Attachments.Add(att);

                // the body
                message.Body = tbCONTENT.Text;

                // the subject
                message.Subject = tbSubject.Text;
                //send email update 10-12-2021
                client.Host = tmptb.Rows[0][0].ToString();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                DataTable dtUserEmail = crud.ExecQuery("select MAIL_ADDRESS,MAIL_PASSWORD from user_claim_email_info where SMTP_CLIENT = 'smtp.office365.com'");
               
                client.Credentials = new System.Net.NetworkCredential
                {
                    //UserName = "no-reply@forteinsurance.com",
                    //Password = "Forte@12345"
                    //UserName = "support.infoins@forteinsurance.com",
                    //Password = "pw-IBU@321"
                    UserName = dtUserEmail.Rows[0][0].ToString(),
                    Password = Cipher.Decrypt(dtUserEmail.Rows[0][1].ToString(), HashPass).ToString()
                };

                ///////

                System.Net.ServicePointManager.Expect100Continue = true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ///

                client.EnableSsl = true;
                port = Convert.ToInt32(tmptb.Rows[0][1]);
                client.Port = port;
                client.Send(message);
                message.Dispose();
                client.Dispose();
                ////now we create a networkcredential and pass our username and password for the smtp server
                //client.Credentials = new System.Net.NetworkCredential("support.infoins@forteinsurance.com", "IBU@321");

                ////many smtp require ssl so we will enable it
                //client.EnableSsl = false;

                ////we will set the port
                //client.Port = Convert.ToInt32(tmptb.Rows[0][1]);

                //// and finally, send the message
                //client.Send(message);

                Cursor.Current = Cursors.AppStarting;
                MessageBox.Show("Email Sent!");
                
                File.Delete(SavedPath);
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot send Message: " + ex.Message);
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbCC.Text = "";
            tbCONTENT.Text = "";
            tbSubject.Text = "";
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

        private void frmSendEmail_Load(object sender, EventArgs e)
        {
            for (int i = 2015; i <= Convert.ToInt32(DateTime.Now.ToString("yyyy")); i++)
            {
                cbYear.Items.Add(i.ToString());
            }

            CRUD crud = new CRUD();
            DataTable dt = crud.ExecQuery("select * from USER_PENDING_RI_EMAIL");
            cbMonth.Text = DateTime.Now.ToString("MMMM");
            cbYear.Text = DateTime.Now.ToString("yyyy");
            tbSubject.Text = FileName;
            foreach (DataRow dr in dt.Rows)
            {
                ((TextBox)this.Controls.Find("tb" + dr["TYPE"].ToString(), true)[0]).Text = dr["CONTENT"].ToString();
            }
            Content = tbCONTENT.Text.ToString().Replace("@Date@", FileName).Replace("@newLine@", "\r\n");
            tbCONTENT.Text = Content;
            Loaded = true;
        }

        private void ConvertExcel(string path = "")
        {
            CRUD crud = new CRUD();
            string sql = "select * from VIEW_RI_PENDING where ";
            if (rbMonth.Checked == true)
            {
                sql += "trim(MONTH) = '" + cbMonth.Text + "' and trim(YEAR) = '" + cbYear.Text + "'";
            }
            else
            {
                sql += "TRUNC(CREATED_DATE) >= '" + dtpFrom.Value.ToString("dd-MMM-yyyy") + "' and TRUNC(CREATED_DATE) <= '" + dtpTo.Value.ToString("dd-MMM-yyyy") + "'";
            }
            dt = new DataTable();
            Msgbox.Show(sql);
            dt = crud.ExecQuery(sql);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("No data found in this period.");
                return;
            }

            //My_DataTable_Extensions.ExportToExcelXML(dt, path);
            
            My_DataTable_Extensions.ExportToExcelXMLSharepoint(dt,FileName, "https://forteinsurancegroup.sharepoint.com/sites/forteinsurance/Shared Documents/MIS/Samnang/Monthly Report/RI Pending");
            
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loaded == false)
                return;

            if ((cbMonth.SelectedIndex + 1).ToString("D2") == DateTime.Now.ToString("MM") && cbYear.Text == DateTime.Now.ToString("yyyy"))
                FileName = "RI Pending as of " + DateTime.Now.ToString("dd-MM-yyyy");              
            else
                FileName = "RI Pending for " + (cbMonth.SelectedIndex + 1).ToString("D2") + "-" + cbYear.Text;

            tbSubject.Text = FileName;
            tbCONTENT.Text = Content;
        }

        private void gbPeriod_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
