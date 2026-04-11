using System;
using System.Collections.Generic;
using System.Text;

namespace metaheuristics_geneticAlgorithm
{
    internal class instance_generator
    {
        private Random rnd = new Random();

        //metoda generująca i tasująca macierz
        public byte[][] GenerateInstance(int m, int n, double fillPercentage, int errorCount)
        {
            
            byte[][] matrix = new byte[m][];
            for (int i = 0; i < m; i++)
            {
                matrix[i] = new byte[n];
            }

            
            int x = (int)Math.Round(n * (fillPercentage / 100.0));
            if (x < 1) x = 1;
            if (x >= n) x = n - 1;

            int maxStartId = n - x;

            int[] startCounts = new int[maxStartId + 1];
            int limit = (m / (maxStartId + 1)) + 2;
            int[] rowStarts = new int[m];

            for (int r = 0; r < m; r++)
            {
                int startId;
                do
                {
                    startId = rnd.Next(0, maxStartId + 1);
                }
                while (startCounts[startId] >= limit);

                startCounts[startId]++;
                rowStarts[r] = startId;

                for (int col = startId; col < startId + x; col++)
                {
                    matrix[r][col] = 1;
                }
            }

            
            bool maPusteKolumny = true;
            while (maPusteKolumny)
            {
                maPusteKolumny = false;
                for (int c = 0; c < n; c++)
                {
                    bool hasOne = false;
                    for (int r = 0; r < m; r++)
                    {
                        if (matrix[r][c] == 1) { hasOne = true; break; }
                    }

                    if (!hasOne)
                    {
                        maPusteKolumny = true;
                        int randomRow = rnd.Next(0, m);

                        for (int i = rowStarts[randomRow]; i < rowStarts[randomRow] + x; i++)
                            matrix[randomRow][i] = 0;

                        int newStart = c - (x / 2);
                        if (newStart < 0) newStart = 0;
                        if (newStart > n - x) newStart = n - x;

                        rowStarts[randomRow] = newStart;
                        for (int i = newStart; i < newStart + x; i++)
                            matrix[randomRow][i] = 1;
                    }
                }
            }

           
            if (errorCount > 0)
            {
                int zrobioneBledy = 0;
                HashSet<string> usedCells = new HashSet<string>();
                int attempts = 0;

                while (zrobioneBledy < errorCount && attempts < errorCount * 10)
                {
                    attempts++;
                    int r = rnd.Next(0, m);
                    int id = rowStarts[r];
                    int opcja = rnd.Next(1, 3);

                    if (opcja == 1 && x >= 3) 
                    {
                        int errCol = rnd.Next(id + 1, id + x - 1);
                        string key = $"{r},{errCol}";
                        if (!usedCells.Contains(key))
                        {
                            matrix[r][errCol] = 0;
                            usedCells.Add(key);
                            zrobioneBledy++;
                        }
                    }
                    else if (opcja == 2) 
                    {
                        List<int> availableCols = new List<int>();
                        for (int c = 0; c <= id - 2; c++) availableCols.Add(c);
                        for (int c = id + x + 1; c < n; c++) availableCols.Add(c);

                        if (availableCols.Count > 0)
                        {
                            int errCol = availableCols[rnd.Next(availableCols.Count)];
                            string key = $"{r},{errCol}";
                            if (!usedCells.Contains(key))
                            {
                                matrix[r][errCol] = 1;
                                usedCells.Add(key);
                                zrobioneBledy++;
                            }
                        }
                    }
                }
            }

            //tasowanie Fisher-Yates
           
            int[] colIndices = new int[n];
            for (int i = 0; i < n; i++) colIndices[i] = i;

            bool isIdentical;
            do
            {
                isIdentical = true; 

                
                for (int i = n - 1; i > 0; i--)
                {
                    int j = rnd.Next(0, i + 1);
                    int temp = colIndices[i];
                    colIndices[i] = colIndices[j];
                    colIndices[j] = temp;
                }

                
                for (int i = 0; i < n; i++)
                {
                    if (colIndices[i] != i)
                    {
                        isIdentical = false; 
                        break;
                    }
                }

                
                if (n <= 1) break;

            } while (isIdentical); 

            
            byte[][] shuffledMatrix = new byte[m][];
            for (int r = 0; r < m; r++)
            {
                shuffledMatrix[r] = new byte[n];
                for (int c = 0; c < n; c++)
                {
                    shuffledMatrix[r][c] = matrix[r][colIndices[c]];
                }
            }
            return shuffledMatrix;
        }
    }
}