/* NOTES:
   - For more information on this selection method visit:
    https://en.wikipedia.org/wiki/Tournament_selection
    http://mnemstudio.org/genetic-algorithms-selection.htm
*/

using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.Utilities;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection
{
    /// <summary>
    /// Generic implementation for the Tournament selection (TOS). A genetic operator used in GA to select potentially useful solutions for recombination. 
    /// </summary>
    public sealed class TournamentSelection<T> : SelectionOperator<T>
    {
        #region properties 

        /// <summary>
        /// The selection operator type for this specific selection implementation (TOS).
        /// </summary>
        public override SelectionType SelectionType => SelectionType.Tos;

        /// <summary>
        /// The tournament size calculated by the populationSize * tournamentSizePercentage which are passed in the parameter. 
        /// </summary>
        private int TournamentSize { get; }

        /// <summary>
        /// A set which holds the indexes for the participants. Using hashset to get constant time lookup O(1).  
        /// </summary>
        private HashSet<int> TournamentParticipantIndexes { get; }

        #endregion properties 

        #region constructor/s

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="populationSize">The population size.</param>
        /// <param name="random">An instance of random used in the <see cref="SelectionOperator{T}"/>.</param>
        /// <param name="tournamentSizePercentage">The tournament size percentage which is calculated using PopulationSize * percentage.</param>
        public TournamentSelection(int populationSize, Random random, double tournamentSizePercentage) : base(populationSize, random)
        {
            TournamentSize = (int)(tournamentSizePercentage * populationSize);
            if (TournamentSize >= populationSize)
            {
                throw new Exception("The tournament size must be less than the population size make sure to set the tournament size percentage smaller than 1.");
            }

            if (TournamentSize <= 1)
            {
                TournamentSize = 2;
            }

            TournamentParticipantIndexes = new HashSet<int>(Integer32EqualityComparer.Default);
        }

        #endregion constructor/s

        #region private methods 

        /// <summary>
        /// Selects the next candidate from the population using TOS, to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate <see cref="Chromosome{T}"/> selected by the selection method which will be used for breeding/crossover process.</returns>
        protected override Chromosome<T> _PopulationSelection(Population<T> population)
        {
            var populationSize = population.PopulationSize;
            var tournamentParticipants = new Chromosome<T>[TournamentSize];

            GetUniqueIntValues(TournamentSize, 0, populationSize);

            var counter = 0;
            for (int i = 0; i < populationSize; i++)
            {
                if (TournamentParticipantIndexes.Contains(i))
                {
                    tournamentParticipants[counter] = population.Chromosomes[i];
                    counter++;
                }
            }

            var tournamentWinner = tournamentParticipants.OrderByDescending(c => c.Fitness).First();
            return tournamentWinner;
        }

        /// <summary>
        /// Gets an array of ints with unique values.
        /// </summary>
        /// <param name="size">The length of the array.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <returns>Returns a int array with unique values between the specified minimum and maximum.</returns>
        public void GetUniqueIntValues(int size, int minimumValue, int maximumValue)
        {
            var difference = maximumValue - minimumValue;
            if (difference < size)
            {
                throw new Exception("The difference between the maximum and minimum values must be greater than the size.");
            }

            TournamentParticipantIndexes.Clear();
            var orderedValues = Enumerable.Range(minimumValue, difference).ToList();
            for (int i = 0; i < size; i++)
            {
                var removeIndex = Random.Next(0, orderedValues.Count);
                TournamentParticipantIndexes.Add(orderedValues[removeIndex]);
                orderedValues.RemoveAt(removeIndex);
            }
        }

        /// <summary>
        /// Preparing the selection operator for the next generation.
        /// </summary>
        protected override void _SetNextGeneration() { }

        #endregion private methods 
    }
}
