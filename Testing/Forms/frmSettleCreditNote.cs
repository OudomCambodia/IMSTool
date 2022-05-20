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
    public partial class frmSettleCreditNote : Form
    {

        DataTable result;
        CRUD crud = new CRUD();

        public frmSettleCreditNote()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string dfrom = dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00",
                    dto = dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59";
                result = crud.ExecSP_OutPara("SP_SETTLE_CREDIT",
                    new string[] { "date_from", "date_to" }, new string[] { dfrom, dto });
                dgvResult.DataSource = result;
                Cursor.Current = Cursors.AppStarting;
                if (result.Rows.Count == 0)
                {
                    Msgbox.Show("No data found!");
                }

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show(ex.Message);
            }
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            if (dgvResult.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(result, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpTo.Value = DateTime.Now;
            dgvResult.DataSource = null;
            dgvResult.Rows.Clear();
        }

        private void dgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvResult);
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotalNum.Text = dgvResult.RowCount.ToString();
        }
    }
}
