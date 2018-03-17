using Domain.GeneticAlgorithm.SelectionMethods;

/* 
* <author>Dylan Vassallo</author>
* <date>17/03/2018</date>
*/

/* NOTES:
    - Goldberg and Lingle (1985)
    https://www.researchgate.net/figure/Partially-mapped-crossover-operator-PMX_fig1_226665831
    http://user.ceng.metu.edu.tr/~ucoluk/research/publications/tspnew.pdf (PMX section includes pros and cons)
*/

// TODO -> Comments and more notes
// TODO -> There must be a check to make sure that PMX is only used when permutation encoding is used 
namespace Domain.GeneticAlgorithm.CrossoverMethods
{
    public sealed class PartiallyMapped<T> : IChrossoverMethod<T>
    {
        #region properties & fields 

        /// <summary>
        /// The crossover operator for this specific crossover implementation (PMX).
        /// </summary>
        public CrossoverOperator CrossoverOperator => CrossoverOperator.Pmx;

        #endregion properties & fields  

        #region methods 

        #region public methods 

        // TODO -> This specific crossover returns two children instead of one 

        public Chromosome<T> Crossover(ISelectionMethod<T> selectionMethod)
        {

            return null;
        }

        #endregion public methods 

        #endregion methods
    }
}
