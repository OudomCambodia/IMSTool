using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Testing.Forms
{
    public partial class frmNotification : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public frmNotification()
        {
            InitializeComponent(); 
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        protected override void OnLoad(EventArgs e)
        {
            PlaceLowerRight();
            base.OnLoad(e);
        }

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
                //rightmost = Screen.AllScreens.OrderBy(s => s.WorkingArea.Right).Last();
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

        private void btnCloseNoti3_Click(object sender, EventArgs e)
        {
            foreach (Control control in flpNoti.Controls)
            {
                if (control.Name == "pnNoti3")
                {
                    flpNoti.Controls.Remove(control);
                    control.Dispose();
                }
            }
            SetFormSize();
        }

        private void btnCloseNoti2_Click(object sender, EventArgs e)
        {
            foreach (Control control in flpNoti.Controls)
            {
                if (control.Name == "pnNoti2")
                {
                    flpNoti.Controls.Remove(control);
                    control.Dispose();
                }
            }
            SetFormSize();
        }

        private void btnCloseNoti1_Click(object sender, EventArgs e)
        {
            foreach (Control control in flpNoti.Controls)
            {
                if (control.Name == "pnNoti1")
                {
                    flpNoti.Controls.Remove(control);
                    control.Dispose();
                }
            }
            SetFormSize();
        }

        private void SetFormSize()
        {
            int controlCount = flpNoti.Controls.Count;
            if (controlCount == 2)
                Size = new Size(515, 156);
            else if (controlCount == 1)
                Size = new Size(515, 79);
            else
                Close();

            PlaceLowerRight();
        }

        private void frmNotification_Load(object sender, EventArgs e)
        {
            List<string> notiInfo = frmMain.NotiInfo;

            if (notiInfo.Count <= 0)
                return;

            if (notiInfo.Count == 3)
            {
                string[] notiAndDate1 = notiInfo[0].Split('*');
                string[] notiAndDate2 = notiInfo[1].Split('*');
                string[] notiAndDate3 = notiInfo[2].Split('*');

                lblNoti1.Text = notiAndDate1[0].ToString();
                lblNoti1Date.Text = Convert.ToDateTime(notiAndDate1[1]).ToString("dd-MM-yyyy hh:mm tt");

                lblNoti2.Text = notiAndDate2[0].ToString();
                lblNoti2Date.Text = Convert.ToDateTime(notiAndDate2[1]).ToString("dd-MM-yyyy hh:mm tt");

                lblNoti3.Text = notiAndDate3[0].ToString();
                lblNoti3Date.Text = Convert.ToDateTime(notiAndDate3[1]).ToString("dd-MM-yyyy hh:mm tt");
            }
            else if (notiInfo.Count == 2)
            {
                string[] notiAndDate1 = notiInfo[0].Split('*');
                string[] notiAndDate2 = notiInfo[1].Split('*');

                lblNoti1.Text = notiAndDate1[0].ToString();
                lblNoti1Date.Text = Convert.ToDateTime(notiAndDate1[1]).ToString("dd-MM-yyyy hh:mm tt");

                lblNoti2.Text = notiAndDate2[0].ToString();
                lblNoti2Date.Text = Convert.ToDateTime(notiAndDate2[1]).ToString("dd-MM-yyyy hh:mm tt");

                flpNoti.Controls.Remove(pnNoti3);
                Size = new Size(515, 156);
            }
            else
            {
                string[] notiAndDate1 = notiInfo[0].Split('*');

                lblNoti1.Text = notiAndDate1[0].ToString();
                lblNoti1Date.Text = Convert.ToDateTime(notiAndDate1[1]).ToString("dd-MM-yyyy hh:mm tt");

                flpNoti.Controls.Remove(pnNoti2);
                flpNoti.Controls.Remove(pnNoti3);
                Size = new Size(515, 79);
            }
            PlaceLowerRight();
        }
    }
}
