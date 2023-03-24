using GA4lib.GA;

namespace GA4lib.GP
{
    /*
    public class CoefficientsChromosome : IChromosome
    {
        private GpContext _context;
        public GpChromosome Parent { get; }
        public List<double> Coefficients { get; private set; }
        public CoefficientsFitness FitnessFunction { get; private set; }

        public CoefficientsChromosome(GpChromosome parent, GpContext context, List<double> coefficients, CoefficientsFitness fitnessFunc)
        {
            Parent = parent;
            _context = context;
            Coefficients = coefficients;
            FitnessFunction = fitnessFunc;
        }

        public List<IChromosome> Crossover(IChromosome another)
        {
            var coefficientsChromosome = (CoefficientsChromosome)another;
            List<IChromosome> ret = new(2);

            CoefficientsChromosome thisClone = (CoefficientsChromosome)Clone();
            CoefficientsChromosome anotherClone = (CoefficientsChromosome)another.Clone();

            for (int i = 0; i < thisClone.Coefficients.Count; i++)
            {
                if (Random.Shared.NextDouble() > _context.PCrossover)
                {
                    thisClone.Coefficients[i] = coefficientsChromosome.Coefficients[i];
                    anotherClone.Coefficients[i] = Coefficients[i];
                }
            }

            ret.Add(thisClone);
            ret.Add(anotherClone);

            return ret;
        }

        public IChromosome Mutate()
        {
            var ret = (CoefficientsChromosome)Clone();
            for (int i = 0; i < ret.Coefficients.Count; ++i)
                if (Random.Shared.NextDouble() > _context.PMutation)
                    ret.Coefficients[i] += _context.GetRandomMutationValue();
            return ret;
        }

        public object Clone() => new CoefficientsChromosome(Parent, _context, new(Coefficients), FitnessFunction);
    }

    public class CoefficientsFitness : IFitness<CoefficientsChromosome, double>
    {
        public double Calculate(CoefficientsChromosome chromosome)
        {
            SyntaxTreeUtils.SetCoefficients(chromosome.Parent.SyntaxTree, chromosome.Coefficients);
            return chromosome.Parent.FitnessFunction.Calculate(chromosome.Parent);
        }
    }
    */

    public class GpChromosome : IChromosome
    {
        public Expression SyntaxTree { get; private set; }
        public GpContext Context { get; private set; }

        private double fitness;

        public IFitness<GpChromosome, double> FitnessFunction { get; private set; }

        public GpChromosome(Expression syntaxTree, GpContext context, IFitness<GpChromosome, double> fitnessFunction)
        {
            SyntaxTree = syntaxTree;
            Context = context;
            FitnessFunction = fitnessFunction;
        }

        public GpChromosome(Expression syntaxTree, GpContext context, IFitness<GpChromosome, double> fitnessFunction, double fitness)
        {
            SyntaxTree = syntaxTree;
            Context = context;
            FitnessFunction = fitnessFunction;
            this.fitness = fitness;
        }

        public List<IChromosome> Crossover(IChromosome another)
        {
            List<IChromosome> ret = new(2);

            if (Random.Shared.NextDouble() > Context.PCrossover) return ret;

            GpChromosome thisClone = new GpChromosome(SyntaxTree.Clone(), Context, FitnessFunction);
            GpChromosome anotherClone = new GpChromosome(SyntaxTree.Clone(), Context, FitnessFunction);

            Expression thisRandomNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
            Expression anotherRandomNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);

            Expression thisRandomSubTreeClone = thisRandomNode.Clone();
            Expression anotherRandomSubTreeClone = anotherRandomNode.Clone();

            SyntaxTreeUtils.SwapNode(thisRandomNode, anotherRandomSubTreeClone);
            SyntaxTreeUtils.SwapNode(anotherRandomNode, thisRandomSubTreeClone);

            ret.Add(thisClone);
            ret.Add(anotherClone);

            thisClone.OptimizeTree();
            anotherClone.OptimizeTree();
            
            return ret;
        }

        private void OptimizeTree()
        {
            OptimizeTree(10);
        }
        
        private void OptimizeTree(int maxIteration)
        {
            SyntaxTreeUtils.CutTree(SyntaxTree, Context, Context.MaxDepth);
            /*SyntaxTreeUtils.SimplifyTree(SyntaxTree, Context);

            List<double> coeffsOfTree = SyntaxTreeUtils.GetCoefficients(SyntaxTree);

            if (coeffsOfTree.Count > 0)
            {
                IFitness<CoefficientsChromosome, double> fit = new CoefficientsFitness();
                CoefficientsChromosome initialChromosome =
                    new CoefficientsChromosome(this, Context, coeffsOfTree, (CoefficientsFitness)fit);
                Population<CoefficientsChromosome> population = new();
                for (int i = 0; i < Context.PopulationSize; i++)
                    population.Add((CoefficientsChromosome)initialChromosome.Mutate());
                population.Add(initialChromosome);


                GAEngine<CoefficientsChromosome, double> env =
                    new GAEngine<CoefficientsChromosome, double>(population, fit);

                //env.AddListener(Update);
                env.Evolve(maxIteration);

                List<double> optimizedCoefficients = env.Best().Coefficients;

                SyntaxTreeUtils.SetCoefficients(SyntaxTree, optimizedCoefficients);
            }
            */
        }


