namespace GenAlgLab_1_1
{
    /// <summary>
    /// Простейшая реализация алгоритма селекции: вернуть 50% наиболее приспособленных особей
    /// </summary>
    public class UpperHalfSelector : IGeneticSelector
    {
        // don't even need the rng in this one
        public IEnumerable<GeneticInstance> Select(IEnumerable<GeneticInstance> instances,
            int count = -1, Random? _ = null)
        {
            var all_count = instances.Count();
            count = (count <= 0) ? Math.Max(1, count / 2) : count;
            // temp: order just by the max value
            GeneticInstance[] left = instances.OrderByDescending(
                instance => instance.Function.Get(instance.ValueReal))
                .Take(count).ToArray();
            return left;
        }
    }
}
