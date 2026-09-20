
namespace GenAlgLab_1_1
{
    // турнирный метод селекции
    // вообще не помню что там было
    public class TournamentSelector : IGeneticSelector
    {
        IEnumerable<GeneticInstance> IGeneticSelector.Select(
            IEnumerable<GeneticInstance> instances, int count, Random? rng = null)
        {
            throw new NotImplementedException();
        }
    }
}
