using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresentationControls;

namespace Testing.Forms
{
    public partial class frmUserManagement : Form
    {
        private CRUD crud = new CRUD();
        private DBS11SqlCrud sqlCrud = new DBS11SqlCrud();
        private string _hashPass = "Forte@2017";

        private readonly string HEAD_FILING = "HEAD_FILING";
        private readonly string ALL_REGIONALS = "ALL_REGIONALS";
        private readonly string ALL_BANKS = "ALL_BANKS";
        private readonly string ALL_BROKERS = "ALL_BROKERS";

        public frmUserManagement()
        {
            InitializeComponent();

            cboSpecialCode.Items.Add(new ComboboxItem("N/A", "N/A"));
            cboSpecialCode.Items.Add(new ComboboxItem(HEAD_FILING, HEAD_FILING));
            cboSpecialCode.Items.Add(new ComboboxItem(ALL_REGIONALS, ALL_REGIONALS));
            cboSpecialCode.Items.Add(new ComboboxItem(ALL_BANKS, ALL_BANKS));
            cboSpecialCode.Items.Add(new ComboboxItem(ALL_BROKERS, ALL_BROKERS));
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            ActiveControl = txtUserCode;
            txtUserCode.CharacterCasing = CharacterCasing.Upper;
            txtUsername.CharacterCasing = CharacterCasing.Upper;
            txtRemark.CharacterCasing = CharacterCasing.Upper;

            string sql = "SELECT DISTINCT(CODE) FROM USER_PRINT_CONTROL_ACCESS ORDER BY CODE";
            DataTable dtTypes = crud.ExecQuery(sql);

            if (dtTypes.Rows.Count > 0)
            {
                cboType.ValueMember = "CODE";
                cboType.DisplayMember = "CODE";
                cboType.DataSource = dtTypes;
            }

            cboSpecialCode.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUserCode_Leave(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + txtUserCode.Text.Trim() + "'";
                DataTable dtUsername = crud.ExecQuery(sql);

                if (dtUsername.Rows.Count > 0)
                {
                    txtUsername.Text = dtUsername.Rows[0]["USER_NAME"].ToString();
                    txtPassword.Text = !string.IsNullOrEmpty(dtUsername.Rows[0]["PASSWORD"].ToString()) ? Cipher.Decrypt(dtUsername.Rows[0]["PASSWORD"].ToString(), _hashPass) : string.Empty;
                    dtpCreatedDate.Value = Convert.ToDateTime(dtUsername.Rows[0]["USER_CREATE_DATE"].ToString());
                    dtpExpiryDate.Value = Convert.ToDateTime(dtUsername.Rows[0]["EXPIRY_DATE"].ToString());
                    cboType.SelectedValue = dtUsername.Rows[0]["TYPE"].ToString();
                    txtRemark.Text = dtUsername.Rows[0]["REMARK"].ToString();
                    txtEmail.Text = dtUsername.Rows[0]["EMAIL"].ToString();
                    txtEmailPassword.Text = !string.IsNullOrEmpty(dtUsername.Rows[0]["EMAIL_PW"].ToString()) ? Cipher.Decrypt(dtUsername.Rows[0]["EMAIL_PW"].ToString(), _hashPass) : string.Empty;
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
                        cboType.SelectedIndex = 0;
                        txtRemark.Clear();
                        txtEmail.Clear();
                        txtEmailPassword.Clear();
                    }
                }

                ReloadData();

                var dsSpecialCode = sqlCrud.LoadData("select * from tbDOC_SPECIAL_CODE where USER_ID = (select USER_CODE from tbDOC_USER where USER_NAME = '" + txtUserCode.Text.Trim() + "')").Tables[0];
                var specialCode = string.Empty;
                if (dsSpecialCode.Rows.Count > 0)
                {
                    specialCode = dsSpecialCode.Rows[0]["SPECIAL_CODE"].ToString().Trim();
                    cboSpecialCode.SelectedIndex = cboSpecialCode.FindString(specialCode);
                }
                else
                    cboSpecialCode.SelectedIndex = 0;
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

        private void txtUserCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                return;

            txtPassword.UseSystemPasswordChar = txtPassword.UseSystemPasswordChar ? false : true;
        }

