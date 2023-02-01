using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iFont = iTextSharp.text.Font;
using System.Net.Mime;
using System.Net;

namespace Testing.Forms
{
    public partial class frmWindscreen : Form
    {
        public frmWindscreen()
        {
            InitializeComponent();
        }

        CRUD crud = new CRUD();
        DataTable reportDt = new DataTable();
        DataTable polinfo = new DataTable();
        DataTable polclinfo = new DataTable();
        DataTable clinfo = new DataTable();
        DataTable windscreeninfo = new DataTable();
        public string Username = "";
        string HashPass = "Forte@2017";
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        string UserFullName = "";

        private Attachment getMailAttachment(bool IsResend)
        {
            //Get Windscreen Peril Record
            //DataTable windscreenRow = clinfo.Select("REINSTATE_STATUS = 'No'").CopyToDataTable();
            //if (windscreenRow.Rows.Count <= 0)
            //{
            //    Msgbox.Show("Cannot find Windscreeen Peril with Reinstatement Status \"No\".");
            //    return;
            //}
            if (windscreeninfo.Rows.Count <= 0)
            {
                windscreeninfo = clinfo.Select("REINSTATE_STATUS = 'No'").CopyToDataTable();
                if (windscreeninfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Cannot find Windscreeen Peril with Reinstatement Status \"No\".");
                    return default(Attachment);
                }
                else 
                {
                    DataRow r = windscreeninfo.AsEnumerable().Last();
                    windscreeninfo = new[] { r }.CopyToDataTable();
                }                
            }           
            
            DataRow lastRow = windscreeninfo.Rows[0];
            //

            //getAllVar
            string SendingDate = DateTime.UtcNow.AddHours(7).ToString("dd MMMM yyyy");
            string AdditionalInsured = lastRow["ADDITIONAL_INSURED"].ToString(), 
                PolicyNo = lastRow["POLICYNO"].ToString(),
                ClaimNo = lastRow["CLAIM_NO"].ToString(), 
                VehicleNo = lastRow["INT_PRS_NAME"].ToString(), 
                DateofAccident = Convert.ToDateTime(lastRow["DATEOFLOSS"]).ToString("dd MMMM yyyy"),
                PlaceofAccident = lastRow["INT_PLACE_LOSS"].ToString();

            string ProductName = ClaimNo.Substring(7, 3),
            ClaimedAmt = lastRow["CLAIM_AMOUNT"].ToString(),
            ExtraPrem = lastRow["REINSTATEMENT_PREMIUM"].ToString(),
            last4digit = ClaimNo.Substring(16),
            BranchCode = ClaimNo.Substring(4, 1),
            informDate = DateTime.UtcNow.AddHours(7).AddDays(45).ToString("dd MMMM yyyy");
            //

