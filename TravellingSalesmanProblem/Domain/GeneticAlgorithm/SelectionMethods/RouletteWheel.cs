namespace Domain.GeneticAlgorithm.SelectionMethods
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>12/03/2018</date>
    */

    /* NOTES:
       - For more information on this selection method visit:
       https://en.wikipedia.org/wiki/Fitness_proportionate_selection

        - Selection Methods for Genetic Algorithms (PDF Download Available).
        - Available from: https://www.researchgate.net/publication/259461147_Selection_Methods_for_Genetic_Algorithms [accessed Mar 16 2018].
        a well-known drawback of this technique is the risk of premature convergence of the GA to a local optimum,  
        due to the possible presence of a dominant individual that always wins the competition and is selected as a parent. 
    */

    /// <summary>
    /// Generic implementation for the roulette wheel selection (RWS) a.k.a 'Fitness proportionate selection'. <para/> A genetic operator used in GA to select potentially useful solutions for recombination. 
    /// </summary>
    public sealed class RouletteWheel<T> : ISelectionMethod<T>
    {
        #region methods 

        #region public methods 

        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process using RWS. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate selected by the selection method which will be used for breeding/crossover process.</returns>
        public Chromosome<T> PopulationSelection(Population<T> population)
        {

            return null; 
        }

        #endregion public methods

        #endregion method 
    }
}
