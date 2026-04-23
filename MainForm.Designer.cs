namespace metaheuristics_geneticAlgorithm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            Button_matrix = new Button();
            Matrix_output = new TextBox();
            error = new Label();
            ones = new Label();
            col = new Label();
            row = new Label();
            label1 = new Label();
            error_num = new NumericUpDown();
            percent_num = new NumericUpDown();
            col_num = new NumericUpDown();
            row_num = new NumericUpDown();
            tabPageManual = new TabPage();
            dgvManualShuffled = new DataGridView();
            dgvManualInput = new DataGridView();
            btnManualShuffle = new Button();
            btnManualCreate = new Button();
            manual_col_num = new NumericUpDown();
            manual_row_num = new NumericUpDown();
            manual_col = new Label();
            manual_row = new Label();
            tabPage2 = new TabPage();
            btnStop = new Button();
            btnStart = new Button();
            mutation = new Label();
            iteration = new Label();
            population = new Label();
            mutation_num = new NumericUpDown();
            iterate_num = new NumericUpDown();
            pupulation_num = new NumericUpDown();
            tabPage3 = new TabPage();
            plotResults = new ScottPlot.WinForms.FormsPlot();
            console = new TextBox();
            progress = new Label();
            progressBar = new ProgressBar();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)error_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)percent_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)col_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)row_num).BeginInit();
            tabPageManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvManualShuffled).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvManualInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)manual_col_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)manual_row_num).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mutation_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iterate_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pupulation_num).BeginInit();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPageManual);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1475, 720);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(Button_matrix);
            tabPage1.Controls.Add(Matrix_output);
            tabPage1.Controls.Add(error);
            tabPage1.Controls.Add(ones);
            tabPage1.Controls.Add(col);
            tabPage1.Controls.Add(row);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(error_num);
            tabPage1.Controls.Add(percent_num);
            tabPage1.Controls.Add(col_num);
            tabPage1.Controls.Add(row_num);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1467, 687);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Generator instancji";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // Button_matrix
            // 
            Button_matrix.Location = new Point(83, 317);
            Button_matrix.Name = "Button_matrix";
            Button_matrix.Size = new Size(162, 29);
            Button_matrix.TabIndex = 10;
            Button_matrix.Text = "Generuj macierz";
            Button_matrix.UseVisualStyleBackColor = true;
            // 
            // Matrix_output
            // 
            Matrix_output.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Matrix_output.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Matrix_output.Location = new Point(426, 44);
            Matrix_output.Multiline = true;
            Matrix_output.Name = "Matrix_output";
            Matrix_output.ScrollBars = ScrollBars.Both;
            Matrix_output.Size = new Size(990, 552);
            Matrix_output.TabIndex = 9;
            // 
            // error
            // 
            error.AutoSize = true;
            error.Location = new Point(23, 247);
            error.Name = "error";
            error.Size = new Size(105, 20);
            error.TabIndex = 8;
            error.Text = "Liczba błędów";
            // 
            // ones
            // 
            ones.AutoSize = true;
            ones.Location = new Point(23, 203);
            ones.Name = "ones";
            ones.Size = new Size(128, 20);
            ones.TabIndex = 7;
            ones.Text = "% wypełnienia \"1\"";
            // 
            // col
            // 
            col.AutoSize = true;
            col.Location = new Point(23, 158);
            col.Name = "col";
            col.Size = new Size(104, 20);
            col.TabIndex = 6;
            col.Text = "Liczba kolumn";
            // 
            // row
            // 
            row.AutoSize = true;
            row.Location = new Point(23, 111);
            row.Name = "row";
            row.Size = new Size(103, 20);
            row.TabIndex = 5;
            row.Text = "Liczba wierszy";
            row.Click += row_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(342, 203);
            label1.Name = "label1";
            label1.Size = new Size(21, 20);
            label1.TabIndex = 4;
            label1.Text = "%";
            label1.Click += label1_Click;
            // 
            // error_num
            // 
            error_num.Location = new Point(173, 247);
            error_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            error_num.Name = "error_num";
            error_num.Size = new Size(150, 27);
            error_num.TabIndex = 3;
            // 
            // percent_num
            // 
            percent_num.Location = new Point(173, 203);
            percent_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            percent_num.Name = "percent_num";
            percent_num.Size = new Size(150, 27);
            percent_num.TabIndex = 2;
            // 
            // col_num
            // 
            col_num.Location = new Point(173, 158);
            col_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            col_num.Name = "col_num";
            col_num.Size = new Size(150, 27);
            col_num.TabIndex = 1;
            col_num.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // row_num
            // 
            row_num.Location = new Point(173, 111);
            row_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            row_num.Name = "row_num";
            row_num.Size = new Size(150, 27);
            row_num.TabIndex = 0;
            row_num.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // tabPageManual
            // 
            tabPageManual.Controls.Add(dgvManualShuffled);
            tabPageManual.Controls.Add(dgvManualInput);
            tabPageManual.Controls.Add(btnManualShuffle);
            tabPageManual.Controls.Add(btnManualCreate);
            tabPageManual.Controls.Add(manual_col_num);
            tabPageManual.Controls.Add(manual_row_num);
            tabPageManual.Controls.Add(manual_col);
            tabPageManual.Controls.Add(manual_row);
            tabPageManual.Location = new Point(4, 29);
            tabPageManual.Name = "tabPageManual";
            tabPageManual.Size = new Size(1467, 687);
            tabPageManual.TabIndex = 3;
            tabPageManual.Text = "Własna macierz";
            tabPageManual.UseVisualStyleBackColor = true;
            // 
            // dgvManualShuffled
            // 
            dgvManualShuffled.AllowUserToAddRows = false;
            dgvManualShuffled.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvManualShuffled.Location = new Point(987, 111);
            dgvManualShuffled.Name = "dgvManualShuffled";
            dgvManualShuffled.ReadOnly = true;
            dgvManualShuffled.RowHeadersWidth = 51;
            dgvManualShuffled.Size = new Size(429, 393);
            dgvManualShuffled.TabIndex = 7;
            // 
            // dgvManualInput
            // 
            dgvManualInput.AllowUserToAddRows = false;
            dgvManualInput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvManualInput.Location = new Point(479, 111);
            dgvManualInput.Name = "dgvManualInput";
            dgvManualInput.RowHeadersWidth = 51;
            dgvManualInput.Size = new Size(425, 393);
            dgvManualInput.TabIndex = 6;
            // 
            // btnManualShuffle
            // 
            btnManualShuffle.Location = new Point(610, 530);
            btnManualShuffle.Name = "btnManualShuffle";
            btnManualShuffle.Size = new Size(171, 29);
            btnManualShuffle.TabIndex = 5;
            btnManualShuffle.Text = "Zapisz i przetasuj";
            btnManualShuffle.UseVisualStyleBackColor = true;
            btnManualShuffle.Click += btnManualShuffle_Click;
            // 
            // btnManualCreate
            // 
            btnManualCreate.Location = new Point(101, 181);
            btnManualCreate.Name = "btnManualCreate";
            btnManualCreate.Size = new Size(173, 29);
            btnManualCreate.TabIndex = 4;
            btnManualCreate.Text = "Utwórz pustą siatkę";
            btnManualCreate.UseVisualStyleBackColor = true;
            btnManualCreate.Click += btnManualCreate_Click;
            // 
            // manual_col_num
            // 
            manual_col_num.Location = new Point(185, 111);
            manual_col_num.Maximum = new decimal(new int[] { -1981284352, -1966660860, 0, 0 });
            manual_col_num.Name = "manual_col_num";
            manual_col_num.Size = new Size(150, 27);
            manual_col_num.TabIndex = 3;
            // 
            // manual_row_num
            // 
            manual_row_num.Location = new Point(185, 56);
            manual_row_num.Maximum = new decimal(new int[] { -1981284352, -1966660860, 0, 0 });
            manual_row_num.Name = "manual_row_num";
            manual_row_num.Size = new Size(150, 27);
            manual_row_num.TabIndex = 2;
            // 
            // manual_col
            // 
            manual_col.AutoSize = true;
            manual_col.Location = new Point(45, 111);
            manual_col.Name = "manual_col";
            manual_col.Size = new Size(104, 20);
            manual_col.TabIndex = 1;
            manual_col.Text = "Liczba kolumn";
            // 
            // manual_row
            // 
            manual_row.AutoSize = true;
            manual_row.Location = new Point(45, 56);
            manual_row.Name = "manual_row";
            manual_row.Size = new Size(103, 20);
            manual_row.TabIndex = 0;
            manual_row.Text = "Liczba wierszy";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnStop);
            tabPage2.Controls.Add(btnStart);
            tabPage2.Controls.Add(mutation);
            tabPage2.Controls.Add(iteration);
            tabPage2.Controls.Add(population);
            tabPage2.Controls.Add(mutation_num);
            tabPage2.Controls.Add(iterate_num);
            tabPage2.Controls.Add(pupulation_num);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1467, 687);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Metaheurystyka";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(168, 261);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(94, 29);
            btnStop.TabIndex = 9;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(46, 261);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 8;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // mutation
            // 
            mutation.AutoSize = true;
            mutation.Location = new Point(40, 140);
            mutation.Name = "mutation";
            mutation.Size = new Size(208, 20);
            mutation.TabIndex = 6;
            mutation.Text = "Prawdopodobieństwo mutacji";
            // 
            // iteration
            // 
            iteration.AutoSize = true;
            iteration.Location = new Point(40, 89);
            iteration.Name = "iteration";
            iteration.Size = new Size(100, 20);
            iteration.TabIndex = 5;
            iteration.Text = "Liczba iteracji";
            // 
            // population
            // 
            population.AutoSize = true;
            population.Location = new Point(40, 44);
            population.Name = "population";
            population.Size = new Size(130, 20);
            population.TabIndex = 4;
            population.Text = "Rozmiar populacji";
            // 
            // mutation_num
            // 
            mutation_num.Location = new Point(296, 133);
            mutation_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            mutation_num.Name = "mutation_num";
            mutation_num.Size = new Size(150, 27);
            mutation_num.TabIndex = 2;
            mutation_num.ValueChanged += numericUpDown7_ValueChanged;
            // 
            // iterate_num
            // 
            iterate_num.Location = new Point(296, 87);
            iterate_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            iterate_num.Name = "iterate_num";
            iterate_num.Size = new Size(150, 27);
            iterate_num.TabIndex = 1;
            // 
            // pupulation_num
            // 
            pupulation_num.Location = new Point(296, 42);
            pupulation_num.Maximum = new decimal(new int[] { -159383552, 46653770, 5421, 0 });
            pupulation_num.Name = "pupulation_num";
            pupulation_num.Size = new Size(150, 27);
            pupulation_num.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(plotResults);
            tabPage3.Controls.Add(console);
            tabPage3.Controls.Add(progress);
            tabPage3.Controls.Add(progressBar);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1467, 687);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Wyniki";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // plotResults
            // 
            plotResults.Location = new Point(1058, 79);
            plotResults.Name = "plotResults";
            plotResults.Size = new Size(387, 372);
            plotResults.TabIndex = 3;
            // 
            // console
            // 
            console.Location = new Point(65, 79);
            console.Multiline = true;
            console.Name = "console";
            console.ScrollBars = ScrollBars.Vertical;
            console.Size = new Size(973, 550);
            console.TabIndex = 2;
            console.TextChanged += console_TextChanged;
            // 
            // progress
            // 
            progress.AutoSize = true;
            progress.Location = new Point(65, 33);
            progress.Name = "progress";
            progress.Size = new Size(56, 20);
            progress.TabIndex = 1;
            progress.Text = "Postęp:";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(127, 33);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(125, 29);
            progressBar.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1475, 720);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)error_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)percent_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)col_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)row_num).EndInit();
            tabPageManual.ResumeLayout(false);
            tabPageManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvManualShuffled).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvManualInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)manual_col_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)manual_row_num).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mutation_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)iterate_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)pupulation_num).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label1;
        private NumericUpDown error_num;
        private NumericUpDown percent_num;
        private NumericUpDown col_num;
        private NumericUpDown row_num;
        private Label error;
        private Label ones;
        private Label col;
        private Label row;
        private TextBox Matrix_output;
        private Button Button_matrix;
        private Label mutation;
        private Label iteration;
        private Label population;
        private NumericUpDown mutation_num;
        private NumericUpDown iterate_num;
        private NumericUpDown pupulation_num;
        private Button btnStop;
        private Button btnStart;
        private Label progress;
        private ProgressBar progressBar;
        private TextBox console;
        private ScottPlot.WinForms.FormsPlot plotResults;
        private TabPage tabPageManual;
        private Button btnManualShuffle;
        private Button btnManualCreate;
        private NumericUpDown manual_col_num;
        private NumericUpDown manual_row_num;
        private Label manual_col;
        private Label manual_row;
        private DataGridView dgvManualShuffled;
        private DataGridView dgvManualInput;
    }
}