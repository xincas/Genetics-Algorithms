namespace GA7
{
    abstract public class Function
    {
        public double[] MinValues { get; private set; }
        public double[] MaxValues { get; private set; }

        protected Function(double[] mins, double[] maxs) 
        {
            MinValues= mins;
            MaxValues= maxs;
        }

        public abstract double Calc(double[] position);

        protected virtual double GetPenalty(double[] position, double ratio)
        {
            double penalty = 0.0;

            for (int i = 0; i < position.Length; i++)
            {
                if (position[i] < MinValues[i])
                {
                    penalty += Math.Abs(position[i] - MinValues[i]);
                }

                if (position[i] > MaxValues[i])
                {
                    penalty += Math.Abs(position[i] - MaxValues[i]);
                }
            }

            return penalty * ratio;
        }
    }

    public class RastriginFunction : Function
    {
        public RastriginFunction(double[] mins, double[] maxs) : base(mins, maxs) { }

        public override double Calc(double[] position)
        {
            double result = 0.0;

            foreach (double x in position)
            {
                result += x * x - 10.0 * Math.Cos(2.0 * Math.PI * x);
            }

            result += 10.0 * MinValues.Length + GetPenalty(position, 10000.0);

            return result;
        }
    }

    public class EasonFunction : Function
    {
        public EasonFunction(double[] mins, double[] maxs) : base(mins, maxs) { }

        public override double Calc(double[] position)
        {
            return -Math.Cos(position[0]) * Math.Cos(position[1]) * Math.Exp(-(Math.Pow(position[0] - Math.PI, 2) + Math.Pow(position[1] - Math.PI, 2)));
        }
    }
}
