using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Testing
{
    public partial class CustomerProfitability : Form
    {
        public string UserName = "SICL";
        DataTable dt = new DataTable();
        CRUD crud = new CRUD();
        public CustomerProfitability()
        {
            InitializeComponent();
        }

       private  bool  CheckGroupCustomerExist() 
        {
            
                DataTable dtGroup = new DataTable();
                string sql = "select c.CUS_CODE from UW_M_CUSTOMERS c,RC_T_DEBIT_NOTE d where C.CUS_CODE= d.DEB_CUS_CODE and c.CUS_GRP_CODE in ('" + CboGroupCustomer.SelectedValue + "') ";
                        
                dtGroup = crud.ExecQuery(sql);
                if (dtGroup.Rows.Count > 0)
                    return true;
           
           return false;
        }
        private void butQuery_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = "";
                if (txtCusCode.Text.Trim().ToString() == "")
                {
                    Msgbox.Show("Customer Code must be input before seaching.");
                    return;
                }

                if (CboGroupCustomer.SelectedIndex != 0)
                {
                    if (CheckGroupCustomerExist() == false)
                    {
                        Msgbox.Show("There is no data for the selected Group Customer " +CboGroupCustomer.Text+ ".");
                        return;
                    }
                }
                System.Diagnostics.Stopwatch watch = new Stopwatch();
                watch.Start();
                string sql = "INSERT INTO user_print_history (user_name, print_datetime, filter2, type) VALUES ('" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + txtCusCode.Text + ";;;" + txtCusName.Text + "', '3')";
                crud.ExecNonQuery(sql);
                query();
                watch.Stop();
                label3.Text = watch.Elapsed.ToString();
            }
            catch (Exception ex) {
                Msgbox.Show(ex.Message);
            }
        }
        private void query()
        {
            try
            {
                string sql = "SELECT * FROM view_premium_claim where ";
             //   string Total;
                if (txtCusCode.Text.Trim() != "")
                    sql += " INSUREDCODE like '%" + txtCusCode.Text.Trim().ToUpper() + "%'";
                if (txtCusName.Text.Trim() != "")
                {
                    if (txtCusCode.Text.Trim() != "")
                        sql += " and";
                    sql += " INSUREDNAME like '%" + txtCusName.Text.Trim().ToUpper() + "%'";
                }

                if (CboGroupCustomer.SelectedIndex != 0)
                {
                    //if (txtCusName.Text.Trim() != "")
                    //    sql += " and";
                    sql += "and GroupCus in ('" + CboGroupCustomer.SelectedValue + "')";
                }
                Cursor.Current = Cursors.WaitCursor;
               
               // string oradb = "User Id=sicl;Password=sicl;Data Source=192.168.110.241:1521/infolive;";
              //  OracleConnection conn = new OracleConnection(oradb); // C#
              //  conn.Open();
                //OracleCommand cmd = new OracleCommand();
                //cmd.Connection = conn;
                //cmd.CommandText = sql;//"SELECT * FROM view_premium_claim where INSUREDCODE like '%" + txtCusCode.Text.ToUpper() + "%' OR INSUREDNAME LIKE '%" + txtCusName.Text.ToUpper() +"%'";
                //cmd.CommandType = CommandType.Text;
                //dt.Load(cmd.ExecuteReader());
                dt = new DataTable();

                //OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //da.Fill(dt);
                //da.Dispose();
                dt=crud.ExecQuery(sql);            

                //cmd.Connection.Close();

                RUW_PREMIUMANDCLAIM myDataReport = new RUW_PREMIUMANDCLAIM();
                myDataReport.SetDataSource(dt);
               // myDataReport.SetParameterValue("My Parameter", Total);
                crystalReportViewer1.ReportSource = myDataReport;
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex) 
            {
                Msgbox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCusCode.Text = "";
            txtCusName.Text = "";
            CboGroupCustomer.SelectedIndex = 0;
            label3.Text = "";
        }

        private void CustomerProfitability_Load(object sender, EventArgs e)
        {
            BindComboBox();
        }
        public void BindComboBox()
        {
            DataRow dr;
            string SQLcombox = "select GRP_CODE,GRP_DESCRIPTION from uw_r_groups order by GRP_CODE,GRP_DESCRIPTION";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            CboGroupCustomer.ValueMember = "GRP_CODE";
            CboGroupCustomer.DisplayMember = "GRP_DESCRIPTION";
            CboGroupCustomer.DataSource = dtCombox;
        }     
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dt.Columns.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(dt,"");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }

        private void CustomerProfitability_Activated(object sender, EventArgs e)
        {
            txtCusCode.Focus();
        }
    
    }
}
