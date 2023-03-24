namespace GA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_maxGen = new System.Windows.Forms.TextBox();
            this.tB_popSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_precision = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_crossRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tB_mutationRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tB_from = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tB_to = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tB_extremum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.l_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 12);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(658, 537);
            this.cartesianChart1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(752, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 9;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(793, 526);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 23);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(707, 528);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 55;
            this.label1.Text = "Generation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(676, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 19);
            this.label2.TabIndex = 99;
            this.label2.Text = "Max generations:";
            // 
            // tB_maxGen
            // 
            this.tB_maxGen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tB_maxGen.Location = new System.Drawing.Point(793, 33);
            this.tB_maxGen.Name = "tB_maxGen";
            this.tB_maxGen.Size = new System.Drawing.Size(70, 23);
            this.tB_maxGen.TabIndex = 1;
            this.tB_maxGen.Text = "100";
            // 
            // tB_popSize
            // 
            this.tB_popSize.Location = new System.Drawing.Point(793, 62);
            this.tB_popSize.Name = "tB_popSize";
            this.tB_popSize.Size = new System.Drawing.Size(70, 23);
            this.tB_popSize.TabIndex = 2;
            this.tB_popSize.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(684, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 88;
            this.label3.Text = "Population size:";
            // 
            // tB_precision
            // 
            this.tB_precision.Location = new System.Drawing.Point(793, 91);
            this.tB_precision.Name = "tB_precision";
            this.tB_precision.Size = new System.Drawing.Size(70, 23);
            this.tB_precision.TabIndex = 3;
            this.tB_precision.Text = "1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(721, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 77;
            this.label4.Text = "Precision:";
            // 
            // tB_crossRate
            // 
            this.tB_crossRate.Location = new System.Drawing.Point(793, 120);
            this.tB_crossRate.Name = "tB_crossRate";
            this.tB_crossRate.Size = new System.Drawing.Size(70, 23);
            this.tB_crossRate.TabIndex = 4;
            this.tB_crossRate.Text = "0,5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(713, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 66;
            this.label5.Text = "Cross rate:";
            // 
            // tB_mutationRate
            // 
            this.tB_mutationRate.Location = new System.Drawing.Point(793, 149);
            this.tB_mutationRate.Name = "tB_mutationRate";
            this.tB_mutationRate.Size = new System.Drawing.Size(70, 23);
            this.tB_mutationRate.TabIndex = 5;
            this.tB_mutationRate.Text = "0,001";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(690, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 19);
            this.label6.TabIndex = 55;
            this.label6.Text = "Mutation rate:";
            // 
            // tB_from
            // 
            this.tB_from.Location = new System.Drawing.Point(726, 178);
            this.tB_from.Name = "tB_from";
            this.tB_from.Size = new System.Drawing.Size(46, 23);
            this.tB_from.TabIndex = 6;
            this.tB_from.Text = "-10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(676, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 19);
            this.label7.TabIndex = 44;
            this.label7.Text = "From:";
            // 
            // tB_to
            // 
            this.tB_to.Location = new System.Drawing.Point(817, 179);
            this.tB_to.Name = "tB_to";
            this.tB_to.Size = new System.Drawing.Size(46, 23);
            this.tB_to.TabIndex = 7;
            this.tB_to.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(785, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "To:";
            // 
            // tB_extremum
            // 
            this.tB_extremum.Location = new System.Drawing.Point(793, 208);
            this.tB_extremum.Name = "tB_extremum";
            this.tB_extremum.Size = new System.Drawing.Size(70, 23);
            this.tB_extremum.TabIndex = 8;
            this.tB_extremum.Text = "9,6";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(713, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 19);
            this.label9.TabIndex = 22;
            this.label9.Text = "Extremum:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(746, 490);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 19);
            this.label10.TabIndex = 100;
            this.label10.Text = "Time:";
            // 
            // l_time
            // 
            this.l_time.AutoSize = true;
            this.l_time.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.l_time.Location = new System.Drawing.Point(793, 490);
            this.l_time.Name = "l_time";
            this.l_time.Size = new System.Drawing.Size(0, 19);
            this.l_time.TabIndex = 101;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.l_time);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tB_extremum);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tB_to);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tB_from);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tB_mutationRate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tB_crossRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tB_precision);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tB_popSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tB_maxGen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cartesianChart1);
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private TextBox tB_maxGen;
        private TextBox tB_popSize;
        private Label label3;
        private TextBox tB_precision;
        private Label label4;
        private TextBox tB_crossRate;
        private Label label5;
        private TextBox tB_mutationRate;
        private Label label6;
        private TextBox tB_from;
        private Label label7;
        private TextBox tB_to;
        private Label label8;
        private TextBox tB_extremum;
        private Label label9;
        private Label label10;
        private Label l_time;
    }
}