using System.Diagnostics.CodeAnalysis;

namespace GA4lib.GA
{
    public class GAEngine<C, T> where C : class, IChromosome where T: IComparable
    {
        public Population<C> Population { get; private set; }

        private IFitness<C, T> _fitnessFunction;

        private ChromosomeComparator _chromosomeComparator;

        private GAListeners? _listeners;

        public GAEngine(Population<C> population, IFitness<C, T> fitnessFunction, GAListeners? listeners = null)
        {
            Population = population;
            _fitnessFunction = fitnessFunction;
            _chromosomeComparator = new ChromosomeComparator(_fitnessFunction);
            _listeners = listeners;
        }

        public bool IsOptimal { get; private set; }

        public void Terminate() => IsOptimal = true;

        public int Iteration { get; private set; }

        public void Evolve()
        {
            int parentPopulationSize = Population.Count;
            Population<C> newPopulation = new();

            newPopulation.Chromosomes.AddRange(Population.Chromosomes);

            for (int i = 0; i < parentPopulationSize; ++i)
            {
                C chromosome = Population.Chromosomes[i];
                C? mutated = (C?)chromosome.Mutate();

                C otherChromosome = Population.RandomChromosome();
                var crossed = chromosome.Crossover(otherChromosome).Select(it => (C)it);

                if (mutated is not null)
                    newPopulation.Add(mutated);
                foreach (var c in crossed)
                    newPopulation.Add(c);
            }

            newPopulation.Sort(_chromosomeComparator);
            newPopulation.Distinct(_chromosomeComparator);
            newPopulation.Trim(parentPopulationSize);
            Population = newPopulation;
        }

        public void Evolve(int maxIteration)
        {
            IsOptimal = false;

            for (Iteration = 0; Iteration < maxIteration; ++Iteration)
            {
                if (IsOptimal) return;

                Evolve();
                _listeners?.Invoke(this);
            }
        }

        public C Best() => Population.Chromosomes.First();

        public C Worst() => Population.Chromosomes.Last();

        public T Fitness(C chromosome) => _fitnessFunction.Calculate(chromosome);
        
        public delegate void GAListeners(GAEngine<C, T> engine);

        public void AddListener(GAListeners listener) => _listeners += listener;

        private class ChromosomeComparator : IComparer<C>, IEqualityComparer<C>
        {
            private IFitness<C, T> _fitnessFunction;

            public ChromosomeComparator(IFitness<C, T> fitnessFunction)
            {
                _fitnessFunction = fitnessFunction;
            }

            public int Compare(C? x, C? y)
            {
                T fit1 = _fitnessFunction.Calculate(x!);
                T fit2 = _fitnessFunction.Calculate(y!);

                return fit1.CompareTo(fit2);
            }

            public bool Equals(C? x, C? y)
            {
                return _fitnessFunction.Calculate(x!).CompareTo(_fitnessFunction.Calculate(y!)) == 0;
            }

            public int GetHashCode([DisallowNull] C obj)
            {
                return _fitnessFunction.Calculate(obj).GetHashCode();
            }
        }
    }
}
