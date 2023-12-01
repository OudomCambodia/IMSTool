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
using System.IO;

namespace Testing.Forms
{
    public partial class frmGenerateSettlementLetterNotice : Form
    {
        private string sp_type = string.Empty;
        private string claimNo = string.Empty;
        private string cc = string.Empty;
        private string path = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\Settlement_Notice";
        //private string path = @"D:\Settlement_Notice\";
        public static string FPath = string.Empty;
        public static string Paid = string.Empty;
        public static string NonPaid = string.Empty;

        private CRUD crud = new CRUD();

        public frmGenerateSettlementLetterNotice(string spType, string ClaimNo)
        {
            InitializeComponent();

            sp_type = spType;
            claimNo = ClaimNo;

            //Oudom
            txtAdditionalText.Enabled = spType == "ParNew";

            ActiveControl = dtAdmissionDate;
        }

        private void frmGenerateSettlementLetterNotice_Load(object sender, EventArgs e)
        {
            txtPolNo.Text = frmSendEmailClaim.PolNo;
            txtClaimNo.Text = claimNo;

            var cusCode = string.Empty;

            var dtRiskName = crud.ExecQuery("select INT_PRS_NAME, INT_CUS_CODE from CL_T_INTIMATION where INT_CLAIM_NO = '" + claimNo + "' and rownum = 1");
            if (dtRiskName.Rows.Count > 0)
            {
                txtRiskName.Text = dtRiskName.Rows[0]["INT_PRS_NAME"].ToString();
                cusCode = dtRiskName.Rows[0]["INT_CUS_CODE"].ToString();
            }

            var dtInsured = crud.ExecQuery("SELECT nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) FROM UW_M_CUSTOMERS WHERE CUS_CODE = '"+ cusCode +"'");
            if (dtInsured.Rows.Count > 0)
            {
                txtInsured.Text = dtInsured.Rows[0][0].ToString();
            }

            var qBuilder = new StringBuilder();
            qBuilder.Append("(select ADR_LOC_DESCRIPTION || ', ' || chr(10) || ")
                .Append("(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ', '  || ")
                .Append("(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ', '  || ")
                .Append("(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE ")
                .AppendFormat("from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = '{0}' and rownum = 1) ", cusCode);

            var dtAddress = crud.ExecQuery(qBuilder.ToString());
            if (dtAddress.Rows.Count > 0)
            {
                txtAddress.Text = dtAddress.Rows[0][0].ToString();
            }

            var dtExeCode = crud.ExecQuery("select POL_MARKETING_EXECUTIVE_CODE from UW_T_POLICIES where POL_POLICY_NO = '" + txtPolNo.Text.Trim() + "' and rownum = 1");
            if (dtExeCode.Rows.Count > 0)
            {
                var dtSf = crud.ExecQuery("select SFC_SURNAME from SM_M_SALES_FORCE where SFC_CODE = '" + dtExeCode.Rows[0][0].ToString() + "'");
                if (dtSf.Rows.Count > 0)
                {
                    cc = dtSf.Rows[0][0].ToString();
                } 
            } 
        }

        private void txtClaimAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var dtReport = new DataTable();
                dtReport.Columns.Add("REQUISITION_NO", typeof(string));
                dtReport.Columns.Add("REQ_AMOUNT", typeof(string));
                dtReport.Columns.Add("CLAIM_NO", typeof(string));
                dtReport.Columns.Add("INSURED", typeof(string));
                dtReport.Columns.Add("RISK_NAME", typeof(string));
                dtReport.Columns.Add("ADMISSION_DATE", typeof(string));
                dtReport.Columns.Add("CLAIMED_AMT", typeof(string));
                dtReport.Columns.Add("PAYABLE_AMT", typeof(string));
                dtReport.Columns.Add("CREATED_DATE", typeof(string));
                dtReport.Columns.Add("ADDRESS", typeof(string));
                dtReport.Columns.Add("CC", typeof(string));
                dtReport.Columns.Add("PANEL_HOSPITAL", typeof(string));
                dtReport.Columns.Add("HOSPITAL_ADDRESS", typeof(string));
                dtReport.Columns.Add("PV", typeof(string));
                dtReport.Columns.Add("BANK_TRAN_NO", typeof(string));
                dtReport.Columns.Add("CHQ_NO", typeof(string));
                dtReport.Columns.Add("LAST_PAYMENT", typeof(string));
                dtReport.Columns.Add("PREVIOUS_PAYMENT", typeof(string));
                dtReport.Columns.Add("NON_PAYABLE", typeof(string));
                dtReport.Columns.Add("DEAR", typeof(string));
                dtReport.Columns.Add("TEXT", typeof(string));
                dtReport.Columns.Add("EMAIL", typeof(string));
                dtReport.Columns.Add("SIGNATURE", typeof(string));
                dtReport.Columns.Add("USER_CC", typeof(string));
                dtReport.Columns.Add("POL_NO", typeof(string));

