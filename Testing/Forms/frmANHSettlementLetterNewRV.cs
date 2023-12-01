using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace Testing.Forms
{
    public partial class frmANHSettlementLetterNewRV : Form
    {
        private DataSet dsReport = new DataSet();

        private string path = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice";
        //private string path = @"D:\Settlement_Notice\";
        public static string FPath = string.Empty;
        string claimNo = string.Empty;

        public frmANHSettlementLetterNewRV(DataSet DsReport, string ClaimNo)
        {
            InitializeComponent();

            dsReport = DsReport;
            claimNo = ClaimNo;

            CrystalDecisions.CrystalReports.Engine.ReportClass report = new CrystalDecisions.CrystalReports.Engine.ReportClass();
            report = new Reports.SettlementLetterNew();

            report.SetDataSource(dsReport);

            reportViewer.ReportSource = report;

            System.IO.Directory.CreateDirectory(path);

            FPath = path + @"\" + claimNo.Replace('/', '-');

            report.ExportToDisk(ExportFormatType.WordForWindows, FPath + ".doc");

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            word.Visible = false;

            Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(FPath + ".doc");

            doc.SaveAs2(path + @"\" + claimNo.Replace('/', '-'), Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);

            doc.Close();
            word.Quit();

            File.Delete(FPath + ".doc");
            FPath = path + @"\" + claimNo.Replace('/', '-') + ".pdf";
        }
    }
}
