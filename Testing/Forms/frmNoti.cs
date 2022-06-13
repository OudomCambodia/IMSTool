using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertUserRoleIntoNewTable
{
    public partial class frmNoti : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public frmNoti()
        {
            InitializeComponent();
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            TopLevel = true;
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
    }
}
