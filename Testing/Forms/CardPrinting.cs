using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class CardPrinting : Form
    {
        CRUD crud = new CRUD();
        DataTable dtChecked = new DataTable();
        CheckBox checkboxHeader = new CheckBox();
        MyDB Mydb = new MyDB();
        DataTable dtmaxNumber = new DataTable();
        string policyno = "";
        public string username = "SICL";
        int num;
        bool isChecked;
        bool Excess = false;

        //bool PrintCard = false;
        //string fwdpolno = string.Empty;

        public CardPrinting()
        {
            InitializeComponent();
        }

        //public CardPrinting(bool PrintCard,string fwdpolno) //Switch from Document Control
        //{
        //    InitializeComponent();
        //    this.PrintCard = PrintCard;
        //    this.fwdpolno = fwdpolno;
        //}

        private void ResetDataGridView()
        {
            DataTable dtChecked = new DataTable();
            dtPolicy.CancelEdit();
            dtPolicy.Columns.Clear();
            dtPolicy.DataSource = null;
            policyno = "";
            lblSel.Text = "0";
        }

        private bool CheckExist()
        {
            ResetDataGridView();
            checkboxHeader.Checked = false;
            if (txtPolicyNo.Text.Trim() == "" || txtPolicyNo.TextLength != 20)
            {
                Msgbox.Show("This Policy No is incorrect or expired or no card membership for printing.");
                // Msgbox.Show("This Policy No is incorrect ");
                txtPolicyNo.Focus();
                return false;
            }
            policyno = txtPolicyNo.Text.Trim().ToUpper().Substring(7, 3);
            string view = "";

            if (policyno != "BHP" && policyno != "CYC" && policyno != "VCM" && policyno != "VPC" && policyno != "HNS" && policyno != "GPA" && policyno != "MCW" && policyno != "PAC" && policyno != "PAE")
            {
                Msgbox.Show("This Policy No is not available for printing in this system. Please contact System Admin or Mr. Soeung Tola (Ext. 488) for more details.");
                txtPolicyNo.Focus();
                return false;
            }


            if (policyno == "BHP") view = "VIEW_MEMBERSHIP_BHP";
            if (policyno == "CYC") view = "VIEW_MEMBERSHIP_CYC";
            if (policyno == "GPA")
            {
                if (chkGPAExcludeSI.Checked)
                {
                    view = "VIEW_MEMBERSHIP_GPA_NO_SI";
                }
                else
                {
                    view = "VIEW_MEMBERSHIP_GPA";
                }
            }
            if (policyno == "HNS") view = "VIEW_MEMBERSHIP_HNS";
            if (policyno == "VCM") view = "VIEW_MEMBERSHIP_VCM";
            if (policyno == "VPC") view = "VIEW_MEMBERSHIP_VPC";

            //Update 04-Dec-19:Add MCW
            if (policyno == "MCW") view = "VIEW_MEMBERSHIP_MCW";

            //Update 12-Aug-20:Add PAC
            if (policyno == "PAC") view = "VIEW_MEMBERSHIP_PAC";

            //Updated 10-02-2022: Add PAE
            if (policyno == "PAE") view = "VIEW_MEMBERSHIP_PAE";
            string sql = "select ROWNUM NO ,V.* from " + view + " V where POLICY_NO=q'[" + txtPolicyNo.Text.Trim().ToUpper() + "]'";
            DataTable dt = new DataTable();
            dt = crud.ExecQuery(sql);
            if (dt.Rows.Count > 0)
            {
                DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                //CheckBox chk = new CheckBox();
                CheckboxColumn.Width = 35;
                dtPolicy.Columns.Add(CheckboxColumn);
                string insured = "";
                switch (policyno)
                {
                    case "BHP":
                    case "GPA":
                    case "PAC"://Update 12-Aug-20:Add PAC
                    case "PAE"://Update 10-02-22:Add PAE
                    case "HNS":
                    case "MCW"://Update 04-Dec-19:Add MCW
                        insured = "INSURED_MEMBER";
                        break;
                    case "CYC":
                    case "VCM":
                    case "VPC":
                        insured = "REG_NO";
                        break;
                    default:
                        insured = "";
                        break;
                }

                if (rbAlp.Checked)
                {
                    dt = dt.AsEnumerable().OrderBy(ite => ite.Field<string>(insured)).CopyToDataTable();
                }
                dtPolicy.DataSource = dt;

                DataGridViewColumn column = dtPolicy.Columns[0];
                column.Width = 35;
                dtPolicy.Columns[0].Resizable = DataGridViewTriState.False;
                dtPolicy.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                dtPolicy.Columns[1].Width = 35;
                dtPolicy.Columns[1].Resizable = DataGridViewTriState.False;
                dtPolicy.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // add checkbox header
                Rectangle rect = dtPolicy.GetCellDisplayRectangle(0, -1, true);
                // set checkbox header to center of header cell. +1 pixel to position correctly.
                rect.X = rect.Location.X + 12;
                rect.Y = rect.Location.Y + 12;
                rect.Width = rect.Size.Width;
                rect.Height = rect.Size.Height;

                checkboxHeader.Visible = true;
                checkboxHeader.Name = "checkboxHeader";
                checkboxHeader.Size = new Size(15, 15);
                checkboxHeader.Location = rect.Location;
                checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                dtPolicy.Controls.Add(checkboxHeader);

                for (int i = 0; i < dtPolicy.Columns.Count; i++)
                {
                    dtPolicy.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                //Update 18-May-20 
                for (int i = 1; i < dtPolicy.Columns.Count; i++)
                {
                    dtPolicy.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }
                //

                for (int i = 1; i < dtPolicy.Columns.Count; i++)
                {
                    dtPolicy.Columns[i].ReadOnly = true;
                }

                if (policyno == "HNS")
                {
                    Excess = false;

                    DataTable dtPlanDes = crud.ExecQuery("select LONG_DESC from user_hns_excess_types");
                    string pol_excess = crud.ExecQuery("select POL_EXCESS_TXT from uw_t_policies where POL_STATUS <> 9 and POL_POLICY_NO = q'[" + txtPolicyNo.Text.Trim().ToUpper() + "]'").Rows[0].ItemArray[0].ToString();

                    foreach (DataRow dr in dtPlanDes.Rows)
                    {
                        if (pol_excess.Contains(dr[0].ToString()))
                            Excess = true;
                    }

                    if (!Excess)
                        dtPolicy.Columns["EXCESS_AMT"].Visible = false;
                    else
                        dtPolicy.Columns["EXCESS_AMT"].Visible = true;
                }
                //Update 03-Dec-19 &//Update 04-Dec-19:Add MCW &//Update 12-Aug-20:Add PAC
                if (policyno == "HNS" || policyno == "GPA" || policyno == "MCW" || policyno == "PAC" || policyno == "BHP" || policyno == "PAE")
                    dtPolicy.Columns["MEMBER_ID"].Visible = false;
            }
            else
            {
                Msgbox.Show("This Policy No is incorrect or expired or canceled.");
                this.ActiveControl = txtPolicyNo;
                txtPolicyNo.SelectAll();
                return false;
            }

            return true;
        }

        private void CardPrinting_Load(object sender, EventArgs e)
        {
            //DataTable dtuser = Mydb.ExecQuery("select role from [DocumentControlDB].[dbo].[tbDOC_USER] where ");
            if (username.Replace("-IMS", "").Equals("KOY DAVON"))
                return;

            DataTable dtuser = crud.ExecQuery("select remark from user_print_system where user_name = '" + username.Replace("-IMS","") + "'");
            if (dtuser.Rows[0][0].ToString() == "PRODUCER")
            {
                //bnSearch.Enabled = false;
                //btnPreview.Enabled = false;
                //btnClear.Enabled = false;
                //btnSend.Enabled = false;
                //btnHistory.Enabled = false;
                //frmDocumentControl.disabledButt(bnSearch);
                frmDocumentControl.disabledButt(btnPreview);
                //frmDocumentControl.disabledButt(btnClear);
                frmDocumentControl.disabledButt(btnSend);
                frmDocumentControl.disabledButt(btnHistory);
            }
            //  InsertToSQL();

            //if (PrintCard)
            //{
            //    txtPolicyNo.Text = fwdpolno;
            //    bnSearch.PerformClick();
            //}


        }

        //private void txtPolicyNo_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        CheckExist();
        //        Cursor.Current = Cursors.AppStarting;
        //    }
        //    catch (Exception ex)
        //    {
        //        Msgbox.Show("This Policy No is incorrect or no card membership for printing.");
        //        txtPolicyNo.Focus();
        //    }
        //}


        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;

                for (int i = 0; i < dtPolicy.RowCount; i++)
                {
                    dtPolicy[0, i].Value = ((CheckBox)dtPolicy.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dtPolicy[0, i].Value;
                    CheckCount(isChecked);
                }
                dtPolicy.EndEdit();

                lblSel.Text = num.ToString();
            }
            catch (Exception EX)
            {
                Msgbox.Show(EX.Message);
            }
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            //try
            //   {
            Cursor.Current = Cursors.WaitCursor;
            CheckExist();
            Cursor.Current = Cursors.AppStarting;
            //  }
            // catch (Exception ex)
            //{
            //  Msgbox.Show("This Policy No is incorrect or expired or no card membership for printing.");
            //  //  txtPolicyNo.Focus();
            //  }
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Index != 1)
                {
                    if (column.Visible || column.Name == "MEMBER_ID")
                    {
                        dt.Columns.Add(column.HeaderText);
                    }
                }
            }

            object[] cellValues = new object[dgv.Columns.Count - 1];
            if (policyno == "HNS" || policyno == "PAE" && !Excess)
                cellValues = cellValues.Take(cellValues.Count() - 1).ToArray();

            string status = "";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {
                        for (int i = 0; i < ((policyno == "HNS" || policyno == "PAE" && !Excess) ? row.Cells.Count - 1 : row.Cells.Count); i++)
                        {
                            if (i != 1)
                            {
                                if (i > 1)
                                    cellValues[i - 1] = row.Cells[i].Value.ToString();
                                else
                                    cellValues[i] = row.Cells[i].Value.ToString();
                            }
                        }

                        dt.Rows.Add(cellValues);
                    }
                }
            }

            return dt;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSel.Text == "0")
                {
                    Msgbox.Show("There is no selected item to be previewed.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(GetDataTableFromDGV(dtPolicy), "");
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show("There is no item to be previewed.");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetDataGridView();
            txtPolicyNo.Text = "";
            checkboxHeader.Visible = false;
            checkboxHeader.Checked = false;
            chkGPAExcludeSI.Checked = false;
        }
        private void InsertToSQL()
        {
            int maxnum = 1;
            dtmaxNumber = new DataTable();
            DataTable existing = dtChecked.Clone();
            if (policyno == "VCM" || policyno == "VPC" || policyno == "CYC")
            {
                foreach (DataRow rw in dtChecked.Rows)
                {
                    if (GetPrintedRecord(rw[3].ToString(), rw[1].ToString(), rw[6].ToString(), Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd")).Rows.Count > 0)
                    {
                        existing.Rows.Add(rw.ItemArray);
                    }
                }
            }
            else if (policyno == "HNS" || policyno == "PAE") //Updated 10-Feb-22:Add PAE
            {
                foreach (DataRow rw in dtChecked.Rows)
                {
                    if (GetPrintedRecord(rw[5].ToString(), rw[2].ToString(), rw[3].ToString(), Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd")).Rows.Count > 0)
                    {
                        existing.Rows.Add(rw.ItemArray);
                    }
                }
            }
            else if (policyno == "MCW")
            {
                foreach (DataRow rw in dtChecked.Rows)
                {
                    if (GetPrintedRecord(rw[4].ToString(), rw[2].ToString(), rw[3].ToString(), Common.strToDate(rw[5].ToString()).ToString("yyyy-MM-dd"), Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd")).Rows.Count > 0)
                    {
                        existing.Rows.Add(rw.ItemArray);
                    }
                }
            }
            else if (policyno == "GPA" || policyno == "PAC")  //Update 12-Aug-20:Add PAC
            {
                foreach (DataRow rw in dtChecked.Rows)
                {
                    if (GetPrintedRecord(rw[5].ToString(), rw[2].ToString(), rw[3].ToString(), Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd")).Rows.Count > 0)
                    {
                        existing.Rows.Add(rw.ItemArray);
                    }
                }
            }
            else if (policyno == "BHP")
            {
                foreach (DataRow rw in dtChecked.Rows)
                {
                    if (GetPrintedRecord(rw[2].ToString(), rw[4].ToString(), rw[5].ToString(), Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd")).Rows.Count > 0)
                    {
                        existing.Rows.Add(rw.ItemArray);
                    }
                }
            }

            if (existing.Rows.Count > 0)
            {
                Msgbox.Show("The card(s) of " + existing.Rows.Count + " member(s) are already printed/in pending! Please see the Excel File for the list of already printed/pending card(s). Click on Close to open the Excel File.", Color.FromArgb(255, 60, 60));
                try
                {
                    My_DataTable_Extensions.ExportToExcel(existing, "");
                }
                catch (Exception) { }
                DialogResult dr = Msgbox.Show("Do you still want to reprint all those cards?", "Warning", Color.FromArgb(255, 60, 60));
                if (dr == System.Windows.Forms.DialogResult.No)
                    return;
            }

            if (policyno == "BHP")
            {
                dtmaxNumber = Mydb.getDataTable("sp_figtree_blue_max_print_number", "user", username);
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_figtree_blue_insert", "ref", rw[2], "policyholder", rw[3], "name", rw[4], "pp_no", rw[5], "valid_from", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"),
                        "@valid_to", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "excess", rw[8], "module", rw[9], "member_since",
                        Common.strToDate(rw[12].ToString()).ToString("yyyy-MM-dd"), "created_by", username, "print_number", maxnum, "insured_id", rw[1],
                        "outpatient", rw[10], "maternity", rw[11]);
                }
                MSG();
            }
            else if (policyno == "CYC")
            {
                dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "MC");
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_auto_insert", "policy_no", rw[3], "coverage", rw[4], "make_model", rw[5], "chasis_engine_no", rw[6], "policy_holder", rw[2], "valid_from", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[1], "auto_type", "MC", "created_by", username, "print_number", maxnum);
                }
                MSG();
            }
            else if (policyno == "VCM")
            {
                dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "CV");
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_auto_insert", "policy_no", rw[3], "coverage", rw[4], "make_model", rw[5], "chasis_engine_no", rw[6], "policy_holder", rw[2], "valid_from", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[1], "auto_type", "CV", "created_by", username, "print_number", maxnum);
                }
                MSG();
            }
            else if (policyno == "VPC")
            {
                dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "PV");
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_auto_insert", "policy_no", rw[3], "coverage", rw[4], "make_model", rw[5], "chasis_engine_no", rw[6], "policy_holder", rw[2], "valid_from", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[1], "auto_type", "PV", "created_by", username, "print_number", maxnum);
                }
                MSG();
            }
            else if (policyno == "GPA" || policyno == "PAC")  //Update 12-Aug-20:Add PAC
            {
                dtmaxNumber = Mydb.getDataTable("sp_gpa_max_print_number", "user", username);
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    DateTime validfrom = DateTime.Parse((Common.strToDate(rw[6].ToString())).ToString());
                    DateTime validto = DateTime.Parse((Common.strToDate(rw[7].ToString())).ToString());
                    Mydb.ExecuteMySql("sp_gpa_insert", "insured_member", rw[2], "policy_holder", rw[4], "policy_no", rw[5], "valid_from", validfrom, "@valid_to", validto, "optional_benefit", rw[8], "dependent", rw[3], "sum_insured", Convert.ToDecimal(rw[9]), "medical_expense", Convert.ToDecimal(rw[10]), "created_by", username, "print_number", maxnum, "insured_id", rw[1]);
                }
                MSG();
            }
            else if (policyno == "MCW") //Update 04-Dec-19 Add MCW
            {
                dtmaxNumber = Mydb.getDataTable("sp_mcw_max_print_number", "user", username);
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_mcw_insert", "insured_member", rw[2], "policy_holder", rw[3], "policy_no", rw[4], "valid_from", Common.strToDate(rw[5].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "dependent", "", "sum_insured", rw[9], "created_by", username, "print_number", maxnum, "insured_id", rw[1]);
                }
                MSG();
            }
            else if (policyno == "HNS" || policyno == "PAE")//Update 04-Feb-22 Add PAE
            {
                dtmaxNumber = Mydb.getDataTable("sp_hns_max_print_number", "user", username);
                if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                {
                    maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                }
                foreach (DataRow rw in dtChecked.Rows)
                {
                    Mydb.ExecuteMySql("sp_hns_insert_excess", "insured_member", rw[2], "policy_holder", rw[4], "policy_no", rw[5], "valid_from", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd"), "optional_benefit", " ", "dependent", rw[3], "plan_type", rw[6], "excess", Excess ? rw[11] : "", "created_by", username, "print_number", maxnum, "insured_id", rw[1]);
                }
                MSG();
            }

            Mydb.Dispose();
        }
        void MSG()
        {
            Msgbox.Show("This Policy no " + txtPolicyNo.Text + " with " + dtChecked.Rows.Count + " item(s) has been sent to card printing system.");

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSel.Text == "0")
                {
                    Msgbox.Show("There is no item to be sent for printing.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                dtChecked = GetDataTableFromDGV(dtPolicy);
                Cursor.Current = Cursors.AppStarting;

                if (dtChecked.Rows.Count > 0)
                {
                    DialogResult dr = Msgbox.Show("Are you sure that you want to send this " + txtPolicyNo.Text + " with " + dtChecked.Rows.Count + " item(s) to card printing system.?", "Confirmation");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        SendingCardInfo();
                    }
                }
                else
                {
                    Msgbox.Show("There is no item to be sent for printing.");
                }

            }
            catch (Exception ex)
            {
                ////Update 04-Jun-20
                //if (ex.Message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server."))
                //{
                //    System.Diagnostics.Process process = new System.Diagnostics.Process();
                //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //    startInfo.FileName = "cmd.exe";
                //    startInfo.Arguments = "/C ipconfig /release && ipconfig /renew";
                //    process.StartInfo = startInfo;
                //    process.EnableRaisingEvents = true;
                //    process.Exited += new EventHandler(RenewedIP);
                //    process.Start();

                //}
                ////End of update
                //else 
                Msgbox.Show(ex.Message);
            }
        }

        private void RenewedIP(object sender, EventArgs e)
        {
            SendingCardInfo();
        }

        //Update 04-Jun-20
        private void SendingCardInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            InsertToSQL();
            string sql = "INSERT INTO user_print_history (user_name, print_datetime, filter4, type) VALUES ('" + username + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + txtPolicyNo.Text + ";;; SumInsuredExclude=" + chkGPAExcludeSI.Checked + "', '4')";
            crud.ExecNonQuery(sql);
            Cursor.Current = Cursors.AppStarting;
            btnClear.PerformClick();
            chkGPAExcludeSI.Checked = false;
        }
        //End of update

        private void txtPolicyNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    CheckExist();
                    Cursor.Current = Cursors.AppStarting;
                }
                catch (Exception ex)
                {
                    Msgbox.Show(ex.Message);
                    txtPolicyNo.Focus();
                }
            }
        }

        private void dtPolicy_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotNumber.Text = dtPolicy.RowCount.ToString();
        }

        private void dtPolicy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                checkboxHeader.Checked = true;
                checkboxHeader_CheckedChanged(sender, e);
            }
        }

        private void dtPolicy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            num = 0;

            if (e.RowIndex == -1)
                return;

            if (dtPolicy.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dtPolicy.SelectedCells)
                {
                    dtPolicy[0, dgvc.RowIndex].Value = true;
                }

                for (int i = 0; i < dtPolicy.RowCount; i++)
                {
                    isChecked = (bool)dtPolicy.Rows[i].Cells[0].EditedFormattedValue;
                    CheckCount(isChecked);
                }

                lblSel.Text = num.ToString();
            }
        }

        private void bnHistory_Click(object sender, EventArgs e)
        {
            ViewHistoryCardPrint vhcp = new ViewHistoryCardPrint();
            vhcp.username = username;
            vhcp.WindowState = FormWindowState.Maximized;
            vhcp.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int col = (policyno == "HNS" || policyno == "GPA" || policyno == "PAC") ? 3 : 2;  //Update 12-Aug-20:Add PAC
                CommonFunctions.GoTo(dtPolicy, textBox1, col);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
                //  dtPolicy.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void CheckCount(bool isChecked)
        {
            if (isChecked)
            {
                num++;
            }
        }

        private void dtPolicy_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dtPolicy);
        }

        private void chkGPAExcludeSI_CheckedChanged(object sender, EventArgs e)
        {
            ResetDataGridView();
            checkboxHeader.Visible = false;
            checkboxHeader.Checked = false;
        }

        private DataTable GetPrintedRecord(string para1, string para2, string para3, string para4, string para5)
        {    //Update 04-Dec-19 Add MCW
            string sql = @"select t.* from (
                        select policy_no
	                            ,reg_no
                                ,chasis_engine_no
                                ,valid_from
                                ,valid_to
                                ,print_status from tbl_auto
                        union 
                        select policy_no
	                            ,insured_member
	                            ,dependent
                                ,valid_from
                                ,valid_to
                                ,print_status from tbl_hns
                        union
                        select policy_no
                                ,insured_member
                                ,dependent
                                ,valid_from
                                ,valid_to
                                ,print_status from tbl_mcw
                        union
                        select policy_no
	                            ,insured_member
                                ,dependent
                                ,valid_from
                                ,valid_to
                                ,print_status from tbl_gpa
                        union
                        select ref
                                ,name
                                ,pp_no
                                ,valid_from
                                ,valid_to
                                ,print_status from tbl_figtree_blue) t 
                        where print_status in ('P','A') and ltrim(rtrim(t.policy_no)) = '" + para1.Trim().Replace("'", "''") + @"' 
                        and ltrim(rtrim(t.reg_no)) = '" + para2.Trim().Replace("'", "''") + "' and ltrim(rtrim(t.chasis_engine_no)) = '" + para3.Trim().Replace("'", "''") + "' and t.valid_from = '" + para4.Trim().Replace("'", "''") + "' and t.valid_to = '" + para5.Trim().Replace("'", "''") + "'";
            return Mydb.ExecQuery(sql);
        }

        private void CardPrinting_Activated(object sender, EventArgs e)
        {
            txtPolicyNo.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSel.Text == "0")
                {
                    Msgbox.Show("There is no item to be printing.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                dtChecked = GetDataTableFromDGV(dtPolicy);
                Cursor.Current = Cursors.AppStarting;

                if (dtChecked.Rows.Count > 0)
                {
                    PrintCard();
                }
                else
                {
                    Msgbox.Show("There is no item to be sent for printing.");
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        void PrintCard()
        {
            Cursor.Current = Cursors.WaitCursor;

            //DataTable PrintData = dtChecked.Clone();
            //PrintData.Columns.RemoveAt(0);

            CrystalDecisions.CrystalReports.Engine.ReportClass rpt = new CrystalDecisions.CrystalReports.Engine.ReportClass();
            var frm = new frmViewInstructionNote();

            if (policyno == "VPC")
            {
                rpt = new Reports.VPC_Card();
                frm.Text = "VPC Card";
            }
            else if (policyno == "CYC")
            {
                rpt = new Reports.CYC_Card();
                frm.Text = "CYC Card";
            }
            else if (policyno == "VCM")
            {
                rpt = new Reports.VCM_Card();
                frm.Text = "VCM Card";
            }
            else if (policyno == "PAE")
            {
                rpt = new Reports.CPAE_card();
                frm.Text = "PAE CARD";
            }
            //add GPA card 21/04/2022 
            else if (policyno == "GPA" || policyno == "PAC" )
            {
                rpt = new Reports.GPAECard();
                frm.Text = "GPA CARD";
            }
            else if (policyno == "HNS")
            {
                rpt = new Reports.ECardHNS();
                frm.Text = "HNS CARD";
            }
            else if (policyno == "BHP")
            {
                rpt = new Reports.ECardBHP();
                frm.Text = "BHP CARD";
            }
            //Updated on 21-Feb-2022 - Add ECard printing and save file
            #region CPAE
            if (policyno == "PAE")
            {
                Reports.CPAE_card rpt2 = new Reports.CPAE_card();
                DialogResult msg = Msgbox.Show("Do you want to save ECard to pdf file?", "Confirmation", "Process", "Abort");
                if (msg == System.Windows.Forms.DialogResult.Yes)
                {
                    #region saveecard

                    FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                    if (directchoosedlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        EcardPDF(directchoosedlg, 1);
                        if (dtChecked.Rows.Count <= 0)
                        {
                            Msgbox.Show("No record selected!");
                            return;
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult dr = Msgbox.Show("Download successfully,would you like to preview Ecard?", "Confirmation", "Yes", "No");
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                //Reports.EngineeringLetterDW rpt1 = new Reports.EngineeringLetterDW();
                                //string datetemp = DateTime.Now.ToString("dd MMMM yyyy");

                                rpt2.SetDataSource(dtChecked);

                                //foreach (DataRow drt in dtChecked.Rows)
                                //{

                                //    rpt2.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                                //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                                //}
                                frm.rpt = rpt2;
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
                    #endregion
                else
                {
                    rpt2.SetDataSource(dtChecked);

                    //foreach (DataRow drt in dtChecked.Rows)
                    //{

                    //    rpt2.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                    //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                    //}
                    frm.rpt = rpt2;
                    frm.Show();
                }


            }
            #endregion
            #region GPA && PAC
            else if (policyno == "GPA" || policyno == "PAC")
            {
                Reports.GPAECard rpt2 = new Reports.GPAECard();
                DialogResult msg = Msgbox.Show("Do you want to save ECard to pdf file?", "Confirmation", "Process", "Abort");
                if (msg == System.Windows.Forms.DialogResult.Yes)
                {
                    #region saveecard

                    FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                    if (directchoosedlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        EcardPDF(directchoosedlg, 2);
                        if (dtChecked.Rows.Count <= 0)
                        {
                            Msgbox.Show("No record selected!");
                            return;
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult dr = Msgbox.Show("Download successfully,would you like to preview Ecard?", "Confirmation", "Yes", "No");
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                //Reports.EngineeringLetterDW rpt1 = new Reports.EngineeringLetterDW();
                                //string datetemp = DateTime.Now.ToString("dd MMMM yyyy");

                                rpt2.SetDataSource(dtChecked);

                                //foreach (DataRow drt in dtChecked.Rows)
                                //{

                                //    rpt2.SetParameterValue("EFFECTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());
                                //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());

                                //}
                                frm.rpt = rpt2;
                                frm.Show();
                            }
                            else
                            {
                                // Msgbox.Show("Download Successfully!");
                                return;
                            }


                        }
                    }
                    #endregion
                }
                else
                {
                    rpt2.SetDataSource(dtChecked);
                    if (policyno == "PAE")
                    {
                        //foreach (DataRow drt in dtChecked.Rows)
                        //{

                        //    rpt2.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                        //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                        //}
                        frm.rpt = rpt2;
                        frm.Show();
                    }
                    else
                    {
                        //foreach (DataRow drt in dtChecked.Rows)
                        //{

                        //    rpt2.SetParameterValue("EFFECTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());
                        //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());
                        //}
                        frm.rpt = rpt2;
                        frm.Show();
                    }
                }

            }
            #endregion
            #region HNS
            if (policyno == "HNS")
            {
                Reports.ECardHNS rpt2 = new Reports.ECardHNS();
                DialogResult msg = Msgbox.Show("Do you want to save ECard to pdf file?", "Confirmation", "Process", "Abort");
                if (msg == System.Windows.Forms.DialogResult.Yes)
                {
                    #region saveecard

                    FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                    if (directchoosedlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        EcardPDF(directchoosedlg, 3);
                        if (dtChecked.Rows.Count <= 0)
                        {
                            Msgbox.Show("No record selected!");
                            return;
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult dr = Msgbox.Show("Download successfully,would you like to preview Ecard?", "Confirmation", "Yes", "No");
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                //Reports.EngineeringLetterDW rpt1 = new Reports.EngineeringLetterDW();
                                //string datetemp = DateTime.Now.ToString("dd MMMM yyyy");

                                rpt2.SetDataSource(dtChecked);

                                //foreach (DataRow drt in dtChecked.Rows)
                                //{

                                //    rpt2.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                                //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                                //}
                                frm.rpt = rpt2;
                                frm.Show();
                            }
                            else
                            {
                                // Msgbox.Show("Download Successfully!");
                                return;
                            }


                        }
                    }
                    #endregion
                }
                else
                {
                    rpt2.SetDataSource(dtChecked);
                    frm.rpt = rpt2;
                    frm.Show();
                }
            }
            #endregion
            #region BHP
            if (policyno == "BHP")
            {
                Reports.ECardBHP rpt2 = new Reports.ECardBHP();
                DialogResult msg = Msgbox.Show("Do you want to save ECard to pdf file?", "Confirmation", "Process", "Abort");
                if (msg == System.Windows.Forms.DialogResult.Yes)
                {
                    #region saveecard

                    FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                    if (directchoosedlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        EcardPDF(directchoosedlg, 4);
                        if (dtChecked.Rows.Count <= 0)
                        {
                            Msgbox.Show("No record selected!");
                            return;
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult dr = Msgbox.Show("Download successfully,would you like to preview Ecard?", "Confirmation", "Yes", "No");
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                

                                rpt2.SetDataSource(dtChecked);

                              
                                frm.rpt = rpt2;
                                frm.Show();
                            }
                            else
                            {
                                // Msgbox.Show("Download Successfully!");
                                return;
                            }


                        }
                    }
                    #endregion
                }
                else
                {
                    rpt2.SetDataSource(dtChecked);

                    //foreach (DataRow drt in dtChecked.Rows)
                    //{

                    //    rpt2.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(drt["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                    //    rpt2.SetParameterValue("EXPIRYD", Convert.ToDateTime(drt["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                    //}
                    frm.rpt = rpt2;
                    frm.Show();
                }
            }
            #endregion
            else
            {
                rpt.SetDataSource(dtChecked);
                frm.rpt = rpt;
                frm.Show();
            }
            Cursor.Current = Cursors.AppStarting;
        }
        //Updated on 21-Feb-2022 - Add ECard printing and save file
        private void EcardPDF(FolderBrowserDialog directchoosedlg, int option)
        {


            //string status = "";
            string folderPath = "";
            string filep = "";



            foreach (DataRow row in dtChecked.Rows)
            {

                if (row != null)
                {



                    //status = row.Cells[0].Value.ToString();

                    DataTable dt1 = new DataTable();
                    dt1 = dtChecked.Clone();
                    dt1.Rows.Add(row.ItemArray);




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

                    #region CPAE option 1
                    if (option == 1)
                    {
                        if (row["DEPENDENT"].ToString() == "")
                            filep += "CPAE-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["INSURED_MEMBER"].ToString().Substring(0, row["INSURED_MEMBER"].ToString().Length - 1).ToUpper() + ".pdf";
                        else
                            filep += "CPAE-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["DEPENDENT"].ToString().Substring(0, row["DEPENDENT"].ToString().Length - 1).ToUpper() + ".pdf";

                        Reports.CPAE_card rpt1 = new Reports.CPAE_card();
                        //Reports.EngineeringLettereMAIL rpt = new Reports.EngineeringLettereMAIL();
                        rpt1.SetDataSource(dt1);
                        rpt1.SetParameterValue("EFFECTTIVED", Common.strToDate(row["RISK_VALID_FROM"].ToString()).ToString("dd MMMM yyyy").ToUpper());
                        rpt1.SetParameterValue("EXPIRYD", Common.strToDate(row["RISK_VALID_TO"].ToString()).ToString("dd MMMM yyyy").ToUpper());

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
                        Cursor.Current = Cursors.WaitCursor;
                        rpt1.Export();
                        if (rpt1 != null)
                        {
                            rpt1.Close();
                            rpt1.Dispose();
                        }
                    }
                    #endregion
                    #region GPA Option 2
                    if (option == 2)
                    {
                        if (row["DEPENDENT"].ToString() == "")
                            filep += "GPA-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["INSURED_MEMBER"].ToString().Substring(0, row["INSURED_MEMBER"].ToString().Length).ToUpper() + ".pdf";
                        else
                            filep += "GPA-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["DEPENDENT"].ToString().Substring(0, row["DEPENDENT"].ToString().Length).ToUpper() + ".pdf";

                        Reports.GPAECard rpt1 = new Reports.GPAECard();
                        //Reports.EngineeringLettereMAIL rpt = new Reports.EngineeringLettereMAIL();
                        rpt1.SetDataSource(dt1);
                        //rpt1.SetParameterValue("EFFECTIVED", Convert.ToDateTime(row["RISK_VALID_FROM"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());
                        //rpt1.SetParameterValue("EXPIRYD", Convert.ToDateTime(row["RISK_VALID_TO"].ToString()).ToString("dd-MM-yyyy").Replace('/', '-').ToUpper());
                        //rpt1.SetParameterValue("SUMINSURED", Convert.ToDecimal(row["SUMINSURED"]));

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
                        Cursor.Current = Cursors.WaitCursor;
                        rpt1.Export();
                        if (rpt1 != null)
                        {
                            rpt1.Close();
                            rpt1.Dispose();
                        }



                    }

                    #endregion
                    #region HNS option 3
                    if (option == 3)
                    {
                        if (row["DEPENDENT"].ToString() == "")
                            filep += "HHNS-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["INSURED_MEMBER"].ToString().Substring(0, row["INSURED_MEMBER"].ToString().Length).ToUpper() + ".pdf";
                        else
                            filep += "HHNS-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["DEPENDENT"].ToString().Substring(0, row["DEPENDENT"].ToString().Length).ToUpper() + ".pdf";

                        Reports.ECardHNS rpt1 = new Reports.ECardHNS();
                        //Reports.EngineeringLettereMAIL rpt = new Reports.EngineeringLettereMAIL();
                        rpt1.SetDataSource(dt1);
                        //rpt1.SetParameterValue("EFFECTTIVED", Convert.ToDateTime(row["RISK_VALID_FROM"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());
                        //rpt1.SetParameterValue("EXPIRYD", Convert.ToDateTime(row["RISK_VALID_TO"].ToString()).ToString("dd-MMMM-yyyy").Replace('-', ' ').ToUpper());

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
                        Cursor.Current = Cursors.WaitCursor;
                        rpt1.Export();
                        if (rpt1 != null)
                        {
                            rpt1.Close();
                            rpt1.Dispose();
                        }
                    }
                    #endregion
                    #region BHP Option 4
                    if (option == 4)
                    {
                       
                            filep += "HBHP-" + row["POLICY_NO"].ToString().Replace('/', '-').Substring(11).ToUpper() + "-" + row["INSURED_MEMBER"].ToString().Substring(0, row["INSURED_MEMBER"].ToString().Length).ToUpper() + ".pdf";

                        Reports.ECardBHP rpt1 = new Reports.ECardBHP();
                        rpt1.SetDataSource(dt1);
                        
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
                        Cursor.Current = Cursors.WaitCursor;
                        rpt1.Export();
                        if (rpt1 != null)
                        {
                            rpt1.Close();
                            rpt1.Dispose();
                        }
                    }
                    #endregion






                }
            }


        }
        //---------------------------------------------------//
    }
}



public class Common
{
    /// <summary>
    /// Author: bunthet
    /// </sumamry>
    public static DateTime strToDate(string date)
    {
        // convert dd/MM/YYYY to YYYY/MM/dd
        string[] dates = date.Split('/');
        if (dates.Length == 1)
        {
            dates = date.Split('-');
        }
        //return (Convert.ToDateTime(dates[2] + "/" + dates[1] + "/" + dates[0]));
        return new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));

    }
    //date to string
    public static string DateToStr(DateTime date)
    {
        return string.Format("{0:00}", date.Day) + "/" + string.Format("{0:00}", date.Month) + "/" + date.Year.ToString();
    }
}

