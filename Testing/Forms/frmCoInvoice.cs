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
    public partial class frmCoInvoice : Form
    {
        CRUD crud = new CRUD();
        DataRow dr;
        DataTable dt = new DataTable();

        string sql;
        public string UserName = "SICL";
        public frmCoInvoice()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPolicyNo.Clear();
            comBoxDebit.DataSource = null;
            comBoxDebit.Text = "Select ALL";
            crystalReportViewer1.ReportSource = null;
        }
        private void dgvCoIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvCoIn);
        }

        public void BindComboBox(string policyNo)
        {
            DataRow dr, dr1;
            //string SQLcombox = "select DEB_DEB_NOTE_NO ,DEB_DEB_NOTE_NO No from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + policyNo + "' order by CREATED_DATE DESC";
            string SQLcombox = "select DEB_DEB_NOTE_NO ,No from " +
            "( " +
            "select DEB_DEB_NOTE_NO ,DEB_DEB_NOTE_NO No, CREATED_DATE from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + policyNo + "' " +
            "union " +
            "select CRN_CREDIT_NOTE_NO ,CRN_CREDIT_NOTE_NO No, CREATED_DATE from RC_T_CREDIT_NOTE where CRN_POL_POLICY_NO='" + policyNo + "' " +
            ") " +
            "order by CREATED_DATE DESC";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            comBoxDebit.ValueMember = "DEB_DEB_NOTE_NO";
            comBoxDebit.DisplayMember = "NO";
            comBoxDebit.DataSource = dtCombox;
           
            
            //end of combobox of Policy Transaction *All per policy
        }

        private void tbPolicyNo_Leave(object sender, EventArgs e)
        {
            try
            {
                BindComboBox(tbPolicyNo.Text.ToUpper());

            }

            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void frmCoInvoice_Load(object sender, EventArgs e)
        {

          
            sql = "SELECT allow FROM USER_PRINT_TYPE WHERE TYPE = (SELECT TYPE FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + UserName + "')";
            string allow = crud.ExecQuery(sql).Rows[0].ItemArray[0].ToString();
            string[] splitAllow = allow.Split(',').ToArray();
            sql = "select * from USER_PRINT_ALLOW_BY_FORM Where REFERENCE_NO in (";
            foreach (string eachAllow in splitAllow)
                sql += "'" + eachAllow + "',";
            sql = sql.Remove(sql.Length - 1, 1);
            sql += ") and FORM_NAME = '" + this.Name + "'";
            DataTable allowRef = crud.ExecQuery(sql);
            foreach (DataRow drRef in allowRef.Rows)
            {
                if (this.Controls.Find(drRef[2].ToString(), true).Length > 0)
                {
                    ((Button)this.Controls.Find(drRef[2].ToString(), true)[0]).Enabled = true;
                    ((Button)this.Controls.Find(drRef[2].ToString(), true)[0]).BackColor = Color.FromArgb(0, 9, 47);
                }
            }

            dgvCoIn.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCoIn.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCoinvoice = new DataTable();

                Cursor.Current = Cursors.WaitCursor;
                string sp_type = "CO_FILING";
                string[] Keys = new string[] { "sp_type", "user_dn_cn"};
                string[] Values = new string[] { sp_type, comBoxDebit.SelectedValue.ToString() };
                dtCoinvoice = crud.ExecSP_OutPara("SP_CO_FILING", Keys, Values);
               

                if (dtCoinvoice.Rows.Count <= 0)
                {
                    Msgbox.Show("Please check whether you input the correct Policy Number!");
                }
                else
                {
                    
                    string a = dtCoinvoice.Rows[0]["ENDORSEMENT_NO"].ToString();
                    if(a != "R") {
                        Reports.CoFilingInvoiceEnd coinv = new Reports.CoFilingInvoiceEnd();
                        coinv.SetDataSource(dtCoinvoice);
                        if(dgvCoIn.Rows.Count !=0)
                            coinv.SetParameterValue("CO_SHARE", decimal.Parse(String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(dgvCoIn.Rows[0].Cells[1].Value.ToString()) ? 0.00 : Convert.ToDouble(dgvCoIn.Rows[0].Cells[1].Value.ToString())))));
                        crystalReportViewer1.ReportSource = coinv;
                    }
                    else
                    {
                        Reports.CoFilingInvoice coinv = new Reports.CoFilingInvoice();
                        coinv.SetDataSource(dtCoinvoice);
                        if (dgvCoIn.Rows.Count != 0)

                            coinv.SetParameterValue("CO_SHARE", decimal.Parse(String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(dgvCoIn.Rows[0].Cells[1].Value.ToString()) ? 0.00 : Convert.ToDouble(dgvCoIn.Rows[0].Cells[1].Value.ToString())))));
                        crystalReportViewer1.ReportSource = coinv;
                    }
                    
                }
                
                
                
            }catch(Exception ex){
                Msgbox.Show(ex.Message);
            }
        }

        private void comBoxDebit_SelectedValueChanged(object sender, EventArgs e)
        {
            dgvCoIn.DataSource = null;
            dgvCoIn.Columns.Clear();
            
            //string dnNumber = comBoxDebit.SelectedValue.ToString();
            ////Set CoIn Info
            //string cmd = "select (select INC_INSCOM_DESC from SM_R_INSURANCE_COM where INC_INSCOM_CODE = PCI_INS_CODE) CO_PARTY,nvl(SHARE_PER,0) SHARE_PER from ( " +
            //"select PCI_POL_SEQ_NO,PCI_INS_CODE,sum(PCI_SHARE_PCNTG) SHARE_PER " +
            //"from UW_T_POL_COINS_INFO  " +
            //"where PCI_POL_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')  " +
            //"group by PCI_POL_SEQ_NO,PCI_INS_CODE " +
            //"union " +
            //"select ECI_EDT_SEQ_NO,ECI_INS_CODE,sum(ECI_SHARE_PCNTG) SHARE_PER " +
            //"from UW_T_END_COINS_INFO  " +
            //"where ECI_EDT_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
            //"group by ECI_EDT_SEQ_NO,ECI_INS_CODE " +
            //"union " +
            //"select HCI_PHS_SEQ_NO,HCI_INS_CODE, sum(HCI_SHARE_PCNTG) SHARE_PER " +
            //"from UW_H_HIST_COINS_INFO  " +
            //"where HCI_PHS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
            //"group by HCI_PHS_SEQ_NO,HCI_INS_CODE " +
            //"union " +
            //"select NCI_NDS_SEQ_NO,NCI_INS_CODE,sum(NCI_SHARE_PCNTG) SHARE_PER " +
            //"from UW_H_EHIST_COINS_INFO  " +
            //"where NCI_NDS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
            //"group by NCI_NDS_SEQ_NO,NCI_INS_CODE)";
            //DataTable dtTemp = new DataTable();
            //dtTemp = crud.ExecQuery(cmd);
            //if (dtTemp.Rows.Count > 0)
            //{
            //    dgvCoIn.DataSource = dtTemp;
            //}
            //
        }

        private void comBoxDebit_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvCoIn.DataSource = null;
            dgvCoIn.Columns.Clear();
            if (comBoxDebit.SelectedIndex != 0)
            {
                string dnNumber = comBoxDebit.SelectedValue.ToString();
                //Set CoIn Info
                string cmd = "select (select INC_INSCOM_DESC from SM_R_INSURANCE_COM where INC_INSCOM_CODE = PCI_INS_CODE) CO_PARTY,nvl(SHARE_PER,0) SHARE_PER from ( " +
                "select PCI_POL_SEQ_NO,PCI_INS_CODE,sum(PCI_SHARE_PCNTG) SHARE_PER " +
                "from UW_T_POL_COINS_INFO  " +
                "where PCI_POL_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')  " +
                "group by PCI_POL_SEQ_NO,PCI_INS_CODE " +
                "union " +
                "select ECI_EDT_SEQ_NO,ECI_INS_CODE,sum(ECI_SHARE_PCNTG) SHARE_PER " +
                "from UW_T_END_COINS_INFO  " +
                "where ECI_EDT_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
                "group by ECI_EDT_SEQ_NO,ECI_INS_CODE " +
                "union " +
                "select HCI_PHS_SEQ_NO,HCI_INS_CODE, sum(HCI_SHARE_PCNTG) SHARE_PER " +
                "from UW_H_HIST_COINS_INFO  " +
                "where HCI_PHS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
                "group by HCI_PHS_SEQ_NO,HCI_INS_CODE " +
                "union " +
                "select NCI_NDS_SEQ_NO,NCI_INS_CODE,sum(NCI_SHARE_PCNTG) SHARE_PER " +
                "from UW_H_EHIST_COINS_INFO  " +
                "where NCI_NDS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '" + dnNumber + "' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '" + dnNumber + "')   " +
                "group by NCI_NDS_SEQ_NO,NCI_INS_CODE)";
                DataTable dtTemp = new DataTable();
                dtTemp = crud.ExecQuery(cmd);
                if (dtTemp.Rows.Count > 0)
                {
                    dgvCoIn.DataSource = dtTemp;
                }
                
            }
           
        }
    }
}
