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
    // TODO -> Not Sure This is needed !!! if common methods are here this will be needed if not no need 

    /// <summary>
    /// A generic selection method base class from which all the selection methods inherit from. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SelectionMethod<T> : ISelectionMethod<T>
    {
        #region properties & fields 

        /// <summary>
        /// Selection method type which is set by the child class.
        /// </summary>
        protected abstract SelectionMethodType SelectionMethodType { get;  }

        /// <summary>
        /// Gets the selection method type set by the child class.
        /// </summary>
        public SelectionMethodType MethodType => SelectionMethodType;

        #endregion properties & fields 

        #region method/s

        #region public method/s

        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate selected by the selection method which will be used for breeding/crossover process.</returns>
        public Chromosome<T> PopulationSelection(Population<T> population)
        {
            return null;
        }

        #endregion public method/s

        #endregion method/s
    }
}
