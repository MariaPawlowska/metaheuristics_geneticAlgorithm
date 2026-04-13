using System;
using System.Windows.Forms;

namespace metaheuristics_geneticAlgorithm
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            // Podstawowa konfiguracja okienek Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Uruchomienie Twojego głównego okna
            Application.Run(new MainForm());
        }
    }
}