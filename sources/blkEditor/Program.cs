using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Threading;
using System.Windows.Forms;

namespace blkEditor
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{F6770DA4-652B-4D3F-B7BD-BE80BB26CD8A}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                #region SplashScreen
                new SplashScreenUsingVBFramework().Run(args);
                #endregion
                mutex.ReleaseMutex();
            }
        }
    }

    #region SplashScreen2
    class SplashScreenUsingVBFramework : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            base.OnCreateSplashScreen();
            this.SplashScreen = new frmSplash();
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();
            this.MainForm = new frmMain();
        }
    }
    #endregion
}
