using GA5.GA;

namespace GA5
{
    public class Chromosome : IChromosome
    {
        private List<double> _xs;
        private List<double> _variance;
        private IFitness<Chromosome, double> _fitness;
        private Context _context;
        private int _successMutation = 0;
        private int _mutations = 0;
        private double Fi => (double)_successMutation / _mutations; 

        public double Fitness => _fitness.Calculate(this);
        public List<double> Xs { get => _xs; private set => _xs = value; }
        public List<double> Variance { get => _variance; private set => _variance = value; }
        public int SuccessMutation { get => _successMutation; set => _successMutation = value; }
        public int Mutations { get => _mutations; set => _mutations = value; }

        public Chromosome(List<double> xs, IFitness<Chromosome, double> fitness, Context context)
        {
            Xs = new(xs);
            _fitness = fitness;
            Variance = new List<double>();
            for (int i = 0; i < Xs.Count; i++)
                Variance.Add(context.Variance);
            _context = context;
        }

        private Chromosome(List<double> xs, List<double> variance, IFitness<Chromosome, double> fitness, Context context)
        {
            Xs = new(xs);
            _fitness = fitness;
            Variance = new(variance);
            _context = context;
        }

        public object Clone()
        {
            return new Chromosome(Xs, Variance, _fitness, _context) { _successMutation = this._successMutation, _mutations = this._mutations };
        }

        public List<IChromosome> Crossover(IChromosome another)
        {
            return new();
        }

        public IChromosome? Mutate()
        {
            Chromosome son = (Chromosome)Clone();
            for (int i = 0; i < Xs.Count; i++)
                son.Xs[i] += GaussRandom.Next(0, Variance[i]);

            _mutations++;
            if (son.Fitness < Fitness) _successMutation++;
            
            if (_mutations % 5 == 0)
            {
                if (Fi < 0.2) Variance = Variance.Select(it => it * 0.82).ToList();
                else if (Fi > 0.2) Variance = Variance.Select(it => it * 1.22).ToList();
            }

            son.SuccessMutation = SuccessMutation;
            son.Mutations = Mutations;
            son.Variance = new(Variance);

            return son;
        }

    }
}
