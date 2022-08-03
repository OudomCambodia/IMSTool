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
    public partial class frmMedicalRejectionHistory : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtRejectionHistory = new DataTable();
        private string histClaimNo = string.Empty;

        public frmMedicalRejectionHistory()
        {
            InitializeComponent();
            txtClaimNo.CharacterCasing = CharacterCasing.Upper;
            txtClaimNo.MaxLength = 20;
        }

        private void frmMedicalRejectionHistory_Load(object sender, EventArgs e)
        {
            dtRejectionHistory = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A') CAUSE, " +
                "nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, nvl(nullif(instr(INT_COMMENTS, '('),0),instr(INT_COMMENTS, 'D:')) - instr(INT_COMMENTS, 'H:') - 2)), 'N/A') HOSPITAL, " +
                "nvl(REGEXP_SUBSTR(nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'H:') + 2, instr(INT_COMMENTS, 'D:') - instr(INT_COMMENTS, 'H:') - 2)), 'N/A'), '\\(([^)]*)\\)', 1, 1, NULL, 1),'N/A') TREATMENT_DATE, " +
                "INT_BPARTY_CODE CC, (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE = PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO IN (select CLAIM_NO from USER_MEDICAL_REJECTION_LETTER) order by CLAIM_NO");

            dgvClaimRejectionHist.DataSource = dtRejectionHistory;
            dgvClaimRejectionHist.DefaultCellStyle.ForeColor = Color.Black;
            dgvClaimRejectionHist.DefaultCellStyle.SelectionBackColor = Color.Black;
            dgvClaimRejectionHist.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void btnViewRefDoc_Click(object sender, EventArgs e)
        {
            frmMedicalRejectionLetter frmMedicalRejectionLetter = new frmMedicalRejectionLetter(true, histClaimNo);
            frmMedicalRejectionLetter.ShowDialog();
        }

        private void dgvClaimRejectionHist_SelectionChanged(object sender, EventArgs e)
        {
            var currentRow = dgvClaimRejectionHist.CurrentRow;
            if (currentRow != null)
                histClaimNo = currentRow.Cells["CLAIM_NO"].Value.ToString().Trim();
        }

        private void dgvClaimRejectionHist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnViewRefDoc_Click(null, null);
        }

        private void txtClaimNo_KeyUp(object sender, KeyEventArgs e)
        {
            DataView dvDtRejectClaimHist = new DataView(dtRejectionHistory);
            dvDtRejectClaimHist.RowFilter = " [CLAIM_NO] LIKE '%" + txtClaimNo.Text.Trim() + "%' ";
            dgvClaimRejectionHist.DataSource = dvDtRejectClaimHist;
        }
    }
}
