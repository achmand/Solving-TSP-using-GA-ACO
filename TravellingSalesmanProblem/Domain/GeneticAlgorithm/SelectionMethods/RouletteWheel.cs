using System;

/* 
  * <author>Dylan Vassallo</author>
  * <date>12/03/2018</date>
  */

/* NOTES:
   - For more information on this selection method visit:
   https://en.wikipedia.org/wiki/Fitness_proportionate_selection
   http://www.edc.ncl.ac.uk/highlight/rhjanuary2007g02.php

    - Selection Methods for Genetic Algorithms (PDF Download Available).
    - Available from: https://www.researchgate.net/publication/259461147_Selection_Methods_for_Genetic_Algorithms [accessed Mar 16 2018].
    a well-known drawback of this technique is the risk of premature convergence of the GA to a local optimum,  
    due to the possible presence of a dominant individual that always wins the competition and is selected as a parent. 
*/

namespace Domain.GeneticAlgorithm.SelectionMethods
{
    /// <summary>
    /// Generic implementation for the roulette wheel selection (RWS) a.k.a 'Fitness proportionate selection'. A genetic operator used in GA to select potentially useful solutions for recombination. 
    /// </summary>
    public sealed class RouletteWheel<T> : ISelectionMethod<T>
    {
        #region properties & fields 

        /// <summary>
        /// The method type for this specific selection implementation (RWS).
        /// </summary>
        public SelectionMethodType MethodType => SelectionMethodType.Rws;

        private double[] _normalizedFitness;

        private bool isFitnessNormalized;

        private Random _random;

        #endregion properties & fields

        #region methods 

        #region public methods 

        // TODO -> Reset stuff 
        
        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process using RWS. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate selected by the selection method which will be used for breeding/crossover process.</returns>
        public Chromosome<T> PopulationSelection(Population<T> population)
        {
            if (_normalizedFitness == null)
            {
                _normalizedFitness = new double[population.PopulationSize];
                _random = new Random();
            }

            if (!isFitnessNormalized)
            {
                NormalizeFitness(population);
            }

            var index = 0;
            var r = _random.NextDouble();
            while (r > 0)
            {
                r -= _normalizedFitness[index];
                index ++;
            }

            index --;
            return population.Chromosomes[index];
        }

        #endregion public methods

        #region private methods 

        private void NormalizeFitness(Population<T> population)
        {
            var chromosomes = population.Chromosomes;
            var highestFit = population.HighestFit;

            for (var i = 0; i < population.PopulationSize; i++)
            {
                _normalizedFitness[i] = chromosomes[i].Fitness / highestFit;
            }

            isFitnessNormalized = true;
        }

        #endregion private methods 

        #endregion method 
    }
}
