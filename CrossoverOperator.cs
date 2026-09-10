namespace GenAlgLab_1_1
{
    /// <summary>
    /// Класс, содержащий оператор кроссинговера.
    /// </summary>
    public static class CrossoverOperator
    {
        /// <summary>
        /// Выполняет операцию кроссинговера (обмена участками хромосом между парой особей)
        /// </summary>
        /// <param name="first">Первая особь</param>
        /// <param name="second">Вторая особь</param>
        /// <param name="seed">Объект генератора случайных чисел, для обеспечения повторяемости
        /// результатов (необязательно)</returns>
        public static Tuple<GeneticInstance, GeneticInstance> Crossover(GeneticInstance first,
            GeneticInstance second, Random? rng = null)
        {
            if (rng == null) rng = new Random();
            // пока что пущай будет делать только одно-… что-то там, в общем, кроссинговер
            // с одной точкой деления
            // будет разделение хромосомы особи на две части, затем обмен частями
            // выбор точки разделения (в т. ч. возможно на самом краю, т. е. без изменений,
            // но довольно маловероятно)
            int where = rng.Next(0, 64);
            // старшие (64 - where) бит = 1, младшие where бит = 0
            UInt64 mask = UInt64.MaxValue << where;
            // оставить
            // старшие биты первой
            UInt64 a_high = first.Value & mask;
            // младшие биты первой
            UInt64 a_low = first.Value & ~mask;
            // старшие биты второй
            UInt64 b_high = second.Value & mask;
            // и младшие биты второй
            UInt64 b_low = second.Value & ~mask;
            return new( // и рекомбинировать их обратно (hopefully)
                new(first.Function, a_high | b_low),
                new(second.Function, b_high | a_low));
        }
        /// <summary>
        /// Выполняет операцию кроссинговера и рекомбинацию, но уже берёт в качестве параметра
        /// кортеж с объектами.
        /// </summary>
        /// <param name="instances">Кортеж с объектами особей</param>
        /// <param name="rng">Объект генератора случайных чисел, для обеспечения повторяемости
        /// результатов (необязательно)</param>
        /// <returns></returns>
        public static Tuple<GeneticInstance, GeneticInstance> Crossover(
            Tuple<GeneticInstance, GeneticInstance> instances, Random? rng = null) =>
            CrossoverOperator.Crossover(instances.Item1, instances.Item2, rng);
    }
}
