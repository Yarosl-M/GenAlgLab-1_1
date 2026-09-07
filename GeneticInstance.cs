namespace GenAlgLab_1_1
{
    public class GeneticInstance
    {
        public IGeneticFunction Function { get; set; }
        public double Value { get; set; }
        public UInt64 ValueEncoded
        {
            // this is actually bad because we don't have this encoding as the "principal" one
            // it's not stored in memory
            get
            {
                // this is just a ratio of the segment lengths
                // should be between 0.0 and 1.0
                double percentage = (Value - Function.MinX) / (Function.MaxX - Function.MinX);
                // just multiplies the max 64-bit int value by this amount
                UInt64 mapped_value = (UInt64)(percentage * (double)UInt64.MaxValue) ;
                return mapped_value;
            }
            set
            {
                // the inverse operation
                // ratio between a new val and the max val
                double percentage = (double)value / (double)UInt64.MaxValue;
                Value = percentage * (Function.MaxX - Function.MinX) + Function.MinX;
                Value = percentage * (Function.MaxX - Function.MinX) + Function.MinX;
            }
        }
    }
}
