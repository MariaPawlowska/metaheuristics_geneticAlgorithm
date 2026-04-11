using System;
using System.Windows.Forms;

namespace metaheuristics_geneticAlgorithm
{
    internal class metaheuristics 
    {
       
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

      
    }
}