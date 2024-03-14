using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;
using Microsoft.Office.Interop.Word;

namespace Testing.Forms
{
    public partial class frmANHSettlementLetterNew : Form
    {
        private CRUD crud = new CRUD();
        private DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        private System.Data.DataTable dtExplBeni = new System.Data.DataTable();
        private System.Data.DataTable dtExplBeniReport = new System.Data.DataTable();
        private System.Data.DataTable dtNote = new System.Data.DataTable();
        private System.Data.DataTable dtNoteReport = new System.Data.DataTable();
        private System.Data.DataTable dtClaimInfoReport = new System.Data.DataTable();
        private bool stringInput = false;
        private string claimNo = string.Empty;

        private string template = string.Empty;

        private string path = System.Windows.Forms.Application.StartupPath + @"\";
        public static string FilePath = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice";
        public static string FClaimNo = string.Empty;

        public static System.Data.DataTable DtNote = new System.Data.DataTable();
        public static System.Data.DataTable DtExplainBene = new System.Data.DataTable();

        public static string Paid = string.Empty;
        public static string NonPaid = string.Empty;
        public static string NonPayableReason = string.Empty;

        private Microsoft.Office.Interop.Word._Application oWord = new Application();
        private Microsoft.Office.Interop.Word._Document templateDoc = new Document();

        private bool isExist = false;
        private long existSettlementNoticeId = 0;

        private System.Data.DataTable dtCurrentEob = new System.Data.DataTable();

        public frmANHSettlementLetterNew(string ClaimNo)
        {
            InitializeComponent();
            claimNo = ClaimNo;
        }

        private void frmANHSettlementLetterNew_Load(object sender, EventArgs e)
        {
            dtNote = new System.Data.DataTable();
            dtNote.Columns.Add("Amount", typeof(string));
            dtNote.Columns.Add("Description", typeof(string));

            dgvNote.DataSource = dtNote;
            dgvNote.Columns["Amount"].Width = 200;

            var dtCurSettlementNotice = sqlcrud.LoadData("select * from tbSETTLEMENT_NOTICE where CLAIM_NO = '" + claimNo.ToUpper().Trim() + "'").Tables[0];
            if (dtCurSettlementNotice.Rows.Count > 0)
            {
                isExist = true;

                var drCurSettlementNotice = dtCurSettlementNotice.Rows[0];
                existSettlementNoticeId = Convert.ToInt64(drCurSettlementNotice["ID"].ToString());

                txtAttn.Text = drCurSettlementNotice["ATTN"].ToString();
                txtPolicyholder.Text = drCurSettlementNotice["POLICY_HOLDER"].ToString();
                txtAddress.Text = drCurSettlementNotice["CUSTOMER_ADDRESS"].ToString();
                txtMemberName.Text = drCurSettlementNotice["MEMBER_NAME"].ToString();
                txtPolicy.Text = drCurSettlementNotice["POLICY"].ToString();
                txtPolicyNo.Text = drCurSettlementNotice["POLICY_NO"].ToString();
                dtpAdmissonDate.Value = Convert.ToDateTime(drCurSettlementNotice["ADMISSION_DATE"].ToString());
                txtClaimNo.Text = drCurSettlementNotice["CLAIM_NO"].ToString();
                dtpDischargeDate.Value = Convert.ToDateTime(drCurSettlementNotice["DISCHARGE_DATE"].ToString());
                txtPlan.Text = drCurSettlementNotice["POLICY_PLAN"].ToString();
                txtLengthOfStay.Text = drCurSettlementNotice["LENGTH_OF_STAY"].ToString();
                dtpRegistrationDate.Value = Convert.ToDateTime(drCurSettlementNotice["REGISTERATION_DATE"].ToString());
                txtICUStay.Text = drCurSettlementNotice["ICU_STAY"].ToString();
                txtPanelHospital.Text = drCurSettlementNotice["PANEL_HOSPITAL"].ToString();
                txtOtherHospital.Text = drCurSettlementNotice["OTHER_HOSPITAL"].ToString();
                txtDiagnosis.Text = drCurSettlementNotice["DIAGNOSIS"].ToString();
                txtPaymentBene.Text = drCurSettlementNotice["PAYMENT_BENEFICIARY"].ToString();
                txtExchangeRate.Text = drCurSettlementNotice["EXCHANGE_RATE"].ToString();
                txtEmail.Text = drCurSettlementNotice["EMAIL"].ToString();
                txtCC.Text = drCurSettlementNotice["CC"].ToString();

                var curSettlementNoticeID = Convert.ToInt64(dtCurSettlementNotice.Rows[0]["ID"].ToString());

                dtCurrentEob = sqlcrud.LoadData("select * from tbSETTLEMENT_NOTICE_EOB where SETTLEMENT_NOTICE_ID = " + curSettlementNoticeID + "").Tables[0];

                var dtCurrentNote = sqlcrud.LoadData("select * from tbSETTLEMENT_NOTICE_NOTE where SETTLEMENT_NOTICE_ID = " + curSettlementNoticeID + "").Tables[0];
                if (dtCurrentNote.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentNote.Rows.Count; i++)
                    {
                        var drCurrentNote = dtCurrentNote.Rows[i];
                        string amount = "USD " + Convert.ToDecimal(drCurrentNote["AMOUNT"]).ToString("N2");
                        string des = drCurrentNote["DESCRIPTION"].ToString();

                        var drNote = dtNote.NewRow();
                        drNote["Amount"] = amount;
                        drNote["Description"] = des;

                        dtNote.Rows.Add(drNote);
                    }
                }
            }

