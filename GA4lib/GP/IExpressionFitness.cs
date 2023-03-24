namespace GA4lib.GP
{
    public interface IExpressionFitness
    {
        double Fitness(Expression expression, GpContext context);
    }
    public class Target
    {
        public Dictionary<string, double> contextState { get; private set; }
        public double TargetValue { get; private set; }

        public Target() => contextState = new Dictionary<string, double>();

        public Target(Dictionary<string, double> contextState, double targetValue)
        {
            this.contextState = new Dictionary<string, double>(contextState);
            TargetValue = targetValue;
        }

        public Target When(string variableName, double variableValue)
        {
            contextState.Add(variableName, variableValue);
            return this;
        }

        public Target TargetIs(double val)
        {
            TargetValue = val;
            return this;
        }
    }
    public class TabulatedFunctionFitness : IExpressionFitness
    {
        public List<Target> Targets { get; private set; }

        public TabulatedFunctionFitness(params Target[] targets)
        {
            Targets = new List<Target>();
            foreach (var target in targets) Targets.Add(target);
        }

        public TabulatedFunctionFitness(List<Target> targets) => Targets = targets;

        public double Fitness(Expression expression, GpContext context)
        {
            double diff = 0;

            foreach (var target in Targets)
            {
                foreach (var keyVal in target.contextState)
                    context.Variables[keyVal.Key] = keyVal.Value;

                double targetValue = target.TargetValue;
                double calculatedValue = expression.Eval(context);
                if (calculatedValue is Double.NaN)
                {
                    diff = Double.MaxValue;
                    break;
                }
                diff += Math.Abs(targetValue - calculatedValue); //Math.Pow(targetValue - calculatedValue, 2);
            }

            return diff;
        }
    }
}
