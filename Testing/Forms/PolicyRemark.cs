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
    public partial class PolicyRemark : Form
    {
        CRUD crud = new CRUD();
        string[] Keys = new string[] { "sp_type", "sp_policy" };
        DataTable dt;
        DataTable dtRiskRemark;

        public PolicyRemark()
        {
            InitializeComponent();
            dgvPolRem.SelectionChanged -= dgvPolRem_SelectionChanged;
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            if (tbPol.Text.Length != 20)
            {
                Msgbox.Show("Incorrect Policy No!");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string[] Values = new string[] { "Pol_remark", tbPol.Text.ToString() };
                dt = crud.ExecSP_OutPara("sp_user_print_pol", Keys, Values);
                dgvPolRem.DataSource = dt;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                    dgvPolRem.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                for (int i = 4; i < dgvPolRem.Columns.Count; i++)
                    dgvPolRem.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvPolRem.SelectionChanged += dgvPolRem_SelectionChanged;
                dgvPolRem_SelectionChanged(null, null);
            }
            else
            {
                dgvDetailPolRem.DataSource = null;
                dgvPolRem.SelectionChanged += dgvPolRem_SelectionChanged;
            }
                
            Cursor.Current = Cursors.AppStarting;
        }

        private void bnView_Click(object sender, EventArgs e)
        {
            if (dgvPolRem.SelectedCells.Count > 0)
                Msgbox.Show(dgvPolRem.SelectedCells[0].Value.ToString());
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            if (dtRiskRemark == null || dtRiskRemark.Rows.Count <= 0)
            {
                if (dt == null || dt.Rows.Count <= 0)
                {
                    Msgbox.Show("No Data found in the table!");
                    return;
                }
                My_DataTable_Extensions.ExportToExcelXML(dt);
            }
            else
            {
                var ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(dtRiskRemark);

                My_DataTable_Extensions.ExportToExcelXMLDataSet(ds);
            }
        }

        private void dgvPolRem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Msgbox.Show(dgvPolRem.SelectedCells[0].Value.ToString());
        }

        private void PolicyRemark_Activated(object sender, EventArgs e)
        {
            tbPol.Focus();
        }

        private void dgvPolRem_SelectionChanged(object sender, EventArgs e)
        {
            var current = dgvPolRem.CurrentRow;
            if (current != null) 
            {
                string policyNo = current.Cells["POLICY_NO"].Value.ToString();

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("select distinct PRS_NAME, PPR_REMARKS ")
                    .Append("from ( select pr.PRS_NAME, pp.PPR_REMARKS ")
                    .Append("from uw_t_pol_risks pr ")
                    .Append("inner join uw_t_pol_perils pp on pr.prs_seq_no = pp.ppr_prs_seq_no ")
                    .Append("where prs_policy_no = upper('" + policyNo + "') ")
                    .Append(") where PPR_REMARKS is not null ");

                dtRiskRemark = crud.ExecQuery(queryBuilder.ToString());
                dgvDetailPolRem.DataSource = dtRiskRemark;
            }
        }

        private void dgvDetailPolRem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Msgbox.Show(dgvDetailPolRem.SelectedCells[0].Value.ToString());
        }
    }
}
