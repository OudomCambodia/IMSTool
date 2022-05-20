using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class OPScheme : Form
    {
        CRUD crud = new CRUD();
        DataTable dtcopy = new DataTable();
        DataTable dtv2 = new DataTable();
        //double sum, sumpy;
        //Regex regex = new Regex(@"^[0-9]*(\.[0-9]{1,2})?$");
        Regex regex = new Regex(@"^[0-9,]*(\.[0-9]{1,2})?$");
        public HospitalSurgicalScheme mainFrm = new HospitalSurgicalScheme();
        public OPScheme()
        {
            InitializeComponent();
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


        bool IsNum(TextBox txt)
        {
            return !(String.IsNullOrEmpty(txt.Text) || !regex.IsMatch(txt.Text));
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
            double TADue = 0, TPDue = 0, TANPDue = 0, pav = 0, ppv = 0, pnp = 0;
            for (int i = 1; i <= 3; i++)
            {
                string txtname = "txtAC" + i;
                TextBox PYtxt = GetTextBox(txtname);
                PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(PYtxt.Text) ? 0.00 : Convert.ToDouble(PYtxt.Text)));
                double tmpd = IsNum(PYtxt) ? Convert.ToDouble(PYtxt.Text) : 0.00;
                    ACSum += tmpd;
            }

            for (int i = 1; i <= 3; i++)
            {
                string txtname = "txtPY" + i;
                TextBox PYtxt = GetTextBox(txtname);
                PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(PYtxt.Text) ? 0.00 : Convert.ToDouble(PYtxt.Text)));
                double tmpd = IsNum(PYtxt) ? Convert.ToDouble(PYtxt.Text) : 0.00;
                PYSum += tmpd;
            }


            for (int i = 1; i <= 3; i++)
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
            for (int i = 1; i <= 3; i++)
            {
                string txtname = "txtNP" + i;
                TextBox NPtxt = GetTextBox(txtname);
                double tmpd = IsNum(NPtxt) ? Convert.ToDouble(NPtxt.Text) : 0.00;
                NPSum += tmpd;
            }
            for (int i = 1; i <= 2; i++)
            {
                string txtname = "txtOP" + i;
                TextBox NPtxt = GetTextBox(txtname);
                NPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(NPtxt.Text) ? 0.00 : Convert.ToDouble(NPtxt.Text))); NPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(NPtxt.Text) ? 0.00 : Convert.ToDouble(NPtxt.Text)));

            }
            pav = IsNum(txtAC2) ? Convert.ToDouble(txtAC2.Text) : 0.00;
            ppv = IsNum(txtPY2) ? Convert.ToDouble(txtPY2.Text) : 0.00;
            pnp = IsNum(txtNP2) ? Convert.ToDouble(txtNP2.Text) : 0.00;
            TADue = Math.Round(ACSum - pav, 2);
            TPDue = Math.Round(PYSum - ppv, 2);
            
            TANPDue = Math.Round(NPSum - pnp, 2);
            txtAAC.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(ACSum, 2)));
            txtAPY.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(PYSum, 2)));
            txtANP.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(NPSum, 2)));
            txtAACDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TADue, 2)));
            txtAPYDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TPDue, 2)));
            txtANPDue.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(TANPDue, 2)));

        }

        private void cus_button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_temp = crud.ExecQuery("select CLAIM_SEQ_NO from user_claim_scheme where CLAIM_NUMBER='" + mainFrm.txtClaimNo.Text.ToUpper() + "' and SCHEME_DETAILS='btnOPScheme'");
                if (dt_temp.Rows.Count > 0)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    crud.ExecNonQuery("delete from user_scheme_op_limit where OP_CS_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                    crud.ExecNonQuery("delete from user_claim_details_scheme where CLAIM_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                    crud.ExecNonQuery("delete from user_claim_scheme where CLAIM_SEQ_NO = to_number('" + dt_temp.Rows[0][0].ToString() + "')");
                    if (call_sp_scheme("Claim_Insert"))
                    {
                        Msgbox.Show("Updated!");
                        Cursor.Current = Cursors.WaitCursor;
                    }
                    else
                    {
                        Msgbox.Show("Some input field is missing");
                    }
                    
                }
                else
                {

                    Cursor.Current = Cursors.WaitCursor;
                    if (call_sp_scheme("Claim_Insert"))
                    {
                        Msgbox.Show("Success!");
                        Cursor.Current = Cursors.WaitCursor;
                    }
                    else
                    {
                        Msgbox.Show("Some input field is missing");
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        public bool call_sp_scheme(string a)
        {

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
                               txtRemarkOPS.Text,"btnOPScheme","",txtDaignosis.Text  });

                for (int i = 1; i <= 3; i++)
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

                for (int i = 1; i <= 2; i++)
                {

                    TextBox OPtxt = GetTextBox("txtOP" + i);
                    TextBox OPDesctxt = GetTextBox("txtOpDesc" + i);
                    DataTable dt_OP = crud.ExecSP_OutPara("SP_USER_OP_SCHEME_DETAILS",
                              new string[] { "cl_input_type", "cl_op_description", 
                                         "cl_op_num_val", "cl_op_cs_seq_no", 
                                         "cl_create_date","cl_claim_op_chk"},
                              new string[] { a, OPDesctxt.Text, OPtxt.Text, dt.Rows[0][0].ToString(), DateTime.Now.ToShortDateString(), "" });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void txtOP1_Leave(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtOP2_Leave(object sender, EventArgs e)
        {
            Calculation();
        }


        
        private void OPScheme_Load(object sender, EventArgs e)
        {
            
            DataTable dt_load = crud.ExecQuery("select distinct t.*,CLAIM_NUMBER,SCHEME_DETAILS,CL_DAIGNOSIS,REMARK,INSURED_NAME,POLICY_NUMBER,regexp_replace(TO_CHAR(INCEPTION_DATE,'DD MONTH YYYY'),'\\s+', ' ') INCEPTION_DATE,regexp_replace(TO_CHAR(EXPIRY_DATE,'DD MONTH YYYY'),'\\s+', ' ') EXPIRY_DATE,PREMIUM_STATUS,MEMBER,MEMBER_STATUS,GENDER,to_CHAR(AGE) AGE,PLAN,regexp_replace(TO_CHAR(LOSS_DATE,'DD MONTH YYYY'),'\\s+', ' ') LOSS_DATE from  " +
            "(select OP_CS_SEQ_NO,OP_DESCRIPTION,OP_NUM_VAL,0 pay_cost from user_scheme_op_limit " +
            "union " +
            "select CLAIM_SEQ_NO,BENEFIT_DESC,ACTUAL_COST,PAYABLE_COST from user_claim_details_scheme)t, " +
            "user_claim_scheme where CLAIM_SEQ_NO = OP_CS_SEQ_NO " +
            "and SCHEME_DETAILS = 'btnOPScheme' and CLAIM_NUMBER='" + mainFrm.txtClaimNo.Text.ToUpper() + "'");
           
            if (dt_load.Rows.Count > 0)
            {
                dtv2 = dt_load.Copy();
                dtcopy = dt_load.Rows.Cast<DataRow>().GroupBy(r => r["CLAIM_NUMBER"]).Select(g => g.First()).CopyToDataTable();
                txtDaignosis.Text = dt_load.Rows[0]["CL_DAIGNOSIS"].ToString();
                txtRemarkOPS.Text = dt_load.Rows[0]["REMARK"].ToString();
              
                //load op limit data from database
                for (int i = 1; i <= 2; i++)
                {

                    TextBox OPtxt = GetTextBox("txtOP" + i);
                    TextBox OPDesctxt = GetTextBox("txtOpDesc" + i);
                    for (int j = 0; j < dt_load.Rows.Count; j++)
                    {
                        if (OPDesctxt.Text == dt_load.Rows[j]["OP_DESCRIPTION"].ToString())
                        {
                            OPtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt_load.Rows[j][2].ToString()), 2)));
                        }
                    }

                }

                //load ac and py data form database
                for (int i = 1; i <= 3; i++)
                {

                    TextBox ACtxt = GetTextBox("txtAC" + i);
                    TextBox PYtxt = GetTextBox("txtPY" + i);
                    TextBox Desctxt = GetTextBox("txtDesc" + i);
                    for (int j = 0; j < dt_load.Rows.Count; j++)
                    {
                        if (Desctxt.Text == dt_load.Rows[j]["OP_DESCRIPTION"].ToString())
                        {
                            ACtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2)));
                            PYtxt.Text = String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][3].ToString()), 2)));
                        }
                    }

                }
                Calculation();
            }

            
        }

        private void cus_button5_Click(object sender, EventArgs e) 
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                cus_button4.PerformClick();
                OPScheme_Load(sender, e);
                Reports.OPSchemeRP rpt = new Reports.OPSchemeRP();
                var frm = new frmViewInstructionNote();
                rpt.SetDataSource(dtcopy);
                if (dtv2.Rows.Count > 0)
                {

                    for (int i = 1; i <= 2; i++)
                    {

                        TextBox OPtxt = GetTextBox("txtOP" + i);
                        TextBox OPDesctxt = GetTextBox("txtOpDesc" + i);
                        for (int j = 0; j < dtv2.Rows.Count; j++)
                        {
                            if (OPDesctxt.Text == dtv2.Rows[j]["OP_DESCRIPTION"].ToString())
                            {
                                rpt.SetParameterValue("txtOP" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2))));
                            }
                        }

                    }
                    //load ac and py data form database
                    for (int i = 1; i <= 3; i++)
                    {

                        TextBox ACtxt = GetTextBox("txtAC" + i);
                        TextBox PYtxt = GetTextBox("txtPY" + i);
                        TextBox NPtxt = GetTextBox("txtNP" + i);
                        TextBox Desctxt = GetTextBox("txtDesc" + i);
                        for (int j = 0; j < dtv2.Rows.Count; j++)
                        {
                            if (Desctxt.Text == dtv2.Rows[j]["OP_DESCRIPTION"].ToString())
                            {
                                rpt.SetParameterValue("txtAC" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][2].ToString()), 2))));
                                rpt.SetParameterValue("txtPY" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtv2.Rows[j][3].ToString()), 2))));
                                rpt.SetParameterValue("txtNP" + i, String.Format("{0:N}", Convert.ToDecimal(Math.Round(Convert.ToDecimal(NPtxt.Text), 2))));
                            }
                        }

                    }

                    //rpt.SetParameterValue("inceptiondate", dtcopy.Rows[0]["INCEPTION_DATE"].ToString());
                    frm.rpt = rpt;
                    frm.Show();
                    
                }
                else
                {
                    Msgbox.Show("No Data found to be loaded. Please fill in data first!");
                }
            }
            catch(Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
           
            dtcopy.Clear(); 
        }
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
        #region enterkeytoanothertab
        //private void txtDaignosis_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    enterkeytotab(e);
        //}

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

        private void txtOP1_KeyPress(object sender, KeyPressEventArgs e)
        {

            enterkeytotab(e);
        }

        private void txtOP2_KeyPress(object sender, KeyPressEventArgs e)
        {

            enterkeytotab(e);
        }

        //private void txtRemarkOPS_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

    }
}
