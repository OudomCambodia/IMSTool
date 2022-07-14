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
using System.IO;
using System.Net.Mail;
using System.Net.Mime;


namespace Testing.Forms
{
    public partial class TicketRequest : Form
    {
        string filep;
        string a, b;
        CRUD crud = new CRUD();
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        public string Username = "a";
        string HashPass = "Forte@2017";
        //int count_ticket;
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        int count;
        public TicketRequest()
        {
            InitializeComponent();
        }

        private void TicketRequest_Load(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvTicketView);
            txtUsername.Text = Username;
            if (Username == "ADMIN")
            {
                btnViewTicket.Visible = true;
            }

            DataSet dtTicketView = sqlcrud.LoadData(" select [ticketID],[Subject],[Reason],[Status],[Owner],[Describe] FROM [DocumentControlDB].[dbo].[tbTicketRequests] WHERE [Requestor] = '" + txtUsername.Text.ToUpper() + "' order by [ticketID] desc");
            dgvTicketView.DataSource = dtTicketView.Tables[0];
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            this.dgvFile.ForeColor = System.Drawing.Color.Black;
            OpenFileDialog ofdUpload = new OpenFileDialog();
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            ofdUpload.Multiselect = true;
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvFile.Rows.Count; i++)
                    {
                        if (dgvFile.Rows[i].Cells[0].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }
                    dgvFile.Rows.Add(filename, path);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            
            try
            {

                DataTable dt = sqlcrud.LoadData("select TOP 1 [CountTicket] from [DocumentControlDB].[dbo].[tbTicketRequests] where [CreateDate] = '" +
                            DateTime.Now.ToShortDateString() + "' and [Requestor] = '" + txtUsername.Text + "' ORDER BY [ticketID] desc  ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["CountTicket"].ToString()) == 3)
                    {
                        Msgbox.Show("Limit create ticket has reached ! Please contact admin.");
                    }

                }

                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0]["CountTicket"].ToString()) < 3)
                {
                    


                        if ((cbOwner.Text != "" && cbStatus.Text != "") || txtDescribe.Text == "")
                        {
                            //Check DGVfile file 

                            for (int i = 0; i < dgvFile.Rows.Count; i++)
                            {

                                copyfile(dgvFile.Rows[i].Cells[1].Value.ToString(), dgvFile.Rows[i].Cells[0].Value.ToString());
                                filep += dgvFile.Rows[i].Cells[1].Value.ToString() + "\\" + dgvFile.Rows[i].Cells[0].Value.ToString() + ";";
                            }

                            ////////////
                            sqlcrud.Executing("INSERT INTO [DocumentControlDB].[dbo].[tbTicketRequests]" +
                            "([Requestor] " +
                            ",[Subject] " +
                            ",[Reason] " +
                            ",[Status] " +
                            ",[Owner] " +
                            ",[Describe] " +
                            ",[Filepath] " +
                            ",[CreateDate] " +
                            ",[CountTicket] " +
                            ",[TicketStatus] )" +
                            "VALUES " +
                            "('" + txtUsername.Text + "'" +
                            ",'" + tbSubject.Text + "'" +
                            ",'" + txtReason.Text + "'" +
                            ",'" + cbStatus.Text + "'" +
                            ",'" + cbOwner.Text + "'" +
                            ",'" + txtDescribe.Text + "'" +
                            ",'" + filep + "'" +
                            ",'" + DateTime.Now.ToShortDateString() + "'," +
                           count +
                           ",'Open')"
                            );
                            ///send email
                            ///

                            try
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                //FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();

                                //if (mail_add.Trim() == "" || mail_pass == "")
                                //{
                                //    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                                //    return;
                                //}
                                //if (tbTo.Text.Trim() == "")
                                //{
                                //    Msgbox.Show("Please input email to!");
                                //    return;
                                //}


                                // DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");






                                Cursor.Current = Cursors.WaitCursor;
                                string content = crud.ExecFunc_String("USER_TICKET_EMAIL",
                                new string[] { "MailType", "Reason", "EContent" },
                                new string[] { "Ticket", txtReason.Text, txtDescribe.Text }).ToString();
                                string body = string.Empty;
                                //using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                                //{
                                //    body = reader.ReadToEnd();
                                //}
                                //body = body.Replace("{text}", content);
                                //DataTable d1 = crud.ExecQuery("select SIGNATURE from user_claim_signature where FULL_NAME ='" + UserFullName.ToUpper() + "'");
                                //if (d1.Rows.Count != 0)
                                //{
                                //    body = body.Replace("{text}", content);
                                //    body = body.Replace("{department}", d1.Rows[0][0].ToString());
                                //    body = body.Replace("{username}", UserFullName);
                                //    body = body.Replace("{user_email}", mail_add);
                                //}
                                //else
                                //{
                                //    body = body.Replace("{text}", content);
                                //    //body = body.Replace("{department}", d1.Rows[0][0].ToString());
                                //    body = body.Replace("{username}", UserFullName);
                                //    body = body.Replace("{user_email}", mail_add);
                                //}
                                SmtpClient client = new SmtpClient(smtpSer);

                                MailMessage message = new MailMessage();
                                //set formatting email message
                                message.BodyEncoding = Encoding.UTF8;
                                // message.IsBodyHtml = true;
                                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                                message.From = new MailAddress("no-reply@forteinsurance.com");
                                message.Body = "Dear Mr/Ms." + cbOwner.Text + Environment.NewLine + Environment.NewLine + txtDescribe.Text + Environment.NewLine + Environment.NewLine + "Best Regards," + Environment.NewLine + Environment.NewLine + txtUsername.Text;

                                System.Net.ServicePointManager.Expect100Continue = true;
                                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                                //get Account Handler email, CC
                                //string agentcode = r["INTERME_CODE"].ToString(), ahcode = r["ACC_HANDLER"].ToString(), selectedcode;
                                //if (agentcode[0] == '0' || ahcode == "U-BNK") //001,002,003 => no agent/broker          //U-BNK => bank 
                                //    selectedcode = ahcode;
                                //else //agent/broker...
                                //    selectedcode = agentcode;

                                DataTable dtuser = crud.ExecQuery("SELECT MAIL_TO,MAIL_CC FROM USER_PE_EMAIL_ENGLETTER WHERE CODE = '" + cbOwner.Text + "'");
                                if (dtuser.Rows.Count > 0)
                                {
                                    a = dtuser.Rows[0]["MAIL_TO"].ToString();
                                    b = dtuser.Rows[0]["MAIL_CC"].ToString();
                                }
                                //

                                if (a != "")
                                {
                                    string[] to = a.Split(';');
                                    foreach (string s in to)
                                    {
                                        if (s.Trim() != "")
                                            message.To.Add(s.Trim());
                                    }
                                }

                                message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'Ticket'").Rows[0][0].ToString();
                                message.Subject = message.Subject.Replace("%TicketNo%", sqlcrud.LoadData("select TOP 1 ticketID from [DocumentControlDB].[dbo].[tbTicketRequests] where [CreateDate] = '" +
                                DateTime.Now.ToShortDateString() + "' and [Requestor] = '" + txtUsername.Text + "' ORDER BY [ticketID] DESC ").Tables[0].Rows[0]["ticketID"].ToString());
                                message.Subject = message.Subject.Replace("%SubjectC%", tbSubject.Text);
                                message.Subject = message.Subject.Replace("%Status%", cbStatus.Text);
                                message.Subject = message.Subject.Replace("%reason%", txtReason.Text);
                                message.Subject = message.Subject.Replace("%content%", txtDescribe.Text);
                                //message.IsBodyHtml = true;
                                ////default CC auto PE team
                                //DataTable dtTemp = new DataTable();
                                //dtTemp = crud.ExecQuery("SELECT MAIL_CC FROM USER_PE_EMAIL WHERE TEAM = 'Default'");
                                //if (dtTemp.Rows.Count > 0)
                                //{
                                //    string[] cc = dtTemp.Rows[0][0].ToString().Split(';');
                                //    foreach (string s in cc)
                                //    {
                                //        if (s.Trim() != "")
                                //            message.CC.Add(new MailAddress(s.Trim()));
                                //    }
                                //}
                                ////

                                if (b.Trim() != "")
                                {
                                    string[] cc = b.Split(';');
                                    foreach (string s in cc)
                                    {
                                        if (s.Trim() != "")
                                            message.CC.Add(new MailAddress(s.Trim()));
                                    }
                                }

                                //attchment send auto 
                                //string filepath = r["PATH"].ToString();
                                //if (!string.IsNullOrEmpty(filepath))
                                //{
                                //    message.Attachments.Add(new Attachment(filepath));
                                //}
                                //attachment
                                if (dgvFile.Rows.Count > 0)
                                {
                                    foreach (DataGridViewRow dgvr in dgvFile.Rows)
                                    {
                                        message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                                    }
                                }
                                //
                                //
                                //embeded pictures
                                //AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                                //LinkedResource img1 = new LinkedResource(@"Html\Standard_Forte.png", "image/png");
                                //img1.ContentId = "Forte_Logo";
                                //LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                                //img2.ContentId = "FB_logo";
                                //LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                                //img3.ContentId = "YT_logo";
                                //LinkedResource img4 = new LinkedResource(@"Html\mail.png", "image/png");
                                //img4.ContentId = "Mail_logo";

                                //avHtml.LinkedResources.Add(img1);
                                //avHtml.LinkedResources.Add(img2);
                                //avHtml.LinkedResources.Add(img3);
                                //avHtml.LinkedResources.Add(img4);
                                //message.AlternateViews.Add(avHtml);

                                //client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);
                                //client.EnableSsl = false;
                                client.Host = "smtp.office365.com";
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.UseDefaultCredentials = true;
                                client.Credentials = new System.Net.NetworkCredential
                                {
                                    UserName = "no-reply@forteinsurance.com",
                                    Password = "Nrpl@20622#"
                                };
                                client.EnableSsl = true;

                                client.Port = 587;
                                client.Send(message);
                                message.Dispose();
                                client.Dispose();



                                //Msgbox.Show("Email sent! ");

                                Cursor.Current = Cursors.AppStarting;



                            }

                            catch (Exception ex)
                            {
                                Msgbox.Show(ex.Message);
                            }




                            //////


                            Msgbox.Show("Successfully created ticket number " +
                                sqlcrud.LoadData("select TOP 1 ticketID from [DocumentControlDB].[dbo].[tbTicketRequests] where [CreateDate] = '" +
                                DateTime.Now.ToShortDateString() + "' and [Requestor] = '" + txtUsername.Text + "' ORDER BY [ticketID] desc ").Tables[0].Rows[0]["ticketID"].ToString());
                            count = Convert.ToInt32(sqlcrud.LoadData("select CountTicket from [DocumentControlDB].[dbo].[tbTicketRequests] where [CreateDate] = '" +
                               DateTime.Now.ToShortDateString() + "' and [Requestor] = '" + txtUsername.Text + "' ORDER BY [ticketID] desc ").Tables[0].Rows[0]["CountTicket"].ToString());
                            //if (count != 3)
                            //{

                                count++;
                                sqlcrud.Executing("UPDATE [dbo].[tbTicketRequests] SET [CountTicket] =" + count + " where [Requestor] = '" + txtUsername.Text + "' and [CreateDate]='" + DateTime.Now.ToShortDateString() + "'");
                            //}
                            clearall();
                            Cursor.Current = Cursors.WaitCursor;
                        }
                        else
                        {
                            
                            Msgbox.Show("Some missing input field - either status and owner must input!");
                        }
                    }
                
              
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        
        }
        public void copyfile(string filename, string fname)
        {
            string targetpath;
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                FileInfo fileInfo = new FileInfo(filename);
                //targetpath = Path.Combine(ofd.SelectedPath, fileInfo.Name);
                targetpath = "P:\\Ticket" + "\\" + fname;
                File.Copy(filename, targetpath, true);
                //MessageBox.Show("You have been succesfully copied.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
        }
        public void clearall(){
            txtDescribe.Clear();
            txtReason.Clear();
            tbSubject.Clear();
            dgvFile.DataSource = null;
            dgvFile.Rows.Clear();
            
            dgvFile.Refresh();
            
        }

        private void btnViewTicket_Click(object sender, EventArgs e)
        {
            ViewTicketRequest frmv = new ViewTicketRequest();
            frmv.Show();
        }

    }
}
