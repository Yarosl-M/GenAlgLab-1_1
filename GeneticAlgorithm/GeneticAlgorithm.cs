namespace GenAlgLab_1_1
{
    public class GeneticAlgorithm
    {
        private IGeneticFunction _function;
        private IGeneticSelector _selector;
        // maybe make other classes have instances/interfaces too
        // like:
        // mutation operator
        // crossover operator
        public int Seed {  get; set; }
        public int GenerationCount { get; set; }
        public IEnumerable<GeneticInstance> Instances { get; set; }

        public GeneticAlgorithm(IGeneticFunction function, IGeneticSelector selector, int? seed=null, int instance_count=64)
        {
            _function = function;
            _selector = selector;
            Seed = seed is null ? new Random().Next() : seed.Value;
            // fill it up later
            Instances = new List<GeneticInstance>(instance_count);

        }
    }
}
