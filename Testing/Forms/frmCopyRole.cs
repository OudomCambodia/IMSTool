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
    public partial class frmCopyRole : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtAllowAccessCode;

        public frmCopyRole()
        {
            InitializeComponent();
            MaximizeBox = false;

            txtGroupCode.CharacterCasing = CharacterCasing.Upper;

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

        private void frmCopyRole_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var code = frmUserRoleManagement.GroupCode;
                ActiveControl = tvUserRole;
                txtGroupCode.Text = string.Concat(code, " - ", "COPY");

                dtAllowAccessCode = crud.ExecQuery("select * from USER_PRINT_CONTROL_ACCESS where code = '" + code + "'");

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

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void tvUserRole_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetValidator(new Control[] { txtGroupCode }))
                    return;

                var isExistGroupCode = crud.ExecQuery("select CODE from USER_PRINT_CONTROL_ACCESS where CODE = '" + txtGroupCode.Text.Trim() + "'").Rows.Count > 0;
                if (isExistGroupCode)
                {
                    Msgbox.Show("Group Code already existed!");
                    return;
                }

                DialogResult confirmSave = Msgbox.Show("Are you sure you want to save this role?", "Save Role");
                if (confirmSave == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    for (int i = 0; i < dtAllowAccessCode.Rows.Count; i++)
                    {
                        var code = txtGroupCode.Text.Trim();
                        var controlId = dtAllowAccessCode.Rows[i]["CONTROL_ID"].ToString();
                        var visible = dtAllowAccessCode.Rows[i]["VISIBLE"].ToString();
                        var enabled = dtAllowAccessCode.Rows[i]["ENABLED"].ToString();

                        StringBuilder insertBuilder = new StringBuilder();
                        insertBuilder.Append("INSERT INTO USER_PRINT_CONTROL_ACCESS(CODE, CONTROL_ID, VISIBLE, ENABLED) VALUES('" + code + "', '" + controlId + "', '" + visible + "', '" + enabled + "')");

                        crud.ExecNonQuery(insertBuilder.ToString());
                    }
                    Msgbox.Show("Role successfully saved!");
                    DialogResult = DialogResult.OK;

                    Cursor = Cursors.Arrow;
                }
                else
                    DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private bool SetValidator(Control[] controls)
        {
            var isEmpty = false;
            var setFocus = false;

            foreach (var control in controls)
            {
                if ((control is TextBox && string.IsNullOrEmpty(control.Text)) || (control is ComboBox && string.IsNullOrEmpty(control.Text)))
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
    }
}
