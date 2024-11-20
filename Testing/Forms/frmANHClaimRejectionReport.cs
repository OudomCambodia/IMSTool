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
    public partial class frmANHClaimRejectionReport : Form
    {
        private CRUD crud = new CRUD();

        public frmANHClaimRejectionReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try 
            {
                string[] Keys = new string[] { "p_date_from", "p_date_to" };
                string[] Values = new string[] { dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                var dtClaimRejection = crud.ExecSP_OutPara("SP_USER_ANH_CLAIM_REJECTION", Keys, Values);

                if (dtClaimRejection == null || dtClaimRejection.Rows.Count <= 0)
                {
                    Cursor = Cursors.Arrow;
                    dgvClaimRejection.DataSource = null;
                    return;
                }

                dgvClaimRejection.DataSource = dtClaimRejection;
                    
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }

            Cursor = Cursors.Arrow;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (dgvClaimRejection.DataSource == null)
                {
                    Cursor = Cursors.Arrow;
                    Msgbox.Show("No row to export");
                    return;
                }

                My_DataTable_Extensions.ExportToExcelXML(dgvClaimRejection.DataSource as DataTable);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }

            Cursor = Cursors.Arrow;
        }
    }
}
