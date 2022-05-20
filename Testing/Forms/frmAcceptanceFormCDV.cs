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
    public partial class frmAcceptanceFormCDV : Form
    {
        public string Username = "";
        CRUD crud = new CRUD();
        DataTable clpolinfo = new DataTable();


        public frmAcceptanceFormCDV()
        {
            InitializeComponent();
        }

        private void clearControl()
        {
            foreach (Control ctl in groupBox1.Controls)
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

            foreach (Control ctl in groupBox7.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            foreach (Control ctl in gbTP.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                    ((TextBox)ctl).Enabled = false;
                }
                else if (ctl is ComboBox)
                {
                    ((ComboBox)ctl).Text = "";
                    ((ComboBox)ctl).Enabled = false;
                }
            }

            dgvPaymentDetail.DataSource = null;
            dgvPaymentDetail.Columns.Clear();

            tbDeductible.Text = "";
            tbCoIn.Text = "";



            foreach (Control ctl in gbLetters.Controls)
            {
                if (ctl is RadioButton)
                {
                    ((RadioButton)ctl).Enabled = false;
                    ((RadioButton)ctl).Checked = false;
                }
            }
            frmDocumentControl.disabledButt(bnGenerate);
            tbNote.Enabled = false;
            tbAdjustAmt.Enabled = false;
        }



        private void bnClaimSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string clno = tbClaimNo.Text.Trim().ToUpper(), proclass = "", subclass = "";

                clearControl();

                if (clno == "" || clno.Length != 20)
                {
                    Msgbox.Show("Please Input Claim No!");
                    return;
                }

                clpolinfo = crud.ExecQuery("SELECT * FROM VIEW_CLAIM_INFO WHERE CLAIM_NO = '" + clno + "'");
                if (clpolinfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim No not found!");
                    tbClaimNo.Focus();
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //DataTable dtTemp = crud.ExecQuery("SELECT POL_PERIOD_FROM,POL_PERIOD_TO FROM VIEW_POLICIES WHERE POL_SEQ_NO = " +
                    //    "(SELECT INT_POLICY_SEQ FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + clno + "')");


                    tbRiskName.Text = clpolinfo.Rows[0]["INT_PRS_NAME"].ToString();
                    tbDateofLoss.Text = Convert.ToDateTime(clpolinfo.Rows[0]["DATEOFLOSS"]).ToString("dd'/'MM'/'yyyy");
                    //tbDateofLoss.Text = DateTime.ParseExact(clpolinfo.Rows[0]["DATEOFLOSS"].ToString(), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd'/'MM'/'yyyy");
                    tbPlaceofAcc.Text = clpolinfo.Rows[0]["INT_PLACE_LOSS"].ToString();
                    tbLossInfo.Text = clpolinfo.Rows[0]["LOSS_DESC"].ToString();
                    tbIncurredAmount.Text = String.Format("{0:N}", clpolinfo.Rows[0]["INCURRED_AMT"]);
                    tbInsured.Text = clpolinfo.Rows[0]["ADDITIONAL_INSURED"].ToString();
                    tbPolicyNo.Text = clpolinfo.Rows[0]["POLICYNO"].ToString();
                    tbPolicyPeriod.Text = "from " + (Convert.ToDateTime(clpolinfo.Rows[0]["INT_PERIOD_FROM"]).ToString("dd'/'MM'/'yyyy"))
                        + " to " + (Convert.ToDateTime(clpolinfo.Rows[0]["INT_PERIOD_TO"]).ToString("dd'/'MM'/'yyyy"));
                    tbIntermediary.Text = clpolinfo.Rows[0]["AGENT_CODE"].ToString() + " - " + clpolinfo.Rows[0]["AGENT_NAME"].ToString();
                    tbAH.Text = clpolinfo.Rows[0]["ACCOUNT_HANDLER"].ToString();
                    tbSumInsured.Text = String.Format("{0:N}", clpolinfo.Rows[0]["RISK_SUM_INSURED"]);
                    tbPolicyPremium.Text = String.Format("{0:N}", clpolinfo.Rows[0]["POL_PREMIUM"]);
                    string PaymentStatus = clpolinfo.Rows[0]["POL_PAYMENT_STATUS"].ToString();
                    tbPaidorOS.Text = (PaymentStatus == "Y") ? "PAID" : "O/S";
                    proclass = clpolinfo.Rows[0]["MAIN_CLASS"].ToString();
                    subclass = clpolinfo.Rows[0]["SUBCLASS"].ToString();

                    //Payment Detail Table
                    //DataTable Paymenttbl = crud.ExecQuery("select PAID_PERILS, SUM(RRD_VALUE) as PAID_AMOUNT from " +
                    //"(select (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                    //"(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE) as PAID_PERILS,RRD_VALUE " +
                    //"from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0) group by PAID_PERILS");
                    //dgvPaymentDetail.DataSource = Paymenttbl;
                    //dgvPaymentDetail.Columns["PAID_AMOUNT"].DefaultCellStyle.Format = "c";
                    ////

                    //if (Paymenttbl.Rows.Count <= 0)
                    //{
                    //    if (subclass == "GPA" || subclass == "PAC")  //need to show info before paid for some reasons :) xD
                    //    {
                    //        Paymenttbl = crud.ExecQuery("select 'MAIN CLAIM' || ' / ' || REQ_COMMENTS PAID_PERILS, REQ_AMOUNT PAID_AMOUNT from CL_T_REQUISITION a, CL_T_INTIMATION b " +
                    //        "where a.REQ_INT_SEQ = b.INT_SEQ_NO and b.INT_CLAIM_NO = '"+clno+"'");
                    //        dgvPaymentDetail.DataSource = Paymenttbl;
                    //        dgvPaymentDetail.Columns["PAID_AMOUNT"].DefaultCellStyle.Format = "c";
                    //    }
                    //}


                    DataTable Paymenttbl = new DataTable();
                    if (subclass == "GPA" || subclass == "PAC" || subclass == "TRI" || subclass == "PAP" || subclass == "PAE")
                    {
                        //Paymenttbl = crud.ExecQuery("select PAID_PERILS, RRD_VALUE as PAID_AMOUNT from " +
                        //                  "(select (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                        //                  "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE) as PAID_PERILS,RRD_VALUE " +
                        //                  "from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0)");

                        //if (Paymenttbl.Rows.Count <= 0) //need CDV before paid => get Requisition Amt
                        //{
                        //    Paymenttbl = crud.ExecQuery("select 'MAIN CLAIM' || ' / ' || REQ_COMMENTS PAID_PERILS, REQ_AMOUNT PAID_AMOUNT from CL_T_REQUISITION a, CL_T_INTIMATION b " +
                        //       "where a.REQ_INT_SEQ = b.INT_SEQ_NO and b.INT_CLAIM_NO = '" + clno + "'");
                        //}

                        Paymenttbl = crud.ExecQuery("select PAID_PERILS, RRD_VALUE as PAID_AMOUNT from " +
                                          "(select (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                                          "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE) as PAID_PERILS,RRD_VALUE " +
                                          "from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0) " +
                                          "union " +
                                          "select 'MAIN CLAIM' || ' / ' || REQ_COMMENTS PAID_PERILS, REQ_AMOUNT PAID_AMOUNT from CL_T_REQUISITION a, CL_T_INTIMATION b " +
                               "where a.REQ_INT_SEQ = b.INT_SEQ_NO and b.INT_CLAIM_NO = '" + clno + "'");
                    }
                    else
                    {
                        Paymenttbl = crud.ExecQuery("select PAID_PERILS, SUM(RRD_VALUE) as PAID_AMOUNT from " +
                                           "(select (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                                           "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE) as PAID_PERILS,RRD_VALUE " +
                                           "from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0) group by PAID_PERILS");
                    }


                    dgvPaymentDetail.DataSource = Paymenttbl;
                    dgvPaymentDetail.Columns["PAID_AMOUNT"].DefaultCellStyle.Format = "c";




                    //Deductible,CoIn
                    DataTable dtTemp = crud.ExecQuery("select POL_EXCESS_TXT, " +
                    "(select RFT_DESCRIPTION from CM_R_REFERENCE_TWO where RFT_CODE =  " +
                    "PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO((select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "'),'CO-INSURANCE') AND RFT_TYPE = 'CI') as CO_INSURANCE from " +
                    "(select POL_SEQ_NO,POL_EXCESS_TXT from UW_T_POLICIES where POL_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select EDT_SEQ_NO,EDT_EXCESS_TXT from UW_T_ENDORSEMENTS where EDT_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select PHS_SEQ_NO,PHS_EXCESS_TXT from UW_H_POLICY_HISTORY where PHS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select NDS_SEQ_NO,NDS_EXCESS_TXT from UW_H_ENDORSEMENT_HISTORY where NDS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "')) T1");
                    string DedutibleText = dtTemp.Rows[0]["POL_EXCESS_TXT"].ToString(),
                        CoInsurance = dtTemp.Rows[0]["CO_INSURANCE"].ToString();
                    tbDeductible.Text = (DedutibleText == "") ? "N/A" : DedutibleText;
                    tbCoIn.Text = (CoInsurance == "") ? "N/A" : CoInsurance;
                    //

                    //Risk Info (Auto)
                    if (proclass == "AUTOMOBILE")
                    {
                        groupBox7.Visible = true;

                        dtTemp = crud.ExecSP_OutPara("SP_MOTOR_CL_INFO", new string[] { "WKCLAIMNO" }, new string[] { clno });
                        if (dtTemp.Rows.Count > 0)
                        {
                            tbVechileNo.Text = tbRiskName.Text;
                            tbMakeModel.Text = dtTemp.Rows[0]["MAKE_MODEL"].ToString();
                            tbYearOfManu.Text = dtTemp.Rows[0]["YEAR_OF_MANUFACTURE"].ToString();
                            tbChasisNo.Text = dtTemp.Rows[0]["CHASSIS_NO"].ToString();
                            tbEngineNo.Text = dtTemp.Rows[0]["ENGINE_NO"].ToString();
                            tbCapacity.Text = dtTemp.Rows[0]["CAPACITY"].ToString() + " " + dtTemp.Rows[0]["CAPACITY_TYPE"].ToString();
                        }
                    }
                    else
                    {
                        groupBox7.Visible = false;

                        foreach (Control ctl in groupBox7.Controls)
                        {
                            if (ctl is TextBox)
                            {
                                ((TextBox)ctl).Text = "";
                            }
                        } 
                    }
                    //

                    //Enable/Disable Control
                    tbNote.Enabled = true;
                    frmDocumentControl.enabledButt(bnGenerate);
                    if (Paymenttbl.Rows.Count > 0) //Claim paid available => open CDV
                    {
                        rbLetter1.Enabled = true;
                        rbLetter2.Enabled = true;
                    }

                    if (!(proclass == "AUTOMOBILE" || subclass == "GPA" || subclass == "PAC" || subclass == "TRI" || subclass == "PAP" || subclass == "PAE")) //Not Auto and PA => open Letter of Acceptance
                    {
                        rbLetter3.Enabled = true;
                        rbLetter4.Enabled = true;
                        rbLetter5.Enabled = true;
                        tbAdjustAmt.Enabled = true;

                        foreach (Control ctl in gbTP.Controls)
                        {
                            if (ctl is TextBox)
                            {
                                ((TextBox)ctl).Enabled = true;
                            }
                            else if (ctl is ComboBox)
                            {
                                ((ComboBox)ctl).Enabled = true;
                            }
                        }
                    }
                    else 
                        rbLetter2.Enabled = false; //Auto and PA => No CDV for TP
                    //

                    Cursor.Current = Cursors.AppStarting;
                    bnRiskEndoDetail.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void frmAcceptanceFormCDV_Load(object sender, EventArgs e)
        {
            tbClaimNo.Focus();

            clearControl();

            dgvPaymentDetail.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvPaymentDetail.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            bnRiskEndoDetail.Enabled = false;

            //Get TP Combo Data
            DataTable dtTemp = crud.ExecQuery("SELECT DISTINCT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_SMG_LEVEL = 9 ORDER BY GPL_DESC");
            cbTPSangkat.DataSource = dtTemp;
            cbTPSangkat.DisplayMember = "GPL_DESC";
            dtTemp = crud.ExecQuery("SELECT DISTINCT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_SMG_LEVEL = 3 ORDER BY GPL_DESC");
            cbTPKhan.DataSource = dtTemp;
            cbTPKhan.DisplayMember = "GPL_DESC";
            dtTemp = crud.ExecQuery("SELECT DISTINCT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_SMG_LEVEL = 2 ORDER BY GPL_DESC");
            cbTPCity.DataSource = dtTemp;
            cbTPCity.DisplayMember = "GPL_DESC";
            dtTemp = crud.ExecQuery("SELECT DISTINCT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_SMG_LEVEL = 1 ORDER BY GPL_DESC");
            cbTPCountry.DataSource = dtTemp;
            cbTPCountry.DisplayMember = "GPL_DESC";
            //
        }

        private void bnRiskEndoDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var frm = new frmDeductibleRiskEndo(clpolinfo.Rows[0]["INT_PRS_NAME"].ToString(), clpolinfo.Rows[0]["POLICYNO"].ToString());
            frm.ShowDialog();
            Cursor.Current = Cursors.AppStarting;
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void bnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string proclass = clpolinfo.Rows[0]["MAIN_CLASS"].ToString(),
                subclass = clpolinfo.Rows[0]["SUBCLASS"].ToString(),
                product = clpolinfo.Rows[0]["SUBCLASS"].ToString(),
                cause = clpolinfo.Rows[0]["NATUREOFLOSS"].ToString(),
                risk = clpolinfo.Rows[0]["INT_PRS_NAME"].ToString(),
                dateofloss = Convert.ToDateTime(clpolinfo.Rows[0]["DATEOFLOSS"]).ToString("dd MMMM yyyy"),
                clno = clpolinfo.Rows[0]["CLAIM_NO"].ToString(),
                text = ""; //use for Display form title

                DataView view = new DataView(clpolinfo);
                DataTable rptdata = new DataTable();


                if (rbLetter1.Checked || rbLetter2.Checked) //CDV
                {
                    string SelectedPeril = "";
                    decimal SelectedPerilAmt = 0;

                    //Check select peril
                    if (dgvPaymentDetail.CurrentCell.RowIndex == -1)
                    {
                        Msgbox.Show("Please select any Main Claim Peril first.");
                        return;
                    }
                    else
                    {
                        int CurrentRow = dgvPaymentDetail.CurrentCell.RowIndex;
                        SelectedPeril = dgvPaymentDetail.Rows[CurrentRow].Cells["PAID_PERILS"].Value.ToString();
                        SelectedPerilAmt = Convert.ToDecimal(dgvPaymentDetail.Rows[CurrentRow].Cells["PAID_AMOUNT"].Value);

                        if (!SelectedPeril.Contains("MAIN CLAIM"))
                        {
                            Msgbox.Show("Please select Main Claim Peril.");
                            return;
                        }
                    }
                    //

                    rptdata = view.ToTable("Selected", false, "CLAIM_NO", "POLICYNO", "ADDITIONAL_INSURED",
                        "INT_PRS_NAME", "DATEOFLOSS", "INT_PLACE_LOSS");
                    rptdata.Columns.Add("CLAIMANT", typeof(string));
                    rptdata.Columns.Add("AMT_IN_WORD", typeof(string));
                    rptdata.Columns.Add("PAID_AMT", typeof(string));
                    rptdata.Columns.Add("CONDITION", typeof(string));
                    rptdata.Columns.Add("NOTE", typeof(string));

                    //Check Condition, Note
                    string Condition = "", Note = tbNote.Text.Trim().ToUpper();

                    if (Note != "") Note = "(" + Note + ")"; //add bracket if available

                    if (proclass == "AUTOMOBILE")
                    {
                        if (SelectedPeril.Contains("AUTO - OWN DAMAGE"))
                            Condition = "DAMAGE TO MY/OUR AFORESAID VEHICLE";
                        else if (SelectedPeril.Contains("AUTO - THIRD PARTY LIABILITY"))
                            Condition = "THIRD PARTY LIABILITY RESULTING FROM MY/OUR AFORESAID VEHICLE";
                        else if (SelectedPeril.Contains("AUTO - TOTAL LOSS AND CONSTRUCTIVE TOTAL LOSS"))
                            Condition = "TOTAL LOSS OR CONSTRUCTIVE TOTAL LOSS TO MY/OUR AFORESAID VEHICLE";
                        else if (SelectedPeril.Contains("AUTO - TOWING FEE"))
                            Condition = "TOWING FEE UNDER ADDITIONAL BENEFITS OF MY/OUR AFORESAID VEHICLE FOR THE ACCIDENT OCCURRING";
                        else if (SelectedPeril.Contains("AUTO - WINDSCREEN COVER - FOC"))
                            Condition = "DAMAGE TO THE WINDSCREEN OF MY/OUR AFORESAID VEHICLE";
                    }
                    else
                        Condition = cause;
                    //

                    foreach (DataRow dr in rptdata.Rows)
                    {
                        if (proclass == "AUTOMOBILE")
                            dr["CLAIMANT"] = "VEHICLE NUMBER";
                        else if (product == "GPA" || product == "PAC" || product == "TRI" || product == "TRV" || product == "PAE" || product == "PAP")
                            dr["CLAIMANT"] = "CLAIMANT";
                        else
                            dr["CLAIMANT"] = "";

                        dr["AMT_IN_WORD"] = CommonFunctions.ToWords(SelectedPerilAmt);
                        dr["PAID_AMT"] = String.Format("{0:N}", SelectedPerilAmt);
                        dr["CONDITION"] = Condition;
                        dr["NOTE"] = Note;
                    }

                    text = "Claim Discharge Voucher";
                }
                else if (rbLetter3.Checked || rbLetter4.Checked || rbLetter5.Checked) //Acceptance Letter
                {
                    string TPName = tbTPName.Text.Trim().ToUpper(),
                        TPHouse = tbTPHouse.Text.Trim().ToUpper(),
                        TPSangkat = cbTPSangkat.Text.Trim().ToUpper(),
                        TPKhan = cbTPKhan.Text.Trim().ToUpper(),
                        TPCity = cbTPCity.Text.Trim().ToUpper(),
                        TPCountry = cbTPCountry.Text.Trim().ToUpper();
                    //Check TP Info for Acceptance Letter TP
                    if (rbLetter3.Checked && TPName == "")
                    {
                        Msgbox.Show("Please input Third Party Name");
                        tbTPName.Focus();
                        return;
                    }
                    //

                    rptdata = view.ToTable("Selected", false, "CLAIM_NO", "POLICYNO", "ADDITIONAL_INSURED",
                        "INT_PRS_NAME", "DATEOFLOSS", "INT_PLACE_LOSS", "NATUREOFLOSS", "ADDRESS");
                    rptdata.Columns.Add("TP", typeof(string));
                    rptdata.Columns.Add("TP_ADDRESS", typeof(string));
                    rptdata.Columns.Add("ADJUST_AMT", typeof(string));
                    rptdata.Columns.Add("AMT_IN_WORD", typeof(string));
                    rptdata.Columns.Add("NOTE", typeof(string));

                    
                    //ADJUST AMT, TP ADDRESS, Note
                    decimal AdjustAmt = Math.Round(tbAdjustAmt.Value, 2);
                    string Address = TPHouse + ", "+TPSangkat+", "+Environment.NewLine+
                        TPKhan + ", " + Environment.NewLine + TPCity + ", " + Environment.NewLine + TPCountry,
                    Note = tbNote.Text.Trim().ToUpper();

                    if (Note != "") Note = "(" + Note + ")"; //add bracket if available

                    //

                    foreach (DataRow dr in rptdata.Rows)
                    {
                        dr["TP"] = TPName;
                        dr["TP_ADDRESS"] = Address;
                        dr["ADJUST_AMT"] = String.Format("{0:N}", AdjustAmt);
                        dr["AMT_IN_WORD"] = CommonFunctions.ToWords(AdjustAmt);
                        dr["NOTE"] = Note;
                    }

                    text = "Letter of Acceptance";
                }


                if (rptdata.Rows.Count > 1) //keep only 1 row
                {
                    for (int i = 1; i < rptdata.Rows.Count; i++)
                        rptdata.Rows.RemoveAt(i);
                }

                //if (proclass == "PROPERTY") //For Property, file name use Claim no instead of Risk name
                //{
                //    risk = clno;
                //}

                if (!(proclass == "AUTOMOBILE" || subclass == "GPA" || subclass == "PAC" || product == "TRI" || product == "PAE" || product == "PAP")) //For Property, file name use Claim no instead of Risk name
                {
                    risk = clno;
                }

                //Create Crystal Report
                CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (rbLetter1.Checked) //CDV For Insured
                {
                    report = new Reports.CDVForInsured();
                    report.SummaryInfo.ReportTitle = ("CDV - " + risk + " - " + dateofloss).Replace('/', '-');
                }
                else if (rbLetter2.Checked) //CDV For TP
                {
                    report = new Reports.CDVForTP();
                    report.SummaryInfo.ReportTitle = ("CDV - For TP - " + risk + " - " + dateofloss).Replace('/', '-');
                }
                else if (rbLetter3.Checked) //Acceptance Letter TP
                {
                    report = new Reports.AcceptanceLetterTP();
                    report.SummaryInfo.ReportTitle = ("LETTER OF ACCEPTANCE - For TP - " + clno + " - " + dateofloss).Replace('/', '-');
                }
                else if (rbLetter4.Checked) //Acceptance Letter no Subr
                {
                    report = new Reports.AcceptanceLetterNoSubr();
                    report.SummaryInfo.ReportTitle = ("LETTER OF ACCEPTANCE (no Subr.) - " + clno + " - " + dateofloss).Replace('/', '-');
                }
                else if (rbLetter5.Checked) //Acceptance Letter with Subr
                {
                    report = new Reports.AcceptanceLetterWithSubr();
                    report.SummaryInfo.ReportTitle = ("LETTER OF ACCEPTANCE (With Subr.) - " + clno + " - " + dateofloss).Replace('/', '-');
                }
                //

                report.SetDataSource(rptdata);
                var frm = new frmViewInstructionNote();
                frm.rpt = report;
                frm.Text = text;
                frm.Show();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

            Cursor.Current = Cursors.AppStarting;

        }

        private void frmAcceptanceFormCDV_Activated(object sender, EventArgs e)
        {
            tbClaimNo.Focus();
        }
    }
}
