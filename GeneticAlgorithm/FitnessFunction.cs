namespace GenAlgLab_1_1
{
    public class FitnessFunction
    {
        public double MinX { get; init; }
        public double MaxX { get; init; }
        // OLD FUNCTION, extremes are:
        // maximum at (0.3171 1.0892)
        // minimum at (0.807 -0.9563)
        public IEnumerable<double> ExtremumX { get; init; }
        public Func<double, double> Function { get; init; }
        //public double Get(double x)
        //{
        //    return Math.Sin(6 * x - 1) + Math.Cos(4 * x) + 2 * Math.Pow(x, 5);
        //}
        public double Get(double x) => Function(x);

        public FitnessFunction(double min_x,  double max_x, IEnumerable<double> extremes,
            Func<double, double> function)
        {
            MinX = min_x; MaxX = max_x; ExtremumX = extremes; Function = function;
        }
    }
}
