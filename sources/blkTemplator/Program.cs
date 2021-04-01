using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blkTemplator
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{E3336F44-B693-478F-B553-9FE9D0BB1B06}");

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
                Application.Run(new frmMain());
                mutex.ReleaseMutex();
            }
        }
    }
}
