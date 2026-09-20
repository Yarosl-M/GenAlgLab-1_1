using GenAlgLab_1_1;

namespace GeneticAlgorithm
{
    public interface ICrossoverOperator
    {
        public Tuple<GeneticInstance, GeneticInstance> Crossover(
            GeneticInstance first, GeneticInstance second, Random? rng = null);
    }
}
