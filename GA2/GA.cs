using System.Text;

namespace GA2
{
    internal static class Rand
    {
        public static Random GetRand { get; } = new();
    }
    public delegate double GAFunction(params double[] xs);
    public class Point
    {
        public double Y { get; set; }
        public double[] Xs { get; set; }
        public Point(double y, double[] xs)
        {
            Y = y;
            Xs = xs;
        }
        public Point(double[] xs)
        {
            Y = 0;
            Xs = xs;
        }
        public Point(int varSize, DoubleRange range)
        {
            Y = 0;
            Xs = new double[varSize];
            for (int i = 0; i < varSize; ++i)
                Xs[i] = range.Min + (range.Max - range.Min) * Rand.GetRand.NextDouble();
        }
        public Point(Point point)
        {
            Y = point.Y;
            Xs = new double[point.Xs.Length];
            point.Xs.CopyTo(Xs, 0);
        }
        public void Mutation(int gen, int maxGen)
        {
            const double b = 2;
            int i = Rand.GetRand.Next(Xs.Length);
            double delta = Math.Abs(Xs[i]) * (1 - Math.Pow(Rand.GetRand.NextDouble(), Math.Pow(1 - gen / maxGen, b)));
            delta *= Rand.GetRand.Next(0, 2) == 0 ? 1 : -1;
            Xs[i] = Xs[i] + delta;
        }
        public override string? ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (var x in Xs)
                stringBuilder.Append($"{x}\t");
            return $"{stringBuilder.ToString()}{Y}";
        }
    }
    public class GenInfo
    {
        public int Index { get; init; }
        public List<Point> Points { get; init; }
        public GenInfo(int index, List<Point> points)
        {
            Index = index;
            Points = new List<Point>();
            foreach (var point in points)
                Points.Add(new Point(point));
        }
    }
    public struct DoubleRange
    {
        public double Min { get; init; }
        public double Max { get; init; }
        public DoubleRange(double min, double max)
        {
            Min = min;
            Max = max;
        }
    }
    public class GASolver
    {
        readonly GAFunction FitnessFunction;
        readonly int VARIABLE_SIZE;
        readonly DoubleRange Range;
        readonly bool isMax;

        readonly int POPULATION_SIZE;
        readonly int MAX_GEN;
        readonly double CROSS_RATE;
        readonly double MUTATION_RATE;
        readonly int TOURNAMENT_SIZE;
        readonly int MAX_REPEATE;

        List<Point> population;
        int currentGen;

        public GASolver(GAFunction fitnessFunction, int vARIABLE_SIZE, DoubleRange range, int pOPULATION_SIZE, int mAX_GEN, double cROSS_RATE, double mUTATION_RATE, bool isMax = true, int tOURNAMENT_SIZE = 3, int mAX_REPEATE = 25)
        {
            FitnessFunction = fitnessFunction;
            VARIABLE_SIZE = vARIABLE_SIZE;
            Range = range;
            POPULATION_SIZE = pOPULATION_SIZE;
            MAX_GEN = mAX_GEN;
            CROSS_RATE = cROSS_RATE;
            MUTATION_RATE = mUTATION_RATE;
            this.isMax = isMax;
            TOURNAMENT_SIZE = tOURNAMENT_SIZE;
            MAX_REPEATE = mAX_REPEATE;

            population = new List<Point>();
            currentGen = 1;
        }

        public List<GenInfo> Solve()
        {
            currentGen = 0;
            PopulationInit();
            var info = new List<GenInfo>();

            double lastExtremum = 0;
            int i = 0;
            while (currentGen != MAX_GEN && i != MAX_REPEATE)
            {
                population.ForEach(it => it.Y = FitnessFunction(it.Xs));
                population.Sort((x, y) => x.Y.CompareTo(y.Y));
                if (isMax) population.Reverse();

                info.Add(new GenInfo(++currentGen, population));

                double curExtremum = Extremum(population);
                if (Math.Round(lastExtremum, 5) == Math.Round(curExtremum, 5)) i++;
                else { lastExtremum = curExtremum; i = 0; }

                population = Selection();
                Cross();
                Mutation();
            }

            return info;
        }

        private List<Point> Selection()
        {
            List<Point> newPopulation = new();
            double ex = Extremum(population);
            newPopulation.Add(new Point(population.Find(it => ex == FitnessFunction(it.Xs))!));
            for (int i = 0; i < POPULATION_SIZE - 1; ++i)
            {
                List<int> indexs = new();
                do
                {
                    indexs.Clear();
                    for (int j = 0; j < TOURNAMENT_SIZE; j++)
                        indexs.Add(Rand.GetRand.Next(POPULATION_SIZE));
                } while (indexs.Distinct().Count() != 3);

                List<Point> inds = new();
                foreach (int index in indexs)
                    inds.Add(population[index]);
                double extremum = Extremum(inds);
                newPopulation.Add(new Point(inds.Find(it => extremum == FitnessFunction(it.Xs))!));
            }
            newPopulation.Sort((x, y) => x.Y.CompareTo(y.Y));
            if (isMax) newPopulation.Reverse();

            return newPopulation;
        }

        private double Extremum(List<Point> inds)
        {
            return (isMax) ?
                inds.Max(it => it.Y) :
                inds.Min(it => it.Y);
        }

        private void Cross()
        {
            const int n = 3; // [2, 5]

            for (int i = 0; i < POPULATION_SIZE - 1; i+=2)
            {
                if (Rand.GetRand.NextDouble() < CROSS_RATE)
                {
                    double u = Rand.GetRand.NextDouble();
                    var beta = u <= 0.5 ? Math.Pow(2 * u, 1 / (n + 1)) : Math.Pow(0.5 * (1 - u), 1 / (n + 1));

                    double[] axs = new double[VARIABLE_SIZE];
                    double[] bxs = new double[VARIABLE_SIZE];
                    for (int j = 0; j < VARIABLE_SIZE; ++j)
                    {
                        axs[j] = 0.5 * ((1 - beta) * population[i].Xs[j] + (1 + beta) * population[i + 1].Xs[j]);
                        bxs[j] = 0.5 * ((1 - beta) * population[i + 1].Xs[j] + (1 + beta) * population[i].Xs[j]);
                    }

                    population[i].Xs = axs;
                    population[i + 1].Xs = bxs;
                }
            }
        }

        private void Mutation()
        {
            foreach (var p in population)
                if (Rand.GetRand.NextDouble() < MUTATION_RATE)
                    p.Mutation(currentGen, MAX_GEN);
        }

        private void PopulationInit()
        {
            population.Clear();
            for (int i = 0; i < POPULATION_SIZE; ++i) 
                population.Add(new Point(VARIABLE_SIZE, Range));
        }
    }
}