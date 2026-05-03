using System;
using System.Windows.Forms;

namespace metaheuristics_geneticAlgorithm
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            //podstawowa konfiguracja okienek Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //uruchomienie głównego okna
            Application.Run(new MainForm());
        }
    }
}