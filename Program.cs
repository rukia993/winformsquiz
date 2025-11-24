using System;
using System.Windows.Forms;

namespace WinFormsQuizApp
{
    internal static class Program
    {
        /// <summary>
        ///     Entry point of the Windows Forms application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}



