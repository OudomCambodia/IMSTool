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
using Range = Microsoft.Office.Interop.Word.Range;

namespace Testing.Forms
{
    public partial class frmEmailNoticeAttachmentEdit : Form
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
        private DataTable dtSelectedRows = frmEmailNoticeAttachment.selectedDoc;
        private DataTable dtClaims = frmEmailNoticeAttachment.dtClaimDt;
        private string khBody = string.Empty;
        private string engBody = string.Empty;
        //private string path = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\Email_Claim_Rejection_Notice\";
        //private string path = @"D:\Email_Claim_Rejection_Notice\";

        public static string FolderPath = string.Empty;
        public static string KhPath = string.Empty;
        public static string EngPath = string.Empty;
        public bool IsSaveSuccess = false;

        private TempFileCollection tempfile = new TempFileCollection();

        private Microsoft.Office.Interop.Word._Application khWord = new Application();
        private Microsoft.Office.Interop.Word._Document khDoc;

        private Microsoft.Office.Interop.Word._Application engWord = new Application();
        private Microsoft.Office.Interop.Word._Document engDoc;

        public frmEmailNoticeAttachmentEdit()
        {
            InitializeComponent();
        }

        private void frmEmailNoticeAttachmentEdit_Load(object sender, EventArgs e)
        {
            string path = System.Windows.Forms.Application.StartupPath + @"\";
            khDoc = khWord.Documents.Open(path + @"Html\AH-Claim-Rej-Notice-Kh.docx");
            engDoc = engWord.Documents.Open(path + @"Html\AH-Claim-Rej-Notice-Eng.docx");

            LoadReferenceDocument();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            var confirmSave = Msgbox.Show("Are you sure you want to save this Claim Rejection Notice?", "Save Information");
            if (confirmSave == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string path = Path.GetTempPath();

                    var claimNo = frmEmailNoticeAttachment.dtClaimDt.Rows[0]["CLAIM_NO"].ToString().Trim();
                    claimNo = frmEmailNoticeAttachment.dtClaimDt.Rows[0]["CLAIM_NO"].ToString().Trim().Replace("/", "-");
                    System.IO.Directory.CreateDirectory(path + claimNo);

                    //path = path + claimNo;

                    FolderPath = path;

                    //khDoc.SaveAs2(path + @"\" + claimNo + "-KH.docx");
                    //engDoc.SaveAs2(path + @"\" + claimNo + "-ENG.docx");

                    khDoc.ExportAsFixedFormat(path + @"\" + claimNo + "-KH.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                    engDoc.ExportAsFixedFormat(path + @"\" + claimNo + "-ENG.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

                    Cursor = Cursors.Arrow;

                    Msgbox.Show("Claim Rejection Notice successfully saved.");

                    KhPath = path + @"\" + claimNo + "-KH.pdf";
                    EngPath = path + @"\" + claimNo + "-ENG.pdf";

                    IsSaveSuccess = true;

                    Close();
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Arrow;
                    Msgbox.Show(ex.ToString());
                }
            }
        }

        private void LoadReferenceDocument()
        {
            DataTable dtClaimTemplateEng = crud.ExecQuery("select * from USER_REJECTLETTER_TEMP where PRODUCTS = 'HNS EMAIL NOTICE' and LANGUAGE = 'ENG'");
            if (dtClaimTemplateEng.Rows.Count > 0)
                engBody = dtClaimTemplateEng.Rows[0]["TEMPLATE"].ToString();

            DataTable dtClaimTemplateKh = crud.ExecQuery("select * from USER_REJECTLETTER_TEMP where PRODUCTS = 'HNS EMAIL NOTICE' and LANGUAGE = 'KH'");
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
                var isNullOthHospital = dtClaims.Rows[0]["IS_NULL_OTH_HOSPITAL"].ToString() == "true";

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
                var accountHandler = string.Empty;

                var dtExeCode = crud.ExecQuery("select POL_MARKETING_EXECUTIVE_CODE from UW_T_POLICIES where POL_POLICY_NO = '" + policyNo.ToUpper().Trim() + "' and rownum = 1");
                if (dtExeCode.Rows.Count > 0)
                {
                    var dtSf = crud.ExecQuery("select SFC_SURNAME from SM_M_SALES_FORCE where SFC_CODE = '" + dtExeCode.Rows[0][0].ToString() + "'");
                    if (dtSf.Rows.Count > 0)
                    {
                        accountHandler = dtSf.Rows[0][0].ToString();
                    }
                }

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

                khBody = khBody.Replace("%DateTimeN%", CommonFunctions.KhDateNew(DateTime.Now));
                khBody = khBody.Replace("%PolicyHolder%", policyHolder);
                khBody = khBody.Replace("%SituationOfRisk%", situationOfRisk);
                khBody = khBody.Replace("%Policy No%", policyNo);
                khBody = khBody.Replace("%ClaimNo%", claimNo);
                khBody = khBody.Replace("%InsuredMember%", insuredMember);
                khBody = khBody.Replace("%ClaimAmount%", claimAmount); //CommonFunctions.KhNum(Convert.ToDouble(claimAmount))
                khBody = khBody.Replace("%ClaimCause%", claimCause);
                khBody = khBody.Replace("%Hospital%", hospital);

                if (DateTime.TryParseExact(treatmentDateString, dateTimeformats, new CultureInfo("en-US"), DateTimeStyles.None, out dtTreatmentDate))
                    treatmentDate = CommonFunctions.KhDateNew(Convert.ToDateTime(treatmentDate));

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

                if (claimNo.ToLower().Contains("hns"))
                {
                    engBody = engBody.Replace("%ProductType%", "Group Hospital and Surgical Insurance");
                    khBody = khBody.Replace("%ProductType%", "បណ្ណសន្យារ៉ាប់រងសម្រាកពេទ្យ និងវះកាត់");
                }
                else if (claimNo.ToLower().Contains("gpa"))
                {
                    engBody = engBody.Replace("%ProductType%", "Group Personal Accident Insurance");
                    khBody = khBody.Replace("%ProductType%", "បណ្ណសន្យារ៉ាប់រងគ្រោះថ្នាក់បុគ្គល");
                }
                else
                {
                    engBody = engBody.Replace("%ProductType%", "Figtree Blue Insurance");
                    khBody = khBody.Replace("%ProductType%", "បណ្ណសន្យារ៉ាប់រងសុខភាពអន្តរជាតិ Figtree Blue");
                }

                SetKhDoc(khBody, splitContainer1.Panel1);
                SetEngDoc(engBody, splitContainer1.Panel2);

                //SetReferenceDocument(khBody, splitContainer1.Panel1, true);
                //SetReferenceDocument(engBody, splitContainer1.Panel2, false);
            }
        }

