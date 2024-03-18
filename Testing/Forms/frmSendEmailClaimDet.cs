using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MailKit.Search;
using MimeKit;
using System.Text.RegularExpressions;

namespace Testing.Forms
{
    public partial class frmSendEmailClaimDet : Form
    {
        //DataTable dt_insured;
        public string UserName = "Default";
        public string sp_type = "Ack";
        public string remind = "";
        public bool resend = false;
        public bool doc_rec = false;
        public bool rec_form = false;
        string non_paid = "";
        string req_no = "";
        string polno = frmSendEmailClaim.PolNo;
        string admissionDate = string.Empty;
        string dischargeDate = string.Empty;
        string claimAmount = string.Empty;
        public frmSendEmailClaim sec;
        Color redHi = Color.FromArgb(255, 190, 190);
        string preRem = "";
        bool Init = true;
        string code = "";
        string HashPass = "Forte@2017";
        string[] CCdata = new string[1]; //Update 10-Jul-19 (Textbox CC)
        string[] Receiverdata = new string[1]; //Update 07-Aug-19 (Receiver Improve)

        //email information
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        string UserFullName = "";
        public static string finalizecontent = ""; //Update 16-Jul-19 (Edit Email Content)
        List<string> attachfile = new List<string>();

        private string hospital = string.Empty;
        private string ContentToSaveToHist = string.Empty;
        private string senderEmail = string.Empty;

        DataTable dtExc = new DataTable();
        DataTable dtRecCCHist = new DataTable();

        CRUD crud = new CRUD();

        public frmSendEmailClaimDet()
        {
            InitializeComponent();
        }

        private void disabledButt(Button bn)
        {
            bn.Enabled = false;
            bn.BackColor = Color.Gray;
        }

        private void enabledButt(Button bn)
        {
            bn.Enabled = true;
            bn.BackColor = Color.FromArgb(0, 9, 47);
        }

        private void sending_email(string content, string email_to, string subject)
        {
            string body = string.Empty;
            string mail_add_claim_team = string.Empty; //Update 12-Jul-19 (Improve BCC)
            //using (StreamReader reader = new StreamReader("Html/Email.html"))

            using (StreamReader reader = new StreamReader("Html/2022Email.html"))//Update mail signature 2020
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", content);
            //body = body.Replace("{department}", "A&H Claims Unit | Underwriting Department");

            //string sqltemp = "SELECT * FROM VIEW_CLAIM_EMAIL_UN_ADD WHERE USER_CODE = '" + UserName + "'";
            //DataTable dt = crud.ExecQuery(sqltemp);

            string sqltemp = "SELECT * from USER_ANH_CLAIM_TEAM where USER_CODE = '" + UserName + "'";
            DataTable dt = crud.ExecQuery(sqltemp);
            if (dt.Rows.Count != 0)
            {
                body = body.Replace("{department}", dt.Rows[0]["POSITION"].ToString() + " | " + "Accident and Health Department");
                body = body.Replace("{username}", dt.Rows[0]["SENDER"].ToString()); //Update 16-Jul-19 (Edit Email Content)
                body = body.Replace("{phone}", dt.Rows[0]["PHONE_NUMBER"].ToString()); //Update 16-Jul-19 (Edit Email Content)
                body = body.Replace("{user_email}", dt.Rows[0]["SENDER_EMAIL"].ToString()); //Update 16-Jul-19 (Edit Email Content)
                
                senderEmail = dt.Rows[0]["SENDER_EMAIL"].ToString();

                //body = body.Replace("{username}", dt.Rows[0][1].ToString());
                //body = body.Replace("{user_email}", dt.Rows[0][2].ToString());
                //mail_add_claim_team = dt.Rows[0][2].ToString();
            }
            else
            {
                body = body.Replace("{username}", UserFullName);
                body = body.Replace("{user_email}", mail_add);
            }

            // oudom
            //using (StreamReader reader = new StreamReader("Html/2022EmailNew.html"))//Update mail signature 2022
            //{
            //    body = reader.ReadToEnd();
            //}

            //body = body.Replace("{text}", content);

            //var pro = lbClaimNo.Text.ToString().Substring(6, 4).ToLower();
            //if (pro.ToLower().Contains("gpa"))
            //{
            //    body = body.Replace("{dept}", "Group Personal Accident");
            //    body = body.Replace("{sub_dept}", "GPA | Accident and Health Department");
            //    body = body.Replace("{dept_phone}", "M. (+855) 92 666 378");
            //    body = body.Replace("{dept_mail}", "gpa@forteinsurance.com");
            //}
            //else if (pro.ToLower().Contains("hns"))
            //{
            //    body = body.Replace("{dept}", "Hospital and Surgical");
            //    body = body.Replace("{sub_dept}", "H&S | Accident & Health Department");
            //    body = body.Replace("{dept_phone}", "M. (+855 89 666 797) & (+855 81 666 795)");
            //    body = body.Replace("{dept_mail}", "hnsclaims@forteinsurance.com");
            //}
            //else
            //{
            //    body = body.Replace("{dept}", "Figtree Blue");
            //    body = body.Replace("{sub_dept}", "Figtree Blue | Accident & Health Department");
            //    body = body.Replace("{dept_phone}", "M. (+855) 12 777 135" + "<br/>" + "M. (+855) 81 666 795");
            //    body = body.Replace("{dept_mail}", "figtree_blue@forteinsurance.com");
            //}

            //oudom
            DataTable dtHospital = crud.ExecQuery("select nvl(INT_OTH_HOSPITAL, INT_COMMENTS) HOSPITAL, nvl(INT_OTH_HOSPITAL, 'true') IS_NULL_OTH_HOSPITAL from CL_T_INTIMATION,CL_T_CLM_MEMBERS where CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + lbClaimNo.Text.ToUpper().Trim() + "'");
            if (dtHospital.Rows.Count > 0)
            {
                var isNullOthHospital = dtHospital.Rows[0]["IS_NULL_OTH_HOSPITAL"].ToString() == "true";
                hospital = dtHospital.Rows[0]["HOSPITAL"].ToString();

                if (isNullOthHospital)
                {
                    var hIndex = hospital.IndexOf("H:");
                    if (hIndex == -1)
                        hIndex = hospital.IndexOf("H :");

                    var strHospital = hospital.Substring(hIndex + 2);

                    var dIndex = strHospital.IndexOf(":");
                    var tmpHospital = strHospital.Substring(0, dIndex - 1).Trim();

                    var bHospital = tmpHospital.IndexOf("(");

                    hospital = bHospital != -1 ? tmpHospital.Substring(0, bHospital) : tmpHospital;
                }

                body = body.Replace("%Place%", hospital);
                content = content.Replace("%Place%", hospital);
            }

            //DataTable dtHospital = crud.ExecQuery("select (select COM_NAME from CL_M_ORGANIZATIONS where COM_CODE = INT_HOSPITAL_CODE) PANEL_HOSPITAL from CL_T_INTIMATION where INT_CLAIM_NO = '" + lbClaimNo.Text.ToUpper().Trim() + "'");
            //if (dtHospital.Rows.Count > 0)
            //{
            //    hospital = dtHospital.Rows[0]["PANEL_HOSPITAL"].ToString();

            //    body = body.Replace("%Place%", hospital);
            //    content = content.Replace("%Place%", hospital);
            //}

            //if (!string.IsNullOrEmpty(frmGenerateSettlementLetterNotice.Paid))
            //{
            //    body = body.Replace("%Paid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.Paid).ToString("0.00"));
            //    content = content.Replace("%Paid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.Paid).ToString("0.00"));
            //}

            //if (!string.IsNullOrEmpty(frmGenerateSettlementLetterNotice.NonPaid))
            //{
            //    body = body.Replace("%NonPaid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.NonPaid).ToString("0.00"));
            //    content = content.Replace("%NonPaid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.NonPaid).ToString("0.00"));
            //}

            if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.Paid.Trim()))
                body = body.Replace("%Paid%", "USD " + frmANHSettlementLetterNew.Paid);

