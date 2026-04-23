using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Data;

namespace metaheuristics_geneticAlgorithm
{
    public partial class MainForm : Form
    {
        private BackgroundWorker bw;
        private byte[][] CurrentMatrix;

        //listy do trzymania historii wyników dla wykresu
        private List<double> dataX = new List<double>();
        private List<double> dataY = new List<double>();

        private Stopwatch stoper = new Stopwatch(); //obiekt do mierzenia czasu

        //zmienne do obsługi wprowadzania własnej macierzy
        DataTable DTableManualInput;
        BindingSource SBindManualInput;

        //zmienne do wyświetlania przetasowanej własnej macierzy
        DataTable DTableManualShuffled;
        BindingSource SBindManualShuffled;


        public MainForm()
        {
            InitializeComponent();

            //eventy przycisków
            Button_matrix.Click += Button_matrix_Click;
            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            btnManualCreate.Click += btnManualCreate_Click;
            btnManualShuffle.Click += btnManualShuffle_Click;

            btnStop.Enabled = false;

            //konfiguracja BackgroundWorker 
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

            //przygotowanie wykresu
            plotResults.Plot.Clear();
            plotResults.Refresh();
        }

        //generator instancji - podpięcie
        private void Button_matrix_Click(object sender, EventArgs e)
        {
            try
            {
                int m = (int)row_num.Value;
                int n = (int)col_num.Value;
                double fill = (double)percent_num.Value;
                int errors = (int)error_num.Value;

                instance_generator generator = new instance_generator();

                //wywołanie generatora tylko raz
                CurrentMatrix = generator.GenerateInstance(m, n, fill, errors);
                byte[][] finalMatrix = CurrentMatrix;

                StringBuilder sb = new StringBuilder();
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        sb.Append(finalMatrix[r][c]);
                    }
                    sb.AppendLine();
                }

