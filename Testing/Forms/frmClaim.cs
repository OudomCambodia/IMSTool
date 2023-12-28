using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Testing.Forms
{
    public partial class frmClaim : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        string sql;
        public string UserName = "SICL";
        private string[] fieldNames = { "Claim No", "Risk Name", "Date of Loss", "Year", "Notified Date", "Policy No", "Insured Code", "Insured Name", "Business", "Incurred Amt", "Paid Amt", "Outstanding Amt", "Status", "Nature of Loss", "Loss Desc.", "Class", "Product", "Branch Code", "Acc Handler", "Agent Code", "Agent Name" };

        private System.CodeDom.Compiler.TempFileCollection tempfile = new System.CodeDom.Compiler.TempFileCollection();

        public frmClaim()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPolicyNo.Text = "";
            tbClaimNo.Text = "";
            tbInsured.Text = "";
            tbRiskName.Text = "";
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpTo.Value = DateTime.Now;
            dtpIntFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpIntTo.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            this.ActiveControl = tbPolicyNo;
        }

        private void frmClaim_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpIntFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpIntFrom.Enabled = false;
            dtpIntTo.Enabled = false;
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "";
                if (tbPolicyNo.Text.Trim().ToString() == "" && tbInsured.Text.Trim().ToString() == "")
                {
                    Msgbox.Show("You must input either Policy No or Insured Name before seaching.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                sql = "INSERT INTO user_print_history (user_name, print_datetime, filter2, type) VALUES ('" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + tbPolicyNo.Text + ";;;" + tbInsured.Text + ";;;" + tbClaimNo.Text + "', '2')";
                crud.ExecNonQuery(sql);
                string[] Key = new string[] { "p_pol_no", "p_ins_name", "p_claim_no", "p_loss_date_fr", "p_loss_date_to", "p_noti_date_fr", "p_noti_date_to", "p_risk_name" };
                string[] Value = null;
                if (rbLoss.Checked)
                    Value = new string[] { tbPolicyNo.Text.Trim().ToUpper(), tbInsured.Text.Trim().ToUpper(),  tbClaimNo.Text.Trim().ToUpper(),
                    dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59",
                    "NA","NA", (tbRiskName.Text.Trim() == "")?"*":tbRiskName.Text.Trim().ToUpper().Replace(',','|')};
                else if (rbInt.Checked)
                    Value = new string[] { tbPolicyNo.Text.Trim().ToUpper(), tbInsured.Text.Trim().ToUpper(),  tbClaimNo.Text.Trim().ToUpper(),"NA","NA",
                    dtpIntFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpIntTo.Value.ToString("yyyy/MM/dd") + " 23:59:59",
                    (tbRiskName.Text.Trim() == "")?"*":tbRiskName.Text.Trim().ToUpper().Replace(',','|')};
                dt = crud.ExecSP_OutPara("sp_claim_checking", Key, Value);

                dataGridView1.DataSource = dt;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].Width = 150;

                for (int i = 0; i < fieldNames.Count(); i++)
                    dataGridView1.Columns[i].HeaderText = fieldNames[i];

                //dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MMM/yyyy";

                btnClaimExp.Enabled = tbPolicyNo.Text.Length == 20 && dt.Rows.Count > 0;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotNumber.Text = dataGridView1.RowCount.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dtTempt = new DataTable();
                    dtTempt = dt.Copy();
                    //DataSet dataReport = new DataSet();
                    //dataReport.Tables.Clear();
                    //dataReport.Tables.Add(dtTempt);
                    Reports.ClaimIncurredReport myDataReport = new Reports.ClaimIncurredReport();
                    Forms.frmClaimIncurredReport frmReport = new Forms.frmClaimIncurredReport();

                    myDataReport.SetDataSource(dtTempt);
                    frmReport.rpt = myDataReport;
                    //frmReport.crystalReportViewer1.ReportSource = myDataReport;
                    frmReport.ShowDialog();

                    Cursor.Current = Cursors.AppStarting;
                }
                else
                {
                    Msgbox.Show("No data found to be printed.");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            if (dataGridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcelXML(dt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            CloseFind();
        }

        private void frmClaim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                pnFind.Visible = true;
                pnFind.Enabled = true;
                this.ActiveControl = tbFind;
                tbFind.SelectAll();
            }
        }

        private void btnLookInExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    OpenFileDialog openKeywordsFileDialog = new OpenFileDialog();
                    openKeywordsFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    openKeywordsFileDialog.Multiselect = false;
                    openKeywordsFileDialog.ValidateNames = true;
                    openKeywordsFileDialog.DereferenceLinks = false; // Will return .lnk in shortcuts.
                    openKeywordsFileDialog.Filter = "Excel |*.xlsx";
                    openKeywordsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(OpenKeywordsFileDialog_FileOk);
                    var dialogResult = openKeywordsFileDialog.ShowDialog();
                    // OpenFileDialog fileDialog = sender as OpenFileDialog;
                    string selectedFile = openKeywordsFileDialog.FileName;
                    if (dialogResult == DialogResult.OK) // Test result.
                    {
                        Forms.CheckClaimInExcel chkClaim = new Forms.CheckClaimInExcel();
                        DataTable d = new DataTable();


                        ////Logic



                        chkClaim.dtClaim.DataSource = My_DataTable_Extensions.ConvertExcelToDataTable(selectedFile);
                        chkClaim.ShowDialog();
                    }

                    Cursor.Current = Cursors.AppStarting;
                }
                else
                {
                    Msgbox.Show("No data found to be compared.");

                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        void OpenKeywordsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog fileDialog = sender as OpenFileDialog;
            string selectedFile = fileDialog.FileName;
            if (string.IsNullOrEmpty(selectedFile) || selectedFile.Contains(".lnk"))
            {
                Msgbox.Show("Please select a valid Excel File");
                e.Cancel = true;
            }
            return;
        }

        private void tbFind_KeyUp(object sender, KeyEventArgs e)
        {
            FindFunc();
        }

        private void tbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseFind();
        }

        private void pnFind_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl == dataGridView1)
                return;

            CloseFind();
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != pnFind && this.ActiveControl != tbFind && this.ActiveControl != btnX)
                CloseFind();
        }

        private void FindFunc()
        {
            var query = from myRow in dt.AsEnumerable()
                        where myRow.Field<string>("INT_PRS_NAME").ToUpper().Contains(tbFind.Text.Trim().ToUpper())
                        select myRow;

            DataTable dtTemp = dt.Clone();

            if (!query.Any())
            {
                dtTemp.Rows.Clear();
                dataGridView1.DataSource = dtTemp;
                return;
            }

            dtTemp = query.CopyToDataTable<DataRow>();
            dataGridView1.DataSource = dtTemp;
        }

        private void CloseFind()
        {
            tbFind.Text = "";
            pnFind.Visible = false;
            pnFind.Enabled = false;
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dataGridView1);
        }

        private void rbInt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLoss.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
                dtpIntFrom.Enabled = false;
                dtpIntTo.Enabled = false;
            }
            else if (rbInt.Checked)
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
                dtpIntFrom.Enabled = true;
                dtpIntTo.Enabled = true;
            }
        }

        private void tbRiskName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("For multiple risk names, please use \",\" as seperator.", tbRiskName);
        }

        private void tbPolicyNo_Leave(object sender, EventArgs e)
        {
            string polno = tbPolicyNo.Text.ToUpper().Trim();
            if (polno.Length == 20)
            {
                DataTable dtTemp = crud.ExecQuery("select POL_POLICY_NO,POL_PERIOD_FROM,POL_PERIOD_TO from UW_T_POLICIES where POL_POLICY_NO = '" + polno + "' and POL_STATUS in (4,5,6,10)");
                if (dtTemp.Rows.Count <= 0)
                {
                    dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
                    dtpTo.Value = DateTime.Now;
                }
                else
                {
                    dtpFrom.Value = (DateTime)dtTemp.Rows[0][1];
                    dtpTo.Value = (DateTime)dtTemp.Rows[0][2];
                }
            }
        }

        private void btnClaimExp_Click(object sender, EventArgs e)
        {
            var dtCopy = new DataTable();
            dtCopy.Columns.Add("No.");

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.HeaderText.Equals("Claim No") || col.HeaderText.Equals("Risk Name") || col.HeaderText.Equals("Date of Loss") || col.HeaderText.Equals("Year")
                    || col.HeaderText.Equals("Notified Date") || col.HeaderText.Equals("Policy No") || col.HeaderText.Equals("Insured Code") || col.HeaderText.Equals("Insured Name")
                    || col.HeaderText.Equals("Incurred Amt") || col.HeaderText.Equals("Paid Amt") || col.HeaderText.Equals("Outstanding Amt") || col.HeaderText.Equals("Nature of Loss")
                    || col.HeaderText.Equals("PATIENT_TYPE"))
                {
                    dtCopy.Columns.Add(col.HeaderText);
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow newRow = dtCopy.Rows.Add();
                newRow["No."] = row.Index + 1;
                newRow["Claim No"] = row.Cells["CLAIM_NO"].Value;
                newRow["Risk Name"] = row.Cells["INT_PRS_NAME"].Value;
                newRow["Date of Loss"] = row.Cells["DATEOFLOSS"].Value;
                newRow["Year"] = row.Cells["PERIOD_YEAR"].Value;
                newRow["Notified Date"] = row.Cells["NOTIFIED_DATE"].Value;
                newRow["Policy No"] = row.Cells["POLICY_NO"].Value;
                newRow["Insured Code"] = row.Cells["INSUREDCODE"].Value;
                newRow["Insured Name"] = row.Cells["INSUREDNAME"].Value;
                newRow["Incurred Amt"] = row.Cells["INCURRED_AMT"].Value;
                newRow["Paid Amt"] = row.Cells["PAID_AMT"].Value;
                newRow["Outstanding Amt"] = row.Cells["OS_AMT"].Value;
                newRow["Nature of Loss"] = row.Cells["NATUREOFLOSS"].Value;
                newRow["PATIENT_TYPE"] = row.Cells["PATIENT_TYPE"].Value;
            }

            GenerateExcel(dtCopy);
        }

        private void GenerateExcel(DataTable dt, string ExcelFilePath = null)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var insuredCode = dt.Rows[0]["Insured Code"].ToString();
                var insuredName = dt.Rows[0]["Insured Name"].ToString();
                var policyNo = dt.Rows[0]["Policy No"].ToString();
                var fromDate = rbLoss.Checked ? dtpFrom.Value.ToString("dd/MM/yyyy") : dtpIntFrom.Value.ToString("dd/MM/yyyy");
                var toDate = rbLoss.Checked ? dtpTo.Value.ToString("dd/MM/yyyy") : dtpIntTo.Value.ToString("dd/MM/yyyy");
                var reportDate = DateTime.Now.ToString("dd/MM/yyyy");
                var byUser = frmLogIn.Usert.ToUpper();

                IXLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Claim Experience");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                #region --- Insured Name ---
                ws.Cell(2, 2).SetValue("INSURED NAME:");
                ws.Cell(2, 2).Style.Font.FontSize = 9f;
                ws.Cell(2, 2).Style.Font.Bold = true;
                ws.Cell(2, 2).Style.Font.FontName = "Arial";
                ws.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                ws.Cell(2, 3).SetValue(insuredName);
                ws.Cell(2, 3).Style.Font.FontSize = 9f;
                ws.Cell(2, 3).Style.Font.Bold = true;
                ws.Cell(2, 3).Style.Font.FontName = "Arial";
                ws.Cell(2, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                #region --- Insured Code ---
                ws.Cell(3, 2).SetValue("INSURED CODE:");
                ws.Cell(3, 2).Style.Font.FontSize = 9f;
                ws.Cell(3, 2).Style.Font.Bold = true;
                ws.Cell(3, 2).Style.Font.FontName = "Arial";
                ws.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                ws.Cell(3, 3).SetValue(insuredCode);
                ws.Cell(3, 3).Style.Font.FontSize = 9f;
                ws.Cell(3, 3).Style.Font.Bold = true;
                ws.Cell(3, 3).Style.Font.FontName = "Arial";
                ws.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                #region --- Policy No ---
                ws.Cell(4, 2).SetValue("POLICY NO.:");
                ws.Cell(4, 2).Style.Font.FontSize = 9f;
                ws.Cell(4, 2).Style.Font.Bold = true;
                ws.Cell(4, 2).Style.Font.FontName = "Arial";
                ws.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                ws.Cell(4, 3).SetValue(policyNo);
                ws.Cell(4, 3).Style.Font.FontSize = 9f;
                ws.Cell(4, 3).Style.Font.Bold = true;
                ws.Cell(4, 3).Style.Font.FontName = "Arial";
                ws.Cell(4, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                #region --- Claim Report ---
                ws.Cell(5, 2).SetValue("CLAIM REPORT:");
                ws.Cell(5, 2).Style.Font.FontSize = 9f;
                ws.Cell(5, 2).Style.Font.Bold = true;
                ws.Cell(5, 2).Style.Font.FontName = "Arial";
                ws.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                ws.Cell(5, 3).SetValue(string.Concat("From: ", fromDate, " - ", "To: ", toDate));
                ws.Cell(5, 3).Style.Font.FontSize = 9f;
                ws.Cell(5, 3).Style.Font.Bold = true;
                ws.Cell(5, 3).Style.Font.FontName = "Arial";
                ws.Cell(5, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                //ws.Cell(5, 4).SetValue(string.Concat("To: ", toDate));
                //ws.Cell(5, 4).Style.Font.FontSize = 9f;
                //ws.Cell(5, 4).Style.Font.Bold = true;
                //ws.Cell(5, 4).Style.Font.FontName = "Arial";
                //ws.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                #region --- Report Date ---
                ws.Cell(8, 1).SetValue("DATE:");
                ws.Cell(8, 1).Style.Font.FontSize = 9f;
                ws.Cell(8, 1).Style.Font.Bold = true;
                ws.Cell(8, 1).Style.Font.FontName = "Arial";
                ws.Cell(8, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                ws.Cell(8, 2).SetValue(reportDate);
                ws.Cell(8, 2).Style.Font.FontSize = 9f;
                ws.Cell(8, 2).Style.Font.Bold = true;
                ws.Cell(8, 2).Style.Font.FontName = "Arial";
                ws.Cell(8, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                #region --- By ---
                ws.Cell(8, 5).SetValue(string.Concat("BY: ", byUser));
                ws.Cell(8, 5).Style.Font.FontSize = 9f;
                ws.Cell(8, 5).Style.Font.Bold = true;
                ws.Cell(8, 5).Style.Font.FontName = "Arial";
                ws.Cell(8, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                #endregion

                // No need to show all these 3 columns in detail part because there is only one Policy
                dt.Columns.Remove("POLICY NO");
                dt.Columns.Remove("INSURED CODE");
                dt.Columns.Remove("INSURED NAME");

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var cell = ws.Cell(10, i + 1); //10 means start adding from row 10, +1 cuz it starts from column 1
                    cell.Style.Font.FontName = "Arial";
                    cell.Style.Font.FontSize = 9f;
                    cell.Style.Font.Bold = true;

                    if (dt.Columns[i].ColumnName.ToUpper().Equals("PATIENT_TYPE"))
                        cell.Value = "OUTPATIENT/INPATIENT";
                    else if (dt.Columns[i].ColumnName.ToUpper().Equals("YEAR"))
                        cell.Value = "UWY";
                    else
                        cell.Value = dt.Columns[i].ColumnName.ToUpper().Equals("NO.") ? "No." : dt.Columns[i].ColumnName.ToUpper();

                    cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    if (dt.Columns[i].ColumnName.ToUpper().Equals("NO.") || dt.Columns[i].ColumnName.ToUpper().Equals("YEAR"))
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    else
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    cell.Style.Fill.SetBackgroundColor(XLColor.Yellow);

                    
                }
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    DataRow dr = dt.Rows[r];

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        ws.Cell(r + 11, c + 1).SetValue(dr[c].ToString());

                        ws.Cell(r + 11, c + 1).Style.Font.FontName = "Arial";
                        ws.Cell(r + 11, c + 1).Style.Font.FontSize = 9f;
                        ws.Cell(r + 11, c + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Cell(r + 11, c + 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell(r + 11, c + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Cell(r + 11, c + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(r + 11, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(r + 11, c + 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                        if (dt.Columns[c].ColumnName.ToUpper().Equals("YEAR")
                            || dt.Columns[c].ColumnName.ToUpper().Equals("NO."))
                        {
                            ws.Cell(r + 11, c + 1).DataType = XLDataType.Number;
                            ws.Cell(r + 11, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        }
                        else if (dt.Columns[c].ColumnName.ToUpper().Equals("INCURRED AMT")
                            || dt.Columns[c].ColumnName.ToUpper().Equals("PAID AMT")
                            || dt.Columns[c].ColumnName.ToUpper().Equals("OUTSTANDING AMT"))
                        {
                            ws.Cell(r + 11, c + 1).DataType = XLDataType.Number;
                            ws.Cell(r + 11, c + 1).Style.NumberFormat.Format = "#,##0.00";
                        }
                    }
                }

                // Set Adjust To Contents to all columns
                for (int i = 1; i <= 14; i++)
                    ws.Column(i).AdjustToContents();

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
