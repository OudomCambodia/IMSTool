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
using System.Runtime.InteropServices;
using System.Diagnostics;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Testing.Forms
{
    public partial class frmMedicalRejectionLetter : Form
    {
        #region --- METHODS AND CONSTANTS FOR EMBEDDING WINDOWS ---
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
           CharSet = CharSet.Unicode, ExactSpelling = true,
           CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hwnd, uint Msg, int wParam, int lParam);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CLOSE = 0x10;
        private const int WS_CHILD = 0x40000000;
        private const int WS_MAXIMIZE = 0x01000000;
        #endregion

        #region --- VARIABLES ---
        private IntPtr gpsHandle;
        private ProcessStartInfo gpsPSI = new ProcessStartInfo();
        #endregion

        private CRUD crud = new CRUD();
        private DBS11SqlCrud sqlCrud = new DBS11SqlCrud();

        private DataTable dtClaims = frmSendEmailClaim.dtClaimDt;
        private DataTable dtSelectedRows = frmSendEmailClaim.selectedDoc;

        private string bodyengBody = string.Empty;
        private string khBody = string.Empty;
        private string engBody = string.Empty;
        private string otherExclusion = frmSendEmailClaim.OtherExclusion;
        private string HistClaimNo;
        private const string NA = "--- N/A ---";
        private string path = @"\\192.168.110.234\Infoins_IMS_Upload_doc$\Medical_Rejection_Letter_Doc\";
        private string ClaimNo = string.Empty;

        private bool IsViewHistory;

        private TempFileCollection tempfile = new TempFileCollection();

        private Microsoft.Office.Interop.Word._Document khDoc;
        private Microsoft.Office.Interop.Word._Document engDoc;



        public frmMedicalRejectionLetter(bool isViewHistory, string histClaimNo = null, string claimNo = null)
        {
            InitializeComponent();
            IsViewHistory = isViewHistory;
            HistClaimNo = histClaimNo;
            ClaimNo = claimNo;
        }

        private void frmMedicalRejectionLetter_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Text = IsViewHistory ? "Medical Rejection Letter History" : "Medical Rejection Letter";
                btnSave.Visible = !IsViewHistory;

                if (IsViewHistory)
                    LoadReferenceDocumentHistory();
                else
                    LoadReferenceDocument();

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var claimNoToSave = frmSendEmailClaim.dtClaimDt.Rows[0]["CLAIM_NO"].ToString().Trim();

            var isAlreadySaved = crud.ExecQuery("select CLAIM_NO from USER_MEDICAL_REJECTION_LETTER where CLAIM_NO = '" + claimNoToSave + "'").Rows.Count > 0;
            if (isAlreadySaved)
            {
                Msgbox.Show("This Claim No " + claimNoToSave + " is already saved. Please check again!");
                BringToFront();
                return;
            }

            var confirmSave = Msgbox.Show("Are you sure you want to save this Claim Rejection?", "Save Information");
            if (confirmSave == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    var claimNo = frmSendEmailClaim.dtClaimDt.Rows[0]["CLAIM_NO"].ToString().Trim();
                    claimNo = frmSendEmailClaim.dtClaimDt.Rows[0]["CLAIM_NO"].ToString().Trim().Replace("/", "-");
                    var referenceDoc = string.Concat(claimNo, "-KH.docx", "*", claimNo, "-ENG.docx");

                    var insertBuilder = new StringBuilder();
                    insertBuilder.Append("insert into USER_MEDICAL_REJECTION_LETTER(SEQ_NO, CLAIM_NO, REJECTION_DATE, REJECTION_REFERENCE_DOC) ")
                        .Append("values(USER_MEDICAL_REJECTION_SEQ.NEXTVAL, '" + claimNoToSave + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + referenceDoc + "')");
                    crud.ExecNonQuery(insertBuilder.ToString());

                    System.IO.Directory.CreateDirectory(path + claimNo);

                    path = path + claimNo;

                    khDoc.SaveAs2(path + @"\" + claimNo + "-KH.docx");
                    engDoc.SaveAs2(path + @"\" + claimNo + "-ENG.docx");

                    Cursor = Cursors.Arrow;

                    Msgbox.Show("Claim Rejection successfully saved.");
                    Close();
                }
                catch (Exception ex)
                {
                    crud.ExecNonQuery("delete from USER_MEDICAL_REJECTION_LETTER where CLAIM_NO = '" + claimNoToSave + "'");
                    Cursor = Cursors.Arrow;
                    Msgbox.Show(ex.ToString());
                }
            }
        }

        private void frmMedicalRejectionLetter_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] processList = Process.GetProcesses().Where(x => x.ProcessName.ToLower() == "winword").ToArray();
            foreach (var process in processList)
            {
                if (process.MainWindowHandle == (IntPtr)0x00000000)
                    process.Kill();
            }
        }

        private void LoadReferenceDocumentHistory()
        {
            if (string.IsNullOrEmpty(HistClaimNo))
                return;

            var dsReferenceDoc = crud.ExecQuery("select REJECTION_REFERENCE_DOC from USER_MEDICAL_REJECTION_LETTER where CLAIM_NO = '" + HistClaimNo + "'");
            if (dsReferenceDoc.Rows.Count <= 0)
                return;

            string[] khEngDoc = dsReferenceDoc.Rows[0]["REJECTION_REFERENCE_DOC"].ToString().Split('*');

            SetReferenceDocumentHistory(khEngDoc[0], splitContainer1.Panel1, true);
            SetReferenceDocumentHistory(khEngDoc[1], splitContainer1.Panel2, false);
        }

        private void SetReferenceDocumentHistory(string khEngDoc, Control panel, bool isKhDoc)
        {
            try
            {
                var fileInfo = new FileInfo(path + HistClaimNo.Replace(@"/", "-") + @"\" + khEngDoc);

                if (!fileInfo.Exists)
                    return;

                Microsoft.Office.Interop.Word._Application oWord = new Application();
                object oMissing = System.Reflection.Missing.Value;
                object oTrue = true;
                object oFalse = false;

                object missing = System.Reflection.Missing.Value;
                object visible = true;

                Microsoft.Office.Interop.Word._Document doc = oWord.Documents.Add(ref missing, ref missing, ref missing, ref visible);

                var bookMark = doc.Words.First.Bookmarks.Add("entry");
                bookMark.Range.InsertFile(fileInfo.FullName);

                doc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                doc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
                doc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
                doc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
                doc.PageSetup.TopMargin = InchesToPoints(isKhDoc ? 0.6f : 0.85f);
                doc.PageSetup.LeftMargin = InchesToPoints(0.6f);
                doc.PageSetup.RightMargin = InchesToPoints(0.6f);
                doc.PageSetup.BottomMargin = InchesToPoints(0.5f);
                doc.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyFormFields);

                oWord.Visible = true;
                oWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;
                oWord.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;

                gpsHandle = (IntPtr)oWord.ActiveWindow.Hwnd;
                SetParent(gpsHandle, panel.Handle);
                SetWindowLong(gpsHandle, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE);
                MoveWindow(gpsHandle, 0, 0, panel.Width, panel.Height, true);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void LoadReferenceDocument()
        {
            if (otherExclusion.Equals(NA))
            {
                DataTable dtClaimTemplateEng = crud.ExecQuery("select * from USER_REJECTLETTER_TEMP where PRODUCTS = 'HNS' and LANGUAGE = 'ENG'");
                if (dtClaimTemplateEng.Rows.Count > 0)
                    engBody = dtClaimTemplateEng.Rows[0]["TEMPLATE"].ToString();

                DataTable dtClaimTemplateKh = crud.ExecQuery("select * from USER_REJECTLETTER_TEMP where PRODUCTS = 'HNS' and LANGUAGE = 'KH'");
                if (dtClaimTemplateEng.Rows.Count > 0)
                    khBody = dtClaimTemplateKh.Rows[0]["TEMPLATE"].ToString();

                if (dtClaims.Rows.Count > 0)
                {
                    var eng = dtSelectedRows.Rows[0]["ENG"].ToString();
                    var kh = dtSelectedRows.Rows[0]["KH"].ToString();
                    var part = dtSelectedRows.Rows[0]["PARTS"].ToString();
                    var type = dtSelectedRows.Rows[0]["TYPE"].ToString();
                    var partKh = dtSelectedRows.Rows[0]["PARTS_KH"].ToString();
                    var typeKh = dtSelectedRows.Rows[0]["TYPE_KH"].ToString();

                    var currentDate = DateTime.Now.ToString("dd MMMM yyyy");
                    var policyHolder = dtClaims.Rows[0]["POLICY_HOLDER"].ToString();
                    var situationOfRisk = dtClaims.Rows[0]["ADDRESS"].ToString();
                    var policyNo = dtClaims.Rows[0]["POLICY_NO"].ToString();
                    var claimNo = dtClaims.Rows[0]["CLAIM_NO"].ToString();
                    var insuredMember = dtClaims.Rows[0]["MEMBER"].ToString();
                    var claimAmount = dtClaims.Rows[0]["CLAIMED_AMOUNT"].ToString();
                    var claimCause = dtClaims.Rows[0]["CAUSE"].ToString();
                    var hospital = dtClaims.Rows[0]["HOSPITAL"].ToString();

                    string[] dateTimeformats = { "dd/MM/yy", "dd/MM/yyyy", "dd-MMM-yy" };
                    string treatmentDateString = dtClaims.Rows[0]["TREATMENT_DATE"].ToString();
                    string treatmentDate = string.Empty;

                    DateTime dtTreatmentDate;
                    if (DateTime.TryParseExact(treatmentDateString, dateTimeformats, new CultureInfo("en-US"), DateTimeStyles.None, out dtTreatmentDate))
                        treatmentDate = Convert.ToDateTime(dtClaims.Rows[0]["TREATMENT_DATE"]).ToString("dd MMMM yyyy");
                    else
                        treatmentDate = dtClaims.Rows[0]["TREATMENT_DATE"].ToString();

                    var defExclu = string.Empty;
                    var khDefExclu = string.Empty;

                    if (dtSelectedRows.Rows.Count > 0)
                    {
                        if (dtSelectedRows.Rows.Count == 1)
                        {
                            defExclu = string.Concat(part, " --- ", type, " ", eng);
                            khDefExclu = string.Concat(partKh, " --- ", typeKh, " ", kh);
                        }
                        else
                        {
                            for (int i = 0; i < dtSelectedRows.Rows.Count; i++)
                            {
                                eng = dtSelectedRows.Rows[i]["ENG"].ToString();
                                kh = dtSelectedRows.Rows[i]["KH"].ToString();
                                part = dtSelectedRows.Rows[i]["PARTS"].ToString();
                                type = dtSelectedRows.Rows[i]["TYPE"].ToString();
                                partKh = dtSelectedRows.Rows[i]["PARTS_KH"].ToString();
                                typeKh = dtSelectedRows.Rows[i]["TYPE_KH"].ToString();

                                defExclu += string.Concat("&#8226;", "  ", part, " --- ", type, " ", eng);
                                khDefExclu += string.Concat("&#8226;", "  ", partKh, " --- ", typeKh, " ", kh);
                            }
                        }

                    }
                    var accountHandler = dtClaims.Rows[0]["CC"].ToString();

                    engBody = engBody.Replace("%DateTimeN%", currentDate);
                    engBody = engBody.Replace("%PolicyHolder%", policyHolder);
                    engBody = engBody.Replace("%SituationOfRisk%", situationOfRisk);
                    engBody = engBody.Replace("%Policy No%", policyNo);
                    engBody = engBody.Replace("%ClaimNo%", claimNo);
                    engBody = engBody.Replace("%InsuredMember%", insuredMember);
                    engBody = engBody.Replace("%ClaimAmount%", claimAmount);
                    engBody = engBody.Replace("%ClaimCause%", claimCause);
                    engBody = engBody.Replace("%Hospital%", hospital);
                    engBody = engBody.Replace("%Dateloss%", treatmentDate);
                    engBody = engBody.Replace("%DefExclus%", defExclu);
                    engBody = engBody.Replace("%AccountHandler%", accountHandler);

                    khBody = khBody.Replace("%DateTimeN%", CommonFunctions.KhDate(DateTime.Now));
                    khBody = khBody.Replace("%PolicyHolder%", policyHolder);
                    khBody = khBody.Replace("%SituationOfRisk%", situationOfRisk);
                    khBody = khBody.Replace("%Policy No%", policyNo);
                    khBody = khBody.Replace("%ClaimNo%", claimNo);
                    khBody = khBody.Replace("%InsuredMember%", insuredMember);
                    khBody = khBody.Replace("%ClaimAmount%", CommonFunctions.KhNum(Convert.ToDouble(claimAmount)));
                    khBody = khBody.Replace("%ClaimCause%", claimCause);
                    khBody = khBody.Replace("%Hospital%", hospital);

                    if (DateTime.TryParseExact(treatmentDateString, dateTimeformats, new CultureInfo("en-US"), DateTimeStyles.None, out dtTreatmentDate))
                        treatmentDate = CommonFunctions.KhDate(Convert.ToDateTime(treatmentDate));

                    khBody = khBody.Replace("%Dateloss%", treatmentDate);
                    khBody = khBody.Replace("%DefExclus%", khDefExclu);
                    khBody = khBody.Replace("%AccountHandler%", accountHandler);

                    string partTypeReplace = string.Concat(part, " --- ", type);
                    string partTypeKhReplace = string.Concat(partKh, " --- ", typeKh);
                    engBody = engBody.Replace(partTypeReplace, string.Format("<strong>{0}</strong>", partTypeReplace));
                    khBody = khBody.Replace(partTypeKhReplace, string.Format("<strong>{0}</strong>", partTypeKhReplace));

                    if (dtSelectedRows.Rows.Count > 1)
                    {
                        engBody = engBody.Replace("By virtue of the Group Hospital and Surgical Insurance Policy at", "By virtue of the Group Hospital and Surgical Insurance Policy at:");
                        for (int i = 0; i < dtSelectedRows.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                engBody = engBody.Replace("&#8226;", "<p style=\"margin: 0pt 0pt 0pt 15pt; font-family: Calibri;\"> <br> &#8226;");
                                khBody = khBody.Replace("&#8226;", "<br><br/> &#8226;");
                            }

                            if (i == dtSelectedRows.Rows.Count)
                                engBody = engBody.Replace("we regret to inform you that this claim is not covered.</p>", "we regret to inform you that this claim is not covered.</p></p>");
                        }
                    }
                    engBody = engBody.Replace("<td><strong>CLAIM REJECTION</strong></td>", "<td style=\"width: 150px;\"><strong>CLAIM REJECTION</strong></td>");
                    engBody = engBody.Replace("“", "&ldquo;").Replace("”", "&rdquo;");
                    engBody = engBody.Replace("‘", "&lsquo;").Replace("’", "&rsquo;");

                    khBody = khBody.Replace("“", "&ldquo;").Replace("”", "&rdquo;");
                    khBody = khBody.Replace("‘", "&lsquo;").Replace("’", "&rsquo;");

                    SetReferenceDocument(khBody, splitContainer1.Panel1, true);
                    SetReferenceDocument(engBody, splitContainer1.Panel2, false);
                }
            }
            else
            {
                DataTable dtClaimTemplate = crud.ExecQuery("select TEMPLATE, LANGUAGE from USER_REJECTLETTER_TEMP where PRODUCTS = '" + otherExclusion + "'");
                if (dtClaimTemplate.Rows.Count > 0)
                {
                    engBody = dtClaimTemplate.AsEnumerable().Where(dr => dr.Field<string>("LANGUAGE").Equals("ENG")).FirstOrDefault()[0].ToString();
                    khBody = dtClaimTemplate.AsEnumerable().Where(dr => dr.Field<string>("LANGUAGE").Equals("KH")).FirstOrDefault()[0].ToString();

                    var beniAmt = string.Empty;

                    if (otherExclusion == "HNS OUTPATIENT REACH LIMIT")
                    {
                        var qBuilder = new StringBuilder();
                        qBuilder.Append("select pci_char_value ")
                            .Append("from uw_t_pol_common_information ")
                            .Append("where pci_pol_seq_no = (select pol_seq_no from uw_t_policies ")
                            .AppendFormat("where pol_policy_no = (select int_policy_no from cl_t_intimation where int_claim_no = '{0}') and pol_status in (4, 5, 6, 10) and rownum = 1)", ClaimNo.ToUpper())
                            .Append("and pci_description = 'MEMORANDUM'");

                        var dtText = crud.ExecQuery(qBuilder.ToString());

                        if (dtText != null && dtText.Rows.Count > 0)
                        {
                            var text = dtText.Rows[0][0].ToString();
                            var oIndex = text.IndexOf("OPTIONAL OUT- PATIENT CARE");
                            var uIndex = text.IndexOf("USD");
                            var strBeni = text.Substring(uIndex, 15);
                            var strBeniAmt = strBeni.Split('.')[0];
                            beniAmt = strBeniAmt.Substring(strBeniAmt.IndexOf("D") + 1).Trim();

                            decimal num = 0;
                            var isNum = decimal.TryParse(beniAmt, out num);

                            beniAmt = isNum ? beniAmt : Regex.Match(beniAmt, @"(\d+(\.\d+)?)|(\.\d+)").Value;
                        }
                    }

                    var currentDate = DateTime.Now.ToString("dd MMMM yyyy");
                    var policyHolder = dtClaims.Rows[0]["POLICY_HOLDER"].ToString();
                    var situationOfRisk = dtClaims.Rows[0]["ADDRESS"].ToString();
                    var policyNo = dtClaims.Rows[0]["POLICY_NO"].ToString();
                    var claimNo = dtClaims.Rows[0]["CLAIM_NO"].ToString();
                    var insuredMember = dtClaims.Rows[0]["MEMBER"].ToString();
                    var claimAmount = dtClaims.Rows[0]["CLAIMED_AMOUNT"].ToString();
                    var claimCause = dtClaims.Rows[0]["CAUSE"].ToString();
                    var hospital = dtClaims.Rows[0]["HOSPITAL"].ToString();

                    if (!string.IsNullOrEmpty(hospital))
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

                    string[] dateTimeformats = { "dd/MM/yy", "dd/MM/yyyy", "dd-MMM-yy" };
                    string treatmentDateString = dtClaims.Rows[0]["TREATMENT_DATE"].ToString();
                    string treatmentDate = string.Empty;

                    DateTime dtTreatmentDate;
                    if (DateTime.TryParseExact(treatmentDateString, dateTimeformats, new CultureInfo("en-US"), DateTimeStyles.None, out dtTreatmentDate))
                        treatmentDate = Convert.ToDateTime(dtClaims.Rows[0]["TREATMENT_DATE"]).ToString("dd MMMM yyyy");
                    else
                        treatmentDate = dtClaims.Rows[0]["TREATMENT_DATE"].ToString();

                    var accountHandler = dtClaims.Rows[0]["CC"].ToString();

                    engBody = engBody.Replace("%DateTimeN%", currentDate);
                    engBody = engBody.Replace("%PolicyHolder%", policyHolder);
                    engBody = engBody.Replace("%SituationOfRisk%", situationOfRisk);
                    engBody = engBody.Replace("%Policy No%", policyNo);
                    engBody = engBody.Replace("%ClaimNo%", claimNo);
                    engBody = engBody.Replace("%InsuredMember%", insuredMember);
                    engBody = engBody.Replace("%ClaimAmount%", claimAmount);
                    engBody = engBody.Replace("%ClaimCause%", claimCause);
                    engBody = engBody.Replace("%Hospital%", hospital);
                    engBody = engBody.Replace("%Dateloss%", treatmentDate);
                    engBody = engBody.Replace("%AccountHandler%", accountHandler);
                    if (otherExclusion == "HNS OUTPATIENT REACH LIMIT")
                    {
                        engBody = engBody.Replace("%BeniAmt%", beniAmt);
                    }

                    khBody = khBody.Replace("%DateTimeN%", CommonFunctions.KhDate(DateTime.Now));
                    khBody = khBody.Replace("%PolicyHolder%", policyHolder);
                    khBody = khBody.Replace("%SituationOfRisk%", situationOfRisk);
                    khBody = khBody.Replace("%Policy No%", policyNo);
                    khBody = khBody.Replace("%ClaimNo%", claimNo);
                    khBody = khBody.Replace("%InsuredMember%", insuredMember);
                    khBody = khBody.Replace("%ClaimAmount%", CommonFunctions.KhNum(Convert.ToDouble(claimAmount)));
                    khBody = khBody.Replace("%ClaimCause%", claimCause);
                    khBody = khBody.Replace("%Hospital%", hospital);
                    if (otherExclusion == "HNS OUTPATIENT REACH LIMIT")
                    {
                        khBody = khBody.Replace("%BeniAmt%", CommonFunctions.KhNum(Convert.ToDouble(beniAmt)));
                    }

                    if (DateTime.TryParseExact(treatmentDateString, dateTimeformats, new CultureInfo("en-US"), DateTimeStyles.None, out dtTreatmentDate))
                        treatmentDate = CommonFunctions.KhDate(Convert.ToDateTime(treatmentDate));

                    khBody = khBody.Replace("%Dateloss%", treatmentDate);
                    khBody = khBody.Replace("%AccountHandler%", accountHandler);

                    SetReferenceDocument(khBody, splitContainer1.Panel1, true);
                    SetReferenceDocument(engBody, splitContainer1.Panel2, false);
                }
            }
        }

        private void SetReferenceDocument(string htmlText, Control panel, bool isKhmer)
        {
            try
            {
                string filePath = "";

                tempfile = new TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                tempfile.KeepFiles = false; //will be used when dispose tempfile
                filePath = tempfile.AddExtension("txt"); //add extension to the created Temporary File

                System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath); //open the file for writing.
                writer.Write(htmlText); //write text to the file
                writer.Close(); //remember to close the file again.
                writer.Dispose();

                var fileInfo = new FileInfo(filePath);

                Microsoft.Office.Interop.Word._Application oWord = new Application();
                Microsoft.Office.Interop.Word._Document oWordDoc = new Document();
                object oMissing = System.Reflection.Missing.Value;
                object oTrue = true;
                object oFalse = false;

                object missing = System.Reflection.Missing.Value;
                object visible = true;

                if (isKhmer)
                {
                    khDoc = oWord.Documents.Add(ref missing, ref missing, ref missing, ref visible);

                    var bookMark = khDoc.Words.First.Bookmarks.Add("entry");
                    bookMark.Range.InsertFile(fileInfo.FullName);

                    khDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                    khDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
                    khDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
                    khDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
                    khDoc.PageSetup.TopMargin = InchesToPoints(isKhmer ? 1.1f : 1.25f);
                    khDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
                    khDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
                    khDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);

                    if (otherExclusion.Equals("HNS MATERNITY"))
                    {
                        khDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
                    }
                    if (otherExclusion.Equals("HNS CABINET") || otherExclusion.Equals("HNS MEDICAL CHECK-UP REACH LIMIT")
                        || otherExclusion.Equals("HNS OUTPATIENT") || otherExclusion.Equals("HNS OUTPATIENT REACH LIMIT"))
                    {
                        khDoc.Paragraphs.LineSpacing = InchesToPoints(0.17f);
                    }

                }
                else
                {
                    engDoc = oWord.Documents.Add(ref missing, ref missing, ref missing, ref visible);

                    var bookMark = engDoc.Words.First.Bookmarks.Add("entry");
                    bookMark.Range.InsertFile(fileInfo.FullName);

                    engDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                    engDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
                    engDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
                    engDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
                    engDoc.PageSetup.TopMargin = InchesToPoints(isKhmer ? 1.1f : 1.25f);
                    engDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
                    engDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
                    engDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);
                    engDoc.Range().Font.Size = 11.0f;

                    if (otherExclusion.Equals("HNS MATERNITY"))
                    {
                        engDoc.Paragraphs.LineSpacing = InchesToPoints(0.123f);
                        engDoc.Range().Font.Size = 10.5f;
                    }
                    if (otherExclusion.Equals("HNS CABINET") || otherExclusion.Equals("HNS MEDICAL CHECK-UP REACH LIMIT")
                        || otherExclusion.Equals("HNS OUTPATIENT") || otherExclusion.Equals("HNS OUTPATIENT REACH LIMIT"))
                    {
                        engDoc.Paragraphs.LineSpacing = InchesToPoints(0.17f);
                    }
                }

                oWord.Visible = true;
                oWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;
                oWord.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;

                gpsHandle = (IntPtr)oWord.ActiveWindow.Hwnd;
                SetParent(gpsHandle, panel.Handle);
                SetWindowLong(gpsHandle, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE);
                MoveWindow(gpsHandle, 0, 0, panel.Width, panel.Height, true);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }

        #region --- OLD CODING ---
        private void txtKhClientCompany_TextChanged(object sender, EventArgs e)
        {
            //khBody = khBody.Replace(policyHolder, txtKhClientCompany.Text);
            //policyHolder = txtKhClientCompany.Text;
            //wbKhLetter.DocumentText = khBody;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //wbEngLetter.Document.ExecCommand("SelectAll", false, null);
            //wbEngLetter.Document.ExecCommand("Copy", false, null);
            //txtEngLetter.Paste();

            //wbKhLetter.Document.ExecCommand("SelectAll", false, null);
            //wbKhLetter.Document.ExecCommand("Copy", false, null);
            //txtKhLetter.Paste();

            //splitContainer1.Enabled = true;
        }

        private void txtEngLetter_TextChanged(object sender, EventArgs e)
        {
            //rtfToHtml.OutputFormat = RtfToHtml.eOutputFormat.HTML_401;

            //string engHtml = rtfToHtml.ConvertString(txtEngLetter.Rtf);

            //engHtml = engHtml.Replace("Times New Roman", "Calibri");
            //engHtml = engHtml.Replace("Microsoft Sans Serif", "Calibri");

            //string policyHolderReplace = string.Format("<p style=\"margin:0pt 0pt 0pt 0pt;\"><span class=\"st2\">{0}</span></p></td>", policyHolder.Replace("&", "&amp;"));
            //engHtml = engHtml.Replace(policyHolderReplace, string.Format("<p style=\"margin:0pt 0pt 0pt 10pt;\"><span class=\"st2\">{0}</span></p></td>", policyHolder));

            //string situationOfRiskReplace = string.Format("<p style=\"margin:0pt 0pt 0pt 0pt;\"><span class=\"st1\">{0}</span></p></td>", situationOfRisk.Replace("'", "&#8217;"));
            //engHtml = engHtml.Replace(situationOfRiskReplace, string.Format("<p style=\"margin:0pt 0pt 0pt 10pt;\"><span class=\"st1\">{0}</span></p></td>", situationOfRisk.Replace("'", "&#8217;")));

            //engHtml = engHtml.Replace("<p style=\"margin:5pt 0pt 5pt 0pt;\"><span class=\"st1\">&nbsp;</span></p>", "<p style=\"margin:0pt 0pt 0pt 0pt;\"><span class=\"st1\">&nbsp;</span></p>");

            //if (engHtml.Contains("82.55pt;"))
            //{
            //    engHtml = engHtml.Replace("<td valign=\"top\" width=\"25\" style=\"width:19pt;border:none;padding:0pt 0pt 0pt 0pt;\">", "<td valign=\"top\" width=\"25\" style=\"width:26.5pt;border:none;padding:0pt 0pt 0pt 0pt;\">");
            //    engHtml = engHtml.Replace("<td valign=\"top\" width=\"110\" style=\"width:82.55pt;border:none;padding:0pt 0pt 0pt 0pt;\">", "<td valign=\"top\" width=\"110\" style=\"width:100pt;border:none;padding:0pt 0pt 0pt 0pt;\">");
            //}

            //string partTypeReplace = string.Concat(part, " --- ", type);
            //engHtml = engHtml.Replace(partTypeReplace, string.Format("<strong>{0}</strong>", partTypeReplace));

            //if (dtSelectedRows.Rows.Count > 1)
            //{
            //    engHtml = engHtml.Replace("Group Hospital and Surgical Insurance Policy at", "Group Hospital and Surgical Insurance Policy at:");

            //    // add bullets dot
            //    engHtml = engHtml.Replace("&middot;", "&#8226;");

            //    for (int i = 0; i < dtSelectedRows.Rows.Count; i++)
            //    {
            //        if (i == 0)
            //            engHtml = engHtml.Replace("&#8226;", "<p style=\"margin: 0pt 0pt 0pt 15pt; font-family: Calibri;\"> <br/> &#8226;");

            //        if (i == dtSelectedRows.Rows.Count)
            //            engHtml = engHtml.Replace("we regret to inform you that </span><span class=\"st3\">this claim is not covered.</span></p>", "we regret to inform you that </span><span class=\"st3\">this claim is not covered.</span></p></p>");
            //    }
            //}

            //string urlReplace = "\"https://www.sautinsoft.com/products/rtf-to-html/order.php\"";
            //string licenseReplace = string.Format("<div style=\"text-align:center;\">The unlicensed version of &laquo;RTF to HTML .Net&raquo;.<br><a href={0}>Get the full featured version!</a></div>", urlReplace);
            //engHtml = engHtml.Replace(licenseReplace, "");

            //this.wbEngLetter.DocumentText = engHtml;
        }
        #endregion
    }
}
