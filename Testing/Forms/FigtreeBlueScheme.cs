using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class FigtreeBlueScheme : Form
    {
        CRUD crud = new CRUD();
        public bool abort = false, chck = false;
    
        public FigtreeBlueScheme()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void ConfirmChange()
        {
            if (chck == true)
            {
                DialogResult dr = Msgbox.Show("Your inquiry in the current form will be lost. Do you want to proceed to another form?", "Confirmation");
                if (dr == System.Windows.Forms.DialogResult.No)
                    abort = true;
                else
                    abort = false;

            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            clear_text();
            dis_button();
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            if (txtClaimNo.Text == "")
            {
                Msgbox.Show("Transactioin appear to have no content - Claim Number can not be NULL!");
            }
            else
            {
                if (txtClaimNo.Text.Substring(7, 3).ToUpper() != "BHP")
                {
                    Msgbox.Show("Transaction appear to have no content- Available only for product BHP");
                }
                else
                {
                   // //DataTable dt = crud.ExecQuery("select INT_CLAIM_NO,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) as Ins_Name,INT_PRS_NAME,to_char(INT_DATE_LOSS,'dd/mm/yyyy') as INT_DATE_LOSS,INT_POLICY_NO,INT_PERIOD_FROM,INT_PERIOD_TO,INT_DATE_LOSS,nvl(EXTRACT(year FROM CLM_MEM_DOB),extract(year from sysdate)) as year,(case when to_char(CLM_MEM_TYPE)='F' then 'FEMALE' when to_char(CLM_MEM_TYPE)='M' then 'MALE' end) Gender,(SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN,(PK_MONTHLY_REPORTS.FN_GET_DN_CN_ISPAID (PK_MONTHLY_REPORTS.FN_GET_ORIGINAL_DN(INT_POLICY_NO,INT_PERIOD_FROM))) payment_status,PRS_RELATIONSHIP_TYPE from UW_M_CUSTOMERS,CL_T_INTIMATION,CL_T_CLM_MEMBERS,uw_t_pol_risks where INT_CUS_CODE = CUS_CODE and INT_PRS_SEQ_NO = PRS_SEQ_NO and CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO='" + txtClaimNo.Text.ToUpper() + "'");
                   // DataTable dt = crud.ExecQuery("select INT_CLAIM_NO,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) as Ins_Name,INT_PRS_NAME, " +
                   //"regexp_replace(to_char(INT_DATE_LOSS,'DD MONTH YYYY'),'\\s+', ' ') as INT_DATE_LOSS,INT_POLICY_NO, " +
                   //"regexp_replace(to_char(INT_PERIOD_FROM,'DD MONTH YYYY'),'\\s+', ' ') INT_PERIOD_FROM,regexp_replace(to_char(INT_PERIOD_TO,'DD MONTH YYYY'),'\\s+', ' ') INT_PERIOD_TO, " +
                   //"nvl(EXTRACT(year FROM CLM_MEM_DOB),extract(year from sysdate)) as year,(case when to_char(CLM_MEM_TYPE)='F' then 'FEMALE' when to_char(CLM_MEM_TYPE)='M' then 'MALE' end) Gender, " +
                   //"(SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN,(PK_MONTHLY_REPORTS.FN_GET_DN_CN_ISPAID (PK_MONTHLY_REPORTS.FN_GET_ORIGINAL_DN(INT_POLICY_NO,INT_PERIOD_FROM))) payment_status,PRS_RELATIONSHIP_TYPE from UW_M_CUSTOMERS,CL_T_INTIMATION,CL_T_CLM_MEMBERS, "+
                   // "(select distinct * from ( select PRS_SEQ_NO,PRS_RELATIONSHIP_TYPE,PRS_R_SEQ,PRS_PLC_POL_SEQ_NO from uw_t_pol_risks " +
                   // "union " +
                   // "select ERS_SEQ_NO,ERS_RELATIONSHIP_TYPE,ERS_R_SEQ,ERS_ELC_EDT_SEQ_NO from uw_t_end_risks " +
                   // "union " +
                   // "select HRS_SEQ_NO,HRS_RELATIONSHIP_TYPE,HRS_R_SEQ,HRS_HLC_PHS_SEQ_NO from uw_h_hist_risks " +
                   // "union " +
                   // "select NRS_SEQ_NO,NRS_RELATIONSHIP_TYPE,NRS_R_SEQ,NRS_NLC_NDS_SEQ_NO from uw_h_ehist_risks ) ) " +
                   // "where INT_CUS_CODE = CUS_CODE and (INT_PRS_SEQ_NO = PRS_SEQ_NO or (PRS_R_SEQ=INT_PRS_R_SEQ AND INT_POLICY_SEQ=PRS_PLC_POL_SEQ_NO)) and CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO='" + txtClaimNo.Text.ToUpper() + "'");

                    string sp_type = "CheckClaim";
                    string[] Keys = new string[] { "sp_type", "sp_claim_no" };
                    //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                    string[] Values = new string[] { sp_type, txtClaimNo.Text.ToUpper() };
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dt = crud.ExecSP_OutPara("USER_HOPITAL_SCHEME_CHECK", Keys, Values);



                    if (dt.Rows.Count != 0)
                    {

                        enab_button();
                        foreach (DataRow dr in dt.Rows)
                        {
                            int a = DateTime.Today.Year - Convert.ToInt32(dr["year"].ToString());
                            txtPolicyHolder.Text = dr["Ins_Name"].ToString().ToUpper();
                            txtPolicyNumber.Text = dr["INT_POLICY_NO"].ToString().ToUpper();
                            txtMember.Text = dr["INT_PRS_NAME"].ToString().ToUpper();
                            txtEffectiveDate.Text = dr["INT_PERIOD_FROM"].ToString().ToUpper();
                            txtExpiryDate.Text = dr["INT_PERIOD_TO"].ToString().ToUpper();
                            txtLossDate.Text = dr["INT_DATE_LOSS"].ToString().ToUpper();
                            //txtAge.Text = (a == 0) ? "N/A" : Convert.ToString(a);
                            txtAge.Text = Convert.ToString(a); 
                            txtGender.Text = dr["Gender"].ToString().ToUpper();
                            txtPlan.Text = dr["Plan"].ToString().ToUpper();
                            txtPremiumStatus.Text = (dr["payment_status"].ToString().ToUpper() == "Y") ? "YES" : "NO";
                            txtMemberStatus.Text = dr["PRS_RELATIONSHIP_TYPE"].ToString().ToUpper();
                        }
                    }
                    else
                    {
                        Msgbox.Show("Transaction appear to have no content - Incorrect Claim Number!");
                    }
                }

            }
        }
        public void dis_button()
        {
            frmDocumentControl.disabledButt(btnSchemeOld);
            frmDocumentControl.disabledButt(btnSchemeLite);
        
        }
        public void en_button()
        {
            frmDocumentControl.enabledButt(btnSchemeOld);
            frmDocumentControl.enabledButt(btnSchemeLite);
            
        }
        public void enab_button()
        {
            DataTable dt = crud.ExecQuery("select SCHEME_DETAILS from user_claim_scheme where CLAIM_NUMBER='" + txtClaimNo.Text.ToUpper() + "'");

            if (dt.Rows.Count > 0)
            {
                DialogResult dr = Msgbox.Show("Claim number already has existing scheme, would you like to issue another scheme ?", "Confirmation", "YES", "NO");
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Button btnSchemeDt = Getbutton(dt.Rows[i][0].ToString());
                        frmDocumentControl.enabledButt(btnSchemeDt);
                    }

                }
                else
                {
                    en_button();
                }
            }
            else
            {
                en_button();
            }

        }
        Button Getbutton(string txtname)
        {
            Button Result = new Button();
            foreach (Control ctrl in splitContainer1.Panel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button tb = (Button)ctrl;
                    if (tb.Name == txtname)
                    {
                        Result = tb;
                        break;
                    }
                }
            }
            return Result;
        }
        public void clear_text()
        {
            txtPolicyHolder.Text = "";
            txtPolicyNumber.Text = "";
            txtEffectiveDate.Text = "";
            txtExpiryDate.Text = "";
            txtPremiumStatus.Text = "";
            txtMember.Text = "";
            txtMemberStatus.Text = "";
            txtGender.Text = "";
            txtAge.Text = "";
            txtPlan.Text = "";
            txtLossDate.Text = "";
        }

        private void FigtreeBlueScheme_Load(object sender, EventArgs e)
        {
            dis_button();
        }

        private void btnSchemeOld_Click(object sender, EventArgs e)
        {
            BHPSchemeOld frm = new BHPSchemeOld { TopLevel = false, TopMost = true };
            SetParent(frm.Handle, splitContainer1.Panel2.Handle);
            splitContainer1.Panel2.Controls.AddRange(new System.Windows.Forms.Control[] { frm });
            frm.Width = splitContainer1.Panel2.Width;
            //frm.Height = splitContainer1.Panel2.Height;
            frm.mainFrm = this;
            frm.BringToFront();
            ConfirmChange();
            if (abort == true)
                return;
            chck = true;
            frm.Show();
        }

        private void btnSchemeLite_Click(object sender, EventArgs e)
        {
            BHPSchemeLite frm = new BHPSchemeLite { TopLevel = false, TopMost = true };
            SetParent(frm.Handle, splitContainer1.Panel2.Handle);
            splitContainer1.Panel2.Controls.AddRange(new System.Windows.Forms.Control[] { frm });
            frm.Width = splitContainer1.Panel2.Width;
            //frm.Height = splitContainer1.Panel2.Height;
            frm.mainFrm = this;
            frm.BringToFront();
            ConfirmChange();
            if (abort == true)
                return;
            chck = true;
            frm.Show();
        }
        
    }
}
