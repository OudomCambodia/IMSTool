using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    static class RichTextBoxExtension //Create (19-Sep-19)--Ponleur
    {
        public static void ToggleBold(Control ctrl)
        {
            RichTextBox rtb = ctrl as RichTextBox;
            if (rtb.SelectionFont != null)
            {
                Font currentFont = rtb.SelectionFont;
                FontStyle newFontStyle = FontStyle.Regular;     //Set first style with Regular
                if (rtb.SelectionFont.Bold == true) //true when the text Bold so we will make it back to Regular
                {

                    if (rtb.SelectionFont.Italic) newFontStyle |= FontStyle.Italic; //add other existing style so if true => style = Regular + Italic
                    if (rtb.SelectionFont.Underline) newFontStyle |= FontStyle.Underline;
                    if (newFontStyle != rtb.SelectionFont.Style)
                    {
                        rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle); //set new style to selected text
                    }
                    return;
                }
                newFontStyle = FontStyle.Bold;
                if (rtb.SelectionFont.Italic) newFontStyle |= FontStyle.Italic;
                if (rtb.SelectionFont.Underline) newFontStyle |= FontStyle.Underline;
                if (newFontStyle != rtb.SelectionFont.Style)
                {
                    rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
        }

        public static void ToggleItalic(Control ctrl) 
        {
            RichTextBox rtb = ctrl as RichTextBox;
            if (rtb.SelectionFont != null)
            {
                Font currentFont = rtb.SelectionFont;
                FontStyle newFontStyle = FontStyle.Regular;
                if (rtb.SelectionFont.Italic == true)
                {
                    if (rtb.SelectionFont.Bold) newFontStyle |= FontStyle.Bold;
                    if (rtb.SelectionFont.Underline) newFontStyle |= FontStyle.Underline;
                    if (newFontStyle != rtb.SelectionFont.Style)
                    {
                        rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    }
                    return;
                }
                newFontStyle = FontStyle.Italic;
                if (rtb.SelectionFont.Bold) newFontStyle |= FontStyle.Bold;
                if (rtb.SelectionFont.Underline) newFontStyle |= FontStyle.Underline;
                if (newFontStyle != rtb.SelectionFont.Style)
                {
                    rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }

            }
        }

        public static void ToggleUnderline(Control ctrl) 
        {
            RichTextBox rtb = ctrl as RichTextBox;
            if (rtb.SelectionFont != null)
            {
                Font currentFont = rtb.SelectionFont;
                FontStyle newFontStyle = FontStyle.Regular;
                if (rtb.SelectionFont.Underline == true)
                {

                    if (rtb.SelectionFont.Bold) newFontStyle |= FontStyle.Bold;
                    if (rtb.SelectionFont.Italic) newFontStyle |= FontStyle.Italic;
                    if (newFontStyle != rtb.SelectionFont.Style)
                    {
                        rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                    }
                    return;
                }
                newFontStyle = FontStyle.Underline;
                if (rtb.SelectionFont.Bold) newFontStyle |= FontStyle.Bold;
                if (rtb.SelectionFont.Italic) newFontStyle |= FontStyle.Italic;
                if (newFontStyle != rtb.SelectionFont.Style)
                {
                    rtb.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
        }

        public static void generateFormatCode(Control ctrl,ref String[][] TextDetail)
        {
            RichTextBox rtb = ctrl as RichTextBox;
            char[] temp = rtb.Text.ToCharArray();  //get text in rtbEditor to char array temp
            for (int i = 0; i < temp.Length; i++)
            {
                TextDetail[i] = new string[2];  //initialize jagged array 
                TextDetail[i][0] = temp[i].ToString(); //assign char to index 0
                TextDetail[i][1] = getFormatCode(i, temp[i], ctrl); //assign format code to index 1
                if (TextDetail[i][1] == string.Empty)
                {
                    MessageBox.Show("Error in getting code");
                    return;
                }
                Array.Resize(ref TextDetail, TextDetail.Length + 1); //increase array rtbTextDetail size by 1
            }
            Array.Resize(ref TextDetail, TextDetail.Length - 1); //clear the empty element (last element of array rtbTextDetail)
        }

        private static string getFormatCode(int index, char c, Control ctrl)
        {
            RichTextBox rtb = ctrl as RichTextBox;
            string code = string.Empty;
            rtb.SelectionStart = index;
            rtb.SelectionLength = 1;
            if (rtb.SelectedText == c.ToString())
            {
                if (rtb.SelectionFont.Style == FontStyle.Regular)
                    code = code + '1';
                else code = code + '0';
                if (rtb.SelectionFont.Bold == true)
                    code = code + '1';
                else code = code + '0';
                if (rtb.SelectionFont.Italic == true)
                    code = code + '1';
                else code = code + '0';
                if (rtb.SelectionFont.Underline == true)
                    code = code + '1';
                else code = code + '0';
                return code;
            }
            else return "0000";
        }

        private static string generateHTMLscript(string subText, string code)
        {
            string bOTag = "<b>", bCTag = "</b>", uOTag = "<u>", uCTag = "</u>", iOTag = "<i>", iCTag = "</i>";
            if (subText != string.Empty)
            {
                if (code[1] == '1')
                    subText = bOTag + subText + bCTag;
                if (code[2] == '1')
                    subText = iOTag + subText + iCTag;
                if (code[3] == '1')
                    subText = uOTag + subText + uCTag;
                return subText;
            }

            else return string.Empty;
        }

        public static string groupFormatCode(ref string[][] TextDetail)
        {
            string htmlscript=string.Empty,subText = TextDetail[0][0], start = TextDetail[0][1];  //declare and assign the first char in rtbTextDetail
            if (TextDetail[0][0] != string.Empty)
            {
                for (int i = 1; i < TextDetail.Length; i++)
                {

                    if (TextDetail[i][1] == start)  //compare format code
                    {
                        subText = subText + TextDetail[i][0]; //connect char with the same format code
                        if (i == TextDetail.Length - 1)  //true if i is the last element of rtbTextDetail
                        {
                            htmlscript += generateHTMLscript(subText, start);  //generate script by add tag to the subtext which has the same format code
                        }
                    }
                    else
                    {
                        if (i == TextDetail.Length - 1) //true if i is the last element of rtbTextDetail
                        {
                            htmlscript += generateHTMLscript(subText, start); //the char that left in subtext
                            htmlscript += generateHTMLscript(TextDetail[i][0], TextDetail[i][1]); //last element of rtbTextDetail
                            continue;

                        }
                        htmlscript += generateHTMLscript(subText, start);
                        subText = TextDetail[i][0]; //replace old subtext (cuz we already add it to htmlscript)
                        start = TextDetail[i][1]; //assign new format code
                    }
                }
            }
            string newhtmlscript = string.Empty;
            for (int j = 0; j < htmlscript.Length; j++)
            {

                if (htmlscript[j] != '\n')
                    newhtmlscript += htmlscript[j];
                else
                    newhtmlscript = newhtmlscript + "<br/>"; //replace newline with br tag (won't work in Outlook if use other br tag such as: </br>....)
            }
            htmlscript = newhtmlscript;
            newhtmlscript = string.Empty;
            for (int j = 0; j < htmlscript.Length; j++)
            {

                if (htmlscript[j] != '\t')
                    newhtmlscript += htmlscript[j];
                else
                    newhtmlscript += "&emsp;"; //replace tab with &emsp (4 white spaces)
            }
            htmlscript = newhtmlscript;
            return htmlscript;
        }    
    }
}
