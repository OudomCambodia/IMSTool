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
    public partial class frmUserRoleManagement : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtCode;
        private DataTable dtControlToUpdate;
        public static string GroupCode;
        public static string ControlId;
        public static string ControlName;
        public static string ControlDesc;
        public static string SubMenuOf;
        public static bool IsAddNew;
        private string groupCode = string.Empty;
        private string keyToUpdateOrDelete;

        public frmUserRoleManagement()
        {
            InitializeComponent();
            txtSearchRole.CharacterCasing = CharacterCasing.Upper;
            txtGroupCode.CharacterCasing = CharacterCasing.Upper;
            MaximizeBox = false;
        }

        private void frmUserRoleManagement_Load(object sender, EventArgs e)
        {
            LoadLstCode();
            LoadAllAccessControl();
            lstCode.SelectedIndex = 0;
        }

        private void txtSearchRole_Enter(object sender, EventArgs e)
        {
            if (txtSearchRole.Text == "--- SEARCH ROLE ---")
                txtSearchRole.Clear();
            txtSearchRole.ForeColor = Color.Black;
        }

        private void txtSearchRole_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchRole.Text))
            {
                txtSearchRole.Text = "--- SEARCH ROLE ---";
                txtSearchRole.ForeColor = Color.DarkGray;
            }  
        }

        private void txtSearchRole_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView filterRole = new DataView(dtCode);
                filterRole.RowFilter = " [CODE] LIKE '%" + txtSearchRole.Text.Trim() + "%' ";
                var dtFilterRole = filterRole.ToTable();

                if (dtFilterRole.Rows.Count > 0 && dtFilterRole != null)
                    lstCode.Items.Clear();

                for (int i = 0; i < dtFilterRole.Rows.Count; i++)
                {
                    lstCode.Items.Add(dtFilterRole.Rows[i]["CODE"]).ToString();
                }
                lstCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GroupCode = lstCode.SelectedItems[0].ToString();
                txtGroupCode.Text = GroupCode;

                gbEnabled.Enabled = !txtGroupCode.Text.Equals("DEFAULT");

                groupCode = lstCode.SelectedItems[0].ToString();
                LoadControlAccessByGroupCode();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            } 
        }

        private void lstCode_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupCode))
            {
                frmRoleDetail frmRoleDetail = new frmRoleDetail();
                frmRoleDetail.ShowDialog();
            }
        }

        private void txtGroupCode_TextChanged(object sender, EventArgs e)
        {
            var groupCode = crud.ExecQuery("select CODE from USER_PRINT_CONTROL_ACCESS where CODE = '" + GroupCode + "'").Rows[0]["CODE"];
            txtGroupCode.Enabled = !txtGroupCode.Text.Trim().Equals("DEFAULT");
            btnUpdateCode.Enabled = !txtGroupCode.Text.Trim().Equals("DEFAULT") && !string.IsNullOrEmpty(txtGroupCode.Text.Trim()) && !txtGroupCode.Text.Trim().Equals(groupCode);
        }

        private void btnUpdateCode_Click(object sender, EventArgs e)
        {
            try
            {
                var isExistGroupCode = crud.ExecQuery("select CODE from USER_PRINT_CONTROL_ACCESS where CODE = '" + txtGroupCode.Text.Trim() + "'").Rows.Count > 0;
                if (isExistGroupCode)
                {
                    Msgbox.Show("Group Code already existed!");
                    return;
                }

                DialogResult confirmDelete = Msgbox.Show("Are you sure you want to update this Group Code?", "Update Group Code");
                if (confirmDelete == DialogResult.Yes)
                {
                    StringBuilder updateCodeBuilder = new StringBuilder();
                    updateCodeBuilder.Append("update USER_PRINT_CONTROL_ACCESS set CODE = '" + txtGroupCode.Text.Trim() + "' WHERE CODE = '" + GroupCode + "'");
                    crud.ExecNonQuery(updateCodeBuilder.ToString());
                    Msgbox.Show("Group Code successfully updated!");
                    LoadLstCode();
                    txtSearchRole_TextChanged(null, null);
                    lstCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void tvUserRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var key = e.Node.Name;
                keyToUpdateOrDelete = key;

                btnUpdateControl.Enabled = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = '" + key + "'").Rows.Count <= 0;

                dtControlToUpdate = crud.ExecQuery("select * from USER_PRINT_CONTROL WHERE CONTROL_ID = '" + key + "' ");

                var enabled = crud.ExecQuery("select VISIBLE, ENABLED from USER_PRINT_CONTROL_ACCESS WHERE CODE = '" + groupCode + "' AND CONTROL_ID = '" + key + "' ");
                var isEnabled = enabled.Rows[0]["VISIBLE"].ToString() == "Y" && enabled.Rows[0]["ENABLED"].ToString() == "Y";

                if (isEnabled)
                    rbEnabledTrue.Checked = true;
                else
                    rbEnabledFalse.Checked = true;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void tvUserRole_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnUpdateControl_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtControlToUpdate.Rows.Count > 0)
                {
                    IsAddNew = false;
                    ControlId = dtControlToUpdate.Rows[0]["CONTROL_ID"].ToString();
                    ControlName = dtControlToUpdate.Rows[0]["CONTROL_NAME"].ToString();
                    ControlDesc = dtControlToUpdate.Rows[0]["CONTROL_DESC"].ToString();
                    SubMenuOf = dtControlToUpdate.Rows[0]["SUBMENU_OF"].ToString();

                    frmAddNewControl frmAddNewControl = new frmAddNewControl();
                    if (frmAddNewControl.ShowDialog() == DialogResult.OK)
                    {
                        LoadAllAccessControl();
                        lstCode.SelectedIndex = 0;
                        lstCode_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void btnAddNewControl_Click(object sender, EventArgs e)
        {
            try
            {
                IsAddNew = true;
                frmAddNewControl frmAddNewControl = new frmAddNewControl();
                if (frmAddNewControl.ShowDialog() == DialogResult.OK)
                {
                    LoadAllAccessControl();
                    lstCode.SelectedIndex = 0;
                    lstCode_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void rbEnabledTrue_Click(object sender, EventArgs e)
        {
            EnableDisableControl("Y", false);
        }

        private void rbEnabledFalse_Click(object sender, EventArgs e)
        {
            EnableDisableControl("N", true);
        }

        private void copyRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GroupCode))
                {
                    frmCopyRole frmCopyRole = new frmCopyRole();
                    if (frmCopyRole.ShowDialog() == DialogResult.OK)
                        LoadLstCode();
                    txtSearchRole_TextChanged(null, null);
                    lstCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void deleteRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var roleInUsed = crud.ExecQuery("select ups.type from USER_PRINT_SYSTEM ups inner join USER_PRINT_TYPE upt on upt.TYPE = ups.TYPE where ups.type = '" + GroupCode + "' group by ups.TYPE ").Rows.Count > 0;
                DialogResult confirmDelete = Msgbox.Show(roleInUsed ? "'" + GroupCode + "' is already in used. Do you still want to delete?" : "Are you sure you want to delete '" + GroupCode + "'?", "Delete Role");
                if (confirmDelete == DialogResult.Yes)
                {
                    if (roleInUsed)
                    {
                        DialogResult confirmDelete1 = Msgbox.Show("Jbas hahhhhhhh?", "Code ng ke k'pg tah use hahhhhhh!!");
                        if (confirmDelete1 == DialogResult.Yes)
                            DeleteRole();
                    }
                    else
                        DeleteRole();
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void ctmsCopyRole_Opening(object sender, CancelEventArgs e)
        {
            deleteRoleToolStripMenuItem.Enabled = !txtGroupCode.Text.Equals("DEFAULT");
        }

        private void deleteControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var isMainNode = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = '" + keyToUpdateOrDelete + "'").Rows.Count > 0;

                DialogResult confirmDelete = Msgbox.Show(isMainNode ? "Delete this Control will delete all Child Control inside. Are you sure you want to delete?"
                    : "Are you sure you want to delete this control?", "Delete Control");
                if (confirmDelete == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    if (isMainNode)
                    {
                        var subMenuKeys = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = '" + keyToUpdateOrDelete + "'");
                        if (subMenuKeys.Rows.Count > 0)
                        {
                            crud.ExecQuery("delete from USER_PRINT_CONTROL where CONTROL_ID = '" + keyToUpdateOrDelete + "'");
                            crud.ExecQuery("delete from USER_PRINT_CONTROL_ACCESS where CONTROL_ID = '" + keyToUpdateOrDelete + "'");

                            for (int i = 0; i < subMenuKeys.Rows.Count; i++)
                            {
                                var subMenuKey = subMenuKeys.Rows[i]["CONTROL_ID"].ToString();
                                crud.ExecQuery("delete from USER_PRINT_CONTROL where CONTROL_ID = '" + subMenuKey + "'");
                                crud.ExecQuery("delete from USER_PRINT_CONTROL_ACCESS where CONTROL_ID = '" + subMenuKey + "'");
                            }
                        }
                    }
                    else
                    {
                        crud.ExecQuery("delete from USER_PRINT_CONTROL where CONTROL_ID = '" + keyToUpdateOrDelete + "'");
                        crud.ExecQuery("delete from USER_PRINT_CONTROL_ACCESS where CONTROL_ID = '" + keyToUpdateOrDelete + "'");
                    }
                    Msgbox.Show("Control successfully deleted!");
                    LoadAllAccessControl();
                    lstCode.SelectedIndex = 0;
                    lstCode_SelectedIndexChanged(null, null);

                    Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void DeleteRole()
        {
            StringBuilder deleteBuilder = new StringBuilder();
            deleteBuilder.Append("delete from USER_PRINT_CONTROL_ACCESS where code = '" + GroupCode + "'");
            crud.ExecNonQuery(deleteBuilder.ToString());
            Msgbox.Show("Role successfully deleted!");
            LoadLstCode();
            txtSearchRole_TextChanged(null, null);
            lstCode.SelectedIndex = 0;
        }
        private void EnableDisableControl(string enableControl, bool isDisable)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var enable = enableControl;
                var enableControlCount = 0;
                var groupCode = lstCode.SelectedItem;
                var keyToUpdate = keyToUpdateOrDelete;
                var isMainNode = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = '" + keyToUpdate + "'").Rows.Count > 0;

                if (isMainNode)
                {
                    crud.ExecNonQuery("update USER_PRINT_CONTROL_ACCESS set VISIBLE = '" + enable + "', ENABLED = '" + enable + "' where CODE = '" + groupCode + "' and CONTROL_ID = '" + keyToUpdate + "'");
                    var dtSubMenuKeysToUpdate = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = '" + keyToUpdate + "'");
                    for (int i = 0; i < dtSubMenuKeysToUpdate.Rows.Count; i++)
                    {
                        var subMenuKeyToUpdate = dtSubMenuKeysToUpdate.Rows[i]["CONTROL_ID"].ToString();
                        crud.ExecNonQuery("update USER_PRINT_CONTROL_ACCESS set VISIBLE = '" + enable + "', ENABLED = '" + enable + "' where CODE = '" + groupCode + "' and CONTROL_ID = '" + subMenuKeyToUpdate + "'");
                    }
                }
                else
                {
                    crud.ExecNonQuery("update USER_PRINT_CONTROL_ACCESS set VISIBLE = '" + enable + "', ENABLED = '" + enable + "' where CODE = '" + groupCode + "' and CONTROL_ID = '" + keyToUpdate + "'");

                    var enabledChildNodes = crud.ExecQuery("select CONTROL_ID, VISIBLE, ENABLED from USER_PRINT_CONTROL_ACCESS where CONTROL_ID IN((select CONTROL_ID from USER_PRINT_CONTROL where SUBMENU_OF = (select SUBMENU_OF from USER_PRINT_CONTROL where CONTROL_ID = '" + keyToUpdate + "'))) AND CODE = '" + groupCode + "'");
                    if (enabledChildNodes.Rows.Count > 0 && !string.IsNullOrEmpty(enabledChildNodes.Rows[0]["CONTROL_ID"].ToString()))
                    {
                        var mainNodeKey = crud.ExecQuery("select SUBMENU_OF from USER_PRINT_CONTROL where CONTROL_ID = '" + enabledChildNodes.Rows[0]["CONTROL_ID"].ToString() + "'").Rows[0]["SUBMENU_OF"].ToString();
                        for (int i = 0; i < enabledChildNodes.Rows.Count; i++)
                        {
                            if (enabledChildNodes.Rows[i]["VISIBLE"].ToString().Equals(enable) || enabledChildNodes.Rows[i]["ENABLED"].ToString().Equals(enable))
                                enableControlCount = enableControlCount + 1;
                        }

                        if (isDisable)
                        {
                            if (enableControlCount == enabledChildNodes.Rows.Count)
                                crud.ExecNonQuery("update USER_PRINT_CONTROL_ACCESS set VISIBLE = '" + enable + "', ENABLED = '" + enable + "' where CODE = '" + groupCode + "' and CONTROL_ID = '" + mainNodeKey + "'");
                        }
                        else
                        {
                            if (enableControlCount > 0)
                                crud.ExecNonQuery("update USER_PRINT_CONTROL_ACCESS set VISIBLE = '" + enable + "', ENABLED = '" + enable + "' where CODE = '" + groupCode + "' and CONTROL_ID = '" + mainNodeKey + "'");
                        }

                    }
                }

                LoadControlAccessByGroupCode();

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
                throw;
            }
        }
        private void LoadLstCode()
        {
            lstCode.Items.Clear();
            dtCode = crud.ExecQuery("select distinct CODE from USER_PRINT_CONTROL_ACCESS order by case when CODE = 'DEFAULT' then 0 else 1 end, CODE");

            for (int m = 0; m < dtCode.Rows.Count; m++)
            {
                lstCode.Items.Add(dtCode.Rows[m]["CODE"]).ToString();
            }
        }
        private void LoadAllAccessControl()
        {
            tvUserRole.Nodes.Clear();
            DataTable dtPrintReference = crud.ExecQuery("select CONTROL_ID, CONTROL_DESC from USER_PRINT_CONTROL where SUBMENU_OF IS NULL order by CONTROL_DESC");
            for (int i = 0; i < dtPrintReference.Rows.Count; i++)
            {
                string mainNoteKey = dtPrintReference.Rows[i]["CONTROL_ID"].ToString();
                string mainNoteText = dtPrintReference.Rows[i]["CONTROL_DESC"].ToString();

                DataTable dtPrintReferenceDetail = crud.ExecQuery("select CONTROL_ID, CONTROL_DESC from USER_PRINT_CONTROL where SUBMENU_OF = '" + mainNoteKey + "' order by CONTROL_DESC");

                tvUserRole.Nodes.Add(mainNoteKey, mainNoteText);

                if (dtPrintReferenceDetail.Rows.Count > 0)
                {
                    for (int j = 0; j < dtPrintReferenceDetail.Rows.Count; j++)
                    {
                        string childNoteKey = dtPrintReferenceDetail.Rows[j]["CONTROL_ID"].ToString();
                        string childNoteText = dtPrintReferenceDetail.Rows[j]["CONTROL_DESC"].ToString();

                        tvUserRole.Nodes[i].Nodes.Add(childNoteKey, childNoteText);
                    }
                }
            }
        }
        private void LoadControlAccessByGroupCode()
        {
            var code = lstCode.SelectedItems[0].ToString();
            var dtAllowAccessCode = crud.ExecQuery("select * from USER_PRINT_CONTROL_ACCESS where code = '" + code + "'");

            var notAllowAccessRows = dtAllowAccessCode.Select(" [VISIBLE] = 'N' AND [ENABLED] = 'N' ");
            var notAllowAccess = new DataTable();
            if (notAllowAccessRows.Length > 0)
                notAllowAccess = notAllowAccessRows.CopyToDataTable();

            var allowAccessRows = dtAllowAccessCode.Select(" [VISIBLE] = 'Y' AND [ENABLED] = 'Y' ");
            var allowAccess = new DataTable();
            if (allowAccessRows.Length > 0)
                allowAccess = allowAccessRows.CopyToDataTable();

            for (int i = 0; i < notAllowAccess.Rows.Count; i++)
            {
                var key = notAllowAccess.Rows[i]["CONTROL_ID"].ToString();
                var index = tvUserRole.Nodes.IndexOfKey(key);

                if (tvUserRole.Nodes.ContainsKey(key))
                    tvUserRole.Nodes[index].ForeColor = Color.Gray;

                var childNoteKeys = crud.ExecQuery("select upc.CONTROL_ID, upa.VISIBLE, upa.ENABLED from USER_PRINT_CONTROL upc inner join USER_PRINT_CONTROL_ACCESS upa on upa.CONTROL_ID = upc.CONTROL_ID where SUBMENU_OF = '" + key + "' and CODE = '" + code + "'");
                bool hasChildNote = childNoteKeys.Rows.Count > 0;
                int childKeyCount = 0;

                if (hasChildNote)
                {
                    for (int l = 0; l < childNoteKeys.Rows.Count; l++)
                    {
                        var childKey = childNoteKeys.Rows[l]["CONTROL_ID"].ToString();
                        var childIndex = tvUserRole.Nodes[index].Nodes.IndexOfKey(childKey);

                        if (tvUserRole.Nodes[index].Nodes.ContainsKey(childKey))
                        {
                            if (childNoteKeys.Rows[l]["VISIBLE"].ToString() == "Y" && childNoteKeys.Rows[l]["ENABLED"].ToString() == "Y")
                                tvUserRole.Nodes[index].Nodes[childIndex].ForeColor = Color.Black;
                            else
                            {
                                tvUserRole.Nodes[index].Nodes[childIndex].ForeColor = Color.Gray;
                                childKeyCount = childKeyCount + 1;
                            }
                        }

                    }
                    if (childKeyCount == childNoteKeys.Rows.Count)
                        tvUserRole.Nodes[index].ForeColor = Color.Gray;
                    else
                        tvUserRole.Nodes[index].ForeColor = Color.Black;
                }
            }

            for (int j = 0; j < allowAccess.Rows.Count; j++)
            {
                var key = allowAccess.Rows[j]["CONTROL_ID"].ToString();
                var index = tvUserRole.Nodes.IndexOfKey(key);

                if (tvUserRole.Nodes.ContainsKey(key))
                    tvUserRole.Nodes[index].ForeColor = Color.Black;

                var childNoteKeys = crud.ExecQuery("select upc.CONTROL_ID, upa.VISIBLE, upa.ENABLED from USER_PRINT_CONTROL upc inner join USER_PRINT_CONTROL_ACCESS upa on upa.CONTROL_ID = upc.CONTROL_ID where SUBMENU_OF = '" + key + "' and CODE = '" + code + "'");
                bool hasChildNote = childNoteKeys.Rows.Count > 0;
                int childKeyCount = 0;

                if (hasChildNote)
                {
                    for (int l = 0; l < childNoteKeys.Rows.Count; l++)
                    {
                        var childKey = childNoteKeys.Rows[l]["CONTROL_ID"].ToString();
                        var childIndex = tvUserRole.Nodes[index].Nodes.IndexOfKey(childKey);

                        if (tvUserRole.Nodes[index].Nodes.ContainsKey(childKey))
                        {
                            if (childNoteKeys.Rows[l]["VISIBLE"].ToString() == "N" && childNoteKeys.Rows[l]["ENABLED"].ToString() == "N")
                            {
                                tvUserRole.Nodes[index].Nodes[childIndex].ForeColor = Color.Gray;
                                childKeyCount = childKeyCount + 1;
                            }
                            else
                                tvUserRole.Nodes[index].Nodes[childIndex].ForeColor = Color.Black;
                        }

                    }
                    if (childKeyCount == childNoteKeys.Rows.Count)
                        tvUserRole.Nodes[index].ForeColor = Color.Gray;
                    else
                        tvUserRole.Nodes[index].ForeColor = Color.Black;
                }
            }
        }
    }
}
