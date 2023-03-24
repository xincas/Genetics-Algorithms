using GA4lib.GA;

namespace GA4lib.GP
{
    public class GPEngine
    {
        private GAEngine<GpChromosome, double> _gaEngine;

        private GpContext _context;

        private IExpressionFitness _expressionFitness;

        public GPEngine(IExpressionFitness expressionFitness, List<string> variables)
        {
            _expressionFitness = expressionFitness;
            _context = new GpContext(
                variables: variables,
                populationSize: 100,
                pCrossover: 0.5,
                pMutation: 0.05,
                maxValue: 500,
                minValue: -500,
                maxMutationValue: 50,
                minMutationValue: -50,
                maxDepth: 6);
            SymbolicRegressionFitness fitnessFunction = new SymbolicRegressionFitness(expressionFitness);
            Population<GpChromosome> population = CreatePopulation(_context, fitnessFunction, _context.PopulationSize);
            _gaEngine = new(population, fitnessFunction);
        }

        private Population<GpChromosome> CreatePopulation(GpContext context, SymbolicRegressionFitness fitnessFunction, int defaultPopulationSize)
        {
            Population<GpChromosome> population = new();
            for (int i = 0; i < defaultPopulationSize; i++)
                population.Add(new GpChromosome(SyntaxTreeUtils.CreateTree(_context.MaxDepth, context), context, fitnessFunction));
            return population;
        }

        public void Evolve(int maxIteration) => _gaEngine.Evolve(maxIteration);

        public GpChromosome Best() => _gaEngine.Best();

        public double Fitness(Expression expression) => _expressionFitness.Fitness(expression, _context);

        public bool IsOptimum => _gaEngine.IsOptimal;

        public int Iteration => _gaEngine.Iteration;

        public void AddListener(GAEngine<GpChromosome, double>.GAListeners listener) => _gaEngine.AddListener(listener);

        private class SymbolicRegressionFitness : IFitness<GpChromosome, double>
        {
            private IExpressionFitness _expressionFitness;
            public SymbolicRegressionFitness(IExpressionFitness expressionFitness) => _expressionFitness = expressionFitness;
            public double Calculate(GpChromosome chromosome) => _expressionFitness.Fitness(chromosome.SyntaxTree, chromosome.Context);
        }
    }
}
