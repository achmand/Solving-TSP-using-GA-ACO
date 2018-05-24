namespace EvolutionaryComputation.EvolutionaryComputation
{
    /// <summary>
    /// An iteration stopping criteria. This stops once a max iteration is reach. 
    /// This class implements <see cref="IStoppingCriteria"/>
    /// </summary>
    public sealed class IterationStoppingCriteria : IStoppingCriteria
    {
        #region properties

        /// <summary>
        /// The current iteration.
        /// </summary>
        private int CurrentIteration { get; set; }

        /// <summary>
        /// The maximum iteration that it has to reach so that the stopping criteria is met.
        /// </summary>
        private int MaxIteration { get; }

        /// <summary>
        /// The stopping criteria type. 
        /// </summary>
        public StoppingCriteriaType StoppingCriteria => StoppingCriteriaType.SpecifiedIterations;

        #endregion properties 

        #region constructor/s

        /// <summary>
        /// Constructor with parameters. 
        /// </summary>
        /// <param name="maxIteration">The maximum iteration which has to be reach so the stopping criteria is met.</param>
        public IterationStoppingCriteria(int maxIteration)
        {
            CurrentIteration = 0;
            MaxIteration = maxIteration;
        }

        #endregion constructor/s

        #region public methods  

        /// <summary>
        /// Checks if criteria threshold is reach or not.
        /// </summary>
        /// <returns>Returns true if threshold is reach, false otherwise.</returns>
        public bool IsCriteriaMet()
        {
            var isCriteriaMet =  CurrentIteration > MaxIteration;
            CurrentIteration++;

            return isCriteriaMet;
        }

        /// <summary>
        /// Returns the stopping criteria information as a string.
        /// </summary>
        /// <returns>Criteria information.</returns>
        public string CriteriaToString()
        {
            return $"Stopping Criteria Type: {StoppingCriteria}, Maximum Iteration: {MaxIteration}";
        }

        #endregion
    }
}
