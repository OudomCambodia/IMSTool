using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testing.Properties;
using IExcel = Microsoft.Office.Interop.Excel;

namespace Testing.Forms
{
    public partial class frmDocumentControl : Form
    {
        int num; bool isChecked;
        public string UserName = "sicl";
        CRUD crud = new CRUD();
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        DataTable _dtNoti = new DataTable();
        string[] Role = null, Status = null;
        string FullName = "", UserID = "", Team = "";
        public static Dictionary<string, string> docType = new Dictionary<string, string>() 
        {
            {"P", "Policy"},{"Q", "Quotation"},{"RQ", "Renewal Quotation"},{"RP", "Renewal Policy"},{"E", "Endorsement"},{"C", "Cancellation"}
        };
        public static Dictionary<string, string> docStatus = new Dictionary<string, string>()
        {
            {"0", "SUBMITTED TO UW"},{"1", "CONTROLLER ACCEPTED"},{"2", "DP PROCESSING"},{"3", "DP PROCESSED"},
            {"4", "PENDING FOR SIGNATURE"},{"5", "PACKAGING"},{"6","PACKAGED"},{"7","DONE"},
            {"8", "CANCEL BY PRODUCER"},{"9","CANCEL BY CONTROLLER"}
        };
        DataTable dtDoc = new DataTable();
        CheckBox checkboxHeader = new CheckBox();

        string status = "0"; //use as global status based on selectedStatusBtn
        public string buttonName;
        private string currentSelectedButton;

        public IExcel.Application xlprog = new IExcel.Application();

        public static bool SubFrmChange = true;//avoid requery when no change

        public bool notiTriggered = false;
        public string notiRemark = string.Empty;
        int notiDocIndex = -1;

        double[] holiday = null;

        //public static bool PrintCard = false;//switch to Submit Card Printing form
        //public static string fwdpolno = string.Empty;

        private readonly string HEAD_FILING = "HEAD_FILING";
        private readonly string ALL_REGIONALS = "ALL_REGIONALS";
        private readonly string ALL_BANKS = "ALL_BANKS";
        private readonly string ALL_BROKERS = "ALL_BROKERS";

        public static string SelectionColor;

        private bool isClickReject;
        private bool isClickPending;
        private bool isClickReverse;

        public frmDocumentControl()
        {
            InitializeComponent();
            btnChangeStatus.Click += btnChangeStatus_Click;
            btnAddDoc.Click += btnAddDoc_Click;
            btnCloseReopen.Click += btnCloseReopen_Click;
            btnReassignDP.Click += btnReassignDP_Click;
            btnReturn.Click += btnReturn_Click;
            dgvNoti.SelectionChanged -= dgvNoti_SelectionChanged;
        }

