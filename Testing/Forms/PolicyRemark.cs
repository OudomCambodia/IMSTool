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

        public PolicyRemark()
        {
            InitializeComponent();
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
            if (dt == null || dt.Rows.Count <= 0)
            {
                Msgbox.Show("No Data found in the table!");
                return;
            }

            My_DataTable_Extensions.ExportToExcel(dt, "");
        }

        private void dgvPolRem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Msgbox.Show(dgvPolRem.SelectedCells[0].Value.ToString());
        }

        private void PolicyRemark_Activated(object sender, EventArgs e)
        {
            tbPol.Focus();
        }
    }
}