        public IChromosome? Mutate()
        {
            if (Random.Shared.NextDouble() > Context.PMutation) return null;

            GpChromosome ret = new GpChromosome(SyntaxTree.Clone(), Context, FitnessFunction);
            int type = Random.Shared.Next(7);
            switch (type)
            {
                case 0:
                    ret.MutateByRandomChangeOfFunction();
                    break;
                case 1:
                    ret.MutateByRandomChangeOfChild();
                    break;
                case 2:
                    ret.MutateByRandomChangeOfNodeToChild();
                    break;
                case 3:
                    ret.MutateByReverseOfChildsList();
                    break;
                case 4:
                    ret.MutateByRootGrowth();
                    break;
                case 5:
                    ret.SyntaxTree = SyntaxTreeUtils.CreateTree(Context.MaxDepth, Context);
                    break;
                case 6:
                    ret.MutateByReplaceEntireTreeWithAnySubTree();
                    break;
            }

            ret.OptimizeTree();
            return ret;
        }

        private void MutateByReplaceEntireTreeWithAnySubTree()
        {
            SyntaxTree = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
        }

        private void MutateByRootGrowth()
        {
            //ExpressionType function = (ExpressionType)Random.Shared.Next(2, Enum.GetNames(typeof(ExpressionType)).Length);
            ExpressionType function = (ExpressionType)Random.Shared.Next(1, Enum.GetNames(typeof(ExpressionType)).Length);
            Expression newRoot = new Expression(function);
            newRoot.Childs.Add(SyntaxTree);

            if (function is ExpressionType.Mul or ExpressionType.Sub or ExpressionType.Sum or ExpressionType.Div or ExpressionType.Pow)
                newRoot.Childs.Add(SyntaxTreeUtils.CreateTree(Context.MaxDepth / 2, Context));

            SyntaxTree = newRoot;
        }

        private void MutateByReverseOfChildsList()
        {
            Expression mutatingNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
            if (mutatingNode.Childs.Count > 1 && (mutatingNode.Operation is ExpressionType.Div or ExpressionType.Sub or ExpressionType.Pow))
                mutatingNode.Childs.Reverse();
            else
                MutateByRandomChangeOfFunction();
        }

        private void MutateByRandomChangeOfNodeToChild()
        {
            Expression mutatingNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
            if (mutatingNode.Childs.Count != 0)
            {
                int index = Random.Shared.Next(mutatingNode.Childs.Count);
                Expression child = mutatingNode.Childs[index];
                SyntaxTreeUtils.SwapNode(mutatingNode, child.Clone());
            }
            else
                MutateByRandomChangeOfFunction();
        }

        private void MutateByRandomChangeOfChild()
        {
            Expression mutatingNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
            if (mutatingNode.Childs.Count != 0)
            {
                int index = Random.Shared.Next(mutatingNode.Childs.Count);
                mutatingNode.Childs[index] = SyntaxTreeUtils.CreateTree(Context.MaxDepth / 2, Context);
            }
            else
                MutateByRandomChangeOfFunction();
        }

        private void MutateByRandomChangeOfFunction()
        {
            Expression mutatingNode = SyntaxTreeUtils.GetRandomNode(SyntaxTree);
            ExpressionType odlFunction = mutatingNode.Operation;
            ExpressionType? newFunction = null;

            for (int i = 0; i < 3; i++)
            {
                newFunction = (ExpressionType)Random.Shared.Next(Enum.GetNames(typeof(ExpressionType)).Length);
                if (newFunction != odlFunction) break;
            }

            mutatingNode.Operation = (ExpressionType)newFunction!;
            mutatingNode.Value = null;
            mutatingNode.Variable = null;

            if (newFunction is ExpressionType.Variable)
                mutatingNode.Variable =
                    Context.Variables.Keys.ElementAt(Random.Shared.Next(Context.Variables.Keys.Count));

            //if (newFunction is ExpressionType.Constant)
            //    mutatingNode.Value = Context.GetRandomValue();

            if (newFunction is ExpressionType.Sub or ExpressionType.Div or ExpressionType.Pow or ExpressionType.Mul
                    or ExpressionType.Sum)
            {
                for (int i = mutatingNode.Childs.Count; i < 2; ++i)
                    mutatingNode.Childs.Add(SyntaxTreeUtils.CreateTree(Context.MaxDepth / 2, Context));
            }
            else if (newFunction is ExpressionType.Sqrt or ExpressionType.Sin or ExpressionType.Cos
                     or ExpressionType.Ln or ExpressionType.Exp)
            {
                for (int i = mutatingNode.Childs.Count; i < 1; ++i)
                    mutatingNode.Childs.Add(SyntaxTreeUtils.CreateTree(Context.MaxDepth / 2, Context));
                mutatingNode.Childs = mutatingNode.Childs.GetRange(0, 1);
            }
        }

        public object Clone() => new GpChromosome(SyntaxTree.Clone(), Context, FitnessFunction, fitness);

        /*public void Update(GAEngine<CoefficientsChromosome, double> engine)
        {
            var bestChromosome = engine.Best();
            double currentFitnessValue = engine.Fitness(bestChromosome);
            Console.WriteLine($"\t\titer = {engine.Iteration}\tfit = {currentFitnessValue}\tcoeffs = [{string.Join(", ", bestChromosome.Coefficients)}]");
            foreach (CoefficientsChromosome chromosome in engine.Population)
                Console.WriteLine(
                    $"\t\t\tfit = {engine.Fitness(chromosome)}\tcoeffs = [{string.Join(", ", bestChromosome.Coefficients)}]");
        }*/
    }
}
