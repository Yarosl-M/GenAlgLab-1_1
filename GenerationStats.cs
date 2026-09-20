namespace GenAlgLab_1_1
{
    // статистика популяции в одном поколении:
    // расстояние от каждой особи до x экстремума, свёрнутое в min/max/mean/std
    public class GenerationStats
    {
        public int Generation { get; }
        public double MinDistance { get; }
        public double MaxDistance { get; }
        public double MeanDistance { get; }
        public double StdDevDistance { get; }

        public GenerationStats(int generation, IReadOnlyCollection<double> distances)
        {
            Generation = generation;
            MinDistance = distances.Min();
            MaxDistance = distances.Max();
            MeanDistance = distances.Average();

            var mean = MeanDistance;
            var variance = distances.Select(d => (d - mean) * (d - mean)).Average();
            StdDevDistance = Math.Sqrt(variance);
        }

        public static GenerationStats FromPopulation(
            int generation, IEnumerable<GeneticInstance> population, IGeneticFunction function)
        {
            var distances = population
                .Select(p => Math.Abs(p.ValueReal - function.ExtremumX))
                .ToList();
            return new GenerationStats(generation, distances);
        }
    }
}
