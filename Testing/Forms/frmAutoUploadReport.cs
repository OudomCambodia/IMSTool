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
    public partial class frmAutoUploadReport : Form
    {
        private CRUD crud = new CRUD();

        public frmAutoUploadReport()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExcelPath.Text == "")
                    Msgbox.Show("Batch No is required!");
                else
                {
                    Cursor = Cursors.WaitCursor;

                    string[] Keys = new string[] { "sp_batch_no" };
                    string[] Values = new string[] { txtExcelPath.Text.ToUpper() };
                    var dtReport = crud.ExecSP_OutPara("SP_USER_AUTO_UPLOAD_REPORT", Keys, Values);
                    dgvView.DataSource = dtReport;

                    if (dtReport.Rows.Count > 0)
                    {
                        CommonFunctions.HighLightGrid(dgvView);
                        dgvView.ForeColor = System.Drawing.Color.Black;
                        dgvView.DataSource = dtReport;

                        lbTotal.Text = dtReport.Rows.Count.ToString();
                    }
                    else
                        Msgbox.Show("Batch is not existed!");

                    Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.Message);
            }
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (dgvView.Rows.Count <= 0)
                    Msgbox.Show("No Data to export!");
                else
                    My_DataTable_Extensions.ExportToExcel(dgvView.DataSource as DataTable);

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.Message);
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            txtExcelPath.Text = string.Empty;
            lbTotal.Text = string.Empty;
            dgvView.DataSource = null;

            Cursor = Cursors.Arrow;
        }
    }
}

