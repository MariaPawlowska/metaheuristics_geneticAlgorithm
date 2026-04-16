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

    public class Individual
    {
        public int[] Genotype { get; set; }
        public double Fitness { get; set; }

        public Individual(int numberOfCol)
        {
            Genotype = new int[numberOfCol]; // tablica dla osobnika
        }
    }

    public class metaheuristics
    {
        private Random rnd = new Random();

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

        private int ColScore(byte[] row, int[] genotype)
        {
            int n = genotype.Length;
            int allOnces = 0; // licznik jedynek


            for (int i = 0; i< n; i++) // zliczanie jedynek w genotypie osobnika
            {
                if (row[genotype[i]] == 1)
                {
                    allOnces++; 
                }
            }


            int minScore = allOnces;

            for (int start = 0; start < n; start++)
            {
                int onesSection = 0; // jednynki w bloku "1"
                int zeorsSection = 0;

                for (int end = start; end < n; end++)
                {
                    if (row[genotype[end]] == 1) // czy jedynka w bloku
                        onesSection++;
                   
                    else
                        zeorsSection++; // tyle trzeba zamienić 0 -> 1

                    int noneSectionOnes = allOnces - onesSection; // jedynki po za blokiem "1" (tyle trzeba zamienić 1 -> 0)
                    int currentScore = zeorsSection + noneSectionOnes; // całkowity koszt operacji zmiany

                    if (currentScore < minScore)
                    {
                        minScore = currentScore;    
                    }

                }
            }

            return minScore;
        }

        private void FitnessScore(Individual individual, byte[][] matrix)
        {
            int numCol = matrix.Length;
            double sumScore = 0;

            for (int r = 0; r < numCol; r++)
            {
                sumScore += ColScore(matrix[r], individual.Genotype); //suma kosztów dla każdego wiersza w macierzy
            }
            individual.Fitness = sumScore;
        }


        private Individual TournamentSelection(List<Individual> population)
        {
            int tournamentSize = 3;
            Individual bestIndividual = null;

            for(int i = 0; i < tournamentSize; i++)
            {
                int randomId = rnd.Next(population.Count);
                Individual candidate = population[randomId];    

                if(bestIndividual == null || candidate.Fitness < bestIndividual.Fitness) //jeżeli pierwszy kandydat lub lepszy ma fitness score niż dotychczasowy najlepszy, obecny kandydat
                {
                    bestIndividual = candidate;
                } 
            }
            return bestIndividual;
        }
    }
}