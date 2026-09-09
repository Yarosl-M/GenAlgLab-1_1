namespace GenAlgLab_1_1
{
    /// <summary>
    /// Выполняет селекцию методом "рулетки":
    /// выплоняет "взвешенную" селекцию, когда выше вероятность выбрать особь
    /// с бо́льшим значением fitness-функции
    /// </summary>
    public class RouletteSelector : IGeneticSelector
    {
        public IEnumerable<GeneticInstance> Select(IEnumerable<GeneticInstance> instances,
            int count = -1, int? seed = null)
        {
            // пока что будет: минимальному fitness из выборки соответствует вес 1,
            // затем увеличивается линейно
            Random rng = seed is null ? new() : new(seed.Value);
            double weight_sum = 0.0;
            // минимальное значение fitness-функции, для которого вес будет равен 1
            double weight_base = instances.Min(instance =>
            instance.Function.Get(instance.ValueReal));
            // веса для всех элементов списка
            Dictionary<GeneticInstance, double> weights = instances.ToDictionary(
                // ключ в словаре: особь в популяции
                 keySelector: instance => instance,
                // значение: вес, определённый по fitness-функции
                elementSelector: instance => {
                    double w = 1 + instance.FitnessValue - weight_base;
                    weight_sum += w;
                    return w;
                    });
            // опять количество в конечном массиве 
            count = (count <= 0) ? Math.Max(1, count / 2) : count;
            List<GeneticInstance> result = [];
            // for each element to pick
            for (int i = 0; i < count; i++)
            {
                // yay my favourite weighted random choice stuff
                double rand_val = rng.NextDouble() * weight_sum;
                foreach (var kvp in weights)
                {
                    rand_val -= kvp.Value;
                    if (rand_val < 0)
                    {
                        result.Add(kvp.Key);
                        break;
                    }
                }
            }
            return result;
        }
    }
}
