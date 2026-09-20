using GeneticAlgorithm;

namespace GenAlgLab_1_1
{
    public class GeneticAlgorithm
    {
        public FitnessFunction Function { get; set; }
        public IGeneticSelector Selector { get; set; }
        public IMutationOperator Mutator { get; set; }
        public ICrossoverOperator CrossoverOperator { get; set; }
        // maybe make other classes have instances/interfaces too
        // like:
        // mutation operator
        // crossover operator
        public int Seed {  get; set; }
        private Random rng;
        public int GenerationCount { get; set; }
        public GeneticInstance[] Instances { get; set; }
        public double MutationRate { get; set; }

        public GeneticAlgorithm(FitnessFunction function, IGeneticSelector selector,
            IMutationOperator mutator, ICrossoverOperator crossoverOperator,
            int? seed=null, int instance_count=64)
        {
            Function = function;
            Selector = selector;
            Seed = seed is null ? new Random().Next() : seed.Value;
            // populate later
            Instances = new GeneticInstance[instance_count];
            rng = new Random(Seed);
            for (int i = 0; i < instance_count; i++)
            {
                Instances[i] = new(Function,
                    rng.NextDouble())
            }
        }
    }
}
