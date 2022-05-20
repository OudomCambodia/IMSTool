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
    public partial class frmClaimIncurredReport : Form
    {

        public object rpt;

        public frmClaimIncurredReport()
        {
            InitializeComponent();
        }

        private void frmClaimIncurredReport_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = rpt;
        }

    }
}
