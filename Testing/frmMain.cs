using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testing.Properties;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;

namespace Testing
{
    public partial class frmMain : Form
    {
        public string UserName = "SICL";
        public string FullName = "SICL";
        public static List<string> NotiInfo = new List<string>();
        

        string title;
        public bool abort = false;
        private bool isOpenNotification;
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        public frmLogIn frmLog;

        //Noti
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();

        //private static int WM_QUERYENDSESSION = 0x11;
        //private static bool systemShutdown = false;

        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    if (m.Msg == WM_QUERYENDSESSION)
        //        systemShutdown = true;

        //    // If this is WM_QUERYENDSESSION, the closing event should be raised in the base WndProc.
        //    base.WndProc(ref m);

        //}

        public frmMain()
        {
            InitializeComponent();
            Text = "\u00b6" + Text + "\u00b6";
         //   btnClaimPaidPayee.Visible = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);

            //try
            //{
                //Application.Exit();
                //if (systemShutdown)
                //    e.Cancel = false;
                //else
                //{
                //    e.Cancel = true;
                //    WindowState = FormWindowState.Minimized;
                //    ShowInTaskbar = false;
                //}
            //}
            //catch (Exception ex)
            //{
                //Msgbox.Show(ex.Message);
            //}
        }

        private void btnRiskSearch_Click(object sender, EventArgs e)
        {
            frmSearch search = new frmSearch();
            search.UserName = this.UserName;
            openForm(search, (Button)sender);
        }

        private void btnClaimCheck_Click(object sender, EventArgs e)
        {
            Forms.frmClaim claim = new Forms.frmClaim();
            claim.UserName = this.UserName;
            openForm(claim, (Button)sender);
        }

        private void ClearMdiChild()
        {
            ClearPanel();

            if (this.ActiveMdiChild != null)
                ActiveMdiChild.Close();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            //CustomerProfitability cp = new CustomerProfitability();
            //cp.UserName = this.UserName;
            //openForm(cp, (Button)sender);
            SubPan(pnCustomerProfit);
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            Forms.frmUploadInformation UI = new Forms.frmUploadInformation();
            UI.UserName = this.UserName;
            openForm(UI, (Button)sender);
        }

        private void ConfirmChange()
        {
            if (this.ActiveMdiChild != null)
            {
                DialogResult dr = Msgbox.Show("Your inquiry in the current form will be lost. Do you want to proceed to another form?", "Confirmation");
                if (dr == System.Windows.Forms.DialogResult.No)
                    abort = true;
                else
                    abort = false;
            }
                
        }