            dgvExplBeni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNote.DefaultCellStyle.ForeColor = Color.Black;
            dgvExplBeni.DefaultCellStyle.ForeColor = Color.Black;

            LoadData();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                SetTemplate();

                string pClaimNo = claimNo;

                string filePath = "";
                TempFileCollection tempfile = new TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                tempfile.KeepFiles = false; //will be used when dispose tempfile
                filePath = tempfile.AddExtension("txt"); //add extension to the created Temporary File

                System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath); //open the file for writing.
                writer.Write(template); //write text to the file
                writer.Close(); //remember to close the file again.
                writer.Dispose();

                var fileInfo = new FileInfo(filePath);

                templateDoc = oWord.Documents.Open(path + @"Html\AH-Settlement-Notice-Template.docx");
                var bookMark = templateDoc.Words.First.Bookmarks.Add("entry");

                object bookmarkObj = bookMark;
                Range bookmarkRange = templateDoc.Bookmarks.get_Item(ref bookmarkObj).Range;

                bookmarkRange.InsertFile(fileInfo.FullName);

                templateDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                templateDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
                templateDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
                templateDoc.Paragraphs.LineSpacing = InchesToPoints(0.18f);
                templateDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
                templateDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
                templateDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);

                for (int i = templateDoc.Paragraphs.Count; i >= 1; i--)
                {
                    Microsoft.Office.Interop.Word.Paragraph para = templateDoc.Paragraphs[i];
                    if (string.IsNullOrWhiteSpace(para.Range.Text))
                    {
                        // Delete the empty paragraph
                        para.Range.Delete();
                    }
                }

                SaveOrUpdateToDatabase();

                templateDoc.SaveAs2(@"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice\" + pClaimNo.Replace("/", "-") + ".docx");
                templateDoc.ExportAsFixedFormat(@"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice\" + pClaimNo.Replace("/", "-") + ".pdf", WdExportFormat.wdExportFormatPDF, true);

                templateDoc.Close();
                oWord.Quit();

                File.Delete(@"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice\" + pClaimNo.Replace("/", "-") + ".docx");

                FClaimNo = @"\" + claimNo.Replace('/', '-') + ".pdf";

                FilePath = FilePath + FClaimNo;

                Close();

                //DtNote = dtNote.Copy();
                //DtExplainBene = dtExplBeni.Copy();

                //dtClaimInfoReport.TableName = "ANH_SL_CLAIM_INFO";
                //dtNoteReport.TableName = "ANH_SL_NOTES";
                //dtExplBeniReport.TableName = "ANH_SL_EXPL_BENI";

                //DataSet dsReport = new DataSet();
                //dsReport.Tables.Add(dtClaimInfoReport);
                //dsReport.Tables.Add(dtNoteReport);
                //dsReport.Tables.Add(dtExplBeniReport);

                //frmANHSettlementLetterNewRV frmReport = new frmANHSettlementLetterNewRV(dsReport, txtClaimNo.Text.ToUpper());
                //frmReport.ShowDialog();
                //Close();

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                templateDoc.Close();
                oWord.Quit();

                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }
        }

        private void SetTemplate()
        {
            GetClaimInfoReport();
            GetExplBeniReport();
            GetNoteHtmlReport();

            template = File.ReadAllText(path + @"Html\Settlement-Notice-Template.txt");

            var drClaimInfoReport = dtClaimInfoReport.Rows[0];
            template = template.Replace("%DateTimeN%", DateTime.Now.ToString("dd MMMM yyyy"));
            template = template.Replace("%Attn%", drClaimInfoReport["ATTN"].ToString());
            template = template.Replace("%PolicyHolder%", drClaimInfoReport["POLICY_HOLDER"].ToString());
            template = template.Replace("%Address%", drClaimInfoReport["ADDRESS"].ToString());
            template = template.Replace("%MemberName%", drClaimInfoReport["MEMBER_NAME"].ToString());
            template = template.Replace("%Policy%", drClaimInfoReport["POLICY"].ToString());
            template = template.Replace("%PolicyNo%", drClaimInfoReport["POLICY_NO"].ToString());
            template = template.Replace("%AdmiDate%", drClaimInfoReport["ADMISSION_DATE"].ToString());
            template = template.Replace("%ClaimNo%", drClaimInfoReport["CLAIM_NO"].ToString());
            template = template.Replace("%DisDate%", drClaimInfoReport["DISCHARGE_DATE"].ToString());
            template = template.Replace("%Plan%", drClaimInfoReport["PLAN"].ToString());
            template = template.Replace("%LStay%", drClaimInfoReport["LENGTH_OF_STAY"].ToString());
            template = template.Replace("%RegisterationDate%", drClaimInfoReport["REGISTRATION_DATE"].ToString());
            template = template.Replace("%ICUStay%", drClaimInfoReport["ICU_STAY"].ToString());
            template = template.Replace("%PanelHospital%", drClaimInfoReport["PANEL_HOSPITAL"].ToString());
            template = template.Replace("%OtherHospital%", drClaimInfoReport["OTHER_HOSPITAL"].ToString());
            template = template.Replace("%Diagnosis%", drClaimInfoReport["DIAGNOSIS"].ToString());
            template = template.Replace("%Beneficiary%", drClaimInfoReport["PAYMENT_BENE"].ToString());
            template = template.Replace("%ExchangeRate%", drClaimInfoReport["EXCHANGE_RATE"].ToString());
            template = template.Replace("%Email%", drClaimInfoReport["EMAIL"].ToString());
            template = template.Replace("%CC%", drClaimInfoReport["CC"].ToString());

            string feob = string.Empty, fnotes = string.Empty;
            decimal toc = 0.00M, tciusd = 0.00M, tenc = 0.00M, tdc = 0.00M, tol = 0.00M, ttac = 0.00M;

            for (int i = 0; i < dtExplBeniReport.Rows.Count; i++)
            {
                var drExplBeniReport = dtExplBeniReport.Rows[i];
                string eob = File.ReadAllText(path + @"Html\Settlement-Notice-EOB-Template.txt");

                eob = eob.Replace("%Stp%", drExplBeniReport["SERVICE_PROVIDED"].ToString());
                eob = eob.Replace("%Oc%", drExplBeniReport["ORIGINAL_CURRENCY"].ToString());
                eob = eob.Replace("%Ciusd%", drExplBeniReport["CURRENCY_IN_USD"].ToString());
                eob = eob.Replace("%Enc%", drExplBeniReport["EXPENSES_NOT_COVERED"].ToString());
                eob = eob.Replace("%Dc%", drExplBeniReport["DEDUCTIBLE_OR_COPLAY"].ToString());
                eob = eob.Replace("%Ol%", drExplBeniReport["OVER_LIMIT"].ToString());
                eob = eob.Replace("%Tac%", drExplBeniReport["TOTAL_AMT_COVERED"].ToString());

                feob += eob;

                toc += Convert.ToDecimal(drExplBeniReport["ORIGINAL_CURRENCY"].ToString());
                tciusd += Convert.ToDecimal(drExplBeniReport["CURRENCY_IN_USD"].ToString());
                tenc += Convert.ToDecimal(drExplBeniReport["EXPENSES_NOT_COVERED"].ToString());
                tdc += Convert.ToDecimal(drExplBeniReport["DEDUCTIBLE_OR_COPLAY"].ToString());
                tol += Convert.ToDecimal(drExplBeniReport["OVER_LIMIT"].ToString());
                ttac += Convert.ToDecimal(drExplBeniReport["TOTAL_AMT_COVERED"].ToString());
            }

            template = template.Replace("%EOB%", feob);
            template = template.Replace("%Toc%", toc.ToString("N2"));
            template = template.Replace("%Tciusd%", tciusd.ToString("N2"));
            template = template.Replace("%Tenc%", tenc.ToString("N2"));
            template = template.Replace("%Tdc%", tdc.ToString("N2"));
            template = template.Replace("%Tol%", tol.ToString("N2"));
            template = template.Replace("%Ttac%", ttac.ToString("N2"));

            for (int i = 0; i < dtNote.Rows.Count; i++)
            {
                var drNote = dtNote.Rows[i];
                string notes = File.ReadAllText(path + @"Html\Settlement-Notice-Notes-Template.txt");

                notes = notes.Replace("%Amt%", drNote["Amount"].ToString());
                notes = notes.Replace("%Description%", drNote["Description"].ToString());

                fnotes += notes;

                NonPayableReason += string.Concat("- ", drNote["Amount"].ToString(), " : ", drNote["Description"].ToString(), "<br>");
            }

            template = template.Replace("%Notes%", fnotes);

            Paid = ttac.ToString("N2");
            NonPaid = (tenc + tdc + tol).ToString("N2");

            if (!string.IsNullOrEmpty(NonPayableReason))
                NonPayableReason = NonPayableReason.Remove(NonPayableReason.Length - 4);
        }

        private void SaveOrUpdateToDatabase()
        {
            if (isExist)
            {
                sqlcrud.Executing("delete from tbSETTLEMENT_NOTICE where ID = " + existSettlementNoticeId + "");
                sqlcrud.Executing("delete from tbSETTLEMENT_NOTICE_EOB where SETTLEMENT_NOTICE_ID = " + existSettlementNoticeId + "");
                sqlcrud.Executing("delete from tbSETTLEMENT_NOTICE_NOTE where SETTLEMENT_NOTICE_ID = " + existSettlementNoticeId + "");
            }

            System.Data.DataTable dtTemp = sqlcrud.ExecuteMySqlOutPara("dbo.SP_INSERT_SETTLEMENT_NOTICE",
                "@ATTN", txtAttn.Text.Trim(),
                "@POLICY_HOLDER", txtPolicyholder.Text.Trim(),
                "@CUSTOMER_ADDRESS", txtAddress.Text.Trim(),
                "@MEMBER_NAME", txtMemberName.Text.Trim(),
                "@POLICY", txtPolicy.Text.Trim(),
                "@POLICY_NO", txtPolicyNo.Text.Trim(),
                "@ADMISSION_DATE", dtpAdmissonDate.Value,
                "@CLAIM_NO", txtClaimNo.Text.Trim(),
                "@DISCHARGE_DATE", dtpDischargeDate.Value,
                "@POLICY_PLAN", txtPlan.Text.Trim(),
                "@LENGTH_OF_STAY", txtLengthOfStay.Text.Trim(),
                "@REGISTERATION_DATE", dtpRegistrationDate.Value,
                "@ICU_STAY", txtICUStay.Text.Trim(),
                "@PANEL_HOSPITAL", txtPanelHospital.Text.Trim(),
                "@OTHER_HOSPITAL", txtOtherHospital.Text.Trim(),
                "@DIAGNOSIS", txtDiagnosis.Text.Trim(),
                "@PAYMENT_BENEFICIARY", txtPaymentBene.Text.Trim(),
                "@EXCHANGE_RATE", txtExchangeRate.Text.Trim(),
                "@EMAIL", txtEmail.Text.Trim(),
                "@CC", txtCC.Text.Trim(),
                "@CREATED_DATE", DateTime.Now,
                "@CREATED_USER", frmLogIn.Usert.ToUpper()
                );

            if (dtTemp.Rows.Count > 0)
            {
                long settlementNoticeID = Convert.ToInt64(dtTemp.Rows[0][0].ToString());


                for (int i = 0; i < dtExplBeniReport.Rows.Count; i++)
                {
                    var drExplBeniReport = dtExplBeniReport.Rows[i];
                    sqlcrud.ExecuteMySql("dbo.SP_INSERT_SETTLEMENT_NOTICE_EOB",
                        "@SETTLEMENT_NOTICE_ID", settlementNoticeID,
                        "@SERVICES_OR_TREATMENT_PROVIDED", drExplBeniReport["SERVICE_PROVIDED"].ToString(),
                        "@ORIGINAL_CURRENCY", Convert.ToDecimal(drExplBeniReport["ORIGINAL_CURRENCY"].ToString()),
                        "@CURRENCY_IN_USD", Convert.ToDecimal(drExplBeniReport["CURRENCY_IN_USD"].ToString()),
                        "@EXPENSES_NOT_COVERED", Convert.ToDecimal(drExplBeniReport["EXPENSES_NOT_COVERED"].ToString()),
                        "@DEDUCTIBLE_OR_COPAY", Convert.ToDecimal(drExplBeniReport["DEDUCTIBLE_OR_COPLAY"].ToString()),
                        "@OVER_LIMIT", Convert.ToDecimal(drExplBeniReport["OVER_LIMIT"].ToString()),
                        "@TOTAL_AMOUNT_COVER", Convert.ToDecimal(drExplBeniReport["TOTAL_AMT_COVERED"].ToString()),
                        "@CREATED_DATE", DateTime.Now,
                        "@CREATED_USER", frmLogIn.Usert.ToUpper()
                        );
                }

                for (int i = 0; i < dtNote.Rows.Count; i++)
                {
                    var drNote = dtNote.Rows[i];
                    decimal amount = Convert.ToDecimal(drNote["Amount"].ToString().Replace("USD", "").Trim());
                    sqlcrud.ExecuteMySql("dbo.SP_INSERT_SETTLEMENT_NOTICE_NOTE",
                        "@SETTLEMENT_NOTICE_ID", settlementNoticeID,
                        "@AMOUNT", amount,
                        "@DESCRIPTION", drNote["Description"].ToString(),
                        "@CREATED_DATE", DateTime.Now,
                        "@CREATED_USER", frmLogIn.Usert.ToUpper()
                        );
                }
            }
        }

        private float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }

        private void LoadData()
        {
            try
            {
                //SetDefaultValue();

                Cursor = Cursors.WaitCursor;

                var dtClaimInfo = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                "nvl(INT_DIAGNOSIS, nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A')) CAUSE, " +
                "INT_COMMENTS HOSPITAL, INT_OTH_HOSPITAL, (select COM_NAME from CL_M_ORGANIZATIONS where COM_CODE = INT_HOSPITAL_CODE) PANEL_HOSPITAL," +
                "TO_CHAR(INT_INITIMATE_DT) REGISTERATION_DATE, NVL(TO_CHAR(INT_DATE_ADMISSION), TO_CHAR(INT_DATE_LOSS)) TREATMENT_DATE, INT_DATE_DISCHARGE DISCHARGE_DATE," +
                "(select case when INT_BPARTY_CODE = 'U-BRK' then SFC_FIRST_NAME else SFC_FIRST_NAME || ' ' || SFC_SURNAME end from SM_M_SALES_FORCE where SFC_CODE = INT_BPARTY_CODE) CC, (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where  CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + claimNo + "'");

                if (dtClaimInfo == null || dtClaimInfo.Rows.Count <= 0)
                {
                    //SetDefaultValue();
                    //ClearTotalValue();
                    dgvNote.DataSource = null;
                    dgvExplBeni.DataSource = null;
                    Cursor = Cursors.Arrow;
                    return;
                }
                else
                {
                    var proCode = claimNo.Substring(6, 4).ToLower();

                    if (!isExist)
                    {
                        txtCC.Text = dtClaimInfo.Rows[0]["CC"].ToString();
                        txtPolicy.Text = GetPolicyType(proCode);
                        txtPolicyholder.Text = dtClaimInfo.Rows[0]["POLICY_HOLDER"].ToString();
                        txtAddress.Text = dtClaimInfo.Rows[0]["ADDRESS"].ToString();
                        txtMemberName.Text = dtClaimInfo.Rows[0]["MEMBER"].ToString();
                        txtPolicyNo.Text = dtClaimInfo.Rows[0]["POLICY_NO"].ToString();
                        dtpAdmissonDate.Value = Convert.ToDateTime(dtClaimInfo.Rows[0]["TREATMENT_DATE"].ToString());
                        dtpDischargeDate.Value = (!string.IsNullOrEmpty(dtClaimInfo.Rows[0]["DISCHARGE_DATE"].ToString()) ? Convert.ToDateTime(dtClaimInfo.Rows[0]["DISCHARGE_DATE"].ToString()) : DateTime.Now);
                        txtClaimNo.Text = dtClaimInfo.Rows[0]["CLAIM_NO"].ToString();
                        txtPlan.Text = GetPlan(proCode, dtClaimInfo.Rows[0]["PLAN_DESCRIPTION"].ToString());
                        dtpRegistrationDate.Value = Convert.ToDateTime(dtClaimInfo.Rows[0]["REGISTERATION_DATE"].ToString());
                        //txtPanelHospital.Text = GetPanelHospital(dtClaimInfo.Rows[0]["HOSPITAL"].ToString());
                        txtPanelHospital.Text = dtClaimInfo.Rows[0]["PANEL_HOSPITAL"].ToString();
                        txtOtherHospital.Text = dtClaimInfo.Rows[0]["INT_OTH_HOSPITAL"].ToString();
                        txtDiagnosis.Text = dtClaimInfo.Rows[0]["CAUSE"].ToString();
                        txtEmail.Text = GetEmailByProduct(proCode);
                    }
                    

                    BindToDgvExplBeni(proCode);
                }

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }

        }

        private void dgvExplBeni_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dgvExplBeni.Columns[e.ColumnIndex];
                if (column.Name == "ORIGINAL_CURRENCY")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["ORIGINAL_CURRENCY"].Value != null && row.Cells["ORIGINAL_CURRENCY"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["ORIGINAL_CURRENCY"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblOriCur.Text = sum.ToString("$0.00");
                }
                else if (column.Name == "CURRENCY_IN_USD")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["CURRENCY_IN_USD"].Value != null && row.Cells["CURRENCY_IN_USD"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["CURRENCY_IN_USD"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblCurInUsd.Text = sum.ToString("$0.00");
                }
                else if (column.Name == "EXPENSES_NOT_COVERED")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["EXPENSES_NOT_COVERED"].Value != null && row.Cells["EXPENSES_NOT_COVERED"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["EXPENSES_NOT_COVERED"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblExpNotCovered.Text = sum.ToString("$0.00");
                }
                else if (column.Name == "DEDUCTIBLE_OR_COPLAY")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["DEDUCTIBLE_OR_COPLAY"].Value != null && row.Cells["DEDUCTIBLE_OR_COPLAY"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["DEDUCTIBLE_OR_COPLAY"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblDeductible.Text = sum.ToString("$0.00");
                }
                else if (column.Name == "OVER_LIMIT")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["OVER_LIMIT"].Value != null && row.Cells["OVER_LIMIT"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["OVER_LIMIT"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblOverLimit.Text = sum.ToString("$0.00");
                }
                else if (column.Name == "TOTAL_AMT_COVERED")
                {
                    decimal sum = 0;
                    foreach (DataGridViewRow row in dgvExplBeni.Rows)
                    {
                        if (row.Cells["TOTAL_AMT_COVERED"].Value != null && row.Cells["TOTAL_AMT_COVERED"].Value.ToString() != "")
                        {
                            decimal valueDecimal = Convert.ToDecimal(row.Cells["TOTAL_AMT_COVERED"].Value);
                            sum += valueDecimal;
                        }
                    }
                    lblTotalAmtCovered.Text = sum.ToString("$0.00");
                }
            }
        }

        private void dgvExplBeni_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["SERVICE_PROVIDED"].Index)
            {
                stringInput = true;
            }
            else if (dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["ORIGINAL_CURRENCY"].Index
                || dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["CURRENCY_IN_USD"].Index
                || dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["EXPENSES_NOT_COVERED"].Index
                || dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["DEDUCTIBLE_OR_COPLAY"].Index
                || dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["OVER_LIMIT"].Index
                || dgvExplBeni.CurrentCell.ColumnIndex == dgvExplBeni.Columns["TOTAL_AMT_COVERED"].Index)
            {
                stringInput = false;
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = !stringInput;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = !stringInput;
            }
        }

        private string GetPolicyType(string proCode)
        {
            string polType = string.Empty;

            var dtPro = crud.ExecQuery("select prd_description from uw_m_products where prd_code like '%" + proCode.Substring(1).ToUpper() + "%'");
            if (dtPro.Rows.Count > 0)
            {
                polType = dtPro.Rows[0][0].ToString();
            }

            return polType;
        }

        private string GetPlan(string proCode, string plan)
        {
            if (proCode.Contains("hns"))
            {
                var pIndex = plan.IndexOf("PLAN");
                if (pIndex != -1)
                    plan = plan.Substring(pIndex + 5);
            }
            return plan;
        }

        private string GetPanelHospital(string hospital)
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

            return hospital;
        }

        private string GetEmailByProduct(string proCode)
        {
            string email = string.Empty;

            if (proCode.Contains("gpa"))
                email = "gpa@forteinsurance.com";
            else if (proCode.Contains("hns"))
                email = "hnsclaims@forteinsurance.com";
            else
                email = "figtree_blue@forteinsurance.com";

            return email;
        }

        private System.Data.DataTable GetExplBeni(string proCode)
        {
            var dtList = crud.ExecQuery("select * from user_settlement_letter_explbeni where product_code = '" + proCode + "'");
            return dtList;
        }

        private void BindToDgvExplBeni(string proCode)
        {
            dtExplBeni = GetExplBeni(proCode.Substring(1).ToUpper());

            if (isExist)
            {
                for (int i = 0; i < dtCurrentEob.Rows.Count; i++)
                {
                    var drCurrentEob = dtCurrentEob.Rows[i];
                    var defaultRows = dtExplBeni.Select("SERVICE_PROVIDED = '" + drCurrentEob["SERVICES_OR_TREATMENT_PROVIDED"] + "'");
                    if (defaultRows.Count() > 0)
                    {
                        DataRow defaultRow = dtExplBeni.Select("SERVICE_PROVIDED = '" + drCurrentEob["SERVICES_OR_TREATMENT_PROVIDED"] + "'")[0];
                        defaultRow["ORIGINAL_CURRENCY"] = drCurrentEob["ORIGINAL_CURRENCY"];
                        defaultRow["CURRENCY_IN_USD"] = drCurrentEob["CURRENCY_IN_USD"];
                        defaultRow["EXPENSES_NOT_COVERED"] = drCurrentEob["EXPENSES_NOT_COVERED"];
                        defaultRow["DEDUCTIBLE_OR_COPLAY"] = drCurrentEob["DEDUCTIBLE_OR_COPAY"];
                        defaultRow["OVER_LIMIT"] = drCurrentEob["OVER_LIMIT"];
                        defaultRow["TOTAL_AMT_COVERED"] = drCurrentEob["TOTAL_AMOUNT_COVER"];
                    }
                    else
                    {
                        var drExplBeni = dtExplBeni.NewRow();
                        drExplBeni["SERVICE_PROVIDED"] = drCurrentEob["SERVICES_OR_TREATMENT_PROVIDED"];
                        drExplBeni["ORIGINAL_CURRENCY"] = drCurrentEob["ORIGINAL_CURRENCY"];
                        drExplBeni["CURRENCY_IN_USD"] = drCurrentEob["CURRENCY_IN_USD"];
                        drExplBeni["EXPENSES_NOT_COVERED"] = drCurrentEob["EXPENSES_NOT_COVERED"];
                        drExplBeni["DEDUCTIBLE_OR_COPLAY"] = drCurrentEob["DEDUCTIBLE_OR_COPAY"];
                        drExplBeni["OVER_LIMIT"] = drCurrentEob["OVER_LIMIT"];
                        drExplBeni["TOTAL_AMT_COVERED"] = drCurrentEob["TOTAL_AMOUNT_COVER"];

                        dtExplBeni.Rows.Add(drExplBeni);
                    }

                }
                dgvExplBeni.DataSource = dtExplBeni;
            }
            else
                dgvExplBeni.DataSource = dtExplBeni;


            dgvExplBeni.Columns["PRODUCT_CODE"].Visible = false;

            dgvExplBeni.Columns["ORIGINAL_CURRENCY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["CURRENCY_IN_USD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["EXPENSES_NOT_COVERED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["DEDUCTIBLE_OR_COPLAY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["OVER_LIMIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["TOTAL_AMT_COVERED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["SERVICE_PROVIDED"].Width = 450;

            //ClearTotalValue();
        }

        private void GetClaimInfoReport()
        {
            dtClaimInfoReport = new System.Data.DataTable();

            dtClaimInfoReport.Columns.Add("ATTN", typeof(string));
            dtClaimInfoReport.Columns.Add("PAYMENT_BENE", typeof(string));
            dtClaimInfoReport.Columns.Add("CC", typeof(string));
            dtClaimInfoReport.Columns.Add("POLICY_HOLDER", typeof(string));
            dtClaimInfoReport.Columns.Add("ADDRESS", typeof(string));
            dtClaimInfoReport.Columns.Add("MEMBER_NAME", typeof(string));
            dtClaimInfoReport.Columns.Add("POLICY", typeof(string));
            dtClaimInfoReport.Columns.Add("POLICY_NO", typeof(string));
            dtClaimInfoReport.Columns.Add("ADMISSION_DATE", typeof(string));
            dtClaimInfoReport.Columns.Add("CLAIM_NO", typeof(string));
            dtClaimInfoReport.Columns.Add("DISCHARGE_DATE", typeof(string));
            dtClaimInfoReport.Columns.Add("PLAN", typeof(string));
            dtClaimInfoReport.Columns.Add("LENGTH_OF_STAY", typeof(string));
            dtClaimInfoReport.Columns.Add("REGISTRATION_DATE", typeof(string));
            dtClaimInfoReport.Columns.Add("ICU_STAY", typeof(string));
            dtClaimInfoReport.Columns.Add("PANEL_HOSPITAL", typeof(string));
            dtClaimInfoReport.Columns.Add("OTHER_HOSPITAL", typeof(string));
            dtClaimInfoReport.Columns.Add("DIAGNOSIS", typeof(string));
            dtClaimInfoReport.Columns.Add("EMAIL", typeof(string));
            dtClaimInfoReport.Columns.Add("EXCHANGE_RATE", typeof(string));

            var drClaimInfoReport = dtClaimInfoReport.NewRow();
            drClaimInfoReport["ATTN"] = txtAttn.Text.Trim();
            drClaimInfoReport["PAYMENT_BENE"] = txtPaymentBene.Text.Trim();
            drClaimInfoReport["CC"] = txtCC.Text.Trim();
            drClaimInfoReport["POLICY_HOLDER"] = txtPolicyholder.Text.Trim();
            drClaimInfoReport["ADDRESS"] = txtAddress.Text.Trim();
            drClaimInfoReport["MEMBER_NAME"] = txtMemberName.Text.Trim();
            drClaimInfoReport["POLICY"] = txtPolicy.Text.Trim();
            drClaimInfoReport["POLICY_NO"] = txtPolicyNo.Text.Trim();
            drClaimInfoReport["ADMISSION_DATE"] = dtpAdmissonDate.Value.ToString("dd/MM/yyyy");
            drClaimInfoReport["CLAIM_NO"] = txtClaimNo.Text.Trim();
            drClaimInfoReport["DISCHARGE_DATE"] = dtpDischargeDate.Value.ToString("dd/MM/yyyy");
            drClaimInfoReport["PLAN"] = txtPlan.Text.Trim();
            drClaimInfoReport["LENGTH_OF_STAY"] = txtLengthOfStay.Text.Trim();
            drClaimInfoReport["REGISTRATION_DATE"] = dtpRegistrationDate.Value.ToString("dd/MM/yyyy");
            drClaimInfoReport["ICU_STAY"] = txtICUStay.Text.Trim();
            drClaimInfoReport["PANEL_HOSPITAL"] = txtPanelHospital.Text.Trim();
            drClaimInfoReport["OTHER_HOSPITAL"] = txtOtherHospital.Text.Trim();
            drClaimInfoReport["DIAGNOSIS"] = txtDiagnosis.Text.Trim();
            drClaimInfoReport["EMAIL"] = txtEmail.Text.Trim();
            drClaimInfoReport["EXCHANGE_RATE"] = txtExchangeRate.Text.Trim();
            dtClaimInfoReport.Rows.Add(drClaimInfoReport);
        }

        private void GetNoteHtmlReport()
        {
            dtNoteReport = new System.Data.DataTable();
            dtNoteReport.Columns.Add("AMT");
            dtNoteReport.Columns.Add("DES");

            string amt = string.Empty;
            string des = string.Empty;
            for (int i = 0; i < dtNote.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dtNote.Rows[i]["Amount"].ToString().Trim()) || string.IsNullOrEmpty(dtNote.Rows[i]["Description"].ToString().Trim()))
                    continue;

                amt += "- " + dtNote.Rows[i]["Amount"].ToString().Trim() + "<br>";
                des += ": " + dtNote.Rows[i]["Description"].ToString().Trim() + "<br>";
            }

            if (!string.IsNullOrEmpty(amt))
                amt = amt.Remove(amt.Length - 4);

            if (!string.IsNullOrEmpty(des))
                des = des.Remove(des.Length - 4);

            if (!string.IsNullOrEmpty(amt) && !string.IsNullOrEmpty(des))
            {
                var drNoteReport = dtNoteReport.NewRow();
                drNoteReport["AMT"] = amt;
                drNoteReport["DES"] = des;

                dtNoteReport.Rows.Add(drNoteReport);
            }
        }

        private void GetExplBeniReport()
        {
            dtExplBeniReport = dtExplBeni.Clone();
            dtExplBeniReport.Columns.Remove("PRODUCT_CODE");

            for (int i = 0; i < dtExplBeni.Rows.Count; i++)
            {
                var serPro = dtExplBeni.Rows[i]["SERVICE_PROVIDED"].ToString().Trim();
                var oriCur = dtExplBeni.Rows[i]["ORIGINAL_CURRENCY"].ToString().Trim();
                var curUsd = dtExplBeni.Rows[i]["CURRENCY_IN_USD"].ToString().Trim();
                var expNotCovered = dtExplBeni.Rows[i]["EXPENSES_NOT_COVERED"].ToString().Trim();
                var deductible = dtExplBeni.Rows[i]["DEDUCTIBLE_OR_COPLAY"].ToString().Trim();
                var overLimit = dtExplBeni.Rows[i]["OVER_LIMIT"].ToString().Trim();
                var totalAmtCovered = dtExplBeni.Rows[i]["TOTAL_AMT_COVERED"].ToString().Trim();

                if (!string.IsNullOrEmpty(oriCur) || !string.IsNullOrEmpty(curUsd) || !string.IsNullOrEmpty(expNotCovered)
                    || !string.IsNullOrEmpty(deductible) || !string.IsNullOrEmpty(overLimit) || !string.IsNullOrEmpty(totalAmtCovered))
                {
                    var drExplBeniReport = dtExplBeniReport.NewRow();

                    drExplBeniReport["SERVICE_PROVIDED"] = serPro;

                    decimal deOriCur;
                    if (Decimal.TryParse(oriCur, out deOriCur))
                        drExplBeniReport["ORIGINAL_CURRENCY"] = Convert.ToDecimal(oriCur);
                    else
                        drExplBeniReport["ORIGINAL_CURRENCY"] = 0.00;

                    decimal deCurUsd;
                    if (Decimal.TryParse(curUsd, out deCurUsd))
                        drExplBeniReport["CURRENCY_IN_USD"] = Convert.ToDecimal(curUsd);
                    else
                        drExplBeniReport["CURRENCY_IN_USD"] = 0.00;

                    decimal deExpNotCovered;
                    if (Decimal.TryParse(expNotCovered, out deExpNotCovered))
                        drExplBeniReport["EXPENSES_NOT_COVERED"] = Convert.ToDecimal(expNotCovered);
                    else
                        drExplBeniReport["EXPENSES_NOT_COVERED"] = 0.00;

                    decimal deDeductible;
                    if (Decimal.TryParse(deductible, out deDeductible))
                        drExplBeniReport["DEDUCTIBLE_OR_COPLAY"] = Convert.ToDecimal(deductible);
                    else
                        drExplBeniReport["DEDUCTIBLE_OR_COPLAY"] = 0.00;

                    decimal deOverLimit;
                    if (Decimal.TryParse(overLimit, out deOverLimit))
                        drExplBeniReport["OVER_LIMIT"] = Convert.ToDecimal(overLimit);
                    else
                        drExplBeniReport["OVER_LIMIT"] = 0.00;

                    decimal deTotalAmtCovered;
                    if (Decimal.TryParse(totalAmtCovered, out deTotalAmtCovered))
                        drExplBeniReport["TOTAL_AMT_COVERED"] = Convert.ToDecimal(totalAmtCovered);
                    else
                        drExplBeniReport["TOTAL_AMT_COVERED"] = 0.00;

                    dtExplBeniReport.Rows.Add(drExplBeniReport);
                }
            }
        }

        //private void ClearTotalValue()
        //{
        //    lblOriCur.Text = "$0.00";
        //    lblCurInUsd.Text = "$0.00";
        //    lblExpNotCovered.Text = "$0.00";
        //    lblDeductible.Text = "$0.00";
        //    lblOverLimit.Text = "$0.00";
        //    lblTotalAmtCovered.Text = "$0.00";
        //}

        //private void SetDefaultValue()
        //{
        //    dtNote.Clear();

        //    foreach (Control control in gbClaimIno.Controls)
        //    {
        //        if (control is TextBox)
        //        {
        //            if (((TextBox)control).Name.Equals("txtClaimNoSearch"))
        //            {
        //                ///ActiveControl = txtClaimNoSearch;
        //                continue;
        //            }
        //            ((TextBox)control).Text = string.Empty;
        //        }
        //        if (control is DateTimePicker)
        //        {
        //            ((DateTimePicker)control).Value = DateTime.Now;
        //        }
        //    }
        //}
    }
}
