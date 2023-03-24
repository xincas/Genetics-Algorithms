using System.Collections;

namespace GA5.GA
{
    public class Population<C> : IEnumerable where C : IChromosome
    {
        private static readonly int DEFAULT_SIZE = 32;
        public List<C> Chromosomes { get; private set; } = new(DEFAULT_SIZE);
        public int Count => Chromosomes.Count;
        public void Add(C chromosome) => Chromosomes.Add(chromosome);
        public C RandomChromosome() => Chromosomes[Random.Shared.Next(0, Chromosomes.Count)];
        public void Sort(IComparer<C> chComparer) => Chromosomes.Sort(chComparer);
        public void Distinct(IEqualityComparer<C> chComparer) => Chromosomes = Chromosomes.Distinct(chComparer).ToList();
        public void Trim(int len)
        {
            if (len < Count)
                Chromosomes = Chromosomes.GetRange(0, len);
        }

        public IEnumerator GetEnumerator() => Chromosomes.GetEnumerator();
    }
}