        private void frmDocumentControl_Load(object sender, EventArgs e)
        {
            try
            {
                #region --- SELECTION COLOR ---
                var colors = new List<string>();

                colors.Add("4,124,212"); //white
                colors.Add("0,153,153"); // white
                colors.Add("51,204,255"); // black
                colors.Add("51,102,153"); // white
                colors.Add("128,128,128"); // white
                colors.Add("0, 38, 58");

                for (int i = 0; i < colors.Count; i++)
                    cboColor.Items.Add(colors[i].ToString());

                var selectionColor = sqlcrud.LoadData("select SELECTION_COLOR from tbDOC_USER where USER_NAME = '" + UserName + "'").Tables[0].Rows[0][0].ToString();
                cboColor.SelectedIndex = cboColor.FindString(selectionColor);
                SelectionColor = selectionColor;
                #endregion

                DataTable dt = sqlcrud.LoadData("select * from dbo.Product").Tables[0];
                frmAddDocument1.product.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    frmAddDocument1.product.Add(dr["ProType"].ToString(), dr["ProLine"].ToString());
                }
                //Test:
                //UserName = "Controller1";
                //

                //Set role,fullname
                DataTable dtTemp = sqlcrud.LoadData("SELECT ROLE,FULL_NAME,USER_CODE,TEAM FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "'").Tables[0];
                Role = dtTemp.Rows[0][0].ToString().Split(',');
                FullName = dtTemp.Rows[0][1].ToString(); 
                UserID = dtTemp.Rows[0][2].ToString();
                Team = dtTemp.Rows[0][3].ToString(); 
                //

                pNotification.Visible = false;
                timerNoti.Start();
                _dtNoti = sqlcrud.LoadData("SELECT TOP 50 * FROM [DocumentControlDB].[dbo].[tbNoti] WHERE NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC").Tables[0];
                DataTable dtNotiCount = sqlcrud.LoadData("SELECT COUNT(SEQ_NO) AS TOTAL_NOTI FROM (SELECT TOP 50 * FROM [DocumentControlDB].[dbo].[tbNoti] WHERE NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC) t WHERE IS_READ = 0").Tables[0];
                if (dtNotiCount.Rows.Count > 0)
                {
                    int notiCount = 0;
                    for (int i = 0; i < dtNotiCount.Rows.Count; i++)
                    {
                        notiCount += Convert.ToInt32(dtNotiCount.Rows[i]["TOTAL_NOTI"]);
                    }
                    lblNotiCount.Visible = true;
                    lblNotiCount.Text = notiCount.ToString();

                    if (notiCount == 0)
                    {
                        lblNotiCount.Visible = false;
                        btnNotification.Image = Resources.notification_unscreen_1;
                    }
                    else
                        btnNotification.Image = Resources.notification_unscreen;
                }
                
                NotiSentDuration();

                dgvNoti.DataSource = _dtNoti;
                 
                //set Status
                string tmpstatus = "";
                foreach (string s in Role)
                {
                    tmpstatus += sqlcrud.LoadData("SELECT STATUS FROM dbo.tbChangeStatus_Allow WHERE ROLE = '" + s.Trim() + "'").Tables[0].Rows[0][0].ToString() + ",";
                }
                tmpstatus = tmpstatus.Remove(tmpstatus.Length - 1);
                Status = tmpstatus.Split(',');
                Status = Status.Distinct().ToArray();
                //

                holiday = getHolidayinOADate();
                if (holiday == null)
                {
                    holiday = new double[1];
                    holiday[0] = new DateTime(1900, 1, 1).ToOADate(); //add 1900/1/1 as holiday when no holiday found to prevent error
                }

                //requeryDGV();

                if (!Role.Contains("PRODUCER") && !Role.Contains("PCD")) 
                    disabledButt(btnAddDoc);
                else
                    enabledButt(btnAddDoc);
                //if (!Role.Contains("PRODUCER".Contains) && !Role.Contains("PCD".Contains)) disabledButt(btnAddDoc); else enabledButt(btnAddDoc);
                //if (!Role.Contains("DP") disabledButt(btnReassignDP); else enabledButt(btnReassignDP);

                //Style
                dgvDoc.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvDoc.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
                dgvDoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgvOpen.DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#D31145");
                //dgvOpen.DefaultCellStyle.SelectionForeColor = Color.White;
                //

                if (Role.Contains("DP"))
                    btnDPProcessing.PerformClick();
                if (Role.Contains("FILLING"))
                    btnPackaging.PerformClick();
                if (!Role.Contains("FILLING") && !Role.Contains("DP"))
                    btnSubmittedtoUW.PerformClick();
                if (Role.Contains("CONTROLLER") && Role.Contains("DP"))
                    btnSubmittedtoUW.PerformClick();
                if (!Role.Contains("FILLING")) btnManageCrono.Enabled = false; else btnManageCrono.Enabled = true;


                if (Role.Contains("UWHEAD"))
                {
                    enabledButt(btnAddDoc);
                    btnManageCrono.Enabled = true;
                }


                //Noti
                if (notiTriggered)
                {
                    btnCancel.PerformClick();
                    tbFilterdgvDoc.Text = notiRemark;
                    tbFilterdgvDoc_TextChanged(tbFilterdgvDoc, EventArgs.Empty);
                }
                //

                //rbCustomer.Checked = true;
                rbRefID.Checked = true;

                gbAllRecordOption.Visible = false;


                //
                //DataTable dt = sqlcrud.LoadData("select * from dbo.Product").Tables[0];
                //frmAddDocument1.product.Clear();
                //foreach(DataRow dr in dt.Rows){
                //    frmAddDocument1.product.Add(dr["ProType"].ToString(),dr["ProLine"].ToString());
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        //private void dgvOpen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == dgvDoc.Columns["Action"].Index || e.RowIndex < 0) return;
        //    int row = e.RowIndex;
        //    var frm = new frmDocumentDetail1();
        //    frm.DocId = dgvDoc.Rows[row].Cells["REF_ID"].Value.ToString();
        //    frm.printable = (Role.Contains("PRODUCER") ? true : false;
        //    frm.ShowDialog();
        //}

        private void btnAddDoc_Click(object sender, EventArgs e)
        {
            var frm = new frmAddDocument1();
            frm.username = UserName;
            frm.usercode = UserID;
            frm.team = Team;
            SubFrmChange = true;
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.Show();
            
        }

        void frmClose(object sender, FormClosedEventArgs e)
        {
            rbRefID.Checked = true;
            if(SubFrmChange) requeryDGV();
        }

        //void frmCloseforDPProcess(object sender, FormClosedEventArgs e)
        //{
        //    rbRefID.Checked = true;
        //    if (PrintCard)
        //    {
        //        Msgbox.Show("Submit Card Printing will appeared next. (Print Card = Yes)");

        //        CardPrinting printcardfrm = new CardPrinting(PrintCard,fwdpolno);
        //        printcardfrm.MdiParent = this.MdiParent;
        //        this.ParentForm.ActiveMdiChild.Close();
        //        printcardfrm.Dock = DockStyle.Fill;
        //        printcardfrm.Show();
        //        return;
        //    }
        //    if (SubFrmChange) requeryDGV();
        //}

        private void requeryDGV()
        {
            try
            {
                lblSel.Text = "0";
                lblTot.Text = "0";
                dgvDoc.DataSource = null;
                dgvDoc.Columns.Clear();
                checkboxHeader.Visible = false;
                checkboxHeader.Checked = false;

                Cursor.Current = Cursors.WaitCursor;

                //string dgvOpensqlstring = "select * from dbo.VIEW_DOC WHERE " + ((status == "8") ? "(DOC_CUR_STATUS IN (8,9)) " 
                //    : (status == "99") ? "DOC_CUR_STATUS like '%%'" : (status == "13") ? "DOC_CUR_STATUS IN (13,14,15,16) " : "DOC_CUR_STATUS = " + status);

                //dgvOpensqlstring = (Role.Contains("UWHEAD") || Role.Contains("CONTROLLER")) ? dgvOpensqlstring :
                //    (Role.Contains("PRODUCER")) ? dgvOpensqlstring + " AND CREATE_BY like '%" + FullName + "%'" :
                //    (Role.Contains("PCD")) ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (SELECT FULL_NAME FROM dbo.tbDOC_USER WHERE USER_NAME like 'R-%'))" :
                //    (Role.Contains("DP") && UserID != "D01") ? dgvOpensqlstring + " AND DP_NAME = '" + FullName + "'" :
                //    (Role.Contains("DP") && UserID == "D01" && (status == "4" || status == "5" || status == "6" || status == "7")) ? dgvOpensqlstring + " AND PRODUCT_TYPE IN ('EMC','STN','MED','Chinese PA') " :
                //    (Role.Contains("DP") && UserID == "D01" && status != "4" && status != "5" && status != "6" && status != "7") ? dgvOpensqlstring + " AND DP_NAME = '" + FullName + "'" :
                //    (Role.Contains("FILLING")) ? dgvOpensqlstring + " AND PRODUCT_TYPE NOT IN ('EMC','STN','MED','Chinese PA') " : dgvOpensqlstring;


                string dgvOpensqlstring = "select * from dbo.VIEW_DOC WHERE " + ((status == "8") ? "(DOC_CUR_STATUS IN (8,9)) "
                    : (status == "99") ? "DOC_CUR_STATUS like '%%'" : (status == "13") ? "DOC_CUR_STATUS IN (13,14,15,16) " : "DOC_CUR_STATUS = " + status);
                //update check on regional team can see each other doc - 23-08-2022 - request Oum Thavrak - Theane
                DataTable dtRegional = sqlcrud.LoadData("select * from tbRegional where username like '%" + frmLogIn.Usert.ToUpper() + "%'").Tables[0];
                if (dtRegional.Rows.Count != 0)
                {

                    dgvOpensqlstring += " and CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where USER_NAME in (" + dtRegional.Rows[0][2].ToString().ToUpper() + ")) ";
                    //dtDoc = sqlcrud.LoadData(dgvOpensqlstring).Tables[0];

                }
                else { 
                //Role[0] is primary role
                #region --- OLD CODING ---
                //dgvOpensqlstring = (UserID == "S01")
                //    ? dgvOpensqlstring + " AND PRODUCT_LINE IN ('A&H','FL') "
                //    : (Role[0] == "UWHEAD" || Role[0] == "CONTORLLER" || Role[0] == "FILLING" || Role[0] == "UNW")
                //    ? dgvOpensqlstring
                //    : (Role[0] == "PRODUCER")
                //        ? (UserID == "P09" || UserID == "P10")
                //        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (SELECT FULL_NAME FROM dbo.tbDOC_USER WHERE USER_NAME like 'R-%'))"
                //        : (UserID == "P70" || UserID == "D44")
                //        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where [GROUP] = 'AGENTTEAM' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where t.USER_CODE in (select USER_CODE from dbo.tbExceptionalRole where USER_CODE = t.USER_CODE and EXCEPTION_CODE = 'U-BNK'))))"
                //        : (UserID == "P42")
                //        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where [GROUP] = 'BROKERTEAM'))"
                //        : dgvOpensqlstring + " AND CREATE_BY like '%" + FullName + "%'"
                //    : (Role[0] == "DP")
                //        ? dgvOpensqlstring + " AND DP_NAME = '" + FullName + "'"
                //        : dgvOpensqlstring;
                #endregion

                #region --- NEW CODING ---
                var dsSpecialCode = sqlcrud.LoadData("select * from tbDOC_SPECIAL_CODE where USER_ID = '" + UserID + "'").Tables[0];
                var specialCode = string.Empty;
                if (dsSpecialCode.Rows.Count > 0)
                    specialCode = dsSpecialCode.Rows[0]["SPECIAL_CODE"].ToString().Trim();

                dgvOpensqlstring = (specialCode.Equals(HEAD_FILING))
                    ? dgvOpensqlstring + " AND PRODUCT_LINE IN ('A&H','FL') "
                    : (Role[0] == "UWHEAD" || Role[0] == "CONTORLLER" || Role[0] == "FILLING" || Role[0] == "UNW")
                    ? dgvOpensqlstring
                    : (Role[0] == "PRODUCER")
                        ? (specialCode.Equals(ALL_REGIONALS))
                        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (SELECT FULL_NAME FROM dbo.tbDOC_USER WHERE USER_NAME like 'R-%'))"
                        : (specialCode.Equals(ALL_BANKS))
                        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where [GROUP] = 'AGENTTEAM' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where t.USER_CODE in (select USER_CODE from dbo.tbExceptionalRole where USER_CODE = t.USER_CODE and EXCEPTION_CODE = 'U-BNK'))))"
                        : (specialCode.Equals(ALL_BROKERS))
                        ? dgvOpensqlstring + " AND (CREATE_BY like '%" + FullName + "%' OR CREATE_BY in (select FULL_NAME from dbo.tbDOC_USER t where [GROUP] = 'BROKERTEAM'))"
                        : dgvOpensqlstring + " AND CREATE_BY like '%" + FullName + "%'"
                    : (Role[0] == "DP")
                        ? dgvOpensqlstring + " AND DP_NAME = '" + FullName + "'"
                        : dgvOpensqlstring;
                #endregion
                }
                if (Role[0] == "FILLING")
                {
                    if (Team != "A&H")// Filling For A&H
                        dgvOpensqlstring += " AND PRODUCT_TYPE NOT IN ('EMC','STN','MED','Chinese PA') ";
                    //else
                    //    dgvOpensqlstring += " AND PRODUCT_TYPE IN ('EMC','STN','MED','Chinese PA') ";
                }

                string[] TeamSplit = Team.Split(',');
                if (!String.IsNullOrEmpty(Team) && frmAddDocument1.product.ContainsValue(TeamSplit[0]))
                {
                    string ProType = "";
                    bool check = false;
                    foreach (string t in TeamSplit)
                    {
                        foreach (KeyValuePair<string, string> entry in frmAddDocument1.product)
                        {
                            if (entry.Value == t)
                            {
                                check = true;
                                ProType += "'" + entry.Key + "',";
                            }
                        }
                    }
                    if (check)
                    {
                        ProType = ProType.Remove(ProType.Length - 1);
                        dgvOpensqlstring += " AND PRODUCT_TYPE IN (" + ProType + ")";
                    }
                }

                //All records option
                if ((status == "99" || status == "7") && rbSpecificDate.Checked)
                {
                    dgvOpensqlstring += " AND convert(datetime,CREATE_DATE,103) >= '" + dtpSpecificDateFr.Text + " 00:00:00' AND convert(datetime,CREATE_DATE,103) <= '" + dtpSpecificDateTo.Text + " 23:59:59'";
                }
                //
                
               
                
                dtDoc = sqlcrud.LoadData(dgvOpensqlstring + " order by REF_ID asc, CREATE_DATE asc").Tables[0];


                DataColumn dcRowString = dtDoc.Columns.Add("_RowString", typeof(string));
                DataColumn WorkHrs = dtDoc.Columns.Add("WorkHrs", typeof(string));
                foreach (DataRow dataRow in dtDoc.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(dataRow["REF_ID"].ToString());
                    sb.Append("\t");
                    sb.Append(dataRow["CUSTOMER"].ToString());
                    sb.Append("\t");
                    sb.Append(dataRow["POLICY_NO"].ToString());
                    dataRow[dcRowString] = sb.ToString();

                    dataRow[WorkHrs] = getWorkingHour(Convert.ToDateTime(dataRow["STATUS_SET_ON"]), DateTime.Now);
                }

