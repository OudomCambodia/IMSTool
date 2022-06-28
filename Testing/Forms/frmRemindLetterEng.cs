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
    public partial class frmRemindLetterEng : Form
    {
        CRUD crud = new CRUD();
        int num;
        string datetemp = DateTime.Now.ToString("dd MMMM yyyy");
        DataTable dt;
        CheckBox checkboxHeader = new CheckBox();
        public string Username = "";
        string HashPass = "Forte@2017";
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;

        DataGridViewRow SelectedRow = new DataGridViewRow();
        string UserFullName = "";
        //int num;
        bool isChecked;
        public frmRemindLetterEng()
        {
            InitializeComponent();
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            dgvResult.DataSource = null;
            dgvResult.Columns.Clear();
            checkboxHeader.Visible = false;
            dgvFile.DataSource = null;
            label5.Text = "";
            tbCC.Text = "";
            tbTo.Text = "";
            label5.Text = "DD-MMM-YYYY";
            lblSel.Text = "";
            lbTotal.Text = "";
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                string sp_type = "Engineering";
                string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                string[] Values = new string[] { sp_type, dtpDateFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpDateTo.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                dt = crud.ExecSP_OutPara("user_engineering_uw", Keys, Values);

                Cursor.Current = Cursors.AppStarting;
                if (dt.Rows.Count == 0)
                {
                    Msgbox.Show("No data found!");
                }
                else
                {
                    dgvResult.DataSource = null;
                    dgvResult.Columns.Clear();
                    bnSendEmail.Enabled = true;
                    DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                    //CheckBox chk = new CheckBox();
                    dgvResult.Columns.Add(CheckboxColumn);
                    dgvResult.DataSource = dt;

                    dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewColumn column = dgvResult.Columns[0];
                    column.Width = 35;
                    dgvResult.Columns[0].Resizable = DataGridViewTriState.False;
                    dgvResult.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    // add checkbox header
                    Rectangle rect = dgvResult.GetCellDisplayRectangle(0, -1, true);
                    // set checkbox header to center of header cell. +1 pixel to position correctly.
                    rect.X = rect.Location.X + 10;
                    rect.Y = rect.Location.Y + 15;
                    rect.Width = rect.Size.Width;
                    rect.Height = rect.Size.Height;

                    checkboxHeader.Checked = false;
                    checkboxHeader.Visible = true;
                    checkboxHeader.Name = "checkboxHeader";
                    checkboxHeader.Size = new Size(15, 15);
                    checkboxHeader.Location = rect.Location;
                    checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                    dgvResult.Controls.Add(checkboxHeader);

                    for (int i = 1; i < dgvResult.Columns.Count; i++)
                    {
                        dgvResult.Columns[i].ReadOnly = true;
                        //if (i == 2)
                        //    dgvResult.Columns[i].Visible = false;
                        //if (i==3) 

                    }
                    dgvResult.Columns[2].Visible = false;
                    dgvResult.Columns[3].Visible = false;
                    dgvResult.Columns[4].Visible = false;
                }
                lbTotal.Text = dgvResult.Rows.Count.ToString();
                dgvResult.ClearSelection();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show(ex.Message);
            }
        }
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                for (int i = 0; i < dgvResult.RowCount; i++)
                {
                    dgvResult[0, i].Value = ((CheckBox)dgvResult.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dgvResult[0, i].Value;
                    CheckCount(isChecked);
                }
                lblSel.Text = num.ToString();
                dgvResult.EndEdit();


            }
            catch (Exception EX)
            {
                Msgbox.Show(EX.Message);
            }
        }
        private void CheckCount(bool isChecked)
        {
            if (isChecked)
                num++;
        }


        private void dgvResult_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            num = 0;
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex != 0)
                return;


            if (dgvResult.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvResult.SelectedCells)
                {
                    dgvResult[0, dgvc.RowIndex].Value = true;
                   
                }
                for (int i = 0; i < dgvResult.RowCount; i++)
                {
                    isChecked = (bool)dgvResult.Rows[i].Cells[0].EditedFormattedValue;
                   
                    CheckCount(isChecked);
                }
                lblSel.Text = num.ToString();
            }
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {

            CommonFunctions.HighLightGrid(dgvResult);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
            if (directchoosedlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable selectedDoc = GetDataTableFromDGV(dgvResult, directchoosedlg,1);
                if (selectedDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No record selected!");
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DialogResult dr = Msgbox.Show("Download successfully,would you like to preview letter?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Reports.EngineeringLettereView rpt = new Reports.EngineeringLettereView();
                        //Reports.EngineeringLetterDW rpt1 = new Reports.EngineeringLetterDW();
                        //string datetemp = DateTime.Now.ToString("dd MMMM yyyy");
                        rpt.SetDataSource(selectedDoc);
                        rpt.SetParameterValue("DATE_CURR", datetemp.ToUpper());
                        //rpt.SetDataSource(selectedDoc);
                        //rpt.SetParameterValue("DATE_CURR", datetemp.ToUpper());
                        var frm = new frmViewInstructionNote();
                        frm.rpt = rpt;
                        frm.Show();
                    }
                    else
                    {
                        // Msgbox.Show("Download Successfully!");
                        return;
                    }


                }
            }
        }
        private DataTable GetDataTableFromDGV(DataGridView dgv, FolderBrowserDialog directchoosedlg, int Option)
        {

            DataTable dt1 = new DataTable();
            string status = "";
            string folderPath = "";
            string filep="";
            dt1.Columns.Add("POLICY_NO");
            dt1.Columns.Add("ADD_INSURED_NAME");
            dt1.Columns.Add("PRODUCT");
            dt1.Columns.Add("DUE_DATE");
            dt1.Columns.Add("INSURED_NAME");
            dt1.Columns.Add("POL_PERIOD_FROM");
            dt1.Columns.Add("POL_PERIOD_TO");
            dt1.Columns.Add("TITLE_CONTRACT");
            dt1.Columns.Add("MAINTENANCE_PERIOD");
            dt1.Columns.Add("LOC_DES");
            dt1.Columns.Add("ACC_HANDLER");
            dt1.Columns.Add("ACC_HANDLER_NAME");
            dt1.Columns.Add("INTERME_CODE");
            dt1.Columns.Add("INTERME_NAME");
            dt1.Columns.Add("PATH");
            DataTable dTemp = new DataTable();
            dTemp = dt1.Clone();

            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {
                        if (Option != 1)
                        {
                            filep = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\POLICY EXPIRY REMINDING LETTER-" + row.Cells["POLICY_NO"].Value.ToString().Replace('/', '-') + "-" + row.Cells["POL_PERIOD_TO"].Value.ToString() + ".pdf";
                           
                        }
                        else
                        {
                            
                            if (filep == "")
                            {
                                filep = directchoosedlg.SelectedPath;
                                if (filep.Substring(filep.Length - 2) != "\\")
                                    filep = directchoosedlg.SelectedPath + "\\";
                                else
                                    filep = directchoosedlg.SelectedPath;

                                folderPath = filep;
                                //else break;
                                
                            }
                            filep += "POLICY EXPIRY REMINDING LETTER-" + row.Cells["POLICY_NO"].Value.ToString().Replace('/', '-') + "-" + row.Cells["POL_PERIOD_TO"].Value.ToString() + ".pdf"; 
                           
                        }
                        
                        
                        dt1.Rows.Add(row.Cells["POLICY_NO"].Value.ToString(), row.Cells["ADD_INSURED_NAME"].Value.ToString(), row.Cells["PRODUCT"].Value.ToString(), row.Cells["DUE_DATE"].Value.ToString(), row.Cells["INSURED_NAME"].Value.ToString(), row.Cells["POL_PERIOD_FROM"].Value.ToString(), row.Cells["POL_PERIOD_TO"].Value.ToString(), row.Cells["TITLE_CONTRACT"].Value.ToString(), row.Cells["MAINTENANCE_PERIOD"].Value.ToString(), row.Cells["LOC_DES"].Value.ToString(), row.Cells["ACC_HANDLER"].Value.ToString(), row.Cells["ACC_HANDLER_NAME"].Value.ToString(), row.Cells["INTERME_CODE"].Value.ToString(), row.Cells["INTERME_NAME"].Value.ToString(), filep);
                        dTemp.Rows.Add(row.Cells["POLICY_NO"].Value.ToString(), row.Cells["ADD_INSURED_NAME"].Value.ToString(), row.Cells["PRODUCT"].Value.ToString(), row.Cells["DUE_DATE"].Value.ToString(), row.Cells["INSURED_NAME"].Value.ToString(), row.Cells["POL_PERIOD_FROM"].Value.ToString(), row.Cells["POL_PERIOD_TO"].Value.ToString(), row.Cells["TITLE_CONTRACT"].Value.ToString(), row.Cells["MAINTENANCE_PERIOD"].Value.ToString(), row.Cells["LOC_DES"].Value.ToString(), row.Cells["ACC_HANDLER"].Value.ToString(), row.Cells["ACC_HANDLER_NAME"].Value.ToString(), row.Cells["INTERME_CODE"].Value.ToString(), row.Cells["INTERME_NAME"].Value.ToString(), filep);
                        Reports.EngineeringLettereMAIL rpt1 = new Reports.EngineeringLettereMAIL();
                            
                        
                        //Reports.EngineeringLettereMAIL rpt = new Reports.EngineeringLettereMAIL();
                        rpt1.SetDataSource(dTemp);
                        rpt1.SetParameterValue("DATE_CURR", datetemp.ToUpper());

                        CrystalDecisions.Shared.ExportOptions CrExportOptions;
                        CrystalDecisions.Shared.DiskFileDestinationOptions CrDiskFileDestinationOptions = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                        CrystalDecisions.Shared.PdfRtfWordFormatOptions CrFormatTypeOptions = new CrystalDecisions.Shared.PdfRtfWordFormatOptions();


                        CrDiskFileDestinationOptions.DiskFileName = filep;
                        filep = folderPath;

                        CrExportOptions = rpt1.ExportOptions;
                        {
                            CrExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                            CrExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        }

                        rpt1.Export();
                         
                        dTemp.Rows.Clear();
                        
                    }
                }
            }


            return dt1;
        }


        private void frmRemindLetterEng_Load(object sender, EventArgs e)
        {
            bnClear.PerformClick();
            this.dgvResult.ForeColor = System.Drawing.Color.Black;
            this.dgvFile.ForeColor = System.Drawing.Color.Black;
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
            bnSendEmail.Enabled = false;
        }

        private void bnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                DataTable t1 = GetDataTableFromDGV(dgvResult, directchoosedlg, 2);
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return;
                }
                //if (tbTo.Text.Trim() == "")
                //{
                //    Msgbox.Show("Please input email to!");
                //    return;
                //}

                if (t1.Rows.Count == 0)
                    Msgbox.Show("Please select one of the data to send email!");
                else
                {
                    DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {


                        foreach (DataRow r in t1.Rows)
                        {

                            Cursor.Current = Cursors.WaitCursor;
                            string content = crud.ExecFunc_String("USER_GET_EMAIL_ENGLETTER",
                                new string[] { "MailType","AdditionalInsured","PolicyNo","PolicyType", "PolPeriodFrom",
                        "PolPeriodTo","NoDay",""},
                                new string[] { "Engineering", r["ADD_INSURED_NAME"].ToString(), r["POLICY_NO"].ToString(), r["PRODUCT"].ToString(), r["POL_PERIOD_FROM"].ToString(), r["POL_PERIOD_TO"].ToString(), r["DUE_DATE"].ToString(), "" }).ToString();
                            string body = string.Empty;
                            using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                            {
                                body = reader.ReadToEnd();
                                
                            }
                            
                            //End of Update
                            DataTable d1 = crud.ExecQuery("select SIGNATURE from user_claim_signature where FULL_NAME ='"+ UserFullName.ToUpper()+ "'");
                            if (d1.Rows.Count != 0)
                            {
                            body = body.Replace("{text}", content);
                            body = body.Replace("{department}", d1.Rows[0][0].ToString());
                            body = body.Replace("{username}", UserFullName);
                            body = body.Replace("{user_email}", mail_add);
                            }
                            else
                            {
                                body = body.Replace("{text}", content);
                                //body = body.Replace("{department}", d1.Rows[0][0].ToString());
                                body = body.Replace("{username}", UserFullName);
                                body = body.Replace("{user_email}", mail_add);
                            }
                            //SmtpClient client = new SmtpClient(smtpSer);

                            MailMessage message = new MailMessage();
                            //set formatting email message
                            message.BodyEncoding = Encoding.UTF8;
                            message.IsBodyHtml = true;
                            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            message.From = new MailAddress(mail_add);


                            System.Net.ServicePointManager.Expect100Continue = true;
                            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                            //get Account Handler email, CC
                            string agentcode = r["INTERME_CODE"].ToString(), ahcode = r["ACC_HANDLER"].ToString(), selectedcode;
                            if (agentcode[0] == '0' || ahcode == "U-BNK") //001,002,003 => no agent/broker          //U-BNK => bank 
                                selectedcode = ahcode;
                            else //agent/broker...
                                selectedcode = agentcode;
                            
                            DataTable  dtuser = crud.ExecQuery("SELECT MAIL_TO,MAIL_CC FROM USER_PE_EMAIL_ENGLETTER WHERE CODE = '" + selectedcode + "'");
                            if (dtuser.Rows.Count > 0)
                            {
                                tbTo.Text = dtuser.Rows[0]["MAIL_TO"].ToString();
                                tbCC.Text = dtuser.Rows[0]["MAIL_CC"].ToString();
                            }
                            //

                            if (tbTo.Text.Trim() != "")
                            {
                                string[] to = tbTo.Text.Split(';');
                                foreach (string s in to)
                                {
                                    if (s.Trim() != "")
                                        message.To.Add(s.Trim());
                                }
                            }

                            message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'Engineering'").Rows[0][0].ToString();
                            message.Subject = message.Subject.Replace("%PolicyNo%", r["POLICY_NO"].ToString());
                            message.Subject = message.Subject.Replace("%PolPeriodTo%", r["POL_PERIOD_TO"].ToString());

                            ////default CC auto PE team
                            DataTable dtTemp = new DataTable();
                            dtTemp = crud.ExecQuery("SELECT MAIL_CC FROM USER_PE_EMAIL WHERE TEAM = 'Default'");
                            if (dtTemp.Rows.Count > 0)
                            {
                                string[] cc = dtTemp.Rows[0][0].ToString().Split(';');
                                foreach (string s in cc)
                                {
                                    if (s.Trim() != "")
                                        message.CC.Add(new MailAddress(s.Trim()));
                                }
                            }
                            ////

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

                            //DataTable selectedDoc = GetDGVData(dgvResult);


                            //DataGridView temp = new DataGridView();
                            //temp.DataSource = selectedDoc;
                            //if (t1.Rows.Count <= 0)
                            //    {
                            //        Msgbox.Show("No selected record!");

                            //    }
                            //else
                            //{
                            //    if (dgvFile.Rows.Count > 0)
                            //    {
                            //        foreach (DataGridViewRow dgvr in dgvFile.Rows)
                            //        {
                            //            message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                            //        }
                            //    }
                            //    foreach (DataRow dgvr in t1.Rows)
                            //    {

                            //        message.Attachments.Add(new Attachment(dgvr["PATH"].ToString()));
                            //    }
                            //}
                            //attchment send auto 
                            string filepath = r["PATH"].ToString();
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                message.Attachments.Add(new Attachment(filepath));
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

                            //client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);
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

                            //client.Port = port;
                            //client.Send(message);
                            var Credential = new System.Net.NetworkCredential(mail_add, mail_pass);
                            var result = CommonFunctions.SendEmail(Credential, message);
                            message.Dispose();
                            //client.Dispose();

                            crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                            new string[] { "Insert", r["POLICY_NO"].ToString(), "EngLetter", tbTo.Text, content, r["PATH"].ToString(), "", "", "", "", "", tbCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), Username });
                            label2.Text = DateTime.Now.ToString("dd MMMM yyyy");
                            Cursor.Current = Cursors.WaitCursor;
                            
                            Msgbox.Show("Email sent! -" + r["POLICY_NO"].ToString());
                            
                            Cursor.Current = Cursors.AppStarting;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
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

        private void groupBox6_Enter(object sender, EventArgs e)
        {

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

        

    }
}
