using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HWND = System.IntPtr;
using System.Text;


namespace Testing
{
    static class Program
    {
        #region --- OLD CODING ---
        //[DllImport("User32.dll")]
        //static extern int SetForegroundWindow(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        //static Int32 WM_SYSCOMMAND = 0x0112;
        //static Int32 SC_RESTORE = 0xF120;
        #endregion

        #region --- VARIABLE DECLARATION ---
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int SW_MAXIMIZE = 9; //Command to restore the window

        internal static readonly IntPtr InvalidHandleValue = IntPtr.Zero;

        [DllImportAttribute("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void ShowToFront(string windowName)
        {
            IntPtr firstInstance = FindWindow(null, windowName);
            ShowWindow(firstInstance, SW_MAXIMIZE);
            SetForegroundWindow(firstInstance);
        }

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);
        #endregion

        //static frmLogIn frmLogInObj;
        //static SplashScreen frmSplashScreen;

        /// <summary>D:\MIS Projest\Testing\Program.cs
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool onlyInstance;
            var appName = Application.ProductName;
            Mutex mutex = new Mutex(true, appName, out onlyInstance);
            if (!onlyInstance)
            {
                var formTitle = GetOpenWindows().Where(f => f.Contains("\u00b6" + "Additional Tools" + "\u00b6")).FirstOrDefault();
                ShowToFront(formTitle);
                return;
            }
            //StartApp();
            GC.KeepAlive(mutex);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogIn());

            #region --- OLD CODING ---
            //bool createdNew = true;
            //using (Mutex mutex = new Mutex(true, "Testing", out createdNew))
            //{
            //    if (createdNew)
            //    {
            //        StartApp();
            //    }
            //    else
            //    {

            //        var proc = Process.GetProcessesByName("Testing").FirstOrDefault();

            //        if (proc != null)
            //        {
            //            var pointer = proc.MainWindowHandle;

            //            SetForegroundWindow(pointer);
            //            SendMessage(pointer, WM_SYSCOMMAND, SC_RESTORE, 0);
            //        }

            //        //System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
            //        //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
            //        //{
            //        //    if (process.Id != current.Id)
            //        //    {
            //        //        SetForegroundWindow(process.MainWindowHandle);
            //        //        break;
            //        //    }
            //        //}

            //        //if (System.Diagnostics.Process.GetProcessesByName("Testing").Count() == 2)
            //        //{
            //        //    StartApp();
            //        //}
            //    }
            //}
            #endregion

        }

        public static List<string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            List<string> windows = new List<string>();

            EnumWindows(delegate(HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows.Add(builder.ToString());
                return true;

            }, 0);

            return windows;
        }



        //private static void frmLogIn_LoadCompleted(object sender, EventArgs e)
        //{
        //    if (frmSplashScreen == null || frmSplashScreen.Disposing || frmSplashScreen.IsDisposed)
        //        return;
        //    frmSplashScreen.Invoke(new Action(() => { frmSplashScreen.Close(); }));
        //    frmSplashScreen.Dispose();
        //    frmSplashScreen = null;
        //    frmLogInObj.TopMost = true;
        //    frmLogInObj.Activate();
        //    //frmLogInObj.TopMost = false;
        //}

        //private static void StartApp()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    frmSplashScreen = new SplashScreen();
        //    if (frmSplashScreen != null)
        //    {
        //        Thread splashThread = new Thread(new ThreadStart(
        //            () => { Application.Run(frmSplashScreen); }));
        //        splashThread.SetApartmentState(ApartmentState.STA);
        //        splashThread.Start();
        //    }
        //    //Create and Show Main Form
        //    frmLogInObj = new frmLogIn();
        //    frmLogInObj.LoadCompleted += frmLogIn_LoadCompleted;
        //    Application.Run(frmLogInObj);
        //    if (!(frmSplashScreen == null || frmSplashScreen.Disposing || frmSplashScreen.IsDisposed))
        //        frmSplashScreen.Invoke(new Action(() =>
        //        {
        //            frmSplashScreen.TopMost = true;
        //            frmSplashScreen.Activate();
        //            frmSplashScreen.TopMost = false;
        //        }));
        //}
    }
}
