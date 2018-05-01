using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection
{
    /// <summary>
    /// Base class for the selection operators used in GA. Selection is the stage in a GA were an individual genomes/chromosomes are chosen from a population for later breeding.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the selection process.</typeparam>
    public abstract class SelectionOperator<T>
    {
        #region properties 

        /// <summary>
        /// The selection operator type used in the concrete implementation. 
        /// </summary>
        public abstract SelectionType SelectionType { get; }

        protected Random Random;

        /// <summary>
        /// Holds a reference to the normalized fitness. 
        /// </summary>
        protected double[] NormalizedFitness;

        /// <summary>
        /// Is fitness normalized.
        /// </summary>
        protected bool FitnessNormalized;

        #endregion properties

        #region constructor/s 

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="populationSize">The population size.</param>
        /// <param name="random"></param>
        protected SelectionOperator(int populationSize, Random random)
        {
            NormalizedFitness = new double[populationSize];
            Random = random;
        }

        #endregion constructor/s 

        #region public methods

        /// <summary>
        /// Selects the next candidate from the population to be used for the breeding/crossover process. 
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate <see cref="Chromosome{T}"/> selected by the selection method which will be used for breeding/crossover process.</returns>
        public Chromosome<T> PopulationSelection(Population<T> population)
        {
            return _PopulationSelection(population);
        }

        public void SetNextGeneration()
        {
            FitnessNormalized = false; // setting this to false, will call the normalize fitness again thus overwriting the previous values
            _SetNextGeneration(); 
        }

        #endregion

        #region private methods 

        /// <summary>
        /// An abstract method for the population selection. This must be implemented in the child classes.
        /// </summary>
        /// <param name="population">The population from where the candidate (parent) will be selected.</param>
        /// <returns>The candidate <see cref="Chromosome{T}"/> selected by the selection method which will be used for breeding/crossover process.</returns>
        protected abstract Chromosome<T> _PopulationSelection(Population<T> population);

        protected abstract void _SetNextGeneration();

        #endregion private metods
    }
}
