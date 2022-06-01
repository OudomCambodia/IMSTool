using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iFont = iTextSharp.text.Font;
using System.Net.Mime;
using Testing.Properties;
using System.Reflection;
using System.Net;

namespace Testing.Forms
{
    public partial class frmDeductible : Form
    {
        public frmDeductible()
        {
            InitializeComponent();
        }

        CRUD crud = new CRUD();
        DataTable reportDt = new DataTable();
        DataTable clpolinfo = new DataTable(); //Query Claim Data
        DataTable cldetail = new DataTable();
        public string Username = "";
        string HashPass = "Forte@2017";
        string smtpSer;
        string mail_add;
        string mail_pass;
        int port;
        string UserFullName = "";

        //public static iTextSharp.text.Font GetTahoma()
        //{
        //    var fontName = "Tahoma";
        //    if (!FontFactory.IsRegistered(fontName))
        //    {
        //        var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\Resources\\tahoma.ttf";
        //        FontFactory.Register(fontPath);
        //    }
        //    return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //}

        public static BaseFont GetTahoma()
        {
            var fontName = "Tahoma";
            if (!FontFactory.IsRegistered(fontName))
            {
                FontFactory.Register(@"Html\tahoma.ttf");
            }
            return BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        }

        private Attachment getMailAttachment(string remindertext = "", string[] sentondate = null)
        {
            //getAllVar
            string SendingDate = DateTime.UtcNow.AddHours(7).ToString("dd MMMM yyyy");
            string AdditionalInsured = clpolinfo.Rows[0]["ADDITIONAL_INSURED"].ToString(),
                PolicyNo = clpolinfo.Rows[0]["POLICYNO"].ToString(),
                ClaimNo = clpolinfo.Rows[0]["CLAIM_NO"].ToString(),
                VehicleNo = clpolinfo.Rows[0]["INT_PRS_NAME"].ToString().Replace("\n", " "),
                DateofAccident = Convert.ToDateTime(clpolinfo.Rows[0]["DATEOFLOSS"]).ToString("dd MMMM yyyy"),
                PlaceofAccident = clpolinfo.Rows[0]["INT_PLACE_LOSS"].ToString(),
                ProductName = ClaimNo.Substring(7, 3),
                last4digit = ClaimNo.Substring(16),
                BranchCode = ClaimNo.Substring(4, 1);
            //get AddressLine,DedutibleText,CoInsurance
            DataTable dtTemp = crud.ExecQuery("select ADR_LOC_DESCRIPTION || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=3 and rownum = 1)) || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=2 and rownum = 1)) || ',' || chr(10) ||" +
"(select GPL_DESC from SM_M_GEOAREA_PARAMLN where GPL_CODE = (select SGD_GPL_DET_CODE from SM_M_GEOAREA_DETAILS where SGD_GPL_CODE = ADR_POSTAL_CODE and SGD_SMG_CODE=1 and rownum = 1)) as ADDRESS_LINE " +
"from UW_M_CUST_ADDRESSES where ADR_CUS_CODE = '" + clpolinfo.Rows[0]["INSUREDCODE"].ToString() + "'");
            string AddressLine = dtTemp.Rows[0]["ADDRESS_LINE"].ToString();

            //            dtTemp = crud.ExecQuery("select POL_EXCESS_TXT," +
            //"(select RFT_DESCRIPTION from CM_R_REFERENCE_TWO where RFT_CODE = " +
            //"PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO((select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '"+ClaimNo+"'),'CO-INSURANCE') AND RFT_TYPE = 'CI') as CO_INSURANCE " +
            //"from VIEW_POLICIES where POL_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + ClaimNo + "')");
            //            string DedutibleText = dtTemp.Rows[0]["POL_EXCESS_TXT"].ToString(),
            //                CoInsurance = dtTemp.Rows[0]["CO_INSURANCE"].ToString();
            //

            //Dedutible Table
            DataView view = new DataView(clpolinfo);
            //DataTable deducttbl = view.ToTable("Selected", false, "PERILS", "PAID_AMOUNT", "DEDUTIBLE", "OS_EXCESS_AMT", "DEDU_PAY_STATUS");
            DataTable deducttbl = view.ToTable("Selected", false, "PERILS", "CLAIM_AMOUNT", "DEDUTIBLE", "OS_EXCESS_AMT", "DEDU_PAY_STATUS");
            double TotalClmAmt = 0, TotalOS = 0;

            //bool PaidtblBlank = false;
            //if (dgvPaymentDetail.Rows.Count <= 0)
            //    PaidtblBlank = true;
            //else
            //    PaidtblBlank = false;

            foreach (DataRow dr in deducttbl.Rows)
            {
                //if (dr["DEDU_PAY_STATUS"].ToString() == "O/S")
                //{
                //    if(PaidtblBlank)
                //        TotalClmAmt += Convert.ToDouble(GetClAmt(dr["PERILS"].ToString()));
                //    else
                //        TotalClmAmt += Convert.ToDouble(GetPaidAmt(dr["PERILS"].ToString()));
                //    TotalOS += Math.Abs(Convert.ToDouble(dr["OS_EXCESS_AMT"].ToString()));
                //}

                //if (PaidtblBlank)
                //    TotalClmAmt += Convert.ToDouble(GetClAmt(dr["PERILS"].ToString()));
                //else
                //    TotalClmAmt += Convert.ToDouble(GetPaidAmt(dr["PERILS"].ToString()));

                double tmp = Convert.ToDouble(GetPaidAmt(dr["PERILS"].ToString()));
                if(tmp == 0)
                    tmp = Convert.ToDouble(GetClAmt(dr["PERILS"].ToString()));

                TotalClmAmt += tmp;

                TotalOS += Math.Abs(Convert.ToDouble(dr["OS_EXCESS_AMT"].ToString()));
            }
            //
            //

            //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
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
            iTextSharp.text.Image imgSoc = iTextSharp.text.Image.GetInstance("Html/Forte_Logo_bak.png");
            imgSoc.ScalePercent(80f);
            imgSoc.SetAbsolutePosition(25, 780);
            cb.AddImage(imgSoc);
            //


            if (remindertext != "")
            {

                PdfContentByte b = writer.DirectContent;
                //Reminder sign
                PdfPTable reminder = new PdfPTable(1);
                reminder.TotalWidth = 125f;
                reminder.LockedWidth = true;
                PdfPCell reminderc = new PdfPCell(new Phrase(remindertext, new iFont(bf, 12, iTextSharp.text.Font.BOLD)));
                reminderc.VerticalAlignment = Element.ALIGN_MIDDLE;
                reminderc.HorizontalAlignment = 1;
                reminderc.FixedHeight = 30f;
                reminder.AddCell(reminderc);
                //
                reminder.WriteSelectedRows(0, -1, 425, 730, b);
                if (remindertext == "FINAL REMINDER" && sentondate != null)
                {
                    //Sent on
                    PdfPTable senton = new PdfPTable(1);
                    senton.TotalWidth = 125f;
                    senton.LockedWidth = true;
                    c1 = new Chunk("This letter has been sent on:\n\n", new iFont(bf, 8));
                    c2 = new Chunk("1. " + sentondate[0] + "\n2. " + sentondate[1] + "\n3. " + sentondate[2] + "\n4. " + sentondate[3] + "\n5. " + SendingDate + " (this letter)", new iFont(bf, 8));
                    ph = new Phrase();
                    ph.Add(c1); ph.Add(c2);
                    PdfPCell sentonc = new PdfPCell(ph);
                    sentonc.VerticalAlignment = 0;
                    sentonc.HorizontalAlignment = 0;
                    senton.AddCell(sentonc);
                    senton.WriteSelectedRows(0, -1, 425, 695, b);
                    //
                }
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


            doc.Add(new Paragraph("RE:      DEDUCTIBLE/CO-INSURANCE REIMBURSEMENT", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
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
            doc.Add(new Paragraph("We would like to inform you that your "
                + crud.ExecQuery("SELECT PRD_DESCRIPTION FROM UW_M_PRODUCTS WHERE PRD_CODE = '" + ClaimNo.Substring(7, 3) + "'").Rows[0][0].ToString() //get Product Desc
                + " Policy has a Deductible and Co-insurance as follows:", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            //Deduc,CoIn
            PdfPTable deco = new PdfPTable(2);
            deco.TotalWidth = doc.PageSize.Width - 80f;
            deco.LockedWidth = true;
            float[] widths = new float[] { 1, 6 };
            deco.SetWidths(widths);
            deco.DefaultCell.FixedHeight = 35f;
            deco.HorizontalAlignment = 0;

            PdfPCell decoc1 = new PdfPCell(new Phrase("DEDUCTIBLE:", new iFont(bf, 8, iTextSharp.text.Font.BOLD)));
            decoc1.BorderWidth = 0;
            deco.AddCell(decoc1);
            decoc1 = new PdfPCell(new Phrase(tbDeductible.Text, new iFont(bf, 8)));
            decoc1.BorderWidth = 0;
            deco.AddCell(decoc1);

            decoc1 = new PdfPCell(new Phrase("CO-INSURANCE:", new iFont(bf, 8, iTextSharp.text.Font.BOLD)));
            decoc1.BorderWidth = 0;
            deco.AddCell(decoc1);
            decoc1 = new PdfPCell(new Phrase(tbCoIn.Text, new iFont(bf, 8)));
            decoc1.BorderWidth = 0;
            deco.AddCell(decoc1);

            doc.Add(deco);
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(new Paragraph("In the Accident involving the insured Vehicle with vehicle R/N as referenced above, the following Deductible/Co-insurance amount applies:", new iFont(bf, 9)));


            //Coverage Table
            PdfPTable coveragetbl = new PdfPTable(3);
            coveragetbl.TotalWidth = doc.PageSize.Width - 80f;
            coveragetbl.LockedWidth = true;
            coveragetbl.DefaultCell.FixedHeight = 35f;
            coveragetbl.HorizontalAlignment = 0;


            PdfPCell cell = new PdfPCell(new Phrase("COVERAGE", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);
            //cell = new PdfPCell(new Phrase((PaidtblBlank) ? "CLAIMED AMOUNT (USD)" : "PAID AMOUNT (USD)", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell = new PdfPCell(new Phrase("CLAIMED/PAID AMOUNT (USD)", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);
            cell = new PdfPCell(new Phrase("DEDUCTIBLE/CO-INSURANCE AMOUNT (USD) *", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);

            bool HasTowing = false; //check Towing Fee Peril
            //Deductible Table Data
            foreach (DataRow dr in deducttbl.Rows)
            {
                cell = new PdfPCell(new Phrase(dr["PERILS"].ToString(), new iFont(bf, 9)));
                cell.VerticalAlignment = 1;
                cell.HorizontalAlignment = 1;
                coveragetbl.AddCell(cell);

                //get Paid Amount for peril
                string Peril = dr["PERILS"].ToString();
                //if (PaidtblBlank)
                //    cell = new PdfPCell(new Phrase(String.Format("{0:N}", Convert.ToDecimal(GetClAmt(Peril))), new iFont(bf, 9)));
                //else
                //    cell = new PdfPCell(new Phrase(String.Format("{0:N}", Convert.ToDecimal(GetPaidAmt(Peril))), new iFont(bf, 9)));
                decimal tmp = Convert.ToDecimal(GetPaidAmt(Peril));
                if(tmp == 0)
                    tmp = Convert.ToDecimal(GetClAmt(Peril));
                cell = new PdfPCell(new Phrase(String.Format("{0:N}", tmp), new iFont(bf, 9)));


                cell.VerticalAlignment = 1;
                cell.HorizontalAlignment = 1;
                coveragetbl.AddCell(cell);
                //
                if (Peril.ToUpper().Contains("TOWING")) HasTowing = true;

                cell = new PdfPCell(new Phrase(String.Format("{0:N}", Math.Abs(Convert.ToDecimal(dr["OS_EXCESS_AMT"].ToString()))), new iFont(bf, 9)));
                cell.VerticalAlignment = 1;
                cell.HorizontalAlignment = 1;
                coveragetbl.AddCell(cell);
            }
            //
            cell = new PdfPCell(new Phrase("TOTAL", new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Format("{0:N}", Convert.ToDecimal(TotalClmAmt)), new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Format("{0:N}", Convert.ToDecimal(TotalOS)), new iFont(bf, 9, iTextSharp.text.Font.BOLD)));
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;
            coveragetbl.AddCell(cell);
            //
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));
            doc.Add(coveragetbl);
            if (HasTowing)
                doc.Add(new Paragraph("* For Towing Fee, this is an amount exceeding the maximum benefit of USD " + ((ProductName == "CYC") ? "50.00" : "150.00") + " provided under your policy.", new iFont(bf, 9)));
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            //Last Para
            Phrase ph1 = new Phrase("Given that the Claimed amount is agreed, please pay your Policy Deductible/Co-insurance of ", new iFont(bf, 9));
            Phrase ph2 = new Phrase("USD " + String.Format("{0:N}", Convert.ToDecimal(TotalOS)), new iFont(bf, 9, iTextSharp.text.Font.BOLD));
            Phrase ph3 = new Phrase(" to Forte Insurance (Cambodia) Plc. via the below Payment Methods:", new iFont(bf, 9));
            Paragraph lastpara = new Paragraph();
            lastpara.Add(ph1);
            lastpara.Add(ph2);
            lastpara.Add(ph3);
            doc.Add(lastpara);
            //doc.Add(new Paragraph("Given that the Claimed amount is agreed, please pay your Policy Deductible/Co-insurance of USD " + String.Format("{0:N}", Convert.ToDecimal(TotalOS)) +
            //    " to Forte Insurance (Cambodia) Plc. via the below Payment Methods:", new iFont(bf, 9)));
            //
            doc.Add(new Paragraph(" ", new iFont(bf, 9)));

            //Payment table
            PdfPTable paymenttbl = new PdfPTable(4);
            paymenttbl.TotalWidth = doc.PageSize.Width - 80f;
            paymenttbl.LockedWidth = true;
            paymenttbl.DefaultCell.FixedHeight = 80f;
            paymenttbl.HorizontalAlignment = 0;

            //paymenttbl.AddCell(new PdfPCell(new Phrase("1. Pay by cash", new iFont(bf, 9))));
            //cell = new PdfPCell(new Phrase("at our office at Vattanac Capital (level 18), Monivong Blvd., Sangkat Wat Phnom, Khan Daun Penh, Phnom Penh, Cambodia", new iFont(bf, 9)));
            //cell.Colspan = 3;
            //paymenttbl.AddCell(cell);
            cell = new PdfPCell(new Phrase("1. Pay at our office at Vattanac Capital (level 18), Monivong Blvd., Sangkat Wat Phnom, Khan Daun Penh, Phnom Penh,\n    Cambodia or any of our Forte branches closest to you.", new iFont(bf, 9)));
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
            c2 = new Chunk("DC" + BranchCode + ProductName + last4digit + "-……………… [Your Mobile No.]", new iFont(bf, 9, iTextSharp.text.Font.BOLD));
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

            //PdfWriter pdfWriter = PdfWriter.GetInstance(doc, instream);
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
            string LetterName = "DC Letter - " + tbRiskName.Text.Replace("\n", " ") + " - " + Convert.ToDateTime(tbDateofLoss.Text).ToString("dd MMMM yyyy");
            if (remindertext != "") //Not the first letter
                LetterName += " - " + remindertext;
            //

            laststream.Position = 0;
            //return new Attachment(mstream, LetterName + ".pdf");
            return new Attachment(laststream, LetterName + ".pdf");
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            tbClaimNo.Text = "";
            tbClaimNo.Focus();
            clearControl();
            bnRiskEndoDetail.Enabled = false;
        }

        private void clearControl()
        {
            foreach (Control ctl in groupBox1.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            foreach (Control ctl in groupBox2.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }

                if (ctl is RichTextBox)
                {
                    ((RichTextBox)ctl).Text = ""; 
                }
            }

            foreach (Control ctl in groupBox7.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = "";
                }
            }

            dgvClaimDetail.DataSource = null;
            dgvClaimDetail.Columns.Clear();
            dgvPaymentDetail.DataSource = null;
            dgvPaymentDetail.Columns.Clear();

            tbTo.Text = "";
            tbCC.Text = "";

            frmDocumentControl.disabledButt(bnSendEmail);
            frmDocumentControl.disabledButt(bnSend1stRemind);
            frmDocumentControl.disabledButt(bnSend2ndRemind);
            frmDocumentControl.disabledButt(bnSend3rdRemind);
            frmDocumentControl.disabledButt(bnLastRemind);

            lblSentDate.Text = "";
            lblSentDate1stRemind.Text = "";
            lblSentDate2ndRemind.Text = "";
            lblSentDate3rdRemind.Text = "";
            lblSentDateLastRemind.Text = "";

            lblTotalOS.Text = "";

            dgvFile.Rows.Clear();

            btnGetDCLetter.Enabled = false;

            tbNote.Text = "";
            btnUpdate.Enabled = false;
        }

        private void bnClaimSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string clno = tbClaimNo.Text.Trim().ToUpper();

                clearControl();

                if (clno == "" || clno.Length != 20)
                {
                    Msgbox.Show("Please Input Claim No!");
                    return;
                }

                string IntFr = "2015/01/01 00:00:00",
                    IntTo = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";

                string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler" };
                string[] Values = new string[] { "MAIN_REPORT", IntFr, IntTo, clno, "", "" };


                clpolinfo = crud.ExecSP_OutPara("SP_DEDUCTIBLE", Key, Values);
                if (clpolinfo.Rows.Count <= 0)
                {
                    Msgbox.Show("Claim No not found!");
                    tbClaimNo.Focus();
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //DataTable dtTemp = crud.ExecQuery("SELECT POL_PERIOD_FROM,POL_PERIOD_TO FROM VIEW_POLICIES WHERE POL_SEQ_NO = " +
                    //    "(SELECT INT_POLICY_SEQ FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + clno + "')");


                    tbRiskName.Text = clpolinfo.Rows[0]["INT_PRS_NAME"].ToString();
                    tbDateofLoss.Text = Convert.ToDateTime(clpolinfo.Rows[0]["DATEOFLOSS"]).ToString("dd'/'MM'/'yyyy");
                    //tbDateofLoss.Text = DateTime.ParseExact(clpolinfo.Rows[0]["DATEOFLOSS"].ToString(), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd'/'MM'/'yyyy");
                    tbPlaceofAcc.Text = clpolinfo.Rows[0]["INT_PLACE_LOSS"].ToString();
                    tbLossInfo.Text = clpolinfo.Rows[0]["LOSS_DESC"].ToString();
                    tbIncurredAmount.Text = String.Format("{0:N}", clpolinfo.Rows[0]["INCURRED_AMT"]);
                    tbInsured.Text = clpolinfo.Rows[0]["ADDITIONAL_INSURED"].ToString();
                    tbPolicyNo.Text = clpolinfo.Rows[0]["POLICYNO"].ToString();
                    tbPolicyPeriod.Text = "from " + (Convert.ToDateTime(clpolinfo.Rows[0]["INT_PERIOD_FROM"]).ToString("dd'/'MM'/'yyyy"))
                        + " to " + (Convert.ToDateTime(clpolinfo.Rows[0]["INT_PERIOD_TO"]).ToString("dd'/'MM'/'yyyy"));
                    tbIntermediary.Text = clpolinfo.Rows[0]["AGENT_CODE"].ToString() + " - " + clpolinfo.Rows[0]["AGENT_NAME"].ToString();
                    tbAH.Text = clpolinfo.Rows[0]["ACCOUNT_HANDLER"].ToString();
                    tbSumInsured.Text = String.Format("{0:N}", clpolinfo.Rows[0]["RISK_SUM_INSURED"]);
                    tbPolicyPremium.Text = String.Format("{0:N}", clpolinfo.Rows[0]["POL_PREMIUM"]);
                    string PaymentStatus = clpolinfo.Rows[0]["POL_PAYMENT_STATUS"].ToString();
                    tbPaidorOS.Text = (PaymentStatus == "Y") ? "PAID" : "O/S";

                    //Payment Detail Table
                    DataTable Paymenttbl = crud.ExecQuery("select PAID_PERILS, SUM(RRD_VALUE) as PAID_AMOUNT from " +
                    "(select (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                    "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=RRD_PERIL_CODE) as PAID_PERILS,RRD_VALUE " +
                    "from CL_T_PROV_REVISION_DTLS where RRD_CLAIM_NO = '" + clno + "' and RRD_FUNCTION_ID = 'PY' and RRD_VALUE <> 0) group by PAID_PERILS");
                    dgvPaymentDetail.DataSource = Paymenttbl;
                    dgvPaymentDetail.Columns["PAID_AMOUNT"].DefaultCellStyle.Format = "c";
                    //

                    //Provision Detail
                    cldetail = crud.ExecQuery("select PROV_PERILS, SUM(CL_AMOUNT) CLAIMED_AMOUNT " +
                    "from " +
                    "( " +
                            "SELECT (case PRD_PRV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                            "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=A.PRD_PERIL_CODE) as PROV_PERILS,A.PRD_VALUE CL_AMOUNT " +
                            "FROM CL_T_PROVISION_DTLS A " +
                            "WHERE A.PRD_CLAIM_NO = '" + clno + "' " +
                            "AND PRD_FUNCTION_ID <> 'RC' " +
                            "AND TRUNC(A.PRD_CREATED_DATE) <= TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59', 'YYYY/MM/DD HH24:MI:SS') " +
                            "UNION ALL " +
                            "SELECT (case RRD_REV_TYPE when 'TRA000' then 'MAIN CLAIM' when 'TRA014' then 'OTHER' end) || ' / ' || " +
                            "(SELECT PRL_DESCRIPTION FROM UW_R_PERILS WHERE PRL_CODE=A.RRD_PERIL_CODE) as PROV_PERILS,(A.RRD_VALUE)*-1 CL_AMOUNT " +
                            "FROM CL_T_PROV_REVISION_DTLS A " +
                            "WHERE A.RRD_CLAIM_NO = '" + clno + "' " +
                            "AND RRD_FUNCTION_ID <> 'PY' " +
                            "AND A.RRD_REV_TYPE<>'TRA017' " +
                            "AND TRUNC(A.RRD_CREATED_DATE) <= TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59', 'YYYY/MM/DD HH24:MI:SS') ) Group by PROV_PERILS");
                    //



                    //Claim Detail Table
                    DataView view = new DataView(clpolinfo);
                    DataTable cldetailtbl = view.ToTable("Selected", false, "PERILS", "PAID_AMOUNT", "DEDUTIBLE", "OS_EXCESS_AMT", "DEDU_PAY_STATUS");
                    dgvClaimDetail.DataSource = cldetailtbl;
                    dgvClaimDetail.Columns["OS_EXCESS_AMT"].Visible = false;
                    dgvClaimDetail.Columns["PAID_AMOUNT"].Visible = false;
                    //dgvClaimDetail.Columns["PAID_AMOUNT"].DefaultCellStyle.Format = "c";
                    dgvClaimDetail.Columns["DEDUTIBLE"].DefaultCellStyle.Format = "c";
                    //Calculate Total OS
                    double TotalOS = 0;
                    foreach (DataRow dr in cldetailtbl.Rows)
                    {
                        if (dr["DEDU_PAY_STATUS"].ToString() == "O/S")
                            TotalOS += Math.Abs(Convert.ToDouble(dr["OS_EXCESS_AMT"].ToString()));
                    }
                    lblTotalOS.Text = String.Format("{0:N}", Convert.ToDecimal(TotalOS));
                    //
                    //

                    //Deductible,CoIn
                    DataTable dtTemp = crud.ExecQuery("select POL_EXCESS_TXT, " +
                    "(select RFT_DESCRIPTION from CM_R_REFERENCE_TWO where RFT_CODE =  " +
                    "PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO((select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "'),'CO-INSURANCE') AND RFT_TYPE = 'CI') as CO_INSURANCE from " +
                    "(select POL_SEQ_NO,POL_EXCESS_TXT from UW_T_POLICIES where POL_CLA_CODE = 'AUTO' and POL_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select EDT_SEQ_NO,EDT_EXCESS_TXT from UW_T_ENDORSEMENTS where EDT_CLA_CODE = 'AUTO' and EDT_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select PHS_SEQ_NO,PHS_EXCESS_TXT from UW_H_POLICY_HISTORY where PHS_CLA_CODE = 'AUTO' and PHS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "') " +
                    "union " +
                    "select NDS_SEQ_NO,NDS_EXCESS_TXT from UW_H_ENDORSEMENT_HISTORY where NDS_CLA_CODE = 'AUTO' and NDS_SEQ_NO = (select INT_POLICY_SEQ from CL_T_INTIMATION where INT_CLAIM_NO = '" + clno + "')) T1");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string DedutibleText = dtTemp.Rows[0]["POL_EXCESS_TXT"].ToString(),
                        CoInsurance = dtTemp.Rows[0]["CO_INSURANCE"].ToString();
                        tbDeductible.Text = (DedutibleText == "") ? "N/A" : DedutibleText;
                        tbCoIn.Text = (CoInsurance == "") ? "N/A" : CoInsurance;
                    }

                    //Risk Info
                    dtTemp = crud.ExecSP_OutPara("SP_MOTOR_CL_INFO", new string[] { "WKCLAIMNO" }, new string[] { clno });
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbVechileNo.Text = tbRiskName.Text;
                        tbMakeModel.Text = dtTemp.Rows[0]["MAKE_MODEL"].ToString();
                        tbYearOfManu.Text = dtTemp.Rows[0]["YEAR_OF_MANUFACTURE"].ToString();
                        tbChasisNo.Text = dtTemp.Rows[0]["CHASSIS_NO"].ToString();
                        tbEngineNo.Text = dtTemp.Rows[0]["ENGINE_NO"].ToString();
                        tbCapacity.Text = dtTemp.Rows[0]["CAPACITY"].ToString() + " " + dtTemp.Rows[0]["CAPACITY_TYPE"].ToString();
                    }
                    //

                    btnUpdate.Enabled = true;  
                    //Note
                    dtTemp = crud.ExecQuery("SELECT NOTE FROM USER_LETTER_HIST WHERE REF_NO = '" + clno + "' AND LETTER_TYPE = 'AUTO DEDUCTIBLE'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbNote.Text = dtTemp.Rows[0]["NOTE"].ToString();
                    }
                    //

                    if (TotalOS != 0)//no OS no need to send Email
                    {
                        //Check History
                        DataTable histtbl = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_HIST WHERE CLAIM_NO = '" + clno + "' AND SEND_TYPE = 'Deductible' ORDER BY HIST_DATE");
                        if (histtbl.Rows.Count <= 0) //no history
                        {
                            frmDocumentControl.enabledButt(bnSendEmail);
                        }
                        else //have history
                        {
                            //tbTo.Text = histtbl.Rows[histtbl.Rows.Count - 1]["RECEIVER"].ToString();
                            //tbCC.Text = histtbl.Rows[histtbl.Rows.Count - 1]["CC"].ToString();

                            Button btnToEnable = bnSendEmail; //will enable to send

                            foreach (DataRow dr in histtbl.Rows)
                            {
                                if (dr["REM_NO"].ToString() == "NO") //First Email
                                {
                                    lblSentDate.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = bnSend1stRemind;
                                }
                                else if (dr["REM_NO"].ToString() == "FIRST") //First Reminder
                                {
                                    lblSentDate1stRemind.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = bnSend2ndRemind;
                                }
                                else if (dr["REM_NO"].ToString() == "SECOND") //Second Reminder
                                {
                                    lblSentDate2ndRemind.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = bnSend3rdRemind;
                                }
                                else if (dr["REM_NO"].ToString() == "THIRD") //Third Reminder
                                {
                                    lblSentDate3rdRemind.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = bnLastRemind;
                                }
                                else if (dr["REM_NO"].ToString() == "FINAL") //Final Reminder
                                {
                                    lblSentDateLastRemind.Text = Convert.ToDateTime(dr["HIST_DATE"]).ToString("dd MMMM yyyy");
                                    btnToEnable = null;
                                }
                            }

                            if (btnToEnable != null) //still has Reminder to send
                                frmDocumentControl.enabledButt(btnToEnable);
                            else
                                frmDocumentControl.enabledButt(bnLastRemind);
                        }
                        //

                        //get Account Handler email, CC
                        string agentcode = clpolinfo.Rows[0]["AGENT_CODE"].ToString(), ahcode = clpolinfo.Rows[0]["ACCOUNT_HANDLER"].ToString(), selectedcode;
                        if (agentcode[0] == '0' || ahcode == "U-BNK") //001,002,003 => no agent/broker          //U-BNK => bank 
                            selectedcode = ahcode;
                        else //agent/broker...
                            selectedcode = agentcode;

                        dtTemp = crud.ExecQuery("SELECT MAIL_TO,MAIL_CC FROM USER_AUTO_CLAIM_EMAIL WHERE CODE = '" + selectedcode + "'");
                        if (dtTemp.Rows.Count > 0)
                        {
                            tbTo.Text = dtTemp.Rows[0]["MAIL_TO"].ToString();
                            tbCC.Text = dtTemp.Rows[0]["MAIL_CC"].ToString();
                        }
                        //
                    }
                    Cursor.Current = Cursors.AppStarting;
                    bnRiskEndoDetail.Enabled = true;
                    btnGetDCLetter.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void frmDeductible_Activated(object sender, EventArgs e)
        {
            tbClaimNo.Focus();
        }

        private void tbClaimNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbClaimNo.Text = tbClaimNo.Text.ToUpper();
                bnClaimSearch.PerformClick();
            }
        }

