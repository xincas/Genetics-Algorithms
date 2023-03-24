using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GA
{
    public delegate double GAFitnessFunction(double x);

    public class Point
    {
        public double x;
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class GenerationInfo
    {
        public int generationIndex;
        public List<Point> points;

        public GenerationInfo(int generationIndex, List<Point> points)
        {
            this.generationIndex = generationIndex;
            this.points = points;
        }
    }
    public class Individual
    {
        BitArray chromosome;
        Random random = new Random();

        public BitArray Chromosome { get { return chromosome; } set { chromosome = value; } }
        public int Value { get { return ChromosomeToInt(); } }

        public Individual(int size)
        {
            chromosome = new BitArray(size);
            for (int i = 0; i < chromosome.Length; ++i)
                chromosome[i] = random.Next(0, 2) == 1;
        }

        public Individual(Individual ind)
        {
            chromosome = new BitArray(ind.chromosome);
        }

        public Individual(BitArray chromosome)
        {
            this.chromosome = chromosome;
        }

        public int ChromosomeToInt()
        {
            int[] array = new int[1];
            chromosome.CopyTo(array, 0);
            return array[0];
        }

        public void Mutation()
        {
            int i = random.Next(chromosome.Length);
            chromosome[i] = !chromosome[i];
        }
    }

    public enum Extremum
    {
        Max = 0,
        Min,
        Undefined
    }
    public class GenerationAlgorithm
    {
        int POPULATION_SIZE;
        int CHROMOSOME_SIZE;
        int MAX_GENERATION;
        int TOURNAMENT_SIZE;

        double CROSS_RATE;
        double MUTATION_RATE;

        List<Individual> population;
        int currentGeneration;

        GAFitnessFunction fitnessFunction;
        double FROM;
        double TO;
        Extremum type;
        double AIM;
        double PRECISION;

        Random random = new Random();

        public int PopulationSize
        {
            get { return POPULATION_SIZE; }
            private set { POPULATION_SIZE = value; }
        }

        public double MaxFitness { get { return population.Max(it => fitnessFunction(ActualValueOfX(it.Value))); } } 

        public GenerationAlgorithm(int pOPULATION_SIZE, int pRECISION, int mAX_GENERATION, double cROSS_RATE, double mUTATION_RATE, GAFitnessFunction fitnessFunction, double fROM, double tO, double aim, Extremum type, int tOURNAMENT_SIZE = 3)
        {
            POPULATION_SIZE = pOPULATION_SIZE;
            PRECISION = pRECISION;
            CHROMOSOME_SIZE = (int)Math.Ceiling(Math.Log2((tO - fROM) * pRECISION));
            MAX_GENERATION = mAX_GENERATION;
            CROSS_RATE = cROSS_RATE;
            MUTATION_RATE = mUTATION_RATE;
            this.fitnessFunction = fitnessFunction;
            FROM = fROM;
            TO = tO;
            this.type = type;
            AIM = aim;
            TOURNAMENT_SIZE = tOURNAMENT_SIZE;

            population = new();
            TOURNAMENT_SIZE = tOURNAMENT_SIZE;
        }

        private void InitPopulation()
        {
            population.Clear();
            for (int i = 0; i < PopulationSize; ++i)
            {
                Individual individual = new Individual(CHROMOSOME_SIZE);
                population.Add(individual);
            }
        }

        public List<GenerationInfo> Solve()
        {
            if (fitnessFunction == null)
                throw new ArgumentNullException();
            
            List<GenerationInfo> result = new();
            currentGeneration = 1;
            InitPopulation();

            
            while (currentGeneration <= MAX_GENERATION)
            {
                result.Add(GetGenerationInfo());

                population = Selection();
                Cross();
                Mutation();
                
                currentGeneration++;

                double extremum = (type == Extremum.Max) ? population.Max(it => fitnessFunction(ActualValueOfX(it.Value))) 
                    : population.Min(it => fitnessFunction(ActualValueOfX(it.Value)));
                if ((type == Extremum.Max && extremum >= AIM) || (type == Extremum.Min && extremum <= AIM))
                {
                    result.Add(GetGenerationInfo());
                    break;
                }
            }

            return result;
        }

        private GenerationInfo GetGenerationInfo()
        {
            List<Point> points = new();
            foreach (Individual individual in population)
                points.Add(new Point(ActualValueOfX(individual.Value), fitnessFunction(ActualValueOfX(individual.Value))));
            return new GenerationInfo(currentGeneration, points);
        }

        private void Cross()
        {
            for (int i = 0; i < PopulationSize - 1; i += 2)
            {
                if (random.NextDouble() < CROSS_RATE)
                {
                    int j = random.Next(CHROMOSOME_SIZE + 1);
                    BitArray orignA = new(population[i].Chromosome);
                    BitArray orignB = new(population[i + 1].Chromosome);
                    BitArray newA = new(orignA);
                    BitArray newB = new(orignB);

                    for (int k = j; k < CHROMOSOME_SIZE; k++)
                        newA[k] = orignB[k];

                    for (int k = j; k < CHROMOSOME_SIZE; k++)
                        newB[k] = orignB[k];

                    population[i].Chromosome = newA;
                    population[i+1].Chromosome = newB;
                }
            }
        }

        private List<Individual> Selection()
        {
            var newPopulation = new List<Individual>();

            for (int i = 0; i < PopulationSize; ++i)
            {
                List<int> indexes = new();
                do
                {
                    indexes.Clear();
                    for (int j = 0; j < TOURNAMENT_SIZE; j++)
                        indexes.Add(random.Next(PopulationSize));
                } while (indexes.Distinct().ToList().Count != 3) ;

                List<Individual> individuals = new List<Individual>();
                foreach (int index in indexes)
                    individuals.Add(population[index]);

                double extremum = (type == Extremum.Max) ? 
                    individuals.Max(it => fitnessFunction(ActualValueOfX(it.Value))) : 
                    individuals.Min(it => fitnessFunction(ActualValueOfX(it.Value)));
                newPopulation.Add(new Individual(individuals.Find(it => extremum == fitnessFunction(ActualValueOfX(it.Value)))));
            }

            return newPopulation;
        }

        void Mutation()
        {
            foreach (Individual individual in population)
                if (random.NextDouble() < MUTATION_RATE)
                    individual.Mutation();
        }

        double ActualValueOfX(int val)
        {
            return FROM + val * (TO - FROM) / (Math.Pow(2.0d, CHROMOSOME_SIZE) - 1);
        }
    }
}
