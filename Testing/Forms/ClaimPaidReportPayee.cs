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
    public partial class ClaimPaidReportPayee : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        string sql;
        public string UserName = "SICL";
        //private string[] fieldNames = {"NO","NOTIFIED DATE","CLASS","CLAIM NO","REQUEST NO","TYPE","PAYEE NAME","INCURRED AMT","PAID AMOUNT"};

        public ClaimPaidReportPayee()
        {
            InitializeComponent();
        }
        public void BindComboBox()
        {
            DataRow dr;
            string SQLcombox = "select PRD_CODE Code, PRD_CODE ||' - '||PRD_DESCRIPTION DESCRIPTION from uw_m_products where PRD_ACTIVE='Y' order by PRD_CLA_CODE,PRD_CODE";
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

                //Update 21-May-20
                //string[] DateTempStore = dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss").Split(' ');
                //string IntFr = DateTempStore[0] + " 00:00:00";
                //DateTempStore = dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss").Split(' ');
                //string IntTo = DateTempStore[0] + " 23:59:59";

                string IntFr = dtpFrom.Value.ToString("yyyy/MM/dd"), IntTo = dtpTo.Value.ToString("yyyy/MM/dd");

                //

                sql = "";
              
                Cursor.Current = Cursors.WaitCursor;

                sql = "INSERT INTO user_print_history (user_name, print_datetime, filter2, type) VALUES ('" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + tbPayeeName.Text + ";;;" + tbClaimNo.Text + "', '2')";
                crud.ExecNonQuery(sql);

                sql = "SELECT rownum No,to_char(vc.NOTIFIED_DATE,'dd/mm/yyyy')as NOTIFIED_DATE,vc.SUBCLASS,vc.CLAIM_NO,vc.REQ_REQUISITION_NO,vc.PAYEE_TYPE,vc.PAYEE_NAME,vc.CHEQUE_WRITTEN_NAME,vc.INCURRED_AMT,vc.PAID_AMT,to_char(vc.PAID_DATE,'dd/mm/yyyy') as PAID_DATE,TPA_CLAIM_NO FROM VIEW_CL_PIAD_PAYEE vc WHERE rownum<50001";

                if (comboBox1.Text.Trim() != "Select ALL")
                    sql += " and SUBCLASS = '" + comboBox1.SelectedValue + "'";

                if (tbPayeeName.Text.Trim() != "")
                    sql += " and PAYEE_NAME like '%" + tbPayeeName.Text.Trim().ToUpper() + "%'";

                if (tbClaimNo.Text.Trim() != "")
                    sql += " and claim_no like '%" + tbClaimNo.Text.Trim().ToUpper() + "%'";

                //sql += " and Notified_Date >= TO_DATE('" + IntFr + "','YYYY/MM/DD HH24:MI:SS')";
                //sql += " and Notified_Date <= TO_DATE('" + IntTo + "','YYYY/MM/DD HH24:MI:SS')";                
                sql += " and Notified_Date >= TO_DATE('" + IntFr + "','YYYY/MM/DD')";
                sql += " and Notified_Date <= TO_DATE('" + IntTo + "','YYYY/MM/DD')";
                sql += " order by rownum, Notified_Date, PAYEE_NAME";

                dt = crud.ExecQuery(sql);
                dgClaimPaid.DataSource = dt;

                //for (int i = 0; i < dgClaimPaid.Columns.Count; i++)
                //    dgClaimPaid.Columns[i].Width = 150;

                //for (int i = 0; i < fieldNames.Count(); i++)
                //    dgClaimPaid.Columns[i].HeaderText = fieldNames[i];

                //dgClaimPaid.Columns[0].Width = 50;
                //dgClaimPaid.Columns[2].Width = 60;
                //dgClaimPaid.Columns[5].Width = 50;
                //dgClaimPaid.Columns[7].Width = 120;
                //dgClaimPaid.Columns[8].Width = 120;
                //dgClaimPaid.Columns[1].DefaultCellStyle.Format = "dd/mm/yyyy";
                //dgClaimPaid.Columns[10].DefaultCellStyle.Format = "dd/mm/yyyy";

                Cursor.Current = Cursors.AppStarting;
                lblTotal.Text = "Total Record(s): " + dgClaimPaid.Rows.Count.ToString();

                if (dgClaimPaid.Rows.Count > 50000) 
                {
                    Msgbox.Show("System allow to query only 50000 records,the result is exceeded there will be missing some records. Please contact IMS team to get full data.");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPayeeName.Text = "";
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
                    //DataSet dataReport = new DataSet();
                    //dataReport.Tables.Clear();
                    //dataReport.Tables.Add(dtTempt);
                    Reports.ClaimPaidReportPayee myDataReport = new Reports.ClaimPaidReportPayee();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

      
    }
}
