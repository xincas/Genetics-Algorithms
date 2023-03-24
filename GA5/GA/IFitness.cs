namespace GA5.GA
{
    public interface IFitness<C, T> where C : IChromosome where T : IComparable
    {
        T Calculate(C chromosome);
    }
}
