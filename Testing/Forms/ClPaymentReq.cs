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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Testing.Forms
{
    public partial class ClPaymentReq : Form

    {
        private System.CodeDom.Compiler.TempFileCollection tempfile = new System.CodeDom.Compiler.TempFileCollection();
        CRUD crud = new CRUD();
        int num;
        string datetemp = DateTime.Now.ToString("dd MMMM yyyy");
        DataTable dt;
        CheckBox checkboxHeader = new CheckBox();
        public string FullName = "SICL";
        DataGridViewRow SelectedRow = new DataGridViewRow();
        
        
        bool isChecked;


        public ClPaymentReq()
        {
            InitializeComponent();
        }

        

        private void ClPaymentReq_Load(object sender, EventArgs e)
        {
            this.dgvResult.ForeColor = System.Drawing.Color.Black;
            Cursor = Cursors.WaitCursor;

            DataTable dtProduct = crud.ExecQuery("select PRD_CODE,PRD_DESCRIPTION,PRD_CODE || ' - ' ||  PRD_DESCRIPTION as displayproducts from uw_m_products  where PRD_CLA_CODE in ('CLTY','MEDI')  and PRD_DESCRIPTION not like '%TEST%' group by PRD_CODE,PRD_DESCRIPTION order by  PRD_CODE");

            ((ListBox)chkProducts).DataSource = dtProduct;
            ((ListBox)chkProducts).ValueMember = "PRD_CODE";
            ((ListBox)chkProducts).DisplayMember =  "displayproducts";
            Cursor = Cursors.Arrow;
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbRequisitionNo.Text = "";
            dgvResult.DataSource = null;
            dgvResult.Columns.Clear();
            lblSel.Text = "";
            lbTotal.Text ="";
            checkboxHeader.Visible = false;
        }

        private void bnView_Click(object sender, EventArgs e)
        {
            try
            {
               
                Cursor.Current = Cursors.WaitCursor;

                string companyName = "";
                if (chkProducts.CheckedItems.Count != 0)
                {
                    foreach (object itemChecked in chkProducts.CheckedItems)
                    {
                        DataRowView castedItem = itemChecked as DataRowView;
                        //comapnyName += "'" + castedItem["PRD_CODE"].ToString() + "',";
                        companyName += castedItem["PRD_CODE"].ToString();
                        if (chkProducts.CheckedItems.IndexOf(itemChecked) != chkProducts.CheckedItems.Count - 1)
                            companyName += "|";

                    }
                    //companyName = "(" + comapnyName.Remove(comapnyName.Length - 1, 1) + ")";

                    if (tbRequisitionNo.Text == "")
                    {


                        string sp_type = "Cl_Requisition_payment";
                        string[] Keys = new string[] { "sp_type", "sp_requisition_no", "sp_date_from", "sp_date_to", "sp_prdcode" };
                        //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                        string[] Values = new string[] { sp_type, "", dtpDateFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpDateTo.Value.ToString("yyyy/MM/dd") + " 23:59:59", companyName };
                        dt = crud.ExecSP_OutPara("USER_CLAIM_REQUISITION_PAYMENT", Keys, Values);


                    }
                    else
                    {




                        string sp_type = "";
                        if (tbRequisitionNo.Text.ToUpper().ToString().Substring(0, 1) == "C")
                            sp_type = "Cl_Requisition_payment_bycl_no";
                        else
                            sp_type = "Cl_Requisition_payment_byreq_no";
                        string[] Keys = new string[] { "sp_type", "sp_requisition_no", "sp_date_from", "sp_date_to", "sp_prdcode", "sp_prdcode" };
                        //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                        string[] Values = new string[] { sp_type, tbRequisitionNo.Text.ToUpper().Trim(), "", "", "" };

                        dt = crud.ExecSP_OutPara("USER_CLAIM_REQUISITION_PAYMENT", Keys, Values);
                    }


                    Cursor.Current = Cursors.AppStarting;

                    if (dt.Rows.Count == 0)
                    {
                        Msgbox.Show("No data found!");
                    }
                    else
                    {
                        dgvResult.DataSource = null;
                        dgvResult.Columns.Clear();

                        DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                        //CheckBox chk = new CheckBox();
                        dgvResult.Columns.Add(CheckboxColumn);
                        dgvResult.DataSource = dt;

                        dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        DataGridViewColumn column = dgvResult.Columns[0];
                        column.Width = 35;
                        dgvResult.Columns[0].Resizable = DataGridViewTriState.False;
                        dgvResult.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                        // add checkbox header
                        Rectangle rect = dgvResult.GetCellDisplayRectangle(0, -1, true);
                        // set checkbox header to center of header cell. +1 pixel to position correctly.
                        rect.X = rect.Location.X + 10;
                        rect.Y = rect.Location.Y + 6;
                        rect.Width = rect.Size.Width;
                        rect.Height = rect.Size.Height;

                        checkboxHeader.Checked = false;
                        checkboxHeader.Visible = true;
                        checkboxHeader.Name = "checkboxHeader";
                        checkboxHeader.Size = new Size(15, 15);
                        checkboxHeader.Location = rect.Location;
                        checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                        dgvResult.Controls.Add(checkboxHeader);

                        for (int i = 1; i < dgvResult.Columns.Count; i++)
                        {
                            dgvResult.Columns[i].ReadOnly = true;
                            //if (i == 2)
                            //    dgvResult.Columns[i].Visible = false;
                            //if (i==3) 

                        }
                        //dgvResult.Columns[2].Visible = false;
                        //dgvResult.Columns[3].Visible = false;
                        //dgvResult.Columns[4].Visible = false;
                    }
                    lbTotal.Text = dgvResult.Rows.Count.ToString();
                    dgvResult.ClearSelection();

                }
                else
                {
                    Msgbox.Show("Please Select Products!");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show(ex.Message);
            }
        
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                for (int i = 0; i < dgvResult.RowCount; i++)
                {
                    dgvResult[0, i].Value = ((CheckBox)dgvResult.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dgvResult[0, i].Value;
                    CheckCount(isChecked);
                }
                lblSel.Text = num.ToString();
                dgvResult.EndEdit();


            }
            catch (Exception EX)
            {
                Msgbox.Show(EX.Message);
            }
        }
        private void CheckCount(bool isChecked)
        {
            if (isChecked)
                num++;
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {

            CommonFunctions.HighLightGrid(dgvResult);
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            num = 0;
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex != 0)
                return;


            if (dgvResult.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvResult.SelectedCells)
                {
                    dgvResult[0, dgvc.RowIndex].Value = true;

                }
                for (int i = 0; i < dgvResult.RowCount; i++)
                {
                    isChecked = (bool)dgvResult.Rows[i].Cells[0].EditedFormattedValue;

                    CheckCount(isChecked);
                }
                lblSel.Text = num.ToString();
            }
        }

        private void btnDirectBill_Click(object sender, EventArgs e)
        {
            DataTable dt1 = getGetDataTableFromDGV(dgvResult);
            //Send Datatable to the ws
            GenerateSummaryReport(dt1,1);
           
        }

        private void GenerateSummaryReport(DataTable dtcopy,int option, string ExcelFilePath = null)
        {
            
            try
            {
                IXLWorkbook wb = wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Claim Report");
                IXLWorksheet ws1 = wb.Worksheets.Add("MIS Report");
                IXLWorksheet ws2 = wb.Worksheets.Add("Peachtree Report");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                int RowsCount = dtcopy.Rows.Count, ColumnsCount = dtcopy.Columns.Count;
                if (RowsCount <= 0)
                {
                    Msgbox.Show("No record selected!");

                }
                else
                {
                    if (option == 1)
                    {
                        //First Sheet No Change
                        FirstSheetClaimReport(dtcopy, wb, ws, ColumnsCount, RowsCount,1);
                        //Second Sheet
                        SecondSheetDirectBilling(dtcopy, wb, ws1, ColumnsCount, RowsCount);
                        //
                        //Third Sheet
                        ThirdSheetDirectBilling(dtcopy, wb, ws2, ColumnsCount, RowsCount);
                        //
                    }
                    if (option == 2)
                    {
                        //First Sheet No Change
                        FirstSheetClaimReport(dtcopy, wb, ws, ColumnsCount, RowsCount,2);
                        //Second Sheet
                        SecondSheetNonDirectBilling(dtcopy, wb, ws1, ColumnsCount, RowsCount);
                        //
                        //Third Sheet
                        ThirdSheetNonDirectBilling(dtcopy, wb, ws2, ColumnsCount, RowsCount);
                        //
                    }

                   
                    
                    //convert Datatable to Excel xlxs
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
                    ///end of conversion to excel file
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export Excel XML: " + ex.Message);
            }

            
        }
        private void wbHeading(string[] Heading, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount)
        {
            #region --- Forte Heading ---

            //string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", "PAYMENT LIST TO ACCOUNTING (Direct Billing)", "Date:", "By:" };
            for (int i = 1; i <= Heading.Length; i++)
            {

                if (i < Heading.Length)
                {
                    ws.Cell(i, 1).SetValue(Heading[i - 1]);
                    ws.Cell(i, 1).Style.Font.FontSize = 16f;
                    ws.Cell(i, 1).Style.Font.FontName = "Arial Narrow";
                    ws.Cell(i, 1).Style.Font.Bold = true;
                    ws.Cell(i, 1).Style.Font.FontColor = XLColor.Black;
                }

                else
                {
                    ws.Cell(i - 1, i + 3).SetValue(Heading[i - 1]);
                    ws.Cell(i - 1, i + 3).Style.Font.FontSize = 16f;
                    ws.Cell(i - 1, i + 3).Style.Font.FontName = "Arial Narrow";
                    ws.Cell(i - 1, i + 3).Style.Font.Bold = true;
                    ws.Cell(i - 1, i + 3).Style.Font.FontColor = XLColor.Black;
                }

            }

            //Date Time Generate Report 
            ws.Cell(3, 2).SetValue(DateTime.Now.ToLongDateString());
            ws.Cell(3, 2).Style.Font.FontSize = 12f;
            ws.Cell(3, 2).Style.Font.FontName = "Arial Narrow";
            ws.Cell(3, 2).Style.Font.Bold = true;
            ws.Cell(3, 2).Style.Font.FontColor = XLColor.Green;

            //Username Generate the report
            ws.Cell(3, 8).SetValue(FullName.ToUpper());
            ws.Cell(3, 8).Style.Font.FontSize = 12f;
            ws.Cell(3, 8).Style.Font.FontName = "Arial Narrow";
            ws.Cell(3, 8).Style.Font.Bold = true;
            ws.Cell(3, 8).Style.Font.FontColor = XLColor.Green;
            #endregion
        }
        private DataTable getGetDataTableFromDGV(DataGridView dgv)
        {
            string status = "";
            int no = 1;

            DataTable dt1 = new DataTable();


            dt1.Columns.Add("No");
            dt1.Columns.Add("Forte Claim No");
            dt1.Columns.Add("Requesition No.");
            dt1.Columns.Add("TPA Claim No.");
            dt1.Columns.Add("Requisition_Narration");
            dt1.Columns.Add("Beneficiary_Name");
            dt1.Columns.Add("Insured Member");
            dt1.Columns.Add("Policyholder");
            dt1.Columns.Add("USD");
            dt1.Columns.Add("Bank");
            dt1.Columns.Add("Bank_AccNo.");
            

            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {
                        dt1.Rows.Add("", row.Cells["CLAIM_NO"].Value.ToString(), row.Cells["REQUISITION_NO"].Value.ToString(), row.Cells["TPA_CLAIM"].Value.ToString(), row.Cells["REQUISITION_NARRATION"].Value.ToString(), row.Cells["BENEFICIARY_NAME"].Value.ToString(), row.Cells["INSURED_MEMBER"].Value.ToString(), row.Cells["INSURED"].Value.ToString(), Convert.ToDouble(row.Cells["AMOUNT"].Value.ToString()), row.Cells["BANK_NAME"].Value.ToString(), row.Cells["BANK_ACCOUNT"].Value.ToString());
                    }
                }
            }
            //sort data in datatable before display in excel due to has to sum up by group of customer and total 
            DataView dv = dt1.DefaultView;
            dv.Sort = "BENEFICIARY_NAME ASC";
            dt1 = dv.ToTable();
            //as because the number of row after sort is not in order so need to adjust the value 
            foreach (DataRow r in dt1.Rows)
            {
                r[0] = no;
                no++;
            }
            return dt1;
        }

        private void btnNonDirect_Click(object sender, EventArgs e)
        {
            DataTable dt1 = getGetDataTableFromDGV(dgvResult);
            //Send Datatable to the ws
            GenerateSummaryReport(dt1,2);
        }

        
        private void FirstSheetClaimReport(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount,int option,string ExcelFilePath = null)
        {
            
            //Worksheet Heading
            string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", option == 1 ? "PAYMENT LIST TO ACCOUNTING (Direct Billing)" : "PAYMENT LIST TO ACCOUNTING (Non-Direct Billing)", "Date:", "By:" };
            wbHeading(Heading,wb,ws,ColumnsCount,RowsCount);
            //Set Header with DataTable dt Column Name
            for (int i = 0; i < ColumnsCount; i++)
            {
                var cell = ws.Cell(5, i + 1);      //+1 cuz it start from 1
                cell.Value = dtcopy.Columns[i].ColumnName;
                //Style Format on Header
                cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                cell.Style.Font.FontSize = 14f;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontName = "Arial Narrow";
                cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                //
            }
            //

            //Set Table Data After Header
            for (int r = 0; r < RowsCount; r++)
            {
                DataRow dr = dtcopy.Rows[r];
                for (int c = 0; c < ColumnsCount; c++)
                {
                    //ws.Cell(r + 2, c + 1).Value = dr[c].ToString();    //+2 cuz it start from second row after header row
                    //if(c==7)
                    //    ws.Cell(r + 7, c + 1).SetValue(Convert.ToDouble(dr[c].ToString()));
                    //else
                    ws.Cell(r + 7, c + 1).SetValue(dr[c].ToString());
                    ws.Cell(r + 7, c + 1).Style.Font.FontName = "Arial Narrow";
                    ws.Cell(r + 7, c + 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Cell(r + 7, c + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                     ws.Cell(r + 7, c + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                     ws.Cell(r + 7, c + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                }
                

            }
             
            Double countr = 0;
            for (int r = 0; r < RowsCount; r++)
            {

                if (countr == 0)
                    countr = Convert.ToDouble(ws.Cell(r + 7, 9).Value.ToString());

                if (ws.Cell(r + 7, 6).Value.ToString() == ws.Cell(r + 8, 6).Value.ToString())
                    countr += Convert.ToDouble(ws.Cell(r + 8, 9).Value.ToString());

                //need to check to insert one row group by beneficiary name
                if (ws.Cell(r + 7, 6).Value.ToString() != ws.Cell(r + 8, 6).Value.ToString())
                {
                    ws.Row(r + 7).InsertRowsBelow(2);
                    ws.Cell(r + 8, 9).SetValue(string.Format("{0:0.00}", countr));
                    //ws.Cell(r + 8, 8).Style.Fill.SetBackgroundColor(XLColor.CoolGrey);
                    ws.Cell(r + 8, 9).Style.Font.Bold = true;
                    ws.Cell(r + 8, 9).Style.Font.FontSize = 12f;
                    countr = 0;
                    r += 2;
                    RowsCount += 2; //need to add two rows as increase by two rows

                }
            }

            ws.Columns(2, 11).AdjustToContents();
            ws.SheetView.ZoomScale = 90;


            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            ws.PageSetup.FitToPages(1, 1);
            ws.PageSetup.PagesTall = 1;
            ws.PageSetup.PagesWide = 1;
            ws.PageSetup.PrintAreas.Clear();
            ws.PageSetup.Margins.Left = 0.5;
            ws.PageSetup.Margins.Right = 0.5;
        }

        private void SecondSheetDirectBilling(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount, string ExcelFilePath = null)
        {
            //Worksheet Heading
            string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", "PAYMENT LIST TO ACCOUNTING (Direct Billing)", "Date:", "By:" };
            wbHeading(Heading, wb, ws, ColumnsCount, RowsCount);
            //End of heading 
            DataTable dtMISReport = new DataTable();
            dtMISReport.Columns.Add("Date");
            dtMISReport.Columns.Add("Forte_Bank");
            dtMISReport.Columns.Add("TT_No.");
            dtMISReport.Columns.Add("Beneficiary_Name");
            dtMISReport.Columns.Add("Description");
            dtMISReport.Columns.Add("USD");
            dtMISReport.Columns.Add("Bank");
            dtMISReport.Columns.Add("BankAcc_No");
            int MISReportRC = dtMISReport.Rows.Count, MISReportCC = dtMISReport.Columns.Count;
            //Set heading after header of excel
            //Set Header with DataTable dt Column Name\var cell
            var cell = ws.Cell(5, 1);
            for (int i = 0; i < MISReportCC; i++)
            {

                    if (i >=6 && i<=7 )
                    {
                       if(i==6)
                        ws.Column(i).InsertColumnsAfter(3);
                        cell = ws.Cell(5, i + 4);
                        cell.Value = dtMISReport.Columns[i].ColumnName;
                        
                    }
                    else
                    {
                        cell = ws.Cell(5, i + 1);
                        cell.Value = dtMISReport.Columns[i].ColumnName;
                        
                    }

                    //Style Format on Header
                    cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                    cell.Style.Font.FontSize = 14f;
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.FontName = "Arial Narrow";
      
            }
            //
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (DataRow item in dtcopy.Rows)
            {
                DataRow newRow = dtMISReport.NewRow();
                newRow["Date"] = DateTime.Today.ToShortDateString();
                newRow["Forte_Bank"] = item["Bank"];
                newRow["TT_No."] = "";
                newRow["Beneficiary_Name"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower());
                //string a = item["TPA Claim No."].ToString().Split('(', ')')[1] == "" ? "" : item["TPA Claim No."].ToString().Split('(', ')')[1];
                newRow["Description"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower()) + " - " + "Medical " + item["Forte Claim No"].ToString().Substring(7, 3) + " To Panel Hospital " + Regex.Match(item["Requisition_Narration"].ToString(), @"\(([^)]*)\)").Groups[1].Value;
                newRow["USD"] = Convert.ToDouble(item["USD"].ToString());
                newRow["Bank"] = item["Bank"];
                newRow["BankAcc_No"] = item["Bank_AccNo."];
                dtMISReport.Rows.Add(newRow);
            }

            DataTable dt2 = dtMISReport.AsEnumerable()
                .GroupBy(r => new { Date = r["Date"], Forte_Bank = r["Forte_Bank"], TT_No = r["TT_No."], Beneficiary_Name = r["Beneficiary_Name"], Description = r["Description"], Bank = r["Bank"], BankAcc_No = r["BankAcc_No"] })
                .Select(g =>
                    {
                        var row = dtMISReport.NewRow();

                        row["Date"] = g.Key.Date;
                        row["Forte_Bank"] = g.Key.Forte_Bank;
                        row["TT_No."] = "";
                        row["Beneficiary_Name"] = g.Key.Beneficiary_Name;
                        row["Description"] = g.Key.Description;
                        row["USD"] = g.Sum(usd => Convert.ToDouble(usd.Field<string>("USD")));
                        row["Bank"] = g.Key.Bank;
                        row["BankAcc_No"] = g.Key.BankAcc_No;
                        

                    return row;

            }).CopyToDataTable();
            
            

            //Set Table Data After Header
            for (int r = 0; r < dt2.Rows.Count; r++)
            {
                DataRow dr = dt2.Rows[r];
                for (int c = 0; c < dt2.Columns.Count; c++)
                {
                    if (c >= 6 && c <= 8)
                    {
                        ws.Cell(r + 6, c + 4).SetValue(dr[c].ToString());
                        
                        
                    }
                    else
                    {
                        ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                         
                    }
                    
                    //ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                    IXLRange range = ws.Range(5, 1, 6 + dt2.Rows.Count, 3 + dt2.Columns.Count);
                    range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    range.Style.Font.FontName = "Arial Narrow";

                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.FitToPages(1, 1);
                    ws.PageSetup.PagesTall = 1;
                    ws.PageSetup.PagesWide = 1;
                    ws.PageSetup.PrintAreas.Clear();
                    ws.PageSetup.Margins.Left = 0.5;
                    ws.PageSetup.Margins.Right = 0.5;
                   
                }
                

            }
            ws.Columns(2, 15).AdjustToContents();
            ws.SheetView.ZoomScale = 90;
        }
        private void ThirdSheetDirectBilling(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount, string ExcelFilePath = null)
        {
            //Worksheet Heading
            string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", "PAYMENT LIST TO ACCOUNTING (Direct Billing)", "Date:", "By:" };
            wbHeading(Heading, wb, ws, ColumnsCount, RowsCount);
            //End of heading 
            DataTable dtMISReport = new DataTable();
            dtMISReport.Columns.Add("Date");
            dtMISReport.Columns.Add("Forte_Bank");
            dtMISReport.Columns.Add("TT_No.");
            dtMISReport.Columns.Add("Beneficiary_Name");
            dtMISReport.Columns.Add("Description");
            dtMISReport.Columns.Add("USD");
            dtMISReport.Columns.Add("Bank");
            dtMISReport.Columns.Add("BankAcc_No");
            int MISReportRC = dtMISReport.Rows.Count, MISReportCC = dtMISReport.Columns.Count;
            //Set heading after header of excel
            //Set Header with DataTable dt Column Name\var cell
            var cell = ws.Cell(5, 1);
            for (int i = 0; i < MISReportCC; i++)
            {

                if (i >= 6 && i <= 7)
                {
                    if (i == 6)
                        ws.Column(i).InsertColumnsAfter(3);
                    cell = ws.Cell(5, i + 4);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                     
                }
                else
                {
                    cell = ws.Cell(5, i + 1);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                }

                //Style Format on Header
                cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                cell.Style.Font.FontSize = 14f;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontName = "Arial Narrow";
                

            }
            //
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (DataRow item in dtcopy.Rows)
            {
                DataRow newRow = dtMISReport.NewRow();
                newRow["Date"] = DateTime.Today.ToShortDateString();
                newRow["Forte_Bank"] = item["Bank"];
                newRow["TT_No."] = "";
                newRow["Beneficiary_Name"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower());

                newRow["Description"] = "Medical " + item["Forte Claim No"].ToString().Substring(7, 3) + " To Panel Hospital " + Regex.Match(item["Requisition_Narration"].ToString(), @"\(([^)]*)\)").Groups[1].Value;
                newRow["USD"] = Convert.ToDouble(item["USD"].ToString());
                newRow["Bank"] = item["Bank"];
                newRow["BankAcc_No"] = item["Bank_AccNo."];
                dtMISReport.Rows.Add(newRow);
            }

            DataTable dt2 = dtMISReport.AsEnumerable()
                .GroupBy(r => new { Date = r["Date"], Forte_Bank = r["Forte_Bank"], TT_No = r["TT_No."], Beneficiary_Name = r["Beneficiary_Name"], Description = r["Description"], Bank = r["Bank"], BankAcc_No = r["BankAcc_No"] })
                .Select(g =>
                {
                    var row = dtMISReport.NewRow();

                    row["Date"] = g.Key.Date;
                    row["Forte_Bank"] = g.Key.Forte_Bank;
                    row["TT_No."] = "";
                    row["Beneficiary_Name"] = g.Key.Beneficiary_Name;
                    row["Description"] = g.Key.Description;
                    row["USD"] = g.Sum(usd => Convert.ToDouble(usd.Field<string>("USD")));
                    row["Bank"] = g.Key.Bank;
                    row["BankAcc_No"] = g.Key.BankAcc_No;


                    return row;

                }).CopyToDataTable();



            //Set Table Data After Header
            for (int r = 0; r < dt2.Rows.Count; r++)
            {
                DataRow dr = dt2.Rows[r];
                for (int c = 0; c < dt2.Columns.Count; c++)
                {
                    if (c >= 6 && c <= 8)
                    {
                        ws.Cell(r + 6, c + 4).SetValue(dr[c].ToString());

                    }
                    else
                    {
                        ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                    }

                    //ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                    IXLRange range = ws.Range(5, 1, 6 + dt2.Rows.Count, 3 + dt2.Columns.Count);
                    range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    range.Style.Font.FontName = "Arial Narrow";
                }

            }
            ws.Columns(2, 15).AdjustToContents();
            ws.SheetView.ZoomScale = 90;

            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            ws.PageSetup.FitToPages(1, 1);
            ws.PageSetup.PagesTall = 1;
            ws.PageSetup.PagesWide = 1;
            ws.PageSetup.PrintAreas.Clear();
            ws.PageSetup.Margins.Left = 0.5;
            ws.PageSetup.Margins.Right = 0.5;
        }

        private void SecondSheetNonDirectBilling(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount, string ExcelFilePath = null)
        {
            //Worksheet Heading
            string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", "PAYMENT LIST TO ACCOUNTING (Non-Direct Billing)", "Date:", "By:" };
            wbHeading(Heading, wb, ws, ColumnsCount, RowsCount);
            //End of heading 
            DataTable dtMISReport = new DataTable();
            dtMISReport.Columns.Add("Date");
            dtMISReport.Columns.Add("Forte_Bank");
            dtMISReport.Columns.Add("TT_No.");
            dtMISReport.Columns.Add("Beneficiary_Name");
            dtMISReport.Columns.Add("Forte_Claim_No");
            dtMISReport.Columns.Add("USD");
            dtMISReport.Columns.Add("Bank");
            dtMISReport.Columns.Add("BankAcc_No");
            int MISReportRC = dtMISReport.Rows.Count, MISReportCC = dtMISReport.Columns.Count;
            //Set heading after header of excel
            //Set Header with DataTable dt Column Name\var cell
            var cell = ws.Cell(5, 1);
            for (int i = 0; i < MISReportCC; i++)
            {

                if (i >= 6 && i <= 7)
                {
                    if (i == 6)
                        ws.Column(i).InsertColumnsAfter(3);
                    cell = ws.Cell(5, i + 4);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                    
                }
                else
                {
                    cell = ws.Cell(5, i + 1);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                    
                }

                //Style Format on Header
                cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                cell.Style.Font.FontSize = 14f;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontName = "Arial Narrow";
            }
            //
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (DataRow item in dtcopy.Rows)
            {
                DataRow newRow = dtMISReport.NewRow();
                newRow["Date"] = DateTime.Today.ToShortDateString();
                newRow["Forte_Bank"] = item["Bank"];
                newRow["TT_No."] = "";
                newRow["Beneficiary_Name"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower());
                //string a = item["TPA Claim No."].ToString().Split('(', ')')[1] == "" ? "" : item["TPA Claim No."].ToString().Split('(', ')')[1];
                newRow["Forte_Claim_No"] = item["Forte Claim No"].ToString() ;
                newRow["USD"] = Convert.ToDouble(item["USD"].ToString());
                newRow["Bank"] = item["Bank"];
                newRow["BankAcc_No"] = item["Bank_AccNo."];
                dtMISReport.Rows.Add(newRow);
            }

            DataTable dt2 = dtMISReport.AsEnumerable()
                .GroupBy(r => new { Date = r["Date"], Forte_Bank = r["Forte_Bank"], TT_No = r["TT_No."], Beneficiary_Name = r["Beneficiary_Name"], Description = r["Forte_Claim_No"], Bank = r["Bank"], BankAcc_No = r["BankAcc_No"] })
                .Select(g =>
                {
                    var row = dtMISReport.NewRow();

                    row["Date"] = g.Key.Date;
                    row["Forte_Bank"] = g.Key.Forte_Bank;
                    row["TT_No."] = "";
                    row["Beneficiary_Name"] = g.Key.Beneficiary_Name;
                    row["Forte_Claim_No"] = g.Key.Description;
                    row["USD"] = g.Sum(usd => Convert.ToDouble(usd.Field<string>("USD")));
                    row["Bank"] = g.Key.Bank;
                    row["BankAcc_No"] = g.Key.BankAcc_No;


                    return row;

                }).CopyToDataTable();



            //Set Table Data After Header
            for (int r = 0; r < dt2.Rows.Count; r++)
            {
                DataRow dr = dt2.Rows[r];
                for (int c = 0; c < dt2.Columns.Count; c++)
                {
                    if (c >= 6 && c <= 8)
                    {
                        ws.Cell(r + 6, c + 4).SetValue(dr[c].ToString());
                        

                    }
                    else
                    {
                        ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                        
                    }

                    //ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                }

            }
            IXLRange range = ws.Range(5, 1, 6 + dt2.Rows.Count, 3 + dt2.Columns.Count);
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            range.Style.Font.FontName = "Arial Narrow";
            ws.Columns(2, 15).AdjustToContents();
            ws.SheetView.ZoomScale = 90;

            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            ws.PageSetup.FitToPages(1, 1);
            ws.PageSetup.PagesTall = 1;
            ws.PageSetup.PagesWide = 1;
            ws.PageSetup.PrintAreas.Clear();
            ws.PageSetup.Margins.Left = 0.5;
            ws.PageSetup.Margins.Right = 0.5;
        }
        private void ThirdSheetNonDirectBilling(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, int ColumnsCount, int RowsCount, string ExcelFilePath = null)
        {
            //Worksheet Heading
            string[] Heading = { "FORTE INSURANCE (CAMBODIA) PLC.", "PAYMENT LIST TO ACCOUNTING (Non-Direct Billing)", "Date:", "By:" };
            wbHeading(Heading, wb, ws, ColumnsCount, RowsCount);
            //End of heading 
            DataTable dtMISReport = new DataTable();
            dtMISReport.Columns.Add("Date");
            dtMISReport.Columns.Add("Forte_Bank");
            dtMISReport.Columns.Add("TT_No.");
            dtMISReport.Columns.Add("Beneficiary_Name");
            dtMISReport.Columns.Add("Forte_Claim_No");
            dtMISReport.Columns.Add("USD");
            dtMISReport.Columns.Add("Bank");
            dtMISReport.Columns.Add("BankAcc_No");
            int MISReportRC = dtMISReport.Rows.Count, MISReportCC = dtMISReport.Columns.Count;
            //Set heading after header of excel
            //Set Header with DataTable dt Column Name\var cell
            var cell = ws.Cell(5, 1);
            for (int i = 0; i < MISReportCC; i++)
            {

                if (i >= 6 && i <= 7)
                {
                    if (i == 6)
                        ws.Column(i).InsertColumnsAfter(3);
                    cell = ws.Cell(5, i + 4);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                    
                }
                else
                {
                    cell = ws.Cell(5, i + 1);
                    cell.Value = dtMISReport.Columns[i].ColumnName;
                    
                }

                //Style Format on Header
                cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                cell.Style.Font.FontSize = 14f;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontName = "Arial Narrow";

            }
            //
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (DataRow item in dtcopy.Rows)
            {
                DataRow newRow = dtMISReport.NewRow();
                newRow["Date"] = DateTime.Today.ToShortDateString();
                newRow["Forte_Bank"] = item["Bank"];
                newRow["TT_No."] = "";
                newRow["Beneficiary_Name"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower());

                newRow["Forte_Claim_No"] = textInfo.ToTitleCase(item["Beneficiary_Name"].ToString().ToLower()) + " - " + item["Forte Claim No"].ToString();
                newRow["USD"] = Convert.ToDouble(item["USD"].ToString());
                newRow["Bank"] = item["Bank"];
                newRow["BankAcc_No"] = item["Bank_AccNo."];
                dtMISReport.Rows.Add(newRow);
            }

            DataTable dt2 = dtMISReport.AsEnumerable()
                .GroupBy(r => new { Date = r["Date"], Forte_Bank = r["Forte_Bank"], TT_No = r["TT_No."], Beneficiary_Name = r["Beneficiary_Name"], Description = r["Forte_Claim_No"], Bank = r["Bank"], BankAcc_No = r["BankAcc_No"] })
                .Select(g =>
                {
                    var row = dtMISReport.NewRow();

                    row["Date"] = g.Key.Date;
                    row["Forte_Bank"] = g.Key.Forte_Bank;
                    row["TT_No."] = "";
                    row["Beneficiary_Name"] = g.Key.Beneficiary_Name;
                    row["Forte_Claim_No"] = g.Key.Description;
                    row["USD"] = g.Sum(usd => Convert.ToDouble(usd.Field<string>("USD")));
                    row["Bank"] = g.Key.Bank;
                    row["BankAcc_No"] = g.Key.BankAcc_No;


                    return row;

                }).CopyToDataTable();



            //Set Table Data After Header
            for (int r = 0; r < dt2.Rows.Count; r++)
            {
                DataRow dr = dt2.Rows[r];
                for (int c = 0; c < dt2.Columns.Count; c++)
                {
                    if (c >= 6 && c <= 8)
                    {
                        ws.Cell(r + 6, c + 4).SetValue(dr[c].ToString());
                        

                    }
                    else
                    {
                        ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                        
                    }

                    //ws.Cell(r + 6, c + 1).SetValue(dr[c].ToString());
                }

            }
            IXLRange range = ws.Range(5, 1, 6 + dt2.Rows.Count, 3 + dt2.Columns.Count);
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            range.Style.Font.FontName = "Arial Narrow";
            ws.Columns(2, 15).AdjustToContents();
            ws.SheetView.ZoomScale = 90;

            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            ws.PageSetup.FitToPages(1, 1);
            ws.PageSetup.PagesTall = 1;
            ws.PageSetup.PagesWide = 1;
            ws.PageSetup.PrintAreas.Clear();
            ws.PageSetup.Margins.Left = 0.5;
            ws.PageSetup.Margins.Right = 0.5;
        }

        
    }
}
