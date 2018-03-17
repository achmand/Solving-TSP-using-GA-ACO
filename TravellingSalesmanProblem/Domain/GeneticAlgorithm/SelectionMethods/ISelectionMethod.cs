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

namespace Domain.GeneticAlgorithm.SelectionMethods
{
    /// <summary>
    /// Different types of selection methods used in a genetic algorithm GA.
    /// </summary>
    public enum SelectionMethodType
    {
        /// <summary>
        /// No selection method at all. 
        /// </summary>
        None = 0,
        /// <summary>
        /// Roulette Wheel Selection (RWS) a.k.a 'Fitness Proportionate Selection'.
        /// </summary>
        Rws,
    }

    /// <summary>
    /// Method signature/s for the selection methods used in GA. Selection is the stage in a GA were an individual genomes/chromosomes are chosen from a population for later breeding.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the selection process.</typeparam>
    public interface ISelectionMethod<T>
    {
        #region method signatures 

        /// <summary>
        /// The selection method type used in the concrete implementation. 
        /// </summary>
        SelectionMethodType MethodType { get; }

        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate selected by the selection method which will be used for breeding/crossover process.</returns>
        Chromosome<T> PopulationSelection(Population<T> population);

        #endregion method signatures 
    }
}
