namespace GA5
{
    public class Context
    {
        public double PopulationSize { get; set; }
        public int N { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Variance { get; set; }

        public Context(double populationSize, int n, double maxValue, double minValue, double variance)
        {
            PopulationSize = populationSize;
            N = n;
            MaxValue = maxValue;
            MinValue = minValue;
            Variance = variance;
        }

        public double GetRandom() => Random.Shared.NextDouble() * (MaxValue - MinValue) + MinValue;
    }
}