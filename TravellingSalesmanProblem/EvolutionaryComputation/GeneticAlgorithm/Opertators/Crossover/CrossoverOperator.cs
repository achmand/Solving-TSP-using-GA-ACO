using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover
{
    /// <summary>
    /// Base class for the crossover operators used in GA. Crossover/Reproduction is the stage in a GA were the programming of a chromosome is varied from one generation to the next.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the crossover process.</typeparam>
    public abstract class CrossoverOperator<T>
    {
        #region properties

        /// <summary>
        /// The crossover operator type used in the concrete implementation. 
        /// </summary>
        public abstract CrossoverType CrossoverType { get; }

        #endregion properties

        #region public methods 

        public Chromosome<T> Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome)
        {
            return _Crossover(fatherChromosome, motherChromosome);
        }

        #endregion public methods

        #region private methods 

        protected abstract Chromosome<T> _Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome);

        #endregion private methods
    }
}
