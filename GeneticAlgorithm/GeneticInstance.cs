namespace GenAlgLab_1_1
{
    public class GeneticInstance
    {
        public FitnessFunction Function { get; set; }

        // Каждый элемент вектора кодирует один параметр/"ген"
        // это "истинное" значение, т. е. the source of truth (grayscale Morgan Freeman reference),
        // именно оно будет храниться в памяти
        public UInt64[] Value { get; set; }

        // Вещественные значения всех параметров
        // вычисляются путём отображения из целочисленного диапазона [0..2^64-1] для Value
        // в вещественный диапазон [MinX; MaxX] для соответствующего параметра
        // TODO: probably heavy refactor of dis
        public double[] ValueReal
        {
            get
            {
                double[] result = new double[Function.ParameterCount];

                for (int i = 0; i < Function.ParameterCount; i++)
                {
                    double percentage =
                        (double)Value[i] / UInt64.MaxValue;

                    result[i] =
                        percentage * (Function.MaxX[i] - Function.MinX[i])
                        + Function.MinX[i];
                }

                return result;
            }
            set
            {
                Value = new UInt64[Function.ParameterCount];

                for (int i = 0; i < Function.ParameterCount; i++)
                {
                    double percentage =
                        (value[i] - Function.MinX[i]) /
                        (Function.MaxX[i] - Function.MinX[i]);

                    Value[i] =
                        (UInt64)(percentage * UInt64.MaxValue);
                }
            }
        }
        public double FitnessValue
        {
            get => Function.Get(ValueReal);
        }
        public GeneticInstance(FitnessFunction function, UInt64[] value)
        {
            Function = function;
            Value = value;
        }
        public GeneticInstance(FitnessFunction function, double[] value_real)
        {
            Function = function;
            ValueReal = value_real;
        }
    }
}