                var drReport = dtReport.NewRow();
                drReport["REQUISITION_NO"] = string.Empty;
                drReport["REQ_AMOUNT"] = string.Empty;
                drReport["CLAIM_NO"] = txtClaimNo.Text;
                drReport["INSURED"] = txtInsured.Text;
                drReport["RISK_NAME"] = txtRiskName.Text;
                drReport["ADMISSION_DATE"] = dtAdmissionDate.Value.ToString("dd-MMM-yyyy");

                if (!string.IsNullOrEmpty(txtClaimAmt.Text.Trim()))
                    drReport["CLAIMED_AMT"] = Convert.ToDecimal(txtClaimAmt.Text).ToString("0.00");

                if (!string.IsNullOrEmpty(txtPayableAmt.Text.Trim()))
                    drReport["PAYABLE_AMT"] = Convert.ToDecimal(txtPayableAmt.Text).ToString("0.00");

                drReport["CREATED_DATE"] = DateTime.Now.ToString("dd-MMM-yyyy");
                drReport["ADDRESS"] = txtAddress.Text;
                drReport["CC"] = cc;
                drReport["PANEL_HOSPITAL"] = string.Empty;
                drReport["HOSPITAL_ADDRESS"] = string.Empty;
                drReport["PV"] = string.Empty;
                drReport["BANK_TRAN_NO"] = string.Empty;
                drReport["CHQ_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtLastPayment.Text.Trim()))
                    drReport["LAST_PAYMENT"] = Convert.ToDecimal(txtLastPayment.Text).ToString("0.00");

                if (!string.IsNullOrEmpty(txtPreviuosPayment.Text.Trim()))
                    drReport["PREVIOUS_PAYMENT"] = Convert.ToDecimal(txtPreviuosPayment.Text).ToString("0.00");

                if (!string.IsNullOrEmpty(txtNonPayableAmt.Text.Trim()))
                    drReport["NON_PAYABLE"] = Convert.ToDecimal(txtNonPayableAmt.Text).ToString("0.00");

                drReport["DEAR"] = "Sir or Madam,";
                drReport["TEXT"] = txtAdditionalText.Text;
                if (txtClaimNo.ToString().ToLower().Contains("hns"))
                {
                    drReport["EMAIL"] = "hnsclaims@forteinsurance.com";
                }
                else if (claimNo.ToLower().Contains("gpa"))
                {
                    drReport["EMAIL"] = "gpa@forteinsurance.com";
                }
                else
                {
                    drReport["EMAIL"] = "figtree_blue@forteinsurance.com";
                }
                drReport["SIGNATURE"] = "Accident and Health Department";
                drReport["USER_CC"] = string.Empty;
                drReport["POL_NO"] = txtPolNo.Text;

                Paid = txtPayableAmt.Text;
                NonPaid = txtNonPayableAmt.Text;

                dtReport.Rows.Add(drReport);

                CrystalDecisions.CrystalReports.Engine.ReportClass report = new CrystalDecisions.CrystalReports.Engine.ReportClass();
                report = new Reports.SettlementLetterToInsured();

                report.SetDataSource(dtReport);

                crViewer.ReportSource = report;

                System.IO.Directory.CreateDirectory(path);

                FPath = path + @"\" + claimNo.Replace('/', '-');

                report.ExportToDisk(ExportFormatType.WordForWindows, FPath + ".doc");

                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

                word.Visible = false;

                Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(FPath + ".doc");

                doc.SaveAs2(path + @"\" + claimNo.Replace('/', '-'), Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);

                doc.Close();
                word.Quit();

                File.Delete(FPath + ".doc");
                FPath = path + @"\" + claimNo.Replace('/', '-') + ".pdf";

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }
        }
    }
}
