using GeneticAlgorithm;
using ScottPlot;

namespace GenAlgLab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FitnessFunction previous_thing = new(min_x: [0.0], max_x: [1.0], extremes: [[0.3171]],
                function: x => Math.Sin(6 * x[0] - 1) + Math.Cos(4 * x[0]) + 2 * Math.Pow(x[0], 5),
                parameter_count: 1);

            FitnessFunction sphere_1d = new(
                min_x: [-5.12],
                max_x: [5.12],
                extremes: [[0.0]],
                function: x => -(x[0] * x[0]),
                parameter_count: 1);

            FitnessFunction sphere_2d = new(
                min_x: [-5.12, -5.12],
                max_x: [5.12, 5.12],
                extremes: [[0.0, 0.0]],
                function: x => -(x[0] * x[0] + x[1] * x[1]),
                parameter_count: 2);

            FitnessFunction sphere_3d = new(
                min_x: [-5.12, -5.12, -5.12],
                max_x: [5.12, 5.12, 5.12],
                extremes: [[0.0, 0.0, 0.0]],
                function: x => -(x[0] * x[0] + x[1] * x[1] + x[2] * x[2]),
                parameter_count: 3);

            FitnessFunction sphere_4d = new(
                min_x: [-5.12, -5.12, -5.12, -5.12],
                max_x: [5.12, 5.12, 5.12, 5.12],
                extremes: [[0.0, 0.0, 0.0, 0.0]],
                function: x => -(-x[0] * x[0] + x[1] * x[1] + x[2] * x[2] + x[3] * x[3]),
                parameter_count: 4);

            FitnessFunction beale = new(
                min_x: [-5.0, -5.0],
                max_x: [5.0, 5.0],
                extremes: [[3, 0.5]],
                function: x => -(Math.Pow(1.5 - x[0] + x[0] * x[1], 2) +
                    Math.Pow(2.25 - x[0] + x[0] * x[1] * x[1], 2) +
                    Math.Pow(2.625 - x[0] + x[0] * x[1] * x[1] * x[1], 2)),
                parameter_count: 2);

            IGeneticSelector half = new UpperHalfSelector();
            IGeneticSelector roulette = new RouletteSelector();

            ICrossoverOperator sp = new SingleCrossoverOperator();

            IMutationOperator mutator = new MutationOperator();

            Console.WriteLine("1—4 — функции сферы от 1 до 4 аргументов;");
            Console.WriteLine("5   — ранее определённая функция (1 аргумент);");
            Console.WriteLine("6   — функция Била (2 аргумента);");
            int choice = ReadIntOrDefault(
                "Выберите функцию: ",
                defaultValue: 1,
                isValid: value => value is >= 1 and <= 6);

            FitnessFunction function =
                new FitnessFunction[]
                {
                    sphere_1d,
                    sphere_2d,
                    sphere_3d,
                    sphere_4d,
                    previous_thing,
                    beale
                }[choice - 1];

            Console.Write(
                "1 для селекции по умолчанию, " +
                "2 для селекции методом рулетки: ");

            int selectorChoice = ReadIntOrDefault(
                prompt: "",
                defaultValue: 1,
                isValid: value => value is 1 or 2);

            IGeneticSelector choiceSelect =
                selectorChoice == 2 ? roulette : half;

            int count = ReadIntOrDefault(
                "Количество особей (по умолчанию = 64): ",
                defaultValue: 64,
                isValid: value => value > 0);

            int generations = ReadIntOrDefault(
                "Количество поколений (по умолчанию = 16): ",
                defaultValue: 16,
                isValid: value => value > 0);

            int percentage = ReadIntOrDefault(
                "Шанс мутации (в %, по умолчанию = 5%): ",
                defaultValue: 5,
                isValid: value => value is >= 0 and <= 100);

            double mutationRate = percentage / 100.0;

            int seed = ReadIntOrDefault(
                "Стартовое значение RNG: ",
                defaultValue: Random.Shared.Next());

            GeneticAlgorithm alg = new GeneticAlgorithm(function: function,
                selector: choiceSelect, mutator: mutator, crossoverOperator: sp,
                seed: seed, generations: generations,
                instance_count: count, mutation_rate: mutationRate);

            var stats_history = new GenerationStats[generations + 1];
            // oh dayum
            // the thing is, it should have 1 more stats object
            // because it's not just after each generation but one should also
            // be before the start of the algorithm
            stats_history[0] = new GenerationStats(0, alg.Population);
            int gen = 0;
            for (gen = 0;  gen < generations; gen++)
            {
                alg.Step();
                stats_history[gen + 1] = new(gen + 1, alg.Population);
            }

            // why was I even doing that old thing in the first place?????

            Console.WriteLine("Поколение | min | max | mean | std (расстояние до экстремума)");
            foreach (var s in stats_history)
            {
                Console.WriteLine(
                    $"{s.Generation,3} | {s.MinDistance:F5} | {s.MaxDistance:F5} | " +
                    $"{s.MeanDistance:F5} | {s.StdDevDistance:F5}");
            }

            PlotStats(stats_history);
        }

        static void PlotStats(IList<GenerationStats> history)
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

            var path = plt.SavePng("generation_stats.png", 1000, 600).Path;
            Console.WriteLine();
            Console.WriteLine($"График сохранён: {path}");
        }

        private static int ReadIntOrDefault(
    string prompt,
    int defaultValue,
    Func<int, bool>? isValid = null)
{
    Console.Write(prompt);

    string? input = Console.ReadLine();

    if (int.TryParse(input, out int value) &&
        (isValid is null || isValid(value)))
    {
        return value;
    }

    return defaultValue;
}
    }
}
