namespace GenAlgLab_1_1
{
    public class FitnessFunction
    {
        public int ParameterCount { get; init; }
        public double[] MinX { get; init; }
        public double[] MaxX { get; init; }
        // OLD FUNCTION, extremes are:
        // maximum at (0.3171 1.0892)
        // minimum at (0.807 -0.9563)
        // элементы внешнего списка — просто отдельные экстремумы
        // внутри каждого списка — уже список координат
        public double[][] ExtremumX { get; init; }
        public Func<double[], double> Function { get; init; }
        //public double Get(double x)
        //{
        //    return Math.Sin(6 * x - 1) + Math.Cos(4 * x) + 2 * Math.Pow(x, 5);
        //}
        /// <summary>
        /// Вычисляет значение функции приспособленности.
        /// </summary>
        /// <param name="x">Вектор аргументов функции.</param>
        /// <returns>Значение функции приспособленности.</returns>
        public double Get(double[] x) => Function(x);
        
        /// <summary>
        /// Вспомогательная функция, выполняющая преобразование: отображение величины x из диапазона
        /// [from_a, from_b] в диапазон [MinX, MaxX]
        /// </summary>
        /// <param name="x">Величина, которую требуется преобразовать.</param>
        /// <param name="parameter_idx">Индекс параметра по порядку (начиная с 0).</param>
        /// <param name="from_a">Нижняя граница исходного диапазона (по умолчанию = 0).</param>
        /// <param name="from_b">Верхняя граница исходного диапазона (по умолчанию = 1).</param>
        /// <returns>Величина после преобразования (например, MapToRange(0.25, 0.0, 1.0) при MinX = 2
        /// и MaxX = 4 вернёт 2.5).</returns>
        public double MapToRange(double x, int parameter_idx=0, double from_a = 0.0, double from_b = 1.0)
        {
            return MinX[parameter_idx] + (x - from_a) *
                (MaxX[parameter_idx] - MinX[parameter_idx]) / (from_b - from_a);
        }

        /// <summary>
        /// Вспомогательная функция, выполняющая преобразование: отображение величины x из диапазона
        /// [MinX, MaxX] в диапазон [to_a, to_b]
        /// </summary>
        /// <param name="x">Величина, которую требуется преобразовать.</param>
        /// <param name="parameter_idx">Индекс параметра по порядку (начиная с 0).</param>
        /// <param name="to_a">Нижняя граница диапазона, в который идёт преобразование (по умолчанию = 0).</param>
        /// <param name="to_b">Верхняя граница диапазона, в который идёт преобразование (по умолчанию = 1).</param>
        /// <returns>Величина после преобразования.</returns>
        public double MapFromRange(double x, int parameter_idx=0, double to_a = 0.0, double to_b = 1.0)
        {
            return to_a + (x - MinX[parameter_idx]) *
                (to_b - to_a) / (MaxX[parameter_idx] - MinX[parameter_idx]);
        }

        public FitnessFunction(IReadOnlyCollection<double> min_x, IReadOnlyCollection<double> max_x,
            IReadOnlyCollection<double[]> extremes, Func<double[], double> function,
            int parameter_count=1)
        {
            ParameterCount = parameter_count;
            MinX = min_x.ToArray(); MaxX = max_x.ToArray();
            ExtremumX = extremes.ToArray(); Function = function;
        }
    }
}
