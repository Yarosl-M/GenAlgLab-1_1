using GeneticAlgorithm;

namespace GenAlgLab_1_1
{
    public class GeneticAlgorithm
    {
        public FitnessFunction Function { get; set; }
        public IGeneticSelector Selector { get; set; }
        public ICrossoverOperator CrossoverOperator { get; set; }
        public IMutationOperator Mutator { get; set; }
        // maybe make other classes have instances/interfaces too
        // like:
        // mutation operator
        // crossover operator
        /// <summary>
        /// Начальное значение для генератора случайных чисел для обеспечения повторяемости экспериментов.
        /// </summary>
        public int Seed {  get; set; }
        private Random rng;
        /// <summary>
        /// Количество поколений, эволюция которых будет отслеживаться.
        /// </summary>
        public int GenerationCount { get; set; }
        private int generations_passed = 0;
        /// <summary>
        /// Собственно популяция для данного экзмепляра алгоритма.
        /// </summary>
        public GeneticInstance[] Population { get; set; }
        /// <summary>
        /// Количество особей в популяции.
        /// </summary>
        public int InstanceCount { get; set; }
        /// <summary>
        /// Примерная доля популяции, к которой применяются мутации (шанс вызова мутации на одной особи).
        /// </summary>
        public double MutationRate { get; set; }
        /// <summary>
        /// Минимальное количество мутаций на этапе мутации.
        /// </summary>
        public int MutationCountMin { get; set; }
        /// <summary>
        /// Максимальное количество мутаций на этапе мутации.
        /// </summary>
        public int MutationCountMax { get; set; }

        public GeneticAlgorithm(FitnessFunction function, IGeneticSelector selector,
            IMutationOperator mutator, ICrossoverOperator crossoverOperator,
            int? seed=null, int generations=16, int instance_count=64, double mutation_rate = 0.05,
            int min_mutation_count=1, int max_mutation_count=2)
        {
            Function = function;
            Selector = selector;
            CrossoverOperator = crossoverOperator;
            GenerationCount = generations;
            InstanceCount = instance_count;
            MutationRate = mutation_rate;
            MutationCountMin = min_mutation_count;
            MutationCountMax = max_mutation_count;
            Seed = seed is null ? new Random().Next() : seed.Value;
            rng = new Random(Seed);
            // create instance array
            Population = new GeneticInstance[instance_count];
            // and populate it
            for (int i = 0; i < instance_count; i++)
            {
                var init_genome = new UInt64[Function.ParameterCount];
                for (int j = 0; j < Function.ParameterCount; j++)
                {
                    byte[] buf = new byte[8];
                    rng.NextBytes(buf);
                    init_genome[j] = BitConverter.ToUInt64(buf);
                }
                Population[i] = new(Function, init_genome);
            }
        }
        // один шаг (итерация) генетического алгоритма
        // возвращает номер только что пройденной итерации или -1, если алгоритм уже завершил работу
        // (по количеству поколений)
        /// <summary>
        /// Выполняет один шаг (итерацию) генетического алгоритма.
        /// </summary>
        /// <returns>Индекс только что пройденной итерации (начиная с 0 после первого поколения)
        /// или -1, если алгоритм уже завершил работу.</returns>
        public int Step()
        {
            if (generations_passed == GenerationCount) return -1;

            var population = Population;
            
            // 1) селекция (Selector)
            var selected = Selector.Select(
                instances: population, rng: rng).ToArray();

            // 2) скрещивание (кроссинговер) и рекомбинация (CrossoverOperator)
            int instances_to_add = InstanceCount - selected.Length;
            // число итераций скрещивания для того, чтобы восстановить популяцию
            int n = instances_to_add - instances_to_add / 2;
            var to_add = new List<GeneticInstance>(instances_to_add);
            for (int j = 0; j < n; j++)
            {
                var first_idx = rng.Next(0, selected.Length);
                var second_idx = rng.Next(0, selected.Length);
                var first = selected[first_idx];
                var second = selected[second_idx];
                var (add_first, add_second) = CrossoverOperator.Crossover(first, second);
                // wonder if i can deconstruct a tuple right in here
                to_add.AddRange([add_first, add_second]);
            }
            // полное восстановление популяции
            population = selected.Concat(to_add).Take(InstanceCount).ToArray(); ;

            // 3) мутации                                   (Mutator)
            foreach (var instance in population)
            {
                if (rng.NextDouble() < MutationRate)
                {
                    Mutator.Mutate(instance, rng.Next(MutationCountMin, MutationCountMax + 1), rng);
                }
            }

            Population = population; // ????? // although yes now

            return generations_passed++;
        }
    }
}
