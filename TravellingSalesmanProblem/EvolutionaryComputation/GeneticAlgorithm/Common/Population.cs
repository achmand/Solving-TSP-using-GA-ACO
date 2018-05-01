using System;

namespace EvolutionaryComputation.GeneticAlgorithm.Common
{
    /// <summary>
    /// Generic implementation for a population used in GA, which holds a subset of solution for the current generation. 
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Chromosome{T}"/> used in the population.</typeparam>
    public sealed class Population<T>
    {
        #region properties 

        /// <summary>
        /// Holds all the chromosomes for the current population. 
        /// </summary>
        public Chromosome<T>[] Chromosomes { get; set; }

        public double HighestFitness { get; private set; }
        public int HighestFitnessIndex { get; private set; }

        /// <summary>
        /// Gets the population size.
        /// </summary>
        public int PopulationSize => Chromosomes.Length;

        #endregion

        #region constructor/s

        /// <summary>
        /// Constructor with params. 
        /// </summary>
        /// <param name="populationSize">The population size for the current population.</param>
        public Population(int populationSize)
        {
            Chromosomes = new Chromosome<T>[populationSize];
        }

        #endregion constructor/s

        #region public methods 
        /// <summary>
        /// Adds a chromsome to the population at the specified index.
        /// </summary>
        /// <param name="index">The index to add the chromsome at.</param>
        /// <param name="chromosome">The chromsome to be added.</param>
        public void AddChromosome(int index, Chromosome<T> chromosome)
        {
            if (index >= PopulationSize || index < 0)
            {
                throw new Exception("Index does not exist.");
            }

            var fitness = chromosome.Fitness;
            if (HighestFitness < fitness)
            {
                HighestFitness = fitness;
                HighestFitnessIndex = index;
            }

            Chromosomes[index] = chromosome;
        }

        #endregion public methods 
    }
}
