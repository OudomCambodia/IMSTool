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
        private DataTable dtGrpCustomer = new DataTable();
        private string producerCode = string.Empty;
        private string cusCode = string.Empty;
        private string grpCode = string.Empty;
        private string grpCusCodeName = string.Empty;

        public frmGenerateCustomerProfitSummary()
        {
            InitializeComponent();
            txtCusCode.CharacterCasing = CharacterCasing.Upper;
            txtSearch.CharacterCasing = CharacterCasing.Upper;
        }

        private void frmGenerateCustomerProfitSummary_Load(object sender, EventArgs e)
        {
            BindComboBox();
        }

        private void BindComboBox()
        {
            DataRow dr;
            string SQLcombox = "select GRP_CODE,GRP_DESCRIPTION from uw_r_groups where grp_code <> 'AC' order by GRP_CODE,GRP_DESCRIPTION";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            cboGroupCustomer.ValueMember = "GRP_CODE";
            cboGroupCustomer.DisplayMember = "GRP_DESCRIPTION";
            cboGroupCustomer.DataSource = dtCombox;
        }

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            cusCode = txtCusCode.Text.Trim();
            if (!string.IsNullOrEmpty(cusCode))
            {
                var dtGrpCode = crud.ExecQuery("select cus_grp_code from uw_m_customers where cus_code = '" + cusCode + "'");
                if (dtGrpCode.Rows.Count > 0)
                {
                    grpCode = dtGrpCode.Rows[0][0].ToString();
                    if (string.IsNullOrEmpty(grpCode))
                    {
                        grpCode = "N/A";
                        cboGroupCustomer.SelectedIndex = 0;
                    }
                    else
                    {
                        var dtGrpName = crud.ExecQuery("select grp_description from uw_r_groups where grp_code = '" + grpCode.ToUpper() + "'").Rows[0][0].ToString();
                        cboGroupCustomer.SelectedIndex = cboGroupCustomer.FindStringExact(dtGrpName);
                    }
                }
                else
                    cboGroupCustomer.SelectedIndex = 0;
            }
            else
                grpCode = cboGroupCustomer.SelectedValue.ToString();
        }

        private void cboGroupCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            chkSelectAll.Checked = false;

            grpCode = cboGroupCustomer.SelectedValue.ToString() == "0" ? "N/A" : cboGroupCustomer.SelectedValue.ToString();


            if (cboGroupCustomer.SelectedIndex != 0)
            {
                txtCusCode.Clear();
                cusCode = string.Empty;
            }
            txtCusCode.Enabled = cboGroupCustomer.SelectedValue.ToString() == "0";

            dtGrpCustomer = crud.ExecQuery("select cus_code, cus_corp_name from uw_m_customers where cus_grp_code = '" + grpCode + "'");

            chkSelectAll.Enabled = dtGrpCustomer.Rows.Count > 0;

            ((ListBox)chkLstGrpCustomer).DataSource = dtGrpCustomer;
            ((ListBox)chkLstGrpCustomer).ValueMember = "CUS_CODE";
            ((ListBox)chkLstGrpCustomer).DisplayMember = "CUS_CORP_NAME";

            txtSearch.Enabled = chkLstGrpCustomer.Items.Count > 0;
            if (chkLstGrpCustomer.Items.Count <= 0)
                txtSearch.Clear();

            Cursor = Cursors.Arrow;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "--- SEARCH CUSTOMER CODE ---")
                txtSearch.Clear();
            txtSearch.ForeColor = Color.Black;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "--- SEARCH CUSTOMER CODE ---";
                txtSearch.ForeColor = Color.DarkGray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var index = 0;

            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                string cusCorpName = crud.ExecQuery("select cus_corp_name from uw_m_customers where cus_code = '" + txtSearch.Text.Trim() + "'").Rows.Count > 0 ? 
                    crud.ExecQuery("select cus_corp_name from uw_m_customers where cus_code = '" + txtSearch.Text.Trim() + "'").Rows[0][0].ToString() : string.Empty;

                index = chkLstGrpCustomer.FindStringExact(cusCorpName);
                chkLstGrpCustomer.SelectedIndex = index;

                if (index != -1)
                {
                    if (chkLstGrpCustomer.GetItemCheckState(index) == CheckState.Checked)
                    {
                        chkLstGrpCustomer.SetItemChecked(index, false);
                    }
                    else
                    {
                        chkLstGrpCustomer.SetItemChecked(index, true);
                    }
                }
            }
            if (index == -1)
            {
                chkLstGrpCustomer.SelectedIndex = 0;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstGrpCustomer.Items.Count; i++)
            {
                if (chkSelectAll.Checked)
                    chkLstGrpCustomer.SetItemChecked(i, true);
                else
                    chkLstGrpCustomer.SetItemChecked(i, false);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                #region --- GET LIST GROUP NAME/CODE ---
                grpCusCodeName = string.Empty;
                var grpCusCode = string.Empty;
                foreach (var chkRow in chkLstGrpCustomer.CheckedItems)
                {
                    var drvChkRow = chkRow as DataRowView;
                    grpCusCode = grpCusCode + drvChkRow.Row[0].ToString() + "|";
                    grpCusCodeName = grpCusCodeName + "- " + drvChkRow.Row[1].ToString() + ", " + drvChkRow.Row[0].ToString() + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(grpCusCode))
                {
                    grpCusCode = grpCusCode.ToString().Remove(grpCusCode.ToString().Length - 1);
                }
                #endregion

                #region --- GET PRODUCER CODE ---
                var pBuilder = new StringBuilder();
                pBuilder.Append("select sfc_code || ' - '  || sfc_surname producer ")
                    .Append("from sm_m_sales_force where sfc_code = ( ")
                    .Append("select sfc_code from ( ")
                    .Append("select cus_bparty_code sfc_code, count(cus_bparty_code) ct ")
                    .Append("from uw_m_customers ");
                if (!string.IsNullOrEmpty(cusCode))
                {
                    pBuilder.AppendFormat("where cus_code = '{0}' ", cusCode);
                }
                else
                {
                    pBuilder.AppendFormat("where regexp_like(nvl({0}, '*'), '*', 'i') ", "'" + grpCusCode + "'")
                    .AppendFormat("and cus_grp_code = nvl({0}, cus_grp_code) ", "'" + grpCode + "'");
                }
                pBuilder.Append("group by cus_bparty_code ")
                .Append("order by ct desc) ")
                .Append("where rownum = 1)");

                producerCode = crud.ExecQuery(pBuilder.ToString()).Rows.Count > 0 ? crud.ExecQuery(pBuilder.ToString()).Rows[0][0].ToString() : string.Empty;
                #endregion

                if (string.IsNullOrEmpty(grpCusCode) && cboGroupCustomer.SelectedIndex != 0)
                {
                    Msgbox.Show("Please check at least one customer");
                    Cursor = Cursors.Arrow;
                    return;
                }

                string[] Key = new string[] { "p_cus_code", "p_grp_code" };
                string[] Values = new string[] { !string.IsNullOrEmpty(grpCusCode) ? grpCusCode : cusCode, grpCode }; //C000028640

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
                                    ratio = Math.Round(claim / premium, 2, MidpointRounding.AwayFromZero);
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
                        dtEachProClass.Rows[1]["TOTAL"] = totalRowClaim.ToString("0.###");

                        var sRatio = ratios.Split('*');

                        // add Ratio row to dtEachProClass
                        var ratioRow = dtEachProClass.NewRow();
                        ratioRow["PRODUCT"] = string.Empty;
                        ratioRow["UWY"] = "RATIO %";

                        // add Ratio value to each column (2015, 2016, 2017, ...)
                        for (var m = 0; m < columnNames.Count; m++)
                        {
                            if (columnNames[m].ToString().Equals("PRODUCT") || columnNames[m].ToString().Equals("UWY"))
                                continue;
                            ratioRow[columnNames[m].ToString()] = Convert.ToDecimal(sRatio[m - 1]);
                        }

                        // calculate totalRatio and add to dtEachProClass
                        if (totalRowClaim > 0 && totalRowPremium > 0)
                            ratioRow["TOTAL"] = Math.Round(Convert.ToDecimal(totalRowClaim / totalRowPremium), 2, MidpointRounding.AwayFromZero).ToString("0.###");
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
                            row["UWY"] = k == 0 ? "CLAIM" : "RATIO %";
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
                drTotalRatio["UWY"] = "RATIO %";
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
                            ratio = Math.Round(claim / premium, 2, MidpointRounding.AwayFromZero);
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

                #region --- GET CUS CODE AND CUS NAME ---
                var cusCode = string.Empty;
                var cusName = string.Empty;

                if (!string.IsNullOrEmpty(txtCusCode.Text.Trim()))
                {
                    cusCode = txtCusCode.Text.Trim();
                    cusName = crud.ExecQuery("select nvl(cus_indv_surname, cus_corp_name) from uw_m_customers where cus_code = '" + cusCode + "'").Rows.Count > 0 ?
                        crud.ExecQuery("select nvl(cus_indv_surname, cus_corp_name) from uw_m_customers where cus_code = '" + cusCode + "'").Rows[0][0].ToString() : string.Empty;
                }
                if (cboGroupCustomer.SelectedIndex != 0)
                {
                    cusCode = cboGroupCustomer.SelectedValue.ToString();
                    cusName = crud.ExecQuery("select grp_description from uw_r_groups where grp_code = '" + cusCode + "'").Rows.Count > 0 ?
                        crud.ExecQuery("select grp_description from uw_r_groups where grp_code = '" + cusCode + "'").Rows[0][0].ToString() : string.Empty;
                }
                #endregion

                IXLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Summary Report");
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                int RowsCount = dt.Rows.Count, ColumnsCount = dt.Columns.Count;

                #region --- SUMMARY REPORT TEXT ---
                ws.Cell(1, 1).SetValue("SUMMARY PROFITABILITY REPORT");
                ws.Cell(1, 1).Style.Font.FontSize = 20f;
                ws.Cell(1, 1).Style.Font.FontName = "Century Gothic";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontColor = XLColor.Green;
                #endregion

                // add empty row
                ws.Cell(2, 1).SetValue("");

                #region --- CUSTOMER CODE ---
                ws.Cell(3, 1).SetValue("CUSTOMER CODE");
                ws.Cell(3, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(3, 1).Style.Font.FontSize = 9f;
                ws.Cell(3, 1).Style.Font.FontName = "Century Gothic";
                ws.Cell(3, 1).Style.Font.Bold = true;

                ws.Cell(3, ColumnsCount - 12).SetValue(":");
                ws.Cell(3, ColumnsCount - 12).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(3, ColumnsCount - 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(3, ColumnsCount - 11).SetValue(cusCode);
                ws.Cell(3, ColumnsCount - 11).Style.Font.FontSize = 9f;
                ws.Cell(3, ColumnsCount - 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(3, ColumnsCount - 11).Style.Font.FontName = "Century Gothic";
                ws.Cell(3, ColumnsCount - 11).Style.Font.Bold = true;
                ws.Column(ColumnsCount - 11).AdjustToContents();
                //ws.Range(3, ColumnsCount - 11, 3, ColumnsCount - 9).Merge();
                #endregion

                #region --- PRODUCER CODE ---
                ws.Cell(3, ColumnsCount - 7).SetValue("PRODUCER CODE");
                ws.Cell(3, ColumnsCount - 7).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(3, ColumnsCount - 7).Style.Font.FontSize = 9f;
                ws.Cell(3, ColumnsCount - 7).Style.Font.FontName = "Century Gothic";
                ws.Cell(3, ColumnsCount - 7).Style.Font.Bold = true;

                ws.Cell(3, ColumnsCount - 4).SetValue(":");
                ws.Cell(3, ColumnsCount - 4).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(3, ColumnsCount - 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(3, ColumnsCount - 3).SetValue(producerCode);
                ws.Cell(3, ColumnsCount - 3).Style.Font.FontSize = 9f;
                ws.Cell(3, ColumnsCount - 3).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(3, ColumnsCount - 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(3, ColumnsCount - 3).Style.Font.FontName = "Century Gothic";
                ws.Cell(3, ColumnsCount - 3).Style.Font.Bold = true;
                ws.Range(3, ColumnsCount - 3, 3, ColumnsCount - 2).Merge();
                #endregion

                #region --- CUSTOMER NAME ---
                ws.Cell(4, 1).SetValue("CUSTOMER NAME");
                ws.Cell(4, 1).Style.Font.FontSize = 9f;
                ws.Cell(4, 1).Style.Font.FontName = "Century Gothic";
                ws.Cell(4, 1).Style.Font.Bold = true;

                ws.Cell(4, ColumnsCount - 12).SetValue(":");
                ws.Cell(4, ColumnsCount - 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(4, ColumnsCount - 11).SetValue(cusName);
                ws.Cell(4, ColumnsCount - 11).Style.Font.FontSize = 9f;
                ws.Cell(4, ColumnsCount - 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(4, ColumnsCount - 11).Style.Font.FontName = "Century Gothic";
                ws.Cell(4, ColumnsCount - 11).Style.Font.Bold = true;
                ws.Range(4, ColumnsCount - 11, 4, ColumnsCount - 9).Merge();
                #endregion

                #region --- GROUP CUSTOMER ---
                ws.Cell(4, ColumnsCount - 7).SetValue("GROUP CUSTOMER");
                ws.Cell(4, ColumnsCount - 7).Style.Font.FontSize = 9f;
                ws.Cell(4, ColumnsCount - 7).Style.Font.FontName = "Century Gothic";
                ws.Cell(4, ColumnsCount - 7).Style.Font.Bold = true;

                ws.Cell(4, ColumnsCount - 4).SetValue(":");
                ws.Cell(4, ColumnsCount - 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(4, ColumnsCount - 3).SetValue(cboGroupCustomer.SelectedIndex != 0 ? "YES" : "NO");
                ws.Cell(4, ColumnsCount - 3).Style.Font.FontSize = 9f;
                ws.Cell(4, ColumnsCount - 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(4, ColumnsCount - 3).Style.Font.FontName = "Century Gothic";
                ws.Cell(4, ColumnsCount - 3).Style.Font.Bold = true;
                //ws.Range(4, ColumnsCount - 3, 4, ColumnsCount - 1).Merge();
                #endregion

                #region --- REPORT DATE ---
                ws.Cell(5, 1).SetValue("REPORT DATE");
                ws.Cell(5, 1).Style.Font.FontSize = 9f;
                ws.Cell(5, 1).Style.Font.FontName = "Century Gothic";
                ws.Cell(5, 1).Style.Font.Bold = true;

                ws.Cell(5, ColumnsCount - 12).SetValue(":");
                ws.Cell(5, ColumnsCount - 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(5, ColumnsCount - 11).SetValue(DateTime.Now.ToString());
                ws.Cell(5, ColumnsCount - 11).DataType = XLDataType.DateTime;
                ws.Cell(5, ColumnsCount - 11).Style.DateFormat.Format = "dd-MM-yyyy hh:mm:ss AM/PM";
                ws.Cell(5, ColumnsCount - 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(5, ColumnsCount - 11).Style.Font.FontSize = 9f;
                ws.Cell(5, ColumnsCount - 11).Style.Font.FontName = "Century Gothic";
                ws.Cell(5, ColumnsCount - 11).Style.Font.Bold = true;
                ws.Range(5, ColumnsCount - 11, 5, ColumnsCount - 9).Merge();
                #endregion

                #region --- LISTED GROUP NAME/CODE ---
                ws.Cell(5, ColumnsCount - 7).SetValue("LISTED GROUP NAME/CODE");
                ws.Cell(5, ColumnsCount - 7).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(5, ColumnsCount - 7).Style.Font.FontSize = 9f;
                ws.Cell(5, ColumnsCount - 7).Style.Font.FontName = "Century Gothic";
                ws.Cell(5, ColumnsCount - 7).Style.Font.Bold = true;

                ws.Cell(5, ColumnsCount - 4).SetValue(":");
                ws.Cell(5, ColumnsCount - 4).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(5, ColumnsCount - 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                ws.Cell(5, ColumnsCount - 3).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                ws.Cell(5, ColumnsCount - 3).SetValue(grpCusCodeName);
                ws.Cell(5, ColumnsCount - 3).Style.Font.FontSize = 9f;
                ws.Cell(5, ColumnsCount - 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(5, ColumnsCount - 3).Style.Font.FontName = "Century Gothic";
                ws.Cell(5, ColumnsCount - 3).Style.Font.Bold = true;
                ws.Rows("5").AdjustToContents();
                ws.Range(5, ColumnsCount - 3, 5, ColumnsCount + 3).Merge();
                #endregion

                // add empty row
                ws.Cell(6, 2).SetValue("");

                //Set Header with DataTable dt Column Name
                for (int i = 0; i < ColumnsCount; i++)
                {
                    var cell = ws.Cell(7, i + 1); //7 means start adding from row 5, +1 cuz it starts from column 1
                    cell.Style.Font.FontName = "Century Gothic";
                    cell.Style.Font.FontSize = 8f;
                    cell.Value = dt.Columns[i].ColumnName;
                    cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

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
                bool isRatio = false;
                int totalCol = 0;

                //Set Table Data After Header
                for (int r = 0; r < RowsCount; r++)
                {
                    DataRow dr = dt.Rows[r];
                    totalCol = dr.ItemArray.Count() - 1;
                    isRatio = dr[1].ToString().Equals("RATIO %");

                    for (int c = 0; c < ColumnsCount; c++)
                    {
                        ws.Cell(r + 8, c + 1).Style.Font.FontName = "Century Gothic";
                        ws.Cell(r + 8, c + 1).Style.Font.FontSize = 8f;

                        ws.Cell(r + 8, c + 1).SetValue(dr[c].ToString()); //+6 cuz it starts from sixth row after Summary Report text

                        if (isRatio)
                            ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "0.0%";
                        else
                            ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "#,##0";

                        if (c > 1)
                        {
                            decimal numericValue = 0;
                            bool isNumber = decimal.TryParse(dr[c].ToString(), out numericValue);
                            if (isNumber && numericValue > 0)
                            {
                                ws.Cell(r + 8, c + 1).SetValue(dr[c].ToString());
                                ws.Cell(r + 8, c + 1).DataType = XLDataType.Number;
                                if (isRatio)
                                    ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "0.0%";
                                else
                                    ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "#,##0";
                            }
                            else
                            {
                                ws.Cell(r + 8, c + 1).SetValue("-");

                                #region --- ADD 0 and 0.00% format ---
                                //if (isRatio)
                                //{
                                //    ws.Cell(r + 8, c + 1).SetValue(0.000);
                                //    ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "0.0%";
                                //}
                                //else
                                //{
                                //    ws.Cell(r + 8, c + 1).SetValue(0);
                                //    ws.Cell(r + 8, c + 1).Style.NumberFormat.Format = "#,##0";
                                //}  
                                #endregion
                            }
                        }
                        if (c == 0)
                            ws.Cell(r + 8, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        else if (c == 1)
                            ws.Cell(r + 8, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        else
                            ws.Cell(r + 8, c + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                        //(!isSet && formatRowCount == 0) => first row after row header, formatRowCount == 3 => add bg color to each 3 rows
                        if ((!isSet && formatRowCount == 0) || formatRowCount == 3)
                        {
                            var colName = ws.Cell(r + 8, c + 1).CachedValue.ToString();

                            if (colName.Equals("TOTAL"))
                            {
                                for (int i = 0; i < ColumnsCount; i++)
                                {
                                    ws.Cell(r + 8, i + 1).Style.Font.FontName = "Century Gothic";
                                    ws.Cell(r + 8, i + 1).Style.Font.FontSize = 8f;

                                    decimal numericValue = 0;
                                    bool isNumber = decimal.TryParse(dr[i].ToString(), out numericValue);
                                    if (isNumber && numericValue > 0)
                                    {
                                        ws.Cell(r + 8, i + 1).SetValue(dr[i].ToString());
                                        ws.Cell(r + 8, i + 1).DataType = XLDataType.Number;
                                        if (isRatio)
                                            ws.Cell(r + 8, i + 1).Style.NumberFormat.Format = "0.0%";
                                        else
                                            ws.Cell(r + 8, i + 1).Style.NumberFormat.Format = "#,##0";
                                        ws.Cell(r + 8, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                    }
                                    else if (i == 0 || i == 1)
                                    {
                                        ws.Cell(r + 8, i + 1).SetValue(i == 0 ? "TOTAL" : "PREMIUM");
                                        if (i == 1)
                                            ws.Cell(r + 8, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                    }
                                    else
                                    {
                                        ws.Cell(r + 8, i + 1).SetValue("-");
                                        ws.Cell(r + 8, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                    }

                                    ws.Cell(r + 8, i + 1).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                                    ws.Cell(r + 8, i + 1).Style.Font.Bold = true;
                                }
                                break;
                            }
                            else
                            {
                                ws.Cell(r + 8, 1).Style.Font.Bold = true;
                                ws.Cell(r + 8, c + 1).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#fed8b1"));
                            }

                            formatRowCount = 0;
                            isSet = false;
                        }
                        if (c == ColumnsCount)
                            isSet = true;

                        // format Total CLaim Column
                        if (r == RowsCount - 1)
                        {
                            ws.Cell(r + 8, c + 1).Style.Font.FontColor = XLColor.Blue;
                            ws.Cell(r + 8, c + 1).Style.Font.Bold = true;
                        }

                        // format Total Ratio Column
                        if (r == RowsCount - 2)
                            ws.Cell(r + 8, c + 1).Style.Font.Bold = true;

                        // set bold to total column
                        if (c == totalCol)
                            ws.Cell(r + 8, c + 1).Style.Font.Bold = true;
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
