using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    /* NOTES:
    - For more information on single swap mutation visit:
    http://www.rubicite.com/Tutorials/GeneticAlgorithms/MutationOperators/SingleSwapMutationOperator.aspx
    */

    public sealed class SingleSwapMutation<T> : MutationOperator<T>
    {
        #region properties 

        /// <summary>
        /// The mutation operator type used in the concrete implementation (SingleSwap). 
        /// </summary>
        public override MutationType MutationType => MutationType.SingleSwap;

        #endregion properties

        #region constructor/s 

        public SingleSwapMutation(Random random) : base(random) { }

        #endregion constructor/s

        #region private methods

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

            //var gene = childChromosome.Genome[indexA];
            //childChromosome.Genome[indexA] = childChromosome.Genome[indexB];
            //childChromosome.Genome[indexB] = gene;

            var gene = childChromosome.GetGene(indexA);
            childChromosome.InsertGene(indexA, childChromosome.GetGene(indexB));
            childChromosome.InsertGene(indexB, gene);
        }

        #endregion private methods
    }
}
