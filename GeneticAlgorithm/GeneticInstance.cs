namespace GenAlgLab_1_1
{
    public class GeneticInstance
    {
        public FitnessFunction Function { get; init; }
        public int ParameterCount { get => Function.ParameterCount; }

        // Каждый элемент вектора кодирует один параметр/"ген"
        // это "истинное" значение, т. е. the source of truth (grayscale Morgan Freeman reference),
        // именно оно будет храниться в памяти
        public UInt64[] Value { get; set; }

        // Вещественные значения всех параметров вычисляются путём отображения
        // между целочисленным диапазоном [0..2^64-1] для Value
        // и вещественным диапазоном [MinX; MaxX] для соответствующего параметра
        // Для многомерного случая (вектор параметров) удобнее сделать это двумя отдельными методами
        public double GetValueReal(int i)
        {

            return Function.MapToRange(
                x: (double)Value[i] / UInt64.MaxValue, // this value is in [0.0, 1.0]
                parameter_idx: i // that's why we don't need to specify the range bc that's the default
            );
        }
        public void SetValueReal(int i, double value)
        {
            // the same thing as above:
            // результирующий диапазон должен быть [0.0, 1.0], поэтому можно его явно не указывать
            double percentage = Function.MapFromRange(x: value, parameter_idx: i);
            Value[i] = (UInt64)(percentage * UInt64.MaxValue);
        }
        public double FitnessValue
        {
            get
            {
                var param_vector = new double[Function.ParameterCount];
                for (int i = 0; i < param_vector.Length; i++) param_vector[i] = GetValueReal(i);
                return Function.Get(param_vector);
            }
        }
        public GeneticInstance(FitnessFunction function, UInt64[] value)
        {
            Function = function;
            Value = new ulong[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                Value[i] = value[i];
            }
        }
        public GeneticInstance(FitnessFunction function, double[] value_real)
        {
            Function = function;
            // инициализация массива
            Value = new UInt64[value_real.Length];
            // и заполнение значениями
            for (int i = 0; i <  value_real.Length; i++) SetValueReal(i, value_real[i]);
        }
    }
}