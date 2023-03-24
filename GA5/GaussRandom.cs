namespace GA5
{
    static class GaussRandom
    {
        public static double Next(double mean = 0.0, double variance = 1.0)
        {
            double u1 = 1 - Random.Shared.NextDouble();
            double u2 = 1 - Random.Shared.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + randStdNormal * variance;
        }
    }

}
