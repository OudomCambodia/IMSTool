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
    public partial class frmDeductibleRiskEndo : Form
    {

        string RiskName = "", PolNo = "";
        CRUD crud = new CRUD();

        public frmDeductibleRiskEndo(string RiskName, string PolNo)
        {
            this.RiskName = RiskName;
            this.PolNo = PolNo;
            InitializeComponent();
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDeductibleRiskEndo_Load(object sender, EventArgs e)
        {
            try
            {
                string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler" };
                string[] Values = new string[] { "RISK_ENDO", "", "", PolNo, RiskName, "" };
                DataTable result = crud.ExecSP_OutPara("SP_DEDUCTIBLE", Key, Values);
                if (result.Rows.Count <= 0)
                {
                    Msgbox.Show("Risk has no endorsement related.");
                    this.Close();
                }

                dgvEndoDetail.DataSource = result;
                dgvEndoDetail.Columns["PRS_SEQ_NO"].Visible = false;
                dgvEndoDetail.Columns["PRS_PLC_POL_SEQ_NO"].Visible = false;
                dgvEndoDetail.Columns["PRS_POLICY_NO"].Visible = false;
                dgvEndoDetail.Columns["PRS_NAME"].Visible = false;
                dgvEndoDetail.Columns["TRAN_PREMIUM"].DefaultCellStyle.Format = "c";

                dgvEndoDetail.RowsDefaultCellStyle.ForeColor = Color.Black;
                dgvEndoDetail.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

                tbPolicyNo.Text = PolNo;
                tbRiskName.Text = RiskName;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dgvEndoDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvEndoDetail);
        }
    }
}
