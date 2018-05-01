using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover
{
    // this will work for permutation encoding 

    public sealed class OrderOne<T> : CrossoverOperator<T>
    {
        #region properties

        /// <summary>
        /// The crossover operator type for this specific selection implementation (Order1).
        /// </summary>
        public override CrossoverType CrossoverType => CrossoverType.OrderOne;

        #endregion properties

        #region private methods

        protected override Chromosome<T> _Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome)
        {
            throw new NotImplementedException();
        }

        #endregion private method
    }
}
