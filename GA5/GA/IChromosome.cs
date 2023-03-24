namespace GA5.GA
{
    public interface IChromosome : ICloneable
    {
        List<IChromosome> Crossover(IChromosome another);
        IChromosome? Mutate();
    }
}
