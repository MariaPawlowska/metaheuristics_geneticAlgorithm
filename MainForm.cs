using System;
using System.Text;
using System.Windows.Forms;

namespace metaheuristics_geneticAlgorithm
{
    public class MainForm : Form
    {
        private TextBox txtRows, txtCols, txtFillPercent, txtErrors, txtMutationFreq;
        private TextBox txtOutput;
        private Button btnGenerate;

        public MainForm()
        {
            this.Text = "Generator Instancji (Problem IV)";
            //rozmiar okna 
            this.Width = 800;
            this.Height = 600;
            //minimalny rozmiar okna - zabespieczenie przed zawinięciem całkowicie/do zera
            this.MinimumSize = new System.Drawing.Size(500, 400);

            int yPos = 20;
            //pola do wpisania przez użytkownika 
            txtRows = AddInputRow("Liczba wierszy (m):", "", ref yPos);
            txtCols = AddInputRow("Liczba kolumn (n):", "", ref yPos);
            txtFillPercent = AddInputRow("% występowania '1':", "", ref yPos);
            txtErrors = AddInputRow("Liczba błędów:", "", ref yPos);
            txtMutationFreq = AddInputRow("Częstotliwość mutacji:", "", ref yPos);

            btnGenerate = new Button() { Text = "Generuj Macierz", Top = yPos, Left = 20, Width = 150, Height = 40 };
            btnGenerate.Click += BtnGenerate_Click;
            this.Controls.Add(btnGenerate);

            txtOutput = new TextBox()
            {
                Top = yPos + 40,
                Left = 20,
                //szerokość i wysokość okina macierzy wynikowej liczona dynamicznie od wielkości okna
                Width = this.ClientSize.Width - 40,
                Height = this.ClientSize.Height - (yPos + 60),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false, //wyłączenie załamywania wierszy
                ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 10),
                //zahaczenie pola tekstowego z macierzą do okna, aby było czytelne
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(txtOutput);
        }

        private TextBox AddInputRow(string labelText, string defaultValue, ref int y)
        {
            Label lbl = new Label() { Text = labelText, Top = y, Left = 20, Width = 200 };
            TextBox txt = new TextBox() { Text = defaultValue, Top = y, Left = 230, Width = 100 };
            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            y += 30;
            return txt;
        }

        private void BtnGenerate_Click(object? sender, EventArgs e)
        {
            try
            {
                int m = int.Parse(txtRows.Text);
                int n = int.Parse(txtCols.Text);
                double fill = double.Parse(txtFillPercent.Text);
                int errors = int.Parse(txtErrors.Text);

                instance_generator generator = new instance_generator();

                byte[][] finalMatrix = generator.GenerateInstance(m, n, fill, errors);

                StringBuilder sb = new StringBuilder();
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        sb.Append(finalMatrix[r][c]);
                    }
                    sb.AppendLine();
                }

                txtOutput.Text = sb.ToString();
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Wypełnij poprawnie wszystkie pola liczbami!\nSzczegóły: " + ex.Message, "Błąd wprowadzania");
            }
        }
    }
}