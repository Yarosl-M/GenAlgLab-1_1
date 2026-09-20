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
        public void Mutate(GeneticInstance instance, int count = 1, Random? rng = null);
    }
}
