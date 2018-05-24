namespace EvolutionaryComputation.EvolutionaryComputation
{
    /// <summary>
    /// The options used in the stopping criteria for a particular algorithm. 
    /// </summary>
    public sealed class StoppingCriteriaOptions
    {
        #region properties 

        /// <summary>
        /// The stopping criteria type used in specific algorithms where it needs a stopping criteria to operate.
        /// </summary>
        public StoppingCriteriaType StoppingCriteriaType { get; set; }

        /// <summary>
        /// The minutes which have to pass until the algorithm stops. This is only used when <see cref="StoppingCriteriaType"/> is set to TimeBased. 
        /// </summary>
        public double MinutesPassed { get; set; }

        /// <summary>
        /// The maximum iterations until the algorithm stops. This is only used when <see cref="StoppingCriteriaType"/> is set to SpecifiedIterations. 
        /// </summary>
        public int MaximumIterations { get; set; }

        #endregion properties 
    }
}
