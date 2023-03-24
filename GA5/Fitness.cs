using GA5.GA;

namespace GA5
{
    public class Fitness : IFitness<Chromosome, double>
    {
        public double Calculate(Chromosome chromosome)
        {
            double f(List<double> xs)
            {
                double sum = 0;
                foreach (var x in xs)
                    sum += Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x);
                return 10d * xs.Count + sum;
            }

            return f(chromosome.Xs);
        }
    }
}
