using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using Aspose.Cells;
using System.IO;
using ExcelDataReader;


namespace Testing.Forms
{
    public static class TableExtension
    {
        public static void ExportToExcel(this System.Data.DataTable DataTable, string ExcelFilePath = null)
        {
            try
            {
                int ColumnsCount;

                if (DataTable == null || (ColumnsCount = DataTable.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

                object[] Header = new object[ColumnsCount];

                // column headings               
                for (int i = 0; i < ColumnsCount; i++)
                    Header[i] = DataTable.Columns[i].ColumnName;

                Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, ColumnsCount]));
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                HeaderRange.Font.Bold = true;

                // DataCells
                int RowsCount = DataTable.Rows.Count;
                object[,] Cells = new object[RowsCount, ColumnsCount];

                for (int j = 0; j < RowsCount; j++)
                    for (int i = 0; i < ColumnsCount; i++)
                        Cells[j, i] = DataTable.Rows[j][i].ToString() ;

                //format all columns to string
                Worksheet.Columns.NumberFormat = "@";

                //ACC_HANDLER	AGENT	AGENT_NAME	INSUREDCODE	INSUREDNAME	UW_YEAR	POLICY_NO


                Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        Worksheet.SaveAs(ExcelFilePath);
                        Excel.Quit();
                        //   System.Windows..Show("Excel file saved!");
                        System.Windows.Forms.MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    Excel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

        

        public static System.Data.DataTable ConvertExcelToDataTableApose(string FileName,bool trim = false)
        {
            // Load the Excel file using the Workbook class
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(FileName);

            // Get the worksheet you want to export
            Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

            // Export data to a DataTable using the Worksheet.Cells.ExportDataTable method
            System.Data.DataTable dataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);

            // Use the DataTable as the data source
            return dataTable;
        }
        public static System.Data.DataTable ConvertExcelToDataTableAposeV1(string FileName, bool trim = false)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

        // Load the Excel file
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(FileName);
        Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0]; // Assuming the data is in the first worksheet

        // Adding columns
        int columnCount = worksheet.Cells.MaxDataColumn + 1;
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            string columnName = worksheet.Cells[0, columnIndex].StringValue;
            dataTable.Columns.Add(columnName);
        }

        // Adding data
        int rowCount = worksheet.Cells.MaxDataRow + 1;
        for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
        {
            DataRow dataRow = dataTable.NewRow();
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                dataRow[columnIndex] = worksheet.Cells[rowIndex, columnIndex].Value;
            }
            dataTable.Rows.Add(dataRow);
        }

        return dataTable;
    }
        
    }
}
