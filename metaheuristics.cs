using ScottPlot.TickGenerators.Financial;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace metaheuristics_geneticAlgorithm{

    public class AlgorithmSettings
    {
        public byte[][] Matrix { get; set; }
        public int NumberOfIteration { get; set; }
        public int PopulationSize { get; set; }
        public double Mutation { get; set; }
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
            int n = settings.Matrix[0].Length;   // liczba próbek (kolumn - genów)

            List<Individual> population = new List<Individual>(); // inicjalizacja początkowej populacji

            for (int p = 0; p < settings.PopulationSize; p++) {
                
                Individual newIndiv = new Individual(n);

                for (int i = 0; i < n; i++) newIndiv.Genotype[i] = i;

                // tasowanie jak w generatorze
                for(int i = n-1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    int temp = newIndiv.Genotype[i];
                    newIndiv.Genotype[i] = newIndiv.Genotype[j];
                    newIndiv.Genotype[j] = temp;
                }

                // ocena wygenerowanego osobnika
                FitnessScore(newIndiv, settings.Matrix);
                population.Add(newIndiv);

            }


            //inicjalizacja najlepszego wyniku
            double generalBestScore = double.MaxValue;
            Individual bestEver = new Individual(n);//najlepszy układ kolumn

            //szukamy najlepszego osobnika już w wylosowanej populacji startowej
            foreach (var individual in population)
            {
                if (individual.Fitness < generalBestScore)
                {
                    generalBestScore = individual.Fitness;
                    Array.Copy(individual.Genotype, bestEver.Genotype, n);
                    bestEver.Fitness = individual.Fitness;
                }
            }

            int stagnacionCount = 0;

            //próg na 20% całkowitej liczby iteracji
            int stagnationLimit = (int)(settings.NumberOfIteration * 0.20);
            if (stagnationLimit < 50)
            {
                stagnationLimit = 50; //zabezpieczenie dla bardzo małej liczby iteracji
            }


            //główna pętla ewolucyjna
            for (int iteration = 1; iteration <= settings.NumberOfIteration; iteration++)
            {
                // czy user nie wcisnął STOP
                if (worker.CancellationPending)
                {
                    break;
                }

                //najlepszy osobnik
                Individual elite = population[0];
                foreach (var individual in population)
                {
                    if(individual.Fitness < elite.Fitness)
                    {
                        elite = individual;
                    }
                }


                //nowe pokolenie
                List<Individual> newPopulation = new List<Individual>();

                // kopiujemy mistrza bezpośrednio do nowej populacji
                Individual eliteCopy = new Individual(n);
                for (int i = 0; i<n; i++) eliteCopy.Genotype[i] = elite.Genotype[i];
                eliteCopy.Fitness = elite.Fitness;
                newPopulation.Add(eliteCopy);

                //tworzenie dzieci dopóki nie osiągniemy rozmiaru populacji
                while (newPopulation.Count < settings.PopulationSize)
                {
                    //selekcja
                    Individual parent1 = TournamentSelection(population);
                    Individual parent2 = TournamentSelection(population);

                    //krzyżowanie
                    Individual children = CrossingOver(parent1, parent2);

                    //mutacja
                    Mutation(children, settings.Mutation);

                    //ocena nowego osobnika i dodanie do populacji
                    FitnessScore(children, settings.Matrix);
                    newPopulation.Add(children);
                }

                population = newPopulation;

                if(elite.Fitness < generalBestScore)
                {
                    generalBestScore = elite.Fitness;
                    stagnacionCount = 0;

                    bestEver = new Individual(n);
                    Array.Copy(elite.Genotype, bestEver.Genotype, n);
                    bestEver.Fitness = elite.Fitness;
                }
                else
                {
                    stagnacionCount++;
                }


                //jeśli nie ma poprawy przez zdefiniowany procent czasu - stop
                if (stagnacionCount >= stagnationLimit)
                {
                    //wypisanie informacji do konsoli 
                    worker.ReportProgress(100, new double[] { iteration, generalBestScore });
                    break;
                }
                //jeżeli osiągnieto zero błędów to przerwanie
                if (generalBestScore == 0)
                {
                    worker.ReportProgress(100, new double[] { iteration, generalBestScore });
                    break;
                }


                //przekaz info do interfejsu - pasek postępu i wykres
                int progressPercentage = (int)((iteration / (double)settings.NumberOfIteration) * 100);

                double[] dataPackage = new double[2];
                dataPackage[0] = iteration;
                dataPackage[1] = generalBestScore;

                worker.ReportProgress(progressPercentage, dataPackage);
            }
            e.Result = bestEver;

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
            int numRow = matrix.Length;
            double sumScore = 0;

            for (int r = 0; r < numRow; r++)
            {
                sumScore += ColScore(matrix[r], individual.Genotype); //suma kosztów dla każdego wiersza w macierzy
            }
            individual.Fitness = sumScore;
        }


        private Individual TournamentSelection(List<Individual> population)
        {
            int tournamentSize = 3;
            Individual bestIndividual = null;
            List<int> usedCandidates = new List<int>();//lista z zapisanymi uczestnikami - metoda bez zwracania

            for(int i = 0; i < tournamentSize; i++)
            {
                int randomId;

                do
                {
                    randomId = rnd.Next(population.Count);
                }
                while (usedCandidates.Contains(randomId));//jeśli ten id już jest na liście to pętla leci od nowa

                usedCandidates.Add(randomId);

                Individual candidate = population[randomId];    

                if(bestIndividual == null || candidate.Fitness < bestIndividual.Fitness) //jeżeli pierwszy kandydat lub lepszy ma fitness score niż dotychczasowy najlepszy, obecny kandydat
                {
                    bestIndividual = candidate;
                } 
            }
            return bestIndividual;
        }

        private Individual CrossingOver(Individual parent1, Individual parent2)
        {
            int n = parent1.Genotype.Length;
            Individual children = new Individual(n);

            for (int i =0; i < n; i++)
            {
                children.Genotype[i] = -1;
            }

            int point1 = rnd.Next(n);
            int point2 = rnd.Next(n);

            //z wylosowanych punktów ustawiamy je od najmniejszego - strat, do największego - end
            int start = Math.Min(point1, point2);
            int end = Math.Max(point1, point2);

            bool[] usedCol = new bool[n]; //tablica pomocnicza - czy kolumna została użyta od rodzica 1

            for(int i = start; i <= end; i++)//kopiowanie genotypu (kolumny) od rodzica 1 
            {
                children.Genotype[i] = parent1.Genotype[i];
                usedCol[parent1.Genotype[i]] = true; // ta kolumna np. nr 2 została użyta na id = 0
            }

            int freeId = 0;

            for (int i = 0; i < n; i++)
            {
                //szukanie pierwszego wolnego indeksu u dziecka
                while (freeId < n && children.Genotype[freeId]!= -1)
                {
                    freeId++; // następny id u dziecka, szukamy kolejnego wolnego (z -1)
                }

                if (freeId >= n) break; // gdy dojdziemy już do momentu gdzie przejdziemy cały genotyp dziecka i uzupełnimy genami rodzica 1 i 2 - break dla for 

                int nextParent2Gen = parent2.Genotype[i];

                if (!usedCol[nextParent2Gen]) // gdy nie użyty juz gen, to zapisujemy w wolne miejsce gen od rodzica 2
                {
                    children.Genotype[freeId] = nextParent2Gen;
                    usedCol[nextParent2Gen] = true; 
                }
            }
            return children;
        }

        private void Mutation(Individual children, double probabOfMutation)
        {
            double x = rnd.NextDouble()*100.0; // losowanie liczby od 0 do 100 

            if(x <= probabOfMutation)
            {
                int n = children.Genotype.Length;

                //losuje 2 indeksy z genotypu dziecka 
                int id1 = rnd.Next(n);
                int id2 = rnd.Next(n);

                //zamiana 2 genów miejscami
                int temp = children.Genotype[id1];
                children.Genotype[id1] = children.Genotype[id2];
                children.Genotype[id2] = temp;
            }
        }
    }
}



