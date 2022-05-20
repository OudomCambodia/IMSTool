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
    public partial class FrmSchSendMail : Form
    {
        public Pol_Schedule pol;
        public string namefile = "";
        public FrmSchSendMail()
           
        {
            InitializeComponent();
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbCC.Text = "";
            tbContent.Text = "";
            tbSubject.Text = "";
        }

        private void bnSend_Click(object sender, EventArgs e)
        {
            CRUD crud = new CRUD();
            try
            {
                if (tbTo.Text == "")
                {
                    Msgbox.Show("Please Input Mail Send To !!!");
                    return;
                }


                if (File.Exists(@"\\fipnhdbs11\Infoins_IMS_Upload_doc$\Pipay File\" + namefile + ".pdf") == true)
                {
                    DialogResult dr2 = MessageBox.Show("The file already exists. Do you want to continue and resend the email?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dr2 == System.Windows.Forms.DialogResult.No)
                        return;
                }

                Cursor.Current = Cursors.WaitCursor;
                //pol.savepdf();
                

                ////first by initializing a new SmtpClient and pass the server ip/hostname to it
                //SmtpClient client = new SmtpClient("192.168.110.251");

                ////then initialize a mail message
                //MailMessage message = new MailMessage();

                ////lets set the from field to the from text field
                //message.From = new MailAddress("support.infoins@forteinsurance.com");

                ////and the to field
                ////string[] tempt = dt.AsEnumerable().Where(ite => ite.Field<string>("SFC_DPT_CODE") != "UW" && ite.Field<string>("SFC_DPT_CODE") != "RGN").Select(ite => ite.Field<string>("EMAIL")).Distinct().ToArray();
                //string[] tempt = tbTo.Text.ToString().Split(';');
                //foreach (string str in tempt)
                //    message.To.Add(str);

                ////cc field
                //string[] ccList = tbCC.Text.ToString().Split(';');
                //foreach (string str in ccList)
                //    message.CC.Add(str.Trim());

                ////attached file
                //Attachment att = new Attachment(@"\\fipnhdbs11\Infoins_IMS_Upload_doc$\Pipay File\" + namefile + ".pdf");
                //message.Attachments.Add(att);

                //// the body
                //message.Body = tbContent.Text;

                //// the subject
                //message.Subject = tbSubject.Text;

                ////now we create a networkcredential and pass our username and password for the smtp server
                //client.Credentials = new System.Net.NetworkCredential("support.infoins@forteinsurance.com", "IBU@321");

                ////many smtp require ssl so we will enable it
                //client.EnableSsl = false;

                ////we will set the port
                //client.Port = 25;

                //// and finally, send the message
                //client.Send(message);

                string sql = "INSERT INTO PI_M_HIS (M_FR, M_TO, M_CC, M_SUB, M_FN, CREATED_DATE, USER_NAME) VALUES ('support.infoins@forteinsurance.com','" + tbTo.Text + "','" + tbCC.Text + "','" + tbSubject.Text + "','" + namefile + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'),'" + frmLogIn.Usert + "')";
                crud.ExecNonQuery(sql);
                
                //Cursor.Current = Cursors.WaitCursor;
                //Cursor.Current = Cursors.AppStarting;
                //MessageBox.Show("Email Sent!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cannot send Message: " + ex.Message);
            }
        }

        private void FrmSchSendMail_Load(object sender, EventArgs e)
        {
            namefile = pol.filename;
        }
    }
}
