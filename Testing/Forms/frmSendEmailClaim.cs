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
using System.Text.RegularExpressions;

namespace Testing.Forms
{
    public partial class frmSendEmailClaim : Form
    {
        public string UserName = "Default";
        CRUD crud = new CRUD();
        int num;
        bool isChecked;
        public static DataTable selectedDoc;
        CheckBox checkboxHeader = new CheckBox();
        CommonFunctions.ListViewColumnSorter lvwColumnSorter = new CommonFunctions.ListViewColumnSorter();
        //email information
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        string HashPass = "Forte@2017";
        public static DataTable dtClaimDt;
        public static string OtherExclusion;
        public static string PolNo;
        private DataTable dtExcDef;
        int[] row_id;
        DataTable dtTemp1 = null;


        public frmSendEmailClaim()
        {
            InitializeComponent();
        }

        private void frmSendEmailClaim_Load(object sender, EventArgs e)
        {
            refreshGrid();

            //populate the pending grid view
            PendingGrid();


            //Rejection Letter
            //DataTable dt = crud.ExecQuery("select * from USER_CLAIM_EMAIL_EXCLU_DEF order by TYPE");

            //lvDefExclu.CheckBoxes = true;
            //lvDefExclu.View = View.Details;

            //lvDefExclu.Columns.Add("Type", 110);
            //lvDefExclu.Columns.Add("Eng", 275);
            //lvDefExclu.Columns.Add("Kh", 275);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    ListViewItem lvi = new ListViewItem(dr["TYPE"].ToString());
            //    lvi.SubItems.Add(dr["ENG"].ToString());
            //    lvi.SubItems.Add(dr["KH"].ToString());

            //    lvDefExclu.Items.Add(lvi);
            //}


            //Settlement Letter
            //DataTable dt = crud.ExecQuery("SELECT * FROM USER_CLAIM_SIGNATURE order by FULL_NAME");
            DataTable dt = crud.ExecQuery("SELECT * FROM USER_CLAIM_SIGNATURE");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cbSignature.Items.Add(new ComboboxMultiVal(dr["FULL_NAME"].ToString()
                        , new Dictionary<string, string>() { 
                            {"Email",dr["EMAIL"].ToString()},{"Signature",dr["SIGNATURE"].ToString()}
                        }));
                }
            }
            cbSignature.SelectedIndex = 0;
            //

            dgvNonPayClaimNo.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvNonPayClaimNo.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            CommonFunctions.HighLightGrid(dgvDefinition);

            var dsOtherExclusions = crud.ExecQuery("select distinct PRODUCTS from USER_REJECTLETTER_TEMP where PRODUCTS not in ('HNS', 'HNS EMAIL NOTICE') order by PRODUCTS");
            cboOtherExclusions.ValueMember = "PRODUCTS";
            cboOtherExclusions.DisplayMember = "PRODUCTS";
            cboOtherExclusions.DataSource = dsOtherExclusions;

            // add discount to Settlement to Hospital
            var dtDiscount = new DataTable();
            dtDiscount.Columns.Add("Name", typeof(string));
            dtDiscount.Columns.Add("Value", typeof(string));

            for (int i = 0; i <= 20; i++)
            {
                var drDiscount = dtDiscount.NewRow();
                drDiscount["Name"] = i.ToString();
                drDiscount["Value"] = i.ToString();
                dtDiscount.Rows.Add(drDiscount);
            }

            cboDiscount.ValueMember = "Value";
            cboDiscount.DisplayMember = "Name";
            cboDiscount.DataSource = dtDiscount;