        private void tbRiskName_TextChanged(object sender, EventArgs e)
        {

        }

        private void bnSearchReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpIntDateTo.Value < dtpIntDateFr.Value)
                {
                    Msgbox.Show("Intimation date to cannot smaller than intimation date from.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;


                string IntFr = dtpIntDateFr.Value.ToString("yyyy/MM/dd") + " 00:00:00",
                    IntTo = dtpIntDateTo.Value.ToString("yyyy/MM/dd") + " 23:59:59",
                    Customer = tbCustomerFilter.Text.Trim().ToUpper(),
                    AH = tbAHFilter.Text.Trim().ToUpper();

                string[] Key = new string[] { "p_type", "p_int_date_fr", "p_int_date_to", "p_claim_no", "p_cus_name", "p_acc_handler" };
                string[] Values = new string[] { "MAIN_REPORT", IntFr, IntTo, "", Customer, AH };


                reportDt = crud.ExecSP_OutPara("SP_DEDUCTIBLE", Key, Values);

                DataRow[] temp = null;

                if (cbDeductibleCoin.Text == "Paid" || cbDeductibleCoin.Text == "OS")
                {
                    if (cbDeductibleCoin.Text == "Paid")
                    {
                        temp = reportDt.Select("DEDU_PAY_STATUS = 'PAID'");

                    }
                    else if (cbDeductibleCoin.Text == "OS")
                    {
                        temp = reportDt.Select("DEDU_PAY_STATUS = 'O/S'");
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

                //reportDt = reportDt.AsEnumerable().Where(x => (x["DEDU_PAY_STATUS"]) == "PAID").CopyToDataTable();

                dgvReport.DataSource = reportDt;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void bnClearReport_Click(object sender, EventArgs e)
        {
            dtpIntDateFr.Value = DateTime.Now;
            dtpIntDateTo.Value = DateTime.Now;
            dgvReport.DataSource = null;
            dgvReport.Rows.Clear();
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
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Dest + @"\\" + "Deductible Report" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".pdf", FileMode.Create));
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
            Paragraph header = new Paragraph("Deductible Report " + DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
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

            } document.Add(table);
            document.Close();
        }

        private void frmDeductible_Load(object sender, EventArgs e)
        {
            cbDeductibleCoin.SelectedIndex = 0;
            cbExportAs.SelectedIndex = 0;
            tbClaimNo.Focus();

            clearControl();

            dgvReport.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClaimDetail.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvClaimDetail.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvPaymentDetail.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvPaymentDetail.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            bnRiskEndoDetail.Enabled = false;

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

            //if (mail_add.Trim() == "" || mail_pass == "")
            //{
            //    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
            //    this.Close();
            //    return;
            //}
        }

        private void bnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (sendEmail())
                {
                    lblSentDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                    frmDocumentControl.disabledButt(bnSendEmail);
                    frmDocumentControl.enabledButt(bnSend1stRemind);
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }


        private bool sendEmail(string reminder = "", string[] reminddate = null)
        {
            try
            {
                if (mail_add.Trim() == "" || mail_pass == "")
                {
                    Msgbox.Show("Your account does not have enough information to be able to send the email! Please contact the system admin to update your account.");
                    return false;
                }

                if (tbRiskName.Text.Trim() == "")
                {
                    Msgbox.Show("Please search claim first!");
                    return false;
                }

                if (tbTo.Text.Trim() == "")
                {
                    Msgbox.Show("Please input email to!");
                    return false;
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to send Email?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //Get OSday,SecondNote
                    string OSday = "0";
                    if (lblSentDate.Text.Trim() != "")
                    {
                        DateTime FirstMailDate = Convert.ToDateTime(lblSentDate.Text.Trim());
                        FirstMailDate = new DateTime(FirstMailDate.Year, FirstMailDate.Month, FirstMailDate.Day, 00, 00, 00);
                        DateTime CurDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                        OSday = (CurDate - FirstMailDate).TotalDays.ToString();
                    }

                    string SecondNote = "";
                    ////First Condition: check whether policy is cancelled or risk is deleted
                    DataTable dtTemp = crud.ExecQuery("SELECT INT_POLICY_SEQ FROM CL_T_INTIMATION WHERE INT_CLAIM_NO = '" + clpolinfo.Rows[0]["CLAIM_NO"].ToString() + "'");
                    string PolSeq = dtTemp.Rows[0]["INT_POLICY_SEQ"].ToString(),
                        RiskName = clpolinfo.Rows[0]["INT_PRS_NAME"].ToString();
                    //Check risk is deleted                
                    dtTemp = crud.ExecQuery("SELECT PRS_DATE_DELETED FROM VIEW_RISKS WHERE PRS_PLC_POL_SEQ_NO = '" + PolSeq + "' AND PRS_NAME = '" + RiskName + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        string DeletedDate = dtTemp.Rows[0]["PRS_DATE_DELETED"].ToString();
                        if (DeletedDate != "" && DeletedDate != null)
                            SecondNote = "The Vehicle has been deleted from the Policy on " + Convert.ToDateTime(DeletedDate).ToString("dd MMMM yyyy") + ".";
                    }
                    //
                    if (SecondNote == "") //prev condition not met
                    {
                        dtTemp = crud.ExecQuery("SELECT POL_CANCELLED_DATE FROM VIEW_POLICIES WHERE POL_SEQ_NO = '" + PolSeq + "'");
                        if (dtTemp.Rows.Count > 0)
                        {
                            string CancelledDate = dtTemp.Rows[0]["POL_CANCELLED_DATE"].ToString();
                            if (CancelledDate != "" && CancelledDate != null)
                                SecondNote = "The Policy was cancelled on " + Convert.ToDateTime(CancelledDate).ToString("dd MMMM yyyy") + ".";
                        }
                    }
                    ////
                    ////Second Condition: check whether policy not yet expire
                    string[] PolExpDate = tbPolicyPeriod.Text.Substring(tbPolicyPeriod.Text.Length - 10).Split('/'); //[0]:day [1]:month [2]:year
                    if (SecondNote == "") //prev condition not met
                    {
                        DateTime ExpDate = new DateTime(Convert.ToInt32(PolExpDate[2]), Convert.ToInt32(PolExpDate[1]), Convert.ToInt32(PolExpDate[0]), 23, 59, 59);
                        if (ExpDate >= DateTime.Now) //not yet expired
                        {
                            SecondNote = "This Policy is going to expire on " + ExpDate.ToString("dd MMMM yyyy")
                                + " which is " + Math.Truncate((ExpDate - DateTime.Now).TotalDays).ToString() + " days from today.";
                        }
                        else //already expired
                        {
                            dtTemp = crud.ExecQuery("select * from VIEW_POLICIES where POL_POLICY_NO = '" + clpolinfo.Rows[0]["POLICYNO"].ToString()
                                + "' and POL_STATUS in (4,5,6,10) and POL_PERIOD_FROM >= TO_DATE('" + ExpDate.ToString("dd'/'MM'/'yyyy") + "','DD/MM/YYYY')");
                            bool isRenew = (dtTemp.Rows.Count > 0) ? true : false;
                            SecondNote = "This Policy already expired on " + ExpDate.ToString("dd MMMM yyyy")
                                + " and is " + ((isRenew) ? "renewed." : "not renewed.");
                        }
                    }
                    ////
                    //
                    ////Sent History
                    string SentHistory = "";
                    if (reminder == "FINAL REMINDER")
                    {
                        SentHistory = "We have also sent the same letter on the following date:<br/>" +
                            "1. " + reminddate[0] + "<br/>" +
                            "2. " + reminddate[1] + "&nbsp;&nbsp;&nbsp;&nbsp;1st REMINDER<br/>" +
                            "3. " + reminddate[2] + "&nbsp;&nbsp;&nbsp;&nbsp;2nd REMINDER<br/>" +
                            "4. " + reminddate[3] + "&nbsp;&nbsp;&nbsp;&nbsp;3rd REMINDER<br/>" +
                            "5. " + DateTime.Now.ToString("dd MMMM yyyy") + "&nbsp;&nbsp;&nbsp;&nbsp;FINAL REMINDER (this letter)<br/>";
                    }
                    ////

                    string content = crud.ExecFunc_String("USER_GET_EMAIL_CONTENT_AUTO",
                        new string[] { "MailType","Reminder","AdditionalInsured","PolicyNo", "ClaimNo",
                        "VehicleNo","DateofLoss","PlaceofAccident","OSday", "SecondNote", "SentHistory" },
                        new string[] { "Deductible", reminder,tbInsured.Text, 
                    tbPolicyNo.Text,tbClaimNo.Text,tbRiskName.Text,Convert.ToDateTime(tbDateofLoss.Text).ToString("dd MMMM yyyy"),
                    tbPlaceofAcc.Text, OSday, SecondNote, SentHistory}).ToString();
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader("Html/2020Email.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{text}", content);
                    body = body.Replace("{department}", "Claims Department");
                    body = body.Replace("{username}", UserFullName);
                    body = body.Replace("{user_email}", mail_add);

                    //SmtpClient client = new SmtpClient(smtpSer);

                    MailMessage message = new MailMessage();

                    //set formatting email message
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    message.From = new MailAddress(mail_add);
                    message.Attachments.Add(getMailAttachment(reminder, reminddate));

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

                    message.Subject = crud.ExecQuery("SELECT EMAIL_SUB FROM USER_CLAIM_EMAIL WHERE EMAIL_TYPE = 'Deductible'").Rows[0][0].ToString();
                    message.Subject = message.Subject.Replace("%VehicleNo%", tbRiskName.Text.Replace("\n", " "));
                    message.Subject = message.Subject.Replace("%DateofLoss%", Convert.ToDateTime(tbDateofLoss.Text).ToString("dd MMMM yyyy"));

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
                    //

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
                    if (dgvFile.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgvFile.Rows)
                        {
                            message.Attachments.Add(new Attachment(dgvr.Cells[1].Value.ToString()));
                        }
                    }
                    //

                    //embeded pictures
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource img1 = new LinkedResource(@"Html\Standard_Forte.png", "image/png");
                    img1.ContentId = "Forte_Logo";
                    LinkedResource img2 = new LinkedResource(@"Html\fb.png", "image/png");
                    img2.ContentId = "FB_logo";
                    LinkedResource img3 = new LinkedResource(@"Html\yt.png", "image/png");
                    img3.ContentId = "YT_logo";
                    LinkedResource img4 = new LinkedResource(@"Html\mail.png", "image/png");
                    img4.ContentId = "Mail_logo";

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


                    string remind = "NO"; // First Email
                    if (reminder == "1st REMINDER") remind = "FIRST";
                    else if (reminder == "2nd REMINDER") remind = "SECOND";
                    else if (reminder == "3rd REMINDER") remind = "THIRD";
                    else if (reminder == "FINAL REMINDER") remind = "FINAL";
                    crud.ExecSP_NoOutPara("sp_user_claim_input", new string[] { "cl_input_type", "cl_e_claim", "cl_e_type", "cl_e_rec", "cl_e_cont", "cl_e_doc", "cl_e_req", "cl_e_rem", "cl_e_non", "cl_e_re", "cl_e_dr", "cl_e_cc", "cl_e_rec_date", "cl_e_user" },
                           new string[] { "Insert", tbClaimNo.Text, "Deductible", tbTo.Text, content, "", "", remind, "", "", "", tbCC.Text, DateTime.Now.ToString("dd-MMM-yyyy"), Username });


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

        private void bnSend1stRemind_Click(object sender, EventArgs e)
        {
            if (sendEmail("1st REMINDER"))
            {
                lblSentDate1stRemind.Text = DateTime.Now.ToString("dd MMMM yyyy");
                frmDocumentControl.disabledButt(bnSend1stRemind);
                frmDocumentControl.enabledButt(bnSend2ndRemind);
            }
        }

        private void bnSend2ndRemind_Click(object sender, EventArgs e)
        {
            if (sendEmail("2nd REMINDER"))
            {
                lblSentDate2ndRemind.Text = DateTime.Now.ToString("dd MMMM yyyy");
                frmDocumentControl.disabledButt(bnSend2ndRemind);
                frmDocumentControl.enabledButt(bnSend3rdRemind);
            }
        }

        private void bnSend3rdRemind_Click(object sender, EventArgs e)
        {
            if (sendEmail("3rd REMINDER"))
            {
                lblSentDate3rdRemind.Text = DateTime.Now.ToString("dd MMMM yyyy");
                frmDocumentControl.disabledButt(bnSend3rdRemind);
                frmDocumentControl.enabledButt(bnLastRemind);
            }
        }

        private void bnLastRemind_Click(object sender, EventArgs e)
        {
            string[] prevdate = new string[] { lblSentDate.Text, lblSentDate1stRemind.Text, lblSentDate2ndRemind.Text, lblSentDate3rdRemind.Text };
            if (sendEmail("FINAL REMINDER", prevdate))
            {
                lblSentDateLastRemind.Text = DateTime.Now.ToString("dd MMMM yyyy");
            }
        }

        private void dgvClaimDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvClaimDetail);
        }

        private void dgvReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvReport);
        }

        private void tbClaimNo_Leave(object sender, EventArgs e)
        {
            tbClaimNo.Text = tbClaimNo.Text.ToUpper();
        }

        private void bnRiskEndoDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var frm = new frmDeductibleRiskEndo(clpolinfo.Rows[0]["INT_PRS_NAME"].ToString(), clpolinfo.Rows[0]["POLICYNO"].ToString());
            frm.ShowDialog();
            Cursor.Current = Cursors.AppStarting;
        }

        private void dgvPaymentDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvPaymentDetail);

        }


        string GetPaidAmt(string peril)
        {
            string PaidAmt = "0";
            foreach (DataGridViewRow dr in dgvPaymentDetail.Rows)
            {
                string per = dr.Cells["PAID_PERILS"].Value.ToString(), amt = dr.Cells["PAID_AMOUNT"].Value.ToString();
                if (per.Contains(peril) && per.Contains("MAIN CLAIM"))
                {
                    PaidAmt = amt;
                }
            }
            return PaidAmt;
        }

        string GetClAmt(string peril)
        {
            string ClAmt = "0";
            foreach (DataRow dr in cldetail.Rows)
            {
                string per = dr["PROV_PERILS"].ToString(), amt = dr["CLAIMED_AMOUNT"].ToString();
                if (per.Contains(peril) && per.Contains("MAIN CLAIM"))
                {
                    ClAmt = amt;
                }
            }
            return ClAmt;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdUpload = new OpenFileDialog();
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            ofdUpload.Multiselect = true;
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvFile.Rows.Count; i++)
                    {
                        if (dgvFile.Rows[i].Cells[0].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }

                    dgvFile.Rows.Add(filename, path);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();

        }

        private void OpenFile()
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No file to open!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                System.Diagnostics.Process.Start(dgvr.Cells[1].Value.ToString());

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No record to remove!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                dgvFile.Rows.Remove(dgvr);
        }

        private void dgvFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            OpenFile();
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

        }

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvFile.Rows.Add(Path.GetFileName(file), file);

        }

        private void btnGetDCLetter_Click(object sender, EventArgs e)
        {
            if (fdbSelectPath.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fdbSelectPath.SelectedPath))
            {
                Cursor.Current = Cursors.WaitCursor;

                string reminder = "";
                string[] reminddate = null;
                Attachment DCLetter = getMailAttachment(reminder, reminddate);
                string LetterName = "DC Letter - " + tbRiskName.Text.Replace("\n", " ") + " - " + Convert.ToDateTime(tbDateofLoss.Text).ToString("dd MMMM yyyy");

                string FilePath = fdbSelectPath.SelectedPath + @"\\" + LetterName + DateTime.Now.ToString(" yyyy_MM_dd_HH_mm_ss") + ".pdf";

                using (var fileStream = File.Create(FilePath))
                {
                    DCLetter.ContentStream.Seek(0, SeekOrigin.Begin);
                    DCLetter.ContentStream.CopyTo(fileStream);
                }

                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show("PDF exported!");

                System.Diagnostics.Process.Start(FilePath); //open file
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string clno = tbClaimNo.Text.ToUpper().Trim(),remark = tbNote.Text.Trim();
            if(clno == "")
            {
                Msgbox.Show("Please input Claim No");
                return;
            }
            
            DialogResult res = Msgbox.Show("Are you sure you want to note \""+((remark=="")?"Nothing":remark)+"\" for "+clno+"?", "Confirmation");
            if (res == System.Windows.Forms.DialogResult.No)
                return;

            DataTable dtTemp = crud.ExecQuery("SELECT * FROM USER_LETTER_HIST WHERE REF_NO = '" + clno + "' AND LETTER_TYPE = 'AUTO DEDUCTIBLE'");
            if (dtTemp.Rows.Count > 0) //if already have Remark
            {
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand();
                cmd.CommandText = "UPDATE USER_LETTER_HIST SET NOTE = :remark WHERE REF_NO = '" + clno + "' AND LETTER_TYPE = 'AUTO DEDUCTIBLE'";
                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("remark", remark));
                crud.ExecNonQuery(cmd);
            }
            else //if no Remark => add new
            {
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand();
                cmd.CommandText = "INSERT INTO USER_LETTER_HIST (LETTER_TYPE,REF_NO,NOTE) VALUES (:lettertype,:clno,:remark)";
                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("lettertype", "AUTO DEDUCTIBLE"));
                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("clno", clno));
                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("remark", remark));
                crud.ExecNonQuery(cmd);
            }

            Msgbox.Show(clno + "'s remark has been updated.");
        }
    }
}
