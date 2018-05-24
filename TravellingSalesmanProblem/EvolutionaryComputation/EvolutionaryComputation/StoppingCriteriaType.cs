namespace EvolutionaryComputation.EvolutionaryComputation
{
    /// <summary>
    /// The stopping criteria types.
    /// </summary>
    public enum StoppingCriteriaType
    {
        /// <summary>
        /// Time based, algorithm stop until some time has passed. 
        /// </summary>
        TimeBased, 
        /// <summary>
        /// Iteration specific, algorithm will stop until a maximum iteration is reached. 
        /// </summary>
        SpecifiedIterations, 
    }
}
