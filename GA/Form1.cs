using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GA
{
    public partial class Form1 : Form
    {
        List<GenerationInfo> results;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GAFitnessFunction fitness = function;

            int popSize, precision, maxGen;
            double cross, mutat, from, to, extremum;

            if(!int.TryParse(tB_popSize.Text, out popSize) ||
               !int.TryParse(tB_precision.Text, out precision) ||
               !int.TryParse(tB_maxGen.Text, out maxGen) || 
               !double.TryParse(tB_crossRate.Text, out cross) ||
               !double.TryParse(tB_mutationRate.Text, out mutat) ||
               !double.TryParse(tB_from.Text, out from) ||
               !double.TryParse(tB_to.Text, out to) ||
               !double.TryParse(tB_extremum.Text, out extremum)
            )
            {
                return;
            }

            GenerationAlgorithm solver = new GenerationAlgorithm
                (
                    popSize,
                    precision,
                    maxGen,
                    cross,
                    mutat,
                    fitness,
                    from,
                    to,
                    extremum,
                    Extremum.Max
                );
            Stopwatch timer = new();
            timer.Start();
            results = solver.Solve();
            timer.Stop();
            l_time.Text = timer.ElapsedMilliseconds + "ms";

            DrawPlot();
            InitComboBox(results.Count);
        }

        double function(double x)
        {
            return (x - 1) * Math.Cos(3 * x - 15);
        }

        void DrawPlot()
        {
            List<ObservablePoint> points = new();

            double dx = 0.1;
            for (double i = -10.0d; i < 10.0d; i += dx)
                points.Add(new ObservablePoint(i, function(i)));

            cartesianChart1.Series = new ISeries[]
            {
                new LineSeries<ObservablePoint>
                {
                    Values = points.ToArray(),
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 1 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,

                },
                new ScatterSeries<ObservablePoint>
                {
                    GeometrySize = 2.5
                }
            };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ObservablePoint> points2 = new();
            foreach (var p in results[comboBox1.SelectedIndex].points)
                points2.Add(new ObservablePoint(p.x, p.y));

            cartesianChart1.Series.ElementAt(1).Values = points2.ToArray();
        }

        void InitComboBox(int size)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < size; i++)
                comboBox1.Items.Add(i + 1);
            comboBox1.SelectedIndex = size - 1;
        }
    }
}