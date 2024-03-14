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
    public partial class frmANHEmailClaimReport : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtData = new DataTable();

        public frmANHEmailClaimReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            string[] Keys = new string[] { "p_date_from", "p_date_to" };
            DateTime[] Values = new DateTime[] { new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, 0, 0, 0), new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, 23, 59, 59) };
            dtData = crud.ExecSP_OutPara("SP_USER_CLAIM_EMAIL_HIST", Keys, Values);
            dgvData.DataSource = dtData;

            Cursor = Cursors.Arrow;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (dtData.Rows.Count > 0)
                My_DataTable_Extensions.ExportToExcelXML(dtData);

            Cursor = Cursors.Arrow;
        }
    }
}
