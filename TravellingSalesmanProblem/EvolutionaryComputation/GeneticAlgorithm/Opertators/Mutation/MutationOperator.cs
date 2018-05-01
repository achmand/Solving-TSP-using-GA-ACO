using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    public abstract class MutationOperator<T>
    {
        #region properties 

        /// <summary>
        /// The mutation operator type used in the concrete implementation. 
        /// </summary>
        public abstract MutationType MutationType { get; }

        protected Random Random;

        #endregion properties 

        #region constructor/s 

        protected MutationOperator(Random random)
        {
            Random = random;
        }

        #endregion constructor/s  

        #region public methods 

        public void Mutate(Chromosome<T> childChromosome)
        {
            _Mutate(childChromosome);
        }

        #endregion public methods

        #region private methods

        protected abstract void _Mutate(Chromosome<T> childChromosome);

        #endregion private methods
    }
}
