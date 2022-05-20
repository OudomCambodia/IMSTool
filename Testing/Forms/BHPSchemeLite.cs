using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class BHPSchemeLite : Form
    {
        CRUD crud = new CRUD();
        DataTable dtcopy = new DataTable();
        DataTable dtv2 = new DataTable();
        //Regex regex = new Regex(@"^[0-9]*(\.[0-9]{1,2})?$");
        Regex regex = new Regex(@"^[0-9,]*(\.[0-9]{1,2})?$");
        public FigtreeBlueScheme mainFrm = new FigtreeBlueScheme();
        TextBox GetDatetimepicker(string txtname)
        {
            TextBox Result = new TextBox();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb.Name == txtname)
                    {
                        Result = tb;
                        break;
                    }
                }
            }
            return Result;
        }
        bool IsNum(TextBox txt)
        {
            return !(String.IsNullOrEmpty(txt.Text) || !regex.IsMatch(txt.Text));
        }
        public void GetDateValue(TextBox textbox)
        {
            string s = textbox.Text;
            DateTime datevalue;
            string formatdate = "dd/MM/yyyy";
            string result = "";
            if (textbox.Text != "")
            {
                if (textbox.Text.Length == 8 && IsNum(textbox))
                {
                    result = s.Substring(0, 2) + "/" + s.Substring(2, 2) + "/" + s.Substring(4, 4);
                    if (DateTime.TryParseExact(result, formatdate, new System.Globalization.CultureInfo("en-US"),
                        System.Globalization.DateTimeStyles.None, out datevalue))
                    {
                        textbox.Text = result;
                    }
                    else
                    {
                        Msgbox.Show("The Input Date format is not correct!");
                    }
                }
                else
                {
                    Msgbox.Show("The Input Date format is not correct!");
                }
            }
            else return;
        }
        TextBox GetTextBox(string txtname)
        {

            TextBox Result = new TextBox();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb.Name == txtname)
                    {
                        string s = tb.Text;
                        Result = tb;
                        break;
                    }
                }
            }
            return Result;
        }
        void Calculation()
        {
            double ACSum = 0;
            double PYSum = 0;
            double NPSum = 0;
            double TADue = 0, TPDue = 0, TANPDue = 0, pav = 0,TPYDue=0, ppv = 0, pnp = 0;
            for (int i = 1; i <= 26; i++)
            {
                string txtname = "txtAC" + i;
                TextBox PYtxt = GetTextBox(txtname);
                PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(PYtxt.Text) ? 0.00 : Convert.ToDouble(PYtxt.Text)));
                double tmpd = IsNum(PYtxt) ? Convert.ToDouble(PYtxt.Text) : 0.00;
                if (i == 26)
                    break;
                else
                    ACSum += tmpd;
            }

            for (int i = 1; i <= 26; i++)
            {
                string txtname = "txtPY" + i;
                TextBox PYtxt = GetTextBox(txtname);
                PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(PYtxt.Text) ? 0.00 : Convert.ToDouble(PYtxt.Text)));
                double tmpd = IsNum(PYtxt) ? Convert.ToDouble(PYtxt.Text) : 0.00;
                if (i == 26)
                    break;
                else
                    PYSum += tmpd;
            }


            for (int i = 1; i <= 26; i++)
            {

                TextBox ACtxt = GetTextBox("txtAC" + i);
                TextBox PYtxt = GetTextBox("txtPY" + i);
                double AC = IsNum(ACtxt) ? Convert.ToDouble(ACtxt.Text) : 0.00;
                double PY = IsNum(PYtxt) ? Convert.ToDouble(PYtxt.Text) : 0.00;
                double NP = Math.Round(AC - PY, 2);
                TextBox NPtxt = GetTextBox("txtNP" + i);
                //if (NP < 0)
                //{
                //    NPtxt.Text = "0.00";
                //}
                //else
                //    //NPtxt.Text = NP.ToString();
                    NPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(NP, 2)));
            }
            for (int i = 1; i <= 26; i++)
            {
                string txtname = "txtNP" + i;
                TextBox NPtxt = GetTextBox(txtname);
                double tmpd = IsNum(NPtxt) ? Convert.ToDouble(NPtxt.Text) : 0.00;
                if (i == 26)
                    break;
                else
                    NPSum += tmpd;
            }

            for (int i = 1; i <= 4; i++)
            {
                string txtname = "txtUSD" + i;
                TextBox NPtxt = GetTextBox(txtname);
                NPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(NPtxt.Text) ? 0.00 : Convert.ToDouble(NPtxt.Text))); NPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(NPtxt.Text) ? 0.00 : Convert.ToDouble(NPtxt.Text)));

            }

            pav = IsNum(txtAC26) ? Convert.ToDouble(txtAC26.Text) : 0.00;
            ppv = IsNum(txtPY26) ? Convert.ToDouble(txtPY26.Text) : 0.00;
            pnp = IsNum(txtNP26) ? Convert.ToDouble(txtNP26.Text) : 0.00;
            TADue = Math.Round(ACSum - pav, 2);
            TPDue = Math.Round(PYSum - ppv, 2);
            TPYDue = Math.Round(NPSum - pnp, 2);
            TANPDue = Math.Round(NPSum - pnp, 2);
            txtAAC.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(ACSum, 2)));
            txtAPY.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(PYSum, 2)));
            txtANP.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(NPSum, 2)));
            txtAACDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TADue, 2)));
            txtAPYDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TPDue, 2)));
            //txtANPDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TANPDue, 2)));
            if (TANPDue < 0)
            {
                txtANPDue.Text = "0.00";
            }
            else
            {
                txtANPDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TANPDue, 2)));
            }

        }
        public bool call_sp_scheme(string a)
        {
            //string desc;
            try
            {

                //double temp = Convert.ToDouble(mainFrm.txtAge.Text.Remove(1)) / 10;
                DataTable dt = crud.ExecSP_OutPara("sp_user_hospital_scheme", new string[] { "cl_input_type", 
                "cl_insured_name", "cl_policy_number", "cl_inception_date", 
                "cl_expiry_date", "cl_premium_status", "cl_member", "cl_member_status", 
                "cl_gender", "cl_age", "cl_plan", "cl_loss_date", "cl_claim_number", "cl_create_date", "cl_remark", "cl_btn_details","cl_seq_no_temp","cl_daignosis" },
                                  new string[] {a,mainFrm.txtPolicyHolder.Text,
                               mainFrm.txtPolicyNumber.Text,
                               mainFrm.txtEffectiveDate.Text,
                               mainFrm.txtExpiryDate.Text,
                               mainFrm.txtPremiumStatus.Text,
                               mainFrm.txtMember.Text,
                               mainFrm.txtMemberStatus.Text,
                               mainFrm.txtGender.Text,
                             mainFrm.txtAge.Text,
                               mainFrm.txtPlan.Text,
                               mainFrm.txtLossDate.Text,
                               mainFrm.txtClaimNo.Text.ToUpper(),
                               DateTime.Now.ToShortDateString(),
                               txtRemarkSchemelite.Text,"btnSchemeLite","",txtDaignosis.Text  });

                for (int i = 1; i <= 4; i++)
                {
                    TextBox ACtxt = GetTextBox("txtAC" + i);
                    TextBox PYtxt = GetTextBox("txtPY" + i);
                    TextBox dtPicker = GetDatetimepicker("dateTimePicker" + i);
                    TextBox dtTo = GetDatetimepicker("dateTimeTo" + i);
                    TextBox NoDaytxt = GetTextBox("txtNoDays" + i);
                    TextBox USDtxt = GetTextBox("txtUSD" + i);
                    if (i <= 2)
                    {
                        //desc = "i) Ordinary Room";
                        DataTable dt_details = crud.ExecSP_OutPara("sp_user_hospital_details",
                              new string[] { "cl_input_type", "cl_actual_cost", 
                                         "cl_payable_cost", "cl_benefit_desc", 
                                         "cl_from_date", "cl_no_days", 
                                         "cl_to_date", "cl_usd_per_days", "cl_claim_seq_no", 
                                         "cl_create_date","cl_seq_no_temp_de"},
                              new string[] { a, ACtxt.Text.Replace(",", ""), PYtxt.Text.Replace(",", ""), "Hospital Room and Board: " + i, String.IsNullOrEmpty(dtPicker.Text) ? "" : Convert.ToDateTime(dtPicker.Text).ToShortDateString(), NoDaytxt.Text, String.IsNullOrEmpty(dtTo.Text) ? "" : Convert.ToDateTime(dtTo.Text).ToShortDateString(), USDtxt.Text, dt.Rows[0][0].ToString(), DateTime.Now.ToShortDateString(), "" });
                    }
                    else
                    {
                        //desc = "ii) Intensive Care Unit / ICU";
                        DataTable dt_details = crud.ExecSP_OutPara("sp_user_hospital_details",
                            new string[] { "cl_input_type", "cl_actual_cost", 
                                         "cl_payable_cost", "cl_benefit_desc", 
                                         "cl_from_date", "cl_no_days", 
                                         "cl_to_date", "cl_usd_per_days", "cl_claim_seq_no", 
                                         "cl_create_date","cl_seq_no_temp_de"},
                            new string[] { a, ACtxt.Text.Replace(",", ""), PYtxt.Text.Replace(",", ""), "Intensive Care Unit / ICU " + i, String.IsNullOrEmpty(dtPicker.Text) ? "" : Convert.ToDateTime(dtPicker.Text).ToShortDateString(), NoDaytxt.Text, String.IsNullOrEmpty(dtTo.Text) ? "" : Convert.ToDateTime(dtTo.Text).ToShortDateString(), USDtxt.Text, dt.Rows[0][0].ToString(), DateTime.Now.ToShortDateString(), "" });
                    }
                }

                for (int i = 5; i <= 26; i++)
                {

                    TextBox ACtxt = GetTextBox("txtAC" + i);
                    TextBox PYtxt = GetTextBox("txtPY" + i);
                    TextBox Desctxt = GetTextBox("txtDesc" + i);

                    DataTable dt_details = crud.ExecSP_OutPara("sp_user_hospital_details",
                              new string[] { "cl_input_type", "cl_actual_cost", 
                                         "cl_payable_cost", "cl_benefit_desc", 
                                         "cl_from_date", "cl_no_days", 
                                         "cl_to_date", "cl_usd_per_days", "cl_claim_seq_no", 
                                         "cl_create_date","cl_seq_no_temp_de"},
                              new string[] { a, ACtxt.Text.Replace(",", ""), PYtxt.Text.Replace(",", ""), Desctxt.Text, "", "", "", "", dt.Rows[0][0].ToString(), DateTime.Now.ToShortDateString(), "" });
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
                
            }
        }

        public BHPSchemeLite()
        {
            InitializeComponent();
        }


        private void cus_button5_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //CultureInfo culture = new CultureInfo("en-US"); 
                btnSave.PerformClick();
                BHPSchemeLite_Load(sender, e);
                Reports.BHPSchemLite rpt = new Reports.BHPSchemLite();
                var frm = new frmViewInstructionNote();
                rpt.SetDataSource(dtcopy);
                if (dtv2.Rows.Count > 0)
                {


                    for (int i = 5; i <= 26; i++)
                    {

                        TextBox ACtxt = GetTextBox("txtAC" + i);
                        TextBox PYtxt = GetTextBox("txtPY" + i);
                        TextBox Desctxt = GetTextBox("txtDesc" + i);
                        TextBox NPtxt = GetTextBox("txtNP" + i);
                        for (int j = 0; j < dtv2.Rows.Count; j++)
                        {
                            if (Desctxt.Text == dtv2.Rows[j]["BENEFIT_DESC"].ToString())
                            {
                                rpt.SetParameterValue("txtAC" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2))));
                                rpt.SetParameterValue("txtPY" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][3].ToString()), 2))));
                                rpt.SetParameterValue("txtNP" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(NPtxt.Text), 2))));
                            }
                        }

                    }
                    //load data i and ii from database
                    for (int i = 1; i <= 4; i++)
                    {
                        TextBox ACtxt = GetTextBox("txtAC" + i);
                        TextBox PYtxt = GetTextBox("txtPY" + i);
                        TextBox NPtxt = GetTextBox("txtNP" + i);
                        TextBox dtPicker = GetDatetimepicker("dateTimePicker" + i);
                        TextBox dtTo = GetDatetimepicker("dateTimeTo" + i);
                        TextBox NoDaytxt = GetTextBox("txtNoDays" + i);
                        TextBox USDtxt = GetTextBox("txtUSD" + i);

                        for (int j = 0; j < dtv2.Rows.Count; j++)
                        {
                            if (dtv2.Rows[j]["BENEFIT_DESC"].ToString() == "Intensive Care Unit / ICU " + i)
                            {
                                // rpt.SetParameterValue("dateTimePicker" + i, Convert.ToDateTime(dtv2.Rows[j]["FROM_DATE"].ToString(),culture));
                                rpt.SetParameterValue("dateTimePicker" + i, dtv2.Rows[j]["FROM_DATE"].ToString());
                                //rpt.SetParameterValue("dateTimeTo" + i, Convert.ToDateTime(dtv2.Rows[j]["TO_DATE_SCHEME"].ToString()));
                                rpt.SetParameterValue("dateTimeTo" + i, dtv2.Rows[j]["TO_DATE_SCHEME"].ToString());
                                rpt.SetParameterValue("txtAC" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2))));
                                rpt.SetParameterValue("txtPY" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][3].ToString()), 2))));
                                rpt.SetParameterValue("txtNP" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(NPtxt.Text), 2))));
                                rpt.SetParameterValue("txtNoDays" + i, dtv2.Rows[j]["NO_DAYS"].ToString());
                                rpt.SetParameterValue("txtUSD" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j]["USD_PER_DAY"].ToString()), 2))));
                            }
                            if (dtv2.Rows[j]["BENEFIT_DESC"].ToString() == "Hospital Room and Board: " + i)
                            {
                                rpt.SetParameterValue("dateTimePicker" + i, dtv2.Rows[j]["FROM_DATE"].ToString());
                                rpt.SetParameterValue("dateTimeTo" + i, dtv2.Rows[j]["TO_DATE_SCHEME"].ToString());
                                rpt.SetParameterValue("txtAC" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2))));
                                rpt.SetParameterValue("txtPY" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][3].ToString()), 2))));
                                rpt.SetParameterValue("txtNP" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(NPtxt.Text), 2))));
                                rpt.SetParameterValue("txtNoDays" + i, dtv2.Rows[j]["NO_DAYS"].ToString());
                                rpt.SetParameterValue("txtUSD" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j]["USD_PER_DAY"].ToString()), 2))));
                            }
                        }
                    }
                    //rpt.SetParameterValue("txtANPDue", String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(txtANPDue.Text), 2))));
                    //rpt.SetParameterValue("inceptiondate", dtcopy.Rows[0]["INCEPTION_DATE"].ToString());
                    frm.rpt = rpt;
                    frm.Show();

                }
                else
                {
                    Msgbox.Show("No Data found to be loaded. Please fill in data first!");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

            dtcopy.Clear();
        }

        private void BHPSchemeLite_Load(object sender, EventArgs e)
        {
            DataTable dt_load = crud.ExecQuery("select distinct t.*,CLAIM_NUMBER,SCHEME_DETAILS,CL_DAIGNOSIS,REMARK,INSURED_NAME,POLICY_NUMBER,regexp_replace(TO_CHAR(INCEPTION_DATE,'DD MONTH YYYY'),'\\s+', ' ') INCEPTION_DATE,regexp_replace(TO_CHAR(EXPIRY_DATE,'DD MONTH YYYY'),'\\s+', ' ') EXPIRY_DATE,PREMIUM_STATUS,MEMBER,MEMBER_STATUS,GENDER,to_CHAR(AGE) AGE,PLAN,regexp_replace(TO_CHAR(LOSS_DATE,'DD MONTH YYYY'),'\\s+', ' ') LOSS_DATE from ( " +
                  "select CLAIM_SEQ_NO,BENEFIT_DESC,ACTUAL_COST,PAYABLE_COST,regexp_replace(TO_CHAR(FROM_DATE,'DD MONTH YYYY'),'\\s+', ' ') FROM_DATE,NO_DAYS,regexp_replace(TO_CHAR(TO_DATE_SCHEME,'DD MONTH YYYY'),'\\s+', ' ') TO_DATE_SCHEME,usd_per_day from user_claim_details_scheme " +
                  "union " +
                  "select OP_CS_SEQ_NO,OP_DESCRIPTION,OP_NUM_VAL,0 pay_cost,regexp_replace(TO_CHAR(to_date(''),'DD MONTH YYYY'),'\\s+', ' '),to_number(''),regexp_replace(TO_CHAR(to_date(''),'DD MONTH YYYY'),'\\s+', ' '),to_number('') from user_scheme_op_limit " +
                  ")t, " +
                  "user_claim_scheme b " +
                  "where b.CLAIM_SEQ_NO = t.CLAIM_SEQ_NO " +
                  "and SCHEME_DETAILS = 'btnSchemeLite' and CLAIM_NUMBER = '" + mainFrm.txtClaimNo.Text.ToUpper() + "'");

            if (dt_load.Rows.Count > 0)
            {
                txtDaignosis.Text = dt_load.Rows[0]["CL_DAIGNOSIS"].ToString();
                txtRemarkSchemelite.Text = dt_load.Rows[0]["REMARK"].ToString();
                dtv2 = dt_load.Copy();
                dtcopy = dt_load.Rows.Cast<DataRow>().GroupBy(r => r["CLAIM_NUMBER"]).Select(g => g.First()).CopyToDataTable();
                //load op limit data from database
                //for (int i = 1; i <= 2; i++)
                //{

                //    TextBox OPtxt = GetTextBox("txtOP" + i);
                //    TextBox OPDesctxt = GetTextBox("txtOpDesc" + i);
                //    for (int j = 0; j < dt_load.Rows.Count; j++)
                //    {
                //        if (OPDesctxt.Text == dt_load.Rows[j]["OP_DESCRIPTION"].ToString())
                //        {
                //            OPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][2].ToString()), 2)));
                //        }
                //    }

                //}

                //load ac and py data form database
                for (int i = 5; i <= 26; i++)
                {

                    TextBox ACtxt = GetTextBox("txtAC" + i);
                    TextBox PYtxt = GetTextBox("txtPY" + i);
                    TextBox Desctxt = GetTextBox("txtDesc" + i);
                    for (int j = 0; j < dt_load.Rows.Count; j++)
                    {
                        if (Desctxt.Text == dt_load.Rows[j]["BENEFIT_DESC"].ToString())
                        {
                            ACtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][2].ToString()), 2)));
                            PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][3].ToString()), 2)));
                        }
                    }

                }
                //load data i and ii from database
                for (int i = 1; i <= 4; i++)
                {
                    TextBox ACtxt = GetTextBox("txtAC" + i);
                    TextBox PYtxt = GetTextBox("txtPY" + i);
                    TextBox dtPicker = GetDatetimepicker("dateTimePicker" + i);
                    TextBox dtTo = GetDatetimepicker("dateTimeTo" + i);
                    TextBox NoDaytxt = GetTextBox("txtNoDays" + i);
                    TextBox USDtxt = GetTextBox("txtUSD" + i);

                    for (int j = 0; j < dt_load.Rows.Count; j++)
                    {
                        if (dt_load.Rows[j]["BENEFIT_DESC"].ToString() == "Intensive Care Unit / ICU " + i)
                        {
                            dtPicker.Text = String.IsNullOrEmpty(dt_load.Rows[j]["FROM_DATE"].ToString()) ? "" : Convert.ToDateTime(dt_load.Rows[j]["FROM_DATE"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dtTo.Text = String.IsNullOrEmpty(dt_load.Rows[j]["TO_DATE_SCHEME"].ToString()) ? "" : Convert.ToDateTime(dt_load.Rows[j]["TO_DATE_SCHEME"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ACtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][2].ToString()), 2)));
                            PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][3].ToString()), 2)));
                            NoDaytxt.Text = dt_load.Rows[j]["NO_DAYS"].ToString();
                            USDtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j]["USD_PER_DAY"].ToString()), 2)));
                        }
                        if (dt_load.Rows[j]["BENEFIT_DESC"].ToString() == "Hospital Room and Board: " + i)
                        {
                            dtPicker.Text = String.IsNullOrEmpty(dt_load.Rows[j]["FROM_DATE"].ToString()) ? "" : Convert.ToDateTime(dt_load.Rows[j]["FROM_DATE"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dtTo.Text = String.IsNullOrEmpty(dt_load.Rows[j]["TO_DATE_SCHEME"].ToString()) ? "" : Convert.ToDateTime(dt_load.Rows[j]["TO_DATE_SCHEME"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ACtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][2].ToString()), 2)));
                            PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][3].ToString()), 2)));
                            NoDaytxt.Text = dt_load.Rows[j]["NO_DAYS"].ToString();
                            USDtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j]["USD_PER_DAY"].ToString()), 2)));
                        }
                    }
                }
                Calculation();
            }
            
        }
        #region Getdatevalue
        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimePicker1);
        }

        private void dateTimeTo1_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimeTo1);
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimePicker2);
        }

        private void dateTimeTo2_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimeTo2);
        }

        private void dateTimePicker3_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimePicker3);
        }

        private void dateTimeTo3_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimeTo3);
        }

        private void dateTimePicker4_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimePicker4);
        }

        private void dateTimeTo4_Leave(object sender, EventArgs e)
        {
            GetDateValue(dateTimeTo4);
        }
        #endregion
        #region textbox
        private void txtUSD1_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtUSD2_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtUSD3_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtUSD4_Leave(object sender, EventArgs e)
        {
            Calculation();
        }
        #endregion

        private void txtAC17_TextChanged(object sender, EventArgs e)
        {
            
        }
        #region Textcallbox
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_temp = crud.ExecQuery("select CLAIM_SEQ_NO from user_claim_scheme where CLAIM_NUMBER='" + mainFrm.txtClaimNo.Text.ToUpper() + "' and SCHEME_DETAILS='btnSchemeLite'");
                if (dt_temp.Rows.Count > 0)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        crud.ExecNonQuery("delete from user_scheme_op_limit where OP_CS_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                        crud.ExecNonQuery("delete from user_claim_details_scheme where CLAIM_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                        crud.ExecNonQuery("delete from user_claim_scheme where CLAIM_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                        if (call_sp_scheme("Claim_Insert"))
                        {
                            Msgbox.Show("Updated!");
                        }
                        else
                        {
                            Msgbox.Show("Some input field are missing!"); 
                        }
                        Cursor.Current = Cursors.WaitCursor;
                        
                    }catch(Exception ex){
                        Msgbox.Show(ex.Message);
                    }
                    
                    Cursor.Current = Cursors.WaitCursor;
                }
                else
                {
                    try
                    {
                        if (call_sp_scheme("Claim_Insert"))
                        {
                            Msgbox.Show("Success!");
                        }
                        else
                        {
                            Msgbox.Show("Some input field are missing!"); 
                        }
                        Cursor.Current = Cursors.WaitCursor;
                    }
                    catch (Exception ex)
                    {
                        Msgbox.Show(ex.Message);
                    }
                    
                    
                    Cursor.Current = Cursors.WaitCursor;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void txtAC1_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY1_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC2_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY2_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC3_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY3_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC4_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY4_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC5_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY5_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC6_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY6_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC7_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY7_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC8_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY8_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC9_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY9_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC10_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY10_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC11_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtPY11_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC12_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY12_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC13_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY13_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC14_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY14_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtAC15_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtPY15_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtAC16_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY16_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC17_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY17_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtAC18_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY18_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC19_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY19_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC20_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY20_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC21_Leave(object sender, EventArgs e)
        {
            Calculation();

        }

        private void txtPY21_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC22_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY22_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY23_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC24_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY24_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC25_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY25_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAC26_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPY26_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        #endregion

        public void enterkeytotab(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ActiveControl != null)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                e.Handled = true; // Mark the event as handled
            }
        }
        #region entertabtoanother
        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimeTo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimeTo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimePicker3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimeTo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimePicker4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void dateTimeTo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtNoDays1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtUSD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtNoDays2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtUSD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtNoDays3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtUSD3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtNoDays4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtUSD4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC5_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC6_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);

        }

        private void txtAC7_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC8_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC9_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC10_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC11_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC12_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC13_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC14_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);

        }

        private void txtAC15_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC16_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC17_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC18_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC19_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC20_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC21_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC22_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC23_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC24_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC25_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtAC26_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        //private void txtRemarkSchemelite_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    enterkeytotab(e);
        //}

        //private void txtDaignosis_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    enterkeytotab(e);
        //}

        private void txtPY1_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY2_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY3_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY4_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);

        }

        private void txtPY5_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY6_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY7_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY8_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY9_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY10_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY11_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY12_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);

        }

        private void txtPY13_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY14_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY15_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY16_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY17_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY18_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY20_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY21_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY22_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY23_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);

        }

        private void txtPY24_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY25_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }

        private void txtPY26_KeyPress(object sender, KeyPressEventArgs e)
        {
            enterkeytotab(e);
        }
        #endregion
    }
}
