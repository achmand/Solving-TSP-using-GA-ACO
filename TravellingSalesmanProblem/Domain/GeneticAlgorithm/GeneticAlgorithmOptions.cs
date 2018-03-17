using Domain.GeneticAlgorithm.CrossoverMethods;
using Domain.GeneticAlgorithm.SelectionMethods;

/* 
* <author>Dylan Vassallo</author>
* <date>12/03/2018</date>
*/

namespace Domain.GeneticAlgorithm
{
    /// <summary>
    /// An object which holds different genetic algorithm options. 
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Chromosome{T}"/> used in the Genetic algorithm.</typeparam>
    public sealed class GeneticAlgorithmOptions<T>
    {
        #region properties & fields 

        public EncodingType EncodingType { get; set; }

        /// <summary>
        /// The selection method used in the GA. 
        /// </summary>
        public SelectionMethodType SelectionMethod { get; set; }

        public CrossoverOperator CrossoverOperator { get; set; }

        /// <summary>
        /// The population size used by the GA's population.
        /// </summary>
        public int PopulationSize { get; }

        /// <summary>
        /// The mutation rate used in the GA.
        /// </summary>
        private float MutationRate { get; }

        private float Elitism { get; }

        #endregion properties & fields

        #region constructor/s 

        public GeneticAlgorithmOptions()
        {
            EncodingType = EncodingType.Permutation;
            SelectionMethod = SelectionMethodType.Rws;
            CrossoverOperator = CrossoverOperator.Pmx;
            PopulationSize = 20;
            MutationRate = 0.3f;
            Elitism = 0;
        }

        public GeneticAlgorithmOptions(SelectionMethodType selectionMethod, CrossoverOperator crossoverOperator, int populationSize, float mutationRate, float elitism)
        {
            SelectionMethod = selectionMethod;
            CrossoverOperator = crossoverOperator;
            PopulationSize = populationSize;
            MutationRate = mutationRate;
            Elitism = elitism > 1f ? 1f : elitism;
        }

        #endregion constructor/s 

        #region method/s 

        #region private method/s 
        
        #endregion private method/s

        #endregion method/s 
    }
}