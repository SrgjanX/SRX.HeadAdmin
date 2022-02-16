//srgjanx

using SRX.HeadAdmin.Forms;
using SRX.HeadAdmin.Utils;
using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin
{
    static class Program
    {
        public static BanMethod banMethod;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}