            if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.NonPaid.Trim()))
                body = body.Replace("%NonPaid%", "USD " + frmANHSettlementLetterNew.NonPaid);

            if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.NonPayableReason.Trim()))
                body = body.Replace("N/A", frmANHSettlementLetterNew.NonPayableReason);

            if (remind == "First")
            {
                var first = new StringBuilder();
                first.Append("<li>If the required documents are not fully submitted after the 1st reminder email, we will send the 2nd reminder email within <b>07 calendar days</b> from the 1st reminder email date.</li>")
                    .Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                    .Append("<li>If the required documents still cannot be provided within 30 calendar days from the 3rd reminder email date, this claim will be closed without payment.</li>");
                body = body.Replace("%Notes%", first.ToString());
            }
            else if (remind == "Second")
            {
                var second = new StringBuilder();
                second.Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                    .Append("<li>If the required documents still cannot be provided within 30 calendar days from the 3rd reminder email date, this claim will be closed without payment.</li>");
                body = body.Replace("%Notes%", second.ToString());
            }
            else if (remind == "Third")
            {
                var third = new StringBuilder();
                third.Append("<li>If the required documents still cannot be provided within 30 calendar days from the 3rd reminder email date, this claim will be closed without payment.</li>");
                body = body.Replace("%Notes%", third.ToString());
            }

            // oudom
            //body = body.Replace("{text}", content);
            //body = body.Replace("{department}", "A&H Claims Unit | Underwriting Department");
            //Update 08-Jul-19
            //string sqltemp = "SELECT * FROM VIEW_CLAIM_EMAIL_UN_ADD WHERE USER_CODE = '" + UserName + "'";
            //DataTable dt = crud.ExecQuery(sqltemp);
            //if (dt.Rows.Count != 0)
            //{
            //    body = body.Replace("{username}", dt.Rows[0][1].ToString());
            //    body = body.Replace("{user_email}", dt.Rows[0][2].ToString());
            //    mail_add_claim_team = dt.Rows[0][2].ToString();
            //}
            ////End of Update            
            //else
            //{
            //    body = body.Replace("{username}", UserFullName);

            //    body = body.Replace("{user_email}", mail_add);
            //}



            //SmtpClient client = new SmtpClient(smtpSer);



            //then initialize a mail message
            MailMessage message = new MailMessage();

            //set formatting email message
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //lets set the from field to the from text field
            message.From = new MailAddress(mail_add);

            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //Update 11-Jul-19 & 12-Jul-19 (Improve BCC)
            //if (mail_add_claim_team == String.Empty) message.Bcc.Add(new MailAddress(mail_add));
            //else message.Bcc.Add(new MailAddress(mail_add_claim_team));


            // Oudom
            //if ((mail_add_claim_team != String.Empty && mail_add_claim_team.Trim() == "gpa@forteinsurance.com"))
            //{
            //    message.CC.Add(new MailAddress(mail_add_claim_team));
            //}
            //else
            //{
            //    message.Bcc.Add(new MailAddress(mail_add));
            //    if (mail_add_claim_team != String.Empty)
            //        message.Bcc.Add(new MailAddress(mail_add_claim_team));
            //}




            //End of Update

            //Update 16-Aug-19 (Attach File)
            attachfile = attachfile.Distinct().ToList();
            if (tbAttachFile.Text != "")
            {
                string[] File = tbAttachFile.Text.Split(';');
                Array.Resize(ref File, File.Length - 1);
                if (File.Length == attachfile.Count)
                    foreach (string s in attachfile)
                        message.Attachments.Add(new Attachment(s));
                else
                {
                    foreach (string s in File)
                    {
                        foreach (string ss in attachfile)
                        {
                            string temp = ss;
                            if (s.Replace(" ", "") == ss.Replace(" ", ""))
                            {
                                message.Attachments.Add(new Attachment(temp));
                            }
                        }
                    }
                }
            }
            //End of Update

            //and the to field
            if (email_to.Trim() != "")
            {
                string[] tempt = email_to.Split(',');
                foreach (string str in tempt)
                {
                    if (str.Trim() != "")
                        message.To.Add(str);
                }
            }


            // Oudom
            //cc field
            if (tbCC.Text.Trim() != "")
            {
                string[] ccList = tbCC.Text.Split(',');
                foreach (string str in ccList)
                {
                    if (str.Trim() != "")
                        message.CC.Add(str.Trim());
                }
            }

            //attached file
            // Attachment att = new Attachment("");
            //message.Attachments.Add("");

            // the body
            //message.Body = body;

            // the subject
            message.Subject = subject;

            //add previous conversation (24-Nov-2020)
            //if ((sp_type == "DocReq" && resend) || sp_type == "Rem") //add previous conversation only when resend DocReq or Reminder email
            //{
            //    using (var imapclient = new ImapClient())
            //    {

            //        DataTable dtTemp = crud.ExecQuery("SELECT INT_INITIMATE_DT FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + lbClaimNo.Text + "'");
            //        DateTime intimatedate = Convert.ToDateTime(dtTemp.Rows[0][0]);
            //        intimatedate = intimatedate.AddDays(-1);

            //        //imapclient.Connect("192.168.110.251", 143, SecureSocketOptions.None);
            //        imapclient.Connect("outlook.office365.com", 993, SecureSocketOptions.SslOnConnect);

            //        imapclient.Authenticate(mail_add, mail_pass);

            //        // The Inbox folder is always available on all IMAP servers...
            //        var inbox = imapclient.Inbox;
            //        inbox.Open(FolderAccess.ReadOnly);
            //        var query = SearchQuery.DeliveredAfter(intimatedate)
            //        //var query = SearchQuery.DeliveredAfter(DateTime.Parse("2020-10-01"))
            //        //.And(SearchQuery.SubjectContains(subject.Substring(0, subject.IndexOf("REQUEST FOR SUPPORTING DOCUMENTS")) + "REQUEST FOR SUPPORTING DOCUMENTS"))
            //        .And(SearchQuery.SubjectContains(lbClaimNo.Text))
            //        .And(SearchQuery.SubjectContains("REQUEST FOR SUPPORTING DOCUMENTS"));
            //        MimeMessage latest1 = null;//latest in inbox
            //        foreach (var uid in inbox.Search(query))
            //        {
            //            if (latest1 == null) //first element
            //            {
            //                latest1 = inbox.GetMessage(uid);
            //            }
            //            else
            //            {
            //                MimeMessage temp = inbox.GetMessage(uid);
            //                if (latest1.Date < temp.Date)
            //                    latest1 = temp;
            //            }
            //        }
            //        //latest.TextBody

            //        var sentitem = imapclient.GetFolder(SpecialFolder.Sent);
            //        sentitem.Open(FolderAccess.ReadOnly);
            //        MimeMessage latest2 = null;//latest in sent item
            //        foreach (var uid in sentitem.Search(query))
            //        {
            //            if (latest2 == null) //first element
            //            {
            //                latest2 = sentitem.GetMessage(uid);
            //            }
            //            else
            //            {
            //                MimeMessage temp = sentitem.GetMessage(uid);
            //                if (latest2.Date < temp.Date)
            //                    latest2 = temp;
            //            }
            //        }


            //        if (latest1 != null || latest2 != null)
            //        {
            //            MimeMessage latest = new MimeMessage();
            //            if (latest1 == null && latest2 != null) latest = latest2;
            //            else if (latest1 != null && latest2 == null) latest = latest1;
            //            else if (latest1 != null && latest2 != null) latest = (latest1.Date >= latest2.Date) ? latest1 : latest2; //compare the latest between inbox and sent item
            //            StringBuilder prevbody = new StringBuilder();
            //            prevbody.AppendFormat("<br/><br/><br/><br/><div><div style='border:none;border-top:solid #B5C4DF 1.0pt;padding:3.0pt 0in 0in 0in'><p class=MsoNormal><b>"
            //                +"<span style='font-size:10.0pt;font-family:'Tahoma',sans-serif;color:windowtext'>From:</span></b> "
            //                +"<span style='font-size:10.0pt;font-family:'Tahoma',sans-serif;color:windowtext'> <a href=\"mailto:{0}\">{0}</a> ", ((MailboxAddress)latest.From[0]).Address);
            //            if (!string.IsNullOrEmpty(latest.From[0].Name))
            //                prevbody.Append(latest.From[0].Name);
            //            if (latest.Date != null)
            //                prevbody.AppendFormat
            //                ("<br/><b>Sent: </b>{0}<br/>", latest.Date.DateTime.ToString("dddd, d MMMM, yyyy hh:mm tt"));
            //            prevbody.Append("<b>To: </b>");
            //            foreach (InternetAddress to in latest.To)
            //            {
            //                prevbody.AppendFormat("<a href=\"mailto:{0}\">{0}</a>; ", ((MailboxAddress)to).Address);
            //            }
            //            prevbody.Append("<br/><b>Cc: </b>");
            //            foreach (InternetAddress cc in latest.Cc)
            //            {
            //                prevbody.AppendFormat("<a href=\"mailto:{0}\">{0}</a>; ", ((MailboxAddress)cc).Address);
            //            }
            //            prevbody.AppendFormat("<br/><b>Subject: </b>{0}<o:p></o:p></span><br/></p></div><br/>", latest.Subject);

            //            //prevbody.Append("<blockquote style=\"display: block;  margin-top: 1em;  margin-bottom: 1em;  margin-left: 40px;  margin-right: 40px;  border-left: 3px gray solid;  padding-left: 5px;\"><pre>");
            //            //prevbody.Append((latest.TextBody == null) ? latest.HtmlBody : latest.TextBody);
            //            //prevbody.Append("</pre></blockquote>");

            //            if (latest.HtmlBody != null)
            //            {
            //                prevbody.Append(latest.HtmlBody);
            //            }
            //            else prevbody.Append("<pre>"+latest.TextBody+"</pre>");
            //            prevbody.Append("</div>");

            //            body += prevbody.ToString(); //add prev to body
            //        }
            //        imapclient.Disconnect(true);
            //    }
            //}
            //

            //embed pictures
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            //LinkedResource img1 = new LinkedResource(@"Html\Forte_Logo.png", "image/png");
            LinkedResource img1 = new LinkedResource(@"Html\forte-general-logo-red.png", "image/png");
            img1.ContentId = "forte-general-logo-red";
            //LinkedResource img2 = new LinkedResource(@"Html\FB_logo.png", "image/png");
            LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
            img2.ContentId = "FB_logo";
            LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
            img3.ContentId = "YT_logo";
            LinkedResource img4 = new LinkedResource(@"Html\linkedin.png", "image/png");
            img4.ContentId = "linkedin";
            LinkedResource img5 = new LinkedResource(@"Html\EmailSignature.png", "image/png");
            img5.ContentId = "EmailSignature";

            avHtml.LinkedResources.Add(img1);
            avHtml.LinkedResources.Add(img2);
            avHtml.LinkedResources.Add(img3);
            avHtml.LinkedResources.Add(img4);
            avHtml.LinkedResources.Add(img5);
            message.AlternateViews.Add(avHtml);

            ////now we create a networkcredential and pass our username and password for the smtp server
            //client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);

            ////many smtp require ssl so we will enable it
            //client.EnableSsl = false;

            //client.Host = smtpSer;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential
            //{
            //    UserName = "no-reply@forteinsurance.com",
            //    Password = "Forte@12345"
            //};
            //client.EnableSsl = true;

            ////we will set the port
            //client.Port = port;

            //// and finally, send the message
            //client.Send(message);


            // Oudom
            var Credential = new System.Net.NetworkCredential(mail_add, mail_pass);
            var result = CommonFunctions.SendEmail(Credential, message);

            message.Dispose();

            ContentToSaveToHist = body;

            // insert receiver and cc to hist, so user no need to add the same receiver and cc again when doing another claim operation.

            if (dtRecCCHist.Rows.Count > 0)
                crud.ExecNonQuery("update user_claim_anh_receiver_cc_hist set receiver = '" + tbReceiver.Text.Trim() + "', cc = '" + tbCC.Text.Trim() + "' where claim_no = '" + lbClaimNo.Text.Trim().ToUpper() + "'");
            else
                crud.ExecNonQuery("insert into user_claim_anh_receiver_cc_hist(claim_no, receiver, cc) values('" + lbClaimNo.Text.Trim().ToUpper() + "', '" + tbReceiver.Text.Trim() + "', '" + tbCC.Text.Trim() + "')");

            //client.Dispose();
        }

        private string getContent()
        {
            if (cbReqNo.Text != "")
                req_no = cbReqNo.Text;

            code = "";
            foreach (ListViewItem lvi in lvDoc.CheckedItems)
            {
                //oudom
                if (sp_type == "RemNew" || sp_type == "DocReqNew" || sp_type == "ClaimClosing") //Update 17-Jul-19 (Document Request)
                {
                    if (lvi.SubItems[3].Text == "Yes")
                        continue;
                }

                code += lvi.Text + ",";
            }
            code = (code != "") ? code.Remove(code.Length - 1) : "";

            if (tbNonPaid.Text != "")
                non_paid = tbNonPaid.Text;

            // oudom
            return crud.ExecFunc_String_New("USER_GET_EMAIL_CONTENT_FN_NEW", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid", "receiver", "noti_date", "User_fullname", "polno", "admission_date", "discharge_date", "claim_amount" }, new string[] { lbClaimNo.Text, sp_type, code, remind, req_no, tbNonPaid.Text, tbReceiver.Text, dpNoti.Value.ToString("dd/MM/yyyy"), UserFullName, polno, admissionDate, dischargeDate, claimAmount });
        }

        private void frmSendEmailClaimDet_Load(object sender, EventArgs e)
        {
            attachfile.Clear();

            //var qBuilder = new StringBuilder();
            //qBuilder.Append("select int_comments ")
            //    .Append("from cl_t_intimation  ")
            //    .AppendFormat("where int_claim_no = '{0}'", lbClaimNo.Text.ToUpper());

            //var dtRemark = crud.ExecQuery(qBuilder.ToString());
            //if (dtRemark.Rows.Count > 0)
            //{
            //    var remark = dtRemark.Rows[0][0].ToString();
            //    var hIndex = remark.IndexOf("H:");
            //    var dIndex = remark.IndexOf("D:");

            //    if (hIndex != -1 && dIndex != -1)
            //    {
            //        admissionDate = remark.Substring(hIndex, dIndex - hIndex).Trim();
            //        string pattern = @"\d{2}/\d{2}/\d{4}";
            //        Match match = Regex.Match(admissionDate, pattern);
            //        if (string.IsNullOrEmpty(match.Value))
            //        {
            //            string pattern1 = @"\d{2}/\d{2}/\d{2}";
            //            match = Regex.Match(admissionDate, pattern1);
            //        }
            //        admissionDate = match.Success ? match.Value : string.Empty;
            //        dischargeDate = admissionDate;
            //    }
            //}

            var qBuilder = new StringBuilder();
            qBuilder.Append("select int_date_admission, int_date_discharge ")
                .Append("from cl_t_intimation  ")
                .AppendFormat("where int_claim_no = '{0}'", lbClaimNo.Text.ToUpper());

            var dtAdDis = crud.ExecQuery(qBuilder.ToString());
            if (dtAdDis.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtAdDis.Rows[0]["int_date_admission"].ToString()))
                    admissionDate = Convert.ToDateTime(dtAdDis.Rows[0]["int_date_admission"]).ToString("dd'/'MM'/'yyyy");

                if (!string.IsNullOrEmpty(dtAdDis.Rows[0]["int_date_discharge"].ToString()))
                    dischargeDate = Convert.ToDateTime(dtAdDis.Rows[0]["int_date_discharge"]).ToString("dd'/'MM'/'yyyy");
            }

            var dtClaimAmount = crud.ExecQuery("select prd_payable_amt from cl_t_provision_dtls where prd_claim_no = '" + lbClaimNo.Text.ToUpper() + "' and rownum = 1");
            if (dtClaimAmount.Rows.Count > 0)
            {
                claimAmount = dtClaimAmount.Rows[0][0].ToString();
            }

            //setting username
            UserName = sec.UserName;

            //getting configuration information about email from the database
            DataTable dtEmailIn = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "emailInfo", UserName });
            smtpSer = dtEmailIn.Rows[0].ItemArray[0].ToString();
            mail_add = dtEmailIn.Rows[0].ItemArray[1].ToString();
            if (dtEmailIn.Rows[0].ItemArray[2].ToString() != "")
                mail_pass = Cipher.Decrypt(dtEmailIn.Rows[0].ItemArray[2].ToString(), HashPass);
            else
                mail_pass = "";
            port = Convert.ToInt16(dtEmailIn.Rows[0].ItemArray[3].ToString());
            UserFullName = dtEmailIn.Rows[0].ItemArray[4].ToString();

            if (mail_add.Trim() == "" || mail_pass == "")
            {
                Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                this.Close();
                return;
            }

            //setting title
            if (sp_type == "Ack")
                lbTitle.Text = "Acknowledgement Email";
            else if (sp_type == "Rej")
                lbTitle.Text = "Rejection Email";
            else if (sp_type == "Par")
                lbTitle.Text = "Partial Payment Email";
            else if (sp_type == "Pay")
                lbTitle.Text = "Full Payment Email";
            else if (sp_type == "DocReqNew") //Update 17-Jul-19 (Document Request) //oudom
                lbTitle.Text = "Document Request Email";
            else if (sp_type == "RemNew" && remind == "First")
                lbTitle.Text = "First Reminder Email";
            else if (sp_type == "RemNew" && remind == "Second")
                lbTitle.Text = "Second Reminder Email";
            else if (sp_type == "RemNew" && remind == "Third")
                lbTitle.Text = "Third Reminder Email";
            else if (sp_type == "ClaimClosing")
                lbTitle.Text = "Claim Closing Email";

            //setting enabled controls based on type

            //oudom
            if (sp_type == "PayNew" || sp_type == "ParNew")
                cbReqNo.Enabled = true;
            // oudom
            if (sp_type == "ParNew")
                tbNonPaid.Enabled = true;


            //get the account handler email
            DataTable dtEmail = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "AHemail", "" });
            if (dtEmail.Rows.Count >= 1)
                tbReceiver.Text = dtEmail.Rows[0].ItemArray[0].ToString();

            //get intimation date
            DataTable dtInti = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "IntiDate", "" });
            if (dtInti.Rows.Count >= 1)
                dpNoti.Value = Convert.ToDateTime(dtInti.Rows[0].ItemArray[0]);
            else
                dpNoti.Value = DateTime.Now;

            //add null value to combobox
            cbReqNo.Items.Add("");
            //get requisition no for claim no
            DataTable dtReq = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "ClaimReq", "" });
            if (dtReq.Rows.Count >= 1)
            {
                foreach (DataRow dr in dtReq.Rows)
                    cbReqNo.Items.Add(dr[0].ToString());
            }

            //setting check box for ListView
            lvDoc.CheckBoxes = true;
            lvDoc.View = View.Details;


            //Update 18-Jul-19 (Add Exclusion)
            disabledButt(bnAddExclu);
            disabledButt(bnAddDoc);

            //if (sp_type == "Rem")

            //oudom
            if (sp_type == "RejNew")
            {
                btnAttachClaimRejection.Visible = true;
                btnGenerateSettlementNotice.Visible = false;
            }
            else if (sp_type == "ParNew" || sp_type == "PayNew")
            {
                btnGenerateSettlementNotice.Visible = true;
                btnAttachClaimRejection.Visible = false;
                btnGenerateSettlementNotice.Location = new Point(107, 662);
            }
            else
            {
                btnAttachClaimRejection.Visible = false;
                btnGenerateSettlementNotice.Visible = false;
            }

            //Oudom
            if (sp_type == "RemNew" || sp_type == "DocReqNew" || sp_type == "ClaimClosing") //Update 17-Jul-19 (Document Request)
            {
                enabledButt(bnAddDoc);
                bnAddDoc.BringToFront();

                //setting columns for ListView
                lvDoc.Columns.Add("Doc Code", 70);
                lvDoc.Columns.Add("Doc Type", 170);
                lvDoc.Columns.Add("Doc Detail", 350);
                lvDoc.Columns.Add("Doc Received", 100);

                DataTable dtDoc = crud.ExecQuery("select * from user_claim_email_doc");
                foreach (DataRow dr in dtDoc.Rows)
                {
                    ListViewItem lvi = new ListViewItem(dr["DOC_CODE"].ToString());
                    lvi.SubItems.Add(dr["DOC_TYPE"].ToString());
                    lvi.SubItems.Add(dr["DOC_CONTENT"].ToString());
                    lvi.SubItems.Add("");

                    lvDoc.Items.Add(lvi);
                }
            }
            //oudom
            else if (sp_type == "ParNew" || sp_type == "RejNew")
            {
                enabledButt(bnAddExclu); //Update 18-Jul-19 (Add Exclusion)
                bnAddExclu.BringToFront();

                //setting columns for ListView
                lvDoc.Columns.Add("Exc Code", 70);
                lvDoc.Columns.Add("Exclusion Type", 900);

                //oudom
                //DataTable dtExc = crud.ExecQuery("select * from user_claim_email_exclus where PRODUCT = '" + lbClaimNo.Text.Substring(7, 3) + "'");
                var pro = lbClaimNo.Text.Substring(6, 4).ToLower();

                var plan = string.Empty;
                DataTable dtClaimDt = crud.ExecQuery("select (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + lbClaimNo.Text.ToUpper().Trim() + "'");
                if (dtClaimDt.Rows.Count > 0)
                {
                    plan = Regex.Replace(dtClaimDt.Rows[0]["PLAN_DESCRIPTION"].ToString(), @"[A-Za-z]+", string.Empty).Trim();
                }

                if (pro.Contains("hns"))
                {
                    dtExc = plan == "+" ? crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS' order by PARTS, ENG")
                    : crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS++' order by PARTS, ENG");
                }
                else if (pro.Contains("gpa") || pro.Contains("pac"))
                {
                    dtExc = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'GPA' order by PARTS, ENG");
                }
                else if (pro.Contains("pae"))
                {
                    dtExc = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'PAE' order by PARTS, ENG");
                }
                else if (pro.Contains("bhp"))
                {
                    dtExc = crud.ExecQuery("select * from user_email_bhp_excludef where PRODUCTS = 'BHP' order by PARTS, ENG");
                }

                if (pro.Contains("trv"))
                {
                    cboExclusionSection.Visible = true;
                    var dtSection = crud.ExecQuery("select distinct PARTS from user_email_trv_excludef order by PARTS");
                    cboExclusionSection.ValueMember = "PARTS";
                    cboExclusionSection.DisplayMember = "PARTS";
                    cboExclusionSection.DataSource = dtSection;
                }
                else if (pro.Contains("trp"))
                {
                    cboExclusionSection.Visible = true;
                    var dtSection = crud.ExecQuery("select distinct PARTS from user_email_trp_excludef order by PARTS");
                    cboExclusionSection.ValueMember = "PARTS";
                    cboExclusionSection.DisplayMember = "PARTS";
                    cboExclusionSection.DataSource = dtSection;
                }
                else if (pro.Contains("tra"))
                {
                    cboExclusionSection.Visible = true;
                    var dtSection = crud.ExecQuery("select distinct PARTS from user_email_tra_excludef order by PARTS");
                    cboExclusionSection.ValueMember = "PARTS";
                    cboExclusionSection.DisplayMember = "PARTS";
                    cboExclusionSection.DataSource = dtSection;
                }

                if (dtExc.Rows.Count == 0 && !pro.Contains("trv") && !pro.Contains("trp") && !pro.Contains("tra"))
                {
                    dtExc.Clear();
                    dtExc = crud.ExecQuery("select * from user_claim_email_exclus");
                }


                foreach (DataRow dr in dtExc.Rows)
                {
                    //ListViewItem lvi = new ListViewItem(dr["EXCL_CODE"].ToString());
                    //lvi.SubItems.Add(dr["EXCLUSION"].ToString());

                    //lvDoc.Items.Add(lvi);

                    if (pro.Contains("hns"))
                    {
                        ListViewItem lvi = new ListViewItem(dr["MED_CODE"].ToString());
                        lvi.SubItems.Add(dr["ENG"].ToString());

                        lvDoc.Items.Add(lvi);
                    }
                    else if (pro.Contains("gpa") || pro.Contains("pac"))
                    {
                        ListViewItem lvi = new ListViewItem(dr["GPA_CODE"].ToString());
                        lvi.SubItems.Add(dr["ENG"].ToString());

                        lvDoc.Items.Add(lvi);
                    }
                    else if (pro.Contains("pae"))
                    {
                        ListViewItem lvi = new ListViewItem(dr["GPA_CODE"].ToString());
                        lvi.SubItems.Add(dr["ENG"].ToString());

                        lvDoc.Items.Add(lvi);
                    }
                    else if (pro.Contains("bhp"))
                    {
                        ListViewItem lvi = new ListViewItem(dr["BHP_CODE"].ToString());
                        lvi.SubItems.Add(dr["ENG"].ToString());

                        lvDoc.Items.Add(lvi);
                    }
                }
            }

            preRem = remind == "Second" ? "First" : (remind == "Third" ? "Second" : "");

            //setting ticked box for second and third reminder
            //oudom
            if (sp_type == "RemNew" && (preRem != "" || rec_form))
            {
                DataTable dtPre = new DataTable();
                if (rec_form && remind == "") //Update 17-Jul-19 (Document Request)
                    //oudom
                    dtPre = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "PreClDocReq", "" }); //PreClDocReq = PreClDoc but specific for DocReq
                else dtPre = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "PreClDoc", rec_form ? remind : preRem });
                foreach (string each in dtPre.Rows[0].ItemArray[0].ToString().Split(','))
                {
                    foreach (ListViewItem lvi in lvDoc.Items)
                    {
                        if (lvi.Text == each)
                        {
                            lvi.Checked = true;
                        }
                    }
                }

                DataTable dtDoc = new DataTable();
                if (rec_form && remind == "") //Update 17-Jul-19 (Document Request)
                    //oudom
                    dtDoc = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "RecClDocReq", "" }); //RecClDocReq = RecClDoc but specific for DocReq
                else dtDoc = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "RecClDoc", rec_form ? remind : preRem });

                if (dtDoc.Rows.Count != 0)
                {
                    foreach (string each in dtDoc.Rows[0].ItemArray[0].ToString().Split(','))
                    {
                        foreach (ListViewItem lvi in lvDoc.Items)
                        {
                            if (lvi.Text == each)
                            {
                                lvi.SubItems[3].Text = "Yes";
                                lvi.BackColor = redHi;
                            }
                        }
                    }
                }
                DataTable dtInf = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "ResInfo", sp_type });
                if (dtInf.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInf.Rows)
                    {
                        if (dr["REM_NO"].ToString() == preRem)
                        {
                            tbCC.Text = dr["CC"].ToString();
                            tbReceiver.Text = dr["RECEIVER"].ToString();
                            dpNoti.Value = Convert.ToDateTime(dr["REC_DATE"].ToString());
                        }
                    }
                }
            }


            //Update 17-Jul-19 (Document Request)
            //setting ticked box for first reminder (both normal and doc_rec)
            //oudom
            if (!resend && ((sp_type == "RemNew" && remind == "First") || sp_type == "ClaimClosing"))
            {
                DataTable dtTemp = new DataTable();
                string res = crud.ExecFunc_String("USER_EMAIL_CHECK", new string[] { "claim_no" }, new string[] { lbClaimNo.Text });
                if (res == "True" || rec_form)

                    dtTemp = crud.ExecQuery("SELECT EMAIL_DOC_CODE,DOC_REC_CODE from USER_CLAIM_EMAIL_HIST WHERE trim(CLAIM_NO) = '" + lbClaimNo.Text + "' and trim(SEND_TYPE) = 'Rem' and REM_NO = 'First' and IS_RESEND = 'N'");

                //if (rec_form) dtTemp = crud.ExecQuery("SELECT EMAIL_DOC_CODE,DOC_REC_CODE from USER_CLAIM_EMAIL_HIST WHERE trim(CLAIM_NO) = '" + lbClaimNo.Text + "' and trim(SEND_TYPE) = 'Rem' and REM_NO = 'First' and IS_RESEND = 'N'");
                //oudom
                else dtTemp = crud.ExecQuery("SELECT EMAIL_DOC_CODE,DOC_REC_CODE from USER_CLAIM_EMAIL_HIST WHERE trim(CLAIM_NO) = '" + lbClaimNo.Text + "' and trim(SEND_TYPE) = 'DocReqNew' and IS_RESEND = 'N'");

                foreach (string each in dtTemp.Rows[0]["EMAIL_DOC_CODE"].ToString().Split(','))
                {
                    foreach (ListViewItem lvi in lvDoc.Items)
                    {
                        if (lvi.Text == each)
                        {
                            lvi.Checked = true;
                        }
                    }
                }

                foreach (string each in dtTemp.Rows[0]["DOC_REC_CODE"].ToString().Split(','))
                {
                    foreach (ListViewItem lvi in lvDoc.Items)
                    {
                        if (lvi.Text == each)
                        {
                            lvi.SubItems[3].Text = "Yes";
                            lvi.BackColor = redHi;
                        }
                    }
                }
                //oudom
                //DataTable dtInf = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "ResInfo", sp_type });
                DataTable dtInf = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "ResInfo", "DocReqNew" });
                if (dtInf.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInf.Rows)
                    {
                        tbCC.Text = dr["CC"].ToString();
                        tbReceiver.Text = dr["RECEIVER"].ToString();
                        dpNoti.Value = Convert.ToDateTime(dr["REC_DATE"].ToString());
                    }
                }
            }
            //End of Update




            //disabled or enabled doc received button
            //if (sp_type == "Rem")
            //oudom
            if (sp_type == "RemNew" || sp_type == "DocReqNew") //Update 17-Jul-19 (Document Request)
                enabledButt(bnDocRec);
            else
                disabledButt(bnDocRec);


            //if it is the received documents form
            if (rec_form)
            {
                bnSend.Text = "Set";
                tbReceiver.Text = "";
                tbReceiver.Enabled = false;
                tbNonPaid.Enabled = false;
                cbReqNo.Enabled = false;
                tbCC.Enabled = false;
                dpNoti.Enabled = false;
                bnPreview.Text = "All Received";
            }

            //load data for resend
            if (resend)
            {
                DataTable dtInf = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "ResInfo", sp_type });
                if (dtInf.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInf.Rows)
                    {
                        //oudom
                        if (sp_type == "RemNew")
                        {
                            if (dr["REM_NO"].ToString() == remind)
                            {
                                tbCC.Text = dr["CC"].ToString();
                                foreach (String each in dr["CC"].ToString().Split(' '))
                                {
                                    tbCC.Text += each + ", ";
                                }

                                tbNonPaid.Text = dr["NON_PAID_AMT"].ToString();
                                tbReceiver.Text = dr["RECEIVER"].ToString();
                                cbReqNo.Text = dr["REQ_NO"].ToString();
                                dpNoti.Value = Convert.ToDateTime(dr["REC_DATE"].ToString());


                                if (remind == "First")
                                {
                                    foreach (string each in dr["EMAIL_DOC_CODE"].ToString().Split(','))
                                    {
                                        foreach (ListViewItem lvi in lvDoc.Items)
                                        {
                                            if (lvi.Text == each)
                                            {
                                                lvi.Checked = true;
                                            }
                                        }
                                    }

                                    foreach (string each in dr["DOC_REC_CODE"].ToString().Split(','))
                                    {
                                        foreach (ListViewItem lvi in lvDoc.Items)
                                        {
                                            if (lvi.Text == each)
                                            {
                                                lvi.SubItems[3].Text = "Yes";
                                                lvi.BackColor = redHi;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else  //Update 17-Jul-19
                        {

                            foreach (String each in dr["CC"].ToString().Split(' '))
                            {
                                tbCC.Text += each + ", ";
                            }
                            tbNonPaid.Text = dr["NON_PAID_AMT"].ToString();
                            tbReceiver.Text = dr["RECEIVER"].ToString();
                            cbReqNo.Text = dr["REQ_NO"].ToString();
                            dpNoti.Value = Convert.ToDateTime(dr["REC_DATE"].ToString());

                            //oudom
                            if (sp_type == "RejNew" || sp_type == "ParNew")
                            {
                                foreach (string each in dr["EMAIL_DOC_CODE"].ToString().Split(','))
                                {
                                    foreach (ListViewItem lvi in lvDoc.Items)
                                    {
                                        if (lvi.Text == each)
                                        {
                                            lvi.Checked = true;
                                        }
                                    }
                                }
                            }

                            //oudom
                            else if (sp_type == "DocReqNew" || sp_type == "ClaimClosing") //Update 17-Jul-19 (Document Request)
                            {
                                foreach (string each in dr["EMAIL_DOC_CODE"].ToString().Split(','))
                                {
                                    foreach (ListViewItem lvi in lvDoc.Items)
                                    {
                                        if (lvi.Text == each)
                                        {
                                            lvi.Checked = true;
                                        }
                                    }
                                }

                                foreach (string each in dr["DOC_REC_CODE"].ToString().Split(','))
                                {
                                    foreach (ListViewItem lvi in lvDoc.Items)
                                    {
                                        if (lvi.Text == each)
                                        {
                                            lvi.SubItems[3].Text = "Yes";
                                            lvi.BackColor = redHi;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            //set initialized to true
            Init = false;

            requeryEmailSuggestion();

            dtRecCCHist = crud.ExecQuery("select * from user_claim_anh_receiver_cc_hist where claim_no = '" + lbClaimNo.Text.Trim().ToUpper() + "'");
            if (dtRecCCHist.Rows.Count > 0)
            {
                tbReceiver.Text = dtRecCCHist.Rows[0]["RECEIVER"].ToString().Trim();
                tbCC.Text = dtRecCCHist.Rows[0]["CC"].ToString().Trim();
            }
        }

        private void bnSend_Click(object sender, EventArgs e)
        {
            if (rec_form == true)
            {
                string RecDoc = "";
                foreach (ListViewItem lvi in lvDoc.Items)
                {
                    if (lvi.SubItems[3].Text == "Yes")
                        RecDoc += lvi.Text + ",";
                }
                RecDoc = (RecDoc != "") ? RecDoc.Remove(RecDoc.Length - 1) : "";

                if (remind == "") //Update 17-Jul-19 (Document Request)
                    //oudom
                    crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                    new string[] { "DocRecDocReq", lbClaimNo.Text, "", "", "", RecDoc, "", "", "", "", "", "", "", "" }); //DocRecDocReq = DocRec but specific for DocReq
                else
                    crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                        new string[] { "DocRec", lbClaimNo.Text, remind, "", "", RecDoc, "", "", "", "", "", "", "", "" });

                sec.refreshWhole();
                this.Close();
            }
            else
            {
                try
                {
                    string msg = "Do you want to send the email?";

                    //validations on some data
                    if (tbReceiver.Text.Trim() == "")
                    {
                        Msgbox.Show("Please input value for Receiver!");
                        return;
                    }

                    // oudom
                    //if (sp_type == "ParNew" || sp_type == "PayNew")
                    //{
                    //    if (cbReqNo.Text == "")
                    //    {
                    //        Msgbox.Show("Please select Requisition No before sending the email!");
                    //        return;
                    //    }
                    //}

                    // oudom
                    //if (sp_type == "ParNew")
                    //{
                    //    if (tbNonPaid.Text.Trim() == "")
                    //    {
                    //        Msgbox.Show("Please input value for Non-Paid Amount!");
                    //        return;
                    //    }
                    //}

                    //get content Update 16-Jul-19
                    //string content = getContent();
                    string content = (finalizecontent != "") ? finalizecontent : getContent();
                    //End of Update

                    // oudom
                    if (sp_type == "DocReqNew" || sp_type == "RemNew" || sp_type == "ParNew" || sp_type == "RejNew" || sp_type == "ClaimClosing") //Update 17-Jul-19
                    {
                        if (code == "")
                            msg = "No values are selected in the box! " + msg;
                    }

                    //Examples emails for each type
                    //Acknowledgement Email
                    //string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_FN", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid" }, new string[] { "C/001/HHNS/18/005095", "Ack", "", "", "", "" });
                    //Remind Email
                    //string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_FN", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid" }, new string[] { "C/001/HHNS/18/005095", "Rem", code, "Third", "", "" });
                    //Partial Payment Email
                    //string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_FN", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid" }, new string[] { "C/001/HHNS/18/005312", "Par", code, "", "R00118HNS006412", "500" });
                    //Payment Email
                    //string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_FN", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid" }, new string[] { "C/001/HHNS/18/005312", "Pay", "", "", "R00118HNS006412", "" });
                    //Reject Email
                    //string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_FN", new string[] { "claim_no", "doc_type", "req_doc", "remind", "requis", "nonPaid" }, new string[] { "C/001/HHNS/18/005312", "Rej", code, "", "", "" });

                    //confirmation before sending email
                    DialogResult res = Msgbox.Show(msg, "Confirmation");
                    if (res == System.Windows.Forms.DialogResult.No)
                        return;


                    //Update sending with insured name for ACLEDA employee dependant - Claim Team - Southeane 28-11-2022
                    //Update on insured name for Acleda Employee Dependant with Name - Theane

                    DataTable dt_insured = crud.ExecQuery("select trim(regexp_substr(ADD_INSURED, '[^/]+$', 1, 1)) as insured_name from (" +
                                 "select * from (" +
                                 "select PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO(INT_POLICY_SEQ,'ADDITIONAL INSURED') as ADD_INSURED " +
                                 "from cl_t_intimation " +
                                 "where INT_CLAIM_NO =  '" + lbClaimNo.Text + "') " +
                                 "where ADD_INSURED like '%ACLEDA%DEPENDANT%') "
                                );

                    //////
                    string subject;
                    if (dt_insured.Rows.Count <= 0)
                    {
                        subject = crud.ExecFunc_String("USER_GET_EMAIL_SUBJECT_FN", new string[] { "claim_no", "doc_type", "remind", "User_fullname" }, new string[] { lbClaimNo.Text, sp_type, remind, "" }).ToString();
                    }
                    else
                    {
                        subject = crud.ExecFunc_String("USER_GET_EMAIL_SUBJECT_FN", new string[] { "claim_no", "doc_type", "remind", "User_fullname" }, new string[] { lbClaimNo.Text, sp_type, remind, dt_insured.Rows[0][0].ToString().ToUpper() }).ToString();
                    }
                    //get the title

                    subject = subject.Replace('\r', ' ').Replace('\n', ' ');

                    if (sp_type == "ParNew")
                    {
                        if (frmANHSettlementLetterNew.DtNote != null && frmANHSettlementLetterNew.DtNote.Rows.Count > 0)
                        {
                            var note = string.Empty;
                            var count = 0;

                            for (int i = 0; i < frmANHSettlementLetterNew.DtNote.Rows.Count; i++)
                            {
                                var amt = frmANHSettlementLetterNew.DtNote.Rows[i]["Amount"].ToString();
                                var des = frmANHSettlementLetterNew.DtNote.Rows[i]["Description"].ToString();

                                note += string.Concat("- ", amt, ": ", des, "<br />");
                                count++;

                                if (frmANHSettlementLetterNew.DtNote.Rows.Count == count)
                                    note = note.Remove(note.Length - 6);
                            }

                            content = content.Replace("N/A", note);
                        }

                        if (frmANHSettlementLetterNew.DtExplainBene != null && frmANHSettlementLetterNew.DtExplainBene.Rows.Count > 0)
                        {
                            var expBeni = frmANHSettlementLetterNew.DtExplainBene;

                            decimal claimAmt = 0.00M;
                            decimal nonPaidAmt = 0.00M;
                            decimal paidAmt = 0.00M;

                            for (int i = 0; i < expBeni.Rows.Count; i++)
                            {
                                var curInUsd = expBeni.Rows[i]["CURRENCY_IN_USD"].ToString();
                                var expenses = expBeni.Rows[i]["EXPENSES_NOT_COVERED"].ToString();
                                var deductible = expBeni.Rows[i]["DEDUCTIBLE_OR_COPLAY"].ToString();
                                var overLimit = expBeni.Rows[i]["OVER_LIMIT"].ToString();

                                if (!string.IsNullOrEmpty(curInUsd))
                                    claimAmt += Convert.ToDecimal(curInUsd);

                                if (!string.IsNullOrEmpty(expenses))
                                    nonPaidAmt += Convert.ToDecimal(expenses);

                                if (!string.IsNullOrEmpty(deductible))
                                    nonPaidAmt += Convert.ToDecimal(deductible);

                                if (!string.IsNullOrEmpty(overLimit))
                                    nonPaidAmt += Convert.ToDecimal(overLimit);
                            }

                            paidAmt = claimAmt - nonPaidAmt;

                            content = content.Replace("%Paid%", string.Concat("USD ", paidAmt.ToString("0.00")));
                            content = content.Replace("%NonPaid%", string.Concat("USD ", nonPaidAmt.ToString("0.00")));
                        }
                    }

                    if (sp_type == "PayNew")
                    {
                        content = content.Replace("%Paid%", string.Concat("USD ", claimAmount));
                    }

                    if (remind == "First")
                    {
                        if (!content.Contains("FIRST REMINDER"))
                            content = "<p style=\"font-family:calibri;\"><b>*FIRST REMINDER*</b></p>" + content;

                        var first = new StringBuilder();
                        first.Append("<li>If the required documents are not fully submitted after the 1st reminder email, we will send the 2nd reminder email within <b>07 calendar days</b> from the 1st reminder email date.</li>")
                            .Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                            .Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                        content = content.Replace("%Notes%", first.ToString());
                    }
                    else if (remind == "Second")
                    {
                        if (!content.Contains("SECOND REMINDER"))
                            content = "<p style=\"font-family:calibri;\"><b>*SECOND REMINDER*</b></p>" + content;

                        var second = new StringBuilder();
                        second.Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                            .Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                        content = content.Replace("%Notes%", second.ToString());
                    }
                    else if (remind == "Third")
                    {
                        if (!content.Contains("LAST REMINDER"))
                            content = "<p style=\"font-family:calibri;\"><b>*LAST REMINDER*</b></p>" + content;

                        var third = new StringBuilder();
                        third.Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                        content = content.Replace("%Notes%", third.ToString());
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    sending_email(content, tbReceiver.Text, subject);

                    string tickedDoc = "";
                    foreach (ListViewItem lvi in lvDoc.CheckedItems)
                    {
                        tickedDoc += lvi.Text + ",";
                    }
                    tickedDoc = (tickedDoc != "") ? tickedDoc.Remove(tickedDoc.Length - 1) : "";

                    //call stored procedure to keep history
                    if (dt_insured.Rows.Count <= 0)
                    {
                        //oudom
                        crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                   new string[] { "Insert", lbClaimNo.Text, sp_type, tbReceiver.Text, ContentToSaveToHist, tickedDoc, req_no, remind, tbNonPaid.Text, resend ? "Y" : "N", doc_rec ? "Y" : "N", tbCC.Text, dpNoti.Value.ToString("dd-MMM-yyyy"), " " });
                    }
                    else
                    {
                        //oudom
                        crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                       new string[] { "Insert", lbClaimNo.Text, sp_type, tbReceiver.Text, ContentToSaveToHist, tickedDoc, req_no, remind, tbNonPaid.Text, resend ? "Y" : "N", doc_rec ? "Y" : "N", tbCC.Text, dpNoti.Value.ToString("dd-MMM-yyyy"), dt_insured.Rows[0][0].ToString().ToUpper() });
                    }

                    //call stored procedure to update Doc Received
                    //oudom
                    if (sp_type == "RemNew")
                    {
                        string RecDoc = "";
                        foreach (ListViewItem lvi in lvDoc.Items)
                        {
                            if (lvi.SubItems[3].Text == "Yes")
                                RecDoc += lvi.Text + ",";
                        }
                        RecDoc = (RecDoc != "") ? RecDoc.Remove(RecDoc.Length - 1) : "";

                        //oudom
                        crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                            new string[] { "DocRec", lbClaimNo.Text, remind, preRem, "", RecDoc, "", "", "", "", "", "", "", "" });
                    }

                    // oudom
                    if (sp_type == "DocReqNew")//Update 17-Jul-19 (Document Request)
                    {
                        string RecDoc = "";
                        foreach (ListViewItem lvi in lvDoc.Items)
                        {
                            if (lvi.SubItems[3].Text == "Yes")
                                RecDoc += lvi.Text + ",";
                        }
                        RecDoc = (RecDoc != "") ? RecDoc.Remove(RecDoc.Length - 1) : "";

                        //oudom
                        crud.ExecSP_NoOutPara("sp_user_claim_input_new", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                            new string[] { "DocRecDocReq", lbClaimNo.Text, "", "", "", RecDoc, "", "", "", "", "", "", "", "" });


                        if (!resend)
                        {
                            //save remind date to send auto reminder for Document Request
                            var docReqSendDate = DateTime.Now.Date;
                            var remFirstDate = docReqSendDate.AddDays(7);
                            var remSecondDate = remFirstDate.AddDays(7);
                            var remThirdDate = remSecondDate.AddDays(7);
                            var closeClaimDate = remThirdDate.AddDays(30);

                            var qBuilder = new StringBuilder();
                            qBuilder.Append("insert into user_claim_anh_auto_rem_email(doc_code, claim_number, first_rem_date, is_send_first_rem, second_rem_date, is_send_second_rem, third_rem_date, is_send_third_rem, close_claim_date, is_send_close_claim, sender, subject, user_name) ")
                                .AppendFormat("values('{0}','{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}',{9},'{10}','{11}','{12}')", tickedDoc, lbClaimNo.Text, remFirstDate.ToString("dd-MMM-yyyy"), 0, remSecondDate.ToString("dd-MMM-yyyy"), 0, remThirdDate.ToString("dd-MMM-yyyy"), 0, closeClaimDate.ToString("dd-MMM-yyyy"), 0, senderEmail, subject, UserName);

                            crud.ExecNonQuery(qBuilder.ToString());
                        }
                        else
                        {
                            crud.ExecNonQuery("update user_claim_anh_auto_rem_email set doc_code = '" + tickedDoc + "' where claim_number = '" + lbClaimNo.Text + "'");
                        }
                    }
                    saveEmailHistory(tbReceiver.Text.Trim().Replace(" ", String.Empty), tbCC.Text.Trim().Replace(" ", String.Empty));
                    requeryEmailSuggestion();

                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show("Email Sent.");

                    frmGenerateSettlementLetterNotice.Paid = string.Empty;
                    frmGenerateSettlementLetterNotice.NonPaid = string.Empty;

                    frmANHSettlementLetterNew.Paid = string.Empty;
                    frmANHSettlementLetterNew.NonPaid = string.Empty;
                    frmANHSettlementLetterNew.NonPayableReason = string.Empty;

                    //Delete Email Rejection Notice folder
                    //if (!string.IsNullOrEmpty(frmEmailNoticeAttachmentEdit.FolderPath))
                    //{
                    //    var dir = new DirectoryInfo(frmEmailNoticeAttachmentEdit.FolderPath);
                    //    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    //    dir.Delete(true);
                    //}

                    //Delete Settlement Notice folder
                    //if (!string.IsNullOrEmpty(frmGenerateSettlementLetterNotice.FPath))
                    //{
                    //    var dir = new DirectoryInfo(frmGenerateSettlementLetterNotice.FPath);
                    //    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    //    dir.Delete(true);
                    //}

                    //refresh the previous form
                    sec.refreshWhole();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Msgbox.Show(ex.Message);
                }


            }
        }

        void saveEmailHistory(string receiver, string cc)
        {
            string sql = "";
            string[] temp = receiver.Split(',');
            foreach (string s in temp)
            {
                if (s != "")
                {
                    sql = "INSERT INTO USER_EMAIL_SENDER_HIST (USER_CODE,SENDER) " +
                        "SELECT '" + UserName + "','" + s + "' FROM DUAL WHERE NOT EXISTS " +
                        "(SELECT * FROM USER_EMAIL_SENDER_HIST WHERE (USER_CODE = '" + UserName + "' AND SENDER = '" + s + "'))";
                    crud.ExecNonQuery(sql);
                }
            }

            temp = cc.Split(',');
            foreach (string s in temp)
            {
                if (s != "")
                {
                    sql = "INSERT INTO USER_EMAIL_CC_HIST (USER_CODE,CC) " +
                        "SELECT '" + UserName + "','" + s + "' FROM DUAL WHERE NOT EXISTS " +
                        "(SELECT * FROM USER_EMAIL_CC_HIST WHERE (USER_CODE = '" + UserName + "' AND CC = '" + s + "'))";
                    crud.ExecNonQuery(sql);
                }
            }
        }

        void requeryEmailSuggestion()
        {
            tbReceiver.EmailAutocompleteSource = null;
            tbCC.EmailAutocompleteSource = null;
            // Add Email suggestion
            string[] email = new string[1];
            DataTable dtTemp = crud.ExecQuery("SELECT DISTINCT SENDER FROM USER_EMAIL_SENDER_HIST WHERE USER_CODE = '" + UserName + "'");
            foreach (DataRow dr in dtTemp.Rows)
            {
                email[email.Length - 1] = dr["SENDER"].ToString();
                if (email.Length < dtTemp.Rows.Count)
                    Array.Resize(ref email, email.Length + 1);
            }
            tbReceiver.EmailAutocompleteSource = email;
            email = new string[1];
            dtTemp = crud.ExecQuery("SELECT DISTINCT CC FROM USER_EMAIL_CC_HIST WHERE USER_CODE = '" + UserName + "'");
            foreach (DataRow dr in dtTemp.Rows)
            {
                email[email.Length - 1] = dr["CC"].ToString();
                if (email.Length < dtTemp.Rows.Count)
                    Array.Resize(ref email, email.Length + 1);
            }
            tbCC.EmailAutocompleteSource = email;
            //
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbNonPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void bnPreview_Click(object sender, EventArgs e)
        {
            if (!rec_form)
            {
                string content = getContent();


                //Update 16-Jul-19 (Edit Email Content)

                if (finalizecontent != "") content = finalizecontent;

                //oudom
                DataTable dtHospital = crud.ExecQuery("select nvl(INT_OTH_HOSPITAL, INT_COMMENTS) HOSPITAL, nvl(INT_OTH_HOSPITAL, 'true') IS_NULL_OTH_HOSPITAL from CL_T_INTIMATION,CL_T_CLM_MEMBERS where CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + lbClaimNo.Text.ToUpper().Trim() + "'");
                var isNullOthHospital = dtHospital.Rows[0]["IS_NULL_OTH_HOSPITAL"].ToString() == "true";
                hospital = dtHospital.Rows[0]["HOSPITAL"].ToString();

                if (isNullOthHospital)
                {
                    var hIndex = hospital.IndexOf("H:");
                    if (hIndex == -1)
                        hIndex = hospital.IndexOf("H :");

                    var strHospital = hospital.Substring(hIndex + 2);

                    var dIndex = strHospital.IndexOf(":");

                    var tmpHospital = string.Empty;

                    if (dIndex != -1)
                        tmpHospital = strHospital.Substring(0, dIndex - 1).Trim();

                    var bHospital = tmpHospital.IndexOf("(");

                    hospital = bHospital != -1 ? tmpHospital.Substring(0, bHospital) : tmpHospital;
                }

                content = content.Replace("%Place%", hospital);

                //DataTable dtHospital = crud.ExecQuery("select (select COM_NAME from CL_M_ORGANIZATIONS where COM_CODE = INT_HOSPITAL_CODE) PANEL_HOSPITAL from CL_T_INTIMATION where INT_CLAIM_NO = '" + lbClaimNo.Text.ToUpper().Trim() + "'");
                //if (dtHospital.Rows.Count > 0)
                //{
                //    hospital = dtHospital.Rows[0]["PANEL_HOSPITAL"].ToString();
                //    content = content.Replace("%Place%", hospital);
                //}

                //if (!string.IsNullOrEmpty(frmGenerateSettlementLetterNotice.Paid))
                //    content = content.Replace("%Paid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.Paid).ToString("0.00"));

                //if (!string.IsNullOrEmpty(frmGenerateSettlementLetterNotice.NonPaid))
                //    content = content.Replace("%NonPaid%", "USD " + Convert.ToDecimal(frmGenerateSettlementLetterNotice.NonPaid).ToString("0.00"));

                if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.Paid.Trim()))
                    content = content.Replace("%Paid%", "USD " + frmANHSettlementLetterNew.Paid);

                if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.NonPaid.Trim()))
                    content = content.Replace("%NonPaid%", "USD " + frmANHSettlementLetterNew.NonPaid);

                if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.NonPayableReason.Trim()))
                    content = content.Replace("N/A", frmANHSettlementLetterNew.NonPayableReason);

                if (remind == "First")
                {
                    if (!content.Contains("FIRST REMINDER"))
                        content = "<p style=\"font-family:calibri;\"><b>*FIRST REMINDER*</b></p>" + content;

                    var first = new StringBuilder();
                    first.Append("<li>If the required documents are not fully submitted after the 1st reminder email, we will send the 2nd reminder email within <b>07 calendar days</b> from the 1st reminder email date.</li>")
                        .Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                        .Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                    content = content.Replace("%Notes%", first.ToString());
                }
                else if (remind == "Second")
                {
                    if (!content.Contains("SECOND REMINDER"))
                        content = "<p style=\"font-family:calibri;\"><b>*SECOND REMINDER*</b></p>" + content;

                    var second = new StringBuilder();
                    second.Append("<li>If the required documents are not fully submitted after the 2nd reminder email, we will send the last reminder email within <b>07 calendar days</b> from the 2nd reminder email date.</li>")
                        .Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                    content = content.Replace("%Notes%", second.ToString());
                }
                else if (remind == "Third")
                {
                    if (!content.Contains("LAST REMINDER"))
                        content = "<p style=\"font-family:calibri;\"><b>*LAST REMINDER*</b></p>" + content;

                    var third = new StringBuilder();
                    third.Append("<li>If the required documents still cannot be provided within <b>30 calendar days</b> from the last reminder email date, this claim will be settled based on available supporting documents.</li>");
                    content = content.Replace("%Notes%", third.ToString());
                }

                //End of Update

                // oudom
                frmViewEmailNew vem = new frmViewEmailNew();
                frmViewEmailNew.resetcontent = content;
                frmViewEmailNew.type = "A&H";
                string body = string.Empty;
                //using (StreamReader reader = new StreamReader("Html/Email.html"))


                //oudom
                //using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                //{
                //    body = reader.ReadToEnd();
                //}
                //body = body.Replace("{text}", content);
                //body = body.Replace("{department}", "A&H Claims Unit | Underwriting Department");

                ////Update 08-Jul-19

                //string sqltemp = "SELECT * FROM VIEW_CLAIM_EMAIL_UN_ADD WHERE USER_CODE = '" + UserName + "'";
                //DataTable dt = crud.ExecQuery(sqltemp);
                //if (dt.Rows.Count != 0)
                //{
                //    body = body.Replace("{username}", dt.Rows[0][1].ToString()); frmViewEmailNew.finalizeusername = dt.Rows[0][1].ToString(); //Update 16-Jul-19 (Edit Email Content)
                //    body = body.Replace("{user_email}", dt.Rows[0][2].ToString()); frmViewEmailNew.finalizemailadd = dt.Rows[0][2].ToString(); //Update 16-Jul-19 (Edit Email Content)
                //}
                ////End of Update
                //else
                //{
                //    body = body.Replace("{username}", UserFullName); frmViewEmail.finalizeusername = UserFullName; //Update 16-Jul-19 (Edit Email Content)
                //    body = body.Replace("{user_email}", mail_add); frmViewEmail.finalizemailadd = mail_add; //Update 16-Jul-19 (Edit Email Content)
                //}
                ////body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Forte_Logo.png");
                ////body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\FB_logo.png");
                //body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Standard_Forte.png");
                //body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\fb.png");
                //body = body.Replace("cid:YT_logo", Application.StartupPath + @"\Html\yt.png");
                //body = body.Replace("cid:Mail_logo", Application.StartupPath + @"\Html\mail.png");
                //vem.wbEmail.DocumentText = body;


                vem.ShowDialog();
            }
            else
            {
                if (sec.RecDoc(lbClaimNo.Text, remind))
                    this.Close();
            }
        }

        private void bnDocRec_Click(object sender, EventArgs e)
        {
            if (lvDoc.SelectedItems.Count <= 0)
            {
                Msgbox.Show("Please select the value first!");
                return;
            }

            if (lvDoc.SelectedItems[0].SubItems[3].Text == "Yes")
            {
                DialogResult dr = Msgbox.Show("Do you want to set back the status of " + lvDoc.SelectedItems[0].SubItems[1].Text + " to Not Received?", "Confirmation");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    lvDoc.SelectedItems[0].SubItems[3].Text = "";
                    lvDoc.SelectedItems[0].BackColor = Color.White;
                }
            }
            else
            {
                if (lvDoc.SelectedItems[0].Checked == false)
                {
                    Msgbox.Show("The item is not checked for required! Cannot set to received.");
                    return;
                }

                DialogResult dr = Msgbox.Show("Have you received " + lvDoc.SelectedItems[0].SubItems[1].Text + "?", "Confirmation");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    lvDoc.SelectedItems[0].SubItems[3].Text = "Yes";
                    lvDoc.SelectedItems[0].BackColor = redHi;
                }
            }

        }

        private void lvDoc_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (rec_form && !Init)
            {
                e.NewValue = e.CurrentValue;
                Msgbox.Show("You cannot change the required documents in this function!");
            }

            finalizecontent = "";//Update 20-Sep-19
        }

        private void bnAddExclu_Click(object sender, EventArgs e)
        {
            //frmAddExclusion frm = new frmAddExclusion();
            //frm.exclutype = lbClaimNo.Text.Substring(7, 3);
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); //Update 18-Jul-19 (Add Exclusion)
            //frm.ShowDialog();

            frmMailExclusion frm = new frmMailExclusion();
            frm.exclutype = lbClaimNo.Text.Substring(7, 3);
            frm.isExclusion = true;
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); //Update 18-Jul-19 (Add Exclusion)
            frm.ShowDialog();
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e) //Update 18-Jul-19 (Add Exclusion)
        {
            if (sp_type == "Rej" || sp_type == "Par")
            {
                lvDoc.Clear();
                lvDoc.Columns.Add("Exc Code", 70);
                lvDoc.Columns.Add("Exclusion Type", 350);
                DataTable dtExc = crud.ExecQuery("select * from user_claim_email_exclus where PRODUCT = '" + lbClaimNo.Text.Substring(7, 3) + "'");
                if (dtExc.Rows.Count == 0)
                {
                    dtExc.Clear();
                    dtExc = crud.ExecQuery("select * from user_claim_email_exclus");
                }
                foreach (DataRow dr in dtExc.Rows)
                {
                    ListViewItem lvi = new ListViewItem(dr["EXCL_CODE"].ToString());
                    lvi.SubItems.Add(dr["EXCLUSION"].ToString());
                    lvDoc.Items.Add(lvi);
                }
            }
            else if (sp_type == "DocReq" || sp_type == "Rem")
            {
                lvDoc.Clear();
                lvDoc.Columns.Add("Doc Code", 70);
                lvDoc.Columns.Add("Doc Type", 170);
                lvDoc.Columns.Add("Doc Detail", 350);
                lvDoc.Columns.Add("Doc Received", 100);

                DataTable dtDoc = crud.ExecQuery("select * from user_claim_email_doc");
                foreach (DataRow dr in dtDoc.Rows)
                {
                    ListViewItem lvi = new ListViewItem(dr["DOC_CODE"].ToString());
                    lvi.SubItems.Add(dr["DOC_TYPE"].ToString());
                    lvi.SubItems.Add(dr["DOC_CONTENT"].ToString());
                    lvi.SubItems.Add("");

                    lvDoc.Items.Add(lvi);
                }
            }
        }

        private void bnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbAttachFile.Text = tbAttachFile.Text + dlg.FileName.ToString() + ";";
                attachfile.Add(dlg.FileName.ToString());
            }
        }

        private void bnAddDoc_Click(object sender, EventArgs e)
        {
            //frmDocumentDetail frm = new frmDocumentDetail();
            //frm.doctype = lbClaimNo.Text.Substring(7, 3);
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); //Update 20-Sep-19 (Add Document)
            //frm.ShowDialog();

            frmMailExclusion frm = new frmMailExclusion();
            frm.exclutype = lbClaimNo.Text.Substring(7, 3);
            frm.isExclusion = false;
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); //Update 18-Jul-19 (Add Exclusion)
            frm.ShowDialog();
        }

        private void btnEditReceiver_Click(object sender, EventArgs e)
        {
            var frm = new frmEditReceiver();
            frm.Username = UserName;
            frm.ShowDialog();
        }

        private void btnAttachClaimRejection_Click(object sender, EventArgs e)
        {
            frmEmailNoticeAttachment frmEmailNotice = new frmEmailNoticeAttachment(lbClaimNo.Text.ToString().ToUpper());
            frmEmailNotice.ShowDialog();

            attachfile.Add(frmEmailNoticeAttachmentEdit.KhPath);
            attachfile.Add(frmEmailNoticeAttachmentEdit.EngPath);

            if (!string.IsNullOrEmpty(frmEmailNoticeAttachmentEdit.KhPath.Trim()) && !string.IsNullOrEmpty(frmEmailNoticeAttachmentEdit.EngPath.Trim()))
                tbAttachFile.Text = frmEmailNoticeAttachmentEdit.KhPath + ";" + frmEmailNoticeAttachmentEdit.EngPath + ";";
        }

        private void btnGenerateSettlementNotice_Click(object sender, EventArgs e)
        {
            //frmGenerateSettlementLetterNotice frm = new frmGenerateSettlementLetterNotice(sp_type, lbClaimNo.Text);
            //frm.ShowDialog();

            frmANHSettlementLetterNew frm = new frmANHSettlementLetterNew(lbClaimNo.Text);
            frm.ShowDialog(); 

            //attachfile.Add(frmANHSettlementLetterNewRV.FPath);
           
            //if (!string.IsNullOrEmpty(frmANHSettlementLetterNewRV.FPath))
            //    tbAttachFile.Text = frmANHSettlementLetterNewRV.FPath + ";";

            if (!string.IsNullOrEmpty(frmANHSettlementLetterNew.FClaimNo))
            {
                attachfile.Add(frmANHSettlementLetterNew.FilePath);
                tbAttachFile.Text = frmANHSettlementLetterNew.FilePath + ";";
            }
        }

        private void cboExclusionSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var proCode = lbClaimNo.Text.Substring(6, 4).ToLower();

            if (proCode.Contains("trv"))
            {
                string parts = cboExclusionSection.SelectedValue == null ? "ALL" : cboExclusionSection.SelectedValue.ToString();
                dtExc = crud.ExecQuery("select * from user_email_trv_excludef where PRODUCTS = 'TRV' and PARTS = '" + parts + "' order by PARTS");
            }
            else if (proCode.Contains("trp"))
            {
                string parts = cboExclusionSection.SelectedValue == null ? "GENERAL EXCLUSIONS" : cboExclusionSection.SelectedValue.ToString();
                dtExc = crud.ExecQuery("select * from user_email_trp_excludef where PRODUCTS = 'TRP' and PARTS = '" + parts + "' order by PARTS, ENG");
            }
            else if (proCode.Contains("tra"))
            {
                string parts = cboExclusionSection.SelectedValue == null ? "General Exclusions" : cboExclusionSection.SelectedValue.ToString();
                dtExc = crud.ExecQuery("select * from user_email_tra_excludef where PRODUCTS = 'TRA' and PARTS = '" + parts + "' order by PARTS, ENG");
            }

            lvDoc.Items.Clear();

            foreach (DataRow dr in dtExc.Rows)
            {
                if (proCode.Contains("trv"))
                {
                    ListViewItem lvi = new ListViewItem(dr["TRV_CODE"].ToString());
                    lvi.SubItems.Add(dr["ENG"].ToString());

                    lvDoc.Items.Add(lvi);
                }
                else if (proCode.Contains("trp"))
                {
                    ListViewItem lvi = new ListViewItem(dr["TRP_CODE"].ToString());
                    lvi.SubItems.Add(dr["ENG"].ToString());

                    lvDoc.Items.Add(lvi);
                }
                else if (proCode.Contains("tra"))
                {
                    ListViewItem lvi = new ListViewItem(dr["TRA_CODE"].ToString());
                    lvi.SubItems.Add(dr["ENG"].ToString());

                    lvDoc.Items.Add(lvi);
                }
            }
        }
    }
}
