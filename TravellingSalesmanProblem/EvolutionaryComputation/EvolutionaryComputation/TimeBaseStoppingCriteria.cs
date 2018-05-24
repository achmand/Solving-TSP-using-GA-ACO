using System;

namespace EvolutionaryComputation.EvolutionaryComputation
{
    /// <summary>
    /// A time based stopping criteria. This stops once the specified minutes have passed. 
    /// This class implements <see cref="IStoppingCriteria"/>
    /// </summary>
    public sealed class TimeBaseStoppingCriteria : IStoppingCriteria
    {
        #region properties 

        /// <summary>
        /// The starting time when we started to check.
        /// </summary>
        private DateTime? StartingTime { get; set; }

        /// <summary>
        /// The minutes which have to pass until the stopping criteria is reached.
        /// </summary>
        private double MinutesPassed { get; }

        /// <summary>
        /// The stopping criteria type. 
        /// </summary>
        public StoppingCriteriaType StoppingCriteria => StoppingCriteriaType.TimeBased;

        #endregion properties 

        #region constructor/s 

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="minutesPassed">The minutes passed until the stopping criteria is met.</param>
        public TimeBaseStoppingCriteria(double minutesPassed)
        {
            MinutesPassed = minutesPassed;
        }

        #endregion constructor/s 

        #region public methods  

        /// <summary>
        /// Checks if criteria threshold is reach or not.
        /// </summary>
        /// <returns>Returns true if threshold is reach, false otherwise.</returns>
        public bool IsCriteriaMet()
        {
            // if first time checking start time 
            if (StartingTime == null)
            {
                StartingTime = DateTime.UtcNow;
            }

            var addedMinutes = StartingTime.Value.AddMinutes(MinutesPassed);
            var isCriteriaMet = addedMinutes < DateTime.UtcNow;

            return isCriteriaMet;
        }

        /// <summary>
        /// Returns the stopping criteria information as a string.
        /// </summary>
        /// <returns>Criteria information.</returns>
        public string CriteriaToString()
        {
            return $"Stopping Criteria Type: {StoppingCriteria}, Minutes to pass: {MinutesPassed}";
        }

        #endregion public methods 
    }
}
