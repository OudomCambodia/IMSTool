using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Testing.Forms
{
    public partial class frmPrintInvoice : Form
    {
        CRUD crud = new CRUD();
        DataRow dr;
        DataTable dt = new DataTable();

        string sql;
        public string UserName = "SICL";

        public frmPrintInvoice()
        {
            InitializeComponent();
        }

        private void frmPrintInvoice_Load(object sender, EventArgs e)
        {
            BindComboBoxBank();
            sql = "SELECT allow FROM USER_PRINT_TYPE WHERE TYPE = (SELECT TYPE FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + UserName + "')";
            string allow = crud.ExecQuery(sql).Rows[0].ItemArray[0].ToString();
            string[] splitAllow = allow.Split(',').ToArray();
            sql = "select * from USER_PRINT_ALLOW_BY_FORM Where REFERENCE_NO in (";
            foreach (string eachAllow in splitAllow)
                sql += "'" + eachAllow + "',";
            sql = sql.Remove(sql.Length - 1, 1);
            sql += ") and FORM_NAME = '" + this.Name + "'";
            DataTable allowRef = crud.ExecQuery(sql);
            foreach (DataRow drRef in allowRef.Rows)
            {
                if (this.Controls.Find(drRef[2].ToString(), true).Length > 0)
                {
                    ((Button)this.Controls.Find(drRef[2].ToString(), true)[0]).Enabled = true;
                    ((Button)this.Controls.Find(drRef[2].ToString(), true)[0]).BackColor = Color.FromArgb(0, 9, 47);
                }
            }

            dgvCoIn.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCoIn.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            Forms.frmCreateBank frmBank = new Forms.frmCreateBank();
            frmBank.frmPrintInvoice = this;
            frmBank.ShowDialog();
        }
        public void BindComboBox(string policyNo)
        {
            DataRow dr, dr1;
            //string SQLcombox = "select DEB_DEB_NOTE_NO ,DEB_DEB_NOTE_NO No from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + policyNo + "' order by CREATED_DATE DESC";
            string SQLcombox = "select DEB_DEB_NOTE_NO ,No from " +
            "( " +
            "select DEB_DEB_NOTE_NO ,DEB_DEB_NOTE_NO No, CREATED_DATE from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + policyNo + "' " +
            "union " +
            "select CRN_CREDIT_NOTE_NO ,CRN_CREDIT_NOTE_NO No, CREATED_DATE from RC_T_CREDIT_NOTE where CRN_POL_POLICY_NO='" + policyNo + "' " +
            ") " +
            "order by CREATED_DATE DESC";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            comBoxDebit.ValueMember = "DEB_DEB_NOTE_NO";
            comBoxDebit.DisplayMember = "NO";
            comBoxDebit.DataSource = dtCombox;
            //Bind combobox of Policy Transaction *All per policy
            string SQLTranAll = "SELECT POL_POLICY_NO,NVL(POL_ENDORSEMENT_NO,POL_POLICY_NO) POL_ENDORSEMENT_NO,POL_AUTHORIZED_DATE  FROM  ( " +
                                "select POL_POLICY_NO,POL_ENDORSEMENT_NO,POL_AUTHORIZED_DATE from UW_T_POLICIES WHERE POL_STATUS in (4,5,6,10) " +
                                "union " +
                                "select EDT_POLICY_NO,EDT_ENDORSEMENT_NO,EDT_AUTHORIZED_DATE from UW_T_ENDORSEMENTS where EDT_STATUS in (4,5,6,10) " +
                                "union " +
                                "select PHS_POLICY_NO,PHS_ENDORSEMENT_NO,PHS_AUTHORIZED_DATE from UW_H_POLICY_HISTORY where PHS_STATUS in (4,5,6,10) " +
                                "union " +
                                "select NDS_POLICY_NO,NDS_ENDORSEMENT_NO,NDS_AUTHORIZED_DATE from UW_H_ENDORSEMENT_HISTORY where NDS_STATUS in (4,5,6,10)) " +
                                "WHERE POL_POLICY_NO = '" + policyNo + "' ORDER BY POL_AUTHORIZED_DATE DESC";
            DataTable dtTranAll = new DataTable();
            dtTranAll = crud.ExecQuery(SQLTranAll);
            dr1 = dtTranAll.NewRow();
            dr1.ItemArray = new object[] { 0, "Select Transaction" };
            dtTranAll.Rows.InsertAt(dr1, 0);
            cbListAllTran.ValueMember = "POL_ENDORSEMENT_NO";
            cbListAllTran.DisplayMember = "POL_ENDORSEMENT_NO";
            cbListAllTran.DataSource = dtTranAll;
            //end of combobox of Policy Transaction *All per policy
        }
        public void BindComboBoxBank()
        {
            DataRow dr;
            string SQLcombox = "select BANK_ID, BANK_NAME, DEFAULT_BANK from USER_BANK_INFO";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            comboBank.ValueMember = "BANK_ID";
            comboBank.DisplayMember = "BANK_NAME";
            comboBank.DataSource = dtCombox;
            comboBank.SelectedValue = (from r in dtCombox.AsEnumerable()
                                       where r.Field<string>("DEFAULT_BANK") == "YES"
                                       select r.Field<decimal>("BANK_ID")).First<decimal>();
        }
        private void tbPolicyNo_Leave(object sender, EventArgs e)
        {
            try
            {
                BindComboBox(tbPolicyNo.Text.ToUpper());

            }

            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
       
        {
            dgvCoIn.DataSource = null;
            dgvCoIn.Columns.Clear();
            //if (!cbListAll.Checked) //Not list all banks
            //{
            //    if (comboBank.SelectedIndex == 0)
            //    {
            //        Msgbox.Show("Please select Bank Name!");
            //        return;
            //    }
            //    if (comBoxDebit.SelectedIndex != 0)
            //    {
            //        sql = "";

            //        Cursor.Current = Cursors.WaitCursor;

            //        sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEBIT_NOTE='" + comBoxDebit.SelectedValue + "'";

            //        dt = crud.ExecQuery(sql);

            //        DataTable dtTempt = new DataTable();
            //        dtTempt = dt.Copy();
            //        DataSet dataReport = new DataSet();
            //        dataReport.Tables.Clear();
            //        dataReport.Tables.Add(dtTempt);

            //        string sqlBank = "select * from USER_BANK_INFO where Bank_ID='" + comboBank.SelectedValue + "'";
            //        DataTable dtBank = new DataTable();
            //        dtBank = crud.ExecQuery(sqlBank);
            //        DataRow drBank = dtBank.Rows[0];

            //        DataRow dr = dt.Rows[0];
            //        if (dr[15].ToString().ElementAt(0) == 'E')
            //        {
            //            Reports.PrintInvoiceEnd myDataReport = new Reports.PrintInvoiceEnd();
            //            myDataReport.SetDataSource(dataReport);
            //            myDataReport.SetParameterValue("BANK", drBank[1].ToString());
            //            myDataReport.SetParameterValue("TRANSFERTO", drBank[2].ToString());
            //            myDataReport.SetParameterValue("ACCOUNTNO", drBank[3].ToString());
            //            crystalReportViewer1.ReportSource = myDataReport;


            //            //myDataReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"D:\ASD.pdf");
            //            //System.Diagnostics.Process.Start(@"D:\ASD.pdf");

            //        }
            //        else
            //        {
            //            Reports.PrintInvoice myDataReport = new Reports.PrintInvoice();
            //            myDataReport.SetDataSource(dataReport);
            //            myDataReport.SetParameterValue("BANK", drBank[1].ToString());
            //            myDataReport.SetParameterValue("TRANSFERTO", drBank[2].ToString());
            //            myDataReport.SetParameterValue("ACCOUNTNO", drBank[3].ToString());
            //            crystalReportViewer1.ReportSource = myDataReport;

            //            //myDataReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"D:\ASD.pdf");
            //            //System.Diagnostics.Process.Start(@"D:\ASD.pdf");
            //        }

            //        Cursor.Current = Cursors.AppStarting;
            //    }
            //    else
            //    {
            //        Msgbox.Show("Please select Debit Note No!");
            //    }
            //}
            //else
            //{
            if (comBoxDebit.SelectedIndex != 0 || cbListAllTran.SelectedIndex != 0)
            {
                sql = "";
                string cbSelect = cbListAllTran.SelectedValue.ToString().ToUpper();
                Cursor.Current = Cursors.WaitCursor;

                string note = comBoxDebit.Text;
                string Trans = cbListAllTran.SelectedValue.ToString();
                //string Trans1 = Trans.Substring(0, 20);
                //string Trans2 = Trans.Substring(21);
                //sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEBIT_NOTE='" + comBoxDebit.SelectedValue + "'";
                if (comBoxDebit.SelectedIndex != 0) {
                    sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEBIT_NOTE='" + note + "'";
                    sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEBIT_NOTE='" + note + "'";
                    
                }
                    
                else if (cbListAllTran.SelectedIndex != 0)
                {
                    if (Trans.Length > 20)
                    {
                        sql = "SELECT * FROM VIEW_PRINT_INVOICE where POL_NO='" + Trans.Substring(0, 20) + "' and ENDORSEMENT_NO=" + Trans.Substring(21) + "  order by DEB_ENDORSEMENT_NO DESC";
                    }
                    else
                    {
                        if (cbSelect[0] == 'R')
                            sql = "SELECT * FROM VIEW_PRINT_INVOICE where DEB_ENDORSEMENT_NO='" + Trans + "' order by DEB_ENDORSEMENT_NO DESC";
                        else
                            sql = "SELECT * FROM VIEW_PRINT_INVOICE where POL_NO='" + Trans + "' order by DEB_ENDORSEMENT_NO DESC";
                    }
                    
                }
                dt = crud.ExecQuery(sql);

                if (dt.Rows.Count <= 0)
                    Msgbox.Show("This transaction appears to have no content - Special Endorsement");
                else
                {
                    //var cusName = dt.Rows[0]["NAME"].ToString();
                    //dt.Rows[0]["NAME"] = cusName + "⠀⠀";

                    dt.Columns.Add("EXCHANGE_RATE", typeof(System.String));
                    dt.Columns.Add("TOTAL_FUND_KH", typeof(System.String));
                    dt.Columns.Add("KH_NAME", typeof(System.String));
                    dt.Columns.Add("KH_ADDR", typeof(System.String));

                    //DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "'");
                    DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + dt.Rows[0]["EX_TRAN_DATE"].ToString() + "'");
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

                    foreach (DataRow row in dt.Rows)
                    {
                        row["EXCHANGE_RATE"] = String.Format("{0:N}", ExchangeRate).Replace(".00", "");
                        //row["TOTAL_FUND_KH"] = String.Format("{0:N}", Convert.ToDouble(row["TOTAL_FUND"]) * ExchangeRate)  ;
                        row["TOTAL_FUND_KH"] = String.Format("{0:N}", Decimal.Round(Convert.ToDecimal(row["TOTAL_FUND"]) * ExchangeRate, 0));
                        row["KH_NAME"] = KhName;
                        row["KH_ADDR"] = KhAddr;
                    }

                    DataRow dr = dt.Rows[0];

                    var polNo = dr[8].ToString().Split('/');
                    var po = string.Concat(polNo[0], " ", "/", polNo[1], " ", "/", polNo[2], " ", "/", polNo[3], " ", "/", polNo[4]);
                    dr[8] = po;

                    var dncnNo = dr[4].ToString().Split('/');
                    var d = string.Concat(dncnNo[0], " ", "/", dncnNo[1], " ", "/", dncnNo[2], " ", "/", dncnNo[3]);
                    dr[4] = d;

                    var accountCode = dr[6].ToString().Split('/');
                    var ac = string.Concat(accountCode[0], " ", "/", accountCode[1], " ", "/", accountCode[2]);
                    dr[6] = ac;

                    var accYear = dr[7].ToString().Split('/');
                    dt.Columns[7].MaxLength = 50;
                    var ay = string.Concat(accYear[0], " ", "/", accYear[1]);
                    dr[7] = ay;

                    if (dnNumber[0] == 'D') //Debit Note
                    {
                        string accountcode = dr["ACCOUNT_CODE"].ToString();
                  
                        string producer = accountcode.Split('/')[1].Trim(), cuscode = accountcode.Split('/')[2].Trim();
                        //update add condition for US Embasssy only as requested from J Cheata - update Southeane Email 28-09-2022
                        if (dt.Rows[0]["CHK_USEMBASSY"].ToString() == "N/A" )
                            dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");
                        else if (dt.Rows[0]["CHK_USEMBASSY"].ToString() == "US_EMBASSY")
                            dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = 'C000040017'");
                        else if (dt.Rows[0]["CHK_USEMBASSY"].ToString() == "DGB")
                            dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = 'C000147860'");
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
                                    Reports.NewInvoiceNAEndo myDataReport = new Reports.NewInvoiceNAEndo();
                                    myDataReport.SetDataSource(dt);
                                    crystalReportViewer1.ReportSource = myDataReport;
                                }
                                else
                                {
                                    Reports.NewInvoiceNA myDataReport = new Reports.NewInvoiceNA();
                                    myDataReport.SetDataSource(dt);
                                    crystalReportViewer1.ReportSource = myDataReport;
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
                                dt.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                                dtTemp.TableName = "PAYMENT_INSTRUCTION";
                                ds.Tables.Add(dt);
                                ds.Tables.Add(dtTemp);
                                
                                if (dr["ENDORSEMENT_NO"].ToString() != "")
                                {
                                    Reports.NewInvoiceEndo myDataReport = new Reports.NewInvoiceEndo();
                                    myDataReport.SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = myDataReport;
                                }
                                else
                                {
                                    Reports.NewInvoice myDataReport = new Reports.NewInvoice();
                                    myDataReport.SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = myDataReport;
                                }
                            }
                        }
                        else //use List All
                        {
                            if (dr["ENDORSEMENT_NO"].ToString() != "")
                            {
                                Reports.PrintInvoiceEndListAll myDataReport = new Reports.PrintInvoiceEndListAll();
                                myDataReport.SetDataSource(dt);
                                crystalReportViewer1.ReportSource = myDataReport;
                            }
                            else
                            {
                                Reports.PrintInvoiceListAll myDataReport = new Reports.PrintInvoiceListAll();
                                myDataReport.SetDataSource(dt);
                                crystalReportViewer1.ReportSource = myDataReport;
                            }
                        }
                    }
                    else if (dnNumber[0] == 'C') //Credit Note
                    {
                        //if (dr[15].ToString().ElementAt(0) == 'E')
                        if (dr["ENDORSEMENT_NO"].ToString() != "")
                        {
                            Reports.CreditNoteEnd myDataReport = new Reports.CreditNoteEnd();
                            myDataReport.SetDataSource(dt);
                            crystalReportViewer1.ReportSource = myDataReport;
                        }
                        else
                        {
                            Reports.CreditNote myDataReport = new Reports.CreditNote();
                            myDataReport.SetDataSource(dt);
                            crystalReportViewer1.ReportSource = myDataReport;
                        }
                    }


                    //Set CoIn Info
                    string cmd = "select (select INC_INSCOM_DESC from SM_R_INSURANCE_COM where INC_INSCOM_CODE = PCI_INS_CODE) CO_PARTY,nvl(SHARE_PER,0) SHARE_PER from ( "+
                    "select PCI_POL_SEQ_NO,PCI_INS_CODE,sum(PCI_SHARE_PCNTG) SHARE_PER "+
                    "from UW_T_POL_COINS_INFO  "+
                    "where PCI_POL_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '"+dnNumber+"' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '"+dnNumber+"')  "+
                    "group by PCI_POL_SEQ_NO,PCI_INS_CODE "+
                    "union "+
                    "select ECI_EDT_SEQ_NO,ECI_INS_CODE,sum(ECI_SHARE_PCNTG) SHARE_PER "+
                    "from UW_T_END_COINS_INFO  "+
                    "where ECI_EDT_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '"+dnNumber+"' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '"+dnNumber+"')   "+
                    "group by ECI_EDT_SEQ_NO,ECI_INS_CODE "+
                    "union "+
                    "select HCI_PHS_SEQ_NO,HCI_INS_CODE, sum(HCI_SHARE_PCNTG) SHARE_PER "+
                    "from UW_H_HIST_COINS_INFO  "+
                    "where HCI_PHS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '"+dnNumber+"' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '"+dnNumber+"')   "+
                    "group by HCI_PHS_SEQ_NO,HCI_INS_CODE "+
                    "union "+
                    "select NCI_NDS_SEQ_NO,NCI_INS_CODE,sum(NCI_SHARE_PCNTG) SHARE_PER "+
                    "from UW_H_EHIST_COINS_INFO  "+
                    "where NCI_NDS_SEQ_NO = (select DEB_POL_SEQ_NO from RC_T_DEBIT_NOTE where DEB_DEB_NOTE_NO = '"+dnNumber+"' union select CRN_POL_SEQ_NO from RC_T_CREDIT_NOTE where CRN_CREDIT_NOTE_NO = '"+dnNumber+"')   " +
                    "group by NCI_NDS_SEQ_NO,NCI_INS_CODE)";

                    dtTemp = crud.ExecQuery(cmd);
                    if (dtTemp.Rows.Count > 0)
                    {
                        dgvCoIn.DataSource = dtTemp;
                    }
                    //

                }
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                // Msgbox.Show("Please select Debit Note No!");
                Msgbox.Show("Please select at least one option Transaction or Debit Note");
            }
            //}

        }

        private void cbListAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbListAll.Checked)
                comboBank.Enabled = false;
            else
                comboBank.Enabled = true;

        }



        private void BHPLetterPrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (BHPLetterPrnt.Checked)
            {
                if (tbPolicyNo.Text == "")
                {
                    Msgbox.Show("Policy number is required");
                }
                else
                {
                    try
                    {
                        string polEnd = cbListAllTran.SelectedValue.ToString().ToUpper();
                        if (polEnd[0] == 'R')
                        {
                            polEnd = tbPolicyNo.Text.ToUpper();
                        }

                        //string sqlBHP = "SELECT (CASE WHEN SUBSTR(POL_ENDORSEMENT_NO,1,1)='R' OR POL_ENDORSEMENT_NO is null THEN POL_POLICY_NO ELSE POL_ENDORSEMENT_NO END) POLICY_NO FROM UW_T_POLICIES WHERE POL_POLICY_NO = " + "'" + tbPolicyNo.Text.ToUpper() + "'" + "ORDER BY POL_AUTHORIZED_DATE DESC";
                        //DataTable dtBHP = crud.ExecQuery(sqlBHP);
                        //string polEnd = dtBHP.Rows[0]["POLICY_NO"].ToString().ToUpper();
                        //Print BHP Acknowledgement Letter
                        if (BHPLetterPrnt.Checked)
                        {
                            string ProType = tbPolicyNo.Text.Substring(6, 4).ToUpper();

                            if (ProType == "HBHP" && cbListAllTran.SelectedIndex != 0)
                            {
                                Reports.BHPLetter bhp_rpt = new Reports.BHPLetter();
                                bhp_rpt.SetParameterValue("POL_NO", polEnd);
                                var BHP_frm = new frmViewInstructionNote();
                                BHP_frm.Text = "BHPLetter";
                                BHP_frm.rpt = bhp_rpt;
                                BHP_frm.Show();
                            }
                            else if (cbListAllTran.SelectedIndex == 0)
                            {
                                Msgbox.Show("This transaction appears to have no content - Transaction Required");
                            }
                            else
                            {
                                Msgbox.Show("This transaction appears to have no content - Applicable for Figtree Blue Product Only");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Msgbox.Show(ex.Message);
                    }
                }
            }

            BHPLetterPrnt.Checked = false;


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPolicyNo.Clear();
            
            cbListAllTran.DataSource = null;
            cbListAllTran.Text = "Select Transaction";
            
            comBoxDebit.DataSource = null;
            comBoxDebit.Text = "Select ALL";
        }

        private void dgvCoIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvCoIn);
        }

    }
}
