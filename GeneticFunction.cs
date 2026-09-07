namespace GenAlgLab_1_1
{
    public interface IGeneticFunction
    {
        public double MinX { get; }
        public double MaxX { get; }
        public double Get(double x);
    }
    public class GeneticFunction : IGeneticFunction
    {
        public double MinX { get; } = 0.0;
        public double MaxX { get; } = 1.0;
        public double Get(double x)
        {
            // currently extremes are:
            // maximum at (0.3171 1.0892)
            // minimum at (0.807 -0.9563)
            return Math.Sin(6 * x - 1) + Math.Cos(4 * x) + 2 * Math.Pow(x, 5);
        }
    }
}