        private void ChangeColorButt(Button btn)
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i] is Button)
                {
                    panel1.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    panel1.Controls[i].ForeColor = Color.White;
                }
            }

            for (int i = 0; i < pnSubClaim.Controls.Count; i++)
            {
                if (pnSubClaim.Controls[i] is Button)
                {
                    pnSubClaim.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    pnSubClaim.Controls[i].ForeColor = Color.White;
                }
            }

            for (int i = 0; i < pnSubAutoClaim.Controls.Count; i++)
            {
                if (pnSubAutoClaim.Controls[i] is Button)
                {
                    pnSubAutoClaim.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    pnSubAutoClaim.Controls[i].ForeColor = Color.White;
                }
            }

            for (int i = 0; i < pnSubAutoClaim.Controls.Count; i++)
            {
                if (pnSubAutoClaim.Controls[i] is Button)
                {
                    pnSubAutoClaim.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    pnSubAutoClaim.Controls[i].ForeColor = Color.White;
                }
            }

            for (int i = 0; i < pnBenifit.Controls.Count; i++)
            {
                if (pnBenifit.Controls[i] is Button)
                {
                    pnBenifit.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    pnBenifit.Controls[i].ForeColor = Color.White;
                }
            }

            for (int i = 0; i < pnEngUW.Controls.Count; i++)
            {
                if (pnEngUW.Controls[i] is Button)
                {
                    pnEngUW.Controls[i].BackColor = Color.FromArgb(0, 38, 58);
                    pnEngUW.Controls[i].ForeColor = Color.White;
                }
            }

            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(0, 38, 58);
        }

        private void tmCheckMaint_Tick(object sender, EventArgs e)
        {
            if (Maintenance.Check())
            {
                tmCheckMaint.Stop();
                tmClose.Start();
                Msgbox.Show("The system is under maintenance mode. It will be closed. Please wait or contact system admin.");
                //Environment.Exit(0);
            }
        }

        private void tmClose_Tick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    if (Maintenance.Check())
        //    {
        //        tmClose.Start();
        //        Msgbox.Show("The system is under maintenance mode. It will be closed. Please wait or contact system admin.");
        //        Environment.Exit(0);
        //    }
        //}

        private bool IsFontInstalled(string fontName)
        {
            using (var testFont = new System.Drawing.Font(fontName, 8))
                return 0 == string.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //System.Timers.Timer runonce = new System.Timers.Timer(20000);
            //runonce.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //runonce.AutoReset = false;
            //runonce.Start();

            //timerNoti.Start();
            tmCheckMaint.Start();
            this.Text += " - " + UserName;
            title = this.Text;
            //add auto vertical scroll bar
            panel1.HorizontalScroll.Maximum = 0;
            panel1.AutoScroll = false;
            panel1.VerticalScroll.Visible = false;
            panel1.AutoScroll = true;

            DateTime dateEvent = DateTime.Now;

            //object O = Resources.ResourceManager.GetObject("Logo_Red");
            //this.BackgroundImage = (Image)(O);
            object O = Resources.ResourceManager.GetObject("Kl");
            this.BackgroundImage = (Image)(O);
            //object O = Resources.ResourceManager.GetObject("Logo_Red");
            //this.BackgroundImage = (Image)(O);
            

            if (dateEvent.Month == 12 && dateEvent.Day>=24 && dateEvent.Day<=26) //Christmas Day
            {
                O = Resources.ResourceManager.GetObject("CH");
                this.BackgroundImage = (Image)(O);
            }

            //if (dateEvent.Month == 1 && dateEvent.Day >= 1 && dateEvent.Day <= 3) //New Year Day
            //{
            //    O = Resources.ResourceManager.GetObject("NYXMAS");
            //    this.BackgroundImage = (Image)(O);
            //}

            //if (dateEvent.Month == 1 && dateEvent.Day >= 24 && dateEvent.Day <= 27) //Chinese New Year  (Update every year due to the lunar calendar)
            //{
            //    O = Resources.ResourceManager.GetObject("CNY");
            //    this.BackgroundImage = (Image)(O);
            //}

            //if (dateEvent.Month == 4 && dateEvent.Day >= 10 && dateEvent.Day <= 20) //Khmer New Year Day
            //{             
            //    O = Resources.ResourceManager.GetObject("KhmerNewYear");
            //    this.BackgroundImage = (Image)(O);
            //}

            this.WindowState = FormWindowState.Maximized;

            //Feature in progress: Doc Control
            //notiTimer.Stop();
            //bnDocCtrl.Visible = false;
            //notificationToolStripMenuItem.Visible = false;
            //

            //Feature in progress: Auto Claim && Renewal List
            //renewalListToolStripMenuItem.Enabled = false;
            //bnAutoClaim.Visible = false;
            //
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmChangePass frmCP = new Forms.frmChangePass();
            frmCP.tbUser.Text = UserName;
            frmCP.ShowDialog();
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            //Application.Exit();
        }

        private void relogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog.Show();
            frmLog.tbPassword.Text = "";
            this.Hide();

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals("EXCEL") && string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    process.Kill();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Forms.CardPrinting card = new Forms.CardPrinting();
            card.username = FullName + "-IMS";
            
            openForm(card, (Button)sender);
        }                
        private void btnClaimPaidPayee_Click(object sender, EventArgs e)
        {
            Forms.ClaimPaidReportPayee clPaidPayee = new Forms.ClaimPaidReportPayee();
            clPaidPayee.UserName = UserName;
            openForm(clPaidPayee, (Button)sender);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.AboutBox1 about = new Forms.AboutBox1();
            about.ShowDialog();
        }

        private void btnListCurrent_Click(object sender, EventArgs e)
        {
            Forms.Latest_List_of_Insured_Members lm = new Forms.Latest_List_of_Insured_Members();
            lm.UserName = UserName;
            openForm(lm, (Button)sender);
        }

        private void btnClaimRequisitionReport_Click(object sender, EventArgs e)
        {
            Forms.ClaimRequisitionReport cr = new Forms.ClaimRequisitionReport();
            cr.UserName = UserName;
            openForm(cr, (Button)sender);
        }

     
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        
        private void btnPrintInvoice_Click_1(object sender, EventArgs e)
        {
            //Forms.frmPrintInvoice cr = new Forms.frmPrintInvoice();
            //cr.UserName = UserName;
            //openForm(cr, (Button)sender);
            SubPan(pnInvoice);
        }

        private void bnClaimRI_Click(object sender, EventArgs e)
        {
            Forms.frmClaimRI claimRI = new Forms.frmClaimRI();
            claimRI.type = "ClaimIncurred";
            openForm(claimRI, (Button)sender);
        }

        private void bnClaimPaidRI_Click(object sender, EventArgs e)
        {
            Forms.frmClaimRI claimRI = new Forms.frmClaimRI();
            claimRI.type = "ClaimPaid";
            openForm(claimRI, (Button)sender);
        }

        private void premiumRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmPremiumRegister pre = new Forms.frmPremiumRegister();
            pre.Show();
        }

        private void userLoginCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CountUserlogin cul = new CountUserlogin();
            cul.Show();
        }

        private void pendingRIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendEmail email = new frmSendEmail();
            email.Show();
        }
       
        private void openForm(Form fm, Button bn)
        {
            ConfirmChange();
            if (abort == true)
                return;

            ChangeColorButt(bn);

            ClearMdiChild();
            fm.MdiParent = this;
            fm.Dock = DockStyle.Fill;
            fm.Show();
        }

        private void bnClaimRep_Click(object sender, EventArgs e)
        {
            SubPan(pnSubClaim);
        }

        private void policyScheduleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.Pol_Schedule pol = new Forms.Pol_Schedule();
            pol.Show();
        }

        private void monthlyReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.MReport MR = new Forms.MReport();
            MR.username = UserName;
            MR.Show();
        }

        private void addCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmAddChara ac = new Forms.frmAddChara();
            ac.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubPan(pnSubFL);
        }

        private void bnPreClaimFL_Click(object sender, EventArgs e)
        {
            Forms.frmPreClaimFL_CL pre_clm = new Forms.frmPreClaimFL_CL();
            openForm(pre_clm, (Button)sender);
        }

        private void SubPan(Panel pn)
        {
            if (pn.Visible == true)
            {
                ClearPanel();
            }
            else
            {
                ClearPanel();
                pn.Visible = true;
                pn.BringToFront();
            }
        }

        private void ClearPanel()
        {
            pnSubClaim.Visible = false;
            pnSubFL.Visible = false;
            pnSubAutoClaim.Visible = false;
            pnBenifit.Visible = false;
            pnEngUW.Visible = false;
            pnInvoice.Visible = false;
            pnCustomerProfit.Visible = false;
            pnTravelReport.Visible = false;
        }

        private void bnPolRem_Click(object sender, EventArgs e)
        {
            Forms.PolicyRemark pol = new Forms.PolicyRemark();
            openForm(pol, (Button)sender);
        }

        private void bnClaimOSRI_Click(object sender, EventArgs e)
        {
            Forms.frmClaimRI claimRI = new Forms.frmClaimRI();
            claimRI.type = "ClaimOS";
            openForm(claimRI, (Button)sender);
        }

        private void aCPREMIUMREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmAcReport ac = new Forms.FrmAcReport();
            ac.Show();
        }

        private void bnClEmail_Click(object sender, EventArgs e)
        {
            Forms.frmSendEmailClaim cl = new Forms.frmSendEmailClaim();
            cl.UserName = UserName;
            openForm(cl, (Button)sender);
        }

        private void timePendingEmail_Tick(object sender, EventArgs e)
        {
            DataTable dtExc = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "Pending", "" });

            String sqlPen = "select EMAIL, EMAIL_PW from USER_PRINT_SYSTEM where USER_CODE = '" + UserName + "'";
            string getEmail = crud.ExecQuery(sqlPen).Rows[0].ItemArray[0].ToString();
            String email_PW = crud.ExecQuery(sqlPen).Rows[0].ItemArray[1].ToString();
            if (!string.IsNullOrEmpty(getEmail) && !string.IsNullOrEmpty(email_PW) && dtExc.Rows.Count > 0 && UserName != "ADMIN")
            {
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                Form prompt = new Form();
                prompt.MinimizeBox = false;
                prompt.MaximizeBox = false;
                prompt.StartPosition = FormStartPosition.Manual;
                prompt.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - prompt.Width,
                       Screen.PrimaryScreen.WorkingArea.Height - prompt.Height);
                prompt.Top = 900;
                prompt.Width = 200;
                prompt.Height = 130;
                prompt.Text = "Warning";
                Label textLabel = new Label() { Left = 25, Top = 20, Text = "Please check pending email", Width = 150 };
                Button confirmation = new Button() { Text = "Ok", Left = 60, Width = 50, Top = 50 };
                confirmation.Click += (sender2, e2) => { prompt.Close(); };
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.ShowDialog();
            }
        }

        private void btnTravelRp_Click(object sender, EventArgs e)
        {
            //Forms.FrmTravelReport tr = new Forms.FrmTravelReport();
            //openForm(tr, (Button)sender);
            SubPan(pnTravelReport);
        }

        private void monthlyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bnDocCtrl_Click(object sender, EventArgs e)
        {

            DataTable dtTemp = sqlcrud.LoadData("SELECT ROLE FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "'").Tables[0];
            if (dtTemp.Rows.Count <= 0)
            {
                Msgbox.Show("You aren't allowed to access Document Control Tracking. Contact Administrator for more detail.");
                return;
            }

            Forms.frmDocumentControl cl = new Forms.frmDocumentControl();
            cl.UserName = UserName;
            openForm(cl, (Button)sender);
        }


        private void notificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bnAutoClaim_Click(object sender, EventArgs e)
        {
            SubPan(pnSubAutoClaim);
        }

        private void bnDeductible_Click(object sender, EventArgs e)
        {
            Forms.frmDeductible frmDeductible = new Forms.frmDeductible();
            frmDeductible.Username = UserName;
            openForm(frmDeductible, (Button)sender);
        }

        private void bnWindscreen_Click(object sender, EventArgs e)
        {
            Forms.frmWindscreen frmWindscreen = new Forms.frmWindscreen();
            frmWindscreen.Username = UserName;
            openForm(frmWindscreen, (Button)sender);
        }

        private void renewalListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bnReports_Click(object sender, EventArgs e)
        {
            Forms.frmAutoClaimReport frmReports = new Forms.frmAutoClaimReport();
            frmReports.Username = UserName;
            openForm(frmReports, (Button)sender);
        }

        private void autoMonthlyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmAutoMonthlyReport frm = new Forms.frmAutoMonthlyReport();
            frm.Show();
        }

        private void btnSettleCreditNote_Click(object sender, EventArgs e)
        {
            Forms.frmSettleCreditNote frm = new Forms.frmSettleCreditNote();
            openForm(frm, (Button)sender);
        }

        private void bnLetter_Click(object sender, EventArgs e)
        {
            Forms.frmAutoLetters frm = new Forms.frmAutoLetters();
            frm.Username = UserName;
            openForm(frm, (Button)sender);
        }

        private void bnAcceptanceFormCDV_Click(object sender, EventArgs e)
        {
            Forms.frmAcceptanceFormCDV frm = new Forms.frmAcceptanceFormCDV();
            frm.Username = UserName;
            openForm(frm, (Button)sender);
        }

        private void InvoiceSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new Forms.frmInvoiceSetting();
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Forms.frmFigtreeBlueRpt fgRpt = new Forms.frmFigtreeBlueRpt();
            fgRpt.Show();
        }

        private void btnBenifitScheme_Click(object sender, EventArgs e)
        {
            
            SubPan(pnBenifit);
        }

        private void btnHNSScheme_Click(object sender, EventArgs e)
        {
            Forms.HospitalSurgicalScheme frm = new Forms.HospitalSurgicalScheme();
            openForm(frm, (Button)sender);
        }

        private void btnProducerClaim_Click(object sender, EventArgs e)
        {
            Forms.frmClaimProducer frm = new Forms.frmClaimProducer();
            openForm(frm, (Button)sender);
        }

        private void btnEngineering_Click(object sender, EventArgs e)
        {
            SubPan(pnEngUW);
        }

        private void btnRemindLetter_Click(object sender, EventArgs e)
        {
            Forms.frmRemindLetterEng frm = new Forms.frmRemindLetterEng();
            frm.Username = UserName;
            openForm(frm, (Button)sender);
        }

        private void btnBHPScheme_Click(object sender, EventArgs e)
        {
            Forms.FigtreeBlueScheme frm = new Forms.FigtreeBlueScheme();
            openForm(frm, (Button)sender);
        }

        private void CreateTicket_Click(object sender, EventArgs e)
        {
            Forms.TicketRequest frm= new Forms.TicketRequest();
            frm.Username = frmLog.fullusername;
            frm.Show();
        }
        private void openForm(Form fm, ToolStripMenuItem bn)
        {
            ConfirmChange();
            if (abort == true)
                return;

           // ChangeColorButt(bn);

            ClearMdiChild();
            fm.MdiParent = this;
            fm.Dock = DockStyle.Fill;
            fm.Show();
        }
        private void tsPMAllDept_Click(object sender, EventArgs e)
        {
            Forms.PremiumRegisterHzz frm = new Forms.PremiumRegisterHzz();
            //frm.Username = UserName;
            openForm(frm, (ToolStripMenuItem)sender);
        }

        private void tsPMAdmin_Click(object sender, EventArgs e)
        {
            Forms.MonthlyReportAdmin frm = new Forms.MonthlyReportAdmin();
            //frm.Username = UserName;
            frm.Show();
        }

        private void timerNoti_Tick(object sender, EventArgs e)
        {
            var docUserSql = sqlcrud.LoadData("SELECT USER_NAME FROM tbDOC_USER WHERE USER_NAME = '" + UserName + "' ").Tables[0];
            var isDocUser = docUserSql.Rows.Count > 0;

            if (!isDocUser)
            {
                timerNoti.Tick -= timerNoti_Tick;
                return;
            }

            var notiSql = sqlcrud.LoadData("SELECT TOP 3 NOTI_DETAIL, NOTI_DATE FROM tbNoti WHERE IS_READ = 0 AND NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC").Tables[0];
            if (notiSql.Rows.Count <= 0)
                return;

            NotiInfo.Clear();
            for (int i = 0; i < notiSql.Rows.Count; i++)
            {
                string concat = string.Concat(notiSql.Rows[i]["NOTI_DETAIL"].ToString(), "*", notiSql.Rows[i]["NOTI_DATE"].ToString());
                NotiInfo.Add(concat);
            }

            FormCollection fc = Application.OpenForms;
           
            foreach (Form frm in fc)
            {
                isOpenNotification = frm.Name == "frmNotification";
            }
            if (isOpenNotification) return;

            Forms.frmNotification frmNoti = new Forms.frmNotification();
            frmNoti.Show();
        }

        private void createNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmUserManagement userManagement = new Forms.frmUserManagement();
            userManagement.ShowDialog();
        }

        private void manageUserRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmUserRoleManagement userRoleManagement = new Forms.frmUserRoleManagement();
            userRoleManagement.ShowDialog();
        } 

        private void btnSubPrintInvoice_Click(object sender, EventArgs e)
        {
            Forms.frmPrintInvoice cr = new Forms.frmPrintInvoice();
            cr.UserName = UserName;
            openForm(cr, (Button)sender);
        }

        private void btnSubBreakdownInvoice_Click(object sender, EventArgs e)
        {
            Forms.BreakdownInvoice bi = new Forms.BreakdownInvoice();
            bi.Username = UserName;
            bi.Show();
        }

        private void niIMSTool_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //ShowInTaskbar = true;
            //niIMSTool.Visible = false;
            //WindowState = FormWindowState.Maximized;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //if (WindowState == FormWindowState.Minimized)
            //{
            //    ShowInTaskbar = true;
            //    niIMSTool.Visible = true;
            //}
            //if (WindowState == FormWindowState.Maximized)
            //    ShowInTaskbar = true;
        }

        private void quitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCustomerProfit_Click(object sender, EventArgs e)
        {
            CustomerProfitability cp = new CustomerProfitability();
            cp.UserName = this.UserName;
            openForm(cp, (Button)sender);
        }

        private void btnCustomerProfitSummary_Click(object sender, EventArgs e)
        {
            pnCustomerProfit.Visible = false;

            var cusProfitSummary = new Forms.frmGenerateCustomerProfitSummary();
            cusProfitSummary.ShowDialog();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //if (frmLogIn.Usert.ToLower() == "c-hrm" || frmLogIn.Usert.ToLower() == "admin" || frmLogIn.Usert.ToLower() == "s-brs")
            //    btnCustomerProfitSummary.Enabled = true;
            //else
            //    btnCustomerProfitSummary.Enabled = false;

            List<string> fonts = new List<string>();
            fonts.Add(@"\\192.168.110.250\public$\MIS\Software\IMS Tool\Niradei Font\Niradei-Regular.ttf");
            fonts.Add(@"\\192.168.110.250\public$\MIS\Software\IMS Tool\Niradei Font\Niradei-Bold.ttf");
            fonts.Add(@"\\192.168.110.250\public$\MIS\Software\IMS Tool\Niradei Font\Niradei-SemiBold.ttf");

            if (!IsFontInstalled("Niradei"))
            {
                foreach (var k in fonts)
                {
                    var shellAppType = Type.GetTypeFromProgID("Shell.Application");
                    var shell = Activator.CreateInstance(shellAppType);
                    var fontFolder = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.Fonts) });
                    if (File.Exists(k))
                        fontFolder.CopyHere(k);
                }
                Environment.Exit(0);
            }
        }

        private void btnCoInvoice_Click(object sender, EventArgs e)
        {
            Forms.frmCoInvoice frmCo= new Forms.frmCoInvoice();
            openForm(frmCo, (Button)sender);
            
        }

        private void renewalListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.frmRenewalList frmRenewalList = new Forms.frmRenewalList();
            frmRenewalList.Show();
        }

        private void pRRiskCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmPRRiskCount frmPRRiskCount = new Forms.frmPRRiskCount();
            frmPRRiskCount.ShowDialog();
        }

        private void clmPaymentReq_Click(object sender, EventArgs e)
        {
            Forms.ClPaymentReq claimRQList = new Forms.ClPaymentReq();
            claimRQList.FullName = this.FullName;
            openForm(claimRQList, (Button)sender);
        }

        private void acledaSalesPersonReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.AcledaSalePerson AcledaSP = new Forms.AcledaSalePerson();
            AcledaSP.Show();
        }

        private void CusProfitV2_Click(object sender, EventArgs e)
        {

        }

        private void btnTravelReport_Click(object sender, EventArgs e)
        {
            Forms.FrmTravelReport tr = new Forms.FrmTravelReport();
            openForm(tr, (Button)sender);
        }

        private void btnTRVExcelUpload_Click(object sender, EventArgs e)
        {
            Forms.frmTRVExcelUpload tr = new Forms.frmTRVExcelUpload();
            openForm(tr, (Button)sender);
        }

        private void btnAutoUploadRpt_Click(object sender, EventArgs e)
        {
            Forms.frmAutoUploadReport tr = new Forms.frmAutoUploadReport();
            openForm(tr, (Button)sender);
        }

       

        

        //private List<string> EverythingBetween(string source, string start, string end)
        //{
        //    var results = new List<string>();

        //    string pattern = string.Format(
        //        "{0}({1}){2}",
        //        Regex.Escape(start),
        //        ".+?",
        //         Regex.Escape(end));

        //    foreach (Match m in Regex.Matches(source, pattern))
        //    {
        //        results.Add(m.Groups[1].Value);
        //    }

        //    return results;
        //}
      
    }
}
