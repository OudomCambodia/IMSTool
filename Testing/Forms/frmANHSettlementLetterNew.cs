using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmANHSettlementLetterNew : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtExplBeni = new DataTable();
        private DataTable dtExplBeniReport = new DataTable();
        private DataTable dtNote = new DataTable();
        private DataTable dtNoteReport = new DataTable();
        private DataTable dtClaimInfoReport = new DataTable();
        private bool stringInput = false;
        private string claimNo = string.Empty;

        public static DataTable DtNote = new DataTable();
        public static DataTable DtExplainBene = new DataTable();

        public frmANHSettlementLetterNew(string ClaimNo)
        {
            InitializeComponent();
            claimNo = ClaimNo;
        }

        private void frmANHSettlementLetterNew_Load(object sender, EventArgs e)
        {
            LoadData();

            dgvExplBeni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNote.DefaultCellStyle.ForeColor = Color.Black;
            dgvExplBeni.DefaultCellStyle.ForeColor = Color.Black;

            dtNote = new DataTable();
            dtNote.Columns.Add("Amount", typeof(string));
            dtNote.Columns.Add("Description", typeof(string));

            dgvNote.DataSource = dtNote;
            dgvNote.Columns["Amount"].Width = 200;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DtNote = dtNote.Copy();
                DtExplainBene = dtExplBeni.Copy();

                GetClaimInfoReport();
                GetNoteHtmlReport();
                GetExplBeniReport();

                dtClaimInfoReport.TableName = "ANH_SL_CLAIM_INFO";
                dtNoteReport.TableName = "ANH_SL_NOTES";
                dtExplBeniReport.TableName = "ANH_SL_EXPL_BENI";

                DataSet dsReport = new DataSet();
                dsReport.Tables.Add(dtClaimInfoReport);
                dsReport.Tables.Add(dtNoteReport);
                dsReport.Tables.Add(dtExplBeniReport);

                frmANHSettlementLetterNewRV frmReport = new frmANHSettlementLetterNewRV(dsReport, txtClaimNo.Text.ToUpper());
                frmReport.ShowDialog();
                Close();

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }
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
                "INT_COMMENTS HOSPITAL, INT_OTH_HOSPITAL, " +
                "TO_CHAR(INT_INITIMATE_DT) REGISTERATION_DATE, TO_CHAR(INT_DATE_LOSS) TREATMENT_DATE, " +
                "(select SFC_SURNAME from SM_M_SALES_FORCE where SFC_CODE = INT_BPARTY_CODE) CC, (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where  CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + claimNo + "'");

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

                    txtCC.Text = dtClaimInfo.Rows[0]["CC"].ToString();
                    txtPolicy.Text = GetPolicyType(proCode);
                    txtPolicyholder.Text = dtClaimInfo.Rows[0]["POLICY_HOLDER"].ToString();
                    txtAddress.Text = dtClaimInfo.Rows[0]["ADDRESS"].ToString();
                    txtMemberName.Text = dtClaimInfo.Rows[0]["MEMBER"].ToString();
                    txtPolicyNo.Text = dtClaimInfo.Rows[0]["POLICY_NO"].ToString();
                    dtpAdmissonDate.Value = Convert.ToDateTime(dtClaimInfo.Rows[0]["TREATMENT_DATE"].ToString());
                    txtClaimNo.Text = dtClaimInfo.Rows[0]["CLAIM_NO"].ToString();
                    txtPlan.Text = GetPlan(proCode, dtClaimInfo.Rows[0]["PLAN_DESCRIPTION"].ToString());
                    dtpRegistrationDate.Value = Convert.ToDateTime(dtClaimInfo.Rows[0]["REGISTERATION_DATE"].ToString());
                    txtPanelHospital.Text = GetPanelHospital(dtClaimInfo.Rows[0]["HOSPITAL"].ToString());
                    txtOtherHospital.Text = dtClaimInfo.Rows[0]["INT_OTH_HOSPITAL"].ToString();
                    txtDiagnosis.Text = dtClaimInfo.Rows[0]["CAUSE"].ToString();
                    txtEmail.Text = GetEmailByProduct(proCode);
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

        private DataTable GetExplBeni(string proCode)
        {
            var dtList = crud.ExecQuery("select * from user_settlement_letter_explbeni where product_code = '" + proCode + "'");
            return dtList;
        }

        private void BindToDgvExplBeni(string proCode)
        {
            dtExplBeni = GetExplBeni(proCode.Substring(1).ToUpper());

            dgvExplBeni.DataSource = dtExplBeni;
            dgvExplBeni.Columns["PRODUCT_CODE"].Visible = false;

            dgvExplBeni.Columns["ORIGINAL_CURRENCY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["CURRENCY_IN_USD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["EXPENSES_NOT_COVERED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["DEDUCTIBLE_OR_COPLAY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["OVER_LIMIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["TOTAL_AMT_COVERED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExplBeni.Columns["SERVICE_PROVIDED"].Width = 600;

            //ClearTotalValue();
        }

        private void GetClaimInfoReport()
        {
            dtClaimInfoReport = new DataTable();

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
            dtNoteReport = new DataTable();
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