        private void btnViewEmailPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailPassword.Text))
                return;

            txtEmailPassword.UseSystemPasswordChar = txtEmailPassword.UseSystemPasswordChar ? false : true;
        }

        private void chkCreateDocUser_CheckedChanged(object sender, EventArgs e)
        {
            gbDocumentControl.Enabled = chkCreateDocUser.Checked;
        }

        private void cboSpecialCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSpecialCode.SelectedIndex == 0)
                lblSpecialCodeInfo.Text = "N/A";
            else
            {
                var specialRole = cboSpecialCode.Text.Trim();
                var dsUserInSpecialRole = sqlCrud.LoadData("select FULL_NAME from tbDOC_USER where USER_CODE IN (select USER_ID from tbDOC_SPECIAL_CODE where SPECIAL_CODE = '" + specialRole + "')").Tables[0];
                if (dsUserInSpecialRole.Rows.Count > 0)
                {
                    var note = "This Special Code is used under user: ";
                    for (int i = 0; i < dsUserInSpecialRole.Rows.Count; i++)
                    {
                        note += dsUserInSpecialRole.Rows[i]["FULL_NAME"].ToString() + ", ";
                    }
                    note = note.Remove(note.Length - 2) + ".";
                    lblSpecialCodeInfo.Text = note;
                }
                else
                    lblSpecialCodeInfo.Text = "This Special Code isn't used by any user.";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetValidator(new Control[] { txtUserCode, txtUsername, txtPassword, cboRole, cboTeam }))
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
                        CreateOrUpdateUserSpecialCode();
                        Msgbox.Show("User successfully updated!");
                        ClearControls();
                        ReloadData();
                        return;
                    }
                    return;
                }

                if (dtUserPrintSystemUserCode.Rows.Count > 0 && dtDocUser.Rows.Count <= 0)
                {
                    DialogResult confirmCreate = Msgbox.Show(chkCreateDocUser.Checked ? "You have already created this user in USER_PRINT_SYSTEM. Do you want to create this user in tbDOC_USER?" :
                        "Do you want to update this user?", "Confirmation");
                    if (confirmCreate == DialogResult.Yes)
                    {
                        UpdateUser(dtDocUser);
                        CreateUser(true);
                        CreateOrUpdateUserSpecialCode();
                        Msgbox.Show(chkCreateDocUser.Checked ? "User successfully created!" : "User successfully updated!");
                        ClearControls();
                        ReloadData();
                        return;
                    }
                    return;
                }

                DialogResult confirmSave = Msgbox.Show("Do you want to create this user?", "Confirmation");
                if (confirmSave == DialogResult.Yes)
                {
                    CreateUser(false);
                    CreateOrUpdateUserSpecialCode();
                    Msgbox.Show("User successfully created!");
                    ClearControls();
                    ReloadData();
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
                var role = cboRole.Text.Replace(" ", string.Empty);
                var team = cboTeam.Text.Replace(" ", string.Empty);

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
                queryBuilderSql.Append("INSERT INTO tbDOC_USER (USER_CODE, USER_NAME, ROLE, TEAM, FULL_NAME, SELECTION_COLOR) ")
                    .Append("VALUES ('" + maxUserCode + "', '" + txtUserCode.Text.Trim() + "', '" + role + "', '" + team + "', '" + fullName.Trim() + "', '0,153,153')");

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
                var role = cboRole.Text.Replace(" ", string.Empty);
                var team = cboTeam.Text.Replace(" ", string.Empty);

                StringBuilder updateSqlBuilder = new StringBuilder();
                updateSqlBuilder.Append("UPDATE tbDOC_USER SET FULL_NAME = '" + fullName.Trim() + "', ROLE = '" + role + "', TEAM = '" + team + "' ")
                    .Append("WHERE USER_NAME = '" +  txtUserCode.Text.Trim() + "'");

                sqlCrud.Executing(updateSqlBuilder.ToString());
            }
        }

        private void CreateOrUpdateUserSpecialCode()
        {
            if (cboSpecialCode.SelectedIndex == 0)
            {
                var dsUserID = sqlCrud.LoadData("select * from tbDOC_SPECIAL_CODE where USER_ID = (select USER_CODE from tbDOC_USER where USER_NAME = '" + txtUserCode.Text.Trim() + "')").Tables[0];
                var UserID = string.Empty;
                if (dsUserID.Rows.Count > 0)
                    UserID = dsUserID.Rows[0]["USER_ID"].ToString().Trim();

                if (string.IsNullOrEmpty(UserID))
                    return;

               sqlCrud.Executing("delete from tbDOC_SPECIAL_CODE where USER_ID = '" + UserID + "'");
            }
            else
            {
                var dsUserID = sqlCrud.LoadData("select * from tbDOC_SPECIAL_CODE where USER_ID = (select USER_CODE from tbDOC_USER where USER_NAME = '" + txtUserCode.Text.Trim() + "')").Tables[0];
                var UserID = string.Empty;
                if (dsUserID.Rows.Count <= 0)
                {
                    if (!chkCreateDocUser.Checked)
                        return;

                    var dsNewUserId = sqlCrud.LoadData("select USER_CODE from tbDOC_USER where USER_NAME = '" + txtUserCode.Text.Trim() + "'").Tables[0];
                    if (dsNewUserId.Rows.Count <= 0)
                        return;

                    UserID = dsNewUserId.Rows[0]["USER_CODE"].ToString();
                    sqlCrud.Executing("insert into tbDOC_SPECIAL_CODE(USER_ID, SPECIAL_CODE) values('" + UserID + "', '" + cboSpecialCode.Text.Trim() + "')");
                }
                else
                {
                    UserID = dsUserID.Rows[0]["USER_ID"].ToString().Trim();
                    sqlCrud.Executing("update tbDOC_SPECIAL_CODE set SPECIAL_CODE = '" + cboSpecialCode.Text.Trim() + "' where USER_ID = '" + UserID + "'");
                }     
            }
        }

        private bool SetValidator(Control[] controls)
        {
            var isEmpty = false;
            var setFocus = false;

            foreach (var control in controls)
            {
                if ((control is TextBox && string.IsNullOrEmpty(control.Text)) || (control is ComboBox && string.IsNullOrEmpty(control.Text)) 
                    || (control is CheckBoxComboBox && string.IsNullOrEmpty(control.Text)))
                {
                    errorProvider.SetError(control, "This field cannot be empty");
                    if (!setFocus)
                    {
                        ActiveControl = control;
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
            cboType.SelectedIndex = 0;
            cboSpecialCode.SelectedIndex = 0;
            ActiveControl = txtUserCode; 
        }

        private void LoadData(CheckBoxComboBox cboBox, string item, string userCode)
        {
            var lstData = new List<string>();
            var dtData = sqlCrud.LoadData(string.Format("select {0} from tbDOC_USER order by {0}", item)).Tables[0];
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                var data = dtData.Rows[i][string.Format("{0}", item)].ToString();
                if (data.Contains(","))
                {
                    var datas = data.Split(',');
                    for (int j = 0; j < datas.Length; j++)
                    {
                        lstData.Add(datas[j].Trim().ToString());
                    }
                }
                else
                    lstData.Add(data);
            }
            var distinctData = lstData.Distinct().ToList();
            distinctData.Sort();

            for (int k = 0; k < distinctData.Count; k++)
            {
                cboBox.Items.Add(distinctData[k].ToString());
            }

            var userData = sqlCrud.LoadData(string.Format("select {0} from tbDOC_USER where USER_NAME = '{1}'", item, userCode)).Tables[0];

            if (userData.Rows.Count <= 0)
            {
                if (item.Equals("ROLE"))
                {
                    var roleData = cboBox.CheckBoxItems["DP"];
                    var roleIndex = cboBox.CheckBoxItems.IndexOf(roleData);
                    cboBox.CheckBoxItems[roleIndex].Checked = true;
                }
                else
                {
                    var teamData = cboBox.CheckBoxItems["NO"];
                    var teamIndex = cboBox.CheckBoxItems.IndexOf(teamData);
                    cboBox.CheckBoxItems[teamIndex].Checked = true;
                }
                return;
            }

            var userDatas = userData.Rows[0][0].ToString().Split(',');
            for (int n = 0; n < userDatas.Length; n++)
            {
                var data = cboBox.CheckBoxItems[userDatas[n].Trim().ToString()];
                var index = cboBox.CheckBoxItems.IndexOf(data);
                cboBox.CheckBoxItems[index].Checked = true;
            }
        }

        private void ReloadData()
        {
            cboRole.Items.Clear();
            cboTeam.Items.Clear();

            LoadData(cboRole, "ROLE", txtUserCode.Text.Trim());
            LoadData(cboTeam, "TEAM", txtUserCode.Text.Trim());
        }
    }
}
