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
    public partial class frmPEMReport : Form
    {
        private CRUD crud = new CRUD();

        public frmPEMReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string[] Keys = new string[] { "sp_date_from", "sp_date_to" };
                string[] Values = new string[] { dtpFromDate.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpToDate.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                var dt = crud.ExecSP_OutPara("SP_USER_PEM_PREMIUM_REPORT", Keys, Values);
                dgvReport.DataSource = dt;

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                My_DataTable_Extensions.ExportToExcelXML((DataTable)dgvReport.DataSource);

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }
    }
}
