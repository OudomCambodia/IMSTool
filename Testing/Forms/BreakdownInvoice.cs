using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Testing.Forms
{
    public partial class BreakdownInvoice : Form 
    {
        public string Username = "Admin";
        public  DataTable dt;
        CRUD crud = new CRUD();
        int button_id=0;
        public BreakdownInvoice()
        {
            InitializeComponent();
        }

        private void BreakdownInvoice_Load(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvInvoiceDetails);
            this.dgvInvoiceDetails.ForeColor = System.Drawing.Color.Black;
            this.dgvInvoiceDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            frmDocumentControl.disabledButt(btnIssue);
        }

        private void btnOptionI_Click(object sender, EventArgs e)
        {

            if (tbInvoiceNo.Text == "" || tbInvoiceNo.Text.Substring(0,1).ToUpper()=="C" )
            {
                Msgbox.Show("Invoice No is required OR Can not issue Breakdown Invoice for CreditNote! ");
            }
            else
            {
                btnIssue.Enabled = true;
                if (chkInvoice())
                {
                    dgvInvoiceDetails.Columns.Clear();
                    string[] dgvClName = { "INVOICENO","INSURED", "SUMINSURED", "GROSSPREMIUM", "ADMINFEE" };
                    loadOption(dgvClName, 5);
                    //int rowId = dgvInvoiceDetails.Rows.Add();
                    //DataGridViewRow row = dgvInvoiceDetails.Rows[rowId];
                }
                else
                {
                    Msgbox.Show("This transaction appears to have no content - Special Endorsement or Wrong Invoice Number");

                }
            }
            button_id = 1;
           
            
        }
        void loadOption(string[] a,int columnno)
        {
           
            for (int i = 0; i < columnno;i++ )
            {
                dgvInvoiceDetails.Columns.Add("cl"+a[i], a[i]);
                
            }
           
        }

        private void btnOptionII_Click(object sender, EventArgs e)
        {
            if(tbInvoiceNo.Text==""){
                Msgbox.Show("Invoice No is required!");
            }
            else
            {
                btnIssue.Enabled = true;
                if (chkInvoice())
                {
                    dgvInvoiceDetails.Columns.Clear();
                    string[] dgvClName = { "INVOICENO","INSURED", "ADDRESS", "NUMBER", "SUMINSURED", "GROSSPREMIUM", "ADMINFEE" };
                    loadOption(dgvClName, 7);
                }
                else
                {
                    Msgbox.Show("This transaction appears to have no content - Special Endorsement or Wrong Invoice Number");

                }
            }
            button_id = 2;
            
        }
        bool chkInvoice()
        {
            string sql = "";
           
                sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEBIT_NOTE='" + tbInvoiceNo.Text.ToUpper() + "'";
                dt = crud.ExecQuery(sql);
                if (dt.Rows.Count <= 0 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            
            
                

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            #region OptionI Invoice
            //Option I
            frmViewInstructionNote frmReport = new frmViewInstructionNote();
            ReportClass rpt = new ReportClass();
            dt.Columns.Add("EXCHANGE_RATE", typeof(System.String));
            dt.Columns.Add("TOTAL_FUND_KH", typeof(System.String));
            dt.Columns.Add("KH_NAME", typeof(System.String));
            dt.Columns.Add("KH_ADDR", typeof(System.String));
            
            //dt.Columns.Add("", typeof(System.String));
            
            //DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "'");
            DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + dt.Rows[0]["TRAN_DATE"].ToString() + "'");
            int ExchangeRate = 0;
            if (dtTemp.Rows.Count > 0)
            {
                ExchangeRate = Convert.ToInt32(dtTemp.Rows[0][0]);
            }


            string KhName = "", KhAddr = "", dnNumber = dt.Rows[0]["DEBIT_NOTE"].ToString().ToUpper();
            //dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = " +
            //    "(SELECT DEB_CUS_CODE FROM RC_T_DEBIT_NOTE WHERE DEB_DEB_NOTE_NO = '" + note + "' " +
            //    "union SELECT CRN_CUS_CODE FROM RC_T_CREDIT_NOTE WHERE CRN_CREDIT_NOTE_NO = '" + note + "') ");
            dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = " +
               "(SELECT DEB_CUS_CODE FROM RC_T_DEBIT_NOTE WHERE DEB_DEB_NOTE_NO = '" + dnNumber + "' " +
               "union SELECT CRN_CUS_CODE FROM RC_T_CREDIT_NOTE WHERE CRN_CREDIT_NOTE_NO = '" + dnNumber + "') ");
            if (dtTemp.Rows.Count > 0)
            {
                KhName = dtTemp.Rows[0]["CUS_NAME"].ToString();
                KhAddr = dtTemp.Rows[0]["CUS_ADDR"].ToString();
            }
            //passing datagridview data to datable
            DataTable dtforCopy = new DataTable();
            DataRow dtRorforcopy;
            
            foreach (DataColumn dtCol in dt.Columns)
            {
                dtforCopy.Columns.Add(dtCol.ColumnName);
            }
            foreach (DataGridViewColumn col in dgvInvoiceDetails.Columns)
            {
                //if (col.Name == "clSUMINSURED" || col.Name == "clADMINFEE" || col.Name == "GROSSPREMIUM")
                //    dtforCopy.Columns.Add(col.Name,typeof(System.Double));
                //else
                    dtforCopy.Columns.Add(col.Name, typeof(System.String));
            }
            dtforCopy.Columns.Add("clFULLPREMIUM", typeof(System.String));
            for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
            {

                foreach (DataRow temp in dt.Rows)
                {
                    dtforCopy.ImportRow(temp);
                }
                foreach (DataGridViewColumn coldgv in dgvInvoiceDetails.Columns)
                {
                    if (coldgv.Name == "clSUMINSURED" || coldgv.Name == "clADMINFEE" || coldgv.Name == "clGROSSPREMIUM")
                    {
                        dtforCopy.Rows[i][coldgv.Name] = dgvInvoiceDetails[coldgv.Name, i].Value.ToString();

                    }


                            //Convert.ToDouble(dgvInvoiceDetails[coldgv.Name, i].Value.ToString());
                    else
                        dtforCopy.Rows[i][coldgv.Name] = dgvInvoiceDetails[coldgv.Name, i].Value.ToString();
                }
                dtforCopy.Rows[i]["clFULLPREMIUM"] = Convert.ToDouble(dtforCopy.Rows[i]["clADMINFEE"].ToString()) + Convert.ToDouble(dtforCopy.Rows[i]["clGROSSPREMIUM"].ToString());
            }

            //-------------------------------------------------------//
            foreach (DataRow row in dtforCopy.Rows)
                {
                    row["EXCHANGE_RATE"] = String.Format("{0:N}", ExchangeRate).Replace(".00", "");
                    row["TOTAL_FUND_KH"] = String.Format("{0:N}", Convert.ToDouble(row["clFULLPREMIUM"]) * ExchangeRate);
                    row["KH_NAME"] = KhName;
                    row["KH_ADDR"] = KhAddr;
                    row["clSUMINSURED"] = String.Format("{0:N}", Convert.ToDouble(row["clSUMINSURED"]));
                    row["clADMINFEE"] = String.Format("{0:N}", Convert.ToDouble(row["clADMINFEE"]));
                    row["clGROSSPREMIUM"] = String.Format("{0:N}", Convert.ToDouble(row["clGROSSPREMIUM"]));
                    row["clFULLPREMIUM"] = String.Format("{0:N}", Convert.ToDouble(row["clFULLPREMIUM"]));
                }
                
            DataRow dr = dt.Rows[0];
            #region check DebitNote Option I
            if (dnNumber[0] == 'D') //Debit Note
            {
                string accountcode = dr["ACCOUNT_CODE"].ToString();
                string producer = accountcode.Split('/')[1], cuscode = accountcode.Split('/')[2];
                dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");

               
                
                if (dtTemp.Rows.Count > 0) //has payment instruction set
                {

                    ////check for N/A
                    DataRow[] ToDelete = dtTemp.Select("BANK_NAME = 'N/A'"); //create this cuz cannot modify data directly in foreach
                    foreach (DataRow row in ToDelete)
                    {
                        dtTemp.Rows.Remove(row); //remove N/A
                    }
                    //

                    if (dtTemp.Rows.Count == 0) //that's mean has only N/A record => use NewInvoice for NA
                    {
                        if (dr["ENDORSEMENT_NO"].ToString() != "")
                        {
                           
                            Reports.NewInvoiceNAEndoBI myDataReport = new Reports.NewInvoiceNAEndoBI();
                            
                            myDataReport.SetDataSource(dtforCopy);
                            //crystalReportViewer1.ReportSource = myDataReport;
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                        else
                        {
                            Reports.NewInvoiceNABI myDataReport = new Reports.NewInvoiceNABI();
                            myDataReport.SetDataSource(dtforCopy);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                    }
                    else //after remove N/A still has other bank records => use NewInvoice with Payment instruction bank table
                    {
                        dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String)); //Add in order to link to another table in Report
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            row["DEBIT_NOTE"] = dr["DEBIT_NOTE"].ToString();
                        }
                        DataSet ds = new DataSet();
                        dtforCopy.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                        dtTemp.TableName = "PAYMENT_INSTRUCTION";
                        ds.Tables.Add(dtforCopy);
                        ds.Tables.Add(dtTemp);

                        if (dr["ENDORSEMENT_NO"].ToString() != "")
                        {
                            Reports.NewInvoiceEndoBI myDataReport = new Reports.NewInvoiceEndoBI();
                            myDataReport.SetDataSource(ds);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                        else
                        {
                            Reports.NewInvoiceBI myDataReport = new Reports.NewInvoiceBI();
                            myDataReport.SetDataSource(ds);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                    }
                }
                else //use List All
                {
                    if (dr["ENDORSEMENT_NO"].ToString() != "")
                    {
                        Reports.PrintInvoiceEndListAllBI myDataReport = new Reports.PrintInvoiceEndListAllBI();
                        myDataReport.SetDataSource(dtforCopy);
                        var frm = new frmViewInstructionNote();
                        frm.rpt = myDataReport;
                        frm.Show();
                    }
                    else
                    {
                        Reports.PrintInvoiceListAllBI myDataReport = new Reports.PrintInvoiceListAllBI();
                        myDataReport.SetDataSource(dtforCopy);
                        var frm = new frmViewInstructionNote();
                        frm.rpt = myDataReport;
                        frm.Show();
                    }
                }
            }
            #endregion

            #region check DebitNote Option II
            if (dnNumber[0] == 'D') //Debit Note
            {
                string accountcode = dr["ACCOUNT_CODE"].ToString();
                string producer = accountcode.Split('/')[1], cuscode = accountcode.Split('/')[2];
                dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");



                if (dtTemp.Rows.Count > 0) //has payment instruction set
                {

                    ////check for N/A
                    DataRow[] ToDelete = dtTemp.Select("BANK_NAME = 'N/A'"); //create this cuz cannot modify data directly in foreach
                    foreach (DataRow row in ToDelete)
                    {
                        dtTemp.Rows.Remove(row); //remove N/A
                    }
                    //

                    if (dtTemp.Rows.Count == 0) //that's mean has only N/A record => use NewInvoice for NA
                    {
                        if (dr["ENDORSEMENT_NO"].ToString() != "")
                        {

                            Reports.NewInvoiceNAEndoBI2 myDataReport = new Reports.NewInvoiceNAEndoBI2();

                            myDataReport.SetDataSource(dtforCopy);
                            //crystalReportViewer1.ReportSource = myDataReport;
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                        else
                        {
                            Reports.NewInvoiceNABI2 myDataReport = new Reports.NewInvoiceNABI2();
                            myDataReport.SetDataSource(dtforCopy);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                    }
                    else //after remove N/A still has other bank records => use NewInvoice with Payment instruction bank table
                    {
                        dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String)); //Add in order to link to another table in Report
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            row["DEBIT_NOTE"] = dr["DEBIT_NOTE"].ToString();
                        }
                        DataSet ds = new DataSet();
                        dtforCopy.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                        dtTemp.TableName = "PAYMENT_INSTRUCTION";
                        ds.Tables.Add(dtforCopy);
                        ds.Tables.Add(dtTemp);

                        if (dr["ENDORSEMENT_NO"].ToString() != "")
                        {
                            Reports.NewInvoiceEndoBI2 myDataReport = new Reports.NewInvoiceEndoBI2();
                            myDataReport.SetDataSource(ds);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                        else
                        {
                            Reports.NewInvoiceBI2 myDataReport = new Reports.NewInvoiceBI2();
                            myDataReport.SetDataSource(ds);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = myDataReport;
                            frm.Show();
                        }
                    }
                }
                else //use List All
                {
                    if (dr["ENDORSEMENT_NO"].ToString() != "")
                    {
                        Reports.PrintInvoiceEndListAllBI2 myDataReport = new Reports.PrintInvoiceEndListAllBI2();
                        myDataReport.SetDataSource(dtforCopy);
                        var frm = new frmViewInstructionNote();
                        frm.rpt = myDataReport;
                        frm.Show();
                    }
                    else
                    {
                        Reports.PrintInvoiceListAllBI2 myDataReport = new Reports.PrintInvoiceListAllBI2();
                        myDataReport.SetDataSource(dtforCopy);
                        var frm = new frmViewInstructionNote();
                        frm.rpt = myDataReport;
                        frm.Show();
                    }
                }
            }
            #endregion
            //else if (dnNumber[0] == 'C') //Credit Note
            //{
            //    //if (dr[15].ToString().ElementAt(0) == 'E')
            //    if (dr["ENDORSEMENT_NO"].ToString() != "")
            //    {
            //        Reports.CreditNoteEnd myDataReport = new Reports.CreditNoteEnd();
            //        myDataReport.SetDataSource(dt);
            //        var frm = new frmViewInstructionNote();
            //        frm.rpt = myDataReport;
            //        frm.Show();
            //    }
            //    else
            //    {
            //        Reports.CreditNote myDataReport = new Reports.CreditNote();
            //        myDataReport.SetDataSource(dt);
            //        var frm = new frmViewInstructionNote();
            //        frm.rpt = myDataReport;
            //        frm.Show();
            //    }
            //}
            ///end of option one
            #endregion
           
        }

        

       

        

      

        private void cus_button1_Click_1(object sender, EventArgs e)
        {
            if (dgvInvoiceDetails.ColumnCount <= 0)
            {
                return;
            }
                
            else
            {
                var index = this.dgvInvoiceDetails.Rows.Add();
                this.dgvInvoiceDetails.Rows[index].Cells[0].Value = Convert.ToString(Convert.ToInt32(index) + 1);
            }
            
        
        }

        private void btnRowMinus_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceDetails.ColumnCount <= 0)
            {
                return;
            }
            else
            {
                foreach (DataGridViewCell oneCell in dgvInvoiceDetails.SelectedCells)
                {
                    if (oneCell.Selected)
                        dgvInvoiceDetails.Rows.RemoveAt(oneCell.RowIndex);
                }
            }
            
        }

        

        

        
       
       

        
    }
}
