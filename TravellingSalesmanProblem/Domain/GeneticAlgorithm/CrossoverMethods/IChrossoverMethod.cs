using Domain.GeneticAlgorithm.SelectionMethods;

/* 
* <author>Dylan Vassallo</author>
* <date>17/03/2018</date>
*/

/* NOTES:
- For more information on crossover methods visit:
https://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)

- For some examples of different crossover methods used in GA visit:
http://ictactjournals.in/paper/IJSC_V6_I1_paper_4_pp_1083_1092.pdf
*/

// TODO -> More notes and finish documentation
namespace Domain.GeneticAlgorithm.CrossoverMethods
{
    /// <summary>
    /// Different types of crossover methods used in a genetic algorithm GA.
    /// </summary>
    public enum CrossoverOperator
    {
        /// <summary>
        /// No crossover method at all. 
        /// </summary>
        None = 0,
        /// <summary>
        /// Partially-mapped crossover operator (PMX).
        /// </summary>
        Pmx = 1
    }

    /// <summary>
    /// Method signature/s for the crossover methods used in GA. Crossover/Reproduction is the stage in a GA were the programming of a chromosome is varied from one generation to the next.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the crossover process.</typeparam>
    public interface IChrossoverMethod<T>
    {
        #region method signatures 

        /// <summary>
        /// The crossover operator used in the concrete implementation. 
        /// </summary>
        CrossoverOperator CrossoverOperator { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectionMethod"></param>
        /// <returns></returns>
        Chromosome<T> Crossover(ISelectionMethod<T> selectionMethod);

        #endregion method signatures
    }
}
