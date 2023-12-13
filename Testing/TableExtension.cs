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
                        Cells[j, i] = DataTable.Rows[j][i];

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

        public static System.Data.DataTable ConvertExcelToDataTable(string FileName, bool trim = false)
        {
            System.Data.DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataTable dt = dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                foreach (DataRow dr in dtResult.Rows)
                {
                    bool HasSth = false;
                    foreach (DataColumn dc in dtResult.Columns)
                    {
                        if (dr[dc.ColumnName].ToString().Trim() != "")
                        {
                            HasSth = true;
                            if (trim == true)
                            {
                                if (dc.DataType == typeof(string))
                                    dr.SetField<string>(dc, dr.Field<string>(dc).Trim());
                            }
                            else
                                break;
                        }
                    }

                    if (HasSth == false)
                        dr.Delete();
                }
                objConn.Close();
                return dtResult; //Returning Datatable  
            }
        }

        public static System.Data.DataTable ConvertExcelToDataTableV2(string FileName,bool trim = false)
        {
            // Create a file stream containing the Excel file to be opened
            FileStream fstream = new FileStream(FileName, FileMode.Open);

            // Instantiate a Workbook object
            //Opening the Excel file through the file stream
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(fstream);

            // Access the first worksheet in the Excel file
            Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

            // Export the contents of 2 rows and 2 columns starting from 1st cell to DataTable
            //System.Data.DataTable dataTable = worksheet.Cells.ExportDataTable(0,0,500,500,true);
            System.Data.DataTable dataTable = worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.Rows.Count, worksheet.Cells.Columns.Count, true);
            

            // Bind the DataTable with DataGrid
            //dataGridView1.DataSource = dataTable;

            // Close the file stream to free all resources
            fstream.Close();
            return dataTable;
        }
    }
}
