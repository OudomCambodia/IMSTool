using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class Msgbox : Form
    {
        static Msgbox newMessageBox;
        static DialogResult Button_id;
        static bool isConfirmation = true; //true means has 2 buttons (Yes/No), false means has 1 button (Close)
        bool release = true;
        int xOffset = 0;
        int yOffset = 0;

        public Msgbox()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string txtMessage)
        {
            newMessageBox = new Msgbox();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblTitle.Text = "Information";
            newMessageBox.btnOk.Visible = false;
            newMessageBox.btnCancel.Text = "Close";
            isConfirmation = false;
            newMessageBox.TopMost = true;
            newMessageBox.ShowDialog();
            //newMessageBox.AcceptButton = newMessageBox.btnCancel;
            //newMessageBox.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            //newMessageBox.btnCancel.Focus();
            return Button_id;
        }

        public static DialogResult Show(string txtMessage, Color col)
        {
            newMessageBox = new Msgbox();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblTitle.Text = "Information";
            newMessageBox.lblMessage.ForeColor = col;
            newMessageBox.btnOk.Visible = false;
            newMessageBox.btnCancel.Text = "Close";
            isConfirmation = false;
            newMessageBox.TopMost = true;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        public static DialogResult Show(string txtMessage, string txtTitle)
        {
            newMessageBox = new Msgbox();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.btnOk.Visible = true;
            newMessageBox.btnCancel.Visible = true;
            isConfirmation = true;
            newMessageBox.TopMost = true;
            newMessageBox.ShowDialog();
            //newMessageBox.AcceptButton = newMessageBox.btnOk;
            //newMessageBox.btnOk.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //newMessageBox.CancelButton = newMessageBox.btnCancel;
            //newMessageBox.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            //newMessageBox.btnOk.Focus();
            return Button_id;
        }

        public static DialogResult Show(string txtMessage, string txtTitle, Color col)
        {
            newMessageBox = new Msgbox();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblMessage.ForeColor = col;
            newMessageBox.btnOk.Visible = true;
            newMessageBox.btnCancel.Visible = true;
            isConfirmation = true;
            newMessageBox.TopMost = true;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        public static DialogResult Show(string txtMessage, string txtTitle, string OkTxt, string CancelTxt)
        {
            newMessageBox = new Msgbox();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.btnOk.Visible = true;
            newMessageBox.btnOk.Text = OkTxt;
            newMessageBox.btnCancel.Visible = true;
            newMessageBox.btnCancel.Text = CancelTxt;
            isConfirmation = true;
            newMessageBox.TopMost = true;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Button_id = DialogResult.Yes;
            newMessageBox.Dispose();
            base.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Button_id = DialogResult.No;
            newMessageBox.Dispose();
            base.Close();
        }             

        private void button1_Click(object sender, EventArgs e)
        {
            Button_id = DialogResult.No;
            newMessageBox.Dispose();
            base.Close();
        }

        private void pnTop_MouseDown(object sender, MouseEventArgs e)
        {
            release = false;

            xOffset = MousePosition.X - this.Location.X;
            yOffset = MousePosition.Y - this.Location.Y;
        }

        private void pnTop_MouseUp(object sender, MouseEventArgs e)
        {
            release = true;
        }

        private void pnTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (release == false)
            {
                this.Location = new Point(Cursor.Position.X - xOffset, Cursor.Position.Y - yOffset);
                this.Update();
            }
        }

        private void pBackground_SizeChanged(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width, pnTop.Height + pBackground.Height + btnOk.Height + 10);
        }

        private void Msgbox_Load(object sender, EventArgs e)
        {

        }

        private void Msgbox_Activated(object sender, EventArgs e)
        {
            if (isConfirmation)
            {
                this.AcceptButton = this.btnOk;
                this.AcceptButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.CancelButton = this.btnCancel;
                this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.No;
                this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
                btnOk.Focus();
            }
            else 
            {
                this.AcceptButton = this.btnCancel;
                this.AcceptButton.DialogResult = System.Windows.Forms.DialogResult.No;
                this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
                this.CancelButton = this.btnCancel;
                this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.No;
                this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
                btnCancel.Focus();
            }
        } 
    }
}
