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
    public partial class frmPrintAutoLabel : Form
    {
        CRUD crud = new CRUD();

        public frmPrintAutoLabel()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPolicyNo.TextLength < 20)
            {
                Msgbox.Show("Wrong policy no");
                ActiveControl = txtPolicyNo;
            }

            var dtPrintAutoLabel = crud.ExecQuery("select * from VIEW_PRINT_AUTO_LABEL where POL_POLICY_NO = '" + txtPolicyNo.Text.ToUpper().Trim() + "'");

            CrystalDecisions.CrystalReports.Engine.ReportClass report = new CrystalDecisions.CrystalReports.Engine.ReportClass();
            report = new Reports.PrintAutoLabel();

            report.SetDataSource(dtPrintAutoLabel);

            rpvPrintAutoLabel.ReportSource = report;
        }
    }
}
