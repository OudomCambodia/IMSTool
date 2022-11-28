using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmAutoLetters : Form
    {
        CRUD crud = new CRUD();
        public string Username = "";
        string HashPass = "Forte@2017";
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        string UserFullName = "";
        DataTable dt;
        DataGridViewRow SelectedRow = new DataGridViewRow();
        public static string finalizecontent = ""; //for Contr No-Letter

        public frmAutoLetters()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                string PolNo = tbPolicyNo.Text.Trim().ToUpper();
                if (PolNo == "" || PolNo.Length != 20)
                {
                    Msgbox.Show("Please enter policy number.");
                    tbPolicyNo.Focus();
                    return;
                }

                string DateFr = dtpDateofLoss.Value.ToString("yyyy/MM/dd") + " 00:00:00",
                    DateTo = dtpDateofLoss.Value.ToString("yyyy/MM/dd") + " 23:59:59";

                string cmd = "select POL_SEQ_NO,PRS_SEQ_NO,PRS_NAME,nvl(PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO(POL_SEQ_NO,'ADDITIONAL INSURED'),PK_UW_M_CUSTOMERS.fn_get_cust_name_full(POL_CUS_CODE)) ADDITIONAL_INSURED, " +
                "POL_POLICY_NO,POL_PERIOD_FROM,POL_PERIOD_TO,POL_BPARTY_CODE ACCOUNT_HANDLER, " +
                "POL_MARKETING_EXECUTIVE_CODE AGENT_CODE,nvl((SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE SFC_CODE=POL_MARKETING_EXECUTIVE_CODE),'') AGENT_NAME, " +
                "POL_SUM_INSURED,nvl(PK_MONTHLY_REPORTS.FN_GET_TRAN_PREMIUM_DNCN(PK_MONTHLY_REPORTS.FN_GET_ORIGINAL_DN(POL_POLICY_NO,POL_PERIOD_FROM)),0) POL_PREMIUM,nvl(PK_MONTHLY_REPORTS.FN_GET_DN_CN_ISPAID(PK_MONTHLY_REPORTS.FN_GET_ORIGINAL_DN(POL_POLICY_NO,POL_PERIOD_FROM)),'') as POL_PAYMENT_STATUS, " +
                "POL_EXCESS_TXT,(select RFT_DESCRIPTION from CM_R_REFERENCE_TWO where RFT_CODE = PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO(POL_SEQ_NO,'CO-INSURANCE') and RFT_TYPE = 'CI') as CO_INSURANCE " +
                "from UW_T_POLICIES a, UW_T_POL_RISKS b " +
                "where a.POL_SEQ_NO = b.PRS_PLC_POL_SEQ_NO " +
                "and POL_POLICY_NO = '" + PolNo + "'  " +
                "and POL_PERIOD_FROM <= TO_DATE('" + DateFr + "', 'YYYY/MM/DD HH24:MI:SS') " +
                "and POL_PERIOD_TO >= TO_DATE('" + DateTo + "', 'YYYY/MM/DD HH24:MI:SS')";


                Cursor.Current = Cursors.WaitCursor;
                DataTable dtTemp = crud.ExecQuery(cmd);
                dgvRiskList.DataSource = dtTemp;
                foreach (DataGridViewColumn col in dgvRiskList.Columns)
                {
                    col.Visible = false;
                }
                //dgvRiskList.Columns["PRS_SEQ_NO"].Visible = true;
                dgvRiskList.Columns["PRS_NAME"].Visible = true;
                if (dtTemp.Rows.Count <= 0)
                {
                    Msgbox.Show("No vehicle found.");
                    return;
                }
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void frmAutoLetters_Activated(object sender, EventArgs e)
        {
            tbPolicyNo.Focus();
        }

        private void frmAutoLetters_Load(object sender, EventArgs e)
        {
            dgvRiskList.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvRiskList.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            //getting configuration information about email from the database
            DataTable dtEmailIn = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "emailInfo", Username });
            smtpSer = dtEmailIn.Rows[0].ItemArray[0].ToString();
            mail_add = dtEmailIn.Rows[0].ItemArray[1].ToString();
            if (dtEmailIn.Rows[0].ItemArray[2].ToString() != "")
                mail_pass = Cipher.Decrypt(dtEmailIn.Rows[0].ItemArray[2].ToString(), HashPass);
            else
                mail_pass = "";
            port = Convert.ToInt16(dtEmailIn.Rows[0].ItemArray[3].ToString());
            UserFullName = dtEmailIn.Rows[0].ItemArray[4].ToString();

            frmDocumentControl.disabledButt(bnSendEmail);

            dgvClExp.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClExp.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            tabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlMain.Appearance = TabAppearance.FlatButtons;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string s = tbSearch.Text.Trim().ToUpper();
            if (s == "") return;
            (dgvRiskList.DataSource as DataTable).DefaultView.RowFilter = "PRS_NAME LIKE '%" + s + "%'";
        }

        private void dgvRiskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRiskList.SelectedRows.Count > 0)
            {
                //Set Policy Detail
                SelectedRow = dgvRiskList.SelectedRows[0]; //get Selected Row info
                tbInsured.Text = SelectedRow.Cells["ADDITIONAL_INSURED"].Value.ToString();
                tbPolicyNo1.Text = SelectedRow.Cells["POL_POLICY_NO"].Value.ToString();
                tbPolicyPeriod.Text = (Convert.ToDateTime(SelectedRow.Cells["POL_PERIOD_FROM"].Value).ToString("dd MMMM yyyy"))
                       + " to " + (Convert.ToDateTime(SelectedRow.Cells["POL_PERIOD_TO"].Value).ToString("dd MMMM yyyy"));
                tbIntermediary.Text = SelectedRow.Cells["AGENT_CODE"].Value.ToString() + " - " + SelectedRow.Cells["AGENT_NAME"].Value.ToString();
                tbAH.Text = SelectedRow.Cells["ACCOUNT_HANDLER"].Value.ToString();
                tbSumInsured.Text = String.Format("{0:N}", SelectedRow.Cells["POL_SUM_INSURED"].Value);
                tbPolicyPremium.Text = String.Format("{0:N}", SelectedRow.Cells["POL_PREMIUM"].Value);
                string PaymentStatus = SelectedRow.Cells["POL_PAYMENT_STATUS"].Value.ToString();
                tbPaidorOS.Text = (PaymentStatus == "Y") ? "PAID" : "O/S";
                tbDeductible.Text = SelectedRow.Cells["POL_EXCESS_TXT"].Value.ToString();
                tbCoIn.Text = SelectedRow.Cells["CO_INSURANCE"].Value.ToString();
                //

                string RiskSeq = SelectedRow.Cells["PRS_SEQ_NO"].Value.ToString();


                Cursor.Current = Cursors.WaitCursor;
                //Set Risk Detail
                DataTable dtTemp = crud.ExecSP_OutPara("SP_MOTOR_INFO", new string[] { "RISKSEQ" }, new string[] { RiskSeq });
                if (dtTemp.Rows.Count > 0)
                {
                    tbVehicleNo.Text = SelectedRow.Cells["PRS_NAME"].Value.ToString();
                    tbMakeModel.Text = dtTemp.Rows[0]["MAKE_MODEL"].ToString();
                    tbYearOfManu.Text = dtTemp.Rows[0]["YEAR_OF_MANUFACTURE"].ToString();
                    tbChasisNo.Text = dtTemp.Rows[0]["CHASSIS_NO"].ToString();
                    tbEngineNo.Text = dtTemp.Rows[0]["ENGINE_NO"].ToString();
                    tbCapacity.Text = dtTemp.Rows[0]["CAPACITY"].ToString() + " " + dtTemp.Rows[0]["CAPACITY_TYPE"].ToString();
                }
                //

                //Allow Send when Premium OS
                //if (tbPaidorOS.Text == "O/S")
                //    frmDocumentControl.enabledButt(bnSendEmail);
                //else
                //    frmDocumentControl.disabledButt(bnSendEmail);
                //
                frmDocumentControl.enabledButt(bnSendEmail); //Allow both Paid and OS

                //Check History
                dtTemp = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + RiskSeq + "' AND SEND_TYPE = 'PremiumOS' ORDER BY HIST_DATE DESC");
                if (dtTemp.Rows.Count > 0)
                {
                    lblSentDate.Text = Convert.ToDateTime(dtTemp.Rows[0]["HIST_DATE"]).ToString("dd MMMM yyyy");
                }
                //

                Cursor.Current = Cursors.AppStarting;

            }
            else
            {
                ClearControl();
            }
        }

        void ClearControl()
        {

            foreach (Control ctl in groupBox7.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }
            foreach (Control ctl in groupBox6.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }
            foreach (Control ctl in groupBox2.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            tbTo.Text = "";
            tbCC.Text = "";

            frmDocumentControl.disabledButt(bnSendEmail);
        }

        private void dgvRiskList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvRiskList);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
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
        private void OpenFile()
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No file to open!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                System.Diagnostics.Process.Start(dgvr.Cells[1].Value.ToString());

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No record to remove!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                dgvFile.Rows.Remove(dgvr);
        }

        private void dgvFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            OpenFile();
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvFile.Rows.Add(Path.GetFileName(file), file);
        }

        private void tbPolicyNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbPolicyNo.Text = tbPolicyNo.Text.ToUpper();
                bnSearch.PerformClick();
            }
        }

        private void tbPolicyNo_Leave(object sender, EventArgs e)
        {
            tbPolicyNo.Text = tbPolicyNo.Text.ToUpper();

        }

        private void bnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return;
                }

                if (tbVehicleNo.Text.Trim() == "")
                {
                    Msgbox.Show("Please select vehicle first!");
                    return;
                }

                if (tbTo.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email to!");
                    return;
                }


                string MsgText = "";
                if (tbPaidorOS.Text == "O/S")
                    MsgText = "Are you sure you want to send Email?";
                else
                    MsgText = "Are you sure you want to send Email? (Original DN Paid)";


                DialogResult dr = Msgbox.Show(MsgText, "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //get Admin Fee, Premium Before Admin
                    //DataTable dtTemp = crud.ExecQuery("Select POC_AMOUNT ADMIN_FEE,POL_TOTAL_TRANSACTION_AMOUNT - POC_AMOUNT PREMIUM_BEFORE_ADMIN "+
                    //"from UW_T_POLICIES a, UW_T_POL_OTH_CHARGES b "+
                    //"where a.POL_SEQ_NO = b.POC_POL_SEQ_NO and POL_SEQ_NO = '" + SelectedRow.Cells["POL_SEQ_NO"].Value.ToString() + "'");
                    DataTable dtTemp = crud.ExecQuery("Select 1 ADMIN_FEE, " +
                    "nvl(PK_MONTHLY_REPORTS.FN_GET_TRAN_PREMIUM_DNCN(PK_MONTHLY_REPORTS.FN_GET_ORIGINAL_DN(POL_POLICY_NO,POL_PERIOD_FROM)),0) - 1  PREMIUM_BEFORE_ADMIN " +
                    "from UW_T_POLICIES " +
                    "where POL_POLICY_NO = '" + SelectedRow.Cells["POL_POLICY_NO"].Value.ToString() + "'");
                    string AdminFee = "0.00", PremiumBeforeAdmin = "0.00";
                    if (dtTemp.Rows.Count > 0)
                    {
                        AdminFee = String.Format("{0:N}", dtTemp.Rows[0]["ADMIN_FEE"]);
                        PremiumBeforeAdmin = String.Format("{0:N}", dtTemp.Rows[0]["PREMIUM_BEFORE_ADMIN"]);
                    }
                    //


                    string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                        new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                        new string[] { "PremiumOS", "", tbInsured.Text, 
                    tbPolicyNo.Text,"",tbVehicleNo.Text,Convert.ToDateTime(dtpDateofLoss.Text).ToString("dd MMMM yyyy"),
                    tbPolicyPeriod.Text, PremiumBeforeAdmin, 
                    AdminFee, tbPolicyPremium.Text}).ToString();
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{text}", content);
                    body = body.Replace("{department}", "Claims Department");
                    body = body.Replace("{username}", UserFullName);
                    body = body.Replace("{user_email}", mail_add);

                    //SmtpClient client = new SmtpClient(smtpSer);

                    MailMessage message = new MailMessage();

                    //set formatting email message
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    message.From = new MailAddress(mail_add);

                    System.Net.ServicePointManager.Expect100Continue = true;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    if (tbTo.Text.Trim() != "")
                    {
                        string[] to = tbTo.Text.Split(';');
                        foreach (string s in to)
                        {
                            if (s.Trim() != "")
                                message.To.Add(s.Trim());
                        }
                    }

                    message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'PremiumOS'").Rows[0][0].ToString();
                    message.Subject = message.Subject.Replace("%PolicyNo%", tbPolicyNo.Text.ToUpper().Trim());
                    message.Subject = message.Subject.Replace("%PeriodofInsurance%", tbPolicyPeriod.Text);

                    //default CC auto claim team
                    dtTemp = crud.ExecQuery("SELECT MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE TEAM = 'Default'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string[] cc = dtTemp.Rows[0][0].ToString().Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }
                    //

                    if (tbCC.Text.Trim() != "")
                    {
                        string[] cc = tbCC.Text.Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }

                    //attachment
                    if (dgvFile.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgvFile.Rows)
                        {
                            message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                        }
                    }
                    //

                    //embeded pictures
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource img1 = new LinkedResource(@"Html\Standard_Forte.png", "image/png");
                    img1.ContentId = "Forte_Logo";
                    LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                    img2.ContentId = "FB_logo";
                    LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                    img3.ContentId = "YT_logo";
                    LinkedResource img4 = new LinkedResource(@"Html\mail.png", "image/png");
                    img4.ContentId = "Mail_logo";

                    avHtml.LinkedResources.Add(img1);
                    avHtml.LinkedResources.Add(img2);
                    avHtml.LinkedResources.Add(img3);
                    avHtml.LinkedResources.Add(img4);
                    message.AlternateViews.Add(avHtml);

                    //client.Host = smtpSer;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = true;
                    //client.Credentials = new System.Net.NetworkCredential
                    //{
                    //    UserName = "no-reply@forteinsurance.com",
                    //    Password = "Forte@12345"
                    //};
                    //client.EnableSsl = true;


                    ////client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);
                    ////client.EnableSsl = false;
                    //client.Port = port;
                    //client.Send(message);
                    var Credential = new System.Net.NetworkCredential(mail_add, mail_pass);
                    var result = CommonFunctions.SendEmail(Credential, message);
                    message.Dispose();
                    //client.Dispose();

                    crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                           new string[] { "Insert", SelectedRow.Cells["PRS_SEQ_NO"].Value.ToString(), "PremiumOS", tbTo.Text, content, "", "", "", "", "", "", tbCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), Username });


                    Msgbox.Show("Email sent!");
                    Cursor.Current = Cursors.AppStarting;
                    lblSentDate.Text = DateTime.Now.ToString("dd MMMM yyyy");

                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        #region Claims Experience

        private void bnClExpSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bnClExpClear.PerformClick();

            string searchdata = tbClExpSearch.Text.Trim().ToUpper();

            if (searchdata == "")
            {
                Msgbox.Show("Please input data to search.");
                tbClExpSearch.Focus();
                return;
            }

            /*string cmd = "SELECT DISTINCT POLICYNO,TO_CHAR(TO_DATE(INT_PERIOD_FROM,'DD/MM/YYYY'),'DD/MON/YYYY') INT_PERIOD_FROM,TO_CHAR(TO_DATE(INT_PERIOD_TO,'DD/MM/YYYY'),'DD/MON/YYYY') INT_PERIOD_TO,INT_PRS_NAME,ADDITIONAL_INSURED,ADDRESS,CLAIM_NO FROM VIEW_CLAIM_INFO WHERE MAIN_CLASS = 'AUTOMOBILE' ";

            if (rbPolNo.Checked)
                cmd += " AND POLICYNO = '" + searchdata + "'";
            else if (rbVehicleNo.Checked)
                cmd += " AND INT_PRS_NAME = '" + searchdata + "'";

            cmd += " ORDER BY INT_PERIOD_FROM";

            DataTable dtTemp = crud.ExecQuery(cmd);


            if (dtTemp.Rows.Count <= 0)
            {
                Msgbox.Show("No Data Found");
                return;
            }

            frmDocumentControl.enabledButt(bnClExpGenerate);

            dtTemp.Columns.Add("PAID_AMT", typeof(string));
            dtTemp.Columns.Add("PERIL", typeof(string));
            dtTemp.Columns.Add("CONTACT_PERSON", typeof(string));
            dtTemp.Columns.Add("CONTACT_NUMBER", typeof(string));
            

            foreach (DataRow dr in dtTemp.Rows)
            {
                DataTable dt = crud.ExecQuery("select RRD_REV_TYPE, SUM(RRD_VALUE) PAID_AMT, listagg(PERIL,', ') within group(order by PERIL) PERILS from "+
                " ( "+
                " select RRD_REV_TYPE,RRD_CLAIM_NO,PERIL,SUM(RRD_VALUE) RRD_VALUE from "+
                " ( "+
                " select RRD_REV_TYPE,RRD_CLAIM_NO, "+
                " replace((SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE),'AUTO - ','') PERIL, "+
                " RRD_VALUE from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + dr["CLAIM_NO"].ToString() + "' and RRD_REV_TYPE = 'TRA000' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0) group by PERIL,RRD_REV_TYPE,RRD_CLAIM_NO) " +
                " group by RRD_REV_TYPE");

                if (dt.Rows.Count <= 0) //no paid
                {
                    dr["PAID_AMT"] = "N/A";
                    dr["PERIL"] = "N/A";
                }
                else
                {
                    dr["PAID_AMT"] = String.Format("{0:N}", Convert.ToDecimal(dt.Rows[0]["PAID_AMT"].ToString()));
                    
                    dr["PERIL"] = dt.Rows[0]["PERILS"].ToString();
                }


                dt = crud.ExecQuery("SELECT (CASE WHEN CUS_TYPE = 'I' THEN CUS_INDV_SURNAME ELSE (SELECT CCT_NAME FROM UW_M_CUST_CONTACTS WHERE CCT_CUS_CODE = CUS_CODE AND ROWNUM = 1) END) AS CONTACT_PERSON, "+
                " (CASE WHEN CUS_TYPE = 'I' THEN CUS_PHONE_1 ELSE (SELECT CCT_PHONE_NO FROM UW_M_CUST_CONTACTS WHERE CCT_CUS_CODE = CUS_CODE AND ROWNUM = 1) END) AS CONTACT_PHONE  "+
                " FROM UW_M_CUSTOMERS WHERE CUS_STATUS = 'A' and CUS_CODE = (SELECT INT_CUS_CODE FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + dr["CLAIM_NO"].ToString() + "')");

                if (dt.Rows.Count > 0)
                {
                    dr["CONTACT_PERSON"] = dt.Rows[0]["CONTACT_PERSON"].ToString();
                    dr["CONTACT_NUMBER"] = dt.Rows[0]["CONTACT_PHONE"].ToString();
                }
            }   

            dgvClExp.DataSource = dtTemp;
            dgvClExp.Columns["ADDITIONAL_INSURED"].Visible = false;
            dgvClExp.Columns["ADDRESS"].Visible = false;
            dgvClExp.Columns["CLAIM_NO"].Visible = false;
            dgvClExp.Columns["CONTACT_PERSON"].Visible = false;
            dgvClExp.Columns["CONTACT_NUMBER"].Visible = false;
            */
            if (rbPolNo.Checked)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string sp_type = "view_claim_infoby_grouprisk";
                    string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                    //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                    string[] Values = new string[] { sp_type, tbClExpSearch.Text, "" };
                    dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);
                    //dgvResult.DataSource = dt;
                    dgvClExp.DataSource = dt;
                    Cursor.Current = Cursors.AppStarting;
                    if (dt.Rows.Count == 0)
                    {
                        Msgbox.Show("No data found!");
                    }
                    else
                    {
                        frmDocumentControl.enabledButt(bnClExpGenerate);
                    }

                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show(ex.Message);
                }

            }
            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string sp_type = "view_claim_infoby_grouprisk_vehichle";
                    string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                    //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                    string[] Values = new string[] { sp_type, tbClExpSearch.Text, "" };
                    dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);
                    //dgvResult.DataSource = dt;
                    dgvClExp.DataSource = dt;
                    Cursor.Current = Cursors.AppStarting;
                    if (dt.Rows.Count == 0)
                    {
                        Msgbox.Show("No data found!");
                    }
                    else
                    {
                        frmDocumentControl.enabledButt(bnClExpGenerate);
                    }

                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show(ex.Message);
                }
            }
            
            Cursor.Current = Cursors.AppStarting;
            dgvClExp.Columns["ADDRESS"].Visible = false;
            dgvClExp.Columns["CONTACT_PERSON"].Visible = false;
            dgvClExp.Columns["CONTACT_NUMBER"].Visible = false;
            dgvClExp.Columns["ADDITIONAL_INSURED"].Visible = false;
        }

        private void bnClExpClear_Click(object sender, EventArgs e)
        {
            frmDocumentControl.disabledButt(bnClExpGenerate);
            dgvClExp.DataSource = null;
            dgvClExp.Columns.Clear();
            ClExpViewer.ReportSource = null;
        }

        private void bnClExpGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            DataTable Latest = new DataTable();
            foreach (DataGridViewColumn col in dgvClExp.Columns)
            {
                Latest.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgvClExp.Rows)
            {
                DataRow dRow = Latest.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                Latest.Rows.Add(dRow);
            }

            //CrystalDecisions.CrystalReports.Engine.ReportClass report = new CrystalDecisions.CrystalReports.Engine.ReportClass();
            CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (rbPolNo.Checked)
                report = new Reports.ClExpPolNo();
            else if (rbVehicleNo.Checked)
                report = new Reports.ClExpVehicleNo();
            report.SummaryInfo.ReportTitle = ("Claim Experience Report - " + Latest.Rows[0]["POLICYNO"].ToString() + " - " + DateTime.Now.ToString("dd MMMM yyyy")).Replace('/', '-');
            report.SetDataSource(Latest);
            ClExpViewer.ReportSource = report;

            ClExpViewer.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat) + (int)(CrystalDecisions.Shared.ViewerExportFormats.WordFormat); 
            //set Export format type

            Cursor.Current = Cursors.AppStarting;

        }
        #endregion
        
        #region Claims Rejection

        private void bnClReSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string clno = tbClReClaimNo.Text.Trim().ToUpper();

                bnClReClear.PerformClick();

                if (clno == "" || clno.Length != 20)
                {
                    Msgbox.Show("Please Input Claim No!");
                    return;
                }

                DataTable clinfo = crud.ExecQuery("SELECT * FROM VIEW_CLAIM_INFO WHERE CLAIM_NO = '" + clno + "' AND ROWNUM = 1");

                if (clinfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim No not found!");
                    tbClReClaimNo.Focus();
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    tbClReRiskName.Text = clinfo.Rows[0]["INT_PRS_NAME"].ToString();
                    tbClReDateofLoss.Text = Convert.ToDateTime(clinfo.Rows[0]["DATEOFLOSS"]).ToString("dd'/'MM'/'yyyy");
                    tbClRePlaceofAcc.Text = clinfo.Rows[0]["INT_PLACE_LOSS"].ToString();
                    tbClReLossInfo.Text = clinfo.Rows[0]["LOSS_DESC"].ToString();
                    tbClReIncurredAmount.Text = String.Format("{0:N}", clinfo.Rows[0]["INCURRED_AMT"]);
                    tbClReInsured.Text = clinfo.Rows[0]["ADDITIONAL_INSURED"].ToString();
                    tbClRePolNo.Text = clinfo.Rows[0]["POLICYNO"].ToString();
                    tbClRePolPeriod.Text = "from " + (Convert.ToDateTime(clinfo.Rows[0]["INT_PERIOD_FROM"]).ToString("dd'/'MM'/'yyyy"))
                        + " to " + (Convert.ToDateTime(clinfo.Rows[0]["INT_PERIOD_TO"]).ToString("dd'/'MM'/'yyyy"));
                    tbClReInter.Text = clinfo.Rows[0]["AGENT_CODE"].ToString() + " - " + clinfo.Rows[0]["AGENT_NAME"].ToString();
                    tbClReAH.Text = clinfo.Rows[0]["ACCOUNT_HANDLER"].ToString();
                    tbClReRiskSI.Text = String.Format("{0:N}", clinfo.Rows[0]["RISK_SUM_INSURED"]);
                    tbClRePolPremium.Text = String.Format("{0:N}", clinfo.Rows[0]["POL_PREMIUM"]);
                    string PaymentStatus = clinfo.Rows[0]["POL_PAYMENT_STATUS"].ToString();
                    tbClRePremiumPaid.Text = (PaymentStatus == "Y") ? "PAID" : "O/S";
                    tbClReNatureofLoss.Text = clinfo.Rows[0]["NATUREOFLOSS"].ToString();


                    //Deductible,CoIn
                    DataTable dtTemp = crud.ExecQuery("select POL_EXCESS_TXT, " +
                    "(select RFT_DESCRIPTION from CM_R_REFERENCE_TWO where RFT_CODE =  " +
                    "PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO((select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "'),'CO-INSURANCE') AND RFT_TYPE = 'CI') as CO_INSURANCE from " +
                    "(select POL_SEQ_NO,POL_EXCESS_TXT from UW_T_POLICIES where POL_CLA_CODE = 'AUTO' and POL_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select EDT_SEQ_NO,EDT_EXCESS_TXT from UW_T_ENDORSEMENTS where EDT_CLA_CODE = 'AUTO' and EDT_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select PHS_SEQ_NO,PHS_EXCESS_TXT from UW_H_POLICY_HISTORY where PHS_CLA_CODE = 'AUTO' and PHS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select NDS_SEQ_NO,NDS_EXCESS_TXT from UW_H_ENDORSEMENT_HISTORY where NDS_CLA_CODE = 'AUTO' and NDS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "')) T1");
                    string DedutibleText = "", CoInsurance = "";
                    if (dtTemp.Rows.Count > 0)
                    {
                        DedutibleText = dtTemp.Rows[0]["POL_EXCESS_TXT"].ToString();
                        CoInsurance = dtTemp.Rows[0]["CO_INSURANCE"].ToString();
                    }
                    tbClReDeduct.Text = (DedutibleText == "") ? "N/A" : DedutibleText;
                    tbClReCoIn.Text = (CoInsurance == "") ? "N/A" : CoInsurance;
                    //

                    //Risk Info
                    dtTemp = crud.ExecSP_OutPara("SP_MOTOR_CL_INFO", new string[] { "WKCLAIMNO" }, new string[] { clno });
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbClReVechileNo.Text = tbClReRiskName.Text;
                        tbClReMakeModel.Text = dtTemp.Rows[0]["MAKE_MODEL"].ToString();
                        tbClReYear.Text = dtTemp.Rows[0]["YEAR_OF_MANUFACTURE"].ToString();
                        tbClReChasis.Text = dtTemp.Rows[0]["CHASSIS_NO"].ToString();
                        tbClReEngineNo.Text = dtTemp.Rows[0]["ENGINE_NO"].ToString();
                        tbClReCapacity.Text = dtTemp.Rows[0]["CAPACITY"].ToString() + " " + dtTemp.Rows[0]["CAPACITY_TYPE"].ToString();
                    }
                    //


                    frmDocumentControl.enabledButt(bnClReSendEmail);
                    bnClReRiskEndoDetail.Enabled = true;


                    DataTable histtbl = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + clno + "' AND SEND_TYPE = 'AutoRejection' ORDER BY HIST_DATE DESC");
                    if (histtbl.Rows.Count > 0) //has history
                    {
                        lblClReSentDate.Text = Convert.ToDateTime(histtbl.Rows[0]["HIST_DATE"]).ToString("dd MMMM yyyy");
                    }
                    histtbl = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + clno + "' AND SEND_TYPE = 'AutoContribute' ORDER BY HIST_DATE DESC");
                    if (histtbl.Rows.Count > 0) //has history
                    {
                        lblClReSentDateCon.Text = Convert.ToDateTime(histtbl.Rows[0]["HIST_DATE"]).ToString("dd MMMM yyyy");
                    }
                    histtbl = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + clno + "' AND SEND_TYPE = 'AutoContributeNoLetter' ORDER BY HIST_DATE DESC");
                    if (histtbl.Rows.Count > 0) //has history
                    {
                        lblClReSentDateConNoLetter.Text = Convert.ToDateTime(histtbl.Rows[0]["HIST_DATE"]).ToString("dd MMMM yyyy");
                    }


                    //get Account Handler email, CC
                    string agentcode = clinfo.Rows[0]["AGENT_CODE"].ToString(), ahcode = clinfo.Rows[0]["ACCOUNT_HANDLER"].ToString(), selectedcode;
                    if (agentcode[0] == '0' || ahcode == "U-BNK") //001,002,003 => no agent/broker          //U-BNK => bank 
                        selectedcode = ahcode;
                    else //agent/broker...
                        selectedcode = agentcode;

                    dtTemp = crud.ExecQuery("SELECT MAIL_TO,MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE CODE = '" + selectedcode + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbClReTo.Text = dtTemp.Rows[0]["MAIL_TO"].ToString();
                        tbClReCC.Text = dtTemp.Rows[0]["MAIL_CC"].ToString();
                    }
                    //

                    Cursor.Current = Cursors.AppStarting;
                    bnClReRiskEndoDetail.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void bnClReClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in groupBox8.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
                if (ctl is RichTextBox)
                {
                    ((RichTextBox)ctl).Text = "";
                }
            }
            foreach (Control ctl in groupBox9.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }
            foreach (Control ctl in groupBox4.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            tbClReTo.Text = "";
            tbClReCC.Text = "";
            frmDocumentControl.disabledButt(bnClReSendEmail);
            lblClReSentDate.Text = "";

            dgvClReAttachment.Rows.Clear();
            dgvClReAttachment.DataSource = null;

            bnClReRiskEndoDetail.Enabled = false;
        }

        private void bnClReBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdUpload = new OpenFileDialog();
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            ofdUpload.Multiselect = true;
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvClReAttachment.Rows.Count; i++)
                    {
                        if (dgvClReAttachment.Rows[i].Cells[0].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }
                    dgvClReAttachment.Rows.Add(filename, path);
                }
            }
        }

        private void bnClReOpen_Click(object sender, EventArgs e)
        {
            if (dgvClReAttachment.Rows.Count <= 0)
            {
                Msgbox.Show("No file to open!");
                return;
            }
            OpenFileClRe();
        }

        void OpenFileClRe()
        {
            foreach (DataGridViewRow dgvr in dgvClReAttachment.SelectedRows)
                System.Diagnostics.Process.Start(dgvr.Cells[1].Value.ToString());
        }

        private void bnClReRemove_Click(object sender, EventArgs e)
        {
            if (dgvClReAttachment.Rows.Count <= 0)
            {
                Msgbox.Show("No record to remove!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvClReAttachment.SelectedRows)
                dgvClReAttachment.Rows.Remove(dgvr);
        }

        private void dgvClReAttachment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            OpenFileClRe();
        }

        private void dgvClReAttachment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void dgvClReAttachment_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvClReAttachment.Rows.Add(Path.GetFileName(file), file);
        }

        private void bnClReRiskEndoDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var frm = new frmDeductibleRiskEndo(tbClReRiskName.Text, tbClRePolNo.Text);
            frm.ShowDialog();
            Cursor.Current = Cursors.AppStarting;
        }

        void SendClRejectionEmail(int type)
        {
            //type 1 = Rejection / 2 = Contribute

            try
            {
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return;
                }

                if (tbClReRiskName.Text.Trim() == "")
                {
                    Msgbox.Show("Please select vehicle first!");
                    return;
                }

                if (tbClReTo.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email to!");
                    return;
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string content = "", note = tbClReNote.Text.Trim().Replace("\n", "<br>");

                    //if (note != "")
                    //    note = "<strong>Notes: </strong>" + note;

                    if (type == 1)
                        content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                        new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                        new string[] { "AutoRejection", "", tbClReInsured.Text.Trim(), 
                        tbClRePolNo.Text,tbClReClaimNo.Text.Trim().ToUpper(),tbClReRiskName.Text,
                        Convert.ToDateTime(tbClReDateofLoss.Text).ToString("dd MMMM yyyy"),
                        tbClRePlaceofAcc.Text, tbClReNatureofLoss.Text, 
                        note, ""}).ToString();
                    else if (type == 2)
                        content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                        new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                        new string[] { "AutoContribute", "", tbClReInsured.Text.Trim(), 
                        tbClRePolNo.Text,tbClReClaimNo.Text.Trim().ToUpper(),tbClReRiskName.Text,
                        Convert.ToDateTime(tbClReDateofLoss.Text).ToString("dd MMMM yyyy"),
                        tbClRePlaceofAcc.Text, tbClReNatureofLoss.Text, 
                        note, tbClRePolDeduct.Text.Trim()}).ToString();
                    else if (type == 3)
                        content = finalizecontent;


                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{text}", content);
                    body = body.Replace("{department}", "Claims Department");
                    body = body.Replace("{username}", UserFullName);
                    body = body.Replace("{user_email}", mail_add);

                    //SmtpClient client = new SmtpClient(smtpSer);

                    MailMessage message = new MailMessage();

                    //set formatting email message
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    message.From = new MailAddress(mail_add);

                    System.Net.ServicePointManager.Expect100Continue = true;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;


                    if (tbClReTo.Text.Trim() != "")
                    {
                        string[] to = tbClReTo.Text.Split(';');
                        foreach (string s in to)
                        {
                            if (s.Trim() != "")
                                message.To.Add(s.Trim());
                        }
                    }

                    if(type == 1)
                        message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'AutoRejection'").Rows[0][0].ToString();
                    else if(type == 2)
                        message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'AutoContribute'").Rows[0][0].ToString();
                    else if (type == 3)
                        message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'AutoContributeNoLetter'").Rows[0][0].ToString();

                    message.Subject = message.Subject.Replace("%ClaimNo%", tbClReClaimNo.Text.Trim().ToUpper());
                    message.Subject = message.Subject.Replace("%RegNo%", tbClReRiskName.Text);
                    message.Subject = message.Subject.Replace("%DateofLoss%", Convert.ToDateTime(tbClReDateofLoss.Text).ToString("dd MMMM yyyy"));

                    //default CC auto claim team
                    DataTable dtTemp = crud.ExecQuery("SELECT MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE TEAM = 'Default'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string[] cc = dtTemp.Rows[0][0].ToString().Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }


                    if (tbClReCC.Text.Trim() != "")
                    {
                        string[] cc = tbClReCC.Text.Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }

                    //attachment
                    if (dgvClReAttachment.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgvClReAttachment.Rows)
                        {
                            message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                        }
                    }
                    //

                    //embeded pictures
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource img1 = new LinkedResource(@"Html\Standard_Forte.png", "image/png");
                    img1.ContentId = "Forte_Logo";
                    LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                    img2.ContentId = "FB_logo";
                    LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                    img3.ContentId = "YT_logo";
                    LinkedResource img4 = new LinkedResource(@"Html\mail.png", "image/png");
                    img4.ContentId = "Mail_logo";

                    avHtml.LinkedResources.Add(img1);
                    avHtml.LinkedResources.Add(img2);
                    avHtml.LinkedResources.Add(img3);
                    avHtml.LinkedResources.Add(img4);
                    message.AlternateViews.Add(avHtml);


                    //client.Host = smtpSer;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = true;
                    //client.Credentials = new System.Net.NetworkCredential
                    //{
                    //    UserName = "no-reply@forteinsurance.com",
                    //    Password = "Forte@12345"
                    //};
                    //client.EnableSsl = true;

                    ////client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);
                    ////client.EnableSsl = false;
                    //client.Port = port;
                    //client.Send(message);
                    var Credential = new System.Net.NetworkCredential(mail_add, mail_pass);
                    var result = CommonFunctions.SendEmail(Credential, message);
                    message.Dispose();
                    //client.Dispose();

                    crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                        new string[] { "Insert", tbClReClaimNo.Text.Trim().ToUpper(), (type == 1) ? "AutoRejection" : (type == 2) ? "AutoContribute" : (type == 3) ? "AutoContributeNoLetter" : "", tbClReTo.Text, content, "", "", "", "", "", "", tbClReCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), Username });


                    Msgbox.Show("Email sent!");
                    Cursor.Current = Cursors.AppStarting;
                    if(type == 1)
                        lblClReSentDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                    else if (type ==2)
                        lblClReSentDateCon.Text = DateTime.Now.ToString("dd MMMM yyyy");
                    else if (type == 3)
                        lblClReSentDateConNoLetter.Text = DateTime.Now.ToString("dd MMMM yyyy");

                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void bnClReSendEmail_Click(object sender, EventArgs e)
        {
            SendClRejectionEmail(1);
        }

        private void bnClReSendEmailCon_Click(object sender, EventArgs e)
        {
            SendClRejectionEmail(2);
        }

        private void bnClReSendEmailConNoLetter_Click(object sender, EventArgs e)
        {
            try
            {
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return;
                }

                if (tbClReRiskName.Text.Trim() == "")
                {
                    Msgbox.Show("Please select vehicle first!");
                    return;
                }

                if (tbClReTo.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email to!");
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;

                string content = "", note = tbClReNote.Text.Trim();

                if (note != "")
                    note = "<strong>Notes: </strong>" + note;
                
                content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                    new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                    new string[] { "AutoContributeNoLetter", "", tbClReInsured.Text.Trim(), 
                        tbClRePolNo.Text,tbClReClaimNo.Text.Trim().ToUpper(),tbClReRiskName.Text,
                        Convert.ToDateTime(tbClReDateofLoss.Text).ToString("dd MMMM yyyy"),
                        tbClRePlaceofAcc.Text, tbClReNatureofLoss.Text, 
                        note, tbClRePolDeduct.Text.Trim()}).ToString();

                var frm = new frmViewEmail();
                frmViewEmail.type = "AUTO";
                frmViewEmail.resetcontent = content;
                frmViewEmail.finalizemailadd = mail_add;
                frmViewEmail.finalizeusername = UserFullName;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{text}", content);
                body = body.Replace("{username}", UserFullName);
                body = body.Replace("{department}", "Claims Department");
                body = body.Replace("{user_email}", mail_add);
                body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Standard_Forte.png");
                body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\fb.png");
                body = body.Replace("cid:YT_logo", Application.StartupPath + @"\Html\yt.png");
                body = body.Replace("cid:Mail_logo", Application.StartupPath + @"\Html\mail.png");

                frm.wbEmail.DocumentText = body;
                frm.FormClosed += new FormClosedEventHandler(frmClose);
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        void frmClose(object sender, FormClosedEventArgs e)
        {
            SendClRejectionEmail(3);            
        }

        #endregion


        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControlMain.SelectedIndex)
            {
                e.Graphics.DrawString(tabControlMain.TabPages[e.Index].Text,
                    new Font(tabControlMain.Font.FontFamily, 10f, FontStyle.Bold),
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControlMain.TabPages[e.Index].Text,
                    new Font(tabControlMain.Font.FontFamily, 10f, FontStyle.Regular),
                    Brushes.Gray,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }
    }
}
