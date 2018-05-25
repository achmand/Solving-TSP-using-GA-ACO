/* NOTES:
   - For more information on this selection method visit:
   https://en.wikipedia.org/wiki/Fitness_proportionate_selection
   http://www.edc.ncl.ac.uk/highlight/rhjanuary2007g02.php

    - Selection Methods for Genetic Algorithms (PDF Download Available).
    - Available from: https://www.researchgate.net/publication/259461147_Selection_Methods_for_Genetic_Algorithms [accessed Mar 16 2018].
    a well-known drawback of this technique is the risk of premature convergence of the GA to a local optimum,  
    due to the possible presence of a dominant individual that always wins the competition and is selected as a parent. 
*/

using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection
{
    /// <summary>
    /// Generic implementation for the roulette wheel selection (RWS) a.k.a 'Fitness proportionate selection'. A genetic operator used in GA to select potentially useful solutions for recombination. 
    /// </summary>
    public sealed class RouletteWheelSelection<T> : SelectionOperator<T>
    {
        #region properties 

        /// <summary>
        /// The selection operator type for this specific selection implementation (RWS).
        /// </summary>
        public override SelectionType SelectionType => SelectionType.Rws;

        #endregion properties

        #region constructor/s 

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="populationSize">The population size.</param>
        /// <param name="random">An instance of random used in the <see cref="SelectionOperator{T}"/>.</param>
        public RouletteWheelSelection(int populationSize, Random random) : base(populationSize, random) { }

        #endregion constructor/s

        #region private methods 

        /// <summary>
        /// Selects the next candidate from the population using RWS, to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate <see cref="Chromosome{T}"/> selected by the selection method which will be used for breeding/crossover process.</returns>
        protected override Chromosome<T> _PopulationSelection(Population<T> population)
        {
            if (!FitnessNormalized)
            {
                NormalizeFitness(population);
            }

            var index = 0;
            var r = Random.NextDouble();
            while (r > 0)
            {
                r -= NormalizedFitness[index];
                index++;
            }

            index--;
            return population.Chromosomes[index];
        }

        /// <summary>
        /// Normalizes fitness to values between 0 and 1. 
        /// </summary>
        /// <param name="population">The current <see cref="Population{T}"/> which will be used in the normalization stage.</param>
        private void NormalizeFitness(Population<T> population)
        {
            var chromosomes = population.Chromosomes;
            var totalFitness = population.TotalFitness;

            for (var i = 0; i < population.PopulationSize; i++)
            {
                NormalizedFitness[i] = chromosomes[i].Fitness / totalFitness;
            }

            FitnessNormalized = true;
        }

        /// <summary>
        /// Preparing the selection operator for the next generation.
        /// </summary>
        protected override void _SetNextGeneration()
        {
            // setting this to false, will call the normalize fitness again thus overwriting the previous values
            FitnessNormalized = false;
        }

        #endregion private methods
    }
}
