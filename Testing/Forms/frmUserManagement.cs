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
    public partial class frmUserManagement : Form
    {
        private CRUD crud = new CRUD();
        private DBS11SqlCrud sqlCrud = new DBS11SqlCrud();
        private string _hashPass = "Forte@2017";

        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            ActiveControl = txtUserCode;
            txtUserCode.CharacterCasing = CharacterCasing.Upper;
            txtUsername.CharacterCasing = CharacterCasing.Upper;
            txtRemark.CharacterCasing = CharacterCasing.Upper;

            string sql = "SELECT DISTINCT(TYPE) FROM USER_PRINT_SYSTEM ORDER BY TYPE";
            DataTable dtTypes = crud.ExecQuery(sql);

            if (dtTypes.Rows.Count > 0)
            {
                cboType.ValueMember = "TYPE";
                cboType.DisplayMember = "TYPE";
                cboType.DataSource = dtTypes;
            }
        }

        private void btnViewPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnViewPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnViewEmailPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmailPassword.UseSystemPasswordChar = false;
        }

        private void btnViewEmailPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtEmailPassword.UseSystemPasswordChar = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUserCode_Leave(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + txtUserCode.Text.Trim() + "' ";
                DataTable dtUsername = crud.ExecQuery(sql);

                if (dtUsername.Rows.Count > 0)
                {
                    txtUsername.Text = dtUsername.Rows[0]["USER_NAME"].ToString();
                    txtPassword.Text = Cipher.Decrypt(dtUsername.Rows[0]["PASSWORD"].ToString(), _hashPass);
                    dtpCreatedDate.Value = Convert.ToDateTime(dtUsername.Rows[0]["USER_CREATE_DATE"].ToString());
                    dtpExpiryDate.Value = Convert.ToDateTime(dtUsername.Rows[0]["EXPIRY_DATE"].ToString());
                    cboType.SelectedValue = dtUsername.Rows[0]["TYPE"].ToString();
                    txtRemark.Text = dtUsername.Rows[0]["REMARK"].ToString();
                    txtEmail.Text = dtUsername.Rows[0]["EMAIL"].ToString();
                    txtEmailPassword.Text = dtUsername.Rows[0]["EMAIL_PW"].ToString();
                }
                else
                {
                    string tmpSql = "SELECT SFC_INITIALS || ' ' || SFC_SURNAME USERNAME FROM SM_M_SALES_FORCE WHERE SFC_CODE = '" + txtUserCode.Text.Trim() + "' ";
                    DataTable dtTmpUsername = crud.ExecQuery(tmpSql);

                    if (dtTmpUsername.Rows.Count > 0)
                    {
                        txtUsername.Text = dtUsername.Rows[0]["USERNAME"].ToString();

                        var tmpLastTwoUsername = txtUsername.Text.Substring(txtUsername.Text.Trim().Length - 2).ToLower().Trim();
                        var lastTwoUsername = string.Concat(char.ToUpper(tmpLastTwoUsername[0]), tmpLastTwoUsername.Substring(1));
                        string[] dateCreate = dtpCreatedDate.Value.ToString("dd-MM-yyyy").Split('-');
                        var date = dateCreate[0];
                        var month = dateCreate[1];
                        var tempPassword = string.Concat("IMST@", lastTwoUsername, "#", date, month);
                        txtPassword.Text = tempPassword;
                        dtpExpiryDate.Value = new DateTime(DateTime.Now.Year, 12, 31);
                    }
                    else
                    {
                        txtUsername.Clear();
                        txtPassword.Clear();
                    }
                } 
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
            
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + txtUserCode.Text.Trim() + "' ";
                DataTable dtUsername = crud.ExecQuery(sql);

                if (dtUsername.Rows.Count > 0 || string.IsNullOrEmpty(txtUsername.Text.Trim()) || txtUsername.Text.Length < 2)
                    return;

                var tmpLastTwoUsername = txtUsername.Text.Substring(txtUsername.Text.Trim().Length - 2).ToLower().Trim();
                var lastTwoUsername = string.Concat(char.ToUpper(tmpLastTwoUsername[0]), tmpLastTwoUsername.Substring(1));
                string[] dateCreate = dtpCreatedDate.Value.ToString("dd-MM-yyyy").Split('-');
                var date = dateCreate[0];
                var month = dateCreate[1];
                var tempPassword = string.Concat("IMST@", lastTwoUsername, "#", date, month);
                txtPassword.Text = tempPassword;
                dtpExpiryDate.Value = new DateTime(DateTime.Now.Year, 12, 31);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetValidator(new TextBox[] { txtUserCode, txtUsername, txtPassword }))
                    return;

                string sqlUserPrintSystem = "SELECT USER_CODE FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + txtUserCode.Text.Trim() + "'";
                DataTable dtUserPrintSystemUserCode = crud.ExecQuery(sqlUserPrintSystem);

                string sqlDocUser = "SELECT USER_NAME FROM tbDOC_USER WHERE USER_NAME = '" + txtUserCode.Text.Trim() + "'";
                DataTable dtDocUser = sqlCrud.LoadData(sqlDocUser).Tables[0];


                if (dtUserPrintSystemUserCode.Rows.Count > 0 && dtDocUser.Rows.Count > 0)
                {
                    DialogResult confirmCreate = Msgbox.Show("Do you want to update this user?", "Confirmation");
                    if (confirmCreate == DialogResult.Yes)
                    {
                        UpdateUser(dtDocUser);
                        Msgbox.Show("User successfully updated!");
                        ClearControls();
                        return;
                    }
                    return;
                }

                if (dtUserPrintSystemUserCode.Rows.Count > 0 && dtDocUser.Rows.Count <= 0)
                {
                    DialogResult confirmCreate = Msgbox.Show(chkCreateDocUser.Checked ? "You have already created this user in USER_PRINT_SYSTEM. Do you want to create this user in tbDOC_USER?" :
                        "Do you want to create this user?", "Confirmation");
                    if (confirmCreate == DialogResult.Yes)
                    {
                        UpdateUser(dtDocUser);
                        CreateUser(true);
                        Msgbox.Show("User successfully created!");
                        ClearControls();
                        return;
                    }
                    return;
                }

                DialogResult confirmSave = Msgbox.Show("Do you want to create this user?", "Confirmation");
                if (confirmSave == DialogResult.Yes)
                {
                    CreateUser(false);
                    Msgbox.Show("User successfully created!");
                    ClearControls();
                } 
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            } 
        }

        private void CreateUser(bool createOnlyDocUser)
        {
            if (!createOnlyDocUser)
            {
                var encryptPassword = Cipher.Encrypt(txtPassword.Text.Trim(), _hashPass);
                var encryptEmailPassword = string.IsNullOrEmpty(txtEmailPassword.Text) ? null : Cipher.Encrypt(txtEmailPassword.Text.Trim(), _hashPass);
                var createdDate = dtpCreatedDate.Value.ToString("dd-MMM-yyyy");
                var expiryDate = dtpExpiryDate.Value.ToString("dd-MMM-yyyy");

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO USER_PRINT_SYSTEM (USER_CODE, USER_NAME, USER_CREATE_DATE, REMARK, EXPIRY_DATE, PASSWORD, TYPE, EMAIL, EMAIL_PW) ")
                    .Append("VALUES ('" + txtUserCode.Text.Trim() + "', '" + txtUsername.Text.Trim() + "', '" + createdDate + "', ")
                    .Append("'" + txtRemark.Text.Trim() + "', '" + expiryDate + "', '" + encryptPassword + "', '" + cboType.SelectedValue + "', ")
                    .Append("'" + txtEmail.Text.Trim() + "', '" + encryptEmailPassword + "')");

                crud.ExecNonQuery(queryBuilder.ToString());
            }
            
            if (chkCreateDocUser.Checked)
            {
                var maxUserCode = string.Empty;
                var username = txtUsername.Text.Trim();
                var role = "DP";
                var team = "NO";

                string sql = "SELECT MAX(USER_CODE) MAX_USER_CODE FROM tbDOC_USER where USER_CODE LIKE '%D%'";
                var dtMaxUserCode = sqlCrud.LoadData(sql).Tables[0];
                
                if (dtMaxUserCode.Rows.Count > 0)
                {
                    maxUserCode = dtMaxUserCode.Rows[0]["MAX_USER_CODE"].ToString();
                    var incraseMaxUserCode = Convert.ToInt32(maxUserCode.ToString().Substring(1)) + 1;
                    maxUserCode = string.Concat("D", incraseMaxUserCode < 10 ? "0" : "",  incraseMaxUserCode.ToString());
                }
                maxUserCode = maxUserCode == string.Empty ? "D01" : maxUserCode;

                string[] splitUsernames = username.Split(' ');
                string fullName = string.Empty;

                foreach (var splitUsername in splitUsernames)
                {
                    var concatName = string.Concat(splitUsername.Substring(0, 1), splitUsername.Substring(1).ToLower(), " ");
                    fullName += concatName;
                }
                
                StringBuilder queryBuilderSql = new StringBuilder();
                queryBuilderSql.Append("INSERT INTO tbDOC_USER (USER_CODE, USER_NAME, ROLE, TEAM, FULL_NAME) ")
                    .Append("VALUES ('" + maxUserCode + "', '" + txtUserCode.Text.Trim() + "', '" + role + "', '" + team + "', '" + fullName.Trim() + "')");

                sqlCrud.Executing(queryBuilderSql.ToString());
            }
        }

        private void UpdateUser(DataTable dtDocUser)
        {
            var encryptPassword = Cipher.Encrypt(txtPassword.Text.Trim(), _hashPass);
            var encryptEmailPassword = string.IsNullOrEmpty(txtEmailPassword.Text) ? null : Cipher.Encrypt(txtEmailPassword.Text.Trim(), _hashPass);
            var createdDate = dtpCreatedDate.Value.ToString("dd-MMM-yyyy");
            var expiryDate = dtpExpiryDate.Value.ToString("dd-MMM-yyyy");

            StringBuilder updateBuilder = new StringBuilder();
            updateBuilder.Append("UPDATE USER_PRINT_SYSTEM SET USER_NAME = '" + txtUsername.Text + "', PASSWORD = '" + encryptPassword + "', ")
                .Append("EXPIRY_DATE = '" + expiryDate + "', TYPE = '" + cboType.SelectedValue + "', ")
                .Append("REMARK = '" + txtRemark.Text.Trim() + "', EMAIL = '" + txtEmail.Text.Trim() + "', EMAIL_PW = '" + encryptEmailPassword + "' ")
                .Append("WHERE USER_CODE = '" + txtUserCode.Text.Trim() + "' ");

            crud.ExecNonQuery(updateBuilder.ToString());

            if (dtDocUser.Rows.Count > 0)
            {
                string[] splitUsernames = txtUsername.Text.Trim().Split(' ');
                string fullName = string.Empty;

                foreach (var splitUsername in splitUsernames)
                {
                    var concatName = string.Concat(splitUsername.Substring(0, 1), splitUsername.Substring(1).ToLower(), " ");
                    fullName += concatName;
                }
                StringBuilder updateSqlBuilder = new StringBuilder();
                updateSqlBuilder.Append("UPDATE tbDOC_USER SET FULL_NAME = '" + fullName.Trim() + "' ")
                    .Append("WHERE USER_NAME = '" +  txtUserCode.Text.Trim() + "'");

                sqlCrud.Executing(updateSqlBuilder.ToString());
            }
        }
        
        private bool SetValidator(TextBox[] txtBoxes)
        {
            var isEmpty = false;
            var setFocus = false;

            foreach (var txtBox in txtBoxes)
            {
                if (string.IsNullOrEmpty(txtBox.Text))
                {
                    errorProvider.SetError(txtBox, "This field cannot be empty");
                    if (!setFocus)
                    {
                        ActiveControl = txtBox;
                        setFocus = true;
                    }
                    isEmpty = true;
                }
                else
                    errorProvider.Clear();
            }
            return isEmpty;
        }

        private void ClearControls()
        {
            txtUserCode.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtRemark.Clear();
            txtEmail.Clear();
            txtEmailPassword.Clear();
            ActiveControl = txtUserCode; 
        }
    }
}
