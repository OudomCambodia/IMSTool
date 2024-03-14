using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using ClosedXML.Excel;
using Microsoft.SharePoint.Client;
using System.IO;
using System.Net;
using System.Security;

namespace Testing
{
    public static class My_DataTable_Extensions
    {

        public static System.CodeDom.Compiler.TempFileCollection tempfile = new System.CodeDom.Compiler.TempFileCollection();

        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="DataTable">Source DataTable</param>
        /// <param name="ExcelFilePath">Path to result file name</param>
        // Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
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
                Excel.DisplayAlerts = false;

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

                //for (int j = 0; j < RowsCount; j++)
                //    for (int i = 0; i < ColumnsCount; i++)
                //        Cells[j, i] = DataTable.Rows[j][i];

                //format all columns to string
                Worksheet.Columns.NumberFormat = "@";
                Worksheet.Rows.NumberFormat = "";

                for (int r = 0; r < RowsCount; r++)
                {
                    DataRow dr = DataTable.Rows[r];
                    for (int c = 0; c < ColumnsCount; c++)
                    {
                        if (dr[c].ToString().Contains("="))
                            Cells[r, c] = dr[c].ToString().Replace("=", "'=");
                        else
                            Cells[r, c] = dr[c];
                    }
                }

                Worksheet.Application.ActiveWindow.SplitRow = 1;
                Worksheet.Application.ActiveWindow.FreezePanes = true;
                Worksheet.Columns.AutoFit();

                //ACC_HANDLER	AGENT	AGENT_NAME	INSUREDCODE	INSUREDNAME	UW_YEAR	POLICY_NO

                //Microsoft.Office.Interop.Excel.Range BodyRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount+1, ColumnsCount]));
                //BodyRange.Value = Cells;


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)Worksheet.Cells[2, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)Worksheet.Cells[1 + RowsCount, ColumnsCount];
                Microsoft.Office.Interop.Excel.Range range = Worksheet.get_Range(c1, c2);
                range.Value = Cells;


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

       

        public static void ExportToExcelXML(this System.Data.DataTable dt, string ExcelFilePath = null)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;

                ////using ClosedXML package
                IXLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Export Sheet");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text


                int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                //Set Header with DataTable dt Column Name
                for (int i = 0; i < ColumnsCount; i++)
                {
                    var cell = ws.Cell(1, i + 1);      //+1 cuz it start from 1
                    cell.Value = dt.Columns[i].ColumnName;
                    //Style Format on Header
                    cell.Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    cell.Style.Font.Bold = true;
                    //
                }
                //

