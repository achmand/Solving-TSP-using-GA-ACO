using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    /* NOTES:
    - For more information on single swap mutation visit:
    http://www.rubicite.com/Tutorials/GeneticAlgorithms/MutationOperators/SingleSwapMutationOperator.aspx
    */

    /// <summary>
    /// A concrete implementation of a Single Swap Mutation operator. This class inherits from <see cref="MutationOperator{T}"/>.
    /// Selects two positions at random from the genome of the <see cref="Chromosome{T}"></see> a interchange it's values. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SingleSwapMutation<T> : MutationOperator<T>
    {
        #region properties 

        /// <summary>
        /// The mutation operator type used in the concrete implementation (SingleSwap). 
        /// </summary>
        public override MutationType MutationType => MutationType.SingleSwap;

        #endregion properties

        #region constructor/s 

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="random">An instance of random used in the <see cref="MutationOperator{T}"/>.</param>
        public SingleSwapMutation(Random random) : base(random) { }

        #endregion constructor/s

        #region private methods

        /// <summary>
        /// Mutates a chromosome to maintain genetic diversity in a population.
        /// </summary>
        /// <param name="childChromosome">The child <see cref="Chromosome{T}"/> which will be mutated to maintain diversity.</param>
        protected override void _Mutate(Chromosome<T> childChromosome)
        {
            var genomeLength = childChromosome.GenomeLength;

            var indexA = 0;
            var indexB = 0;
            while (indexA == indexB)
            {
                indexA = Random.Next(0, genomeLength);
                indexB = Random.Next(0, genomeLength);
            }

            var gene = childChromosome.GetGene(indexA);
            childChromosome.InsertGene(indexA, childChromosome.GetGene(indexB));
            childChromosome.InsertGene(indexB, gene);
        }

        #endregion private methods
    }
}
