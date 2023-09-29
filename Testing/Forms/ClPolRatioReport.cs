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
    public partial class ClPolRatioReport : Form
    {
        public string FullName = "SICL";
        public string username = "SICL";
        CRUD crud = new CRUD();
        DataTable dt;
        CheckBox checkboxHeader = new CheckBox();
        string policy;
        private System.CodeDom.Compiler.TempFileCollection tempfile = new System.CodeDom.Compiler.TempFileCollection();

        public ClPolRatioReport()
        {
            InitializeComponent();
        }
        private void ClPolRatioReport_Load(object sender, EventArgs e)
        {
            this.dgvResult.ForeColor = System.Drawing.Color.Black;
        }
        private void requeryDGV(string Cuscode)
        {
            try
            {


               
                    string sp_type = "Cl_Pol_Over";
                    string[] Keys = new string[] { "sp_type", "sp_customer_no","sp_pol_no" };
                    string[] Values = new string[] { sp_type, tbCustomerCode.Text.ToUpper().Trim()};
                    dt = crud.ExecSP_OutPara("USER_CLAIMRATIO_POLICIES", Keys, Values);
                


                Cursor.Current = Cursors.AppStarting;

                if (dt.Rows.Count == 0)
                {
                    Msgbox.Show("No Data Found!");
                }
                else
                {
                    dgvResult.DataSource = null;
                    dgvResult.Columns.Clear();

                    DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                    
                   
                    dgvResult.DataSource = dt;

                    dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewColumn column = dgvResult.Columns[2];
                    column.Width = 150;
                    dgvResult.Columns[0].Resizable = DataGridViewTriState.False;
                    dgvResult.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


                    for (int i = 1; i < dgvResult.Columns.Count; i++)
                    {
                        if (i >= 4 && i < dgvResult.Columns.Count)
                            dgvResult.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                        dgvResult.Columns[i].ReadOnly = true;

                    }
                    
                }
                dgvResult.Columns[0].Visible = false;
                dgvResult.Columns[1].Visible = false;
               
                dgvResult.ClearSelection();
                


            }

            catch (Exception ex)
            {
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show(ex.Message);
            }
        }
        private void bnView_Click_1(object sender, EventArgs e)
        {
            string sql = "select POL_POLICY_NO from uw_t_policies where POL_STATUS IN (4,5,6,9,10) and pol_cus_code ='" + tbCustomerCode.Text.ToString().Trim().ToUpper() + "' group by POL_POLICY_NO";

            if (tbCustomerCode.Text.ToString() != "")
            {
                //Load Data to gridview
                requeryDGV(tbCustomerCode.Text.ToString().Trim().ToUpper());
                //Load Data to Checklist box
                DataTable dtPolicies = crud.ExecQuery(sql);
                ((ListBox)chkPolicies).DataSource = null;
                chkPolicies.Visible = true;
                ((ListBox)chkPolicies).DataSource = dtPolicies;
                ((ListBox)chkPolicies).ValueMember = "POL_POLICY_NO";
                ((ListBox)chkPolicies).DisplayMember = "displaypolicies";

            }

            else
                Msgbox.Show("Customer code is required!");


            Cursor.Current = Cursors.WaitCursor;
        }
        private void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            GenerateSummaryReport(dt);
        }
        private void GenerateSummaryReport(DataTable dtcopy, string ExcelFilePath = null)
        {

            try
            {
                IXLWorkbook wb = wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add("Report Policy & Claim");
                
                ws.DataType = XLDataType.Text; //Set all cells datatype as Text

                int RowsCount = dtcopy.Rows.Count, ColumnsCount = dtcopy.Columns.Count;
                if (RowsCount <= 0)
                {
                    Msgbox.Show("No record selected!");

                }
                else
                {

                    //First Row of Report
                    FirstRowReport(dtcopy, wb, ws);

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
        private void chkPolicies_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                foreach (object element in chkPolicies.SelectedItems)
                {
                    DataRowView row = (DataRowView)element;
                    //MessageBox.Show(row[0].ToString());
                    policy += row[0].ToString() + "|";
                }
                
                
            }
            else
            {
                foreach (object element in chkPolicies.SelectedItems)
                {
                    DataRowView row = (DataRowView)element;
                    //MessageBox.Show(row[0].ToString());
                    policy = policy.Replace(row[0].ToString() + "|", "");

                }
               
                

            }
        }
        private void FirstRowReport(DataTable dtcopy, IXLWorkbook wb, IXLWorksheet ws, string ExcelFilePath = null)
        {
            ws.Cell(1, 1).SetValue("Policy Holder");
            ws.Cell(1, 1).Style.Font.FontSize = 16f;
            ws.Cell(1, 1).Style.Font.FontName = "Arial Narrow";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontColor = XLColor.Black;
        }

    
      
       

       

       

       

    }
}
