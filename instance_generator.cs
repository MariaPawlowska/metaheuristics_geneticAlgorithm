using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

            //tablica na indywidualne długości bloków dla każdego wiersza
            int[] xPerRow = new int[m];

            //zmienne pomocnicze do oszacowania limitu "startów" kolumn - do jakiej kolumny mozna wstawić blok "1"
            int avgX = (int)Math.Round(n * (fillPercentage / 100.0));
            if (avgX < 1) avgX = 1;
            if (avgX >= n) avgX = n - 1;

            for (int r = 0; r < m; r++)
            {
                //transformacja Boxa-Mullera - rozkład normalny
                double u1 = 1.0 - rnd.NextDouble();
                double u2 = 1.0 - rnd.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                //odchylenie standardowe = 2.0 (ok. 99% wyników zmieści się w odchyleniu +/- 6%)
                double rowFill = fillPercentage + (randStdNormal * 2.0);

                //wyliczenie długości bloku 'x' dla konkretnego wiersza
                int x = (int)Math.Round(n * (rowFill / 100.0));

                // Zabezpieczenia brzegowe - żeby zawsze był min. 1 znak i żeby nie zajął całego wiersza
                if (x < 1) x = 1;
                if (x >= n) x = n - 1;

                xPerRow[r] = x;
            }

            //bezpieczny rozmiar tablicy 'startCounts' obejmujący wszystkie możliwe indeksy startowe
            int[] startCounts = new int[n];
            int limit = (m / ((n - avgX) + 1)) + 2;
            int[] rowStarts = new int[m];

            //pętla wypełniająca używa zróżnicowanych długości bloków (xPerRow[r])
            for (int r = 0; r < m; r++)
            {
                int x = xPerRow[r]; //pobranie 'x' dla aktualnego wiersza
                int maxStartId = n - x;
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

            //naprawa pustych kolumn z uwzględnieniem konkretnego wiersza
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
                        int x = xPerRow[randomRow]; //'x' dla wylosowanego wiersza

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

            //dodawanie błędów przy użyciu odpowiedniego 'x'
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
                    int x = xPerRow[r]; //odczytujemy 'x' dla wiersza, w którym generujemy błąd
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

           //zapis ideal_matrix.txt
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "macierze");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                StringBuilder idealSb = new StringBuilder();
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        idealSb.Append(matrix[r][c]);
                    }
                    idealSb.AppendLine();
                }
                string matrixPath = Path.Combine(folderPath, $"ideal_matrix_{timestamp}.txt");
                File.WriteAllText(matrixPath, idealSb.ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Błąd zapisu pliku z idealną macierzą: " + ex.Message);
            }

            // --- Tasowanie Fisher-Yates ---
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