            // add Province to Settlement to Hospital
            cboAddress.ValueMember = "Value";
            cboAddress.DisplayMember = "Name";
            cboAddress.DataSource = CommonFunctions.Provinces();
        }
        //private void Combobox_Load()
        //{
        //    DataTable dt = crud.ExecQuery("select * from USER_CLAIM_EMAIL_EXCLU_DEF order by TYPE");
        //    lvDefExclu.Columns.Add("Type", 110);
        //    lvDefExclu.Columns.Add("Eng", 275);
        //    lvDefExclu.Columns.Add("Kh", 275);
        //}
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

        public void refreshWhole()
        {
            refreshGrid();
            PendingGrid();
            if (lbClaimNo.Text.Length >= 20)
                ClaimSearch(lbClaimNo.Text);
            else
                bnClear_Click(new Object(), new EventArgs());
        }

        private void refreshGrid()
        {
            //load all history information

            //oudom
            //tbHisSearch.Text = "";
            //dgvAllHis.DefaultCellStyle.ForeColor = Color.Black;
            //dgvAllHis.DataSource = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "ViewHis", "" });
            //dgvAllHis.Columns[0].Visible = false;
        }

        private void PendingGrid()
        {
            //clear old data
            dgvPending.Columns.Clear();

            //bind data source
            dgvPending.DefaultCellStyle.ForeColor = Color.Black;
            //oudom
            dgvPending.DataSource = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "Pending", "" });
            dgvPending.Columns["REM_NO"].Visible = false;

            //add button for Send in grid
            DataGridViewButtonColumn btnSend = new DataGridViewButtonColumn();
            btnSend.HeaderText = "Send Email";
            btnSend.Text = "Send";
            btnSend.Name = "bnPeSend";
            btnSend.UseColumnTextForButtonValue = true;
            dgvPending.Columns.Add(btnSend);

            //add button for Document Received in grid
            DataGridViewButtonColumn btnRec = new DataGridViewButtonColumn();
            btnRec.HeaderText = "Document Received";
            btnRec.Text = "All";
            btnRec.Name = "bnPeDone";
            btnRec.UseColumnTextForButtonValue = true;
            dgvPending.Columns.Add(btnRec);
        }

        private void OpenDetForm(bool IsResend, string type, string remSeq = "")
        {
            frmSendEmailClaimDet cl_det = new frmSendEmailClaimDet();
            cl_det.sp_type = type;
            cl_det.remind = remSeq;
            cl_det.resend = IsResend;
            cl_det.lbClaimNo.Text = lbClaimNo.Text;
            cl_det.sec = this;
            cl_det.FormClosed += new FormClosedEventHandler(cl_det_FormClosed); //Update 16-Jul-19 (Edit Email Content)
            cl_det.ShowDialog();

        }

        void cl_det_FormClosed(object sender, FormClosedEventArgs e) //Update 16-Jul-19 (Edit Email Content)
        {
            frmSendEmailClaimDet.finalizecontent = "";
        }

        private void ClaimSearch(string ClaimNo)
        {
            DataTable dt = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { ClaimNo, "ValClaimNo", "" });

            if (ClaimNo.Length < 20)
            {
                Msgbox.Show("Incorrect Claim No");
                return;
            }

            if (dt.Rows.Count <= 0)
            {
                Msgbox.Show("Invalid Claim No");
                return;
            }

            pnQuery.Visible = true;
            lbClaimNo.Text = dt.Rows[0].ItemArray[0].ToString();

            //disable and enable buttons
            disabledButt(bnAckResend);
            disabledButt(bnRejResend);
            disabledButt(bnParResend);
            disabledButt(bnPayResend);
            disabledButt(bnRem1Resend);
            disabledButt(bnRem2Resend);
            disabledButt(bnRem2Send);
            disabledButt(bnRem3Resend);
            disabledButt(bnRem3Send);
            disabledButt(btnClaimClosing);
            disabledButt(btnClaimClosingResend);
            disabledButt(bnDocReqResend); //Update 17-Jul-19 (Document Request)
            enabledButt(bnDocReqSend); //Update 17-Jul-19 (Document Request)
            enabledButt(bnAckSend);
            enabledButt(bnRejSend);
            enabledButt(bnParSend);
            enabledButt(bnPaySend);
            enabledButt(bnRem1Send);
            enabledButt(bnDocRec);

            //validating buttons for enabled and not
            //oudom
            DataTable dtInfo = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "ViewHis", lbClaimNo.Text });
            dgvHisClaim.DataSource = dtInfo;
            dgvHisClaim.Columns[0].Visible = false;

            //validate the step of reminder emails
            //oudom
            DataTable dtStep = crud.ExecSP_OutPara("sp_user_claim_info_new", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "CurRem", "" });
            for (int i = 0; i < dtStep.Rows.Count; i++)
            {
                if (dtStep.Rows[0].ItemArray[1].ToString() == "1")
                {
                    disabledButt(bnRem1Send);
                    disabledButt(bnRem3Send);
                    enabledButt(bnRem2Send);
                }
                else if (dtStep.Rows[0].ItemArray[1].ToString() == "2")
                {
                    disabledButt(bnRem1Send);
                    disabledButt(bnRem2Send);
                    enabledButt(bnRem3Send);
                }
                else if (dtStep.Rows[0].ItemArray[1].ToString() == "3")
                {
                    disabledButt(bnRem1Send);
                    disabledButt(bnRem2Send);
                    disabledButt(bnRem3Send);
                    enabledButt(btnClaimClosing);
                }
            }

            //Update 17-Jul-19 (Document Request)
            //Oudom
            DataTable dtTemp = crud.ExecQuery("SELECT HIST_SEQ FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO ='" + lbClaimNo.Text + "' AND SEND_TYPE = 'DocReqNew'");
            if (dtTemp.Rows.Count == 0)
            {
                enabledButt(bnDocReqSend);
                disabledButt(bnDocReqResend);
                disabledButt(bnRem1Send);
            }
            else
            {
                enabledButt(bnDocReqResend);
                disabledButt(bnDocReqSend);
                enabledButt(bnRem1Send);
            }
            //End of Update



            //validate the doc received button
            DataTable dtDoc = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "DocRec", "" });
            if (dtDoc.Rows.Count == 0) //Update 17-Jul-19 (Document Request)
            {
                //if (dtDoc.Rows[0].ItemArray[0].ToString() == "Y")
                //    disabledButt(bnDocRec);
                //Update 18-Jul-19
                dtDoc = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "DocRecDocReq", "" });
                if (dtDoc.Rows.Count == 0)
                    disabledButt(bnDocRec);
                else enabledButt(bnDocRec);
            }
            else enabledButt(bnDocRec);
            //End of Update

            //disable buttons if emails are sent once before
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                //oudom
                if (dtInfo.Rows[i].ItemArray[0].ToString() == "AckNew")
                {
                    disabledButt(bnAckSend);
                    enabledButt(bnAckResend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "RejNew")
                {
                    disabledButt(bnRejSend);
                    enabledButt(bnRejResend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "ParNew")
                {
                    disabledButt(bnParSend);
                    enabledButt(bnParResend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "PayNew")
                {
                    disabledButt(bnPaySend);
                    enabledButt(bnPayResend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "DocReqNew") //Update 17-Jul-19 (Document Request)
                {
                    disabledButt(bnDocReqSend);
                    enabledButt(bnDocReqResend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "RemNewFirst")
                {
                    disabledButt(bnRem1Send);
                    enabledButt(bnRem1Resend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "RemNewSecond")
                {
                    disabledButt(bnRem2Send);
                    enabledButt(bnRem2Resend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString() == "RemNewThird")
                {
                    disabledButt(bnRem3Send);
                    enabledButt(bnRem3Resend);
                }
                else if (dtInfo.Rows[i].ItemArray[0].ToString().Contains("ClaimClosing"))
                {
                    disabledButt(btnClaimClosing);
                    enabledButt(btnClaimClosingResend);
                }
            }


            //Update 04-07-19 Claim Basic Info

            string sql = "SELECT * FROM VIEW_CLAIM_BASIC_INFO WHERE INT_CLAIM_NO = '" + tbClaimNo.Text.ToUpper() + "'";
            DataTable dttemp = crud.ExecQuery(sql);
            tbInsName.Text = dttemp.Rows[0][1].ToString();
            tbRiskName.Text = dttemp.Rows[0][2].ToString();
            tbDOL.Text = dttemp.Rows[0][3].ToString();
            tbPolNo.Text = dttemp.Rows[0][4].ToString();
            PolNo = tbPolNo.Text;

        }

        private void bnClaimSearch_Click(object sender, EventArgs e)
        {
            ClaimSearch(tbClaimNo.Text.Trim().ToUpper());
        }

        private void bnAckSend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "AckNew");
        }

        private void bnRejSend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "RejNew");
        }

        private void bnParSend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "ParNew");
        }

        private void bnPaySend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "PayNew");
        }

        private void bnRem1Send_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "RemNew", "First");
        }

        private void bnRem2Send_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "RemNew", "Second");
        }

        private void bnRem3Send_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "RemNew", "Third");
        }

        private void btnClaimClosing_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(false, "ClaimClosing");
        }

        private void bnAckResend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "AckNew");
        }

        private void bnRejResend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "RejNew");
        }

        private void bnParResend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "ParNew");
        }

        private void bnPayResend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "PayNew");
        }

        private void bnRem1Resend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "RemNew", "First");
        }

        private void bnRem2Resend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "RemNew", "Second");
        }

        private void bnRem3Resend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "RemNew", "Third");
        }

        private void btnClaimClosingResend_Click(object sender, EventArgs e)
        {
            //oudom
            OpenDetForm(true, "ClaimClosing");
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbClaimNo.Text = "";
            pnQuery.Visible = false;
        }

        private void bnHisSearch_Click(object sender, EventArgs e)
        {
            //oudom
            //dgvAllHis.DataSource = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "ViewHis", tbHisSearch.Text });
            //dgvAllHis.Columns[0].Visible = false;
        }

        private void bnRefresh_Click(object sender, EventArgs e)
        {
            PendingGrid();
        }

        private void bnDocRec_Click(object sender, EventArgs e)
        {
            if (crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "DocRec", "" }).Rows.Count <= 0)
            {
                if (crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "DocRecDocReq", "" }).Rows.Count <= 0) //Update 18-Jul-19
                {
                    Msgbox.Show("There aren't any document request emails or reminder emails yet!");
                    return;
                }
            }

            frmSendEmailClaimDet cl_det = new frmSendEmailClaimDet();
            cl_det.sp_type = "Rem";
            //cl_det.remind = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "CurRemName", "" }).Rows[0].ItemArray[1].ToString();
            //Update 17-Jul-19 (Document Request)
            DataTable dtTemp = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { lbClaimNo.Text, "CurRemName", "" });
            if (dtTemp.Rows.Count == 0) cl_det.remind = "";
            else cl_det.remind = dtTemp.Rows[0].ItemArray[1].ToString();
            //End of Update
            cl_det.rec_form = true;
            cl_det.lbClaimNo.Text = lbClaimNo.Text;
            cl_det.sec = this;
            cl_det.ShowDialog();
        }

        private void dgvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = dgvPending.SelectedCells[0].RowIndex;
            string remSeq = dgvPending.Rows[RowIndex].Cells["REM_NO"].Value.ToString();
            string claimNo = dgvPending.Rows[RowIndex].Cells["CLAIM_NO"].Value.ToString();

            if (e.ColumnIndex == dgvPending.Columns["bnPeSend"].Index && e.RowIndex >= 0)
            {
                frmSendEmailClaimDet cl_det = new frmSendEmailClaimDet();
                cl_det.sp_type = "RemNew"; //oudom
                cl_det.remind = remSeq == "First" ? "Second" : (remSeq == "Second" ? "Third" : "");
                cl_det.resend = false;
                cl_det.lbClaimNo.Text = claimNo;
                cl_det.sec = this;
                cl_det.ShowDialog();
            }
            else if (e.ColumnIndex == dgvPending.Columns["bnPeDone"].Index && e.RowIndex >= 0)
            {
                RecDoc(claimNo, remSeq);
            }
        }

        public bool RecDoc(string claimNum, string remNo)
        {
            DialogResult dr = Msgbox.Show("Have you received the all document(s) from the client yet?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.No)
                return false;

            crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                    new string[] { "UpdateDocRec", claimNum, remNo, "", "", "", "", "", "", "", "", "", "", "" });

            refreshWhole();
            return true;
        }

        private void bnDocReqSend_Click(object sender, EventArgs e) //Update 17-Jul-19 (Document Request)
        {
            // oudom
            OpenDetForm(false, "DocReqNew");
        }

        private void bnDocReqResend_Click(object sender, EventArgs e) //Update 17-Jul-19 (Document Request)
        {
            // oudom
            OpenDetForm(true, "DocReqNew");
        }

        private void tbClaimNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                bnClaimSearch.PerformClick();
            }
        }

        private void frmSendEmailClaim_Activated(object sender, EventArgs e)
        {
            tbClaimNo.Focus();
            requeryEmailSuggestion();
        }

        #region Rejection Letter
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string ClNo = tbRejectClNo.Text.Trim().ToUpper();
            if (ClNo != "")
            {

                if (ClNo.Length < 20)
                {
                    Msgbox.Show("Incorrect Claim No");
                    return;
                }

                string pro = ClNo.Substring(7, 3);
                if (pro != "GPA" && pro != "PAC" && pro != "HNS")
                {
                    Msgbox.Show("Only GPA ,PAC and HNS product available in this function.");
                    return;
                }
                else if (pro == "GPA" || pro == "PAC")
                {
                    Cursor.Current = Cursors.WaitCursor;

                    DataTable result = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                    "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                    "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                    "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A') CAUSE, " +
                    "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, nvl(nullif(instr(INT_COMMENTS, '('),0),instr(INT_COMMENTS, 'D:')) - instr(INT_COMMENTS, 'H:') - 2)), 'N/A') HOSPITAL, " +
                    "nvl(REGEXP_SUBSTR(nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, instr(INT_COMMENTS, 'D:') - instr(INT_COMMENTS, 'H:') - 2)), 'N/A'), '\\(([^)]*)\\)', 1, 1, NULL, 1),'N/A') TREATMENT_DATE, " +
                    "INT_BPARTY_CODE CC from CL_T_INTIMATION where INT_CLAIM_NO = '" + ClNo + "'");

                    if (result.Rows.Count <= 0)
                    {
                        Msgbox.Show("Not record found!");
                        return;
                    }

                    string SecondPara = "By virtue of Personal Accident Policy, the policy will cover medical expenses for bodily injury to the insured Person caused solely and directly by ",
                        SecondParaKh = "ដោយផ្អែកលើបណ្ណសន្យាគ្រោះថ្នាក់បុគ្គល ";

                    if (lvDefExclu.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvDefExclu.CheckedItems)
                        {
                            if (lvDefExclu.CheckedItems.Count == 1)
                            {
                                SecondPara += lvi.SubItems[0].Text + " " + lvi.SubItems[1].Text;
                                SecondParaKh += KhTranslate(lvi.SubItems[0].Text) + " " + lvi.SubItems[2].Text;
                                break;
                            }
                            else
                            {
                                SecondPara += Environment.NewLine + Environment.NewLine + "•  " + lvi.SubItems[0].Text + " " + lvi.SubItems[1].Text;
                                SecondParaKh += Environment.NewLine + "•  " + KhTranslate(lvi.SubItems[0].Text) + " " + lvi.SubItems[2].Text;
                            }
                        }
                    }


                    result.Columns.Add("SECOND_PARA", typeof(System.String));
                    foreach (DataRow row in result.Rows)
                    {
                        row["SECOND_PARA"] = SecondPara;
                    }

                    DataTable resultKh = result.Copy();
                    string clamt = "";
                    foreach (DataRow row in resultKh.Rows)
                    {
                        row["SECOND_PARA"] = SecondParaKh;
                        clamt = row["CLAIMED_AMOUNT"].ToString();
                    }

                    Reports.RejectionLetter report = new Reports.RejectionLetter();
                    report.SetDataSource(result);
                    crystalReportViewer1.ReportSource = report;

                    Reports.RejectionLetterKH report1 = new Reports.RejectionLetterKH();
                    report1.SetDataSource(resultKh);
                    report1.SetParameterValue("KhDate", CommonFunctions.KhDate(DateTime.Now));
                    report1.SetParameterValue("ClAmtKh", CommonFunctions.KhNum(Convert.ToDouble(clamt)));
                    crystalReportViewer2.ReportSource = report1;


                }
                else if (pro == "HNS")
                {
                    Cursor.Current = Cursors.WaitCursor;

                    DataTable result = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                    "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                    "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                    "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A') CAUSE, " +
                    "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, nvl(nullif(instr(INT_COMMENTS, '('),0),instr(INT_COMMENTS, 'D:')) - instr(INT_COMMENTS, 'H:') - 2)), 'N/A') HOSPITAL, " +
                    "nvl(REGEXP_SUBSTR(nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, instr(INT_COMMENTS, 'D:') - instr(INT_COMMENTS, 'H:') - 2)), 'N/A'), '\\(([^)]*)\\)', 1, 1, NULL, 1),'N/A') TREATMENT_DATE, " +
                    "INT_BPARTY_CODE CC from CL_T_INTIMATION where INT_CLAIM_NO = '" + ClNo + "'");

                    if (result.Rows.Count <= 0)
                    {
                        Msgbox.Show("Not record found!");
                        return;
                    }

                    string SecondPara = "By virtue of the Group Hospital and Surgical Insurance Policy at ",
                        SecondParaKh = "ដោយផ្អែកលើបណ្ណសន្យារ៉ាប់រងសម្រាកពេទ្យ និងវះកាត់ត្រង់ ";

                    if (lvDefExclu.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvDefExclu.CheckedItems)
                        {
                            if (lvDefExclu.CheckedItems.Count == 1)
                            {
                                //SecondPara += lvi.SubItems[0].Text + " " + lvi.SubItems[1].Text;
                                //SecondParaKh += KhTranslate(lvi.SubItems[0].Text) + " " + lvi.SubItems[2].Text;
                                SecondPara += " " + lvi.SubItems[1].Text;
                                SecondParaKh += " " + lvi.SubItems[2].Text;
                                break;
                            }
                            else
                            {
                                SecondPara += Environment.NewLine + Environment.NewLine + "•  " + lvi.SubItems[1].Text;
                                SecondParaKh += Environment.NewLine + Environment.NewLine + "•  " + lvi.SubItems[2].Text;
                            }
                        }
                    }

                    Reports.RejectionLetterHNS report = new Reports.RejectionLetterHNS();
                    result.Columns.Add("SECOND_PARA", typeof(System.String));
                    result.Columns.Add("SECOND_PARA_KH", typeof(System.String));
                    string clamt = "";
                    foreach (DataRow row in result.Rows)
                    {
                        row["SECOND_PARA"] = SecondPara;
                        row["SECOND_PARA_KH"] = SecondParaKh;
                        clamt = row["CLAIMED_AMOUNT"].ToString();
                    }


                    //DataTable resultKh = result.Copy();
                    //string clamt = "";
                    //foreach (DataRow row in resultKh.Rows)
                    //{

                    //    clamt = row["CLAIMED_AMOUNT"].ToString();
                    //}


                    report.SetDataSource(result);
                    report.SetParameterValue("KhDate", CommonFunctions.KhDate(DateTime.Now));
                    report.SetParameterValue("ClAmtKh", CommonFunctions.KhNum(Convert.ToDouble(clamt)));

                    crystalReportViewer2.ReportSource = report;
                    crystalReportViewer2.Location = new System.Drawing.Point(250, 250);
                    crystalReportViewer1.Visible = false;
                    //Reports.RejectionLetterKHHNS report1 = new Reports.RejectionLetterKHHNS();
                    //report1.SetDataSource(resultKh);
                    //report1.SetParameterValue("KhDate", CommonFunctions.KhDate(DateTime.Now));
                    //report1.SetParameterValue("ClAmtKh", CommonFunctions.KhNum(Convert.ToDouble(clamt)));
                    //crystalReportViewer2.ReportSource = report1;

                }
                Cursor.Current = Cursors.AppStarting;
            }
        }

        private void tbRejectClNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGenerate.PerformClick();
            }
        }

        string KhTranslate(string eng)
        {
            if (eng == "Definitions")
                return "និយមន័យ";
            else if (eng == "Exclusions")
                return "ករណីមិនធានា";
            else return "";
        }
        #endregion

        #region Non Payable Letter
        private void btnGenerateNonPay_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (dgvNonPayClaimNo.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim No not found.");
                    return;
                }

                int CurrentRow = dgvNonPayClaimNo.CurrentCell.RowIndex;

                if (CurrentRow == -1)
                {
                    Msgbox.Show("Please select Claim No.");
                    return;
                }

                string clno = dgvNonPayClaimNo.Rows[CurrentRow].Cells["CLAIM_NO"].Value.ToString();

                string pro = clno.Substring(7, 3);
                if (pro != "GPA" && pro != "PAC" && pro != "PAE")
                {
                    Msgbox.Show("Only GPA and PAC product available in this function.");
                    return;
                }


                //          string cmd = " select POLICY_HOLDER,ADDRESS,CLAIM_NO,INSURED_MEMBER,ACCIDENT_DATE,TOTAL_COST,CC from " +
                //"(select INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME INSURED_MEMBER,pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER,to_char(INT_DATE_LOSS,'dd/mm/yyyy') ACCIDENT_DATE, " +
                //"nvl((select SUM(PRD_VALUE) from CL_T_PROVISION_DTLS where PRD_INT_SEQ = INT_SEQ_NO and PRD_COMMENTS is null),0) TOTAL_COST, " +
                //"nvl((select SUM(MRD_VALUE) from CL_T_PROV_PAYMENT_DTLS where MRD_INT_SEQ = INT_SEQ_NO),0) PAYABLE, INT_BPARTY_CODE CC, " +
                //"(select ADR_LOC_DESCRIPTION || ', ' || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE  " +
                //"from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = INT_CUS_CODE) ADDRESS " +
                //"from CL_T_INTIMATION  " +
                //"where INT_CLAIM_NO = '" + clno + "')";


                //          string cmd = " select POLICY_HOLDER,ADDRESS,CLAIM_NO,INSURED_MEMBER,ACCIDENT_DATE,TOTAL_COST,CC from " +
                //"(select INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME INSURED_MEMBER,pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER,to_char(INT_DATE_LOSS,'dd/mm/yyyy') ACCIDENT_DATE, " +
                //"nvl((select SUM(PRD_VALUE) from CL_T_PROVISION_DTLS where PRD_INT_SEQ = INT_SEQ_NO and PRD_COMMENTS is null and " +
                //"CREATED_DATE = (select MAX(CREATED_DATE) from CL_T_PROVISION_DTLS where PRD_INT_SEQ = INT_SEQ_NO and PRD_COMMENTS is null)),0) TOTAL_COST, " +
                //"nvl((select SUM(MRD_VALUE) from CL_T_PROV_PAYMENT_DTLS where MRD_INT_SEQ = INT_SEQ_NO),0) PAYABLE, INT_BPARTY_CODE CC, " +
                //"(select ADR_LOC_DESCRIPTION || ', ' || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE  " +
                //"from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = INT_CUS_CODE) ADDRESS " +
                //"from CL_T_INTIMATION  " +
                //"where INT_CLAIM_NO = '" + clno + "')";

                //        string cmd = "select POLICY_HOLDER,ADDRESS,CLAIM_NO,INSURED_MEMBER,ACCIDENT_DATE, " +
                //"(case when PAYABLE = 0 then PAID else PAYABLE end) as PAYABLE,CC from " +
                //"(select INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME INSURED_MEMBER,pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER,to_char(INT_DATE_LOSS,'dd/mm/yyyy') ACCIDENT_DATE, " +
                //"nvl((select SUM(PRD_VALUE) from CL_T_PROVISION_DTLS where PRD_INT_SEQ = INT_SEQ_NO and PRD_COMMENTS is null),0) TOTAL_COST, " +
                //"nvl((select sum(MRD_VALUE) from CL_T_PROV_PAYMENT_DTLS where MRD_CLAIM_NO = '" + clno + "' and MRD_FUNCTION_ID = '3.1' and MRD_VALUE <> 0 ),0) " +
                //"- nvl((select sum(RRD_VALUE) from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0),0) PAYABLE, " +
                //"nvl((select sum(RRD_VALUE) from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0),0) PAID, INT_BPARTY_CODE CC, " +
                //"(select ADR_LOC_DESCRIPTION || ', ' || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ', '  || " +
                //"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE " + 
                //"from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = INT_CUS_CODE) ADDRESS " +
                //"from CL_T_INTIMATION  " +
                //"where INT_CLAIM_NO = '" + clno + "')";

                string cmd = "select POLICY_HOLDER,ADDRESS,CLAIM_NO,INSURED_MEMBER,ACCIDENT_DATE, " +
        //"PAYABLE,CC from " +
        "TOTAL_COST,CC from " +
        "(select INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME INSURED_MEMBER,pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER,to_char(INT_DATE_LOSS,'dd/mm/yyyy') ACCIDENT_DATE, " +
        "nvl((select SUM(PRD_VALUE) from CL_T_PROVISION_DTLS where PRD_INT_SEQ = INT_SEQ_NO and PRD_COMMENTS is null),0) TOTAL_COST, " +
        "nvl((select * from(select MRD_VALUE from CL_T_PROV_PAYMENT_DTLS where MRD_CLAIM_NO = '" + clno + "' and MRD_FUNCTION_ID = '3.1' and MRD_VALUE <> 0 and rownum = 1 order by CREATED_DATE desc)), 0) PAYABLE, " +
        "INT_BPARTY_CODE CC, " +
        "(select ADR_LOC_DESCRIPTION || ', ' || " +
        "(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ', '  || " +
        "(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ', '  || " +
        "(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE " +
        "from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = INT_CUS_CODE and rownum = 1) ADDRESS " +
        "from CL_T_INTIMATION  " +
        "where INT_CLAIM_NO = '" + clno + "')";

                DataTable result = crud.ExecQuery(cmd);


                if (result.Rows.Count <= 0)
                {
                    Msgbox.Show("Not record found!");
                    return;
                }

                string PaymentMethod = "",
                    NonPayItem = tbNonPayItem.Text.Trim();

                if (rbNonPayChequeNo.Checked)
                    PaymentMethod = "Cheque No." + tbNonPayChequeNo.Text.Trim();
                else if (rbNonPayBankTranNo.Checked)
                    PaymentMethod = "Bank Transfer No." + tbNonPayBankTranNo.Text.Trim();
                else if (rbNonPayRequisition.Checked)
                    PaymentMethod = "Cash";

                double NonPay = 0;

                string HtmlPara = "However, we regret to inform you that the amount of ";

                if (NonPayItem != String.Empty)
                {
                    Dictionary<string, string> NonPayItemList = new Dictionary<string, string>(); //store Non-payable Item
                    string[] temp = NonPayItem.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None); //split string on newline
                    foreach (string s in temp)
                    {
                        if (s.Trim() == string.Empty)
                            continue;
                        if (!s.Contains("="))
                        {
                            Msgbox.Show("Non-payable Item wrong format.");
                            return;
                        }
                        string[] sTemp = s.Split('=');
                        NonPayItemList.Add(sTemp[0].Trim(), sTemp[1].Trim()); //Non-payable Item, Non-payable Amount
                    }

                    if (NonPayItemList.Count > 1)
                    {
                        foreach (KeyValuePair<string, string> entry in NonPayItemList)
                        {
                            HtmlPara += "<br>      •  <b>US$ " + entry.Value + "</b> for the <b>" + entry.Key + "</b>";
                        }
                        HtmlPara += "<br>";
                    }
                    else if (NonPayItemList.Count == 1)
                    {
                        HtmlPara += "<b>US$ " + NonPayItemList.ElementAt(0).Value + "</b> for the <b>" + NonPayItemList.ElementAt(0).Key + "</b> ";
                    }

                    NonPay = NonPayItemList.Sum(x => Convert.ToDouble(x.Value));

                    HtmlPara += NonPayItemList.Count >= 2 ? "are" : "is";
                    HtmlPara += " not covered under the Group Personal Accident insurance policy.";
                }

                result.Columns.Add("PAYABLE", typeof(System.String));
                //result.Columns.Add("TOTAL_COST", typeof(System.String));
                result.Columns.Add("NON_PAYABLE", typeof(System.String));
                result.Columns.Add("CASH_OR_CHEQUE", typeof(System.String));
                result.Columns.Add("HTML_PARA", typeof(System.String));
                result.Columns.Add("USER_NAME", typeof(System.String));

                foreach (DataRow row in result.Rows)
                {
                    row["PAYABLE"] = String.Format("{0:N}", Convert.ToDecimal(Convert.ToDouble(row["TOTAL_COST"]) - NonPay));
                    //row["TOTAL_COST"] = String.Format("{0:N}", Convert.ToDecimal(Convert.ToDouble(row["PAYABLE"]) + NonPay));
                    row["NON_PAYABLE"] = String.Format("{0:N}", NonPay);
                    row["CASH_OR_CHEQUE"] = PaymentMethod;
                    row["HTML_PARA"] = HtmlPara;
                    row["USER_NAME"] = UserName;
                }
                Reports.NonPayLetter report = new Reports.NonPayLetter();
                report.SetDataSource(result);
                crystalReportViewer3.ReportSource = report;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnGetClNo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable dtTemp = new DataTable();
                string RefNo = "";

                dgvNonPayClaimNo.DataSource = null;

                if (rbNonPayChequeNo.Checked)
                {
                    string ChequeNo = tbNonPayChequeNo.Text.ToUpper().Trim();
                    if (ChequeNo == "")
                    {
                        Msgbox.Show("Please input Cheque No.");
                        tbNonPayChequeNo.Focus();
                        return;
                    }
                    dtTemp = crud.ExecQuery("SELECT CLAIM_NO FROM VIEW_REQUISITION WHERE CHQ_NO = '" + ChequeNo + "'");

                    if (dtTemp.Rows.Count <= 0)
                    {
                        Msgbox.Show("No Claim No Found with Cheque No.: " + ChequeNo);
                        return;
                    }
                    RefNo = ChequeNo;
                }
                else if (rbNonPayBankTranNo.Checked)
                {
                    string BankTranNo = tbNonPayBankTranNo.Text.ToUpper().Trim();
                    if (BankTranNo == "")
                    {
                        Msgbox.Show("Please input Bank Transfer No.");
                        tbNonPayBankTranNo.Focus();
                        return;
                    }
                    dtTemp = crud.ExecQuery("SELECT CLAIM_NO FROM VIEW_REQUISITION WHERE BANK_TRAN_NO = '" + BankTranNo + "'");

                    if (dtTemp.Rows.Count <= 0)
                    {
                        Msgbox.Show("No Claim No Found with Bank Transfer No.: " + BankTranNo);
                        return;
                    }
                    RefNo = BankTranNo;
                }
                else if (rbNonPayRequisition.Checked)
                {
                    List<string> SelectedItem = new List<string>();
                    string SelectedRequi = "";

                    if (lvNonPayRequisition.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvNonPayRequisition.CheckedItems)
                        {
                            SelectedItem.Add("'" + lvi.SubItems[0].Text + "'");
                            //SelectedRequi += "'" + lvi.SubItems[0].Text + "',";
                        }

                        SelectedItem.Sort();
                        SelectedRequi = SelectedItem.Aggregate((i, j) => i + "," + j); //Concat list item with ","
                        //SelectedRequi = SelectedRequi.Remove(SelectedRequi.Length - 1);

                        dtTemp = crud.ExecQuery("SELECT CLAIM_NO FROM VIEW_REQUISITION WHERE REQUISITION_NO IN (" + SelectedRequi + ")");

                        if (dtTemp.Rows.Count <= 0)
                        {
                            Msgbox.Show("No Claim Detail Found.");
                            return;
                        }
                        RefNo = SelectedRequi;
                    }
                    else
                    {
                        Msgbox.Show("No record selected in Requisition Table.");
                        return;
                    }
                }

                dgvNonPayClaimNo.DataSource = dtTemp;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        private void btnNonPayRefreshTable_Click(object sender, EventArgs e)
        {
            RefreshRequisitionTable(dtpNonPayFrom.Value, dtpNonPayTo.Value, lvNonPayRequisition);
        }

        private void dgvNonPayClaimNo_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvNonPayClaimNo.Rows.Count > 0)
                frmDocumentControl.enabledButt(btnGenerateNonPay);
            else
                frmDocumentControl.disabledButt(btnGenerateNonPay);
        }

        private void rbNonPayChequeNo_CheckedChanged(object sender, EventArgs e)
        {
            NonPaySelectChange();
        }

        private void rbNonPayBankTranNo_CheckedChanged(object sender, EventArgs e)
        {
            NonPaySelectChange();
        }

        private void rbNonPayRequisition_CheckedChanged(object sender, EventArgs e)
        {
            NonPaySelectChange();
        }

        void NonPaySelectChange()
        {
            tbNonPayChequeNo.Enabled = false;
            tbNonPayBankTranNo.Enabled = false;
            dtpNonPayFrom.Enabled = false;
            dtpNonPayTo.Enabled = false;
            frmDocumentControl.disabledButt(btnNonPayRefreshTable);
            lvNonPayRequisition.Enabled = false;
            dgvNonPayClaimNo.DataSource = null;

            if (rbNonPayChequeNo.Checked)
            {
                tbNonPayChequeNo.Enabled = true;
                tbNonPayBankTranNo.Text = "";
            }
            else if (rbNonPayBankTranNo.Checked)
            {
                tbNonPayBankTranNo.Enabled = true;
                tbNonPayChequeNo.Text = "";
            }
            else if (rbNonPayRequisition.Checked)
            {
                dtpNonPayFrom.Enabled = true;
                dtpNonPayTo.Enabled = true;
                frmDocumentControl.enabledButt(btnNonPayRefreshTable);
                lvNonPayRequisition.Enabled = true;
            }
        }

        private void lvNonPayRequisition_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvNonPayRequisition.Sort();
        }



        #endregion

        #region Settlement Report
        /// Settlement Report

        void RefreshRequisitionTable(DateTime dtFrom, DateTime dtTo, ListView lv)
        {
            try
            {

                if (dtTo.Date < dtFrom.Date)
                {
                    Msgbox.Show("Date to must be greater or equal to Date from!");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                DataTable dt = crud.ExecQuery("select REQUISITION_NO,REQ_AMOUNT,CLAIM_NO,INSURED from " +
                    "(select REQ_REQUISITION_NO REQUISITION_NO,REQ_AMOUNT,INT_CLAIM_NO CLAIM_NO,INSURED,CREATED_DATE from " +
                    "( " +
                    "select REQ_REQUISITION_NO,TRIM(TO_CHAR(REQ_AMOUNT,'999,999,999,990.99')) REQ_AMOUNT,INT_CLAIM_NO, " +
                    "(SELECT nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) FROM UW_M_CUSTOMERS WHERE CUS_CODE = INT_CUS_CODE) INSURED, " +
                    "b.CREATED_DATE from CL_T_INTIMATION a, CL_T_REQUISITION b, CL_T_REQ_PAYEES c " +
                    "where a.INT_SEQ_NO = b.REQ_INT_SEQ and b.REQ_SEQ_NO = c.RPQ_REQ_NO " +
                    ")) " +
                    "where CREATED_DATE >= TO_DATE('" + dtFrom.ToString("yyyy/MM/dd") + " 00:00:00" + "','YYYY/MM/DD HH24:MI:SS') " +
                    "and CREATED_DATE <= TO_DATE('" + dtTo.ToString("yyyy/MM/dd") + " 23:59:59" + "','YYYY/MM/DD HH24:MI:SS') " +
                    "and (CLAIM_NO like '%GPA%' or CLAIM_NO like '%PAC%' or CLAIM_NO like '%HNS%') order by REQUISITION_NO");

                //lvRequisition.Columns.Clear();
                lv.Clear();

                lv.CheckBoxes = true;
                lv.View = View.Details;
                lv.ListViewItemSorter = lvwColumnSorter;

                lv.Columns.Add("Requisition No.", 160);
                lv.Columns.Add("Amount", 80);
                lv.Columns.Add("Claim No.", 180);
                lv.Columns.Add("Policyholder.", 245);

                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem(dr["REQUISITION_NO"].ToString());
                    lvi.SubItems.Add(dr["REQ_AMOUNT"].ToString());
                    lvi.SubItems.Add(dr["CLAIM_NO"].ToString());
                    lvi.SubItems.Add(dr["INSURED"].ToString());

                    lv.Items.Add(lvi);
                }
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }


        private void btnGenerateClaimDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                DataTable dtTemp = new DataTable();
                DataTable dtQ_temp = new DataTable();
                string RefNo = "";

                dgvClaimDetail.DataSource = null;

                if (rbChequeNo.Checked)
                {
                    string ChequeNo = tbChequeNo.Text.ToUpper().Trim();
                    if (ChequeNo == "")
                    {
                        Msgbox.Show("Please input Cheque No.");
                        tbChequeNo.Focus();
                        return;
                    }
                    //update on save record for modification Theane 14-07-2022
                    dtQ_temp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION_TEMP WHERE CHQ_NO = '" + ChequeNo + "'");
                    //-----///

                    dtTemp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION WHERE CHQ_NO = '" + ChequeNo + "'");



                    if (dtTemp.Rows.Count <= 0)
                    {
                        Msgbox.Show("No Claim Detail Found with Cheque No.: " + ChequeNo);
                        return;
                    }
                    RefNo = ChequeNo;
                }
                else if (rbBankTranNo.Checked)
                {
                    string BankTranNo = tbBankTranNo.Text.ToUpper().Trim();
                    if (BankTranNo == "")
                    {
                        Msgbox.Show("Please input Bank Transfer No.");
                        tbBankTranNo.Focus();
                        return;
                    }
                    //update on save record for modification Theane 14-07-2022
                    dtQ_temp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION_TEMP WHERE BANK_TRAN_NO = '" + BankTranNo + "'");
                    //-----///
                    dtTemp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION WHERE BANK_TRAN_NO = '" + BankTranNo + "'");

                    if (dtTemp.Rows.Count <= 0)
                    {
                        Msgbox.Show("No Claim Detail Found with Bank Transfer No.: " + BankTranNo);
                        return;
                    }
                    RefNo = BankTranNo;
                }
                else if (rbRequisition.Checked)
                {
                    List<string> SelectedItem = new List<string>();
                    string SelectedRequi = "";

                    if (lvRequisition.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvRequisition.CheckedItems)
                        {
                            SelectedItem.Add("'" + lvi.SubItems[0].Text + "'");
                            //SelectedRequi += "'" + lvi.SubItems[0].Text + "',";
                        }

                        SelectedItem.Sort();
                        SelectedRequi = SelectedItem.Aggregate((i, j) => i + "," + j); //Concat list item with ","
                        //SelectedRequi = SelectedRequi.Remove(SelectedRequi.Length - 1);
                        //update on save record for modification Theane 14-07-2022
                        dtQ_temp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION_TEMP WHERE REQUISITION_NO IN (" + SelectedRequi + ")");
                        //-----///
                        dtTemp = crud.ExecQuery("SELECT * FROM VIEW_REQUISITION WHERE REQUISITION_NO IN (" + SelectedRequi + ")");

                        if (dtTemp.Rows.Count <= 0)
                        {
                            Msgbox.Show("No Claim Detail Found.");
                            return;
                        }
                        RefNo = SelectedRequi;
                    }
                    else
                    {
                        Msgbox.Show("No record selected in Requisition Table.");
                        return;
                    }
                }

                if (rbToInsured.Checked)
                {
                    dtTemp.Columns.Add("PREVIOUS_PAYMENT", typeof(string));
                    dtTemp.Columns.Add("LAST_PAYMENT", typeof(string));

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        dr["PREVIOUS_PAYMENT"] = "0.00";
                        dr["LAST_PAYMENT"] = "0.00";
                        dr["PAYABLE_AMT"] = dr["REQ_AMOUNT"]; //Change due to request on display
                    }


                }
                else if (rbToHospital.Checked)
                {
                    dtTemp.Columns.Add("DISCOUNT", typeof(string));

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        dr["DISCOUNT"] = "0.00";
                        dr["CLAIMED_AMT"] = dr["REQ_AMOUNT"]; //Change due to request on display
                    }
                }
                //compare two datatable in order to update cell - Theane 17-07-2022 request Chea Sovanravy
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {

                    for (int j = 0; j < dtQ_temp.Rows.Count; j++)
                    {

                        if (dtTemp.Rows[i]["CLAIM_NO"].ToString() == dtQ_temp.Rows[j]["CLAIM_NO"].ToString())
                        {

                            dtTemp.Rows[i][0] = dtQ_temp.Rows[j][0].ToString();
                            dtTemp.Rows[i][1] = dtQ_temp.Rows[j][1].ToString();
                            dtTemp.Rows[i][2] = dtQ_temp.Rows[j][2].ToString();
                            dtTemp.Rows[i][3] = dtQ_temp.Rows[j][3].ToString();
                            dtTemp.Rows[i][4] = dtQ_temp.Rows[j][4].ToString();
                            dtTemp.Rows[i][5] = dtQ_temp.Rows[j][5].ToString();
                            dtTemp.Rows[i][6] = dtQ_temp.Rows[j][6].ToString();
                            dtTemp.Rows[i][7] = dtQ_temp.Rows[j][7].ToString();
                            dtTemp.Rows[i][8] = dtQ_temp.Rows[j][8].ToString();
                            dtTemp.Rows[i][9] = dtQ_temp.Rows[j][9].ToString();
                            dtTemp.Rows[i][10] = dtQ_temp.Rows[j][10].ToString();
                            dtTemp.Rows[i][11] = dtQ_temp.Rows[j][11].ToString();
                            dtTemp.Rows[i][12] = dtQ_temp.Rows[j][12].ToString();
                            dtTemp.Rows[i][13] = dtQ_temp.Rows[j][13].ToString();
                            dtTemp.Rows[i][14] = dtQ_temp.Rows[j][14].ToString();
                            dtTemp.Rows[i][15] = dtQ_temp.Rows[j][15].ToString();
                            dtTemp.Rows[i][16] = dtQ_temp.Rows[j][16].ToString();
                            dtTemp.Rows[i][17] = dtQ_temp.Rows[j][17].ToString();

                        }

                    }

                }
                //-----------------------------------//

                dgvClaimDetail.DataSource = dtTemp;
                //dtTemp1 = dtTemp.Copy();


                dgvClaimDetail.Columns["REQUISITION_NO"].Visible = false;
                dgvClaimDetail.Columns["REQ_AMOUNT"].Visible = false;
                dgvClaimDetail.Columns["INSURED"].Visible = false;
                dgvClaimDetail.Columns["CREATED_DATE"].Visible = false;
                dgvClaimDetail.Columns["PV"].Visible = false;
                dgvClaimDetail.Columns["ADDRESS"].Visible = false;
                dgvClaimDetail.Columns["CC"].Visible = false;
                dgvClaimDetail.Columns["BANK_TRAN_NO"].Visible = false;
                dgvClaimDetail.Columns["CHQ_NO"].Visible = false;
                dgvClaimDetail.Columns["HOSPITAL_ADDRESS"].Visible = false;
                dgvClaimDetail.Columns["PANEL_HOSPITAL"].Visible = false;

                frmDocumentControl.enabledButt(btnSave);


                //Get Letter Hist
                if (rbRequisition.Checked) RefNo = RefNo.Replace("'", "");
                dtTemp = crud.ExecQuery("SELECT * FROM USER_LETTER_HIST WHERE LETTER_TYPE = 'SETTLEMENT' AND REF_NO like '%" + RefNo + "%'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbDear.Text = dtTemp.Rows[0]["DEAR"].ToString();
                    cbSignature.SelectedIndex = cbSignature.FindStringExact(dtTemp.Rows[0]["SIGNATURE"].ToString());
                    tbCC.Text = dtTemp.Rows[0]["CC"].ToString();
                    tbText.Text = dtTemp.Rows[0]["NOTE"].ToString();
                }
                //

                gbSettleSendEmail.Visible = true;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnRefreshTable_Click(object sender, EventArgs e)
        {
            RefreshRequisitionTable(dtpFrom.Value, dtpTo.Value, lvRequisition);
        }

        private void btnGenerateLetter_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable Latest = new DataTable();
                foreach (DataGridViewColumn col in dgvClaimDetail.Columns)
                {
                    Latest.Columns.Add(col.Name);
                }

                Latest.Columns.Add("POL_NO", typeof(string));
                var claimNo = string.Empty;
                foreach (DataGridViewRow row in dgvClaimDetail.Rows)
                {
                    DataRow dRow = Latest.NewRow();
                    bool addPolNo = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                        claimNo = dRow["CLAIM_NO"].ToString();
                        if (!string.IsNullOrEmpty(claimNo) && !addPolNo)
                        {
                            var polNo = crud.ExecQuery("select int_policy_no from cl_t_intimation where int_claim_no = '" + claimNo + "'").Rows[0][0].ToString() ?? string.Empty;
                            dRow["POL_NO"] = polNo;
                            addPolNo = true;
                        }
                    }
                    Latest.Rows.Add(dRow);
                }

                if (rbToInsured.Checked)
                {
                    Latest.Columns.Add("NON_PAYABLE", typeof(string));
                    foreach (DataRow dr in Latest.Rows)
                        dr["NON_PAYABLE"] = String.Format("{0:N}",
                            Convert.ToDecimal(Convert.ToDouble(dr["CLAIMED_AMT"]) - Convert.ToDouble(dr["PAYABLE_AMT"])));
                }
                else if (rbToHospital.Checked)
                {
                    Latest.Columns.Add("AMT_AFTER_DIS", typeof(string));
                    foreach (DataRow dr in Latest.Rows)
                    {
                        dr["INSURED"] = dr["INSURED"].ToString().Trim();
                        dr["AMT_AFTER_DIS"] = dr["CLAIMED_AMT"];
                        if (Convert.ToDouble(cboDiscount.Text) > 0)
                        {
                            dr["DISCOUNT"] = cboDiscount.Text;
                            dr["CLAIMED_AMT"] = String.Format("{0:N}", Convert.ToDecimal(dr["CLAIMED_AMT"]) / (1 - (Convert.ToDecimal(cboDiscount.Text) / 100)));
                        }
                        else
                            dr["DISCOUNT"] = 0;

                        dr["HOSPITAL_ADDRESS"] = cboAddress.Text;

                        //dr["AMT_AFTER_DIS"] = String.Format("{0:N}",
                        //    Convert.ToDecimal(Convert.ToDouble(dr["CLAIMED_AMT"]) -
                        //    (Convert.ToDouble(dr["CLAIMED_AMT"]) * Convert.ToDouble(dr["DISCOUNT"]) / 100)));
                    }
                }

                Latest.Columns.Add("DEAR", typeof(string));
                Latest.Columns.Add("TEXT", typeof(string));
                Latest.Columns.Add("EMAIL", typeof(string));
                Latest.Columns.Add("SIGNATURE", typeof(string));
                Latest.Columns.Add("USER_CC", typeof(string));

                string Dear = tbDear.Text.Trim(), CC = tbCC.Text.Trim();
                if (Dear == "") Dear = "Sir or Madam,";

                foreach (DataRow dr in Latest.Rows)
                {
                    dr["DEAR"] = Dear;
                    dr["TEXT"] = tbText.Text.Trim();

                    // oudom
                    dr["EMAIL"] = (cbSignature.SelectedItem as ComboboxMultiVal).Value["Email"];

                    //claimNo = dr["CLAIM_NO"].ToString();
                    //if (claimNo.ToLower().Contains("hns"))
                    //{
                    //    dr["EMAIL"] = "hnsclaims@forteinsurance.com";
                    //}
                    //else if (claimNo.ToLower().Contains("gpa"))
                    //{
                    //    dr["EMAIL"] = "gpa@forteinsurance.com";
                    //}
                    //else
                    //{
                    //    dr["EMAIL"] = "figtree_blue@forteinsurance.com";
                    //}

                    // oudom
                    dr["SIGNATURE"] = (cbSignature.SelectedItem as ComboboxMultiVal).Value["Signature"];
                    //dr["SIGNATURE"] = "Accident and Health Department";

                    dr["USER_CC"] = CC;
                }

                Latest.DefaultView.Sort = "INSURED ASC";
                Latest = Latest.DefaultView.ToTable();

                //var report = new Reports.SettlementLetterToInsured();
                CrystalDecisions.CrystalReports.Engine.ReportClass report = new CrystalDecisions.CrystalReports.Engine.ReportClass();
                if (rbToInsured.Checked)
                    report = new Reports.SettlementLetterToInsured();
                else if (rbToHospital.Checked)
                    report = new Reports.SettlementLetterToHospitalOld();
                report.SetDataSource(Latest);
                var frm = new frmViewInstructionNote();
                frm.rpt = report;
                frm.Text = "Settlement Letter";
                frm.Show();
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dgvClaimDetail_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvClaimDetail.Rows.Count > 0)
                frmDocumentControl.enabledButt(btnGenerateLetter);
            else
                frmDocumentControl.disabledButt(btnGenerateLetter);
        }

        private void rbChequeNo_CheckedChanged(object sender, EventArgs e)
        {
            SettlementSelectChange();
        }

        void SettlementSelectChange()
        {
            tbChequeNo.Enabled = false;
            tbBankTranNo.Enabled = false;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
            frmDocumentControl.disabledButt(btnRefreshTable);
            frmDocumentControl.disabledButt(btnSave);
            lvRequisition.Enabled = false;

            if (rbChequeNo.Checked)
            {
                tbChequeNo.Enabled = true;
                tbBankTranNo.Text = "";
            }
            else if (rbBankTranNo.Checked)
            {
                tbBankTranNo.Enabled = true;
                tbChequeNo.Text = "";
            }
            else if (rbRequisition.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
                frmDocumentControl.enabledButt(btnRefreshTable);
                lvRequisition.Enabled = true;
            }
        }

        private void rbBankTranNo_CheckedChanged(object sender, EventArgs e)
        {
            SettlementSelectChange();
        }

        private void rbRequisition_CheckedChanged(object sender, EventArgs e)
        {
            SettlementSelectChange();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rbChequeNo.Checked = true;
            SettlementSelectChange();
            lvRequisition.Clear();
            dgvClaimDetail.DataSource = null;
            tbDear.Text = "";
            tbCC.Text = "";
            tbText.Text = "";

            dgvFile.Rows.Clear();
            tbSettleReceiver.Text = "";
            tbSettleCC.Text = "";
            gbSettleSendEmail.Visible = false;
        }

        private void rbToInsured_CheckedChanged(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            tbCC.Enabled = rbToInsured.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable dtTemp = new DataTable();
                string RefNo = "";

                if (rbChequeNo.Checked)
                {
                    RefNo = tbChequeNo.Text.ToUpper().Trim();
                }
                else if (rbBankTranNo.Checked)
                {
                    RefNo = tbBankTranNo.Text.ToUpper().Trim();
                }
                else if (rbRequisition.Checked)
                {
                    List<string> SelectedItem = new List<string>();
                    string SelectedRequi = "";

                    if (lvRequisition.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem lvi in lvRequisition.CheckedItems)
                        {
                            SelectedItem.Add("'" + lvi.SubItems[0].Text + "'");
                        }
                        SelectedItem.Sort();
                        SelectedRequi = SelectedItem.Aggregate((i, j) => i + "," + j); //Concat list item with ","
                    }

                    RefNo = SelectedRequi;
                }

                if (RefNo == "")
                {
                    Msgbox.Show("Error When Saving Record. (No RefNo)");
                    return;
                }

                if (rbRequisition.Checked) RefNo = RefNo.Replace("'", "");
                dtTemp = crud.ExecQuery("SELECT * FROM USER_LETTER_HIST WHERE LETTER_TYPE = 'SETTLEMENT' AND REF_NO like '%" + RefNo + "%'");
                if (dtTemp.Rows.Count > 0) //Has hist => Update
                {
                    Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand();
                    cmd.CommandText = "UPDATE USER_LETTER_HIST SET NOTE = :textbox, DEAR = :dear, SIGNATURE = :signature, CC = :cc WHERE LETTER_TYPE = 'SETTLEMENT' AND REF_NO like '%" + RefNo + "%'";
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("textbox", tbText.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("dear", tbDear.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("signature", cbSignature.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("cc", tbCC.Text));
                    crud.ExecNonQuery(cmd);
                }
                else //No Hist => Insert
                {
                    Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand();
                    cmd.CommandText = "INSERT INTO USER_LETTER_HIST(LETTER_TYPE,REF_NO,NOTE,DEAR,SIGNATURE,CC) VALUES (:lettertype,:refno,:textbox,:dear,:signature,:cc)";
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("lettertype", "SETTLEMENT"));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("refno", RefNo));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("textbox", tbText.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("dear", tbDear.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("signature", cbSignature.Text));
                    cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("cc", tbCC.Text));
                    crud.ExecNonQuery(cmd);
                }

                Msgbox.Show("Record Saved!");

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void lvRequisition_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvRequisition.Sort();
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

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvFile.Rows.Add(Path.GetFileName(file), file);
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
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

        private void btnSettlePreview_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvClaimDetail.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim detail unavailable. Please try again.");
                    return;
                }

                string content = getSettleMailContent();

                var frm = new frmViewEmail();
                frmViewEmail.type = "A&H";
                frmViewEmail.resetcontent = content;
                frmViewEmail.finalizemailadd = "hnsclaims@forteinsurance.com";
                frmViewEmail.finalizeusername = "HNS Claims Team";
                string body = string.Empty;
                using (StreamReader reader = new StreamReader("Html/2022Email.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{text}", content);
                body = body.Replace("{username}", "HNS Claims Team");
                body = body.Replace("{department}", "A&H Claims Unit | Underwriting Department");
                body = body.Replace("{user_email}", "hnsclaims@forteinsurance.com");
                body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Standard_Forte.png");
                body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\fb.png");
                body = body.Replace("cid:YT_logo", Application.StartupPath + @"\Html\yt.png");
                body = body.Replace("cid:Mail_logo", Application.StartupPath + @"\Html\mail.png");

                frm.wbEmail.DocumentText = body;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnSettleSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClaimDetail.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim detail unavailable. Please try again.");
                    return;
                }

                DataTable dtEmailIn = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "emailInfo", UserName });
                smtpSer = dtEmailIn.Rows[0].ItemArray[0].ToString();
                mail_add = dtEmailIn.Rows[0].ItemArray[1].ToString();
                if (dtEmailIn.Rows[0].ItemArray[2].ToString() != "")
                    mail_pass = Cipher.Decrypt(dtEmailIn.Rows[0].ItemArray[2].ToString(), HashPass);
                else
                    mail_pass = "";
                port = Convert.ToInt16(dtEmailIn.Rows[0].ItemArray[3].ToString());

                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return;
                }

                if (tbSettleReceiver.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email reciever!");
                    return;
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    string body = "", content = getSettleMailContent();
                    using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{text}", content);
                    body = body.Replace("{username}", "HNS Claims Team");
                    body = body.Replace("{department}", "A&H Claims Unit | Underwriting Department");
                    body = body.Replace("{user_email}", "hnsclaims@forteinsurance.com");


                    //SmtpClient client = new SmtpClient(smtpSer);

                    MailMessage message = new MailMessage();

                    //set formatting email message
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    message.From = new MailAddress(mail_add);

                    System.Net.ServicePointManager.Expect100Continue = true;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    if (tbSettleReceiver.Text.Trim() != "")
                    {
                        string[] to = tbSettleReceiver.Text.Split(',');
                        foreach (string s in to)
                        {
                            if (s.Trim() != "")
                                message.To.Add(s.Trim());
                        }
                    }

                    message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'PaymentNotice'").Rows[0][0].ToString();
                    message.Subject = message.Subject.Replace("%PolHolder%", dgvClaimDetail.Rows[0].Cells["INSURED"].Value.ToString());

                    message.CC.Add(new MailAddress(mail_add)); //cc the sender to store in Inbox
                    if (tbSettleCC.Text.Trim() != "")
                    {
                        string[] cc = tbSettleCC.Text.Split(',');
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
                    LinkedResource img1 = new LinkedResource(@"Html\forte-general-logo-red.png", "image/png");
                    img1.ContentId = "forte-general-logo-red";
                    LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                    img2.ContentId = "FB_logo";
                    LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                    img3.ContentId = "YT_logo";
                    LinkedResource img4 = new LinkedResource(@"Html\linkedin.png", "image/png");
                    img4.ContentId = "linkedin";

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
                           new string[] { "Insert", dgvClaimDetail.Rows[0].Cells["CLAIM_NO"].Value.ToString(), "PaymentNotice", tbSettleReceiver.Text, content, "", "", "", "", "", "", tbSettleCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), UserName });

                    saveEmailHistory(tbSettleReceiver.Text.Trim().Replace(" ", String.Empty), tbSettleCC.Text.Trim().Replace(" ", String.Empty));
                    requeryEmailSuggestion();

                    Msgbox.Show("Email sent!");
                    Cursor.Current = Cursors.AppStarting;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        string getSettleMailContent()
        {
            string dear = tbDear.Text.Trim(), transfermethod = "", tabledata = "";
            if (dear == "")
                dear = "Sir or Madam";
            if (rbChequeNo.Checked)
                transfermethod = "cheque";
            else if (rbBankTranNo.Checked)
                transfermethod = "bank transfer";
            else if (rbRequisition.Checked)
                transfermethod = "cash";

            foreach (DataGridViewRow dr in dgvClaimDetail.Rows)
            {
                tabledata += "<tr>";
                tabledata += "<td>" + (dr.Cells[0].RowIndex + 1).ToString() + "</td>";
                tabledata += "<td>" + dr.Cells["CLAIM_NO"].Value.ToString() + "</td>";
                tabledata += "<td>" + dr.Cells["RISK_NAME"].Value.ToString() + "</td>";
                tabledata += "</tr>";
            }

            return crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                    new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                    new string[] { "PaymentNotice", dear, transfermethod, tabledata, "", "", "", "", "", "", "" });
        }

        void requeryEmailSuggestion()
        {
            tbSettleReceiver.EmailAutocompleteSource = null;
            tbSettleCC.EmailAutocompleteSource = null;
            // Add Email suggestion
            string[] email = new string[1];
            DataTable dtTemp = crud.ExecQuery("SELECT DISTINCT SENDER FROM USER_EMAIL_SENDER_HIST WHERE USER_CODE = '" + UserName + "'");
            foreach (DataRow dr in dtTemp.Rows)
            {
                email[email.Length - 1] = dr["SENDER"].ToString();
                if (email.Length < dtTemp.Rows.Count)
                    Array.Resize(ref email, email.Length + 1);
            }
            tbSettleReceiver.EmailAutocompleteSource = email;
            email = new string[1];
            dtTemp = crud.ExecQuery("SELECT DISTINCT CC FROM USER_EMAIL_CC_HIST WHERE USER_CODE = '" + UserName + "'");
            foreach (DataRow dr in dtTemp.Rows)
            {
                email[email.Length - 1] = dr["CC"].ToString();
                if (email.Length < dtTemp.Rows.Count)
                    Array.Resize(ref email, email.Length + 1);
            }
            tbSettleCC.EmailAutocompleteSource = email;
            //
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

        #endregion

        #region GPA Claim Checking
        private void bnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string clno = tbClNoGPAClCheck.Text.ToUpper().Trim();

                if (clno.Length < 20)
                {
                    Msgbox.Show("Incorrect Claim No");
                    return;
                }

                string pro = clno.Substring(7, 3);
                if (pro != "GPA" && pro != "PAC" && pro != "TRI" && pro != "PAE" && pro != "PAP")
                {
                    Msgbox.Show("Only GPA PAC TRI PAE, and PAP product available in this function.");
                    return;
                }
                
                string[] Key = new string[] { "p_claim_no" };
                string[] Value = new string[] { clno };

                //DataTable result = crud.ExecQuery("SELECT * FROM VIEW_GPA_CLAIM_CHECK WHERE CLAIM_NO = '" + clno + "'");
                DataTable result = crud.ExecSP_OutPara("sp_gpa_claim_checking", Key, Value);

                if (result.Rows.Count <= 0)
                {
                    Msgbox.Show("Not record found!");
                    return;
                }

                string NotPayable = tbNotPayable.Text.Trim(),
                    GrandTotal = tbGrandTotal.Text.Trim(),
                    NotPayableRemark = tbNotPayableRemark.Text.Trim(),
                    GrandTotalRemark = tbGrandTotalRemark.Text.Trim();

                //if blank no need to convert to currency
                NotPayable = (NotPayable == "") ? "" : String.Format("{0:N}", Convert.ToDecimal(NotPayable));
                GrandTotal = (GrandTotal == "") ? "" : String.Format("{0:N}", Convert.ToDecimal(GrandTotal));

                //remove $ sign 
                NotPayable = NotPayable.Replace("$", "");
                GrandTotal = GrandTotal.Replace("$", "");

                //add bracket if remark not null
                NotPayableRemark = (NotPayableRemark == "") ? "" : " (" + NotPayableRemark + ")";
                GrandTotalRemark = (GrandTotalRemark == "") ? "" : " (" + GrandTotalRemark + ")";

                //add new column to result
                result.Columns.Add("NOT_PAYABLE", typeof(System.String));
                result.Columns.Add("GRAND_TOTAL", typeof(System.String));
                result.Columns.Add("PRODUCT", typeof(System.String));
                result.Columns.Add("USER_NAME", typeof(System.String));


                foreach (DataRow row in result.Rows)
                {
                    row["NOT_PAYABLE"] = NotPayable + NotPayableRemark;
                    row["GRAND_TOTAL"] = GrandTotal + GrandTotalRemark;
                    row["PRODUCT"] = pro;
                    row["USER_NAME"] = UserName;
                }

                Reports.GPAClaimChecking report = new Reports.GPAClaimChecking();
                report.SetDataSource(result);
                GPAClCheckViewer.ReportSource = report;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void tbClNoGPAClCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bnGenerate.PerformClick();
            }
        }

        #endregion

        private void tbRejectClNo_Leave(object sender, EventArgs e)
        {

        }

        private void btnExclDef_Click(object sender, EventArgs e)
        {
            lvDefExclu.Clear();
            string ClNo = tbRejectClNo.Text.Trim().ToUpper();
            if (ClNo != "")
            {
                if (ClNo.Length < 20)
                {
                    Msgbox.Show("Incorrect Claim No");
                    return;
                }

                string pro = ClNo.Substring(7, 3);
                if (pro != "GPA" && pro != "PAC" && pro != "HNS")
                {
                    Msgbox.Show("Only GPA, PAC and HNS product available in this function.");
                    return;
                }
                else if (pro == "GPA" || pro == "PAC")
                {
                    DataTable dt = crud.ExecQuery("select * from USER_CLAIM_EMAIL_EXCLU_DEF WHERE PRODUCT= 'GPA' order by TYPE");

                    lvDefExclu.CheckBoxes = true;
                    lvDefExclu.View = View.Details;

                    lvDefExclu.Columns.Add("Type", 110);
                    lvDefExclu.Columns.Add("Eng", 350);
                    lvDefExclu.Columns.Add("Kh", 350);

                    foreach (DataRow dr in dt.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(dr["TYPE"].ToString());
                        lvi.SubItems.Add(dr["ENG"].ToString());
                        lvi.SubItems.Add(dr["KH"].ToString());

                        lvDefExclu.Items.Add(lvi);
                    }
                }
                #region HNS_letter
                //else if (pro == "HNS")
                //{
                //    DataTable dt;
                //    DataTable plan_dt = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                //  "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                //  "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                //  "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A') CAUSE, " +
                //  "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, nvl(nullif(instr(INT_COMMENTS, '('),0),instr(INT_COMMENTS, 'D:')) - instr(INT_COMMENTS, 'H:') - 2)), 'N/A') HOSPITAL, " +
                //  "nvl(REGEXP_SUBSTR(nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, instr(INT_COMMENTS, 'D:') - instr(INT_COMMENTS, 'H:') - 2)), 'N/A'), '\\(([^)]*)\\)', 1, 1, NULL, 1),'N/A') TREATMENT_DATE, " +
                //  "INT_BPARTY_CODE CC, ( SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where  CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + ClNo + "'");
                //    if (plan_dt.Rows.Count != 0)
                //    {
                //        DataRow dr_plan = plan_dt.Rows[0];
                //        //string plan = dr_plan[10].ToString().Substring(6, 2);
                //        string plan = dr_plan[10].ToString();
                //        if (plan.Count()==8)
                //        {
                //            dt = crud.ExecQuery("select * from USER_CLAIM_EMAIL_EXCLU_DEF WHERE PRODUCT= 'HNS++' order by ENG");
                //        }
                //        else
                //        {
                //            dt = crud.ExecQuery("select * from USER_CLAIM_EMAIL_EXCLU_DEF WHERE PRODUCT= 'HNS' order by ENG");
                //        }
                //        lvDefExclu.CheckBoxes = true;
                //        lvDefExclu.View = View.Details;

                //        lvDefExclu.Columns.Add("Type", 110);
                //        lvDefExclu.Columns.Add("Eng", 350);
                //        lvDefExclu.Columns.Add("Kh", 350);

                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            ListViewItem lvi = new ListViewItem(dr["TYPE"].ToString());
                //            lvi.SubItems.Add(dr["ENG"].ToString());
                //            lvi.SubItems.Add(dr["KH"].ToString());

                //            lvDefExclu.Items.Add(lvi);
                //        }
                //    }
                //    else
                //    {
                //        Msgbox.Show("This transaction appear to have no content- Input Wrong Claim No!");
                //    }
                //    Cursor.Current = Cursors.WaitCursor;
                //}
                #endregion

            }
            else
            {
                Msgbox.Show("This transaction appears to have no content - Please input claim No.");
            }
        }

        private void tbRejectClNo_MouseClick(object sender, MouseEventArgs e)
        {
            cbProduct.Text = "";
            cbProductType.Text = "";
            txtEnglish.Text = null;
            txtKhmer.Text = null;

        }

        private void btnClearDef_Click(object sender, EventArgs e)
        {
            cbProduct.Text = "";
            cbProductType.Text = "";
            txtEnglish.Text = null;
            txtKhmer.Text = null;
            lvDefExclu.Clear();
        }

        private void btnAddDefExcl_Click(object sender, EventArgs e)
        {
            if (cbProduct.Text == "" || cbProductType.Text == "" || txtEnglish.Text == null || txtKhmer.Text == null)
            {
                Msgbox.Show("The input field can not be null");
            }
            else
            {
                DataTable def_dt = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_EXCLU_DEF WHERE TYPE = '" + cbProductType.Text + "' and ENG = '" + txtEnglish.Text + "' and PRODUCT = '" + cbProduct.Text + "' and KH = '" + txtKhmer.Text + "'");
                if (def_dt.Rows.Count == 0)
                {
                    string a = "INSERT INTO USER_CLAIM_EMAIL_EXCLU_DEF VALUES ( '" + cbProduct.Text + "','" + cbProductType.Text + "','" + txtEnglish.Text + "','" + txtKhmer.Text + "')";
                    crud.ExecNonQuery(a);
                    Cursor.Current = Cursors.WaitCursor;
                    Msgbox.Show("Added Successfully - Please regenerate the Exlusion/Definitions to see result");
                }
                else
                {
                    Msgbox.Show("Items are already existing");
                }
            }
        }

        #region MedicalRejectionLetter

        private void btnExcluDefin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClaimNo.Text.Trim()))
                return;

            Cursor = Cursors.WaitCursor;

            dtClaimDt = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
            "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
            "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
            "nvl(INT_DIAGNOSIS, nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A')) CAUSE, " +
            "nvl(INT_OTH_HOSPITAL, INT_COMMENTS) HOSPITAL, nvl(INT_OTH_HOSPITAL, 'true') IS_NULL_OTH_HOSPITAL," +
            "TO_CHAR(INT_DATE_LOSS) TREATMENT_DATE, " +
            "INT_BPARTY_CODE CC, (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where  CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + txtClaimNo.Text.ToUpper() + "'");

            if (dtClaimDt.Rows.Count != 0)
            {
                dgvClaimInfo.DataSource = dtClaimDt;
                dgvClaimInfo.RowsDefaultCellStyle.ForeColor = Color.Black;

                #region DatagridviewData
                dgvDefinition.Columns.Clear();
                var plan = Regex.Replace(dtClaimDt.Rows[0]["PLAN_DESCRIPTION"].ToString(), @"[A-Za-z]+", string.Empty).Trim();

                var pro = txtClaimNo.Text.Substring(6, 4).ToLower();

                if (pro.Contains("hns"))
                {
                    dtExcDef = plan == "+" ? crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS' order by PARTS, ENG")
                    : crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS++' order by PARTS, ENG");
                }
                else if (pro.Contains("gpa") || pro.Contains("pac"))
                {
                    dtExcDef = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'GPA' order by PARTS, ENG");
                }
                else if (pro.Contains("pae"))
                {
                    dtExcDef = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'PAE' order by PARTS, ENG");
                }
                else if (pro.Contains("bhp"))
                {
                    dtExcDef = crud.ExecQuery("select * from user_email_bhp_excludef where PRODUCTS = 'BHP' order by PARTS, ENG");
                }
                DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                //CheckBox chk = new CheckBox();
                dgvDefinition.Columns.Add(CheckboxColumn);
                dgvDefinition.DataSource = dtExcDef;

                dgvDefinition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                SetDgvDefinitionColumnWidth();
                DataGridViewColumn column = dgvDefinition.Columns[0];
                column.Width = 35;
                dgvDefinition.Columns[0].Resizable = DataGridViewTriState.False;
                dgvDefinition.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // add checkbox header
                Rectangle rect = dgvDefinition.GetCellDisplayRectangle(0, -1, true);
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
                dgvDefinition.Controls.Add(checkboxHeader);

                dgvDefinition.Columns[1].Visible = false;
                dgvDefinition.Columns[6].Visible = false;
                dgvDefinition.Columns[7].Visible = false;

                dgvDefinition.RowsDefaultCellStyle.Font = new Font("Khmer OS Siemreap", 9.75f, FontStyle.Regular);

                for (int i = 1; i < dgvDefinition.Columns.Count; i++)
                {
                    dgvDefinition.Columns[i].ReadOnly = true;
                }
                txtSearchDefExclu.Enabled = dgvDefinition.Rows.Count > 0;
                #endregion
            }
            else
            {
                Msgbox.Show("This transaction appear to have no content- Input Wrong Claim No!");
            }

            Cursor = Cursors.Arrow;
        }
        private void txtClaimNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClaimNo.Text.Trim()))
            {
                var pro = txtClaimNo.Text.Substring(6, 4).ToLower();
                cboOtherExclusions.Enabled = pro == "hhns";
            }
        }

        private void dgvDefinition_DataSourceChanged(object sender, EventArgs e)
        {
            this.dgvDefinition.ForeColor = System.Drawing.Color.Black;
            dgvDefinition.Columns[3].Width = 530;
            dgvDefinition.Columns[4].Width = 530;
            CommonFunctions.HighLightGrid(dgvDefinition);

        }
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                for (int i = 0; i < dgvDefinition.RowCount; i++)
                {
                    dgvDefinition[0, i].Value = ((CheckBox)dgvDefinition.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dgvDefinition[0, i].Value;
                    CheckCount(isChecked);
                }
                //lblSel.Text = num.ToString();
                dgvDefinition.EndEdit();


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


        public void btnGenerateClaim_Click(object sender, EventArgs e)
        {
            var isAlreadySaved = crud.ExecQuery("select CLAIM_NO from USER_MEDICAL_REJECTION_LETTER where CLAIM_NO = '" + txtClaimNo.Text.Trim().ToUpper() + "'").Rows.Count > 0;
            if (isAlreadySaved)
            {
                Msgbox.Show("This Claim No " + txtClaimNo.Text.Trim().ToUpper() + " is already generated Rejection Letter. Please check again!");
                BringToFront();
                return;
            }

            if (cboOtherExclusions.SelectedIndex == 0)
            {
                selectedDoc = GetDataTableFromDGV(dgvDefinition);
                if (selectedDoc.Rows.Count <= 0 || selectedDoc == null)
                {
                    Msgbox.Show("Please check some Definition or Exclusion");
                    return;
                }
                if (dgvClaimInfo.Rows.Count <= 0)
                {
                    Msgbox.Show("No claim information to preview");
                    return;
                }
                frmMedicalRejectionLetter frmMedicalRejectionLetter = new frmMedicalRejectionLetter(false, null, txtClaimNo.Text.ToUpper());
                frmMedicalRejectionLetter.ShowDialog();
            }
            else
            {
                if (dgvClaimInfo.Rows.Count <= 0)
                {
                    Msgbox.Show("No claim information to preview");
                    return;
                }
                var overPeriod = string.Empty;

                if (cboOtherExclusions.Text == "HNS ACCIDENT OVER PERIOD")
                {
                    overPeriod = rdbOver24Hours.Checked ? "24" : (rdbOver48Hours.Checked ? "48" : "72");
                }

                frmMedicalRejectionLetter frmMedicalRejectionLetter = new frmMedicalRejectionLetter(false, null, txtClaimNo.Text.ToUpper(), overPeriod);
                frmMedicalRejectionLetter.ShowDialog();
            }
        }
        private void dgvDefinition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex != 0)
                return;


            if (dgvDefinition.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvDefinition.SelectedCells)
                {
                    dgvDefinition[0, dgvc.RowIndex].Value = true;
                }
                for (int i = 0; i < dgvDefinition.RowCount; i++)
                {
                    isChecked = (bool)dgvDefinition.Rows[i].Cells[0].EditedFormattedValue;
                    CheckCount(isChecked);
                    dgvDefinition.Rows[i].Cells[0].Value = isChecked;
                }
                //lblSel.Text = num.ToString();
            }
        }

        public DataTable GetDataTableFromDGV(DataGridView dgv)
        {

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("TYPE");
            dt1.Columns.Add("PARTS");
            dt1.Columns.Add("PARTS_KH");
            dt1.Columns.Add("TYPE_KH");
            dt1.Columns.Add("ENG");
            dt1.Columns.Add("KH");



            foreach (DataGridViewRow row in dgvDefinition.Rows)
            {
                string status = "";
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {

                        dt1.Rows.Add(row.Cells["TYPE"].Value.ToString(), row.Cells["PARTS"].Value.ToString(), row.Cells["PARTS_KH"].Value.ToString(), row.Cells["TYPE_KH"].Value.ToString(), row.Cells["ENG"].Value.ToString(), row.Cells["KH"].Value.ToString());


                    }
                }
            }


            return dt1;
        }

        private void dgvDefinition_DataSourceChanged_1(object sender, EventArgs e)
        {
            this.dgvDefinition.ForeColor = System.Drawing.Color.Black;
            dgvDefinition.Columns[3].Width = 530;
            dgvDefinition.Columns[4].Width = 530;
            CommonFunctions.HighLightGrid(dgvDefinition);
        }

        #endregion

        private void printStripButton_Click(object sender, EventArgs e)
        {
            // printDialog associates with PrintDocument
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print(); // Print the document
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // draws the string onto the print document
            //Font drawFont = new Font("Arial", 42);
            //e.Graphics.DrawString(webBrowser1.Text,drawFont , Brushes.Black, 100, 100);

            //e.Graphics.PageUnit = GraphicsUnit.Inch; 

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //RtfToHtml r = new RtfToHtml();
            //r.OutputFormat = RtfToHtml.eOutputFormat.HTML_401;

            //string html = r.ConvertString(richTextBox1.Rtf);

            //string url = "\"https://www.sautinsoft.com/products/rtf-to-html/order.php\"";
            //string replace = string.Format("<div style=\"text-align:center;\">The unlicensed version of &laquo;RTF to HTML .Net&raquo;.<br><a href={0}>Get the full featured version!</a></div>", url);

            //html = html.Replace(replace, "");

            //this.webBrowser1.DocumentText = html;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //webBrowser1.Document.ExecCommand("SelectAll", false, null);
            //webBrowser1.Document.ExecCommand("Copy", false, null);
            //richTextBox1.Paste(); 
        }

        private void txtClaimNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnExcluDefin_Click(null, null);
        }

        private void dgvDefinition_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            Msgbox.Show(dgvDefinition.SelectedCells[0].Value.ToString(), new Font("Khmer OS Siemreap", 9, FontStyle.Regular));
        }

        private void dgvClaimInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Msgbox.Show(dgvClaimInfo.SelectedCells[0].Value.ToString());
        }

        private void txtSearchDefExclu_Enter(object sender, EventArgs e)
        {
            SetDgvDefinitionColumnWidth();

            if (!txtSearchDefExclu.Text.Trim().Equals("--- Search English Definition or Exclusion ---"))
                return;

            txtSearchDefExclu.Clear();
            txtSearchDefExclu.ForeColor = Color.Black;
        }

        private void txtSearchDefExclu_Leave(object sender, EventArgs e)
        {
            SetDgvDefinitionColumnWidth();

            if (!txtSearchDefExclu.Text.Trim().Equals(string.Empty))
                return;

            txtSearchDefExclu.Text = "--- Search English Definition or Exclusion ---";
            txtSearchDefExclu.ForeColor = Color.Gray;
        }

        private void txtSearchDefExclu_TextChanged(object sender, EventArgs e)
        {
            SetDgvDefinitionColumnWidth();

            if (txtSearchDefExclu.Text.Trim().Equals("--- Search English Definition or Exclusion ---"))
            {
                dgvDefinition.DataSource = dtExcDef;
                return;
            }

            if (txtSearchDefExclu.Text.Trim().Equals(string.Empty))
                return;

            DataView dvExcDef = new DataView(dtExcDef);
            dvExcDef.RowFilter = "[ENG] LIKE '%" + txtSearchDefExclu.Text.Trim() + "%'";
            dgvDefinition.DataSource = dvExcDef;
        }

        private void SetDgvDefinitionColumnWidth()
        {
            dgvDefinition.Columns["TYPE"].Width = 45;
            dgvDefinition.Columns["PARTS"].Width = 20;
            dgvDefinition.Columns["ENG"].Width = 180;
            dgvDefinition.Columns["KH"].Width = 180;
        }


        private void dgvClaimDetail_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            //dgvClaimDetail.Refresh();
            DataTable UpdateDt = new DataTable();
            foreach (DataGridViewColumn col in dgvClaimDetail.Columns)
            {
                UpdateDt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgvClaimDetail.Rows)
            {
                DataRow dRow = UpdateDt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                UpdateDt.Rows.Add(dRow);
            }
            //string sql = "INSERT INTO VIEW_REQUISITION_TEMP VALUES ('";
            //foreach(DataColumn dc in UpdateDt.Columns){
            //    sql += UpdateDt.Rows[e.RowIndex][dc] + "','";
            //}
            //sql += ")";
            DataTable dtLettercheck = crud.ExecQuery("select CLAIM_NO,REQUISITION_NO from view_requisition_temp where CLAIM_NO = '" + UpdateDt.Rows[e.RowIndex][2].ToString() + "'");
            if (dtLettercheck.Rows.Count <= 0)
            {
                crud.ExecSP_NoOutPara("SP_USER_INSERT_SETTLETLETTER",
                              new string[] { "cl_input_type", "clREQUISITION_NO", 
                                         "clREQ_AMOUNT", "clCLAIM_NO", 
                                         "clINSURED","clRISK_NAME","clADMISSION_DATE","clCLAIMED_AMT","clPAYABLE_AMT","clCREATED_DATE","clADDRESS",
                                         "clCC","clPANEL_HOSPITAL","clHOSPITAL_ADDRESS","clPV", "clBank_Tran_no","clChq_no","clPREVIOUS_PAYMENT","clLAST_PAYMENT"},
                              new string[] { "letter_Insert", UpdateDt.Rows[e.RowIndex][0].ToString(), UpdateDt.Rows[e.RowIndex][1].ToString(), UpdateDt.Rows[e.RowIndex][2].ToString(), UpdateDt.Rows[e.RowIndex][3].ToString() ,
                              UpdateDt.Rows[e.RowIndex][4].ToString(),UpdateDt.Rows[e.RowIndex][5].ToString(),UpdateDt.Rows[e.RowIndex][6].ToString(),UpdateDt.Rows[e.RowIndex][7].ToString(),Convert.ToDateTime(UpdateDt.Rows[e.RowIndex][8].ToString()).ToShortDateString(),
                              UpdateDt.Rows[e.RowIndex][9].ToString(),UpdateDt.Rows[e.RowIndex][10].ToString(),UpdateDt.Rows[e.RowIndex][11].ToString(),UpdateDt.Rows[e.RowIndex][12].ToString(),
                              UpdateDt.Rows[e.RowIndex][13].ToString(),UpdateDt.Rows[e.RowIndex][14].ToString(),UpdateDt.Rows[e.RowIndex][15].ToString(),UpdateDt.Rows[e.RowIndex][16].ToString(),UpdateDt.Rows[e.RowIndex][17].ToString()});
            }
            else
            {
                crud.ExecNonQuery("delete from view_requisition_temp where CLAIM_NO = '" + UpdateDt.Rows[e.RowIndex][2].ToString() + "'");
                crud.ExecSP_NoOutPara("SP_USER_INSERT_SETTLETLETTER",
                               new string[] { "cl_input_type", "clREQUISITION_NO", 
                                         "clREQ_AMOUNT", "clCLAIM_NO", 
                                         "clINSURED","clRISK_NAME","clADMISSION_DATE","clCLAIMED_AMT","clPAYABLE_AMT","clCREATED_DATE","clADDRESS",
                                         "clCC","clPANEL_HOSPITAL","clHOSPITAL_ADDRESS","clPV", "clBank_Tran_no","clChq_no","clPREVIOUS_PAYMENT","clLAST_PAYMENT"},
                               new string[] { "letter_Insert", UpdateDt.Rows[e.RowIndex][0].ToString(), UpdateDt.Rows[e.RowIndex][1].ToString(), UpdateDt.Rows[e.RowIndex][2].ToString(), UpdateDt.Rows[e.RowIndex][3].ToString() ,
                              UpdateDt.Rows[e.RowIndex][4].ToString(),UpdateDt.Rows[e.RowIndex][5].ToString(),UpdateDt.Rows[e.RowIndex][6].ToString(),UpdateDt.Rows[e.RowIndex][7].ToString(),Convert.ToDateTime(UpdateDt.Rows[e.RowIndex][8].ToString()).ToShortDateString(),
                              UpdateDt.Rows[e.RowIndex][9].ToString(),UpdateDt.Rows[e.RowIndex][10].ToString(),UpdateDt.Rows[e.RowIndex][11].ToString(),UpdateDt.Rows[e.RowIndex][12].ToString(),
                              UpdateDt.Rows[e.RowIndex][13].ToString(),UpdateDt.Rows[e.RowIndex][14].ToString(),UpdateDt.Rows[e.RowIndex][15].ToString(),UpdateDt.Rows[e.RowIndex][16].ToString(),UpdateDt.Rows[e.RowIndex][17].ToString()});
            }

        }

        private void cboOtherExclusions_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbOverPeriod.Enabled = cboOtherExclusions.Text.Equals("HNS ACCIDENT OVER PERIOD");
            dgvDefinition.Enabled = cboOtherExclusions.SelectedIndex == 0;
            dgvDefinition.DefaultCellStyle.ForeColor = cboOtherExclusions.SelectedIndex == 0 ? Color.Black : Color.Gray;
            OtherExclusion = cboOtherExclusions.Text;
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            frmMedicalRejectionHistory frmMedicalRejectionHistory = new frmMedicalRejectionHistory();
            frmMedicalRejectionHistory.ShowDialog();
        }

        private void btnViewFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"\\192.168.110.228\Infoins_IMS_Upload_doc$\Medical_Rejection_Letter_Doc\");
        }

        private void btnDocReceive_Click(object sender, EventArgs e)
        {
            //oudom
            var dtReqDocHist = crud.ExecQuery("select email_doc_code from user_claim_email_hist where claim_no = '" + lbClaimNo.Text + "' and send_type = 'DocReqNew' and is_resend = 'N' and rownum = 1");
            var dtReqDoc = crud.ExecQuery("select doc_code, is_send_close_claim from user_claim_anh_auto_rem_email where claim_number = '" + lbClaimNo.Text + "'");
            if (dtReqDocHist.Rows.Count <= 0)
            {
                Msgbox.Show("No Request Doc found.");
            }
            else
            {
                if (dtReqDoc.Rows.Count <= 0)
                    return;

                if (dtReqDoc.Rows[0]["IS_SEND_CLOSE_CLAIM"].ToString().Equals("1"))
                {
                    Msgbox.Show("This claim is already closed.");
                }
                else
                {
                    frmRequestDocReceive frmReq = new frmRequestDocReceive(lbClaimNo.Text, dtReqDoc, dtReqDocHist);
                    frmReq.ShowDialog();
                }
            }
        }

        private void btnSettlementNoticeHist_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice\" + tbClaimNo.Text.ToUpper().Replace("/", "-");

            if (!Directory.Exists(folderPath))
                Msgbox.Show("This claim has no settlement notice history.");
            else
                System.Diagnostics.Process.Start(folderPath);
        }
    }
}
