using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Testing.Forms
{
    public partial class frmPRRiskCount : Form
    {
        private CRUD crud = new CRUD();
        private DataTable dtRiskCount = new DataTable();

        public frmPRRiskCount()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            var fromDate = dtFromDate.Value.ToString("yyyy/MM/dd").Replace("-", "/");
            var toDate = dtToDate.Value.ToString("yyyy/MM/dd").Replace("-", "/");

            string query = File.ReadAllText("Html/PR Count Risk B Vannary.txt");

            var qBuilder = new StringBuilder();
            qBuilder.Append("select listagg('''' || PLN_DESCRIPTION || ''' as \"' || PLN_DESCRIPTION || '\"', ',') within group (order by PLN_DESCRIPTION)")
                .Append("from (select distinct PLN_DESCRIPTION from UW_T_PLANS where PLN_PRD_CODE in ")
                .Append("('BHP','CVD','CVO','CVP','EMC','HNS','MED','STN','DPA','GPA','PAC','PAE','PAP','TRI','TRA','TRP','TRV'))");

            var pivot = crud.ExecQuery(qBuilder.ToString()).Rows.Count > 0 ? crud.ExecQuery(qBuilder.ToString()).Rows[0][0].ToString() : string.Empty;
            pivot = pivot.Replace("&", "|| chr(38) ||");

            query = query.Replace("%Pivot%", pivot);
            query = query.Replace("%FromDate%", fromDate);
            query = query.Replace("%ToDate%", toDate);

            dtRiskCount = crud.ExecQuery(query.ToString());

            if (dtRiskCount.Rows.Count > 0)
            {
                dtRiskCount.Columns.Remove("LINKSEQ");
                dtRiskCount.Columns.Remove("SEQ_NO");
                dtRiskCount.Columns.Remove("POL_END_NO");
            }

            dgvRiskCount.DataSource = dtRiskCount;

            Cursor = Cursors.Arrow;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            My_DataTable_Extensions.ExportToExcel(dtRiskCount);
            Cursor = Cursors.Arrow;
        }
    }
}
