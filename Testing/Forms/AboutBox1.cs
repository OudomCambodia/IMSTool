using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                //object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                //if (attributes.Length == 0)
                //{
                //    return "";
                //}
                return "Additional Tools"; //((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            labelCompanyName.Text = "FORTE INSURANCE COMPANY";
            textBoxDescription.Rtf = "{\\rtf1\\ansi\\deff0\\adeflang1025{\\fonttbl{\\f0\\froman\\fprq2\\fcharset0 Times New Roman;}{\\f1\\froman\\fprq2\\fcharset2 Symbol;}{\\f2\\fswiss\\fprq2\\fcharset0 Arial;}{\\f3\\fswiss\\fprq2\\fcharset0 Calibri;}{\\f4\\fnil\\fprq2\\fcharset0 Roboto;}{\\f5\\fnil\\fprq2\\fcharset0 Microsoft YaHei;}{\\f6\\fscript\\fprq1\\fcharset134 DengXian{\\*\\falt ??};}{\\f7\\fnil\\fprq2\\fcharset0 Lucida Sans;}{\\f8\\fswiss\\fprq0\\fcharset128 Lucida Sans;}}{\\colortbl;\\red0\\green0\\blue0;\\red255\\green255\\blue255;\\red128\\green128\\blue128;}{\\stylesheet{\\s0\\snext0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033 Default;}{\\*\\cs15\\snext15 Default Paragraph Font;}{\\s16\\sbasedon0\\snext17\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb240\\sa120\\keepn\\hich\\af5\\langfe2052\\dbch\\af7\\afs28\\lang1107\\loch\\f2\\fs28\\lang1033 Heading;}{\\s17\\sbasedon0\\snext17\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa120\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\loch\\f3\\fs22\\lang1033 Text body;}{\\s18\\sbasedon17\\snext18\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa120\\hich\\af6\\langfe2052\\dbch\\af8\\afs22\\lang1107\\loch\\f3\\fs22\\lang1033 List;}{\\s19\\sbasedon0\\snext19\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb120\\sa120\\noline\\i\\hich\\af6\\langfe2052\\dbch\\af8\\afs24\\lang1107\\ai\\loch\\f3\\fs24\\lang1033 Caption;}{\\s20\\sbasedon0\\snext20\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\noline\\hich\\af6\\langfe2052\\dbch\\af8\\afs22\\lang1107\\loch\\f3\\fs22\\lang1033 Index;}}{\\info{\\author Pen Pichponleur}{\\creatim\\yr2020\\mo6\\dy11\\hr9\\min30}{\\author Pen Pichponleur}{\\revtim\\yr2020\\mo6\\dy11\\hr9\\min31}{\\printim\\yr0\\mo0\\dy0\\hr0\\min0}{\\comment OpenOffice}{\\vern4160}}\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720\\deftab720{\\*\\pgdsctbl{\\pgdsc0\\pgdscuse195\\pgwsxn12240\\pghsxn15840\\marglsxn1440\\margrsxn1440\\margtsxn1440\\margbsxn1440\\pgdscnxt0 Default;}}\\formshade{\\*\\pgdscno0}\\paperh15840\\paperw12240\\margl1440\\margr1440\\margt1440\\margb1440\\sectd\\sbknone\\sectunlocked1\\pgndec\\pgwsxn12240\\pghsxn15840\\marglsxn1440\\margrsxn1440\\margtsxn1440\\margbsxn1440\\ftnbj\\ftnstart1\\ftnrstcont\\ftnnar\\aenddoc\\aftnrstcont\\aftnstart1\\aftnnrlc\\pgndec\\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\b\\dbch\\af4\\ab\\rtlch \\ltrch\\loch\\loch\\f4Description}\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\hich\\af4\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Risk Searching}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: find transactions (added, deleted or amended) on searched risks. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Claim Checking}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: check history of claim by claimant, claim no, policy no. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Customer}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Profitability}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Report}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: check how much premium and claim by customer. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4File}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Management}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: store files or documents related to policy no. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Submit}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Card}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Printing}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: send card information to IT for printing. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4List}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4of}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Policy}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Current}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Members}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: view the list of all current members in a policy. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Print}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Invoice}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: generate invoice using policy no with debit note and a specific bank. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4- }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Sending}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Claim}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4 }{\\cf2\\b\\dbch\\af4\\afs17\\ab\\rtlch \\ltrch\\loch\\fs17\\loch\\f4Emails}{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4: send all type of claim mails to client, view mail history and pending email. }\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sl200\\slmult0\\sb0\\sa0\\ltrpar{\\cf2\\hich\\af4\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4}\\par \\pard\\plain \\s0\\sl256\\slmult1\\ql\\aspalpha\\faauto\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\nowidctlpar{\\*\\hyphen2\\hyphlead2\\hyphtrail2\\hyphmax0}\\hich\\af6\\langfe2052\\dbch\\af3\\afs22\\lang1107\\cf0\\loch\\f3\\fs22\\lang1033\\li0\\ri0\\lin0\\rin0\\fi0\\sb0\\sa160\\ltrpar{\\cf2\\dbch\\af4\\afs17\\rtlch \\ltrch\\loch\\fs17\\loch\\f4***This Tools is developed by IMS Team***}\\par }";
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
