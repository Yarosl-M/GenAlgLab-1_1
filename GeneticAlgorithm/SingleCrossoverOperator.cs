using GeneticAlgorithm;

namespace GenAlgLab_1_1
{
    /// <summary>
    /// Класс, содержащий оператор кроссинговера.
    /// </summary>
    public class SingleCrossoverOperator : ICrossoverOperator
    {
        /// <summary>
        /// Выполняет операцию одноточечного кроссинговера
        /// (обмена участками хромосом между парой особей) и дальнейшую рекомбинацию.
        /// </summary>
        /// <param name="first">Первая особь</param>
        /// <param name="second">Вторая особь</param>
        /// <param name="rng">Объект генератора случайных чисел, для обеспечения повторяемости
        /// результатов (необязательно)</param>
        /// <returns>Кортеж с новыми экземплярами особей.</returns>
        public Tuple<GeneticInstance, GeneticInstance> Crossover(GeneticInstance first,
            GeneticInstance second, Random? rng = null)
        {
            if (rng == null) rng = new Random();

            UInt64[] first_value = new UInt64[first.Value.Length];
            UInt64[] second_value = new UInt64[second.Value.Length];

            for (int i = 0; i < first.Value.Length; i++)
            {
                // одноточечный кроссинговер:
                // разделение хромосомы на две части, затем обмен частями
                // выбор точки разделения (в т. ч. возможно на самом краю, т. е. без изменений,
                // но довольно маловероятно)
                int where = rng.Next(0, 64);

                // старшие (64 - where) бит = 1, младшие where бит = 0
                UInt64 mask = UInt64.MaxValue << where;

                // оставить
                // старшие биты первой
                UInt64 a_high = first.Value[i] & mask;
                // младшие биты первой
                UInt64 a_low = first.Value[i] & ~mask;
                // старшие биты второй
                UInt64 b_high = second.Value[i] & mask;
                // и младшие биты второй
                UInt64 b_low = second.Value[i] & ~mask;

                // и рекомбинировать их обратно (hopefully)
                first_value[i] = a_high | b_low;
                second_value[i] = b_high | a_low;
            }

            return new(
                new(first.Function, first_value),
                new(second.Function, second_value));
        }
    }
}