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
    public partial class frmPrintInvoiceByBatchNo : Form
    {
        public string UserName = "SICL";

        private CRUD crud = new CRUD();

        private DataTable dtNewInvoiceNAEndo = new DataTable();
        private DataTable dtNewInvoiceNA = new DataTable();
        private DataTable dtNewInvoiceEndo = new DataTable();
        private DataTable dtNewInvoice = new DataTable();
        private DataTable dtInvoiceEndoListAll = new DataTable();
        private DataTable dtInvoiceListAll = new DataTable();

        private DataTable dtTempNewInvoiceEndo = new DataTable();
        private DataTable dtTempNewInvoice = new DataTable();

        private DataTable dtDebNote = new DataTable();

        public frmPrintInvoiceByBatchNo()
        {
            InitializeComponent();
        }

        private void frmPrintInvoiceByBatchNo_Load(object sender, EventArgs e)
        {
            rdbNo.Checked = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var qBuilder = new StringBuilder();
            qBuilder.Append("SELECT PUL_REF_NO BATCH_NUM, PUL_POL_REF_NO REF_NO, POL_CLA_CODE MAIN_CLASS, POL_PRD_CODE SUB_CLASS, POL_POLICY_NO POLICY_NO, DEB_DEB_NOTE_NO DEBIT_NOTE_NO, POL_SUM_INSURED SUM_INSURED, POL_TOTAL_PREMIUM PREMIUM ")
                .AppendFormat("FROM (SELECT PUL_REF_NO, TO_NUMBER(PUL_POL_REF_NO DEFAULT NULL ON CONVERSION ERROR) PUL_POL_REF_NO, PUL_DESC FROM UW_T_POL_UPLOAD_LOG WHERE PUL_REF_NO = '{0}') ", txtBatchNo.Text.Trim())
                .Append("LEFT OUTER JOIN UW_T_POLICIES ON POL_PROPOSAL_NO = PUL_DESC ")
                .Append("LEFT OUTER JOIN RC_T_DEBIT_NOTE ON DEB_POL_SEQ_NO = POL_SEQ_NO ")
                .Append("ORDER BY PUL_POL_REF_NO");

            dtDebNote = crud.ExecQuery(qBuilder.ToString());
            dgvBathData.DataSource = dtDebNote;

            Cursor.Current = Cursors.Arrow;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool isPrinted = false;

            bool isCloned = false;
            bool isClonedTemp = false;

            DataTable dtInvoiceToPrint = new DataTable();
            DataTable dtTempToPrint = new DataTable();

            if (dtDebNote == null || dtDebNote.Rows.Count <= 0)
            {
                Msgbox.Show("No batch no. found!");
                return;
            }

            for (int i = 0; i < dtDebNote.Rows.Count; i++)
            {
                string debNote = dtDebNote.Rows[i]["DEBIT_NOTE_NO"].ToString();
                string mainClass = dtDebNote.Rows[i]["MAIN_CLASS"].ToString();

                if (string.IsNullOrEmpty(debNote))
                    continue;

                var dtInvoiceInfo = crud.ExecQuery("SELECT * FROM VIEW_PRINT_INVOICE WHERE DEBIT_NOTE = '" + debNote + "'");
                if (dtInvoiceInfo.Rows.Count > 0)
                {
                    dtInvoiceInfo.Columns.Add("COI", typeof(System.String));
                    dtInvoiceInfo.Columns.Add("IS_PRINTED_COI", typeof(System.String));

                    if (rdbYes.Checked && mainClass.Equals("PROP"))
                    {
                        var qBuilder = new StringBuilder();
                        qBuilder.Append("SELECT PCI_CHAR_VALUE COI ")
                            .Append("FROM UW_T_POL_COMMON_INFORMATION utpci ")
                            .AppendFormat("WHERE utpci.PCI_POL_SEQ_NO = (SELECT DEB_POL_SEQ_NO FROM RC_T_DEBIT_NOTE WHERE DEB_DEB_NOTE_NO = '{0}') ", debNote)
                            .Append("AND utpci.PCI_DESCRIPTION LIKE '%CERTIFICATE NO%' ")
                            .Append("AND ROWNUM = 1");

                        var dtCoi = crud.ExecQuery(qBuilder.ToString());
                        if (dtCoi.Rows.Count > 0)
                        {
                            dtInvoiceInfo.Rows[0]["COI"] = dtCoi.Rows[0]["COI"].ToString();
                            dtInvoiceInfo.Rows[0]["IS_PRINTED_COI"] = ((rdbYes.Checked && mainClass.Equals("PROP")) ? "TRUE" : "FALSE");
                        }
                        else
                        {
                            dtInvoiceInfo.Rows[0]["COI"] = string.Empty;
                            dtInvoiceInfo.Rows[0]["IS_PRINTED_COI"] = "FALSE";
                        }
                    }
                    else
                    {
                        dtInvoiceInfo.Rows[0]["COI"] = string.Empty;
                        dtInvoiceInfo.Rows[0]["IS_PRINTED_COI"] = "FALSE";
                    }

                    dtInvoiceInfo.Columns.Add("EXCHANGE_RATE", typeof(System.String));
                    dtInvoiceInfo.Columns.Add("TOTAL_FUND_KH", typeof(System.String));
                    dtInvoiceInfo.Columns.Add("KH_NAME", typeof(System.String));
                    dtInvoiceInfo.Columns.Add("KH_ADDR", typeof(System.String));

                    DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + dtInvoiceInfo.Rows[0]["EX_TRAN_DATE"].ToString() + "'");

                    int ExchangeRate = 0;
                    if (dtTemp.Rows.Count > 0)
                    {
                        ExchangeRate = Convert.ToInt32(dtTemp.Rows[0][0]);
                    }

                    string KhName = "", KhAddr = "";
                    dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = " +
                       "(SELECT DEB_CUS_CODE FROM RC_T_DEBIT_NOTE WHERE DEB_DEB_NOTE_NO = '" + debNote + "' " +
                       "union SELECT CRN_CUS_CODE FROM RC_T_CREDIT_NOTE WHERE CRN_CREDIT_NOTE_NO = '" + debNote + "') ");

                    if (dtTemp.Rows.Count > 0)
                    {
                        KhName = dtTemp.Rows[0]["CUS_NAME"].ToString();
                        KhAddr = dtTemp.Rows[0]["CUS_ADDR"].ToString();

                        if (!dtTemp.Columns.Contains("DEBIT_NOTE"))
                            dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String));

                        dtTemp.Rows[0]["DEBIT_NOTE"] = debNote;
                    }

                    foreach (DataRow row in dtInvoiceInfo.Rows)
                    {
                        row["EXCHANGE_RATE"] = String.Format("{0:N}", ExchangeRate).Replace(".00", "");
                        row["TOTAL_FUND_KH"] = String.Format("{0:N}", Decimal.Round(Convert.ToDecimal(row["TOTAL_FUND"]) * ExchangeRate, 0));
                        row["KH_NAME"] = KhName;
                        row["KH_ADDR"] = KhAddr;
                    }

                    DataRow dr = dtInvoiceInfo.Rows[0];

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
                    dtInvoiceInfo.Columns[7].MaxLength = 50;
                    var ay = string.Concat(accYear[0], " ", "/", accYear[1]);
                    dr[7] = ay;

                    string accountcode = dr["ACCOUNT_CODE"].ToString();
                    string producer = accountcode.Split('/')[1].Trim(), cuscode = accountcode.Split('/')[2].Trim();

                    if (dtInvoiceInfo.Rows[0]["CHK_USEMBASSY"].ToString() == "N/A")
                        dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");
                    else if (dtInvoiceInfo.Rows[0]["CHK_USEMBASSY"].ToString() == "US_EMBASSY")
                        dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = 'C000040017'");
                    else if (dtInvoiceInfo.Rows[0]["CHK_USEMBASSY"].ToString() == "DGB")
                        dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = 'C000147860'");

                    if (!isCloned)
                    {
                        dtNewInvoiceNAEndo = dtInvoiceInfo.Clone();
                        dtNewInvoiceNA = dtInvoiceInfo.Clone();
                        dtNewInvoiceEndo = dtInvoiceInfo.Clone();
                        dtNewInvoice = dtInvoiceInfo.Clone();
                        dtInvoiceEndoListAll = dtInvoiceInfo.Clone();
                        dtInvoiceListAll = dtInvoiceInfo.Clone();

                        isCloned = true;
                    }

                    if (dtInvoiceInfo.Rows.Count > 0)
                    {
                        if (dtTemp.Rows.Count > 0)
                        {
                            //check for N/A
                            DataRow[] ToDelete = dtTemp.Select("BANK_NAME = 'N/A'"); //create this cuz cannot modify data directly in foreach
                            foreach (DataRow row in ToDelete)
                            {
                                dtTemp.Rows.Remove(row); //remove N/A
                            }

                            if (dtTemp.Rows.Count == 0) //that's mean has only N/A record => use NewInvoice for NA
                            {
                                if (dr["ENDORSEMENT_NO"].ToString() != "")
                                    dtNewInvoiceEndo.ImportRow(dtInvoiceInfo.Rows[0]);
                                else
                                    dtNewInvoiceNA.ImportRow(dtInvoiceInfo.Rows[0]);
                            }
                            else //after remove N/A still has other bank records => use NewInvoice with Payment instruction bank table
                            {
                                if (!dtTemp.Columns.Contains("DEBIT_NOTE"))
                                    dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String)); //Add in order to link to another table in Report

                                if (!isClonedTemp)
                                {
                                    dtTempNewInvoiceEndo = dtTemp.Clone();
                                    dtTempNewInvoice = dtTemp.Clone();
                                    isClonedTemp = true;
                                }

                                foreach (DataRow row in dtTemp.Rows)
                                {
                                    row["DEBIT_NOTE"] = dr["DEBIT_NOTE"].ToString();
                                }

                                if (dr["ENDORSEMENT_NO"].ToString() != "")
                                {
                                    dtNewInvoiceEndo.ImportRow(dtInvoiceInfo.Rows[0]);
                                    dtTempNewInvoiceEndo.ImportRow(dtTemp.Rows[0]);
                                }
                                else
                                {
                                    dtNewInvoice.ImportRow(dtInvoiceInfo.Rows[0]);
                                    dtTempNewInvoice.ImportRow(dtTemp.Rows[0]);
                                }
                            }
                        }
                        else //use List All
                        {
                            if (dr["ENDORSEMENT_NO"].ToString() != "")
                                dtInvoiceEndoListAll.ImportRow(dtInvoiceInfo.Rows[0]);
                            else
                                dtInvoiceListAll.ImportRow(dtInvoiceInfo.Rows[0]);
                        }
                    }
                }
            }

            if (dtNewInvoiceNAEndo.Rows.Count > 0)
            {
                isPrinted = true;

                Reports.NewInvoiceNAEndo myDataReport = new Reports.NewInvoiceNAEndo();
                myDataReport.SetDataSource(dtNewInvoiceNAEndo);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (dtNewInvoiceNA.Rows.Count > 0)
            {
                isPrinted = true;

                Reports.NewInvoiceNA myDataReport = new Reports.NewInvoiceNA();
                myDataReport.SetDataSource(dtNewInvoiceNA);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (dtNewInvoiceEndo.Rows.Count > 0)
            {
                isPrinted = true;

                DataSet ds = new DataSet();
                dtNewInvoiceEndo.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                dtTempNewInvoiceEndo.TableName = "PAYMENT_INSTRUCTION";
                ds.Tables.Add(dtNewInvoiceEndo);
                ds.Tables.Add(dtTempNewInvoiceEndo);

                Reports.NewInvoiceBatchEndo myDataReport = new Reports.NewInvoiceBatchEndo();
                myDataReport.SetDataSource(ds);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (dtNewInvoice.Rows.Count > 0)
            {
                isPrinted = true;

                DataSet ds = new DataSet();
                dtNewInvoice.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                dtTempNewInvoice.TableName = "PAYMENT_INSTRUCTION";
                ds.Tables.Add(dtNewInvoice);
                ds.Tables.Add(dtTempNewInvoice);

                Reports.NewInvoiceBatch myDataReport = new Reports.NewInvoiceBatch();
                myDataReport.SetDataSource(ds);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (dtInvoiceEndoListAll.Rows.Count > 0)
            {
                isPrinted = true;

                Reports.PrintInvoiceEndListAll myDataReport = new Reports.PrintInvoiceEndListAll();
                myDataReport.SetDataSource(dtInvoiceEndoListAll);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (dtInvoiceListAll.Rows.Count > 0)
            {
                isPrinted = true;

                Reports.PrintInvoiceListAll myDataReport = new Reports.PrintInvoiceListAll();
                myDataReport.SetDataSource(dtInvoiceListAll);
                var frm = new frmPrintInvoiceByBatchNoPreview();
                frm.rpt = myDataReport;
                frm.Show();
            }

            if (!isPrinted)
            {
                Cursor.Current = Cursors.Arrow;
                Msgbox.Show("No debit note found!");
                return;
            }

            Cursor.Current = Cursors.Arrow;
        }
    }
}