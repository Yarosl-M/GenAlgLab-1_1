using GeneticAlgorithm;

namespace GenAlgLab_1_1
{

    public class MutationOperator : IMutationOperator
    {
        /// <summary>
        /// Выполняет случайную мутацию в хромосоме экземпляра (изменяет count случайных битов).
        /// </summary>
        /// <param name="instance">Экземпляр особи, мутацию которого необходимо выполнить.</param>
        /// <param name="mutation_count">Количество мутаций за один раз (мутации могут выполняться на
        /// одном и том же бите, поэтому фактически будет меньше изменений).</param>
        /// <param name="rng">Необязательно — объект генератора случайных чисел
        /// для обеспечения повторяемости результатов.</param>
        public GeneticInstance Mutate(GeneticInstance instance, int mutation_count = 1, Random? rng = null)
        {
            if (rng is null) rng = new Random();
            // proper deep copy in the constructor
            GeneticInstance new_instance = new GeneticInstance(
                instance.Function, instance.Value);
            for (int j = 0; j < instance.ParameterCount; j++)
            { // do a mutation for each of the genes independently
                for (int i = 0; i < mutation_count; i++)
                {
                    UInt64 flip_mask = 1UL << rng.Next(64);
                    new_instance.Value[j] ^= flip_mask;
                }
            }
            return new_instance;
        }
    }
}
