namespace GA7
{
    internal class Particle
    {
        static Random _random = new Random();
        private Swarm _swarm;

        public double[] Velocity { get; private set; }
        public double[] Position { get; private set; }
        public double[] LocalBestPositon { get; private set; }
        public double LocalBestFinalFunc => _swarm.Function.Calc(LocalBestPositon);

        public Particle(Swarm swarm)
        {
            _swarm= swarm;

            Position = InitPosition();
            LocalBestPositon = (double[])Position.Clone();
            _swarm.FinalFunction(Position);
            Velocity = InitVelocity();
        }

        public void NextIteration()
        {
            CorrectVelocity();
            Move();
            CheckFinalFunc();
        }

        private double[] InitPosition()
        {
            double[] position = new double[_swarm.Dimension];

            for (int i = 0; i < _swarm.Dimension; i++)
                position[i] = _random.NextDouble() * (_swarm.Function.MaxValues[i] - _swarm.Function.MinValues[i]) + _swarm.Function.MinValues[i];

            return position;
        }

        private double[] InitVelocity()
        {
            double[] velocity = new double[_swarm.Dimension];

            for (int i = 0; i < _swarm.Dimension; i++) 
            {
                double minval = -(_swarm.Function.MaxValues[i] - _swarm.Function.MinValues[i]);
                double maxval = _swarm.Function.MaxValues[i] - _swarm.Function.MinValues[i];

                velocity[i] = _random.NextDouble() * (maxval - minval) + minval;
            }

            return velocity;
        }

        private void CheckFinalFunc()
        {
            double finalFunc = _swarm.FinalFunction(Position);
            
            if (finalFunc < LocalBestFinalFunc)
                LocalBestPositon = (double[])Position.Clone();
        }

        private void Move()
        {
            for (int i = 0; i < _swarm.Dimension; i++)
                Position[i] += Velocity[i];
        }

        private void CorrectVelocity()
        {
            double velocityRatio = _swarm.LocalVelocityRatio + _swarm.GlobalVelocityRatio;

            double commonRatio = (2.0 * _swarm.CurrentVelocityRatio / 
                Math.Abs(2.0 - velocityRatio - Math.Sqrt(velocityRatio * velocityRatio - 4.0 * velocityRatio)));

            for (int i = 0; i < _swarm.Dimension; i++)
            {
                double newVelocityPart1 = commonRatio * Velocity[i];
                double newVelocityPart2 = commonRatio * _swarm.LocalVelocityRatio * _random.NextDouble() * (LocalBestPositon[i] - Position[i]);
                double newVelocityPart3 = commonRatio * _swarm.GlobalVelocityRatio * _random.NextDouble() * (_swarm.BestPosition[i] - Position[i]);

                Velocity[i] = newVelocityPart1 + newVelocityPart2 + newVelocityPart3;
            }
        }
    }
}
