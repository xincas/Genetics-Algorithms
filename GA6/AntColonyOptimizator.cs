using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

namespace GA6
{
    public class AntColonyOptimizator
    {
        public int PopulationSize { get; private set; }
        public Func<Vector<double>, Matrix<double>, double> Func { get; private set; }
        public int TownSize => DistanceMatrix.RowCount;
        public int MaxIterations { get; private set; }
        public double Alpha { get; private set; }
        public double Beta { get; private set; }
        public double Rho { get; private set; }
        public Matrix<double> DistanceMatrix { get; private set; }
        public List<int[]> GenerationsBestWay { get; private set; }
        public List<double> GenerationsBestWayDistance { get; private set; }
        private Matrix<double> PropobalityMatrix { get; set; }
        private Matrix<double> Tau { get; set; }
        private Matrix<double> Table { get; set; }

        public AntColonyOptimizator(Func<Vector<double>, Matrix<double>, double> func, Matrix<double> distanceMatrix, int populationSize = 25, int maxIterations = 25, double alpha = 1, double beta = 2, double rho = 0.5)
        {
            Func = func;
            DistanceMatrix = distanceMatrix;
            PopulationSize = populationSize;
            MaxIterations = maxIterations;
            Alpha = alpha;
            Beta = beta;
            Rho = rho;

            PropobalityMatrix = Matrix<double>.Build.DenseIdentity(TownSize, TownSize).Multiply(1e10).Add(DistanceMatrix).PointwisePower(-1);
            Tau = Matrix<double>.Build.Dense(TownSize, TownSize, 1);
            Table = Matrix<double>.Build.Dense(PopulationSize, TownSize, 0);

            GenerationsBestWay = new(MaxIterations);
            GenerationsBestWayDistance = new(MaxIterations);
        }

        public void Run()
        {
            for (int currentIteration = 0; currentIteration < MaxIterations; currentIteration++)
            {
                var propobalityMatrix = Tau.PointwisePower(Alpha).PointwiseMultiply(PropobalityMatrix.PointwisePower(Beta));
                for (int antIndex = 0; antIndex < PopulationSize; antIndex++)
                {
                    bool isWayExist = true;
                    int tryNumber = 0;
                    do
                    {
                        Table[antIndex, 0] = Random.Shared.Next(TownSize);
                        for (int k = 0; k < TownSize - 1; k++)
                        {
                            var tabooSet = Table.Row(antIndex).Take(k + 1).ToHashSet();
                            var allowSet = Enumerable.Range(0, TownSize).Select(it => (double)it).ToHashSet();
                            allowSet.ExceptWith(tabooSet);

                            var propobility = propobalityMatrix.Row((int)Table[antIndex, k]).Where((el, i) => allowSet.Contains(i));
                            var sum = propobility.Where(it => it != double.PositiveInfinity).Sum();
                            propobility = propobility.Select(it => it / sum);
                            var nextTown = WeightedRandom(allowSet, propobility, ref isWayExist);
                            if (!isWayExist)
                            {
                                Table.SetRow(antIndex, new double[TownSize]);
                                break;
                            }
                            Table[antIndex, k + 1] = nextTown;
                        }
                        if (tryNumber++ == 100000) throw new ArgumentException("Can't find Hamiltonian path");
                    } while (!isWayExist);
                    
                }

                var antsWayDistances = AntsWayDistances(Table);
                var indexBest = antsWayDistances.RowSums().MinimumIndex();
                GenerationsBestWay.Add(Table.Row(indexBest).Select(it => (int)it).ToArray());
                GenerationsBestWayDistance.Add(antsWayDistances[indexBest, 0]);

                var delataTau = Matrix<double>.Build.Dense(TownSize, TownSize, 0);
                for (int antIndex = 0; antIndex < PopulationSize; antIndex++)
                {
                    for (int k = 0; k < TownSize - 1; k++)
                    {
                        var town1 = (int)Table[antIndex, k];
                        var town2 = (int)Table[antIndex, k + 1];
                        delataTau[town1, town2] += 1 / antsWayDistances[antIndex, 0];
                    }
                    var t1 = (int)Table[antIndex, 0];
                    var t2 = (int)Table[antIndex, TownSize - 1];
                    delataTau[t1, t2] += 1 / antsWayDistances[antIndex, 0];
                }

                Tau = Tau.Multiply(1-Rho).Add(delataTau);
            }
        }

        private int WeightedRandom(IEnumerable<double> choices, IEnumerable<double> propobilites, ref bool isWayExist)
        {
            if (propobilites.All(it => it == double.PositiveInfinity))
            {
                isWayExist = false;
                return 0;
            }
            var randomNumber = Random.Shared.NextDouble();
            int index = 0;
            foreach (var (el, i) in propobilites.Select((el, i) => (el, i)))
            {
                if (el == double.PositiveInfinity) ;
                else if (randomNumber < el)
                {
                    index = i;
                    isWayExist = true;
                    break;
                }

                randomNumber -= el;
            }
            return (int)choices.ElementAt(index);
        }

        private Matrix<double> AntsWayDistances(Matrix<double> table)
        {
            double[,] data = new double[PopulationSize, 1];

            foreach (var (index, row) in table.EnumerateRowsIndexed())
                data[index, 0] = Func(row, DistanceMatrix);

            return Matrix<double>.Build.DenseOfArray(data);
        }
    }
}
