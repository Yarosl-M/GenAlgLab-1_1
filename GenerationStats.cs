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
        public GenerationStats(int generation, IList<GeneticInstance> instances)
        {
            this.Generation = generation;

            // NOTE: для одномерного случая это верно, но для евклидовых расстояний
            // с другим числом параметров нужно будет вычислять по-другому
            double[] distances = new double[instances.Count];
            double[,] extremums = instances[0].Function.ExtremumX;
            // сначала надо вычислить расстояние до ближайшего экстремума
            // для каждой пары точки и экстремума
            // затем из них выбрать минимальное значение
            for (int instance_idx = 0; instance_idx < instances.Count; instance_idx++)
            {
                double closest_distance = double.PositiveInfinity;

                for (int extremum_idx = 0;
                    extremum_idx < extremums.GetLength(0);
                    extremum_idx++)
                {
                    double squared_distance = 0.0;

                    for (int parameter_idx = 0;
                        parameter_idx < extremums.GetLength(1);
                        parameter_idx++)
                    {
                        double point_coordinate =
                            instances[instance_idx].GetValueReal(parameter_idx);

                        double extremum_coordinate =
                            extremums[extremum_idx, parameter_idx];

                        double difference = point_coordinate - extremum_coordinate;
                        squared_distance += difference * difference;
                    }

                    double distance = Math.Sqrt(squared_distance);

                    if (distance < closest_distance)
                    {
                        closest_distance = distance;
                    }
                }

                distances[instance_idx] = closest_distance;
            }

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
