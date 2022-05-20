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
    public partial class frmUploadInformation : Form
    {
        CRUD crud = new CRUD();
        public string UserName = "SICL";

        public frmUploadInformation()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPolicyNo.Text.Trim() == "")
            {
                Msgbox.Show("Policy No must be input before seaching.");
                return;
            }
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            DetailButt();
        }

        private void dgvPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //disable event triggered on DataGridView headers clicked
            if (e.RowIndex == -1)
                return;

            DetailButt();
        }

        private void DetailButt()
        {
            if (dgvPolicy.Rows.Count <= 0)
            {
                Msgbox.Show("No data found in the table!");
                return;
            }

            int sel = dgvPolicy.SelectedRows[0].Index;

            if (dgvPolicy.SelectedRows[0].Cells[7].Value.ToString() == "New")
            {
                try
                {
                    string sql = @"insert into USER_UPLOAD_DOC 
                    (CUSTOMER_CODE, CUSTOMER_NAME, POLICY_NO, ISSUE_DATE, ISSUE_BY)
                    values
                    ('" + dgvPolicy.SelectedRows[0].Cells[1].Value.ToString() + "', q'[" + dgvPolicy.SelectedRows[0].Cells[2].Value.ToString() + "]', '" + dgvPolicy.SelectedRows[0].Cells[3].Value.ToString() + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + UserName + "')";
                    crud.ExecNonQuery(sql);
                    DataTable dt = new DataTable();
                    dt = crud.ExecQuery("select * from USER_UPLOAD_DOC where POLICY_NO = '" + dgvPolicy.SelectedRows[0].Cells[3].Value.ToString() + "'");
                    DataTable dtGrid = new DataTable();
                    dtGrid = ((DataTable)(dgvPolicy.DataSource)).Copy();
                    foreach (DataColumn dc in dtGrid.Columns)
                    {
                        dc.ReadOnly = false;
                        //dc.MaxLength = 200;
                    }
                    dtGrid.Rows[sel][0] = dt.Rows[0].ItemArray[0].ToString();
                    dtGrid.Rows[sel][4] = dt.Rows[0].ItemArray[4].ToString();
                    dtGrid.Rows[sel][5] = dt.Rows[0].ItemArray[5].ToString();
                    dtGrid.Rows[sel][6] = dt.Rows[0].ItemArray[6].ToString();
                    dtGrid.Rows[sel][7] = "Existing";
                    dgvPolicy.DataSource = dtGrid;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            frmViewDetailUploadcs frmDetail = new frmViewDetailUploadcs();
            frmDetail.policy_no = dgvPolicy.Rows[sel].Cells[3].Value.ToString();
            frmDetail.cus_code = dgvPolicy.Rows[sel].Cells[1].Value.ToString();
            frmDetail.cus_name = dgvPolicy.Rows[sel].Cells[2].Value.ToString();
            frmDetail.upload_id = dgvPolicy.Rows[sel].Cells[0].Value.ToString();
            frmDetail.UserName = UserName;
            frmDetail.ShowDialog();
        }

        public void GetDataGrid()
        {
            string sql = "select * from USER_UPLOAD_DOC where rownum <= 200 ";

            sql += "and POLICY_NO like q'[%" + txtPolicyNo.Text.Trim().ToUpper() + "%]' ";
            if (txtCusCode.Text.Trim() != "")
                sql += "and CUSTOMER_CODE like q'[%" + txtCusCode.Text.Trim().ToUpper() + "%]' ";
            if (txtCustomerName.Text.Trim() != "")
                sql += "and CUSTOMER_NAME like q'[%" + txtCustomerName.Text.Trim().ToUpper() + "%]' ";
            
            DataTable dt = crud.ExecQuery(sql);
            dt.Columns.Add("Status", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["Status"] = "Existing";
            }

            //if (dt.Rows.Count <= 0)
            //{
            //    sql = "select 'No', POL_CUS_CODE, CUSTOMER_NAME, POL_POLICY_NO, 'Issue_date', 'User', 'Remark' from VIEW_POLICY_INFORMATION where rownum <= 100 ";
            //    sql += "and POL_POLICY_NO like q'[%" + txtPolicyNo.Text.Trim().ToUpper() + "%]' ";
            //    if (txtCusCode.Text.Trim() != "")
            //        sql += "and POL_CUS_CODE like q'[%" + txtCusCode.Text.Trim().ToUpper() + "%]' ";
            //    if (txtCustomerName.Text.Trim() != "")
            //        sql += "and CUSTOMER_NAME like q'[%" + txtCustomerName.Text.Trim().ToUpper() + "%]' ";
            //    dt = crud.ExecQuery(sql); 
            //    dt.Columns.Add("Status", typeof(string));

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dr["Status"] = "New";
            //    }

            //    if (dt.Rows.Count <= 0)
            //    {
            //        Msgbox.Show("This Policy No is incorrect or inactive or canceled or unauthorized.");
            //    }

            //    New = true;
            //}
            if (dt.Rows.Count < 100)
            {
                DataTable dtTemp = new DataTable();
                sql = "select '1', POL_CUS_CODE, CUSTOMER_NAME, POL_POLICY_NO, TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), 'User', 'Remark' from VIEW_POLICY_INFORMATION where rownum <= " + (200 - dt.Rows.Count).ToString();
                sql += " and POL_POLICY_NO not in (select POLICY_NO from USER_UPLOAD_DOC) ";
                sql += " and POL_POLICY_NO like q'[%" + txtPolicyNo.Text.Trim().ToUpper() + "%]' ";
                if (txtCusCode.Text.Trim() != "")
                    sql += " and POL_CUS_CODE like q'[%" + txtCusCode.Text.Trim().ToUpper() + "%]' ";
                if (txtCustomerName.Text.Trim() != "")
                    sql += " and CUSTOMER_NAME like q'[%" + txtCustomerName.Text.Trim().ToUpper() + "%]' ";
                dtTemp = crud.ExecQuery(sql);
                dtTemp.Columns.Add("Status", typeof(string));

                foreach (DataRow dr in dtTemp.Rows)
                    dr["Status"] = "New";

                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (!(dr[3].ToString().Length > 20 && dr[3].ToString().Contains("-")))
                        dt.Rows.Add(dr.ItemArray);
                }

                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("This Policy No is incorrect or inactive or canceled or unauthorized.");
                }
            }
            dgvPolicy.DataSource = dt;

            for (int i = 0; i < dgvPolicy.Columns.Count; i++)
                dgvPolicy.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvPolicy.Columns[0].Visible = false;
            dgvPolicy.Columns[4].Visible = false;
            dgvPolicy.Columns[5].Visible = false;
            dgvPolicy.Columns[6].Visible = false;
        }

        public void GoToViewDetail()
        {
            DetailButt();
        }

        private void frmUploadInformation_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sql = "select * from USER_UPLOAD_DOC where rownum <= 200 ";
            dt = crud.ExecQuery(sql);

            dt.Columns.Add("Status", typeof(string));
            foreach (DataRow dr in dt.Rows)
                dr["Status"] = "Existing";
            dgvPolicy.DataSource = dt;

            for (int i = 0; i < dgvPolicy.Columns.Count; i++)
                dgvPolicy.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;           

            dgvPolicy.Columns[0].Visible = false;
            dgvPolicy.Columns[4].Visible = false;
            dgvPolicy.Columns[5].Visible = false;
            dgvPolicy.Columns[6].Visible = false;
        }

        private void dgvPolicy_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvPolicy);
            lbTotNumber.Text = dgvPolicy.Rows.Count.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvPolicy.DataSource = null;
            txtCusCode.Text = "";
            txtPolicyNo.Text = "";
            txtCustomerName.Text = "";
        }
    }
}