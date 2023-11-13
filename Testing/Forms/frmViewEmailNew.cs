using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Testing.Forms
{
    public partial class frmViewEmailNew : Form
    {
        private string body;

        public frmViewEmailNew()
        {
            InitializeComponent();
        }

        //Update 16-Jul-19 (Edit Email Content)
        public string[][] rtbTextDetail = new string[1][];   //store each char in rtbEditor with code format
        //Code Format Detail:     XXXX ----- has 4 digits (1st digit = Regular, 2nd digit = Bold, 3rd digit = Italic, 4th digit = Underline) Example: 0110 means that char has Bold & Italic format
        string htmlscript = string.Empty;   
        //End of Update

        //20-Jul-21
        public static string type= "", finalizeusername = "", finalizemailadd = "", resetcontent = "";
        //

        private void frmViewEmail_Load(object sender, EventArgs e)
        {
            //Update 16-Jul-19 (Edit Email Content)

            using (StreamReader reader = new StreamReader("Html/EmailContent.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", resetcontent);

            if (frmANHSettlementLetterNew.DtNote != null && frmANHSettlementLetterNew.DtNote.Rows.Count > 0)
            {
                var note = string.Empty;
                var count = 0;

                for (int i = 0; i < frmANHSettlementLetterNew.DtNote.Rows.Count; i++)
                {
                    var amt = frmANHSettlementLetterNew.DtNote.Rows[i]["Amount"].ToString();
                    var des = frmANHSettlementLetterNew.DtNote.Rows[i]["Description"].ToString();

                    note += string.Concat("- ", amt, ": ", des, "<br />");
                    count++;

                    if (frmANHSettlementLetterNew.DtNote.Rows.Count == count)
                        note = note.Remove(note.Length - 6);
                }

                body = body.Replace("N/A", note);
            }

            if (frmANHSettlementLetterNew.DtExplainBene != null && frmANHSettlementLetterNew.DtExplainBene.Rows.Count > 0)
            {
                var expBeni = frmANHSettlementLetterNew.DtExplainBene;

                decimal claimAmt = 0.00M;
                decimal nonPaidAmt = 0.00M;
                decimal paidAmt = 0.00M;

                for (int i = 0; i < expBeni.Rows.Count; i++)
                {
                    var curInUsd = expBeni.Rows[i]["CURRENCY_IN_USD"].ToString();
                    var expenses = expBeni.Rows[i]["EXPENSES_NOT_COVERED"].ToString();
                    var deductible = expBeni.Rows[i]["DEDUCTIBLE_OR_COPLAY"].ToString();
                    var overLimit = expBeni.Rows[i]["OVER_LIMIT"].ToString();

                    if (!string.IsNullOrEmpty(curInUsd))
                        claimAmt += Convert.ToDecimal(curInUsd);

                    if (!string.IsNullOrEmpty(expenses))
                        nonPaidAmt += Convert.ToDecimal(expenses);

                    if (!string.IsNullOrEmpty(deductible))
                        nonPaidAmt += Convert.ToDecimal(deductible);

                    if (!string.IsNullOrEmpty(overLimit))
                        nonPaidAmt += Convert.ToDecimal(overLimit);
                }

                paidAmt = claimAmt - nonPaidAmt;

                body = body.Replace("%Paid%", string.Concat("USD ", paidAmt.ToString("0.00")));
                body = body.Replace("%NonPaid%", string.Concat("USD ", nonPaidAmt.ToString("0.00")));
            }

            this.wbEmail.DocumentText = body;
            //End of Update
        }

        private void bnEdit_Click(object sender, EventArgs e)
        {
            htmlEditor.Enabled = true;
            htmlEditor.Text = this.wbEmail.DocumentText;
        }   

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (htmlEditor.Text == null)
            {
                Msgbox.Show("No text to save");
                return;
            }

            DialogResult res = Msgbox.Show("Do you want to make change on this email content?", "Confirmation");
            if (res == System.Windows.Forms.DialogResult.No)
                return;
            this.wbEmail.DocumentText = htmlEditor.Text;
            frmSendEmailClaimDet.finalizecontent = htmlEditor.Text;
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnReset_Click(object sender, EventArgs e)
        {
            this.wbEmail.DocumentText = body;
            frmSendEmailClaimDet.finalizecontent = body;
        }
    }
}
