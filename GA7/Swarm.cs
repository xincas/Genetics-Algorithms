namespace GA7
{
    delegate void SwarmListener(Swarm swarm);
    internal class Swarm
    {
        public Particle[] Particles { get; private set; }
        public double BestFinalFunc { get; private set; }
        public int Iteration { get; private set; }
        public double CurrentVelocityRatio { get; private set; }
        public double LocalVelocityRatio { get; private set; }
        public double GlobalVelocityRatio { get; private set; }
        public double[] BestPosition { get; private set; }
        public Function Function { get; private set; }
        public int Dimension => Function.MinValues.Length;
        public int Size => Particles.Length;

        public SwarmListener Listeners;
        public void SetListener(SwarmListener f) => Listeners += f;
        public void ClearListeners() => Listeners = null;
        public Swarm(
            Function fun,
            int swarmSize,
            double currentVelocityRatio,
            double localVelocityRatio,
            double globalVelocityRatio)
        {
            Function= fun;
            CurrentVelocityRatio= currentVelocityRatio;
            LocalVelocityRatio= localVelocityRatio;
            GlobalVelocityRatio= globalVelocityRatio;

            BestFinalFunc = double.MaxValue;

            Particles = CreateParticles(swarmSize);
        }

        public void Evolve(int maxIteration)
        {
            Listeners?.Invoke(this);
            for (int i = 0; i < maxIteration; i++)
            {
                foreach(Particle particle in Particles)
                    particle.NextIteration();
                Iteration++;
                Listeners?.Invoke(this);
            }
        }

        public double FinalFunction(double[] position)
        {
            double finalFunc = Function.Calc(position);
            if (finalFunc < BestFinalFunc)
            {
                BestFinalFunc = finalFunc;
                BestPosition = (double[])position.Clone();
            }
            return finalFunc;
        }

        private Particle[] CreateParticles(int size)
        {
            Particle[] particles = new Particle[size];
            for (int i = 0; i < size; i++)
                particles[i] = new Particle(this);

            return particles;
        }
    }
}
