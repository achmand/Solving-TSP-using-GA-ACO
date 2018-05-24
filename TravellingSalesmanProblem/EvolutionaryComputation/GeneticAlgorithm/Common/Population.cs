using System;
using System.Linq;

namespace EvolutionaryComputation.GeneticAlgorithm.Common
{
    // TODO -> http://lipas.uwasa.fi/cs/publications/2NWGA/node11.html#estimate

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
        public Chromosome<T>[] Chromosomes { get; private set; }

        /// <summary>
        /// Highest fitness found in the population.
        /// </summary>
        public double HighestFitness { get; private set; }

        /// <summary>
        /// Chromsome index with the highest fit.
        /// </summary>
        public int HighestFitnessIndex { get; private set; }

        /// <summary>
        /// Total population fitness.
        /// </summary>
        public double TotalFitness { get; private set; }

        public Chromosome<T>[] NextGenChromosomes { get; }

        public double NextGenHighestFitness { get; private set; }

        public int NextGenHighestFitnessIndex { get; private set; }

        public double NextGenTotalFitness { get; private set; }

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
            NextGenChromosomes = new Chromosome<T>[populationSize];
        }

        #endregion constructor/s

        #region public methods 

        // TODO -> This should not be like this 
        public void SortByFitness()
        {
            Chromosomes = Chromosomes.OrderByDescending(c => c.Fitness).ToArray();
        }

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
            NextGenChromosomes[index] = chromosome;
            NextGenTotalFitness += fitness;

            if (NextGenHighestFitness < fitness)
            {
                NextGenHighestFitness = fitness;
                NextGenHighestFitnessIndex = index;
            }
        }


        public void SetNextGeneration()
        {
            HighestFitness = NextGenHighestFitness;
            HighestFitnessIndex = NextGenHighestFitnessIndex;
            TotalFitness = NextGenTotalFitness;

            NextGenHighestFitness = 0;
            NextGenHighestFitnessIndex = 0;
            NextGenTotalFitness = 0;

            Chromosomes = NextGenChromosomes; // set the current population to the next generation population
        }

        #endregion public methods 
    }
}
