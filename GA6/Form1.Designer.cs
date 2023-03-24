namespace GA6
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
            this.label10 = new System.Windows.Forms.Label();
            this.l_time = new System.Windows.Forms.Label();
            this.bVisibleBest = new System.Windows.Forms.Button();
            this.bVisibleBestGA = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lBestWay = new System.Windows.Forms.Label();
            this.lBestWayGa = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lWay = new System.Windows.Forms.Label();
            this.bVisiblePoints = new System.Windows.Forms.Button();
            this.tB_Alpha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tB_Beta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tB_Rho = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bNewWin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(10, 10);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(564, 465);
            this.cartesianChart1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 27);
            this.button1.TabIndex = 9;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(680, 456);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(68, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(601, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "Generation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(579, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 99;
            this.label2.Text = "Max generations:";
            // 
            // tB_maxGen
            // 
            this.tB_maxGen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tB_maxGen.Location = new System.Drawing.Point(683, 29);
            this.tB_maxGen.Name = "tB_maxGen";
            this.tB_maxGen.Size = new System.Drawing.Size(61, 21);
            this.tB_maxGen.TabIndex = 1;
            this.tB_maxGen.Text = "100";
            // 
            // tB_popSize
            // 
            this.tB_popSize.Location = new System.Drawing.Point(683, 54);
            this.tB_popSize.Name = "tB_popSize";
            this.tB_popSize.Size = new System.Drawing.Size(61, 21);
            this.tB_popSize.TabIndex = 2;
            this.tB_popSize.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(586, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 88;
            this.label3.Text = "Population size:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(639, 437);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 16);
            this.label10.TabIndex = 100;
            this.label10.Text = "Time:";
            // 
            // l_time
            // 
            this.l_time.AutoSize = true;
            this.l_time.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.l_time.Location = new System.Drawing.Point(680, 437);
            this.l_time.Name = "l_time";
            this.l_time.Size = new System.Drawing.Size(0, 16);
            this.l_time.TabIndex = 101;
            // 
            // bVisibleBest
            // 
            this.bVisibleBest.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bVisibleBest.Location = new System.Drawing.Point(606, 130);
            this.bVisibleBest.Name = "bVisibleBest";
            this.bVisibleBest.Size = new System.Drawing.Size(64, 27);
            this.bVisibleBest.TabIndex = 102;
            this.bVisibleBest.Text = "Visible Red";
            this.bVisibleBest.UseVisualStyleBackColor = true;
            this.bVisibleBest.Click += new System.EventHandler(this.bVisibleBest_Click);
            // 
            // bVisibleBestGA
            // 
            this.bVisibleBestGA.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bVisibleBestGA.Location = new System.Drawing.Point(675, 130);
            this.bVisibleBestGA.Name = "bVisibleBestGA";
            this.bVisibleBestGA.Size = new System.Drawing.Size(64, 27);
            this.bVisibleBestGA.TabIndex = 103;
            this.bVisibleBestGA.Text = "Visible Blue";
            this.bVisibleBestGA.UseVisualStyleBackColor = true;
            this.bVisibleBestGA.Click += new System.EventHandler(this.bVisibleBestGA_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(606, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 16);
            this.label4.TabIndex = 104;
            this.label4.Text = "Best known way distance:";
            // 
            // lBestWay
            // 
            this.lBestWay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lBestWay.Location = new System.Drawing.Point(606, 234);
            this.lBestWay.Name = "lBestWay";
            this.lBestWay.Size = new System.Drawing.Size(141, 16);
            this.lBestWay.TabIndex = 105;
            this.lBestWay.Text = "7544";
            this.lBestWay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lBestWayGa
            // 
            this.lBestWayGa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lBestWayGa.Location = new System.Drawing.Point(611, 270);
            this.lBestWayGa.Name = "lBestWayGa";
            this.lBestWayGa.Size = new System.Drawing.Size(136, 16);
            this.lBestWayGa.TabIndex = 107;
            this.lBestWayGa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(606, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 16);
            this.label8.TabIndex = 106;
            this.label8.Text = "Best GA way distance:";
            // 
            // lWay
            // 
            this.lWay.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lWay.Location = new System.Drawing.Point(611, 286);
            this.lWay.Name = "lWay";
            this.lWay.Size = new System.Drawing.Size(129, 147);
            this.lWay.TabIndex = 108;
            // 
            // bVisiblePoints
            // 
            this.bVisiblePoints.Location = new System.Drawing.Point(633, 159);
            this.bVisiblePoints.Name = "bVisiblePoints";
            this.bVisiblePoints.Size = new System.Drawing.Size(76, 27);
            this.bVisiblePoints.TabIndex = 109;
            this.bVisiblePoints.Text = "Visible Points";
            this.bVisiblePoints.UseVisualStyleBackColor = true;
            this.bVisiblePoints.Click += new System.EventHandler(this.bVisiblePoints_Click);
            // 
            // tB_Alpha
            // 
            this.tB_Alpha.Location = new System.Drawing.Point(633, 79);
            this.tB_Alpha.Name = "tB_Alpha";
            this.tB_Alpha.Size = new System.Drawing.Size(29, 21);
            this.tB_Alpha.TabIndex = 110;
            this.tB_Alpha.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(586, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 111;
            this.label5.Text = "Alpha: ";
            // 
            // tB_Beta
            // 
            this.tB_Beta.Location = new System.Drawing.Point(715, 79);
            this.tB_Beta.Name = "tB_Beta";
            this.tB_Beta.Size = new System.Drawing.Size(29, 21);
            this.tB_Beta.TabIndex = 112;
            this.tB_Beta.Text = "2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(671, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 113;
            this.label6.Text = "Beta: ";
            // 
            // tB_Rho
            // 
            this.tB_Rho.Location = new System.Drawing.Point(677, 105);
            this.tB_Rho.Name = "tB_Rho";
            this.tB_Rho.Size = new System.Drawing.Size(29, 21);
            this.tB_Rho.TabIndex = 114;
            this.tB_Rho.Text = "0,5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(633, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 16);
            this.label7.TabIndex = 115;
            this.label7.Text = "Rho: ";
            // 
            // bNewWin
            // 
            this.bNewWin.Location = new System.Drawing.Point(639, 0);
            this.bNewWin.Name = "bNewWin";
            this.bNewWin.Size = new System.Drawing.Size(64, 27);
            this.bNewWin.TabIndex = 116;
            this.bNewWin.Text = "Part 1";
            this.bNewWin.UseVisualStyleBackColor = true;
            this.bNewWin.Click += new System.EventHandler(this.bNewWin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 486);
            this.Controls.Add(this.bNewWin);
            this.Controls.Add(this.tB_Rho);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tB_Beta);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tB_Alpha);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bVisiblePoints);
            this.Controls.Add(this.lWay);
            this.Controls.Add(this.lBestWayGa);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lBestWay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bVisibleBestGA);
            this.Controls.Add(this.bVisibleBest);
            this.Controls.Add(this.l_time);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tB_popSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tB_maxGen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cartesianChart1);
            this.MaximumSize = new System.Drawing.Size(774, 525);
            this.MinimumSize = new System.Drawing.Size(774, 525);
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
        private Label label10;
        private Label l_time;
        private Button bVisibleBest;
        private Button bVisibleBestGA;
        private Label label4;
        private Label lBestWay;
        private Label lBestWayGa;
        private Label label8;
        private Label lWay;
        private Button bVisiblePoints;
        private TextBox tB_Alpha;
        private Label label5;
        private TextBox tB_Beta;
        private Label label6;
        private TextBox tB_Rho;
        private Label label7;
        private Button bNewWin;
    }
}