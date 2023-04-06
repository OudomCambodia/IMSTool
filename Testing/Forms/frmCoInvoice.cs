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
                        crystalReportViewer1.ReportSource = coinv;
                    }
                    else
                    {
                        Reports.CoFilingInvoice coinv = new Reports.CoFilingInvoice();
                        coinv.SetDataSource(dtCoinvoice);
                        crystalReportViewer1.ReportSource = coinv;
                    }
                    
                }
                
                
                
            }catch(Exception ex){
                Msgbox.Show(ex.Message);
            }
        }
    }
}
