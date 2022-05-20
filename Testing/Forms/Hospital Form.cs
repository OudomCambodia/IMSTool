using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
//using Microsoft.VisualStudio.Tools.Applications.Runtime;
using System.IO;

namespace Testing.Forms
{
    public partial class Hospital_Form : Form
    {
        string exelFile = @"D:/template.xlsx";

        public Hospital_Form()
        {
            InitializeComponent();
        }

        private void Hospital_Form_Load(object sender, EventArgs e)
        {
            //Reports.Hospital_Form hf = new Reports.Hospital_Form();
            //crystalReportViewer1.ReportSource = hf;
        }

        private void cus_button1_Click(object sender, EventArgs e)
        {
            if (sfdExcel.ShowDialog() == DialogResult.OK)
            {
                string destFile = sfdExcel.FileName;
                File.Copy(exelFile, destFile, true);

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(destFile);
                Microsoft.Office.Interop.Excel.Worksheet sheet = excel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                //range of used cells
                //Microsoft.Office.Interop.Excel.Range range = sheet.UsedRange;

                sheet.Range["C4"].Value = "D/001/HHNS/18/000001";
                sheet.Range["C5"].Value = "Employee Test";
                sheet.Range["G5"].Value = "Male";
                sheet.Range["I5"].Value = "20";
                wb.Close(true, Type.Missing, Type.Missing);
                excel.Quit();
            }
        }
    }
}
