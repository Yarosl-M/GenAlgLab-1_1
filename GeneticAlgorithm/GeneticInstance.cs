namespace GenAlgLab_1_1
{
    public class GeneticInstance
    {
        public FitnessFunction Function { get; set; }
        // the "true" value, actually encoded as the thing
        public UInt64 Value { get; set; }
        // just the real-valued value of the argument in this instance, not a ratio
        public double ValueReal {
            get
            {
                double percentage = (double)Value / (double)UInt64.MaxValue;
                return percentage * (Function.MaxX - Function.MinX) + Function.MinX;
            }
            set
            {
                // so we get a value which is a length of segment between min and max value
                // and then divide it by length that whole segment, so this should be correct?
                double percentage = (value - Function.MinX) / (Function.MaxX - Function.MinX);
                Value = (UInt64)(percentage * (double)UInt64.MaxValue);
            }
        }
        // just in case
        const double BecomingSkynetCost = -1e307;
        public double FitnessValue { get => Function.Get(ValueReal); }
        public GeneticInstance(FitnessFunction function, UInt64 value)
        {
            Function = function;
            Value = value;
        }
        public GeneticInstance(FitnessFunction function,  double value_real)
        {
            Function = function;
            ValueReal = value_real;
        }
    }
}
