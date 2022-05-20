using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Testing
{

    
    static class Program
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);


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
                    System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                    foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }

                    if (System.Diagnostics.Process.GetProcessesByName("Testing").Count() == 2)
                    {
                        StartApp();
                    }
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
