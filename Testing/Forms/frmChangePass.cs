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
    public partial class frmChangePass : Form
    {
        CRUD crud = new CRUD();
        string HashPass = "Forte@2017";

        public frmChangePass()
        {
            InitializeComponent();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnOK_Click(object sender, EventArgs e)
        {
            if (tbOld.Text.Trim() == "" || tbNew.Text.Trim() == "" || tbConfirm.Text.Trim() == "")
            {
                Msgbox.Show("Please fill in all the necessary information.");
                return;
            }

            try
            {
                string sql = "SELECT password FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + tbUser.Text.Trim().ToUpper() + "'";

                DataTable dt = new DataTable();
                dt = crud.ExecQuery(sql);

                DataRow dr = dt.Rows[0];
                string password = dr[0].ToString();

                if (password != Cipher.Encrypt(tbOld.Text, HashPass))
                {
                    Msgbox.Show("Your Current Password is not correct! Please check your password again.");
                    return;
                }

                if (!(tbNew.Text.Any(char.IsUpper) && tbNew.Text.Any(char.IsLower) && tbNew.Text.Any(char.IsDigit) && tbNew.Text.Length >= 8))
                {
                    Msgbox.Show("Your New Password doesn't follow the password rules! Password must be at least 8 characters and contain Uppercase, Lowercase and Number.");
                    return;
                }

                if (tbNew.Text != tbConfirm.Text)
                {
                    Msgbox.Show("Your New Password doesn't match with the Confirm Password! Please check them again.");
                    return;
                }

                sql = "update USER_PRINT_SYSTEM set PASSWORD = '" + Cipher.Encrypt(tbNew.Text, HashPass) + "' where USER_CODE = '" + tbUser.Text + "'";
                crud.ExecNonQuery(sql);

                Msgbox.Show("Your password is successfully updated.");

                this.Close();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            if (tbUser.Text == "SICL")
            {
                Msgbox.Show("Incorrect Username! Please contact system admin.");
                this.Close();
            }
        }
    }
}