                Matrix_output.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd generatora: " + ex.Message);
            }
        }

        //guzik startu
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                console.Clear();
                dataX.Clear();
                dataY.Clear();
                plotResults.Plot.Clear();
                progressBar.Value = 0;

                tabControl1.SelectedIndex = 3; //przejście do zakładki wyniki

                int maxIterations = (int)iterate_num.Value;
                if (maxIterations == 0) maxIterations = 100;

                var settings = new AlgorithmSettings
                {
                    Matrix = CurrentMatrix,
                    NumberOfIteration = (int)iterate_num.Value,
                    PopulationSize = (int)pupulation_num.Value,
                    Mutation = (double)mutation_num.Value
                };

                stoper.Restart();//uruchomienie stopera
                bw.RunWorkerAsync(settings);
            }
        }

        //guzik stopu
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                btnStop.Enabled = false;
                console.AppendText("Przerywanie...\r\n");
            }
        }

        //tworzenie pustej siatki dla manualnego wpisywania matrix
        private void btnManualCreate_Click(object sender, EventArgs e)
        {
            int m = (int)manual_row_num.Value;
            int n = (int)manual_col_num.Value;

            if (m <= 0 || n <= 0)
            {
                MessageBox.Show("Podaj wymiary macierzy większe od zera.");
                return;
            }

            DTableManualInput = new DataTable();
            SBindManualInput = new BindingSource();
            SBindManualInput.DataSource = DTableManualInput;
            dgvManualInput.DataSource = SBindManualInput;

            DTableManualInput.Clear();

            for (int c = 0; c < n; c++)
            {
                DTableManualInput.Columns.Add($"C{c}");
            }

            for (int r = 0; r < m; r++)
            {
                DataRow row = DTableManualInput.NewRow();
                for (int c = 0; c < n; c++)
                {
                    row[c] = "0"; // Domyślna wartość w komórkach
                }
                DTableManualInput.Rows.Add(row);
            }

            foreach (DataGridViewColumn col in dgvManualInput.Columns)
            {
                col.Width = 35;
            }
        }

        //zapis i tasowanie dla manualnej matrix
        private void btnManualShuffle_Click(object sender, EventArgs e)
        {
            if (DTableManualInput == null || DTableManualInput.Rows.Count == 0)
            {
                MessageBox.Show("Najpierw utwórz siatkę i wprowadź dane.");
                return;
            }

            int m = (int)manual_row_num.Value;
            int n = (int)manual_col_num.Value;
            byte[][] manualMatrix = new byte[m][];

            // 1. Zczytywanie i walidacja danych (tylko 0 i 1)
            for (int r = 0; r < m; r++)
            {
                manualMatrix[r] = new byte[n];
                for (int c = 0; c < n; c++)
                {
                    string cellVal = DTableManualInput.Rows[r][c].ToString();
                    if (byte.TryParse(cellVal, out byte val) && (val == 0 || val == 1))
                    {
                        manualMatrix[r][c] = val;
                    }
                    else
                    {
                        MessageBox.Show($"Niedozwolona wartość w wierszu {r + 1}, kolumnie {c + 1}. Dozwolone tylko 0 lub 1.");
                        return;
                    }
                }
            }

            // 2. Zapis macierzy użytkownika do pliku (zanim przetasujemy)
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "macierze");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                StringBuilder manualSb = new StringBuilder();
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < n; c++) manualSb.Append(manualMatrix[r][c]);
                    manualSb.AppendLine();
                }
                File.WriteAllText(Path.Combine(folderPath, $"manual_matrix_{timestamp}.txt"), manualSb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd przy zapisie macierzy: " + ex.Message);
            }

            // 3. Tasowanie kolumn (Fisher-Yates)
            Random rnd = new Random();
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
                    if (colIndices[i] != i) { isIdentical = false; break; }
                }
                if (n <= 1) break;
            } while (isIdentical);

            // 4. Tworzenie przetasowanej macierzy dla algorytmu
            CurrentMatrix = new byte[m][];
            for (int r = 0; r < m; r++)
            {
                CurrentMatrix[r] = new byte[n];
                for (int c = 0; c < n; c++)
                {
                    CurrentMatrix[r][c] = manualMatrix[r][colIndices[c]];
                }
            }

            // 5. Wyświetlenie przetasowanej macierzy w drugim DataGridView[cite: 22]
            DTableManualShuffled = new DataTable();
            SBindManualShuffled = new BindingSource();
            SBindManualShuffled.DataSource = DTableManualShuffled;
            dgvManualShuffled.DataSource = SBindManualShuffled;

            DTableManualShuffled.Clear();

            for (int c = 0; c < n; c++) DTableManualShuffled.Columns.Add($"C{c}");

            for (int r = 0; r < m; r++)
            {
                DataRow row = DTableManualShuffled.NewRow();
                for (int c = 0; c < n; c++)
                {
                    row[c] = CurrentMatrix[r][c];
                }
                DTableManualShuffled.Rows.Add(row);
            }

            foreach (DataGridViewColumn col in dgvManualShuffled.Columns) col.Width = 35;

      
        }

        //obliczenia algorytmu - osobny, główny wątek
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // rzutowanie workera
            BackgroundWorker worker = sender as BackgroundWorker;

            // pobieranie całej paczki ustawień przekazanej z btnStart_Click
            AlgorithmSettings settings = (AlgorithmSettings)e.Argument;

            //tworzenie obiektu metaheurystyki
            metaheuristics algorytm = new metaheuristics();

            //wywołanie metaheurystyki, przekazujemy paczkę ustawień
            algorytm.UruchomAlgorytm(settings, worker, e);
        }

        //odbiór danych na głównym wątku
        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            //pobieramy info z metaheurystyki w tabeli i rozpakowujemy (info o iteracji i f. celu..)
            if (e.UserState is double[] dane)
            {
                int iteracja = (int)dane[0];
                double cel = dane[1];

                console.AppendText($"Iteracja {iteracja}: {cel:F2}\r\n");

                //zapisanie aktualnych danych i zapis do list w celu narysowania kompletnego wykresu
                dataX.Add(iteracja);
                dataY.Add(cel);

                plotResults.Plot.Clear();
                plotResults.Plot.Add.Scatter(dataX.ToArray(), dataY.ToArray());
                plotResults.Plot.Axes.AutoScale();
                plotResults.Refresh();
            }
        }

        //koniec wątku
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            if (e.Cancelled)
                console.AppendText("Zakończono przedwcześnie.\r\n");
            else if (e.Error != null)
                console.AppendText("Błąd: " + e.Error.Message + "\r\n");
            else if (e.Result is Individual najlepszy) // Odbieramy naszego mistrza
            {

                stoper.Stop();//koniec pomiaru czasu
                console.AppendText("\r\n--- WYNIK KOŃCOWY ---\r\n");
                console.AppendText($"Czas wykonania algorytmu: {stoper.Elapsed.TotalSeconds:F3} s\r\n");//czas w sekundach, do 3 miejsc po przecinku
                console.AppendText($"Najlepsza funkcja celu: {najlepszy.Fitness}\r\n");

                // Wyświetlanie indeksów kolumn
                string indeksy = string.Join(", ", najlepszy.Genotype);
                console.AppendText($"Kolejność kolumn: {indeksy}\r\n");


                console.AppendText("Zrekonstruowana macierz:\r\n");
                for (int r = 0; r < CurrentMatrix.Length; r++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int c = 0; c < CurrentMatrix[0].Length; c++)
                    {
                        // z oryginalnej, przetasowanej macierzy bierzemy kolumnę wskazaną przez genotyp
                        sb.Append(CurrentMatrix[r][najlepszy.Genotype[c]]);
                    }
                    console.AppendText(sb.ToString() + "\r\n");
                }

                console.AppendText("----------------------\r\n");
                progressBar.Value = 100;


                //tworzenie folderu na wykresy
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wykresy");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = $"wykres_wynikow_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string filePath = Path.Combine(folderPath, fileName);

                //budowanie pliku
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("Iteracja;Wartosc_Funkcji_Celu");//nagłówek


                for (int i = 0; i < dataX.Count; i++)
                {
                    //zamiana liczb na stringi z kropką dziesiętną - żeby nie było problemu przy odczycie
                    string x = dataX[i].ToString(CultureInfo.InvariantCulture);
                    string y = dataY[i].ToString(CultureInfo.InvariantCulture);

                    csv.AppendLine($"{x};{y}");
                }

                File.WriteAllText(filePath, csv.ToString());
            }
        }

        //puste zdarzenia, missclick
        private void label1_Click(object sender, EventArgs e) { }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }

        private void console_TextChanged(object sender, EventArgs e) { }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void row_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
    }
}