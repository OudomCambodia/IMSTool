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
    public partial class InputBox : Form
    {
        static InputBox newInputBox = new InputBox();
        static bool checkOK;
        static string temp;
        bool release = true;
        int xOffset = 0;
        int yOffset = 0;

        public InputBox()
        {
            InitializeComponent();
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




        public static string Show(string detail, string title)
        {
            newInputBox.lblTitle.Text = title;
            newInputBox.lblMessage.Text = detail;
            newInputBox.ShowDialog();
            newInputBox.tbInput.Focus();
            if (checkOK == true)
                temp = newInputBox.tbInput.Text;
            else if (checkOK == false)
                temp = "";
            return temp;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            checkOK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            checkOK = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnCancel.PerformClick();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            tbInput.Focus();
        }

        private void InputBox_Activated(object sender, EventArgs e)
        {
            tbInput.Focus();
            lblMessage.MaximumSize = new Size(450,110);
        }
    }
}
