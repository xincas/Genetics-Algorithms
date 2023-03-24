using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Diagnostics;
using LiveChartsCore.Measure;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;

namespace GA6
{
    public partial class Form1 : Form
    {
        private bool _kkk;
        private bool _part2;
        private List<Point> _towns;
        private List<int[]> _waysResult;
        private List<double> _distancesResult;
        private int _indexOfBest;

        Matrix<double> _distances;
        int[] _optimum;

        public Form1()
        {
            InitializeComponent();
            _towns = new()
            {
                new Point(565, 575),
                new Point(25, 185),
                new Point(345, 750),
                new Point(945, 685),
                new Point(845, 655),
                new Point(880, 660),
                new Point(25, 230),
                new Point(525, 1000),
                new Point(580, 1175),
                new Point(650, 1130),
                new Point(1605, 620),
                new Point(1220, 580),
                new Point(1465, 200),
                new Point(1530, 5),
                new Point(845, 680),
                new Point(725, 370),
                new Point(145, 665),
                new Point(415, 635),
                new Point(510, 875),
                new Point(560, 365),
                new Point(300, 465),
                new Point(520, 585),
                new Point(480, 415),
                new Point(835, 625),
                new Point(975, 580),
                new Point(1215, 245),
                new Point(1320, 315),
                new Point(1250, 400),
                new Point(660, 180),
                new Point(410, 250),
                new Point(420, 555),
                new Point(575, 665),
                new Point(1150, 1160),
                new Point(700, 580),
                new Point(685, 595),
                new Point(685, 610),
                new Point(770, 610),
                new Point(795, 645),
                new Point(720, 635),
                new Point(760, 650),
                new Point(475, 960),
                new Point(95, 260),
                new Point(875, 920),
                new Point(700, 500),
                new Point(555, 815),
                new Point(830, 485),
                new Point(1170, 65),
                new Point(830, 610),
                new Point(605, 625),
                new Point(595, 360),
                new Point(1340, 725),
                new Point(1740, 245),
            };
            _distances = CreateDistanceMatrix(_towns);
            _optimum = new[]
            {
                0, 48, 31, 44, 18, 40, 7, 8, 9, 42, 32, 50, 10, 51, 13, 12, 46, 25,
                26, 27, 11, 24, 3, 5, 14, 4, 23, 47, 37, 36, 39, 38, 35, 34, 33, 43, 45,
                15, 28, 49, 19, 22, 29, 1, 6, 41, 20, 16, 2, 17, 30, 21
            };
            _part2 = true;
        }

        private Matrix<double> CreateDistanceMatrix(List<Point> towns)
        {
            double[,] distances = new double[towns.Count, towns.Count];

            for (int i = 0; i < towns.Count; i++)
                for (int j = 0; j < towns.Count; j++)
                    distances[i, j] = Distance.Euclidean(
                        new double[] { towns[i].X, towns[i].Y },
                        new double[] { towns[j].X, towns[j].Y });

            return Matrix<double>.Build.DenseOfArray(distances);
        }

        private double DistanceFunc(Vector<double> w, Matrix<double> dist)
        {
            double distance = 0;
            var way = w.Select(it => (int)it).ToArray();
            for (int i = 0; i < way.Length - 1; i++)
                distance += dist[way[i], way[i + 1]];
            distance += dist[way.First(), way.Last()];
            return distance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int popSize, maxGen;
            double alpha, beta, rho;

            if (!int.TryParse(tB_popSize.Text, out popSize) ||
                !int.TryParse(tB_maxGen.Text, out maxGen) ||
                !double.TryParse(tB_Alpha.Text, out alpha) ||
                !double.TryParse(tB_Beta.Text, out beta) ||
                !double.TryParse(tB_Rho.Text, out rho)
               )
            {
                return;
            }

            var antEngine = new AntColonyOptimizator(
                DistanceFunc, 
                _distances, 
                popSize, 
                maxGen,
                alpha,
                beta,
                rho);

            Stopwatch timer = new();
            timer.Start();
            antEngine.Run();
            timer.Stop();
            l_time.Text = timer.ElapsedMilliseconds + "ms";

            _indexOfBest = antEngine.GenerationsBestWayDistance.IndexOf(antEngine.GenerationsBestWayDistance.Min());
            _waysResult = antEngine.GenerationsBestWay;
            _distancesResult = antEngine.GenerationsBestWayDistance;

            DrawPlot();
            InitComboBox(_waysResult.Count);
        }

        List<ObservablePoint> WayToPoints(int[] way)
        {
            List<ObservablePoint> res = new List<ObservablePoint>();

            foreach (var i in way)
                res.Add(new(_towns[i].X, _towns[i].Y));
            res.Add(new(_towns[way[0]].X, _towns[way[0]].Y));

            return res;
        }

