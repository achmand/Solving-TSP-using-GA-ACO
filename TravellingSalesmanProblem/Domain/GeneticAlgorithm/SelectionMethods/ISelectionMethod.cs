namespace Domain.GeneticAlgorithm.SelectionMethods
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>12/03/2018</date>
    */

    /* NOTES:
        - For more information on selection methods visit:
       https://en.wikipedia.org/wiki/Selection_(genetic_algorithm)

        - For some examples of different selection methods used in GA visit:
        https://www.researchgate.net/publication/259461147_Selection_Methods_for_Genetic_Algorithms
     */

    /// <summary>
    /// Method signature/s for the selection methods used in GA. Selection is the stage of a GA in which individual genomes/chromosomes are chosen from a population for later breeding.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the selection process.</typeparam>
    public interface ISelectionMethod<T>
    {
        #region method signatures 

        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate selected by the selection method which will be used for breeding/crossover process.</returns>
        Chromosome<T> PopulationSelection(Population<T> population);

        #endregion method signatures 
    }
}
