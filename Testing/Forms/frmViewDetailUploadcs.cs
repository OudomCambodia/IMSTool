using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmViewDetailUploadcs : Form
    {
        CRUD crud = new CRUD();
        public string policy_no, cus_code, cus_name, upload_id, UserName;

        public frmViewDetailUploadcs()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = crud.ExecQuery("select POL_ENDORSEMENT_NO from VIEW_POLICY_INFORMATION where POL_POLICY_NO = '" + policy_no + "'");

            if (dt.Rows.Count <= 0)
            {
                Msgbox.Show("The policy is not in Authorized Status!");
                return;
            }

            for (int i = 0; i < dgvEndorsement.Rows.Count; i++)
            {
                if (dt.Rows[0].ItemArray[0].ToString() == dgvEndorsement.Rows[i].Cells[2].Value.ToString())
                {
                    Msgbox.Show("The last endorsement of this policy already had the uploaded file(s).");
                    return;
                }
            }

            frmFileUpload frmadd = new frmFileUpload();
            frmadd.upl_id = upload_id;
            frmadd.UserName =  UserName;
            frmadd.policy_no = policy_no;
            frmadd.vdUpload = this;
            frmadd.ShowDialog();
        }

        private void frmViewDetailUploadcs_Load(object sender, EventArgs e)
        {
            txtPolicyNo.Text = policy_no;
            txtCusCode.Text = cus_code;
            txtCustomerName.Text = cus_name;
            
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                GetDataGrid();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

            Cursor.Current = Cursors.AppStarting;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewButt();
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = crud.ExecQuery("select POL_ENDORSEMENT_NO from VIEW_POLICY_INFORMATION where POL_POLICY_NO = '" + policy_no + "'");
            for (int i = 0; i < dgvEndorsement.Rows.Count; i++)
            {
                if (dt.Rows[0].ItemArray[0].ToString() == dgvEndorsement.Rows[i].Cells[2].Value.ToString())
                {
                    DialogResult dr = Msgbox.Show("Are you sure that you want to re-upload file(s) for the last endorsement?", "Confirmation");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        frmFileUpload frmadd = new frmFileUpload();
                        frmadd.upl_id = upload_id;
                        frmadd.UserName = UserName;
                        frmadd.policy_no = policy_no;
                        frmadd.vdUpload = this;
                        frmadd.UpdateBtn = true;
                        frmadd.ShowDialog();
                    }

                    return;
                }
            }

            Msgbox.Show("No uploaded file(s) are found to re-upload in the last endorsement of this policy. The last endorsement of this policy is " + dt.Rows[0].ItemArray[0].ToString());
        }

        private void dgvEndorsement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            ViewButt();
        }

        private void ViewButt()
        {
            if (dgvEndorsement.Rows.Count <= 0)
            {
                Msgbox.Show("No data found in the table!");
                return;
            }

            frmViewFiles fvf = new frmViewFiles();
            fvf.txtPolicyNo.Text = txtPolicyNo.Text;
            fvf.upl_detail_id = dgvEndorsement.SelectedRows[0].Cells[0].Value.ToString();
            fvf.txtEndoNo.Text = dgvEndorsement.SelectedRows[0].Cells[2].Value.ToString();           
            fvf.dtpEffFrom.Text = dgvEndorsement.SelectedRows[0].Cells[3].Value.ToString();
            fvf.dtpEffTo.Text = dgvEndorsement.SelectedRows[0].Cells[4].Value.ToString();
            fvf.txtRemark.Text = dgvEndorsement.SelectedRows[0].Cells[5].Value.ToString();
            fvf.ShowDialog();
        }

        public void GetDataGrid()
        {
            string sql = "select * from user_upload_detail where UPLOAD_ID = " + upload_id;
            dgvEndorsement.DataSource = crud.ExecQuery(sql);

            for (int i = 0; i < dgvEndorsement.Columns.Count; i++)
                dgvEndorsement.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvEndorsement.Columns[0].Visible = false;
            dgvEndorsement.Columns[1].Visible = false;
            dgvEndorsement.Columns[6].Visible = false;
            dgvEndorsement.Columns[7].Visible = false;
            dgvEndorsement.Columns[8].Visible = false;
            dgvEndorsement.Columns[9].Visible = false;
        }

        private void dgvEndorsement_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvEndorsement);
        }

        private void dgvEndorsement_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
