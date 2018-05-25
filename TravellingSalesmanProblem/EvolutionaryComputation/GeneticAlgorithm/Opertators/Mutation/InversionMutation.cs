/* NOTES:
- For more information on single swap mutation visit:
http://www.rubicite.com/Tutorials/GeneticAlgorithms/MutationOperators/SingleSwapMutationOperator.aspx
http://mnemstudio.org/genetic-algorithms-mutation.htm
*/

using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.Utilities;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    /// <summary>
    /// A concrete imlementation of an Inversion Mutation operator. This class inherits from <see cref="MutationOperator{T}"/>. 
    /// Selects two random points from the genome of the <see cref="Chromosome{T}"></see> and reverse the genes between them.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Chromosome{T}"/> used in the population.</typeparam>
    public sealed class InversionMutation<T> : MutationOperator<T>
    {
        #region properties 

        /// <summary>
        /// The mutation operator type used in the concrete implementation (InversionMutation). 
        /// </summary>
        public override MutationType MutationType => MutationType.InversionMutation;

        /// <summary>
        /// The mutation rate. A value from 0 to 1. 
        /// </summary>
        private double MutationRate { get; }

        #endregion

        #region constructor/s

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="random">An instance of random used in the <see cref="MutationOperator{T}"/>.</param>
        /// <param name="mutationRate">The mutation rate. A value from 0 to 1.</param>
        public InversionMutation(Random random, double mutationRate) : base(random)
        {
            MutationRate = mutationRate;
        }

        #endregion constructor/s

        #region private methods

        /// <summary>
        /// Mutates a chromosome using InversionMutation to maintain genetic diversity in a population.
        /// </summary>
        /// <param name="childChromosome">The child <see cref="Chromosome{T}"/> which will be mutated to maintain diversity.</param>
        protected override void _Mutate(Chromosome<T> childChromosome)
        {
            var genomeLength = childChromosome.GenomeLength;
            var mutationRange = (int)Helpers.MapValue(0, 1, 0, genomeLength, MutationRate);

            var indexA = Random.Next(0, genomeLength);

            var tmpIndexB = indexA + mutationRange;
            var indexB = tmpIndexB >= genomeLength ? genomeLength - 1 : tmpIndexB;

            Array.Reverse(childChromosome.GetGenome(), indexA, (indexB - indexA) + 1);
        }

        #endregion
    }
}
