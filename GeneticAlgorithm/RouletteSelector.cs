namespace GenAlgLab_1_1
{
    /// <summary>
    /// Выполняет селекцию методом "рулетки":
    /// "взвешенная" селекция, когда выше вероятность выбрать особь
    /// с бо́льшим значением fitness-функции
    /// </summary>
    public class RouletteSelector : IGeneticSelector
    {
        // похоже, что селекторы работают исключительно со значениями функции приспособленности,
        // а потому рефакторить их для случая нескольких переменных не надо
        public IEnumerable<GeneticInstance> Select(IEnumerable<GeneticInstance> instances,
            int count = -1, Random? rng = null)
        {
            // пока что будет: минимальному fitness из выборки соответствует вес 1,
            // затем увеличивается линейно
            if (rng is null) rng = new Random();
            // минимальное значение fitness-функции, для которого вес будет равен 1
            double weight_base = instances.Min(instance => instance.FitnessValue);
            // веса для всех элементов списка
            Dictionary<GeneticInstance, double> weights = instances.ToDictionary(
                // ключ в словаре: особь в популяции
                 keySelector: instance => instance,
                // значение: вес, определённый по fitness-функции
                elementSelector: instance => Math.Pow(instance.FitnessValue - weight_base, 2.0) + 1);
            // сумма "откалиброванных" весов
            double weight_sum = weights.Sum(kvp => kvp.Value);
            // количество в итоговом массиве
            // если -1, то по умолчанию половина
            count = (count <= 0) ? Math.Max(1, instances.Count() / 2) : count;
            List<GeneticInstance> result = new(count);
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
                        //weights[kvp.Key] /= 2.0;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
