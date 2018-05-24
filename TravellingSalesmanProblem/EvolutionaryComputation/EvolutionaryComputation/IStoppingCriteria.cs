namespace EvolutionaryComputation.EvolutionaryComputation
{
    public interface IStoppingCriteria
    {
        #region method signatures 

        StoppingCriteriaType StoppingCriteria { get; }

        bool IsCriteriaMet();

        string CriteriaToString();

        #endregion method signatures 
    }
}