            //get AddressLine,DedutibleText,CoInsurance
            DataTable dtTemp = crud.ExecQuery("select ADR_LOC_DESCRIPTION || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE " +
"from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = '" + lastRow["INSUREDCODE"].ToString() + "'");
            string AddressLine = dtTemp.Rows[0]["ADDRESS_LINE"].ToString();

            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont bf = BaseFont.CreateFont(@"Html\tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //iFont bf = GetTahoma();

            MemoryStream mstream = new MemoryStream();
            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
            PdfWriter writer = PdfWriter.GetInstance(doc, mstream);
            doc.Open();

            Chunk c1 = new Chunk();
            Chunk c2 = new Chunk();
            Phrase ph = new Phrase();

            //Add Forte Logo
            PdfContentByte cb = writer.DirectContent;
            iTextSharp.text.Image imgSoc = iTextSharp.text.Image.GetInstance("Html/forte-general-logo-red.png");
            imgSoc.ScalePercent(40f);
            imgSoc.SetAbsolutePosition(25, 780);
            cb.AddImage(imgSoc);
            //

            if (IsResend)
            {

                PdfContentByte b = writer.DirectContent;
                //Reminder sign
                PdfPTable resend = new PdfPTable(1);
                resend.TotalWidth = 68f;
                resend.LockedWidth = true;
                PdfPCell resendc = new PdfPCell(new Phrase("RESEND", new iFont(bf, 10)));
                resendc.VerticalAlignment = Element.ALIGN_MIDDLE;
                resendc.HorizontalAlignment = 1;
                resendc.FixedHeight = 20f;
                resend.AddCell(resendc);
                //
                resend.WriteSelectedRows(0, -1, 475, 810, b);
            }

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Date: " + SendingDate, new iFont(bf, 9)));
            doc.Add(new Paragraph(" "));

            PdfPTable additionalinsuredtbl = new PdfPTable(new float[] { 3, 1 });
            //additionalinsuredtbl.TotalWidth = 530f; 
            additionalinsuredtbl.TotalWidth = doc.PageSize.Width - 80f;
            additionalinsuredtbl.LockedWidth = true;
            additionalinsuredtbl.DefaultCell.FixedHeight = 35f;
            additionalinsuredtbl.HorizontalAlignment = 0;


            PdfPCell blankcell = new PdfPCell(); blankcell.BorderWidth = 0;
            Phrase InsuredPh = new Phrase();
            Chunk InsuredCh1 = new Chunk(AdditionalInsured, new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            Chunk InsuredCh2 = new Chunk("\n\n" + AddressLine, new iFont(bf, 9));
            InsuredPh.Add(InsuredCh1); InsuredPh.Add(InsuredCh2);
            PdfPCell ai = new PdfPCell(InsuredPh); ai.BorderWidth = 0;
            additionalinsuredtbl.AddCell(ai); additionalinsuredtbl.AddCell(blankcell);
            //fill column to make empty cell
            doc.Add(additionalinsuredtbl);
            //doc.Add(new Paragraph(AdditionalInsured, new iFont(bf, 9)));
            //doc.Add(new Paragraph(AddressLine, new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));


            doc.Add(new Paragraph("RE:      WINDSCREEN CLAIMS - REINSTATEMENT OPTIONS", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            //Reference
            PdfPTable reference = new PdfPTable(new float[] { 5, 6, 1, 20 });
            reference.TotalWidth = doc.PageSize.Width - 80f;
            reference.LockedWidth = true;
            reference.DefaultCell.FixedHeight = 35f;
            reference.HorizontalAlignment = 0;

            PdfPCell refc1 = new PdfPCell(new Phrase("Reference:", new iFont(bf, 9)));
            refc1.HorizontalAlignment = Element.ALIGN_LEFT;
            refc1.BorderWidth = 0;
            refc1.Rowspan = 5;
            reference.AddCell(refc1);

            refc1 = new PdfPCell(new Phrase("Policy No", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(":", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(PolicyNo, new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase("Claim No", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(":", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(ClaimNo, new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase("Vehicle R/N", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(":", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(VehicleNo, new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase("Date of Accident", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(":", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(DateofAccident.ToUpper(), new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase("Place of Accident", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(":", new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            refc1 = new PdfPCell(new Phrase(PlaceofAccident, new iFont(bf, 9)));
            refc1.BorderWidth = 0;
            reference.AddCell(refc1);
            //
            doc.Add(reference);
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("Dear Sir/Madam,", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));



            Paragraph firstpara = new Paragraph();
            Chunk firstparac1 = new Chunk("We would like to inform you that your " 
                + crud.ExecQuery("SELECT PRD_DESCRIPTION FROM UW_M_PRODUCTS WHERE PRD_CODE = '" + ProductName + "'").Rows[0][0].ToString() //get Product Desc
                + " Policy has an extra benefits extension of breakage of Glass in windscreen or windows, which entitles you to ", new iFont(bf, 9));
            Chunk firstparac2 = new Chunk("one-time", new iFont(bf, 9, iTextSharp.text.Font.UNDERLINE));
            Chunk firstparac3 = new Chunk(" of windscreen claim without losing your no-claim discount or paying the Deductible.", new iFont(bf, 9));
            Phrase firstparap = new Phrase();
            firstparap.Add(firstparac1);
            firstparap.Add(firstparac2);
            firstparap.Add(firstparac3);
            firstpara.Add(firstparap);
            doc.Add(firstpara);
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            Paragraph secondpara = new Paragraph();
            Chunk secondparac1 = new Chunk("After we have paid the claim, this benifit will end unless you decide to reinstate it by paying an ", new iFont(bf, 9));
            Chunk secondparac2 = new Chunk("extra premium.", new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            Phrase secondparap = new Phrase();
            secondparap.Add(secondparac1);
            secondparap.Add(secondparac2);
            secondpara.Add(secondparap);
            doc.Add(secondpara);
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            doc.Add(new Paragraph("In your Windscreen claim involving the insured Vehicle with vehicle R/N as referenced above, the extra premium to reinstate the above benefit is as follows:", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));


            //Windscreen Claims amount
            PdfPTable winamt = new PdfPTable(3);
            winamt.TotalWidth = 530f;
            winamt.LockedWidth = true;
            float[] widths = new float[] { 50f, 170f, 310f };
            winamt.SetWidths(widths);
            winamt.DefaultCell.FixedHeight = 35f;
            winamt.HorizontalAlignment = 0;
            PdfPCell winamtc1 = new PdfPCell(new Phrase("", new iFont(bf, 9)));//blank cell
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            winamtc1 = new PdfPCell(new Phrase("- Windscreen Claims amount", new iFont(bf, 9)));
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            //winamtc1 = new PdfPCell(new Phrase(": USD " + String.Format("{0:N}", Convert.ToInt32(ClaimedAmt)), new iFont(bf, 9)));
            winamtc1 = new PdfPCell(new Phrase(": USD " + String.Format("{0:N}", Convert.ToDouble(ClaimedAmt)), new iFont(bf, 9)));
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            winamtc1 = new PdfPCell(new Phrase("", new iFont(bf, 9)));//blank cell
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            winamtc1 = new PdfPCell(new Phrase("- Extra Premium (Reinstatement)", new iFont(bf, 9)));
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            winamtc1 = new PdfPCell(new Phrase(": USD " + String.Format("{0:N}", Convert.ToInt32(ExtraPrem)), new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            winamtc1.BorderWidth = 0;
            winamt.AddCell(winamtc1);
            doc.Add(winamt);
            //


            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("This payment is base on your own discretion:", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            Paragraph indentpara1 = new Paragraph();
            Chunk indentpara1c1 = new Chunk("If you opt to reinstate this benifit, you will be entitled to ", new iFont(bf, 9));
            Chunk indentpara1c2 = new Chunk("another time", new iFont(bf, 9, iTextSharp.text.Font.UNDERLINE));
            Chunk indentpara1c3 = new Chunk(" of windscreen claim without losing your no-claim discount or paying the Deductible.", new iFont(bf, 9));
            Phrase indentpara1p = new Phrase();
            indentpara1p.Add(indentpara1c1);
            indentpara1p.Add(indentpara1c2);
            indentpara1p.Add(indentpara1c3);
            indentpara1.Add(indentpara1p);
            indentpara1.IndentationLeft = 20;
            doc.Add(indentpara1);
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            Paragraph indentpara2 = new Paragraph("Otherwise, you will need to pay the Deductible and lose your no-claim discount on your next windscreen claim.", new iFont(bf, 9));
            indentpara2.IndentationLeft = 20;
            doc.Add(indentpara2);

            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("Please inform us by " + informDate + " or the Policy's expiry date whichever is earlier if you choose to reinstate the benefit, by signing on this letter and sending it back to us for our documentation processing.", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("If we don't hear from you within the time above, an assumption will be made that you decided to forfeit this benefit.", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));


            //Acceptance Box
            PdfPTable accepttbl = new PdfPTable(1);
            accepttbl.TotalWidth = 250f;
            accepttbl.LockedWidth = true;
            Chunk accepttblc1 = new Chunk("ACCEPTANCE:\n\n", new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            Chunk accepttblc2 = new Chunk("Signature:\n\nName:\n\nDate:\n\n", new iFont(bf, 9));
            Phrase accepttblp = new Phrase();
            accepttblp.Add(accepttblc1);
            accepttblp.Add(accepttblc2);
            PdfPCell accepttblcell = new PdfPCell(accepttblp);
            accepttblcell.PaddingLeft = 10;
            accepttbl.AddCell(accepttblcell);
            accepttbl.HorizontalAlignment = Element.ALIGN_RIGHT;
            //
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(accepttbl);

            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("Should you wish to accept it and pay the extra premium at the same time, you may make payment via the following Payment Methods:", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            //Payment table
            PdfPTable paymenttbl = new PdfPTable(4);
            paymenttbl.TotalWidth = doc.PageSize.Width - 80f;
            paymenttbl.LockedWidth = true;
            paymenttbl.DefaultCell.FixedHeight = 80f;
            paymenttbl.HorizontalAlignment = 0;

            PdfPCell cell = new PdfPCell(new Phrase("1. Pay at our office at Vattanac Capital (level 18), Monivong Blvd., Sangkat Wat Phnom, Khan Daun Penh, Phnom Penh,\n    Cambodia or any of our Forte branches closest to you.", new iFont(bf, 9)));
            cell.Colspan = 4;
            paymenttbl.AddCell(cell);

            paymenttbl.AddCell(new PdfPCell(new Phrase("2. Pay via our bank accounts:", new iFont(bf, 9))));

            PdfPTable temp = new PdfPTable(1);
            temp.AddCell(new PdfPCell(new Phrase("Bank", new iFont(bf, 9, iTextSharp.text.Font.BOLD))));
            temp.AddCell(new PdfPCell(new Phrase("ACLEDA Bank", new iFont(bf, 9))));
            temp.AddCell(new PdfPCell(new Phrase("ABA Bank", new iFont(bf, 9))));
            temp.AddCell(new PdfPCell(new Phrase("Wing", new iFont(bf, 9))));
            PdfPCell tempcell = new PdfPCell(temp);
            tempcell.Padding = 0f;
            paymenttbl.AddCell(tempcell);

            temp = new PdfPTable(1);
            temp.AddCell(new PdfPCell(new Phrase("Account No.", new iFont(bf, 9, iTextSharp.text.Font.BOLD))));
            temp.AddCell(new PdfPCell(new Phrase("0001-10197380-1-2", new iFont(bf, 9))));
            temp.AddCell(new PdfPCell(new Phrase("000 090 740", new iFont(bf, 9))));
            temp.AddCell(new PdfPCell(new Phrase("1130", new iFont(bf, 9))));
            tempcell = new PdfPCell(temp);
            tempcell.Padding = 0f;
            paymenttbl.AddCell(tempcell);

            temp = new PdfPTable(1);
            temp.AddCell(new PdfPCell(new Phrase("Account Name", new iFont(bf, 9, iTextSharp.text.Font.BOLD))));
            temp.AddCell(new PdfPCell(new Phrase("FORTE INSURANCE (CAMBODIA) PLC.", new iFont(bf, 9))));
            tempcell = new PdfPCell(temp);
            tempcell.Padding = 0f;
            paymenttbl.AddCell(tempcell);


            c1 = new Chunk("Please quote in the \"Remark\" when making your transfer: ", new iFont(bf, 9));
            c2 = new Chunk("WS" + BranchCode + ProductName + last4digit + "-……………… [Your Mobile No.]", new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            ph = new Phrase();
            ph.Add(c1); ph.Add(c2);
            cell = new PdfPCell(ph);
            cell.Colspan = 4;
            paymenttbl.AddCell(cell);
            doc.Add(paymenttbl);
            //

            doc.Add(new Paragraph(" ", new iFont(bf, 9)));


            doc.Add(new Paragraph("We thank you for your cooperation and support.", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("Yours sincerely,", new iFont(bf, 9)));
            doc.Add(new Paragraph("FORTE Insurance (Cambodia) Plc.", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            Paragraph lastremark = new Paragraph("** THIS IS COMPUTER GENERATED AND DOES NOT REQUIRE SIGNATURE. **", new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            lastremark.Alignment = Element.ALIGN_CENTER;
            doc.Add(lastremark);

            writer.CloseStream = false;
            doc.Close();
            mstream.Position = 0;

            MemoryStream instream = new MemoryStream();
            Document newdoc = new Document();

            PdfCopy copy = new PdfCopy(newdoc, instream);
            newdoc.Open();

            PdfReader reader = new PdfReader(mstream.ToArray());
            for (int j = 1; j <= reader.NumberOfPages; ++j)
            {
                PdfImportedPage curPg = copy.GetImportedPage(reader, j);
                copy.AddPage(curPg);
            }

            newdoc.Close();

            MemoryStream laststream = new MemoryStream();
            PdfReader lastreader = new PdfReader(instream.ToArray());
            PdfStamper laststamper = new PdfStamper(lastreader, laststream);
            int lastpagecount = lastreader.NumberOfPages;
            PdfContentByte a = laststamper.GetOverContent(1);
            for (int i = 1; i <= lastpagecount; i++)
            {
                a = laststamper.GetOverContent(i);
                a.SetColorFill(BaseColor.DARK_GRAY);
                a.BeginText();
                a.SetFontAndSize(bf, 9);
                a.ShowTextAligned(1, "Page " + i + " of " + lastpagecount, 90, 20, 0);
                a.EndText();
            }
            laststamper.FormFlattening = true;
            laststamper.Writer.CloseStream = false;
            laststamper.Close();

            //get Letter name
            string LetterName = "WS Letter - " + VehicleNo.Replace("\n", " ") + " - " + Convert.ToDateTime(lastRow["DATEOFLOSS"]).ToString("dd MMMM yyyy");
            if (IsResend) //Not the first letter
                LetterName += " - " + "RESEND";
            //

            laststream.Position = 0;
            return new Attachment(laststream, LetterName + ".pdf");
        }

        private void frmWindscreen_Load(object sender, EventArgs e)
        {

            clearControl();

            cbExportAs.SelectedIndex = 0;
            cbWindscreen.SelectedIndex = 0;
            tbPolNo.Focus();
            dgvClaimDetailPolicy.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClaimDetailPolicy.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClaimDetail.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClaimDetail.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            //getting configuration information about email from the database
            DataTable dtEmailIn = crud.ExecSP_OutPara("sp_user_claim_info", new string[] { "cl_no", "cl_type", "cl_cond" }, new string[] { "", "emailInfo", Username });
            smtpSer = dtEmailIn.Rows[0].ItemArray[0].ToString();
            mail_add = dtEmailIn.Rows[0].ItemArray[1].ToString();
            if (dtEmailIn.Rows[0].ItemArray[2].ToString() != "")
                mail_pass = Cipher.Decrypt(dtEmailIn.Rows[0].ItemArray[2].ToString(), HashPass);
            else
                mail_pass = "";
            port = Convert.ToInt16(dtEmailIn.Rows[0].ItemArray[3].ToString());
            UserFullName = dtEmailIn.Rows[0].ItemArray[4].ToString();

        }

        private void bnClearPolicy_Click(object sender, EventArgs e)
        {
            tbPolNo.Text = "";
            tbPolNo.Focus();

            foreach (Control ctl in groupBox6.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            dgvClaimDetailPolicy.DataSource = null;
            dgvClaimDetailPolicy.Columns.Clear();

            lblTotalOS.Text = "";
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbClaimNo.Text = "";
            tbClaimNo.Focus();
            dgvClaimDetail.DataSource = null;
            dgvClaimDetail.Columns.Clear();

            frmDocumentControl.disabledButt(bnSendEmail);
            frmDocumentControl.disabledButt(bnResendEmail);
            frmDocumentControl.disabledButt(bnGetLetter);

            tbTo.Text = "";
            tbCC.Text = "";
            lblSentDate.Text = "";
            lblReSentDate.Text = "";

            lblTotalOSCl.Text = "";
        }

        private void bnClearReport_Click(object sender, EventArgs e)
        {
            dtpIntDateFr.Value = DateTime.Now;
            dtpIntDateTo.Value = DateTime.Now;
            dgvReport.DataSource = null;
            dgvReport.Rows.Clear();
        }

        private void frmWindscreen_Activated(object sender, EventArgs e)
        {
            tbPolNo.Focus();
        }

        private void bnExport_Click(object sender, EventArgs e)
        {
            if (reportDt == null || reportDt.Rows.Count == 0)
            {
                Msgbox.Show("There's no data to export!");
                return;
            }

            if (cbExportAs.Text == "Excel")
            {
                DialogResult dr = Msgbox.Show("Are you sure you want to generate the result to Excel?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    My_DataTable_Extensions.ExportToExcel(reportDt, "");
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show("Excel exported!");
                }
            }
            else if (cbExportAs.Text == "PDF")
            {
                if (fdbSelectPath.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fdbSelectPath.SelectedPath))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Data2PDF(reportDt, fdbSelectPath.SelectedPath);
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show("PDF exported!");
                }
            }
            else
            {
                Msgbox.Show("Please select Export as type.");
                return;
            }
        }

        private void Data2PDF(DataTable dt, string Dest)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Dest + @"\\" + "Windscreen Report" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".pdf", FileMode.Create));
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.Open();
            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("c://ggi logo.bmp");
            //document.Add(img);
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);
            //float[] columnDefinitionSize = { 22F, 22F, 12F, 7.75F, 7.77F, 7.77F, 7.77F, 7.77F, 10.88F, 10.88F, 10.88F, 4.75F, 7.77F, 7.77F, 7.77F, 7.77F, 7.77F, 7.77F, 9F };

            PdfPTable table = new PdfPTable(dt.Columns.Count);

            //float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f }; //Change depends on dt Row Count
            //table.SetWidths(widths);

            table.WidthPercentage = 100;
            Paragraph header = new Paragraph("Windscreen Report " + DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            header.Alignment = Element.ALIGN_CENTER;
            document.Add(header);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));


            //cell.Border = 0;

            //cell.HorizontalAlignment = 1;
            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            }

            //cell.BackgroundColor = new iTextSharp.text.Color(0xC0, 0xC0, 0xC0);

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(r[i].ToString(), font5));
                    }
                }

            }
            document.Add(table);
            document.Close();
        }

        private void bnSearchReport_Click(object sender, EventArgs e)
        {
            if (dtpIntDateTo.Value < dtpIntDateFr.Value)
            {
                Msgbox.Show("Intimation date to cannot smaller than intimation date from.");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            string IntFr = dtpIntDateFr.Value.ToString("yyyy/MM/dd") + " 00:00:00",
                   IntTo = dtpIntDateTo.Value.ToString("yyyy/MM/dd") + " 23:59:59";

            string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler", "p_pol_no" };
            string[] Values = new string[] { "MAIN_REPORT", IntFr, IntTo, "", "", "", "" };


            reportDt = crud.ExecSP_OutPara("SP_WINDSCREEN", Key, Values);

            var windscreenrow = reportDt.Select("PERILS Like '%WINDSCREEN%'"); //Get Only Claim with Windscreen peril
            if (windscreenrow.Count() > 0)
            {
                reportDt = windscreenrow.CopyToDataTable();

                DataRow[] temp = null;

                if (cbWindscreen.Text == "Paid" || cbWindscreen.Text == "OS")
                {
                    if (cbWindscreen.Text == "Paid")
                    {
                        temp = reportDt.Select("CLAIM_STATUS = 'PAID'");
                    }
                    else if (cbWindscreen.Text == "OS")
                    {
                        temp = reportDt.Select("CLAIM_STATUS = 'OS'");
                    }

                    if (temp.Count() > 0)
                        reportDt = temp.CopyToDataTable();
                    else
                    {
                        reportDt = new DataTable();
                        Msgbox.Show("No data found.");
                        return;
                    }
                }
               
            }
            else
            {
                reportDt = new DataTable();
                Msgbox.Show("No data found.");
                return;
            }




            

            dgvReport.DataSource = reportDt;
            Cursor.Current = Cursors.AppStarting;

        }

        private void bnSearchPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                string polno = tbPolNo.Text.Trim().ToUpper();
                //clearControl();
                bnClearPolicy.PerformClick();
                tbPolNo.Text = polno;

                if (polno == "" || polno.Length != 20)
                {
                    Msgbox.Show("Please Input Policy No!");
                    tbPolNo.Focus();
                    return;
                }


                string IntFr = "2015/01/01 00:00:00",
                   IntTo = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";

                string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler", "p_pol_no" };
                string[] Values = new string[] { "POL_DTL", IntFr, IntTo, "", "", "", polno };

                polinfo = crud.ExecSP_OutPara("SP_WINDSCREEN", Key, Values);
                if (polinfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Policy No not found!");
                    tbPolNo.Focus();
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    tbInsured.Text = polinfo.Rows[0]["INSURED"].ToString();
                    tbPolicyPeriod.Text = "from " + (Convert.ToDateTime(polinfo.Rows[0]["PERIOD_FROM"]).ToString("dd'/'MM'/'yyyy"))
                       + " to " + (Convert.ToDateTime(polinfo.Rows[0]["PERIOD_TO"]).ToString("dd'/'MM'/'yyyy"));
                    tbIntermediary.Text = polinfo.Rows[0]["AGENT_CODE"].ToString() + " - " + polinfo.Rows[0]["AGENT_NAME"].ToString();
                    tbAH.Text = polinfo.Rows[0]["ACCOUNT_HANDLER"].ToString();
                    tbPolicyPremium.Text = String.Format("{0:N}", polinfo.Rows[0]["POL_PREMIUM"]);
                    string PaymentStatus = polinfo.Rows[0]["PAYMENT_STATUS"].ToString();
                    tbPaidorOS.Text = (PaymentStatus == "Y") ? "PAID" : "O/S";


                    IntFr = "2015/01/01 00:00:00";
                    IntTo = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";

                    Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler", "p_pol_no" };
                    Values = new string[] { "MAIN_REPORT", IntFr, IntTo, "", "", "", polno };

                    polclinfo = crud.ExecSP_OutPara("SP_WINDSCREEN", Key, Values);
                    if (polclinfo.Rows.Count <= 0)
                    {
                        Msgbox.Show("Claim Details not found!");
                        return;
                    }
                    else
                    {
                        //Create view
                        DataView view = new DataView(polclinfo);
                        DataTable dtTemp = view.ToTable("Selected", false, "CLAIM_NO", "INT_PRS_NAME", "PERILS", "PROV_TYPE", "CLAIM_AMOUNT",
                            "DEDUCTIBLE", "CLAIM_STATUS", "REINSTATEMENT_PREMIUM", "REINSTATE_STATUS");

                        dgvClaimDetailPolicy.DataSource = dtTemp;
                        dgvClaimDetailPolicy.Columns["REINSTATEMENT_PREMIUM"].DefaultCellStyle.Format = "c";

                        double TotalOS = 0;
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            if(dr["CLAIM_STATUS"].ToString() == "OS")
                                TotalOS += Math.Abs(Convert.ToDouble(dr["CLAIM_AMOUNT"].ToString()));
                        }
                        if(TotalOS != 0)
                            lblTotalOS.Text = String.Format("{0:N}", Convert.ToDecimal(TotalOS));
                    }

                    Cursor.Current = Cursors.AppStarting;


                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void clearControl()
        {
            bnClear.PerformClick();
            bnClearPolicy.PerformClick();

        }

        private void dgvClaimDetailPolicy_DataSourceChanged(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvClaimDetailPolicy);
        }

        private void dgvClaimDetailPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int row = e.RowIndex;
            string SelectedClNo = dgvClaimDetailPolicy.Rows[row].Cells["CLAIM_NO"].Value.ToString();


            //tabControlMain.TabPages["tabQueryClaim"].Select();
            tabControlMain.SelectedTab = tabControlMain.TabPages["tabQueryClaim"];
            tbClaimNo.Focus();
            tbClaimNo.Text = SelectedClNo;
            bnClaimSearch.PerformClick();
        }

        private void bnClaimSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string clno = tbClaimNo.Text.Trim().ToUpper();
                bnClear.PerformClick();

                tbClaimNo.Text = clno;

                if (clno == "" || clno.Length != 20)
                {
                    Msgbox.Show("Please Input Claim No!");
                    return;
                }

                string IntFr = "2015/01/01 00:00:00",
                   IntTo = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";

                string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler", "p_pol_no" };
                string[] Values = new string[] { "MAIN_REPORT", IntFr, IntTo, clno, "", "", "" };

                clinfo = crud.ExecSP_OutPara("SP_WINDSCREEN", Key, Values);
                if (clinfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim No not found!");
                    tbClaimNo.Focus();
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    DataView view = new DataView(clinfo);
                    DataTable dtTemp = view.ToTable("Selected", false, "PERILS", "PROV_TYPE", "CLAIM_AMOUNT",
                        "DEDUCTIBLE", "CLAIM_STATUS", "REINSTATEMENT_PREMIUM", "REINSTATE_STATUS");
                    dgvClaimDetail.DataSource = dtTemp;
                    dgvClaimDetail.Columns["REINSTATEMENT_PREMIUM"].DefaultCellStyle.Format = "c";

                    bool NeedReinstatement = false;

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        if (dr["REINSTATE_STATUS"].ToString() == "No")
                            NeedReinstatement = true;
                    }

                    if (NeedReinstatement)
                    {

                        frmDocumentControl.enabledButt(bnGetLetter);


                        //Check History
                        DataTable histtbl = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + clno + "' AND SEND_TYPE = 'Windscreen' ORDER BY HIST_DATE");
                        if (histtbl.Rows.Count <= 0)
                        {
                            frmDocumentControl.enabledButt(bnSendEmail);
                        }
                        else
                        {
                            Button btnToEnable = bnSendEmail;
                            foreach (DataRow dr in histtbl.Rows)
                            {
                                if (dr["REM_NO"].ToString() == "NO")//First Email
                                {
                                    lblSentDate.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = bnResendEmail;
                                }
                                else if (dr["REM_NO"].ToString() == "RESEND")//Resend Email
                                {
                                    lblReSentDate.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = null;
                                }
                            }

                            if (btnToEnable != null)
                                frmDocumentControl.enabledButt(btnToEnable);


                        }

                        //get Account Handler email, CC
                        string agentcode = clinfo.Rows[0]["AGENT_CODE"].ToString(), ahcode = clinfo.Rows[0]["ACCOUNT_HANDLER"].ToString(), selectedcode;
                        if (agentcode[0] == '0' || ahcode == "U-BNK") //001,002,003 => no agent/broker          //U-BNK => bank 
                            selectedcode = ahcode;
                        else //agent/broker...
                            selectedcode = agentcode;

                        DataTable dt = crud.ExecQuery("SELECT MAIL_TO,MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE CODE = '" + selectedcode + "'");
                        if (dt.Rows.Count > 0)
                        {
                            tbTo.Text = dt.Rows[0]["MAIL_TO"].ToString();
                            tbCC.Text = dt.Rows[0]["MAIL_CC"].ToString();
                        }
                        //
                    }

                    double TotalOS = 0;
                    foreach (DataRow dr in clinfo.Rows)
                    {
                        if (dr["CLAIM_STATUS"].ToString() == "OS")
                            TotalOS += Math.Abs(Convert.ToDouble(dr["CLAIM_AMOUNT"].ToString()));
                    }
                    if (TotalOS != 0)
                        lblTotalOSCl.Text = String.Format("{0:N}", Convert.ToDecimal(TotalOS));
                    
                    Cursor.Current = Cursors.AppStarting;
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dgvClaimDetail_DataSourceChanged(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvClaimDetail);
        }

        private bool sendEmail(bool isResend = false)
        {
            try
            {
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return false;
                }

                if (tbTo.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email to!");
                    return false;
                }


                windscreeninfo = clinfo.Select("REINSTATE_STATUS = 'No'").CopyToDataTable();
                if (windscreeninfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Cannot find Windscreeen Peril with Reinstatement Status \"No\".");
                    return false;
                }
                else
                {
                    DataRow lastRow = windscreeninfo.AsEnumerable().Last();
                    windscreeninfo = new[] { lastRow }.CopyToDataTable();
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //Get Note
                    string Note = "";
                    string NODNote = "";

                    ////First Condition: check whether policy is cancelled or risk is deleted
                    DataTable dtTemp = crud.ExecQuery("SELECT INT_POLICY_SEQ FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + windscreeninfo.Rows[0]["CLAIM_NO"].ToString() + "'");
                    string PolSeq = dtTemp.Rows[0]["INT_POLICY_SEQ"].ToString(),
                        RiskName = windscreeninfo.Rows[0]["INT_PRS_NAME"].ToString();
                    //Check risk is deleted                
                    dtTemp = crud.ExecQuery("SELECT PRS_DATE_DELETED FROM VIEW_RISKS WHERE PRS_PLC_POL_SEQ_NO = '" + PolSeq + "' AND PRS_NAME = '" + RiskName + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string DeletedDate = dtTemp.Rows[0]["PRS_DATE_DELETED"].ToString();
                        if (DeletedDate != "" && DeletedDate != null)
                            Note = "The Vehicle has been deleted from the Policy on " + Convert.ToDateTime(DeletedDate).ToString("dd MMMM yyyy") + ".";
                    }
                    //
                    if (Note == "") //prev condition not met
                    {
                        dtTemp = crud.ExecQuery("SELECT POL_CANCELLED_DATE FROM VIEW_POLICIES WHERE POL_SEQ_NO = '" + PolSeq + "'");
                        if (dtTemp.Rows.Count > 0)
                        {
                            string CancelledDate = dtTemp.Rows[0]["POL_CANCELLED_DATE"].ToString();
                            if (CancelledDate != "" && CancelledDate != null)
                                Note = "The Policy was cancelled on " + Convert.ToDateTime(CancelledDate).ToString("dd MMMM yyyy") + ".";
                        }
                    }
                    ////
                    ////Second Condition: check whether policy not yet expire
                    string[] PolExpDate = windscreeninfo.Rows[0]["INT_PERIOD_TO"].ToString().Split('/'); //[0]:day [1]:month [2]:year
                    if (Note == "") //prev condition not met
                    {
                        DateTime ExpDate = new DateTime(Convert.ToInt32(PolExpDate[2]), Convert.ToInt32(PolExpDate[1]), Convert.ToInt32(PolExpDate[0]), 23, 59, 59);
                        if (ExpDate >= DateTime.Now) //not yet expired
                        {
                            double NOD = Math.Truncate((ExpDate - DateTime.Now).TotalDays);

                            Note = "This Policy is going to expire on " + ExpDate.ToString("dd MMMM yyyy")
                                + " which is " + NOD.ToString() + " days from today.";

                            //get NOD Note
                            if (NOD >= 45)
                            {
                                NODNote = "&nbsp;&nbsp;&nbsp;• There are 45 calendar days from today (by "+DateTime.Now.AddDays(45).ToString("dd MMMM yyyy") +") for decision. Over this period, we will assume that this benefit is forfeited.";
                            }
                            else
                            { 
                                NODNote = "&nbsp;&nbsp;&nbsp;• There are "+NOD.ToString()+" calendar days from today for decision before the Policy expires.";
                            }
                            //
                        }
                        else //already expired
                        {
                            //dtTemp = crud.ExecQuery("select * from VIEW_POLICIES where POL_POLICY_NO = '" + clinfo.Rows[0]["POLICYNO"].ToString()
                            //    + "' and POL_STATUS in (4,5,6,10) and POL_PERIOD_FROM >= TO_DATE('" + ExpDate.ToString("dd'/'MM'/'yyyy") + "','DD/MM/YYYY')");
                            //bool isRenew = (dtTemp.Rows.Count > 0) ? true : false;
                            Note = "This Policy already expired on " + ExpDate.ToString("dd MMMM yyyy") + " and this benefit is no longer applicable during this Policy Period.";
                        }
                    }
                    ////
                    //

                    string ResentText = (isResend) ? "{RESEND}" : "";

                    string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                        new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                        new string[] { "Windscreen", ResentText, windscreeninfo.Rows[0]["ADDITIONAL_INSURED"].ToString(),
                    windscreeninfo.Rows[0]["POLICYNO"].ToString(),windscreeninfo.Rows[0]["CLAIM_NO"].ToString(),
                    windscreeninfo.Rows[0]["INT_PRS_NAME"].ToString(),Convert.ToDateTime(windscreeninfo.Rows[0]["DATEOFLOSS"].ToString()).ToString("dd MMMM yyyy"),
                    windscreeninfo.Rows[0]["INT_PLACE_LOSS"].ToString(), Note, NODNote, ""}).ToString();
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader("Html/2022AutoClaimEmail.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{text}", content);
                    body = body.Replace("{department}", "Auto Department");
                    body = body.Replace("{username}", UserFullName);
                    body = body.Replace("{user_email}", mail_add);

                    //SmtpClient client = new SmtpClient(smtpSer);

                    MailMessage message = new MailMessage();

                    //set formatting email message
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    message.From = new MailAddress(mail_add);
                    message.Attachments.Add(getMailAttachment(isResend));

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    if (tbTo.Text.Trim() != "")
                    {
                        string[] to = tbTo.Text.Split(';');
                        foreach (string s in to)
                        {
                            if (s.Trim() != "")
                                message.To.Add(s.Trim());
                        }
                    }

                    message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'Windscreen'").Rows[0][0].ToString();
                    message.Subject = message.Subject.Replace("%VehicleNo%", windscreeninfo.Rows[0]["INT_PRS_NAME"].ToString().Replace("\n", " "));
                    message.Subject = message.Subject.Replace("%DateofLoss%", Convert.ToDateTime(windscreeninfo.Rows[0]["DATEOFLOSS"].ToString()).ToString("dd MMMM yyyy"));

                    //default CC auto claim team
                    dtTemp = crud.ExecQuery("SELECT MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE TEAM = 'Default'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string[] cc = dtTemp.Rows[0][0].ToString().Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }
                    

                    if (tbCC.Text.Trim() != "")
                    {
                        string[] cc = tbCC.Text.Split(';');
                        foreach (string s in cc)
                        {
                            if (s.Trim() != "")
                                message.CC.Add(new MailAddress(s.Trim()));
                        }
                    }

                    //attachment
                    //if (dgvFile.Rows.Count > 0)
                    //{
                    //    foreach (DataGridViewRow dgvr in dgvFile.Rows)
                    //    {
                    //        message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                    //    }
                    //}
                    //

                    //embeded pictures
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource img1 = new LinkedResource(@"Html\forte-general-logo-red.png", "image/png");
                    img1.ContentId = "forte-general-logo-red";
                    LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                    img2.ContentId = "FB_logo";
                    LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                    img3.ContentId = "YT_logo";
                    LinkedResource img4 = new LinkedResource(@"Html\linkedin.png", "image/png");
                    img4.ContentId = "linkedin";

                    avHtml.LinkedResources.Add(img1);
                    avHtml.LinkedResources.Add(img2);
                    avHtml.LinkedResources.Add(img3);
                    avHtml.LinkedResources.Add(img4);
                    message.AlternateViews.Add(avHtml);

                    //client.Credentials = new System.Net.NetworkCredential(mail_add, mail_pass);
                    //client.EnableSsl = false;

                    //client.Host = smtpSer;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = true;
                    //client.Credentials = new System.Net.NetworkCredential
                    //{
                    //    UserName = "no-reply@forteinsurance.com",
                    //    Password = "Forte@12345"
                    //};
                    //client.EnableSsl = true;

                    //client.Port = port;
                    //client.Send(message);
                    var Credential = new System.Net.NetworkCredential(mail_add, mail_pass);
                    var result = CommonFunctions.SendEmail(Credential, message);
                    message.Dispose();
                    //client.Dispose();


                    string remind = (isResend) ? "RESEND" : "NO";
                    crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                           new string[] { "Insert", tbClaimNo.Text, "Windscreen", tbTo.Text, content, "", "", remind, "", "", "", tbCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), Username });


                    Msgbox.Show("Email sent!");
                    Cursor.Current = Cursors.AppStarting;

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
                return false;
            }
        }

        private void bnSendEmail_Click(object sender, EventArgs e)
        {
            if (sendEmail())
            {
                lblSentDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                frmDocumentControl.disabledButt(bnSendEmail);
                frmDocumentControl.enabledButt(bnResendEmail);
            }
        }

        private void bnResendEmail_Click(object sender, EventArgs e)
        {
            if (sendEmail(true))
            {
                lblReSentDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                frmDocumentControl.disabledButt(bnResendEmail);
            }
        }

        private void dgvReport_DataSourceChanged(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvReport);
        }

        private void bnGetLetter_Click(object sender, EventArgs e)
        {
            if (fdbSelectPath.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fdbSelectPath.SelectedPath))
            {
                Cursor.Current = Cursors.WaitCursor;

                Attachment WSLetter = getMailAttachment(false);
                string LetterName = WSLetter.Name;

                string FilePath = fdbSelectPath.SelectedPath + @"\\" + LetterName + DateTime.Now.ToString(" yyyy_MM_dd_HH_mm_ss") + ".pdf";

                using (var fileStream = File.Create(FilePath))
                {
                    WSLetter.ContentStream.Seek(0, SeekOrigin.Begin);
                    WSLetter.ContentStream.CopyTo(fileStream);
                }

                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show("PDF exported!");

                System.Diagnostics.Process.Start(FilePath); //open file
            }
        }
    }
}
