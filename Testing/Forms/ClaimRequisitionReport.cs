using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class ClaimRequisitionReport : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        string sql;
        public string UserName = "SICL";
      //  private string[] fieldNames = {"NO","NOTIFIED DATE","CLASS","CLAIM NO","REQUEST NO","TYPE","PAYEE NAME","INCURRED AMT","PAID AMOUNT"};

        public ClaimRequisitionReport()
        {
            InitializeComponent();
        }
        public void BindComboBox()
        {
            DataRow dr;
            string SQLcombox = "select PRD_CODE Code, PRD_CODE ||' - '||PRD_DESCRIPTION DESCRIPTION from uw_m_products where PRD_ACTIVE='Y' and PRD_CODE IN ('HNS','BHP','EMC','MED','PAC','GPA','CAN') order by PRD_CLA_CODE,PRD_CODE";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            comboBox1.ValueMember = "Code";
            comboBox1.DisplayMember = "DESCRIPTION";
            comboBox1.DataSource = dtCombox;            
        }           
        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string IntFr = dtpFrom.Value.ToString("yyyy/MM/dd"), IntTo = dtpTo.Value.ToString("yyyy/MM/dd");

                sql = "";
              
                Cursor.Current = Cursors.WaitCursor;

                sql = "INSERT INTO user_print_history (user_name, print_datetime, filter2, type) VALUES ('" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + tbClaimNo.Text + "', '4')";
                crud.ExecNonQuery(sql);

                string subclass = comboBox1.Text.Trim();
                subclass = (subclass != "Select ALL") ? comboBox1.SelectedValue.ToString() : "";

                string[] Key = new string[] { "p_int_date_fr", "p_int_date_to", "p_class", "p_claim_no" };
                string[] Values = new string[] { IntFr, IntTo, subclass, tbClaimNo.Text.Trim().ToUpper() };
                dt = crud.ExecSP_OutPara("SP_CLAIM_REQUISITION", Key, Values);
                dgClaimPaid.DataSource = dt;

                Cursor.Current = Cursors.AppStarting;
                lblTotal.Text = "Total Record(s): " + dgClaimPaid.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbClaimNo.Text = "";
            lblTotal.Text = "Total Record(s): 0";
            comboBox1.SelectedValue = 0;
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpTo.Value = DateTime.Now;
            dgClaimPaid.DataSource = null;
            dgClaimPaid.Rows.Clear();
        }

        private void ClaimPaidReportPayee_Load(object sender, EventArgs e)
        {
            btnPDF.Visible = false;
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            BindComboBox();
            lblTotal.Text = "Total Record(s): 0"; ;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            if (dgClaimPaid.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(dt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }            

        private void tbPayeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bnSearch_Click(sender, e);
            }
        }

        private void tbClaimNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bnSearch_Click(sender, e);
            }
        }

        private void dgClaimPaid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgClaimPaid);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgClaimPaid.RowCount > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dtTempt = new DataTable();
                    dtTempt = dt.Copy();
                    Reports.ClaimPaidReportPayee myDataReport = new Reports.ClaimPaidReportPayee();
                    Forms.frmClaimIncurredReport frmReport = new Forms.frmClaimIncurredReport();

                    myDataReport.SetDataSource(dtTempt);
                    frmReport.rpt = myDataReport;
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

      
    }
}
