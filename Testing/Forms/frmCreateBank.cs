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
    public partial class frmCreateBank : Form
    {
        CRUD crud = new CRUD();
        public Forms.frmPrintInvoice frmPrintInvoice;

        public frmCreateBank()
        {
            InitializeComponent();
        }

        private void dgvBank_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvBank);
        }

        private void frmCreateBank_Load(object sender, EventArgs e)
        {
            dgvBank.DataSource = crud.ExecQuery("SELECT * FROM USER_BANK_INFO");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == "" || tbTransfer.Text.Trim() == "" || tbAccount.Text.Trim() == "")
            {
                Msgbox.Show("Please fill in all the information before saving the data.");
                return;
            }



            try
            {
                if (tbID.Text.Trim() == "")
                {
                    crud.ExecNonQuery("INSERT INTO USER_BANK_INFO (BANK_NAME, TRANFER_TO, ACCOUNT_NO, DEFAULT_BANK) VALUES(q'[" + tbName.Text.Trim() + "]', q'[" + tbTransfer.Text.Trim() + "]', q'[" + tbAccount.Text.Trim() + "]', 'NO')");
                    Msgbox.Show("New Record is inserted successfully.");
                    ReloadGrid();
                }
                else
                {
                    crud.ExecNonQuery("UPDATE USER_BANK_INFO SET BANK_NAME = q'[" + tbName.Text.Trim() + "]', TRANFER_TO = q'[" + tbTransfer.Text.Trim() + "]', ACCOUNT_NO = q'[" + tbAccount.Text.Trim() + "]' WHERE BANK_ID = " + tbID.Text.Trim());
                    Msgbox.Show("This Record is updated successfully.");
                    ReloadGrid();
                }

                
                frmPrintInvoice.BindComboBoxBank();
                frmPrintInvoice.comboBank.Refresh();
            }
            catch (Exception ex)
            {
                Msgbox.Show("There is an error from the database. Please contact system admin. \n" + ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            tbID.Text = "";
            tbName.Text = "";
            tbTransfer.Text = "";
            tbAccount.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnDefault_Click(object sender, EventArgs e)
        {
            DialogResult dr = Msgbox.Show("Do you want to make this bank Default for the system?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                crud.ExecNonQuery("update USER_BANK_INFO set DEFAULT_BANK = 'NO' where DEFAULT_BANK = 'YES'");
                crud.ExecNonQuery("update USER_BANK_INFO set DEFAULT_BANK = 'YES' where BANK_ID = " + tbID.Text.ToString());
                ReloadGrid();
            }
        }

        private void tbID_TextChanged(object sender, EventArgs e)
        {
            if (tbID.Text.Trim().ToString() == "")
            {
                bnDefault.BackColor = Color.Gray;
                bnDefault.Enabled = false;
            }
            else
            {
                bnDefault.BackColor = Color.FromArgb(0, 9, 47);
                bnDefault.Enabled = true;
            }
        }

        private void dgvBank_MouseClick(object sender, MouseEventArgs e)
        {
            tbID.Text = dgvBank.SelectedRows[0].Cells[0].Value.ToString();
            tbName.Text = dgvBank.SelectedRows[0].Cells[1].Value.ToString();
            tbTransfer.Text = dgvBank.SelectedRows[0].Cells[2].Value.ToString();
            tbAccount.Text = dgvBank.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void ReloadGrid()
        {
            dgvBank.DataSource = crud.ExecQuery("SELECT * FROM USER_BANK_INFO");
            tbID.Text = "";
            tbName.Text = "";
            tbTransfer.Text = "";
            tbAccount.Text = "";
        }
    }
}
