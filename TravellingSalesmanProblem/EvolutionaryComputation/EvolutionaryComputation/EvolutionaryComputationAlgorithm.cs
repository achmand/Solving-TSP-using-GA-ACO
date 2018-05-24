namespace EvolutionaryComputation.EvolutionaryComputation
{
    /// <summary>
    /// A base class for all evolutionary computation algorithms. 
    /// </summary>
    public abstract class EvolutionaryComputationAlgorithm
    {
        #region properties 

        /// <summary>
        /// The Evolutionary Computation Algorithm Type used in the concrete implementation. 
        /// </summary>
        public abstract EvolutionaryComputationType EvolutionaryComputationType { get; }

        #endregion properties 
    }
}
