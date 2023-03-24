namespace GA4lib.GA
{
    public interface IChromosome : ICloneable
    {
        List<IChromosome> Crossover(IChromosome another);
        IChromosome? Mutate();
    }
}
