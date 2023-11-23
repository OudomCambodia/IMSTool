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
    public partial class FrmTravelReport : Form
    {
        private DataTable dt;
        CRUD crud = new CRUD();
        public FrmTravelReport()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string dfrom = dtpFrom.Value.ToString("yyyy/MM/dd");
                dt = crud.ExecSP_OutPara("PRO_TRAVEL_INFO", new string[] { "date_from", "date_to", "certi" }, new string[] { dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd"), txtCertificateNO.Text.Trim() });
                dgvResult.DataSource = dt;
                Cursor.Current = Cursors.AppStarting;
                if (dt.Rows.Count == 0)
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
                My_DataTable_Extensions.ExportToExcel(dt, "");
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
