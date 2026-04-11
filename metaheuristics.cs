using System;
using System.ComponentModel; 
using System.Threading;

namespace metaheuristics_geneticAlgorithm
{
    public class metaheuristics
    {
        
        public void UruchomAlgorytm(int totalIterations, BackgroundWorker worker, DoWorkEventArgs e)
        {
           

            for (int i = 1; i <= totalIterations; i++)
            {
                //sprawdzanie, czy ktoś wcisnął STOP w okienku
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return; //zakończ metodę natychmiast
                }

            
                Thread.Sleep(50);
                double pseudoFunkcjaCelu = 5000.0 / (i + 5) + new Random().NextDouble() * 10;

                //pakowanie wyników i obliczanie procentów
                int progressPercentage = (int)((i / (double)totalIterations) * 100);

                double[] paczkaDanych = new double[2];
                paczkaDanych[0] = i;
                paczkaDanych[1] = pseudoFunkcjaCelu;

                
                worker.ReportProgress(progressPercentage, paczkaDanych);
            }
        }
    }
}