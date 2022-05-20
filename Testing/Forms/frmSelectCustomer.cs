using Oracle.ManagedDataAccess.Client;
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
    public partial class frmSelectCustomer : Form
    {
        public frmSelectCustomer()
        {
            InitializeComponent();
        }

        string VIEW_CUSTOMER = "(select CUS_CODE, nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) CUS_NAME, CUS_CODE || ' '|| nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) AS SEARCH_STR from UW_M_CUSTOMERS where CUS_STATUS = 'A') ";
        CRUD crud = new CRUD();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text.Trim() == "")
            {
                Msgbox.Show("Please input Customer Code/Name to search.");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            dgvResult.DataSource = null;
            dgvResult.Columns.Clear();

            //DataTable dtTemp = crud.ExecQuery("SELECT CUS_CODE, CUS_NAME, SEARCH_STR FROM " + VIEW_CUSTOMER + " WHERE SEARCH_STR like '%" + tbSearch.Text.Trim().ToUpper() + "%'");
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "SELECT CUS_CODE, CUS_NAME, SEARCH_STR FROM " + VIEW_CUSTOMER + " WHERE SEARCH_STR like '%' || :search || '%'";
            cmd.Parameters.Add(new OracleParameter(":search", tbSearch.Text.Trim().ToUpper()));
            DataTable dtTemp = crud.ExecQuery(cmd);

            dgvResult.DataSource = dtTemp;
            dgvResult.Columns["SEARCH_STR"].Visible = false;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            Cursor.Current = Cursors.AppStarting;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int selectedRow = e.RowIndex;
            string code = dgvResult.Rows[selectedRow].Cells["CUS_CODE"].Value.ToString(),
                name = dgvResult.Rows[selectedRow].Cells["CUS_NAME"].Value.ToString(); ;
            frmAddDocument1.selectedCusCode = code;
            frmAddDocument1.selectedCusName = name;
            frmManageCrono.selectedCusCode = code;
            frmManageCrono.selectedCusName = name;
            frmInvoiceSetting.selectedCusCode = code;
            frmInvoiceSetting.selectedCusName = name;
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.CurrentCell.RowIndex != -1)
                {
                    int selectedRow = dgvResult.CurrentCell.RowIndex;
                    string code = dgvResult.Rows[selectedRow].Cells["CUS_CODE"].Value.ToString(),
                 name = dgvResult.Rows[selectedRow].Cells["CUS_NAME"].Value.ToString(); ;
                    frmAddDocument1.selectedCusCode = code;
                    frmAddDocument1.selectedCusName = name;
                    frmManageCrono.selectedCusCode = code;
                    frmManageCrono.selectedCusName = name;
                    frmInvoiceSetting.selectedCusCode = code;
                    frmInvoiceSetting.selectedCusName = name;
                    this.Close();
                }
                else
                {
                    Msgbox.Show("Please choose customer in table first.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show("Please choose customer in table first.");
                return;
            }
        }
    }
}