                if (dtDoc.Rows.Count > 0)
                {

                    DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                    //CheckBox chk = new CheckBox();
                    dgvDoc.Columns.Add(CheckboxColumn);

                    dgvDoc.DataSource = dtDoc;


                    dgvDoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewColumn column = dgvDoc.Columns[0];
                    column.Width = 35;
                    dgvDoc.Columns[0].Resizable = DataGridViewTriState.False;
                    dgvDoc.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


                    // add checkbox header
                    Rectangle rect = dgvDoc.GetCellDisplayRectangle(0, -1, true);
                    // set checkbox header to center of header cell. +1 pixel to position correctly.
                    rect.X = rect.Location.X + 8;
                    rect.Y = rect.Location.Y + 5;
                    rect.Width = rect.Size.Width;
                    rect.Height = rect.Size.Height;

                    checkboxHeader.Checked = false;
                    checkboxHeader.Visible = true;
                    checkboxHeader.Name = "checkboxHeader";
                    checkboxHeader.Size = new Size(15, 15);
                    checkboxHeader.Location = rect.Location;
                    checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                    dgvDoc.Controls.Add(checkboxHeader);

                    dgvDoc.Columns["DOC_CUR_STATUS"].Visible = false;
                    dgvDoc.Columns["STATUS_SET_ON"].Visible = false;
                    dgvDoc.Columns["RETURN_REASON"].Visible = false;
                    dgvDoc.Columns["RETURN_DATE"].Visible = false;
                    dgvDoc.Columns["PRODUCER_TEAM"].Visible = false;
                    dgvDoc.Columns["WorkHrs"].Visible = false;
                    dgvDoc.Columns["_RowString"].Visible = false;
                    dgvDoc.Columns["POLICY_NO"].Visible = false;
                    dgvDoc.Columns["PRIORITY_TYPE"].Visible = false;

                    dgvDoc.Columns["LATEST_UPDATE_AT"].ValueType = typeof(DateTime);
                    dgvDoc.Columns["CREATE_DATE"].ValueType = typeof(DateTime);
                    dgvDoc.Columns["LATEST_UPDATE_AT"].DefaultCellStyle.Format = "dd'/'MM'/'yyyy HH:mm:ss";
                    dgvDoc.Columns["CREATE_DATE"].DefaultCellStyle.Format = "dd'/'MM'/'yyyy HH:mm:ss";
                    

                    for (int i = 1; i < dgvDoc.Columns.Count; i++)
                    {
                        dgvDoc.Columns[i].ReadOnly = true;
                    }


                    dgvDoc.ClearSelection();
                }

                Cursor.Current = Cursors.AppStarting;


            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                for (int i = 0; i < dgvDoc.RowCount; i++)
                {
                    dgvDoc[0, i].Value = ((CheckBox)dgvDoc.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dgvDoc[0, i].Value;
                    CheckCount(isChecked);
                }
                dgvDoc.EndEdit();

                lblSel.Text = num.ToString();
            }
            catch (Exception EX)
            {
                Msgbox.Show(EX.Message);
            }
        }


        public static void disabledButt(Button bn)
        {
            bn.Enabled = false;
            bn.BackColor = Color.Gray;
        }

        public static void enabledButt(Button bn)
        {
            bn.Enabled = true;
            bn.BackColor = Color.FromArgb(0, 9, 47);
        }

        //private void tbFilterdgvOpen_TextChanged(object sender, EventArgs e)
        //{
        //    //dtDoc.DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", tbFilterdgvDoc.Text);
        //    bool bFound = false;
        //    string strSearch = tbFilterdgvDoc.Text.ToUpper().Trim();
        //    int iIndex = -1, iFirstFoundRow = -1;
        //    dgvDoc.ClearSelection();

        //    if (strSearch == "")
        //    {
        //        dgvDoc.FirstDisplayedScrollingRowIndex = 0;
        //        return;
        //    }

        //    foreach (DataGridViewRow row in dgvDoc.Rows)
        //    {
        //        if ((row.Cells["_RowString"].Value.ToString().ToUpper()).Contains(strSearch))
        //        {
        //            iIndex = row.Index;
        //            if (iFirstFoundRow == -1)  // First row index saved in iFirstFoundRow
        //            {
        //                iFirstFoundRow = iIndex;
        //            }
        //            dgvDoc.Rows[iIndex].Selected = true; // Found row is selected
        //            //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Yellow;
        //            bFound = true; // This is needed to scroll de found rows in display
        //            // break; //uncomment this if you only want the first found row.
        //        }
        //    }
        //    if (bFound == false)
        //    {
        //        dgvDoc.ClearSelection(); // Nothing found clear all Highlights.
        //        //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        // Scroll found rows in display
        //        dgvDoc.FirstDisplayedScrollingRowIndex = iFirstFoundRow;
        //    }
        //}

        private void addToHist(string SelectedDocCode)
        {
            sqlcrud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            requeryDGV();
        }

        private void btnRefreshOpendgv_Click(object sender, EventArgs e)
        {
            requeryDGV();
        }

