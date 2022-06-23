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
    public partial class frmAddNewControl : Form
    {
        private CRUD crud = new CRUD();
        private string controlId = frmUserRoleManagement.ControlId;
        private string controlName = frmUserRoleManagement.ControlName;
        private string controlDesc = frmUserRoleManagement.ControlDesc;
        private string subMenuOf = frmUserRoleManagement.SubMenuOf;
        private bool isAddNew = frmUserRoleManagement.IsAddNew;

        public frmAddNewControl()
        {
            InitializeComponent();
            MaximizeBox = false;

            DataTable dtSubMenuOf = crud.ExecQuery("select CONTROL_ID, CONTROL_DESC from USER_PRINT_CONTROL where SUBMENU_OF is null order by CONTROL_DESC");
            if (dtSubMenuOf.Rows.Count > 0)
            {
                cboSubMenuOf.Items.Add(new ComboboxItem("N/A", "N/A"));

                for (int i = 0; i < dtSubMenuOf.Rows.Count; i++)
                {
                    var value = dtSubMenuOf.Rows[i]["CONTROL_ID"].ToString();
                    var text = dtSubMenuOf.Rows[i]["CONTROL_DESC"].ToString();

                    if (cboSubMenuOf.FindString(text) < 0)
                        cboSubMenuOf.Items.Add(new ComboboxItem(text, value));
                }
            }
        }

        private void frmAddNewControl_Load(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = txtControlName;
                btnSave.Text = isAddNew ? "Save" : "Update";
                Text = isAddNew ? "Add New Control" : "Update Control";

                var dtSubMenuString = crud.ExecQuery("select CONTROL_DESC from USER_PRINT_CONTROL where CONTROL_ID = '" + subMenuOf + "'");
                var subMenuString = string.Empty;
                if (dtSubMenuString.Rows.Count > 0)
                    subMenuString = dtSubMenuString.Rows[0]["CONTROL_DESC"].ToString();

                if (!isAddNew)
                {
                    txtControlName.Text = controlName;
                    txtControlDesc.Text = controlDesc;
                    if (!string.IsNullOrEmpty(subMenuString))
                        cboSubMenuOf.Text = subMenuString;
                    else
                        cboSubMenuOf.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
                throw;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetValidator(new Control[] { txtControlName, txtControlDesc, cboSubMenuOf }))
                    return;

                var dtGroupCodeToInsert = crud.ExecQuery("select distinct CODE from USER_PRINT_CONTROL_ACCESS order by CODE");
                var controlName = txtControlName.Text.Trim();
                var controlDesc = txtControlDesc.Text.Trim();
                var subMenuOfText = cboSubMenuOf.Text == "N/A" ? "0" : cboSubMenuOf.Text;

                if (isAddNew)
                {
                    var existedControl = crud.ExecQuery("select CONTROL_NAME from USER_PRINT_CONTROL where CONTROL_NAME = '" + controlName + "'").Rows.Count > 0;
                    if (existedControl)
                    {
                        Msgbox.Show("Control already existed!");
                        return;
                    }

                    var existedControlDesc = crud.ExecQuery("select CONTROL_DESC from USER_PRINT_CONTROL where CONTROL_DESC = '" + controlDesc + "'").Rows.Count > 0;
                    if (existedControlDesc)
                    {
                        Msgbox.Show("Control Description already existed!");
                        return;
                    }

                    DialogResult confirmInsert = Msgbox.Show("Are you sure you want to insert this control?", "Insert Control");
                    if (confirmInsert == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;

                        var subMenuOfId = string.Empty;
                        var dtSubMenuId = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where CONTROL_DESC = '" + subMenuOfText + "'");
                        subMenuOfId = dtSubMenuId.Rows.Count > 0 ? dtSubMenuId.Rows[0]["CONTROL_ID"].ToString() : null;

                        StringBuilder insertBuilder = new StringBuilder();
                        insertBuilder.Append("insert into USER_PRINT_CONTROL(CONTROL_ID, CONTROL_NAME, CONTROL_DESC, SUBMENU_OF) VALUES(USER_PRINT_CONTROL_SEQ.NEXTVAL, '" + controlName + "', '" + controlDesc + "', '" + subMenuOfId + "')");
                        crud.ExecNonQuery(insertBuilder.ToString());

                        var localControlId = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where CONTROL_NAME = '" + controlName + "'").Rows[0]["CONTROL_ID"];
                        if (dtGroupCodeToInsert.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtGroupCodeToInsert.Rows.Count; i++)
                            {
                                var groupCode = dtGroupCodeToInsert.Rows[i]["CODE"];
                                string insertControlToControlAccess = "insert into USER_PRINT_CONTROL_ACCESS(CODE, CONTROL_ID, VISIBLE, ENABLED) VALUES('" + groupCode + "', '" + localControlId + "', 'N', 'N')";
                                crud.ExecNonQuery(insertControlToControlAccess);
                            }
                        }
                        Cursor = Cursors.Arrow;
                        Msgbox.Show("Control successfully inserted!");
                    }
                }
                else
                {
                    var existedControl = crud.ExecQuery("select CONTROL_NAME from USER_PRINT_CONTROL where CONTROL_NAME = '" + controlName + "' and CONTROL_ID != '" + controlId + "'").Rows.Count > 0;
                    if (existedControl)
                    {
                        Msgbox.Show("Control already existed!");
                        return;
                    }

                    var existedControlDesc = crud.ExecQuery("select CONTROL_DESC from USER_PRINT_CONTROL where CONTROL_DESC = '" + controlDesc + "' and CONTROL_ID != '" + controlId + "'").Rows.Count > 0;
                    if (existedControlDesc)
                    {
                        Msgbox.Show("Control Description already existed!");
                        return;
                    }

                    DialogResult confirmUpdate = Msgbox.Show("Are you sure you want to update this control?", "Update Control");
                    if (confirmUpdate == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;

                        var subMenuOfIdUpdate = string.Empty;
                        var dtSubMenuIdUpdate = crud.ExecQuery("select CONTROL_ID from USER_PRINT_CONTROL where CONTROL_DESC = '" + cboSubMenuOf.Text.Trim() + "'");
                        subMenuOfIdUpdate = dtSubMenuIdUpdate.Rows.Count > 0 ? dtSubMenuIdUpdate.Rows[0]["CONTROL_ID"].ToString() : null;

                        StringBuilder updateBuilder = new StringBuilder();
                        updateBuilder.Append("update USER_PRINT_CONTROL set CONTROL_NAME = '" + controlName + "', CONTROL_DESC = '" + controlDesc + "', SUBMENU_OF = '" + subMenuOfIdUpdate + "' where CONTROL_ID = " + controlId + "");
                        crud.ExecNonQuery(updateBuilder.ToString());
                        Msgbox.Show("Control successfully updated!");

                        Cursor = Cursors.Arrow;
                    }
                }
                DialogResult = DialogResult.OK;
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
