using GeneticAlgorithm;

namespace GenAlgLab_1_1
{

    public class MutationOperator : IMutationOperator
    {
        /// <summary>
        /// Выполняет случайную мутацию в хромосоме экземпляра (изменяет count случайных битов).
        /// </summary>
        /// <param name="instance">Экземпляр особи, мутацию которого необходимо выполнить.</param>
        /// <param name="count">Количество мутаций за один раз (мутации могут выполняться на
        /// одном и том же бите, поэтому фактически будет меньше изменений).</param>
        /// <param name="rng">Необязательно — объект генератора случайных чисел
        /// для обеспечения повторяемости результатов.</param>
        public void Mutate(GeneticInstance instance, int count = 1, Random? rng = null)
        {
            if (rng is null) rng = new Random();
            for (int i = 0; i < count; i++)
            {
                UInt64 flip_mask = 1UL << rng.Next(0, 64);
                instance.Value ^= flip_mask;
            }
        }
    }
}
