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
        public Chromosome<T>[] Chromosomes { get; private set; }

        public double HighestFitness { get; private set; }

        public int HighestFitnessIndex { get; private set; }

        public Chromosome<T>[] NextGenChromosomes { get; }

        public double NextGenHighestFitness { get; private set; }

        public int NextGenHighestFitnessIndex { get; private set; }

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

        /// <summary>
        /// Adds a chromsome to the population at the specified index.
        /// </summary>
        /// <param name="index">The index to add the chromsome at.</param>
        /// <param name="chromosome">The chromsome to be added.</param>
        /// <param name="nextGen"></param>
        public void AddChromosome(int index, Chromosome<T> chromosome, bool nextGen = false)
        {
            if (index >= PopulationSize || index < 0)
            {
                throw new Exception("Index does not exist.");
            }

            var fitness = chromosome.Fitness;
            var currentHighestFitness = nextGen ? NextGenHighestFitness : HighestFitness;
            var registerNewHigh = currentHighestFitness < fitness;

            if (nextGen)
            {
                NextGenChromosomes[index] = chromosome;
                if (!registerNewHigh)
                {
                    return;
                }

                NextGenHighestFitness = fitness;
                NextGenHighestFitnessIndex = index;
            }
            else
            {
                Chromosomes[index] = chromosome;
                if (!registerNewHigh)
                {
                    return;
                }

                HighestFitness = fitness;
                HighestFitnessIndex = index;
            }
        }

        public void SetNextGeneration()
        {
            // TODO -> What if we are using elitisim ?

            HighestFitness = NextGenHighestFitness;
            HighestFitnessIndex = NextGenHighestFitnessIndex;

            NextGenHighestFitness = 0;
            NextGenHighestFitnessIndex = 0;

            Chromosomes = NextGenChromosomes; // set the current population to the next generation population
        }

        #endregion public methods 
    }
}
