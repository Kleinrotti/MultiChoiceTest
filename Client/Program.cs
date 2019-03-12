using System;
using System.Windows.Forms;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var ExameSelection = new Forms.FormExamSelection();
            Application.Run(ExameSelection);

            if(ExameSelection.testStarted)
            {
                Application.Run(new Forms.FormMultipleChoiceTest());
            }
        }
    }
}