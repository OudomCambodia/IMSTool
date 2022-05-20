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
    public partial class frmAddNewInformation : Form
    {
        CRUD crud = new CRUD();
        public string UserName;
        public frmUploadInformation up_inf;
        bool save = false;

        public frmAddNewInformation()
        {
            InitializeComponent();
        }

        private void frmAddNewInformation_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPolicyNo.Text = "";
            txtCusCode.Text = "";
            txtCustomerName.Text = "";
            txtRemark.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql = "select * from USER_UPLOAD_DOC where POLICY_NO = '" + txtPolicyNo.Text.Trim().ToUpper() + "'";

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtPolicyNo.Text.Trim() == "")
                {
                    Msgbox.Show("Policy No cannot be empty.");
                    this.ActiveControl = txtPolicyNo;
                    return;
                }

                DataTable dt = new DataTable();
                dt = crud.ExecQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    Msgbox.Show("This Policy No already exists in the list.");
                    return;
                }

                if (CheckExist() == false)
                    return;

                sql = @"insert into USER_UPLOAD_DOC 
                    (CUSTOMER_CODE, CUSTOMER_NAME, POLICY_NO, ISSUE_DATE, ISSUE_BY, REMARK)
                    values
                    ('" + txtCusCode.Text + "', q'[" + txtCustomerName.Text + "]', '" + txtPolicyNo.Text.Trim().ToUpper() + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + UserName + "', q'[" + txtRemark.Text.Trim() + "]')";
                crud.ExecNonQuery(sql);
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show("The record has been saved.");
                up_inf.txtPolicyNo.Text = txtPolicyNo.Text.Trim().ToUpper();
                up_inf.GetDataGrid();
                save = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void txtPolicyNo_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl == btnClear || this.ActiveControl == btnClose || this.ActiveControl == btnSave)
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CheckExist();
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private bool CheckExist()
        {
            if (txtPolicyNo.Text.Trim() == "")
            {
                txtCusCode.Text = "";
                txtCustomerName.Text = "";
                return false;
            }

            string sql = "select * from VIEW_POLICY_INFORMATION where POL_POLICY_NO = q'[" + txtPolicyNo.Text.Trim().ToUpper() + "]'";
            DataTable dt = new DataTable();
            dt = crud.ExecQuery(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtCusCode.Text = dr[0].ToString();
                txtCustomerName.Text = dr[1].ToString();
            }
            else
            {
                txtCusCode.Text = "";
                txtCustomerName.Text = "";
                Msgbox.Show("This Policy No is incorrect or inactive or canceled.", "Warning");
                this.ActiveControl = txtPolicyNo;
                txtPolicyNo.SelectAll();
                return false;
            }

            return true;
        }

        private void frmAddNewInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (save == true)
                up_inf.GoToViewDetail();
        }
    }
}
