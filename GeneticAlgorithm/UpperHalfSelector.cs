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
            count = (count <= 0) ? Math.Max(1, all_count / 2) : count;
            GeneticInstance[] remaining = instances.OrderByDescending(
                instance => instance.FitnessValue).Take(count).ToArray();
            return remaining;
        }
    }
}
