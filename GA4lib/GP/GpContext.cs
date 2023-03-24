namespace GA4lib.GP
{
    public class GpContext
    {
        public int PopulationSize { get; init; }
        public Dictionary<string, double> Variables { get; init; }
        public double PCrossover { get; init; }
        public double PMutation { get; init; }
        public double MaxValue { get; init; }
        public double MinValue { get; init; }
        public double MaxMutationValue { get; init; }
        public double MinMutationValue { get; init; }
        public int MaxDepth { get; init; }
        public double GetRandomValue() => Random.Shared.NextDouble() * (MaxValue - MinValue) + MinValue;
        public double GetRandomMutationValue() => Random.Shared.NextDouble() * (MaxMutationValue - MinMutationValue) + MinMutationValue;
        public GpContext(List<string> variables, int populationSize, double pCrossover, double pMutation, double maxValue, double minValue, double maxMutationValue, double minMutationValue, int maxDepth)
        {
            Variables = variables.ToDictionary(m => m, _ => 0d);
            PopulationSize = populationSize;
            PCrossover = pCrossover;
            PMutation = pMutation;
            MaxValue = maxValue;
            MinValue = minValue;
            MaxMutationValue = maxMutationValue;
            MinMutationValue = minMutationValue;
            MaxDepth = maxDepth;
        }
    }
}