                //Set Table Data After Header
                for (int r = 0; r < RowsCount; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < ColumnsCount; c++)
                    {
                        //ws.Cell(r + 2, c + 1).Value = dr[c].ToString();    //+2 cuz it start from second row after header row
                        ws.Cell(r + 2, c + 1).SetValue(dr[c].ToString());    //+2 cuz it start from second row after header row
                    }
                }
                //

                // Freeze top row
                ws.SheetView.FreezeRows(1);

                ws.Columns().AdjustToContents();


                using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) //create stream to store workbook data
                {
                    wb.SaveAs(ms);

                    string filePath = "";

                    if (string.IsNullOrEmpty(ExcelFilePath))
                    {
                        tempfile = new System.CodeDom.Compiler.TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                        tempfile.KeepFiles = false; //will be used when dispose tempfile
                        filePath = tempfile.AddExtension("xlsx"); //add extension to the created Temporary File
                    }
                    else
                    {
                        filePath = ExcelFilePath;
                    }

                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
                    {
                        fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }

                    if (string.IsNullOrEmpty(ExcelFilePath))
                        System.Diagnostics.Process.Start(filePath); //Open that File
                    else
                        Msgbox.Show("Excel file saved!");
                }

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show("Export Excel XML: " + ex.Message);
            }
        }

        public static void ExportToExcelXMLDataSet(this System.Data.DataSet ds, string ExcelFilePath = null)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var k = 0;
                IXLWorkbook wb = new XLWorkbook();

                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    k++;
                    ////using ClosedXML package
                    IXLWorksheet ws = wb.Worksheets.Add("Export Sheet" + k.ToString());
                    ws.DataType = XLDataType.Text; //Set all cells datatype as Text


                    int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                    //Set Header with DataTable dt Column Name
                    for (int i = 0; i < ColumnsCount; i++)
                    {
                        var cell = ws.Cell(1, i + 1);      //+1 cuz it start from 1
                        cell.Value = dt.Columns[i].ColumnName;
                        //Style Format on Header
                        cell.Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        cell.Style.Font.Bold = true;
                        //
                    }
                    //

                    //Set Table Data After Header
                    for (int r = 0; r < RowsCount; r++)
                    {
                        DataRow dr = dt.Rows[r];
                        for (int c = 0; c < ColumnsCount; c++)
                        {
                            //ws.Cell(r + 2, c + 1).Value = dr[c].ToString();    //+2 cuz it start from second row after header row
                            ws.Cell(r + 2, c + 1).SetValue(dr[c].ToString());    //+2 cuz it start from second row after header row
                        }
                    }
                    //
                }

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) //create stream to store workbook data
                {
                    wb.SaveAs(ms);

                    string filePath = "";

                    if (string.IsNullOrEmpty(ExcelFilePath))
                    {
                        tempfile = new System.CodeDom.Compiler.TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                        tempfile.KeepFiles = false; //will be used when dispose tempfile
                        filePath = tempfile.AddExtension("xlsx"); //add extension to the created Temporary File
                    }
                    else
                    {
                        filePath = ExcelFilePath;
                    }

                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
                    {
                        fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }

                    if (string.IsNullOrEmpty(ExcelFilePath))
                        System.Diagnostics.Process.Start(filePath); //Open that File
                    else
                        Msgbox.Show("Excel file saved!");
                }

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show("Export Excel XML: " + ex.Message);
            }
        }


        public static void ExportToExcelXMLSharepoint(this System.Data.DataTable dt, string fname, string ExcelFilePath = null)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;

                ////using ClosedXML package
                IXLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Export Sheet");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text


                int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                //Set Header with DataTable dt Column Name
                for (int i = 0; i < ColumnsCount; i++)
                {
                    var cell = ws.Cell(1, i + 1);      //+1 cuz it start from 1
                    cell.Value = dt.Columns[i].ColumnName;
                    //Style Format on Header
                    cell.Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    cell.Style.Font.Bold = true;
                    //
                }
                //

                //Set Table Data After Header
                for (int r = 0; r < RowsCount; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < ColumnsCount; c++)
                    {
                        //ws.Cell(r + 2, c + 1).Value = dr[c].ToString();    //+2 cuz it start from second row after header row
                        ws.Cell(r + 2, c + 1).SetValue(dr[c].ToString());    //+2 cuz it start from second row after header row
                    }
                }
                //

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) //create stream to store workbook data
                {
                    wb.SaveAs(ms);

                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + fname + ".xlsx";

                    //tempfile = new System.CodeDom.Compiler.TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                    //tempfile.KeepFiles = false; //will be used when dispose tempfile
                    //filePath = tempfile.AddExtension("xlsx"); //add extension to the created Temporary File



                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
                    {
                        fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    using (var ctx = new ClientContext("https://forteinsurancegroup.sharepoint.com/sites/forteinsurance"))
                    {
                        SecureString passWord = new SecureString();

                        foreach (char c in "Akira#123".ToCharArray()) passWord.AppendChar(c);

                        ctx.Credentials = new SharePointOnlineCredentials("pichponleur@forteinsurance.com", passWord);

                        CommonFunctions.UploadFile(ctx, ExcelFilePath, filePath);

                    }
                    if (string.IsNullOrEmpty(ExcelFilePath))
                        System.Diagnostics.Process.Start(filePath); //Open that File
                    else
                        Msgbox.Show("Excel file saved!");

                }

                Cursor.Current = Cursors.AppStarting;

            }
            catch (Exception ex)
            {
                Msgbox.Show("Export Excel XML: " + ex.Message);
            }

        }



        public static void ExportToExcelXML(this System.Data.DataSet ds, string ExcelFilePath = null)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;

                ////using ClosedXML package
                IXLWorkbook wb = new XLWorkbook();
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    IXLWorksheet ws = wb.Worksheets.Add(dt.TableName);
                    ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                    int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                    //Set Header with DataTable dt Column Name
                    for (int i = 0; i < ColumnsCount; i++)
                    {
                        var cell = ws.Cell(1, i + 1);      //+1 cuz it start from 1
                        cell.Value = dt.Columns[i].ColumnName;
                        //Style Format on Header
                        cell.Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        cell.Style.Font.Bold = true;
                        //
                    }
                    //

                    //Set Table Data After Header
                    for (int r = 0; r < RowsCount; r++)
                    {
                        DataRow dr = dt.Rows[r];
                        for (int c = 0; c < ColumnsCount; c++)
                        {
                            //ws.Cell(r + 2, c + 1).Value = dr[c].ToString();    //+2 cuz it start from second row after header row
                            ws.Cell(r + 2, c + 1).SetValue(dr[c].ToString());    //+2 cuz it start from second row after header row
                        }
                    }
                    //
                }

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) //create stream to store workbook data
                {
                    wb.SaveAs(ms);

                    string filePath = "";

                    if (string.IsNullOrEmpty(ExcelFilePath))
                    {
                        tempfile = new System.CodeDom.Compiler.TempFileCollection(); //this will create Temporary File, re-initailized it will create new file everytime 
                        tempfile.KeepFiles = false; //will be used when dispose tempfile
                        filePath = tempfile.AddExtension("xlsx"); //add extension to the created Temporary File
                    }
                    else
                    {
                        filePath = ExcelFilePath;
                    }

                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
                    {
                        fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }

                    if (string.IsNullOrEmpty(ExcelFilePath))
                        System.Diagnostics.Process.Start(filePath); //Open that File
                    else
                        Msgbox.Show("Excel file saved!");
                }

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show("Export Excel XML: " + ex.Message);
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

        public static System.Data.DataSet ConvertExcelToDataSetXML(string path, string[] columnToText = null)
        {
            if (path.EndsWith(".xls"))
            {
                var wb = new Aspose.Cells.Workbook(path);
                var newpath = path.Replace(".xls", ".xlsx");
                wb.Save(newpath);
                path = newpath;
            }



            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                foreach (var workSheet in workBook.Worksheets)
                {
                    if (workSheet.Name == "Evaluation Warning")
                        continue;



                    dt = new System.Data.DataTable();
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //check worksheet for info row (merge) then remove
                        if (row.IsMerged())
                        {
                            continue;
                        }
                        //var range = row.Cell(1).MergedRange().RangeAddress;
                        //if (range.ColumnSpan > 1 || range.RowSpan > 1)
                        //{
                        //    continue;
                        //}
                        //



                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                if (!string.IsNullOrEmpty(cell.Value.ToString()))
                                {
                                    dt.Columns.Add(cell.Value.ToString());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            firstRow = false;
                        }
                        else
                        {
                            int i = 0;
                            DataRow toInsert = dt.NewRow();
                            foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                            {
                                try
                                {
                                    var cellName = cell.Address.ToString();
                                    if (columnToText.Count() > 0)
                                    {
                                        for (int j = 0; j < columnToText.Count(); j++)
                                        {
                                            string cellText = columnToText[j];

                                            if (cellName.StartsWith(cellText))
                                            {
                                                toInsert[i] = cell.RichText.ToString();
                                            }
                                            else
                                            {
                                                toInsert[i] = cell.Value.ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        toInsert[i] = cell.Value.ToString();
                                    }
                                }
                                catch (Exception ex)
                                {



                                }
                                i++;
                            }
                            dt.Rows.Add(toInsert);
                        }
                    }



                    dt.TableName = workSheet.Name;

                    ds.Tables.Add(dt.Copy());
                }
            }
            return ds;
        }
    }
}
