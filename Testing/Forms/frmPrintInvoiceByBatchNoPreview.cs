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
    public partial class frmPrintInvoiceByBatchNoPreview : Form
    {
        public object rpt;

        public frmPrintInvoiceByBatchNoPreview()
        {
            InitializeComponent();
        }

        private void frmPrintInvoiceByBatchNoPreview_Load(object sender, EventArgs e)
        {
            crReportViewer.ReportSource = rpt;
        }
    }
}
