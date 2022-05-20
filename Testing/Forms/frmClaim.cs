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

namespace Testing.Forms
{
    public partial class frmClaim : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        string sql;
        public string UserName = "SICL";
        private string[] fieldNames = { "Claim No", "Risk Name", "Date of Loss", "Year", "Notified Date", "Policy No", "Insured Code", "Insured Name", "Business", "Incurred Amt", "Paid Amt", "Outstanding Amt", "Status", "Nature of Loss", "Loss Desc.", "Class", "Product", "Branch Code", "Acc Handler", "Agent Code", "Agent Name" };

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
                string[] Value  = null;
                if(rbLoss.Checked)
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
                DataTable dtTemp = crud.ExecQuery("select POL_POLICY_NO,POL_PERIOD_FROM,POL_PERIOD_TO from UW_T_POLICIES where POL_POLICY_NO = '"+polno+"' and POL_STATUS in (4,5,6,10)");
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
    }
}
