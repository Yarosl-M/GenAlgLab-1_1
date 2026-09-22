namespace GenAlgLab_1_1
{
    // статистика популяции в одном поколении:
    // Евклидово расстояние генотипа каждой особи до экстремума,
    // свёрнутое в min/max/mean/std
    public class GenerationStats
    {
        /// <summary>
        /// Порядковый номер поколения.
        /// </summary>
        public int Generation { get; }
        /// <summary>
        /// Минимальное расстояние до экстремума в поколении.
        /// </summary>
        public double MinDistance { get; }
        /// <summary>
        /// Максимальное расстояние до экстремума в поколении.
        /// </summary>
        public double MaxDistance { get; }
        /// <summary>
        /// Среднее арифметическое расстояний до экстремума в поколении.
        /// </summary>
        public double MeanDistance { get; }
        /// <summary>
        /// Среднеквадратическое отклонение расстояний до экстремума в поколении.
        /// </summary>
        public double StdDevDistance { get; }

        // YAGNI: since all usages of this class are making it from population anyway, so might as well
        // have the class only accept that anyway
        public GenerationStats(int generation, IReadOnlyCollection<GeneticInstance> instances)
        {
            this.Generation = generation;

            // NOTE: для одномерного случая это верно, но для евклидовых расстояний
            // с другим числом параметров нужно будет вычислять по-другому

            // actually I've been working on it for so long that at this point might as well
            // just go with the multiple arguments thing in the first place, I guess
            double[] distances = [.. instances.Select((instance) =>
            Math.Abs(instance.ValueReal - instance.Function.ExtremumX[0]))];

            Generation = generation;
            MinDistance = distances.Min();
            MaxDistance = distances.Max();
            MeanDistance = distances.Average();

            // I always forget how to compute that thing
            var variance = distances.Select(d => (d - MeanDistance) * (d - MeanDistance))
                .Average();
            StdDevDistance = Math.Sqrt(variance);
        }
    }
}
