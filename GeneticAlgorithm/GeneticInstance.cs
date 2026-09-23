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
                    result[i] = Function.MapToRange(
                        x: (double)Value[i] / UInt64.MaxValue, // this value is in [0.0, 1.0]
                        parameter_idx: i // that's why we don't need to specify the range bc that's the default
                    );
                }

                return result;
            }
            set
            {
                Value = new UInt64[Function.ParameterCount];

                for (int i = 0; i < Function.ParameterCount; i++)
                {
                    double percentage = Function.MapFromRange(x: value[i], parameter_idx: i);

                    Value[i] = (UInt64)(percentage * UInt64.MaxValue);
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