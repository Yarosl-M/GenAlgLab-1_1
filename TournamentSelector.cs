
namespace GenAlgLab_1_1
{
    public class TournamentSelector : IGeneticSelector
    {
        
        IEnumerable<GeneticInstance> IGeneticSelector.Select(
            IEnumerable<GeneticInstance> instances, int count, int? seed)
        {
            throw new NotImplementedException();
        }
    }
}
