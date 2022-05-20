using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Testing.Forms
{
    public partial class frmEditEmailContent : Form
    {
        public frmEditEmailContent(string finalizecontent)
        {
            InitializeComponent();
            content = finalizecontent;
            oldcontent = finalizecontent;
        }

        string content = "", oldcontent ="";
        public static bool firsttime = true;

        private void frmEditEmailContent_Load(object sender, EventArgs e)
        {
            string temp = HtmlToPlainText(content);
            tbEmailContent.Text = temp; 
        }



        private static string HtmlToPlainText(string html)
        {
            const string boldopentag = @"<(b)\s{0,1}\/{0,1}>";
            const string boldclosetag = @"<(/b)\s{0,1}\/{0,1}>";
            const string underlineopentag = @"<(u)\s{0,1}\/{0,1}>";
            const string underlineclosetag = @"<(/u)\s{0,1}\/{0,1}>";
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var boldopentagRegex = new Regex(boldopentag, RegexOptions.Multiline);
            var boldclosetagRegex = new Regex(boldclosetag, RegexOptions.Multiline);
            var underlineopentagRegex = new Regex(underlineopentag, RegexOptions.Multiline);
            var underlineclosetagRegex = new Regex(underlineclosetag, RegexOptions.Multiline);

            var text = html;

            text = System.Net.WebUtility.HtmlDecode(text);
            text = boldopentagRegex.Replace(text, "<Bold>"); text = boldclosetagRegex.Replace(text, "</Bold>");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = underlineopentagRegex.Replace(text, "<Underline>"); text = underlineclosetagRegex.Replace(text, "</Underline>");
            return text;
        }

        private static string PlainTextToHtml(string ptext)
        {
            const string boldopentag = @"<(Bold)\s{0,1}\/{0,1}>";
            const string underlineopentag = @"<(Underline)\s{0,1}\/{0,1}>";
            const string boldclosetag = @"<(/Bold)\s{0,1}\/{0,1}>";
            const string underlineclosetag = @"<(/Underline)\s{0,1}\/{0,1}>";
            // const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex("\n", RegexOptions.Multiline);
            var boldopentagRegex = new Regex(boldopentag, RegexOptions.Multiline);
            var underlineopentagRegex = new Regex(underlineopentag, RegexOptions.Multiline);
            var boldclosetagRegex = new Regex(boldclosetag, RegexOptions.Multiline);
            var underlineclosetagRegex = new Regex(underlineclosetag, RegexOptions.Multiline);
            var text = ptext;
            text = System.Net.WebUtility.HtmlDecode(text);
            text = boldopentagRegex.Replace(text, "<b>");
            text = lineBreakRegex.Replace(text, "<br/>");
            text = underlineopentagRegex.Replace(text, "<u>");
            text = boldclosetagRegex.Replace(text, "</b>");
            text = underlineclosetagRegex.Replace(text, "</u>");
            return text;
        }

        //public string replacedoublebrtag(string ptext )
        //{
        //    string temp = ptext;
        //    var doublelineBreakRegex = new Regex("<br/><br/>", RegexOptions.Multiline);
        //    temp = doublelineBreakRegex.Replace(temp, "<br/>");
        //    return temp;
        //}

        private void bnDone_Click(object sender, EventArgs e)
        {
            frmSendEmailClaimDet.finalizecontent = PlainTextToHtml(tbEmailContent.Text);
            this.Close();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            frmSendEmailClaimDet.finalizecontent = oldcontent;
            this.Close();
        }
    }
}
