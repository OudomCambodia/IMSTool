using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Testing
{

    
    static class Program
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        static Int32 WM_SYSCOMMAND = 0x0112;
        static Int32 SC_RESTORE = 0xF120;


        static frmLogIn frmLogInObj;
        static SplashScreen frmSplashScreen;
        /// <summary>D:\MIS Projest\Testing\Program.cs
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "Testing", out createdNew))
            {
                if (createdNew)
                {
                    StartApp();
                }
                else
                {

                    var proc = Process.GetProcessesByName("Testing").FirstOrDefault();

                    if (proc != null)
                    {
                        var pointer = proc.MainWindowHandle;

                        SetForegroundWindow(pointer);
                        SendMessage(pointer, WM_SYSCOMMAND, SC_RESTORE, 0);
                    }

                    //System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                    //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
                    //{
                    //    if (process.Id != current.Id)
                    //    {
                    //        SetForegroundWindow(process.MainWindowHandle);
                    //        break;
                    //    }
                    //}

                    //if (System.Diagnostics.Process.GetProcessesByName("Testing").Count() == 2)
                    //{
                    //    StartApp();
                    //}
                }
            }




        }

        private static void frmLogIn_LoadCompleted(object sender, EventArgs e)
        {
            if (frmSplashScreen == null || frmSplashScreen.Disposing || frmSplashScreen.IsDisposed)
                return;
            frmSplashScreen.Invoke(new Action(() => { frmSplashScreen.Close(); }));
            frmSplashScreen.Dispose();
            frmSplashScreen = null;
            frmLogInObj.TopMost = true;
            frmLogInObj.Activate();
            //frmLogInObj.TopMost = false;
        }

        private static void StartApp()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmSplashScreen = new SplashScreen();
            if (frmSplashScreen != null)
            {
                Thread splashThread = new Thread(new ThreadStart(
                    () => { Application.Run(frmSplashScreen); }));
                splashThread.SetApartmentState(ApartmentState.STA);
                splashThread.Start();
            }
            //Create and Show Main Form
            frmLogInObj = new frmLogIn();
            frmLogInObj.LoadCompleted += frmLogIn_LoadCompleted;
            Application.Run(frmLogInObj);
            if (!(frmSplashScreen == null || frmSplashScreen.Disposing || frmSplashScreen.IsDisposed))
                frmSplashScreen.Invoke(new Action(() =>
                {
                    frmSplashScreen.TopMost = true;
                    frmSplashScreen.Activate();
                    frmSplashScreen.TopMost = false;
                }));
        }
    }
}
