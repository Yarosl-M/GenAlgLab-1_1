using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenAlgLab_1_1;

namespace GeneticAlgorithm
{
    public interface IMutationOperator
    {
        /// <summary>
        /// Выполняет мутацию указанной особи.
        /// </summary>
        /// <param name="instance">Исходная особь для мутации.</param>
        /// <param name="count">Количество мутаций в геноме (применяется
        /// отдельно для каждого гена/параметра).</param>
        /// <param name="rng">Необязательно — объект ГСЧ для обеспечения
        /// повторяемости результатов.</param>
        /// <returns>Новый объект-особь с применённой мутацией;
        /// исходная особь не изменяется.</returns>
        public GeneticInstance Mutate(GeneticInstance instance, int count = 1, Random? rng = null);
    }
}
