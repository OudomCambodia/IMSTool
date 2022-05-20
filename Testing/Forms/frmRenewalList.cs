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
    public partial class frmRenewalList : Form
    {
        private DataTable dt;
        CRUD crud = new CRUD();

        public frmRenewalList()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sp_type = "Renewal_List";
                string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd")+" 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd")+" 23:59:59" };
                dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);
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
    }
}
