using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    /// <summary>
    /// Base class for the mutation operators used in GA. 
    /// Mutation is the stage in a GA were genetic diversity is applied through mutation.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the mutation process.</typeparam>
    public abstract class MutationOperator<T>
    {
        #region properties 

        /// <summary>
        /// The mutation operator type used in the concrete implementation. 
        /// </summary>
        public abstract MutationType MutationType { get; }

        /// <summary>
        /// An instance of Random used for probabilistic purposes.  
        /// </summary>
        protected Random Random;

        #endregion properties 

        #region constructor/s 

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="random">An instance of random used in the <see cref="MutationOperator{T}"/>.</param>
        protected MutationOperator(Random random)
        {
            Random = random;
        }

        #endregion constructor/s  

        #region public methods 

        /// <summary>
        /// Mutates a chromosome to maintain genetic diversity in a population.
        /// </summary>
        /// <param name="childChromosome">The child <see cref="Chromosome{T}"/> which will be mutated to maintain diversity.</param>
        public void Mutate(Chromosome<T> childChromosome)
        {
            _Mutate(childChromosome);
        }

        #endregion public methods

        #region private methods

        /// <summary>
        /// An abstract method for mutate. This must be implemented in child classes. 
        /// </summary>
        /// <param name="childChromosome">The child <see cref="Chromosome{T}"/> which will be mutated to maintain diversity.</param>
        protected abstract void _Mutate(Chromosome<T> childChromosome);

        #endregion private methods
    }
}
