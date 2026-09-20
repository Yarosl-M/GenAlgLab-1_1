namespace GenAlgLab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int GenerationCount = 10;
            var function = new GeneticFunction();
            List<GeneticInstance> population = new List<GeneticInstance>();
            // first population
            var rng = new Random(1225);
            for (int i = 0; i < 64; i++)
            {
                population.Add(
                    new GeneticInstance(function, rng.NextDouble()));
            }

            var statsHistory = new List<GenerationStats>
            {
                GenerationStats.FromPopulation(0, population, function)
            };

            for (int i = 0; i < GenerationCount; i++)
            {
                // 1) селекция
                population = new UpperHalfSelector()
                    .Select(population).ToList();

                // 2) скрещивание и рекомбинация
                var to_add = new List<GeneticInstance>();
                // 32 -> надо сделать 16 пар
                for (int j = 0; j <= 15; j++)
                {
                    var first_idx = rng.Next(0, population.Count);
                    var second_idx = rng.Next(0, population.Count);
                    var first = population[first_idx];
                    var second = population[second_idx];
                    var new_members = CrossoverOperator
                        .Crossover(first, second, rng);
                    to_add.Add(new_members.Item1);
                    to_add.Add(new_members.Item2);
                }
                // добавить в популяцию
                population.AddRange(to_add);

                // 3) мутации
                for (int j = 0; j < population.Count; j++)
                {
                    // 5% шанс мутации (not exactly gonna be 5% of population but whatever)
                    if (rng.NextDouble() > 0.05) continue;

                    MutationOperator.Mutate(population[j], rng: rng);
                }

                statsHistory.Add(GenerationStats.FromPopulation(i + 1, population, function));
            }

            Console.WriteLine("Поколение | min | max | mean | std (расстояние до экстремума)");
            foreach (var s in statsHistory)
            {
                Console.WriteLine(
                    $"{s.Generation,3} | {s.MinDistance:F5} | {s.MaxDistance:F5} | " +
                    $"{s.MeanDistance:F5} | {s.StdDevDistance:F5}");
            }

            PlotStats(statsHistory);
        }

        static void PlotStats(List<GenerationStats> history)
        {
            var plt = new ScottPlot.Plot();

            double[] gens = history.Select(s => (double)s.Generation).ToArray();
            double[] mins = history.Select(s => s.MinDistance).ToArray();
            double[] maxs = history.Select(s => s.MaxDistance).ToArray();
            double[] means = history.Select(s => s.MeanDistance).ToArray();
            double[] meanPlusStd = history.Select(s => s.MeanDistance + s.StdDevDistance).ToArray();
            double[] meanMinusStd = history.Select(s => Math.Max(0, s.MeanDistance - s.StdDevDistance)).ToArray();

            var maxLine = plt.Add.Scatter(gens, maxs);
            maxLine.LegendText = "Максимум";
            maxLine.LineWidth = 1;
            maxLine.MarkerSize = 4;

            var minLine = plt.Add.Scatter(gens, mins);
            minLine.LegendText = "Минимум";
            minLine.LineWidth = 1;
            minLine.MarkerSize = 4;

            var upperLine = plt.Add.Scatter(gens, meanPlusStd);
            upperLine.LegendText = "Среднее + σ";
            upperLine.LinePattern = ScottPlot.LinePattern.Dashed;
            upperLine.MarkerSize = 0;

            var lowerLine = plt.Add.Scatter(gens, meanMinusStd);
            lowerLine.LegendText = "Среднее - σ";
            lowerLine.LinePattern = ScottPlot.LinePattern.Dashed;
            lowerLine.MarkerSize = 0;

            var meanLine = plt.Add.Scatter(gens, means);
            meanLine.LegendText = "Среднее";
            meanLine.LineWidth = 3;
            meanLine.MarkerSize = 5;

            plt.Title("Сходимость популяции к экстремуму по поколениям");
            plt.XLabel("Поколение");
            plt.YLabel("Расстояние до x экстремума");
            plt.ShowLegend();

            plt.SavePng("generation_stats.png", 1000, 600);
            Console.WriteLine();
            Console.WriteLine("График сохранён: generation_stats.png");
        }
    }
}
