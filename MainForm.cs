using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace metaheuristics_geneticAlgorithm
{
    public partial class MainForm : Form
    {
        private BackgroundWorker bw;
        private byte[][] CurrentMatrix;

        //listy do trzymania historii wyników dla wykresu
        private List<double> dataX = new List<double>();
        private List<double> dataY = new List<double>();

        public MainForm() 
        {
            InitializeComponent();

            //eventy przycisków
            Button_matrix.Click += Button_matrix_Click;
            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;

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

                tabControl1.SelectedIndex = 2; //przejście do zakładki wyniki

                int maxIterations = (int)iterate_num.Value;
                if (maxIterations == 0) maxIterations = 100;

                var settings = new AlgorithmSettings
                {
                    Matrix = CurrentMatrix,
                    NumberOfIteration = (int)iterate_num.Value,
                    PopulationSize = (int)pupulation_num.Value,
                    Mutation = (double)mutation_num.Value,
                    Crossing = (double)cross_num.Value
                };

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
            else
            {
                console.AppendText("Zakończono obliczenia.\r\n");
                progressBar.Value = 100;
            }
        }

        //puste zdarzenia, missclick
        private void label1_Click(object sender, EventArgs e) { }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
    }
}