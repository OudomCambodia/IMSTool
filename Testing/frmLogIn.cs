using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Testing
{
    public partial class frmLogIn : Form
    {
        CRUD crud = new CRUD();
        public static string Usert = "";
        string HashPass = "Forte@2017";
        bool Maint = false;
        public string fullusername;

        //SplashScreen SplashScreen = new SplashScreen();
        //int minimumDisplayTime = 1000;

        public frmLogIn()
        {
            InitializeComponent();
            lbUsercode.ForeColor = Color.FromArgb(0, 38, 58);
            lblPassword.ForeColor = Color.FromArgb(0, 38, 58);
            tbUser.ForeColor = Color.FromArgb(0, 38, 58);
            tbPassword.ForeColor = Color.FromArgb(0, 38, 58);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                LogIn();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.StackTrace);
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    LogIn();
                }
                catch (Exception ex)
                {
                    Msgbox.Show(ex.StackTrace);
                }
            }
        }

        private void LogIn()
        {
        K: //Update 19-Jul-19 (Solve Connection Pool Problem)
            try
            {
                string sql = "SELECT password, expiry_date, user_name, type FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + tbUser.Text.Trim().ToUpper() + "'";

                DataTable dt = new DataTable();
                dt = crud.ExecQuery(sql);
                if (dt.Rows.Count == 0 || tbUser.Text.Trim().ToUpper() == "MAINT" || tbUser.Text.Trim().ToUpper() == "SICL")
                {
                    Msgbox.Show("You have input incorrect user code.");
                    return;
                }

                DataRow dr = dt.Rows[0];
                string password = dr[0].ToString();
                string username = dr[2].ToString();
                fullusername = username;
                DateTime expDate = (DateTime)dr[1];
               
                if (Cipher.Encrypt(tbPassword.Text, HashPass) != password)
                {
                    Msgbox.Show("You have input incorrect password");
                    return;
                }

                if (DateTime.Now > expDate)
                {
                    Msgbox.Show("Your account is expired. Please contact system admin.");
                    return;
                }

                #region --- USER ROLE MANAGEMENT OLD CODING ---
                //sql = "SELECT allow FROM USER_PRINT_TYPE WHERE TYPE = '" + dr[3].ToString() + "'";
                //string allow = crud.ExecQuery(sql).Rows[0].ItemArray[0].ToString();
                //string[] splitAllow = allow.Split(',').ToArray();
                //sql = "select * from USER_PRINT_ALLOW_REFERENCE Where REFERENCE_NO in (";
                //foreach (string eachAllow in splitAllow)
                //    sql += "'" + eachAllow + "',";
                //sql = sql.Remove(sql.Length - 1, 1);
                //sql += ")";
                //DataTable allowRef = crud.ExecQuery(sql);

                //frmMain fm = new frmMain();
                //fm.UserName = tbUser.Text.ToUpper();
                //fm.FullName = dr[2].ToString();
                //fm.frmLog = this;

                //foreach (DataRow drRef in allowRef.Rows)
                //{
                //    if (fm.Controls.Find(drRef[2].ToString(), true).Length > 0)
                //    {
                //        ((Button)fm.Controls.Find(drRef[2].ToString(), true)[0]).Enabled = true;
                //        continue;
                //    }

                //    if (fm.menuStrip1.Items.Find(drRef[2].ToString(), true).Length > 0)
                //    {
                //        ((ToolStripMenuItem)fm.menuStrip1.Items.Find(drRef[2].ToString(), true)[0]).Enabled = true;
                //    }
                //}
                //Usert = tbUser.Text;
                //fm.Show();
                //this.Hide();
                #endregion

                #region --- USER ROLE MANAGEMENT NEW CODING ---
                var userCode = tbUser.Text.Trim().ToUpper();
                var userControlAccess = crud.ExecQuery("select upc.CONTROL_NAME, uca.VISIBLE, uca.ENABLED from USER_PRINT_CONTROL_ACCESS uca inner join USER_PRINT_CONTROL upc on upc.CONTROL_ID = uca.CONTROL_ID where uca.CODE = (select TYPE from USER_PRINT_SYSTEM where USER_CODE = '" + userCode + "')");

                frmMain fm = new frmMain();
                fm.UserName = tbUser.Text.ToUpper();
                fm.FullName = dr[2].ToString();
                fm.frmLog = this;

                if (userControlAccess.Rows.Count > 0)
                {
                    for (int i = 0; i < userControlAccess.Rows.Count; i++)
                    {
                        var controlName = userControlAccess.Rows[i]["CONTROL_NAME"].ToString();
                        var isEnabled = userControlAccess.Rows[i]["VISIBLE"].ToString().Equals("Y") && userControlAccess.Rows[i]["ENABLED"].ToString().Equals("Y");
                        if (fm.Controls.Find(controlName, true).Length > 0)
                        {
                            ((Button)fm.Controls.Find(controlName, true)[0]).Enabled = isEnabled;
                            continue;
                        }
                        if (fm.menuStrip1.Items.Find(controlName, true).Length > 0)
                        {
                            ((ToolStripMenuItem)fm.menuStrip1.Items.Find(controlName, true)[0]).Enabled = isEnabled;
                        }
                    }
                }

                Usert = tbUser.Text;
                fm.Show();
                this.Hide();
                #endregion
            }
            catch (Exception e)
            {
                Task.Delay(1000).Wait();
                goto K;
            }
        }

        //Update 19-Jul-19 (Splash Screen)
        public event EventHandler LoadCompleted;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnLoadCompleted(EventArgs.Empty);
        }
        protected virtual void OnLoadCompleted(EventArgs e)
        {
            var handler = LoadCompleted;
            if (handler != null)
                handler(this, e);
        }
        //End of Update

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadSplashScreen();
                Cursor.Current = Cursors.WaitCursor;
                CheckMaint();
                //Thread.Sleep(3000);
                tbUser.Focus();
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void tmCheckMaint_Tick(object sender, EventArgs e)
        {
            CheckMaint();
        }

        //private void CheckMaint()
        //{

        //    string sql = "select PASSWORD from USER_PRINT_SYSTEM where USER_CODE = 'MAINT'";
        //    if (crud.ExecQuery(sql).Rows.Count == 0) return;
        //    string maint = crud.ExecQuery(sql).Rows[0][0].ToString();
        //    if (maint.Trim().ToUpper() == "ON")
        //    {
        //        tmCheckMaint.Stop();
        //        tmClose.Start();
        //        Msgbox.Show("The system is under maintenance mode. It will be closed. Please wait or contact system admin.");
        //        Application.Exit();
        //    }
        //}

        private void CheckMaint()
        {
            //if (Maintenance.Check())
            //{
            //    tmClose.Start();
            //    Msgbox.Show("The system is under maintenance mode. It will be closed. Please wait or contact system admin.");
            //    Environment.Exit(0);
            //}

        K: //Update 19-Jul-19 (Solve Connection Pool Problem)
            try
            {
                checkMaintCon();
            }
            catch (Exception e)
            {
                Task.Delay(1).Wait();
                goto K;
                //checkMaintCon();
            }
        }

        private void tmClose_Tick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkMaintCon()
        {
            //string res = crud.ExecFunc_String("CHECK_MAINT", new string[] { "claim_no" }, new string[] { "" });
            //if (res == "OFF")
            //{
            //    Maint = false;
            //    return;
            //}
            //if (res == "ON")
            //    Maint = true;

            if (Maintenance.Check())
            {
                Maint = true;
            }
            else
                Maint = false;
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbPassword.Focus();
        }


        public void showMaint()
        {
            if (Maint)
            {
                tmCheckMaint.Stop();
                tmClose.Start();
                Msgbox.Show("The system is under maintenance mode. It will be closed. Please wait or contact system admin.");
                Application.Exit();
            }
        }

        private void frmLogIn_Shown(object sender, EventArgs e)
        {
            showMaint();
        }

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void frmLogIn_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        //private void LoadSplashScreen()
        //{
        //    SplashScreen.Show();
        //    DateTime startTime = DateTime.Now;
        //    int cnt = 0;

        //    do
        //    {
        //        Thread.Sleep(100);
        //        cnt++;
        //        Application.DoEvents();

        //    } while (cnt < 10);

        //    int timeDisplayed = (DateTime.Now - startTime).Milliseconds;

        //    if (timeDisplayed < minimumDisplayTime)
        //    {
        //        do
        //        {
        //            Thread.Sleep(100);
        //            Application.DoEvents();
        //            timeDisplayed += 100;

        //        } while (timeDisplayed < minimumDisplayTime);
        //    }
        //    SplashScreen.Close();
        //}
    }
}