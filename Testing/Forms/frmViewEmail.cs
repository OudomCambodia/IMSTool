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
    public partial class frmViewEmail : Form
    {
        public frmViewEmail()
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
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("Html/EmailContent.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", resetcontent);
            this.webBrowserTrick.DocumentText = body;
            //End of Update
        }

        private void bnEdit_Click(object sender, EventArgs e)
        {
            //Update 16-Jul-19 (Edit Email Content)
            rtbEditor.Text = "";
            webBrowserTrick.Document.ExecCommand("SelectAll", false, null);
            webBrowserTrick.Document.ExecCommand("Copy", false, null);
            rtbEditor.Paste();    //Copy all text from webBrowserTrick(Visible = false) 
            //End of Update
        }

        public void requeryWebBrowser(string content) //Update 16-Jul-19 (Edit Email Content)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("Html/EmailContent.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", content);
            this.webBrowserTrick.DocumentText = body; //refresh webBrowserTrick(Visible = false)

            body = string.Empty;
            using (StreamReader reader = new StreamReader("Html/2020Email.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", content);
            body = body.Replace("{department}", (type == "A&H") ? "A&H Claims Unit | Underwriting Department" : "Claims Department");
            body = body.Replace("{username}", finalizeusername);
            body = body.Replace("{user_email}", finalizemailadd);
            //body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Forte_Logo.png");
            //body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\FB_logo.png");
            body = body.Replace("cid:Forte_Logo", Application.StartupPath + @"\Html\Standard_Forte.png");
            body = body.Replace("cid:FB_logo", Application.StartupPath + @"\Html\fb.png");
            body = body.Replace("cid:YT_logo", Application.StartupPath + @"\Html\yt.png");
            body = body.Replace("cid:Mail_logo", Application.StartupPath + @"\Html\mail.png");
            this.wbEmail.DocumentText = body; // refresh wbEmail
        }

        private void bnBold_Click(object sender, EventArgs e)
        {
            if (rtbEditor.Text == "") return;
            RichTextBoxExtension.ToggleBold(rtbEditor);
        }
        
        private void bnUnderline_Click(object sender, EventArgs e)
        {
            if (rtbEditor.Text == "") return;
            RichTextBoxExtension.ToggleUnderline(rtbEditor);
        }

        private void bnItalic_Click(object sender, EventArgs e)
        {
            if (rtbEditor.Text == "") return;
            RichTextBoxExtension.ToggleItalic(rtbEditor);
        }      

        private void bnSave_Click(object sender, EventArgs e)
        {
            //Update 16-Jul-19 (Edit Email Content)
            if (rtbEditor.Text == "")
            {
                Msgbox.Show("There nothing in Editor to save.");
                return;
            }
            DialogResult res = Msgbox.Show("Do you want to make change on this email content?", "Confirmation");
            if (res == System.Windows.Forms.DialogResult.No)
                return;
            Array.Resize(ref rtbTextDetail, 1);       //reset size of array rtbTextDetail
            htmlscript = string.Empty;  //reset htmlscript
            RichTextBoxExtension.generateFormatCode(rtbEditor, ref rtbTextDetail);
            htmlscript = RichTextBoxExtension.groupFormatCode(ref rtbTextDetail);
            htmlscript = "<font face='Calibri',size=3>\n" + htmlscript + "</font>"; //assign font
            if(type == "A&H") frmSendEmailClaimDet.finalizecontent= htmlscript;
            else if (type == "AUTO") frmAutoLetters.finalizecontent = htmlscript;
            requeryWebBrowser(htmlscript);
            rtbEditor.Text = "";
            //End of Update
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnReset_Click(object sender, EventArgs e)
        {
            requeryWebBrowser(resetcontent);
            if (type == "A&H") frmSendEmailClaimDet.finalizecontent = resetcontent;
            else if (type == "AUTO") frmAutoLetters.finalizecontent = htmlscript;
            rtbEditor.Text = "";
        }
    }
}
