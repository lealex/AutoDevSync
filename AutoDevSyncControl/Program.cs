using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoDevSync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmAutoDevSync form = new FrmAutoDevSync(args);
            Application.Run(form);
            // release 1

            // release 2

        }
    }
}
