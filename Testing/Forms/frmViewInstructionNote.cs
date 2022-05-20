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
    public partial class frmViewInstructionNote : Form
    {
        public object rpt;

        public frmViewInstructionNote()
        {
            InitializeComponent();
        }


        private void frmViewInstructionNote_Load(object sender, EventArgs e)
        {
            //crystalReportViewer1.Dock = DockStyle.Fill;
            crystalReportViewer1.ReportSource = rpt;
            //axReportViewer1.ReportSource = rpt;
           // crystalReportViewer1.RefreshReport();            
        }


    }
}
