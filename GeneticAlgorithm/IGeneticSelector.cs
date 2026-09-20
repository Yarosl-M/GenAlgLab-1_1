namespace GenAlgLab_1_1
{
    /// <summary>
    /// Интерфейс для класса, выполняющего селекцию особей.
    /// </summary>
    public interface IGeneticSelector
    {
        /// <summary>
        /// Выполняет селекцию особей и возвращает оставшихся наиболее приспособленных.
        /// </summary>
        /// <param name="instances">Исходные особи для селекции.</param>
        /// <param name="count">Количество наиболее присособленных особей, которых надо оставить (если указано -1, по умолчанию оставляет половину)</param>
        /// <param name="seed">Исходное значение для ГСЧ (если он будет использоваться); null для случайного стартового значения.</param>
        /// <returns>Список, содержащий оставшихся особей, отобранных по определённому алгоритму селекции.</returns>
        public IEnumerable<GeneticInstance> Select(IEnumerable<GeneticInstance> instances,
            int count = -1, Random? rng = null);
    }
}