        private void SetKhDoc(string htmlText, Control panel)
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

            var bookMark = khDoc.Words.First.Bookmarks.Add("entry");

            object bookmarkObj = bookMark;
            Range bookmarkRange = khDoc.Bookmarks.get_Item(ref bookmarkObj).Range;

            bookmarkRange.InsertFile(fileInfo.FullName);

            khDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
            khDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
            khDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
            //khDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
            khDoc.Paragraphs.LineSpacing = InchesToPoints(0.22f);
            khDoc.PageSetup.TopMargin = InchesToPoints(1.1f);
            khDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
            khDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
            khDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);

            for (int i = khDoc.Paragraphs.Count; i >= 1; i--)
            {
                Microsoft.Office.Interop.Word.Paragraph para = khDoc.Paragraphs[i];
                if (string.IsNullOrWhiteSpace(para.Range.Text))
                {
                    // Delete the empty paragraph
                    para.Range.Delete();
                }
            }

            khWord.Visible = true;

            khWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;
            khWord.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;
            gpsHandle = (IntPtr)khWord.ActiveWindow.Hwnd;
            SetParent(gpsHandle, panel.Handle);
            SetWindowLong(gpsHandle, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE);
            MoveWindow(gpsHandle, 0, 0, panel.Width, panel.Height, true);
        }

        private void SetEngDoc(string htmlText, Control panel)
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

            var bookMark = engDoc.Words.First.Bookmarks.Add("entry");

            object bookmarkObj = bookMark;
            Range bookmarkRange = engDoc.Bookmarks.get_Item(ref bookmarkObj).Range;

            bookmarkRange.InsertFile(fileInfo.FullName);

            engDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperLetter;
            engDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
            engDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
            //engDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
            engDoc.Paragraphs.LineSpacing = InchesToPoints(0.16f);
            engDoc.PageSetup.TopMargin = InchesToPoints(1.25f);
            engDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
            engDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
            engDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);

            //engDoc.Range().Font.Size = 11.0f;

            for (int i = engDoc.Paragraphs.Count; i >= 1; i--)
            {
                Microsoft.Office.Interop.Word.Paragraph para = engDoc.Paragraphs[i];
                if (string.IsNullOrWhiteSpace(para.Range.Text))
                {
                    // Delete the empty paragraph
                    para.Range.Delete();
                }
            }

            engWord.Visible = true;

            engWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;
            engWord.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;
            gpsHandle = (IntPtr)engWord.ActiveWindow.Hwnd;
            SetParent(gpsHandle, panel.Handle);
            SetWindowLong(gpsHandle, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE);
            MoveWindow(gpsHandle, 0, 0, panel.Width, panel.Height, true);
        }

        //private void SetReferenceDocument(string htmlText, Control panel, bool isKhmer)
        //{
        //    try
        //    {
        //        string filePath = "";

        //        tempfile = new TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
        //        tempfile.KeepFiles = false; //will be used when dispose tempfile
        //        filePath = tempfile.AddExtension("txt"); //add extension to the created Temporary File

        //        System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath); //open the file for writing.
        //        writer.Write(htmlText); //write text to the file
        //        writer.Close(); //remember to close the file again.
        //        writer.Dispose();

        //        var fileInfo = new FileInfo(filePath);

        //        Microsoft.Office.Interop.Word._Application oWord = new Application();
        //        Microsoft.Office.Interop.Word._Document oWordDoc = new Document();
        //        object oMissing = System.Reflection.Missing.Value;
        //        object oTrue = true;
        //        object oFalse = false;

        //        object missing = System.Reflection.Missing.Value;
        //        object visible = true;

        //        if (isKhmer)
        //        {
        //            //khDoc = oWord.Documents.Add(ref missing, ref missing, ref missing, ref visible);

        //            khDoc = oWord.Documents.Open(@"D:\OD1\Winforms Projects\IMSTool\Testing\Html\A&H-Claim-Rej-Notice-Kh.docx");

        //            var bookMark = khDoc.Words.First.Bookmarks.Add("entry");
        //            object bookmarkObj = bookMark;
        //            Range bookmarkRange = khDoc.Bookmarks.get_Item(ref bookmarkObj).Range;

        //            bookmarkRange.InsertFile(fileInfo.FullName);

        //            khDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
        //            khDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
        //            khDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
        //            khDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
        //            khDoc.PageSetup.TopMargin = InchesToPoints(isKhmer ? 1.1f : 1.25f);
        //            khDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
        //            khDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
        //            khDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);
        //        }
        //        else
        //        {
        //            engDoc = oWord.Documents.Add(ref missing, ref missing, ref missing, ref visible);

        //            engDoc = oWord.Documents.Open(@"D:\OD1\Winforms Projects\IMSTool\Testing\Html\A&H-Claim-Rej-Notice-Eng.docx");

        //            var bookMark = engDoc.Words.First.Bookmarks.Add("entry");
        //            object bookmarkObj = bookMark;
        //            Range bookmarkRange = khDoc.Bookmarks.get_Item(ref bookmarkObj).Range;

        //            bookmarkRange.InsertFile(fileInfo.FullName);

        //            engDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
        //            engDoc.Paragraphs.SpaceBefore = InchesToPoints(0.0f);
        //            engDoc.Paragraphs.SpaceAfter = InchesToPoints(0.0f);
        //            engDoc.Paragraphs.LineSpacing = InchesToPoints(0.13f);
        //            engDoc.PageSetup.TopMargin = InchesToPoints(isKhmer ? 1.1f : 1.25f);
        //            engDoc.PageSetup.LeftMargin = InchesToPoints(0.6f);
        //            engDoc.PageSetup.RightMargin = InchesToPoints(0.6f);
        //            engDoc.PageSetup.BottomMargin = InchesToPoints(0.5f);
        //            engDoc.Range().Font.Size = 11.0f;
        //        }

        //        oWord.Visible = true;
        //        oWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;
        //        oWord.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;

        //        gpsHandle = (IntPtr)oWord.ActiveWindow.Hwnd;
        //        SetParent(gpsHandle, panel.Handle);
        //        SetWindowLong(gpsHandle, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE);
        //        MoveWindow(gpsHandle, 0, 0, panel.Width, panel.Height, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Msgbox.Show(ex.ToString());
        //        throw;
        //    }
        //}

        private float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }

        private void frmEmailNoticeAttachmentEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] processList = Process.GetProcesses().Where(x => x.ProcessName.ToLower() == "winword").ToArray();
            foreach (var process in processList)
            {
                if (process.MainWindowHandle == (IntPtr)0x00000000)
                    process.Kill();
            }
        }
    }
}
