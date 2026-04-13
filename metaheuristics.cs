using System;
using System.ComponentModel;
using System.Threading;

namespace metaheuristics_geneticAlgorithm{

    public class AlgorithmSettings
    {
        public byte[][] Matrix { get; set; }
        public int NumberOfIteration { get; set; }
        public int PopulationSize { get; set; }
        public double Mutation { get; set; }
        public double Crossing { get; set; }
    }

    public class metaheuristics
    {
        
        public void UruchomAlgorytm(AlgorithmSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            
            int m = settings.Matrix.Length;      // liczba fragmentów (wierszy)
            int n = settings.Matrix[0].Length;   // liczba próbek (kolumn)

            // pętla ewolucyjna
            for (int i = 1; i <= settings.NumberOfIteration; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //tutaj kroki AG (Selekcja, Krzyżowanie, Mutacja)
               
                Thread.Sleep(50);

                
                double pseudoFunkcjaCelu = (m * n) * Math.Exp(-i * 0.05) + new Random().NextDouble() * 5;

                int progressPercentage = (int)((i / (double)settings.NumberOfIteration) * 100);

                double[] paczkaDanych = new double[2];
                paczkaDanych[0] = i;
                paczkaDanych[1] = pseudoFunkcjaCelu;

                worker.ReportProgress(progressPercentage, paczkaDanych);
            }
        }
    }
}