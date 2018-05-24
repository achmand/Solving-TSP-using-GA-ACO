namespace EvolutionaryComputation.EvolutionaryComputation
{
    public interface IStoppingCriteria
    {
        #region method signatures 

        StoppingCriteriaType StoppingCriteria { get; }

        bool IsCriteriaMet();

        #endregion method signatures 
    }
}
