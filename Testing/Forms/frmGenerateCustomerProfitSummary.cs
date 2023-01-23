using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Testing.Forms
{
    public partial class frmGenerateCustomerProfitSummary : Form
    {
        private System.CodeDom.Compiler.TempFileCollection tempfile = new System.CodeDom.Compiler.TempFileCollection();

        private CRUD crud = new CRUD();
        private DataTable finalDatatable = new DataTable();
        private DataTable finalColTotalPremium = new DataTable();
        private DataTable finalColTotalClaim = new DataTable();

        public frmGenerateCustomerProfitSummary()
        {
            InitializeComponent();
        }

        private void frmGenerateCustomerProfitSummary_Load(object sender, EventArgs e)
        {
            BindComboBox();
        }

        private void BindComboBox()
        {
            DataRow dr;
            string SQLcombox = "select GRP_CODE,GRP_DESCRIPTION from uw_r_groups order by GRP_CODE,GRP_DESCRIPTION";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            cboGroupCustomer.ValueMember = "GRP_CODE";
            cboGroupCustomer.DisplayMember = "GRP_DESCRIPTION";
            cboGroupCustomer.DataSource = dtCombox;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var grpCode = string.Empty;

                if (cboGroupCustomer.SelectedIndex == 0)
                {
                    var dtGrpCode = crud.ExecQuery("select cus_grp_code from uw_m_customers where cus_code = '" + txtCusCode.Text.Trim().ToUpper() + "'");
                    if (dtGrpCode.Rows.Count > 0)
                    {
                        grpCode = dtGrpCode.Rows[0][0].ToString();
                        if (string.IsNullOrEmpty(grpCode))
                            grpCode = "N/A";
                    }
                }
                else
                {
                    grpCode = cboGroupCustomer.SelectedValue.ToString();
                }

                string cusCode = string.IsNullOrEmpty(txtCusCode.Text.Trim()) ? null : txtCusCode.Text.Trim().ToUpper();

                string[] Key = new string[] { "p_cus_code", "p_grp_code" };
                string[] Values = new string[] { cusCode, grpCode }; //C000028640

                var dtCustomerProfit = crud.ExecSP_OutPara("SP_CUSTOMER_PROFITABILITY", Key, Values);

                if (dtCustomerProfit == null || dtCustomerProfit.Rows.Count <= 0)
                {
                    Msgbox.Show("No data found");
                    Cursor = Cursors.Arrow;
                    return;
                }

                var columnCount = dtCustomerProfit.Columns.Count;
                var columnNames = dtCustomerProfit.Columns;

                // add 0 to row
                dtCustomerProfit = dtCustomerProfit.AsEnumerable().Select(row =>
                {
                    DataRow newrow = dtCustomerProfit.NewRow();
                    newrow.ItemArray = row.ItemArray.Select(i => (string.IsNullOrEmpty(i.ToString()) ? "0" : i)).ToArray();
                    return newrow;
                }).CopyToDataTable();

                // get distinct product class without product class + _CLAIM
                var product = dtCustomerProfit.AsEnumerable().Select(r => r.Field<string>("PRODUCT")).ToArray();
                var tProduct = product.Select(x => x.Replace("_CLAIM", string.Empty)).ToArray();
                var dProduct = tProduct.Distinct().ToList();

                var isClone = false;

                for (int i = 0; i < dProduct.Count(); i++)
                {
                    // get each product class
                    DataView filter = dtCustomerProfit.DefaultView;
                    filter.RowFilter = "PRODUCT like '%" + dProduct[i] + "%'";
                    var dtEachProClass = filter.ToTable();

                    if (!isClone)
                    {
                        finalDatatable = dtEachProClass.Clone();
                        finalColTotalPremium = dtEachProClass.Clone();
                        finalColTotalClaim = dtEachProClass.Clone();
                        isClone = true;
                    }
                    var ratios = string.Empty;

                    // check if product class has both premium and claim
                    if (dtEachProClass.Rows.Count > 1)
                    {
                        var totalRowPremium = 0.00M;
                        var totalRowClaim = 0.00M;
                        for (int j = 0; j < columnCount; j++)
                        {
                            if (j >= 1)
                            {
                                // calculate each product class ratio
                                var claim = string.IsNullOrEmpty(dtEachProClass.Rows[1][j].ToString()) ? 0 : Convert.ToDecimal(dtEachProClass.Rows[1][j].ToString());
                                var premium = string.IsNullOrEmpty(dtEachProClass.Rows[0][j].ToString()) ? 0 : Convert.ToDecimal(dtEachProClass.Rows[0][j].ToString());
                                var ratio = 0.00M;

                                totalRowPremium += premium;
                                totalRowClaim += claim;

                                if (claim > 0 && premium > 0)
                                {
                                    ratio = claim / premium;
                                }
                                else
                                {
                                    ratio = 0.00M;
                                }
                                ratios += string.Concat(ratio.ToString("0.###"), "*");
                            }
                        }
                        // add text to column UWY in each row
                        dtEachProClass.Rows[0]["UWY"] = "PREMIUM";
                        dtEachProClass.Rows[1]["UWY"] = "CLAIM";

                        // add totalRowPremium and totalRowClaim to column TOTAL in each row
                        dtEachProClass.Rows[0]["TOTAL"] = totalRowPremium.ToString("0.###");
                        dtEachProClass.Rows[1]["TOTAL"] = totalRowClaim.ToString("0.###"); ;

                        var sRatio = ratios.Split('*');

                        // add Ratio row to dtEachProClass
                        var ratioRow = dtEachProClass.NewRow();
                        ratioRow["PRODUCT"] = string.Empty;
                        ratioRow["UWY"] = "RATIO%";

                        // add Ratio value to each column (2015, 2016, 2017, ...)
                        for (var m = 0; m < columnNames.Count; m++)
                        {
                            if (columnNames[m].ToString().Equals("PRODUCT") || columnNames[m].ToString().Equals("UWY"))
                                continue;
                            ratioRow[columnNames[m].ToString()] = Convert.ToDecimal(sRatio[m - 1]);
                        }

                        // calculate totalRatio and add to dtEachProClass
                        if (totalRowClaim > 0 && totalRowPremium > 0)
                            ratioRow["TOTAL"] = Convert.ToDecimal(totalRowClaim / totalRowPremium).ToString("0.###");
                        else
                            ratioRow["TOTAL"] = "0";

                        dtEachProClass.Rows.Add(ratioRow);
                    }
                    else // if product class has only premium
                    {
                        // add zero value to each product class claim and ratio
                        var totalRowPremium = 0.00M;
                        for (int j = 0; j < columnCount; j++)
                        {
                            if (j >= 1)
                            {
                                var premium = string.IsNullOrEmpty(dtEachProClass.Rows[0][j].ToString()) ? 0 : Convert.ToDecimal(dtEachProClass.Rows[0][j].ToString());
                                totalRowPremium += premium;
                            }
                        }
                        dtEachProClass.Rows[0]["UWY"] = "PREMIUM";
                        dtEachProClass.Rows[0]["TOTAL"] = totalRowPremium.ToString("0.###");
                        for (int k = 0; k <= 1; k++)
                        {
                            var row = dtEachProClass.NewRow();
                            row["PRODUCT"] = dProduct[i] + (k == 0 ? "_CLAIM" : "_RATIO");
                            row["UWY"] = k == 0 ? "CLAIM" : "RATIO%";
                            foreach (var columnName in columnNames)
                            {
                                if (columnName.ToString().Equals("PRODUCT") || columnName.ToString().Equals("UWY"))
                                    continue;

                                row[columnName.ToString()] = 0;
                            }
                            dtEachProClass.Rows.Add(row);
                        }
                    }

                    // add each product class with calculated ratio, total premium, total claim and total ratio to final datatable
                    foreach (DataRow dataRow in dtEachProClass.Rows)
                    {
                        if (dataRow.IsNull(0))
                            continue;
                        else
                            finalDatatable.ImportRow(dataRow);
                    }

                    // add finalColTotalPremium to finalColTotalPremium datatable
                    DataView finalFilter = finalDatatable.DefaultView;
                    finalFilter.RowFilter = "PRODUCT = '" + dProduct[i] + "'";
                    finalColTotalPremium.ImportRow(finalFilter.ToTable().Rows[0]);

                    // add finalColTotalCLaim to finalColTotalCLaim datatable
                    DataView finalClaimFilter = finalDatatable.DefaultView;
                    finalClaimFilter.RowFilter = "PRODUCT = '" + dProduct[i] + "_CLAIM'";
                    finalColTotalClaim.ImportRow(finalClaimFilter.ToTable().Rows[0]);
                }

                // add and calulate Premium,Claim and Ratio in TOTAL row
                DataRow drTotalPremium = finalDatatable.NewRow();
                drTotalPremium["PRODUCT"] = "TOTAL";
                drTotalPremium["UWY"] = "PREMIUM";
                foreach (var columnName in columnNames)
                {
                    if (columnName.ToString().Equals("PRODUCT") || columnName.ToString().Equals("UWY"))
                        continue;

                    string totalPremium = string.Empty;
                    totalPremium = Convert.ToDecimal(finalColTotalPremium.Compute("SUM([" + columnName.ToString() + "])", "")).ToString("0.###");
                    drTotalPremium[columnName.ToString()] = totalPremium;
                }
                finalDatatable.Rows.Add(drTotalPremium);

                DataRow drTotalClaim = finalDatatable.NewRow();
                drTotalClaim["PRODUCT"] = string.Empty;
                drTotalClaim["UWY"] = "CLAIM";
                foreach (var columnName in columnNames)
                {
                    if (columnName.ToString().Equals("PRODUCT") || columnName.ToString().Equals("UWY"))
                        continue;

                    string totalClaim = string.Empty;
                    totalClaim = Convert.ToDecimal(finalColTotalClaim.Compute("SUM([" + columnName.ToString() + "])", "")).ToString("0.###");
                    drTotalClaim[columnName.ToString()] = totalClaim;
                }
                finalDatatable.Rows.Add(drTotalClaim);

                DataRow drTotalRatio = finalDatatable.NewRow();
                drTotalRatio["PRODUCT"] = string.Empty;
                drTotalRatio["UWY"] = "RATIO%";
                var dt = finalDatatable.Clone();
                dt.ImportRow(drTotalPremium);
                dt.ImportRow(drTotalClaim);
                string totalRatios = string.Empty;

                for (int j = 0; j < columnCount; j++)
                {
                    if (j >= 2)
                    {
                        var claim = string.IsNullOrEmpty(dt.Rows[1][j].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[1][j].ToString());
                        var premium = string.IsNullOrEmpty(dt.Rows[0][j].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0][j].ToString());
                        var ratio = 0.00M;

                        if (claim > 0 && premium > 0)
                        {
                            ratio = claim / premium;
                        }
                        else
                        {
                            ratio = 0.00M;
                        }
                        totalRatios += string.Concat(ratio.ToString("0.###"), "*");
                    }
                }
                var totalRatio = totalRatios.Split('*');
                for (var m = 0; m < columnNames.Count; m++)
                {
                    if (columnNames[m].ToString().Equals("PRODUCT") || columnNames[m].ToString().Equals("UWY"))
                        continue;
                    drTotalRatio[columnNames[m].ToString()] = Convert.ToDecimal(totalRatio[m - 2]);
                }

                finalDatatable.Rows.Add(drTotalRatio);

                for (int i = 0; i < finalDatatable.Rows.Count; i++)
                {
                    string columnValue = finalDatatable.Rows[i][0].ToString();
                    if (columnValue.Contains("_CLAIM") || columnValue.Contains("_RATIO"))
                    {
                        finalDatatable.Rows[i][0] = string.Empty;
                    }
                }
                finalDatatable.Columns[0].ColumnName = " ";
                finalDatatable.AcceptChanges();

                GenerateSummaryReport(finalDatatable);

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }
        }

        private void GenerateSummaryReport(DataTable dt, string ExcelFilePath = null)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var cus = string.Empty;
                var byCusGroup = false;
                
                if (!string.IsNullOrEmpty(txtCusCode.Text.Trim()))
                {
                    cus = crud.ExecQuery("select nvl(cus_indv_surname, cus_corp_name) CUSNAME from uw_m_customers where cus_code = '"+ txtCusCode.Text.Trim().ToUpper() +"'").Rows[0][0].ToString();
                }

                if (string.IsNullOrEmpty(txtCusCode.Text.Trim()) && cboGroupCustomer.SelectedIndex != 0)
                {
                    byCusGroup = true;
                    cus = crud.ExecQuery("select grp_description from uw_r_groups where grp_code = '"+ cboGroupCustomer.SelectedValue.ToString() +"'").Rows[0][0].ToString();
                }

                IXLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Summary Report");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                ws.Cell(1, 2).SetValue(byCusGroup ? "CUSTOMER GROUP" : "CUSTOMER");
                ws.Cell(1, 2).Style.Font.FontSize = 9f;
                ws.Cell(1, 2).Style.Font.FontName = "Century Gothic";
                ws.Cell(1, 2).Style.Font.Bold = true;

                ws.Cell(1, ColumnsCount).SetValue(cus);
                ws.Cell(1, ColumnsCount).Style.Font.FontSize = 9f;
                ws.Cell(1, ColumnsCount).Style.Font.FontName = "Century Gothic";
                ws.Cell(1, ColumnsCount).Style.Font.Bold = true;

                ws.Cell(2, 2).SetValue("REPORT DATE");
                ws.Cell(2, 2).Style.Font.FontSize = 9f;
                ws.Cell(2, 2).Style.Font.FontName = "Century Gothic";
                ws.Cell(2, 2).Style.Font.Bold = true;

                ws.Cell(2, ColumnsCount).SetValue(DateTime.Now.ToString());
                ws.Cell(2, ColumnsCount).DataType = XLDataType.DateTime;
                ws.Cell(2, ColumnsCount).Style.DateFormat.Format = "dd-MMM-yy";
                ws.Cell(2, ColumnsCount).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(2, ColumnsCount).Style.Font.FontSize = 9f;
                ws.Cell(2, ColumnsCount).Style.Font.FontName = "Century Gothic";
                ws.Cell(2, ColumnsCount).Style.Font.Bold = true;

                ws.Cell(3, 2).SetValue("SUMMARY REPORT");
                ws.Cell(3, 2).Style.Font.FontSize = 20f;
                ws.Cell(3, 2).Style.Font.FontName = "Century Gothic";
                ws.Cell(3, 2).Style.Font.Bold = true;
                ws.Cell(3, 2).Style.Font.FontColor = XLColor.Green;

                ws.Cell(4, 2).SetValue("");

                //Set Header with DataTable dt Column Name
                for (int i = 0; i < ColumnsCount; i++)
                {
                    var cell = ws.Cell(5, i + 1); //5 means start adding from row 5, +1 cuz it starts from column 1
                    cell.Value = dt.Columns[i].ColumnName;

                    //Style Format on Header
                    if (i == 0)
                        cell.Style.Fill.SetBackgroundColor(XLColor.White);
                    else
                        cell.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#18345c"));

                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Font.Bold = true;

                    if (i == 0)
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    else if (i == 1)
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    else
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                }
                for (int j = 1; j < RowsCount + 1; j++)
                {
                    var cell = ws.Cell(j, 1);
                    cell.Style.Font.Bold = true;
                }

                int formatRowCount = 0; // count each 3 rows to add orange background color
                bool isSet = false;

                //Set Table Data After Header
                for (int r = 0; r < RowsCount; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < ColumnsCount; c++)
                    {
                        ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString()); //+6 cuz it starts from sixth row after Summary Report text
                        ws.Cell(r + 6, c + 1).Style.NumberFormat.Format = "#,##0.00";

                        if (c > 1)
                        {
                            decimal numericValue = 0;
                            bool isNumber = decimal.TryParse(dr[c].ToString(), out numericValue);
                            if (isNumber && numericValue > 0)
                            {
                                ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                                ws.Cell(r + 6, c + 1).DataType = XLDataType.Number;
                                ws.Cell(r + 6, c + 1).Style.NumberFormat.Format = "#,##0.00";
                            }
                            else
                                ws.Cell(r + 6, c + 1).SetValue("-");
                        }
                        if (c == 0)
                            ws.Cell(r + 6, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        else if (c == 1)
                            ws.Cell(r + 6, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        else
                            ws.Cell(r + 6, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        //(!isSet && formatRowCount == 0) => first row after row header, formatRowCount == 3 => add bg color to each 3 rows
                        if ((!isSet && formatRowCount == 0) || formatRowCount == 3)
                        {
                            var colName = ws.Cell(r + 6, c + 1).CachedValue.ToString();

                            if (colName.Equals("TOTAL"))
                            {
                                for (int i = 0; i < ColumnsCount; i++)
                                {
                                    decimal numericValue = 0;
                                    bool isNumber = decimal.TryParse(dr[i].ToString(), out numericValue);
                                    if (isNumber && numericValue > 0)
                                    {
                                        ws.Cell(r + 6, i + 1).SetValue(dr[i].ToString());
                                        ws.Cell(r + 6, i + 1).DataType = XLDataType.Number;
                                        ws.Cell(r + 6, i + 1).Style.NumberFormat.Format = "#,##0.00";
                                        ws.Cell(r + 6, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    }
                                    else if (i == 0 || i == 1)
                                    {
                                        ws.Cell(r + 6, i + 1).SetValue(i == 0 ? "TOTAL" : "PREMIUM");
                                        if (i == 1)
                                            ws.Cell(r + 6, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                    }
                                    else
                                    {
                                        ws.Cell(r + 6, i + 1).SetValue("-");
                                        ws.Cell(r + 6, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    }

                                    ws.Cell(r + 6, i + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                                    ws.Cell(r + 6, i + 1).Style.Font.Bold = true;
                                }
                                break;
                            }
                            else
                                ws.Cell(r + 6, c + 1).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#fed8b1"));

                            formatRowCount = 0;
                            isSet = false;
                        }
                        if (c == ColumnsCount)
                            isSet = true;

                        // format Total CLaim Column
                        if (r == RowsCount - 1)
                        {
                            ws.Cell(r + 6, c + 1).Style.Font.FontColor = XLColor.Blue;
                            ws.Cell(r + 6, c + 1).Style.Font.Bold = true;
                        }

                        // format Total Ratio Column
                        if (r == RowsCount - 2)
                            ws.Cell(r + 6, c + 1).Style.Font.Bold = true;
                    }
                    formatRowCount++;
                }
                ws.PageSetup.SetPageOrientation(XLPageOrientation.Landscape);

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) //create stream to store workbook data
                {
                    wb.SaveAs(ms);

                    string filePath = "";

                    if (string.IsNullOrEmpty(ExcelFilePath))
                    {
                        tempfile = new System.CodeDom.Compiler.TempFileCollection
                        {
                            KeepFiles = false //will be used when dispose tempfile
                        }; //this will create Temporary File, re-initailized it will create new file everytime 
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
                        MessageBox.Show("Excel file saved!");
                }

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export Excel XML: " + ex.Message);
            }
        }
    }
}
