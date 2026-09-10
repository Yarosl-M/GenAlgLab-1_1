namespace GenAlgLab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int GenerationCount = 10;
            List<GeneticInstance> population =
                new List<GeneticInstance>();
            // first population
            var rng = new Random(1225);
            for (int i = 0; i < 64; i++)
            {
                population.Add(
                    new GeneticInstance(new GeneticFunction(),
                    rng.NextDouble()));
            }
            foreach (var item in population)
            {
                Console.WriteLine($"{item.ValueReal:F5} -> {item.FitnessValue:F5}");
                Console.WriteLine(item.ValueReal);
                Console.WriteLine(item.FitnessValue);
                Console.WriteLine();
            }
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
            }
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            foreach (var item in population)
            {
                Console.WriteLine($"{item.ValueReal:F5} -> {item.FitnessValue:F5}");
                Console.WriteLine(item.ValueReal);
                Console.WriteLine(item.FitnessValue);
                Console.WriteLine();
            }
        }
    }
}