        void DrawPlot()
        {
            _kkk = false;

            cartesianChart1.ZoomMode = ZoomAndPanMode.Both;
            cartesianChart1.TooltipPosition = TooltipPosition.Hidden;
            cartesianChart1.Series = new ISeries[]
            {
                new LineSeries<ObservablePoint>
                {
                    Values = WayToPoints(_optimum).ToArray(),
                    Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 1 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0,
                },
                new LineSeries<ObservablePoint>
                {
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 1 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0,
                },
                new ScatterSeries<ObservablePoint>
                {
                    Values = _towns.Select(it => new ObservablePoint(it.X, it.Y)).ToArray(),
                    Stroke = new SolidColorPaint(SKColors.DarkGreen),
                    Fill = null,
                    GeometrySize = 5,
                    DataLabelsSize = 10,
                    DataLabelsPaint = new SolidColorPaint(SKColors.DarkGreen),
                    DataLabelsPosition = DataLabelsPosition.Left,
                    DataLabelsFormatter = (point) =>
                    {
                        var index = _towns.IndexOf(new Point((int)point.SecondaryValue, (int)point.PrimaryValue));
                        return index != -1 ? index.ToString() : "???";
                    }
                }
            };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            List<ObservablePoint> points2 = WayToPoints(_waysResult[index]);
            lBestWayGa.Text = $"{_distancesResult[index]}";
            lWay.Text = $"{string.Join(" -> ", _waysResult[index])}";
            cartesianChart1.Series.ElementAt(1).Values = points2.ToArray();
        }

        void InitComboBox(int size)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < size; i++)
                comboBox1.Items.Add(i + 1);
            comboBox1.SelectedIndex = _indexOfBest;
        }

        private void bVisibleBestGA_Click(object sender, EventArgs e)
        {
            try
            {
                cartesianChart1.Series.ElementAt(1).IsVisible = !cartesianChart1.Series.ElementAt(1).IsVisible;
            }
            catch
            {
                return;
            }
        }
        
        private void bVisibleBest_Click(object sender, EventArgs e)
        {
            try
            {
                cartesianChart1.Series.ElementAt(0).IsVisible = !cartesianChart1.Series.ElementAt(0).IsVisible;
            }
            catch
            {
                return;
            }
        }

        private void bVisiblePoints_Click(object sender, EventArgs e)
        {
            try
            {
                var series = (ScatterSeries<ObservablePoint>)cartesianChart1.Series.ElementAt(2);
                series.DataLabelsSize = (_kkk = !_kkk) ? 0 : 10;
            }
            catch
            {
                return;
            }
        }

        private void bNewWin_Click(object sender, EventArgs e)
        {
            _part2 = !_part2;
            if (_part2)
            {
                _towns = new()
            {
                new Point(565, 575),
                new Point(25, 185),
                new Point(345, 750),
                new Point(945, 685),
                new Point(845, 655),
                new Point(880, 660),
                new Point(25, 230),
                new Point(525, 1000),
                new Point(580, 1175),
                new Point(650, 1130),
                new Point(1605, 620),
                new Point(1220, 580),
                new Point(1465, 200),
                new Point(1530, 5),
                new Point(845, 680),
                new Point(725, 370),
                new Point(145, 665),
                new Point(415, 635),
                new Point(510, 875),
                new Point(560, 365),
                new Point(300, 465),
                new Point(520, 585),
                new Point(480, 415),
                new Point(835, 625),
                new Point(975, 580),
                new Point(1215, 245),
                new Point(1320, 315),
                new Point(1250, 400),
                new Point(660, 180),
                new Point(410, 250),
                new Point(420, 555),
                new Point(575, 665),
                new Point(1150, 1160),
                new Point(700, 580),
                new Point(685, 595),
                new Point(685, 610),
                new Point(770, 610),
                new Point(795, 645),
                new Point(720, 635),
                new Point(760, 650),
                new Point(475, 960),
                new Point(95, 260),
                new Point(875, 920),
                new Point(700, 500),
                new Point(555, 815),
                new Point(830, 485),
                new Point(1170, 65),
                new Point(830, 610),
                new Point(605, 625),
                new Point(595, 360),
                new Point(1340, 725),
                new Point(1740, 245),
            };
                _distances = CreateDistanceMatrix(_towns);
                _optimum = new[]
                {
                    0, 48, 31, 44, 18, 40, 7, 8, 9, 42, 32, 50, 10, 51, 13, 12, 46, 25,
                    26, 27, 11, 24, 3, 5, 14, 4, 23, 47, 37, 36, 39, 38, 35, 34, 33, 43, 45,
                    15, 28, 49, 19, 22, 29, 1, 6, 41, 20, 16, 2, 17, 30, 21
                };
                bNewWin.Text = "Part 1";
                lBestWay.Text = "7544";
            }
            else
            {
                double[,] m = new double[,]
                {
                   // f  s  o  al b  at d
                    { 0, 1, 0, 4, 5, 0, 0 }, // f
                    { 0, 0, 1, 0, 0, 0, 2 }, // s
                    { 0, 0, 0, 1, 0, 2, 0 }, // o
                    { 0, 2, 0, 0, 0, 0, 3 }, // al
                    { 0, 0, 0, 0, 0, 0, 0 }, // b
                    { 0, 0, 0, 2, 1, 0, 1 }, // at
                    { 0, 2, 0, 3, 0, 0, 0 }  // d
                };
                _towns = new()
                {
                    new Point(500, 500),
                    new Point(525, 525),
                    new Point(550, 550),
                    new Point(575, 525),
                    new Point(600, 500),
                    new Point(570, 475),
                    new Point(530, 475)
                };
                _distances = Matrix<double>.Build.DenseOfArray(m);
                _optimum = new[]
                {
                    0, 3, 6, 1, 2, 5, 4
                };
                bNewWin.Text = "Part 2";
                lBestWay.Text = "18";
            }
        }
    }
}