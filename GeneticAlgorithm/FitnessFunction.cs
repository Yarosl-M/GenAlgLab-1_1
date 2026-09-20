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
        
        /// <summary>
        /// Вспомогательная функция, выполняющая преобразование: отображение величины x из диапазона
        /// [from_a, from_b] в диапазон [MinX, MaxX]
        /// </summary>
        /// <param name="x">Величина, которую требуется преобразовать.</param>
        /// <param name="from_a">Нижняя граница исходного диапазона (по умолчанию = 0).</param>
        /// <param name="from_b">Верхняя граница исходного диапазона (по умолчанию = 1).</param>
        /// <returns>Величина после преобразования (например, MapToRange(0.25, 0.0, 1.0) при MinX = 2
        /// и MaxX = 4 вернёт 2.5).</returns>
        public double MapToRange(double x, double from_a = 0.0, double from_b = 1.0)
        {
            return MinX + (x - from_a) * (MaxX - MinX) / (from_b - from_a);
        }

        /// <summary>
        /// Вспомогательная функция, выполняющая преобразование: отображение величины x из диапазона
        /// [MinX, MaxX] в диапазон [to_a, to_b]
        /// </summary>
        /// <param name="x">Величина, которую требуется преобразовать.</param>
        /// <param name="to_a">Нижняя граница диапазона, в который идёт преобразование (по умолчанию = 0).</param>
        /// <param name="to_b">Верхняя граница диапазона, в который идёт преобразование (по умолчанию = 1).</param>
        /// <returns>Величина после преобразования.</returns>
        public double MapFromRange(double x, double to_a = 0.0, double to_b = 1.0)
        {
            return to_a + (x - MinX) * (to_b - to_a) / (MaxX - MinX);
        }

        public FitnessFunction(double min_x,  double max_x, IEnumerable<double> extremes,
            Func<double, double> function)
        {
            MinX = min_x; MaxX = max_x; ExtremumX = extremes; Function = function;
        }
    }
}