        private void dgvOpen_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.Rows.Count == 0)
                {
                    using (Graphics g = e.Graphics)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(new Point(), new Size(dgv.Width, 25)));
                        g.DrawString("No document available.", new Font("Roboto", 12), Brushes.Red, new PointF(3, 3));
                    }
                }

                if (notiTriggered && notiDocIndex != -1)
                {
                    dgvDoc.ClearSelection();
                    dgvDoc.Rows[notiDocIndex].Selected = true;
                    notiDocIndex = -1;
                    notiTriggered = false;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        //private string getStatusDesc()
        //{
        //    string statusDesc = string.Empty;
        //    if (status == "0") statusDesc = "Submitted to UW";
        //    else if (status == "1") statusDesc = "Controller Accepted";
        //    else if (status == "2") statusDesc = "DP Processing";
        //    else if (status == "3") statusDesc = "DP Processed";
        //    else if (status == "4") statusDesc = "Pending for Signature";
        //    else if (status == "5") statusDesc = "Packaging";
        //    else if (status == "6") statusDesc = "Packaged";
        //    else if (status == "7") statusDesc = "Done";
        //    else if (status == "8") statusDesc = "Close";
        //    return statusDesc;
        //}


        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            disabledButt(btnChangeStatus);
            disabledButt(btnReassignDP);
            disabledButt(btnCloseReopen);
            disabledButt(btnReturn);
            btnReverse.Visible = false;

            if (status != "7" && status != "8" && status != "99" && !Role.Contains("PRODUCER"))
                enabledButt(btnChangeStatus);
            if (status == "2")
                enabledButt(btnReassignDP);
            if ((status == "0" || status == "8") && (Role.Contains("PRODUCER") || Role.Contains("PCD")))
                enabledButt(btnCloseReopen);
            if ((status == "3" || status == "4" || status == "5" || status == "6") && (Role.Contains("PRODUCER") || Role.Contains("CONTROLLER") || Role.Contains("FILLING") || Role.Contains("PCD")))
                enabledButt(btnReturn);
            if ((status == "1" || status == "4" || status == "5") && (Role.Contains("CONTROLLER") || Role.Contains("PCD")))
                btnReverse.Visible = true;

            requeryDGV();
        }

        private void dgvDoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            num = 0;

            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex != 0)
                return;


            if (dgvDoc.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvDoc.SelectedCells)
                {
                    dgvDoc[0, dgvc.RowIndex].Value = true;
                }
                //tbFilterdgvDoc.Text = "";

                tbFilterdgvDoc.SelectAll();
                for (int i = 0; i < dgvDoc.RowCount; i++)
                {
                    isChecked = (bool)dgvDoc.Rows[i].Cells[0].EditedFormattedValue;
                    CheckCount(isChecked);
                    dgvDoc.Rows[i].Cells[0].Value = isChecked;
                }
                lblSel.Text = num.ToString();
            }
            tbFilterdgvDoc.Focus();
        }

        private void CheckCount(bool isChecked)
        {
            if (isChecked)
                num++;
        }

        private void dgvDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDoc.Columns[0].Index || e.RowIndex < 0) return;
            int row = e.RowIndex;
            var frm = new frmDocumentDetail1();
            frm.DocId = dgvDoc.Rows[row].Cells["REF_ID"].Value.ToString();
            frm.printable = (Role.Contains("PRODUCER") || Role.Contains("UWHEAD")) ? true : false;
            frm.Username = FullName;
            frm.Show();
        }


        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();

            dt.Columns.Add("REF_ID");
            dt.Columns.Add("CUSTOMER");
            dt.Columns.Add("TYPE");
            dt.Columns.Add("PRODUCT_TYPE");

            string status = "";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {
                        dt.Rows.Add(row.Cells["REF_ID"].Value.ToString(), row.Cells["CUSTOMER"].Value.ToString(), row.Cells["TYPE"].Value.ToString(), row.Cells["PRODUCT_TYPE"].Value.ToString());
                    }
                }
            }

            return dt;
        }



        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {

                //Check status allow
                if (!Status.Contains(status))
                {
                    Msgbox.Show("You can't change the status of selected document(s).");
                    return;
                }
                
                DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

                if (selectedDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected record!");
                    return;
                }

                string selectedDocCode = getSelectedDocCode(selectedDoc);

                if (status == "0") //Submitted to UW
                {
                    frmAcceptRejectDoc frm = new frmAcceptRejectDoc();
                    frm.SelectedDoc = selectedDoc;
                    frm.UserName = UserName;
                    frm.UserCode = UserID;
                    SubFrmChange = true;
                    frm.FormClosed += new FormClosedEventHandler(frmClose);
                    frm.Show();
                }
                else if (status == "1") //Controller Accepted
                {
                    //Check Pro Line
                    string ProLine = frmAddDocument1.product[selectedDoc.Rows[0]["PRODUCT_TYPE"].ToString()];
                    for (int i = 0; i < selectedDoc.Rows.Count; i++)
                    {
                        string ProLinetmp = frmAddDocument1.product[selectedDoc.Rows[i]["PRODUCT_TYPE"].ToString()];
                        if (ProLine != ProLinetmp)
                        {
                            Msgbox.Show("The selected document(s) not belong in the same product line.");
                            return;
                        }
                    }
                    //
                    frmAssignDP frm = new frmAssignDP();
                    frm.SelectedDoc = selectedDoc;
                    frm.UserName = UserName;
                    frm.UserCode = UserID;
                    frm.ReAssign = false;
                    SubFrmChange = true;
                    frm.FormClosed += new FormClosedEventHandler(frmClose);
                    frm.Show();
                }
                else if (status == "2") //DP Processing
                {
                    DialogResult dr = Msgbox.Show("Are you sure you already processed " + selectedDoc.Rows.Count + " selected document(s)?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        frmDPRemark frm = new frmDPRemark();
                        frm.UserName = UserName;
                        frm.UserCode = UserID;
                        frm.SelectedDoc = selectedDoc;
                        SubFrmChange = true;
                        //PrintCard = false;
                        //fwdpolno = string.Empty;
                        frm.FormClosed += new FormClosedEventHandler(frmClose);
                        frm.Show();
                    }
                }
                else if (status == "3") //DP Processed
                {
                    DialogResult dr = Msgbox.Show("Is/Are " + selectedDoc.Rows.Count + " selected document(s) now pending for signature?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET DOC_CUR_STATUS = 4, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s)' status changed to PENDING FOR SIGNATURE!");
                        requeryDGV();
                    }
                }
                else if (status == "4") //Pending for Signature
                {
                    DialogResult dr = Msgbox.Show("Is/Are " + selectedDoc.Rows.Count + " selected document(s) now packaging?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET DOC_CUR_STATUS = 5, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s)' status changed to PACKAGING!");
                        requeryDGV();
                    }
                }
                else if (status == "5") //Packaging
                {
                    frmDocPackageComplete frm = new frmDocPackageComplete();
                    frm.UserName = UserName;
                    frm.UserCode = UserID;
                    frm.SelectedDoc = selectedDoc;
                    SubFrmChange = true;
                    frm.FormClosed += new FormClosedEventHandler(frmClose);
                    frm.Show();
                }
                else if (status == "6") //Packaged
                {
                    DialogResult dr = Msgbox.Show("Is/Are " + selectedDoc.Rows.Count + " selected document(s) now completed all processes?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET STATUS = 'C', STATUS_REMARK = 'DONE', DOC_CUR_STATUS = 7, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s)' status changed to DONE!");
                        requeryDGV();
                    }
                }
                else if (status == "13") //Pending At DP
                {
                    DialogResult dr = Msgbox.Show("Is/Are " + selectedDoc.Rows.Count + " selected document(s) now ready for DP to process?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET STATUS_REMARK = '', DOC_CUR_STATUS = 2, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s)' status changed to DP Processing!");
                        requeryDGV();
                    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void tbFilterdgvDoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool bFound = false;
                string strSearch = tbFilterdgvDoc.Text.ToUpper().Trim();
                int iIndex = -1, iFirstFoundRow = -1;
                dgvDoc.ClearSelection();

                if (strSearch == "")
                {
                    if (dgvDoc.Rows.Count > 0)
                        dgvDoc.FirstDisplayedScrollingRowIndex = 0;
                    return;
                }

                foreach (DataGridViewRow row in dgvDoc.Rows)
                {
                    if ((row.Cells["_RowString"].Value.ToString().ToUpper()).Contains(strSearch))
                    {
                        iIndex = row.Index;
                        if (iFirstFoundRow == -1)  // First row index saved in iFirstFoundRow
                        {
                            iFirstFoundRow = iIndex;
                        }
                        dgvDoc.Rows[iIndex].Selected = true; // Found row is selected
                        //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Yellow;
                        bFound = true; // This is needed to scroll de found rows in display
                        // break; //uncomment this if you only want the first found row.
                    }
                }
                if (bFound == false)
                {
                    dgvDoc.ClearSelection(); // Nothing found clear all Highlights.
                    //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // Scroll found rows in display
                    dgvDoc.FirstDisplayedScrollingRowIndex = iFirstFoundRow;
                    if (notiTriggered) notiDocIndex = iFirstFoundRow;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        public static string getSelectedDocCode(DataTable SelectedDoc)
        {
            string SelectedDocCode = string.Empty;
            for (int i = 0; i < SelectedDoc.Rows.Count; i++)
                SelectedDocCode += SelectedDoc.Rows[i]["REF_ID"].ToString() + ",";
            SelectedDocCode = SelectedDocCode.Remove(SelectedDocCode.Length - 1);
            return SelectedDocCode;
            //sample: 1,2,3...
        }

        private void btnReassignDP_Click(object sender, EventArgs e)
        {
            DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

            if (selectedDoc.Rows.Count <= 0)
            {
                Msgbox.Show("No selected record!");
                return;
            }

            if (status != "2")
            {
                Msgbox.Show("You can't re-assign DP for this document right now.");
                return;
            }

            frmAssignDP frm = new frmAssignDP();
            frm.UserName = UserName;
            frm.UserCode = UserID;
            frm.SelectedDoc = selectedDoc;
            frm.ReAssign = true;
            SubFrmChange = true;
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.Show();
        }

        private void btnCloseReopen_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

                if (selectedDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected record!");
                    return;
                }

                string selectedDocCode = getSelectedDocCode(selectedDoc);

                if (status == "0")
                {
                    DialogResult dr = Msgbox.Show("Are you sure you want to cancel " + selectedDoc.Rows.Count + " selected document(s)?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET STATUS = 'C', STATUS_REMARK = 'PRODUCER CLOSED', DOC_CUR_STATUS = 8, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s) canceled!");
                        requeryDGV();
                    }
                }
                else if (status == "8")
                {
                    DialogResult dr = Msgbox.Show("Are you sure you want to re-open " + selectedDoc.Rows.Count + " selected document(s)?", "Confirmation", "Yes", "No");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        addToHist(selectedDocCode);
                        sqlcrud.Executing("UPDATE dbo.tbDOC SET CREATE_DATE = '" + DateTime.Now + "', STATUS = 'O', STATUS_REMARK = 'PRODUCER RE-OPEN', DOC_CUR_STATUS = 0, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + selectedDocCode + "',','))");
                        Msgbox.Show(selectedDoc.Rows.Count + " selected document(s) re-opened!");
                        requeryDGV();
                    }
                }
                else
                {
                    Msgbox.Show("You can't cancel/re-open selected document(s) right now.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private double getWorkingHour(DateTime start, DateTime end)
        {
            //1st Half Working Hour: 8-12
            double lower = 8f / 24f, upper = 12f / 24f;
            double firstHalf = WorkingHourAlgorithm(start, end, lower, upper);
            //

            //2nd Half Working Hour: 1-5:30
            lower = 13f / 24f;
            upper = 17.5f / 24f;
            double secondHalf = WorkingHourAlgorithm(start, end, lower, upper);
            //

            double workingHour = Math.Floor((firstHalf + secondHalf) * 24); //Convert to hour only

            return workingHour;
        }

        private double WorkingHourAlgorithm(DateTime start, DateTime end, double lower, double upper)
        {
            //https://exceljet.net/formula/get-work-hours-between-dates-and-times for more detail on the algorithm

            IExcel.WorksheetFunction xlfunc = xlprog.WorksheetFunction;

            double result = (xlfunc.NetworkDays(start.ToOADate(), end.ToOADate(), holiday) - 1) * (upper - lower) + xlfunc.Median(end.ToOADate() % 1, upper, lower) - Math.Abs(xlfunc.Median(start.ToOADate() % 1, upper, lower));
            //xlprog.Quit();
            return result;
        }

        private double[] getHolidayinOADate()
        {
            DataTable dtTemp = sqlcrud.LoadData("SELECT Date FROM dbo.tbHoliday WHERE Date >= '" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + "'").Tables[0];
            double[] holidayinOADate = null;
            if (dtTemp.Rows.Count > 0)
            {
                holidayinOADate = new double[dtTemp.Rows.Count];
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    holidayinOADate[i] = Convert.ToDateTime(dtTemp.Rows[i][0]).ToOADate();
                }
            }
            return holidayinOADate;
        }


        private double getStatusTimeline(string status)
        {
            if (status == "0" || status == "1" || status == "3")
                return 1;
            else if (status == "2" || status == "5") return 4;
            else if (status == "4") return 8;
            else return 0;
        }

        private void dgvDoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvDoc.Rows[e.RowIndex].Cells["PRIORITY_TYPE"].Value.ToString().Trim().Equals("L"))
                {
                    //e.CellStyle.SelectionBackColor = Color.Gray;
                    //e.CellStyle.SelectionForeColor = Color.Black;
                    return;
                }
                    
                double statusTimeline = getStatusTimeline(docStatus.FirstOrDefault(x => x.Value == dgvDoc.Rows[e.RowIndex].Cells["STATUS"].Value.ToString()).Key);// get Key with Value

                if (statusTimeline > 0) //if = 0 means status with no timeline <=> status 0->6
                {
                    string ReturnReason = dgvDoc.Rows[e.RowIndex].Cells["RETURN_REASON"].Value.ToString().Trim();

                    if (Convert.ToDouble(dgvDoc.Rows[e.RowIndex].Cells["WorkHrs"].Value) > statusTimeline)  
                    {
                        //dgvDoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        //dgvDoc.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;

                        if (ReturnReason == "PRODUCER")
                        {
                            e.CellStyle.BackColor = Color.Khaki;
                            e.CellStyle.ForeColor = Color.Black;
                            //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // Yellow
                            //e.CellStyle.SelectionForeColor = Color.White;
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.FromArgb(195, 39, 43);
                            e.CellStyle.ForeColor = Color.White;
                            //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // Red
                        }
                    }
                    else
                    {
                        //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // White
                        //e.CellStyle.SelectionForeColor = Color.White; // White
                    }

                    //Check Return Doc Over Timeline
                    
                    if (ReturnReason == "DP")
                    {
                        DateTime ReturnDate = Convert.ToDateTime(dgvDoc.Rows[e.RowIndex].Cells["RETURN_DATE"].Value);
                        if (ReturnDate.Hour >= 0 && ReturnDate.Hour <= 11) //AM
                        {
                            DateTime ReturnDateOn23 = new DateTime(ReturnDate.Year, ReturnDate.Month, ReturnDate.Day, 23, 59, 59);
                            if (!(DateTime.Compare(DateTime.Now, ReturnDate) >= 0 && DateTime.Compare(DateTime.Now, ReturnDateOn23) <= 0))
                            {
                                e.CellStyle.BackColor = Color.Khaki;
                                e.CellStyle.ForeColor = Color.Black;
                                //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // Yellow
                                //e.CellStyle.SelectionForeColor = Color.White;
                            }
                        }
                        else if (ReturnDate.Hour >= 12 && ReturnDate.Hour <= 23)
                        {
                            DateTime ReturnDateisNextDayOn11 = ReturnDate.AddDays(1);
                            while (true)
                            {
                                if ((ReturnDateisNextDayOn11.DayOfWeek != DayOfWeek.Saturday && ReturnDateisNextDayOn11.DayOfWeek != DayOfWeek.Sunday) && (!isHoliday(ReturnDateisNextDayOn11)))
                                {
                                    break;
                                }
                                else ReturnDateisNextDayOn11 = ReturnDateisNextDayOn11.AddDays(1);
                            }

                            ReturnDateisNextDayOn11 = new DateTime(ReturnDateisNextDayOn11.Year, ReturnDateisNextDayOn11.Month, ReturnDateisNextDayOn11.Day, 11, 59, 59);

                            if (!(DateTime.Compare(DateTime.Now, ReturnDate) >= 0 && DateTime.Compare(DateTime.Now, ReturnDateisNextDayOn11) <= 0))
                            {
                                //dgvDoc.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                                //dgvDoc.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                                e.CellStyle.BackColor = Color.Khaki;
                                e.CellStyle.ForeColor = Color.Black;
                                //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // Yellow
                                //e.CellStyle.SelectionForeColor = Color.White;
                            }
                        }
                        else
                        {
                            //e.CellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153); // White
                            //e.CellStyle.SelectionForeColor = Color.White; // White
                        }
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

            if (selectedDoc.Rows.Count <= 0)
            {
                Msgbox.Show("No selected record!");
                return;
            }

            frmDocumentReturn frm = new frmDocumentReturn();
            frm.UserID = UserID;
            frm.SelectedDoc = selectedDoc;
            SubFrmChange = true;
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.Show();
        }

        private bool isHoliday(DateTime dt)
        {
            DateTime dtResetTime = new DateTime(dt.Year, dt.Month, dt.Day);
            bool isholiday = false;
            for (int i = 0; i < holiday.Count(); i++)
            {
                if (dtResetTime.ToOADate() == holiday[i])
                {
                    isholiday = true;
                    break;
                }
            }
            return isholiday;
        }

        private void frmDocumentControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (xlprog != null)
                {
                    xlprog.Application.Quit();
                    xlprog.Quit();
                    Marshal.ReleaseComObject(xlprog);
                    Marshal.FinalReleaseComObject(xlprog);
                    xlprog = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

                if (selectedDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected record!");
                    return;
                }

                string SelectedDocCode = getSelectedDocCode(selectedDoc);

                DialogResult dr = Msgbox.Show("Are you sure you want to reverse " + selectedDoc.Rows.Count + " selected document(s)' status?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    for (int i = 0; i < selectedDoc.Rows.Count; i++)
                    {
                        sqlcrud.ExecuteMySql("dbo.sp_reverse_doc_status", "@DocCode", selectedDoc.Rows[i]["REF_ID"].ToString());
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO dbo.tbNoti(NOTI_DETAIL, NOTI_TO, NOTI_DATE, REMARK, NOTI_TYPE) SELECT 'Instruction Note No \"' + DOC_CODE + '\" has been reversed by " + frmLogIn.Usert + "', (SELECT USER_NAME FROM dbo.tbDOC_USER WHERE FULL_NAME = CREATE_BY), getdate(), DOC_CODE, '" + CommonFunctions.NotiType.REVERSED + "' FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                    sqlcrud.Executing(cmd);
                }
                Msgbox.Show(selectedDoc.Rows.Count + " selected document(s)' status reversed.");
                requeryDGV();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void setSelectedStatus(Button selectedBtn)
        {
            if (status == "99" || status == "7")
            {
                gbAllRecordOption.Visible = true;
                rbSpecificDate.Checked = true;
                rbAll.Checked = false;
                dtpSpecificDateFr.Value = DateTime.Now;
                dtpSpecificDateTo.Value = DateTime.Now;
            }
            else
            {
                gbAllRecordOption.Visible = false;
                rbSpecificDate.Checked = false;
                rbAll.Checked = true;
            }

            unselectStatusBtnStyle(btnSubmittedtoUW);
            unselectStatusBtnStyle(btnControllerAccepted);
            unselectStatusBtnStyle(btnDPProcessing);
            unselectStatusBtnStyle(btnDPProcessed);
            unselectStatusBtnStyle(btnPendingforSign);
            unselectStatusBtnStyle(btnPackaging);
            unselectStatusBtnStyle(btnPackaged);
            unselectStatusBtnStyle(btnCancel);
            unselectStatusBtnStyle(btnDone);
            unselectStatusBtnStyle(btnAll);
            unselectStatusBtnStyle(btnPendingAtDP);

            selectedStatusBtnStyle(selectedBtn);
            currentSelectedButton = selectedBtn.Name;

            enabledisableFuncBtn();

            requeryDGV();

            tbFilterdgvDoc.Text = "";
            rbRefID.Checked = true;

            ActiveControl = tbFilterdgvDoc;
        }

        private void selectedStatusBtnStyle(Button selectedBtn)
        {
            selectedBtn.BackColor = Color.FromArgb(0, 9, 47);
            selectedBtn.ForeColor = Color.White;
        }

        private void unselectStatusBtnStyle(Button unselectedBtn)
        {
            unselectedBtn.BackColor = Color.Gainsboro;
            unselectedBtn.ForeColor = Color.Black;
        }

        private void btnSubmittedtoUW_Click(object sender, EventArgs e)
        {
            status = "0";
            setSelectedStatus((Button)sender);
        }

        private void btnControllerAccepted_Click(object sender, EventArgs e)
        {
            status = "1";
            setSelectedStatus((Button)sender);
        }

        private void btnDPProcessing_Click(object sender, EventArgs e)
        {
            status = "2";
            setSelectedStatus((Button)sender);
        }

        private void btnDPProcessed_Click(object sender, EventArgs e)
        {
            status = "3";
            setSelectedStatus((Button)sender);

        }

        private void btnPendingforSign_Click(object sender, EventArgs e)
        {
            status = "4";
            setSelectedStatus((Button)sender);

        }

        private void btnPackaging_Click(object sender, EventArgs e)
        {
            status = "5";
            setSelectedStatus((Button)sender);

        }

        private void btnPackaged_Click(object sender, EventArgs e)
        {
            status = "6";
            setSelectedStatus((Button)sender);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            status = "7";
            setSelectedStatus((Button)sender);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            status = "8";
            setSelectedStatus((Button)sender);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            status = "99";
            setSelectedStatus((Button)sender);
        }

        private void btnPendingAtDP_Click(object sender, EventArgs e)
        {
            status = "13";
            setSelectedStatus((Button)sender);
        }

        private void enabledisableFuncBtn()
        {
            disabledButt(btnChangeStatus);
            disabledButt(btnReassignDP);
            disabledButt(btnCloseReopen);
            disabledButt(btnReturn);
            btnReverse.Visible = false;
            btnDPPendingRemark.Visible = false;
            //btnExportRecord.Enabled = false;
            btnReport.Visible = false;
            btnPrint.Visible = false;

            //if (status != "7" && status != "8" && status != "99" && !Role.Contains("PRODUCER".Contains))
            if (status != "7" && status != "8" && status != "99")
                enabledButt(btnChangeStatus);
            if (status == "2" && (Role.Contains("CONTROLLER") || Role.Contains("UWHEAD") || Role.Contains("PCD")))
                enabledButt(btnReassignDP);
            if ((status == "0" || status == "8") && (Role.Contains("PRODUCER") || Role.Contains("UWHEAD") || Role.Contains("PCD")))
                enabledButt(btnCloseReopen);
            if ((status == "2" || status == "3" || status == "4" || status == "5" || status == "6") && (Role.Contains("PRODUCER") || Role.Contains("CONTROLLER") || Role.Contains("FILLING") || Role.Contains("UWHEAD") || Role.Contains("PCD") || Role.Contains("UNW")))
                enabledButt(btnReturn);
            if ((status == "1" || status == "4") && (Role.Contains("CONTROLLER") || Role.Contains("UWHEAD") || Role.Contains("PCD")))
                btnReverse.Visible = true;
            if ((status == "2" || status == "13") && (Role.Contains("DP") || Role.Contains("UWHEAD") || Role.Contains("PCD")))
                btnDPPendingRemark.Visible = true;
            //if ((status == "99") || (status == "4" && (Role.Contains("CONTROLLER") || Role.Contains("UWHEAD"))))
            //    btnExportRecord.Enabled = true;

            if (Role.Contains("PRODUCER") && status == "6")
                enabledButt(btnChangeStatus);

            if (Role.Contains("CONTROLLER") && status == "8")
                enabledButt(btnCloseReopen);

            if (Role.Contains("UWHEAD"))
                btnReport.Visible = true;

            if (Role.Contains("UWHEAD") || Role.Contains("PRODUCER") || Role.Contains("PCD"))
                btnPrint.Visible = true;
        }

        private void btnManageCrono_Click(object sender, EventArgs e)
        {
            frmManageCrono frm = new frmManageCrono();
            SubFrmChange = true;
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.Show();
        }

        private void frmDocumentControl_Activated(object sender, EventArgs e)
        {
            tbFilterdgvDoc.Focus();
        }

        private void btnDPPendingRemark_Click(object sender, EventArgs e)
        {
            if (!Role.Contains("DP") && !Role.Contains("UWHEAD") && !Role.Contains("PCD"))
            {
                Msgbox.Show("You can't set Pending Remark.");
                return;
            }
            DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);
            if (selectedDoc.Rows.Count <= 0)
            {
                Msgbox.Show("No selected record!");
                return;
            }
            frmDPPendingRemark frm = new frmDPPendingRemark();
            frm.UserName = UserName;
            frm.UserCode = UserID;
            if (status == "13") frm.isRevised = true;
            frm.SelectedDoc = selectedDoc;
            SubFrmChange = true;
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.Show();
        }

        private void btnExportRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No record to export!", "Information");
                    return;
                }


                Cursor.Current = Cursors.WaitCursor;

                //Check Whether user has selected any records
                DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

                var dt = new DataTable();
                dt.Columns.Add("REF_ID");
                string Question = "";


                if (selectedDoc.Rows.Count <= 0) //no selected records
                {
                    foreach (DataGridViewRow row in dgvDoc.Rows)
                    {
                        dt.Rows.Add(row.Cells["REF_ID"].Value.ToString());
                    }
                    Question = "Are you sure you want to export all record(s)?";
                }
                else
                {
                    foreach (DataRow row in selectedDoc.Rows)
                    {
                        dt.Rows.Add(row["REF_ID"].ToString());
                    }
                    Question = "Are you sure you want to export "+selectedDoc.Rows.Count+" selected record(s)?";
                }
                //                

                string dgvOpensqlstring = "select * from dbo.VIEW_EXPORT WHERE REF_ID in (SELECT * FROM FNC_SPLIT('" + getSelectedDocCode(dt) + "',','))";



                DialogResult dr = Msgbox.Show(Question, "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.No)
                    return;

                DataTable AllRecorddt = sqlcrud.LoadData(dgvOpensqlstring + " order by REF_ID asc, CREATE_DATE asc").Tables[0];
                MyDB mid01crud = new MyDB();
                SqlCommand cmd = new SqlCommand();
                DateTime createdate = new DateTime();
                DataTable dtTemp = new DataTable();
                AllRecorddt.Columns.Add("SENT_ON", typeof(System.DateTime)); //add Sent_On
                AllRecorddt.Columns.Add("PRINTED_ON", typeof(System.DateTime)); //add Print_On
                foreach (DataRow row in AllRecorddt.Rows)
                {
                    if (row["PRINT_CARD"].ToString() == "Yes")
                    {
                        
                        //Check & Get CARD_SENT CARD_PRINTED status
                        string DocId = row["REF_ID"].ToString(), CusName = row["CUSTOMER"].ToString(), PolicyNo = row["POLICY_NO"].ToString();
                        DataTable temp = sqlcrud.LoadData("SELECT DP_CODE FROM dbo.tbDOC WHERE DOC_CODE = '" + DocId + "' and DP_CODE is not null").Tables[0];
                        string DPcode = "";
                        if (temp.Rows.Count > 0)
                        {
                            DPcode = temp.Rows[0][0].ToString();
                        }

                        if (DPcode != "")
                        {
                            temp = sqlcrud.LoadData("SELECT * FROM dbo.tbDOC_HIST WHERE DOC_CODE = '" + DocId + "' AND DOC_STATUS = 10").Tables[0]; //Check Card Printed History
                            if (temp.Rows.Count == 0) //Don't have Card Printed History
                            {
                                cmd = new SqlCommand();
                                cmd.CommandText = "SELECT * FROM dbo.VIEW_PRINTED_DATE WHERE printed_on >= '" + createdate + "' AND policy_holder like '%' + @cusname + '%' AND policy_no = @policyno order by printed_on desc";
                                cmd.Parameters.Add(new SqlParameter("cusname", CusName));
                                cmd.Parameters.Add(new SqlParameter("policyno", PolicyNo));
                                dt = mid01crud.ExecQuery(cmd);
                                if (dt.Rows.Count > 0)
                                {
                                    sqlcrud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) VALUES('" + DocId + "','" + dt.Rows[0]["printed_on"].ToString() + "',10,'" + DPcode + "','" + dt.Rows[0]["printed_on"].ToString() + "')");
                                }
                            }

                            temp = sqlcrud.LoadData("SELECT * FROM dbo.tbDOC_HIST WHERE DOC_CODE = '" + DocId + "' AND DOC_STATUS = 17").Tables[0]; //Check Card Sent History
                            if (temp.Rows.Count == 0) //Don't have Card Sent History
                            {
                                cmd = new SqlCommand();
                                cmd.CommandText = "SELECT * FROM dbo.VIEW_CREATED_DATE WHERE created_date >= '" + createdate + "' AND policy_holder like '%' + @cusname + '%' AND policy_no = @policyno order by created_date desc";
                                cmd.Parameters.Add(new SqlParameter("cusname", CusName));
                                cmd.Parameters.Add(new SqlParameter("policyno", PolicyNo));
                                dt = mid01crud.ExecQuery(cmd);
                                if (dt.Rows.Count > 0)
                                {
                                    sqlcrud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) VALUES('" + DocId + "','" + dt.Rows[0]["created_date"].ToString() + "',17,'" + DPcode + "','" + dt.Rows[0]["created_date"].ToString() + "')");
                                }
                            }
                        }
                        //

                        //createdate = DateTime.ParseExact(row["CREATE_DATE"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        //cmd = new SqlCommand();
                        //cmd.CommandText = "SELECT * FROM dbo.VIEW_PRINTED_DATE WHERE printed_on > '" + createdate + "' AND policy_holder like '%' + @cusname + '%' AND policy_no like '%' + @protype + '%' order by printed_on desc";
                        //cmd.Parameters.Add(new SqlParameter("cusname", row["CUSTOMER"].ToString()));
                        //cmd.Parameters.Add(new SqlParameter("protype", row["PRODUCT_TYPE"].ToString()));
                        //dtTemp = mid01crud.ExecQuery(cmd);
                        //if (dtTemp.Rows.Count > 0)
                        //{
                        //    row["PRINTED_ON"] = dtTemp.Rows[0]["printed_on"];
                        //}
                        dtTemp = sqlcrud.LoadData("SELECT DOC_STATUS_SET_ON,DOC_STATUS FROM dbo.tbDOC_HIST WHERE DOC_CODE = '"
                            + DocId + "' AND DOC_STATUS IN (10,17)").Tables[0];
                        if (dtTemp.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtTemp.Rows)
                            {
                                if (r["DOC_STATUS"].ToString() == "10")
                                    row["PRINTED_ON"] = r["DOC_STATUS_SET_ON"];
                                else if (r["DOC_STATUS"].ToString() == "17")
                                    row["SENT_ON"] = r["DOC_STATUS_SET_ON"];
                            }
                        }
                    }
                }
                //

                My_DataTable_Extensions.ExportToExcel(AllRecorddt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
                Cursor.Current = Cursors.AppStarting;
            }
        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            tbFilterdgvDoc.Text = "";
            if (dgvDoc.Columns["CUSTOMER"] != null && dgvDoc.Columns["REF_ID"] != null)
            {
                if (rbCustomer.Checked)
                {
                    dgvDoc.Sort(dgvDoc.Columns["CUSTOMER"], ListSortDirection.Ascending);
                }
                else if (rbRefID.Checked)
                {
                    dgvDoc.Sort(dgvDoc.Columns["REF_ID"], ListSortDirection.Ascending);
                }
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            //requeryDGV();
        }

        private void dgvDoc_DataSourceChanged(object sender, EventArgs e)
        {
            lblTot.Text = dgvDoc.RowCount.ToString();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            var frm = new frmDCSMainReport();
            frm.Team = this.Team;
            frm.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable selectedDoc = GetDataTableFromDGV(dgvDoc);

                if (selectedDoc.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected record!");
                    return;
                }

                string printedDocCodeANH = "", printedDocCodeAuto = "", printedDocCodeFLM = "", printedDocCodePNE = "", printedDocCodeFNL = "", printedDocCodePME = "", printedDocCodeMICR="";
                foreach (DataRow dr in selectedDoc.Rows)
                {
                    switch (frmAddDocument1.product[dr["PRODUCT_TYPE"].ToString()])
                    {
                        case "A&H":
                            printedDocCodeANH += "'" + dr["REF_ID"] + "',";
                            break;
                        case "AUTO":
                            printedDocCodeAuto += "'" + dr["REF_ID"] + "',";
                            break;
                        //case "FLM":
                        //    printedDocCodeFLM += "'" + dr["REF_ID"] + "',";
                        //    break;
                        //case "P&E":
                        //    printedDocCodePNE += "'" + dr["REF_ID"] + "',";
                        //    break;
                        case "PE&M":
                            printedDocCodePME += "'" + dr["REF_ID"] + "',";
                            break;
                        case "FL":
                            printedDocCodeFNL += "'" + dr["REF_ID"] + "',";
                            break;
                        case "MICR":
                            printedDocCodeMICR += "'" + dr["REF_ID"] + "',";
                            break;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;
                if (printedDocCodeANH != "")
                {
                    printedDocCodeANH = printedDocCodeANH.Remove(printedDocCodeANH.Length - 1);
                    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeANH + ")").Tables[0];
                    Reports.InstructionNoteANH rpt = new Reports.InstructionNoteANH();
                    dtTemp.Columns.Add("CRIN", typeof(System.String));
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                    }
                    rpt.SetDataSource(dtTemp);
                    var frm = new frmViewInstructionNote();
                    frm.rpt = rpt;
                    frm.Show();
                }
                if (printedDocCodeAuto != "")
                {
                    printedDocCodeAuto = printedDocCodeAuto.Remove(printedDocCodeAuto.Length - 1);
                    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeAuto + ")").Tables[0];
                    Reports.InstructionNoteAuto rpt = new Reports.InstructionNoteAuto();
                    dtTemp.Columns.Add("CRIN", typeof(System.String));
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                    }
                    rpt.SetDataSource(dtTemp);
                    var frm = new frmViewInstructionNote();
                    frm.rpt = rpt;
                    frm.Show();
                }
                //if (printedDocCodeFLM != "")
                //{
                //    printedDocCodeFLM = printedDocCodeFLM.Remove(printedDocCodeFLM.Length - 1);
                //    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeFLM + ")").Tables[0];
                //    Reports.InstructionNoteFLM rpt = new Reports.InstructionNoteFLM();
                //    dtTemp.Columns.Add("CRIN", typeof(System.String));
                //    foreach (DataRow row in dtTemp.Rows)
                //    {
                //        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                //    }
                //    rpt.SetDataSource(dtTemp);
                //    var frm = new frmViewInstructionNote();
                //    frm.rpt = rpt;
                //    frm.Show();
                //}
                //if (printedDocCodePNE != "")
                //{
                //    printedDocCodePNE = printedDocCodePNE.Remove(printedDocCodePNE.Length - 1);
                //    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodePNE + ")").Tables[0];
                //    Reports.InstructionNotePNE rpt = new Reports.InstructionNotePNE();
                //    dtTemp.Columns.Add("CRIN", typeof(System.String));
                //    foreach (DataRow row in dtTemp.Rows)
                //    {
                //        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                //    }
                //    rpt.SetDataSource(dtTemp);
                //    var frm = new frmViewInstructionNote();
                //    frm.rpt = rpt;
                //    frm.Show();
                //}
                if (printedDocCodeFNL != "")
                {
                    printedDocCodeFNL = printedDocCodeFNL.Remove(printedDocCodeFNL.Length - 1);
                    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeFNL + ")").Tables[0];
                    Reports.InstructionNoteFNL rpt = new Reports.InstructionNoteFNL();
                    dtTemp.Columns.Add("CRIN", typeof(System.String));
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                    }
                    rpt.SetDataSource(dtTemp);
                    var frm = new frmViewInstructionNote();
                    frm.rpt = rpt;
                    frm.Show();
                }
                if (printedDocCodePME != "")
                {
                    printedDocCodePME = printedDocCodePME.Remove(printedDocCodePME.Length - 1);
                    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodePME + ")").Tables[0];
                    Reports.InstructionNotePME rpt = new Reports.InstructionNotePME();
                    dtTemp.Columns.Add("CRIN", typeof(System.String));
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                    }
                    rpt.SetDataSource(dtTemp);
                    var frm = new frmViewInstructionNote();
                    frm.rpt = rpt;
                    frm.Show();
                }
                if (printedDocCodeMICR != "")
                {
                    printedDocCodeMICR = printedDocCodeMICR.Remove(printedDocCodeMICR.Length - 1);
                    DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeMICR + ")").Tables[0];
                    Reports.InstructionNoteMicr rpt = new Reports.InstructionNoteMicr();
                    dtTemp.Columns.Add("CRIN", typeof(System.String));
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                    }
                    rpt.SetDataSource(dtTemp);
                    var frm = new frmViewInstructionNote();
                    frm.rpt = rpt;
                    frm.Show();
                }

                Cursor.Current = Cursors.AppStarting;
                
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblNotiCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            try
            {
                if (pNotification.Visible)
                {
                    pNotification.Visible = false;
                    dgvNoti.SelectionChanged -= dgvNoti_SelectionChanged;
                    return;
                }

                pNotification.Visible = true;
                dgvNoti.SelectionChanged += dgvNoti_SelectionChanged;

                dgvNoti.DataSource = null;
                dgvNoti.DataSource = _dtNoti;
                dgvNoti.Columns["SEQ_NO"].Visible = false;
                dgvNoti.Columns["NOTI_TO"].Visible = false;
                dgvNoti.Columns["NOTI_DATE"].Visible = false;
                dgvNoti.Columns["REMARK"].Visible = false;
                dgvNoti.Columns["IS_READ"].Visible = false;
                dgvNoti.Columns["NOTI_TYPE"].Visible = false;

                btnReject_Click(null, null);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dgvNoti_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvNoti.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgvNoti.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvNoti.Rows[selectedrowindex];
                    string seqNo = selectedRow.Cells["SEQ_NO"].Value.ToString();

                    string sql = "UPDATE [DocumentControlDB].[dbo].[tbNoti] SET IS_READ = 1 WHERE SEQ_NO = '" + seqNo + "'";
                    sqlcrud.Executing(sql);

                    selectedRow.Cells["IS_READ"].Value = true;

                    DataTable dt = new DataTable();
                    dt = sqlcrud.LoadData("SELECT COUNT(SEQ_NO) AS TOTAL_NOTI FROM (SELECT TOP 50 * FROM [DocumentControlDB].[dbo].[tbNoti] WHERE NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC) t WHERE IS_READ = 0").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        int notiCount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            notiCount += Convert.ToInt32(dt.Rows[i]["TOTAL_NOTI"]);
                        }
                        lblNotiCount.Visible = true;
                        lblNotiCount.Text = Convert.ToInt32(notiCount).ToString();

                        if (notiCount == 0)
                        {
                            lblNotiCount.Visible = false;
                            btnNotification.Image = Resources.notification_unscreen_1;
                        }
                        else
                            btnNotification.Image = Resources.notification_unscreen;
                    }
                }
                dgvNoti_CellFormatting(null, null);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void timerNoti_Tick(object sender, EventArgs e)
        {
            try
            {
                _dtNoti = sqlcrud.LoadData("SELECT TOP 50 * FROM [DocumentControlDB].[dbo].[tbNoti] WHERE NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC").Tables[0];
                dgvNoti.DataSource = null;
                dgvNoti.DataSource = _dtNoti;
                dgvNoti.Columns["SEQ_NO"].Visible = false;
                dgvNoti.Columns["NOTI_TO"].Visible = false;
                dgvNoti.Columns["NOTI_DATE"].Visible = false;
                dgvNoti.Columns["REMARK"].Visible = false;
                dgvNoti.Columns["IS_READ"].Visible = false;

                DataTable dt = new DataTable();
                dt = sqlcrud.LoadData("SELECT COUNT(SEQ_NO) AS TOTAL_NOTI FROM (SELECT TOP 50 * FROM [DocumentControlDB].[dbo].[tbNoti] WHERE NOTI_TO = '" + UserName + "' ORDER BY NOTI_DATE DESC) t WHERE IS_READ = 0").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int notiCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        notiCount += Convert.ToInt32(dt.Rows[i]["TOTAL_NOTI"]);
                    }
                    lblNotiCount.Visible = true;
                    lblNotiCount.Text = Convert.ToInt32(notiCount).ToString();

                    if (notiCount == 0)
                    {
                        lblNotiCount.Visible = false;
                        btnNotification.Image = Resources.notification_unscreen_1;
                    }
                    else
                        btnNotification.Image = Resources.notification_unscreen;
                        
                }

                NotiSentDuration();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dgvNoti_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dgvNoti.Rows)
            {
                if (Convert.ToBoolean(row.Cells["IS_READ"].Value) == false)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    row.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    row.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular);
                }
            }
        }

        private void dgvNoti_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string remark = dgvNoti.Rows[e.RowIndex].Cells["REMARK"].Value.ToString().Trim();
            string btnName = string.Empty;

            var curStatus = sqlcrud.LoadData("select * from tbDOC where DOC_CODE = '" + remark + "'").Tables[0];
            if (curStatus.Rows.Count > 0)
            {
                string cStatus = curStatus.Rows[0]["DOC_CUR_STATUS"].ToString();
                var buttons = sqlcrud.LoadData("select top 1 BUTTON_NAME from tbDOC_STATUS where STATUS_TYPE = '" + cStatus + "'").Tables[0];
                if (buttons.Rows.Count > 0)
                {
                    btnName = buttons.Rows[0]["BUTTON_NAME"].ToString();
                    status = cStatus;
                } 
            }
            else
            {
                Msgbox.Show("No record found with Instruction No " + remark + ".");
                return;
            }

            var btn = this.Controls.Find(btnName, true);
            var selectedBtn = (Button)btn[0];

            if (!selectedBtn.Name.Equals(currentSelectedButton))
                setSelectedStatus(selectedBtn);

            tbFilterdgvDoc.Text = remark;

            if (e.ColumnIndex == dgvNoti.Columns[0].Index || e.RowIndex < 0) return;
            int row = e.RowIndex;
            var frm = new frmDocumentDetail1();
            frm.DocId = dgvNoti.Rows[row].Cells["REMARK"].Value.ToString();
            frm.printable = (Role.Contains("PRODUCER") || Role.Contains("UWHEAD")) ? true : false;
            frm.Username = FullName;
            frm.Show();
        }

        private void NotiSentDuration()
        {
            for (int i = 0; i < _dtNoti.Rows.Count; i++)
            {
                DataRow dr = _dtNoti.Rows[i];
                var notiDate = Convert.ToDateTime(dr["NOTI_DATE"]);
                var dateNow = DateTime.Now;
                var dateSent = (dateNow - notiDate).TotalDays;

                if (dateSent > 1)
                {
                    var tempTimeString = string.Empty;
                    if (dateSent <= 29)
                    {
                        dateSent = Math.Floor(dateSent);
                        tempTimeString = string.Concat(dateSent.ToString(), dateSent >= 2 ? " days ago" : " day ago");
                    }
                    else
                    {
                        var month = Math.Floor(dateSent / 30) <= 0 ? 1 : Math.Floor(dateSent / 30);
                        if (month < 12)
                            tempTimeString = string.Concat(Math.Floor(month).ToString(), month >= 2 ? " months ago" : " month ago");
                        else
                        {
                            month = Math.Floor(month / 12) <= 0 ? 1 : Math.Floor(month / 12);
                            tempTimeString = string.Concat(Math.Floor(month).ToString(), month >= 2 ? " years ago" : " year ago");
                        }

                    }

                    dr["NOTI_DETAIL"] = string.Concat(dr["NOTI_DETAIL"], Environment.NewLine, "------ ", tempTimeString);
                    dr.AcceptChanges();
                }
                else
                {
                    var tempDateSent = (dateNow - notiDate).TotalHours < 1 ? 0 : (dateNow - notiDate).TotalHours;
                    dateSent = tempDateSent;

                    if (tempDateSent >= 1)
                    {
                        dr["NOTI_DETAIL"] = string.Concat(dr["NOTI_DETAIL"], Environment.NewLine, "------ ", Math.Floor(dateSent).ToString(), dateSent >= 2 ? " hours ago" : " hour ago");
                        dr.AcceptChanges();
                    }
                }

                if (dateSent < 1)
                {
                    dateSent = Math.Floor((dateNow - notiDate).TotalMinutes) == 0 ? 1 : Math.Floor((dateNow - notiDate).TotalMinutes);
                    dr["NOTI_DETAIL"] = string.Concat(dr["NOTI_DETAIL"], Environment.NewLine, "------ ", dateSent.ToString(), dateSent >= 2 ? " minutes ago" : " minute ago");
                    dr.AcceptChanges();
                }
            }
        }

        private void cboColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Rectangle rectangle = e.Bounds;

            if (e.Index >= 0)
            {
                var rgbColor = ((ComboBox)sender).Items[e.Index].ToString().Split(',');

                var r = Convert.ToInt32(rgbColor[0]);
                var g = Convert.ToInt32(rgbColor[1]);
                var b = Convert.ToInt32(rgbColor[2]);

                Color color = Color.FromArgb(r, g, b);
                Brush brush = new SolidBrush(color);
                graphic.FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
        }

        private void cboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionColor = cboColor.SelectedItem.ToString();

            var selectionColor = cboColor.SelectedItem.ToString().Split(',');
            var r = Convert.ToInt32(selectionColor[0]);
            var g = Convert.ToInt32(selectionColor[1]);
            var b = Convert.ToInt32(selectionColor[2]);

            if (cboColor.SelectedItem.ToString().Equals("51,204,255"))
            {
                dgvDoc.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvNoti.DefaultCellStyle.SelectionForeColor = Color.Black;
            } 
            else
            {
                dgvDoc.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvNoti.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            dgvDoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(r, g, b);
            dgvNoti.DefaultCellStyle.SelectionBackColor = Color.FromArgb(r, g, b);

            sqlcrud.Executing("update tbDOC_USER set SELECTION_COLOR = '" + cboColor.SelectedItem.ToString() + "' where USER_NAME = '" + UserName + "'");
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            isClickReject = true;

            pReject.BackColor = Color.Brown;
            pPending.BackColor = Color.Gainsboro;
            pReverse.BackColor = Color.Gainsboro;

            btnReject_MouseLeave(null, null);

            filterNoti(CommonFunctions.NotiType.REJECTED);
        }

        private void btnReject_MouseHover(object sender, EventArgs e)
        {
            btnReject.ForeColor = Color.Brown;
        }

        private void btnReject_MouseLeave(object sender, EventArgs e)
        {
            if (isClickReject)
            {
                isClickPending = false;
                isClickReverse = false;

                btnReject.ForeColor = Color.Brown;
                btnPending.ForeColor = Color.Black;
                btnReverseNoti.ForeColor = Color.Black;
            }
            else
                btnReject.ForeColor = Color.Black;
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            isClickPending = true;

            pReject.BackColor = Color.Gainsboro;
            pPending.BackColor = Color.Brown;
            pReverse.BackColor = Color.Gainsboro;

            btnPending_MouseLeave(null, null);

            filterNoti(CommonFunctions.NotiType.PENDING);
        }

        private void btnPending_MouseHover(object sender, EventArgs e)
        {
            btnPending.ForeColor = Color.Brown;
        }

        private void btnPending_MouseLeave(object sender, EventArgs e)
        {
            if (isClickPending)
            {
                isClickReject = false;
                isClickReverse = false;

                btnReject.ForeColor = Color.Black;
                btnPending.ForeColor = Color.Brown;
                btnReverseNoti.ForeColor = Color.Black;
            }
            else
                btnPending.ForeColor = Color.Black;
        }

        private void btnReverseNoti_Click(object sender, EventArgs e)
        {
            isClickReverse = true;

            pReject.BackColor = Color.Gainsboro;
            pPending.BackColor = Color.Gainsboro;
            pReverse.BackColor = Color.Brown;

            btnReverseNoti_MouseLeave(null, null);

            filterNoti(CommonFunctions.NotiType.REVERSED);
        }

        private void btnReverseNoti_MouseHover(object sender, EventArgs e)
        {
            btnReverseNoti.ForeColor = Color.Brown;
        }

        private void btnReverseNoti_MouseLeave(object sender, EventArgs e)
        {
            if (isClickReverse)
            {
                isClickReject = false;
                isClickPending = false;

                btnReject.ForeColor = Color.Black;
                btnPending.ForeColor = Color.Black;
                btnReverseNoti.ForeColor = Color.Brown;
            }
            else
                btnReverseNoti.ForeColor = Color.Black;
        }

        private void filterNoti(string notiType)
        {
            DataView dvNoti = new DataView(_dtNoti);
            dvNoti.RowFilter = " [NOTI_TYPE] = '" + notiType + "' ";
            dgvNoti.DataSource = dvNoti;
        }
    }
}