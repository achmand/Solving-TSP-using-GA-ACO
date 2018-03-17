using System;

/* 
* <author>Dylan Vassallo</author>
* <date>16/03/2018</date>
*/

namespace Domain.GeneticAlgorithm
{
    /// <summary>
    /// Generic implementation for a population used in GA, which holds a subset of solution for the current generation. 
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Chromosome{T}"/> used in the population.</typeparam>
    public sealed class Population<T>
    {
        #region properties & fields 

        /// <summary>
        /// Holds all the chromosomes for the current population (candidate solution). 
        /// </summary>
        public Chromosome<T>[] Chromosomes { get; set; }

        //public int Generation { get; set; }

        /// <summary>
        /// Gets the population size. 
        /// </summary>
        public int PopulationSize { get; }

        public double TotalFitness { get; private set; }

        public double HighestFit { get; private set; }

        #endregion properties & fields 

        #region constructor/s

        /// <summary>
        /// Constructor with params. 
        /// </summary>
        /// <param name="populationSize">The population size for the current population.</param>
        public Population(int populationSize)
        {
            PopulationSize = populationSize;
            Chromosomes = new Chromosome<T>[populationSize];
        }

        #endregion constructor/s  

        #region method/s

        #region public method/s

        /// <summary>
        /// Inserts a chromsome at the specified index.
        /// </summary>
        /// <param name="index">The index to insert the chromsome at.</param>
        /// <param name="chromosome">The chromsome to be inserted.</param>
        public void InsertChromosome(int index, Chromosome<T> chromosome)
        {
            if (index >= PopulationSize || index < 0)
            {
                throw new Exception("Index does not exist.");
            }

            Chromosomes[index] = chromosome;
            
            // TODO -> Should the fitness be computed here ?????
            // TODO -> Should the fitness be computed here ?????
            // TODO -> Should the fitness be computed here ?????

            var fitness = chromosome.Fitness;
            TotalFitness += fitness;

            if (fitness > HighestFit)
            {
                // TODO -> Should I keep most fit index as a reference
                HighestFit = fitness;
            }
        }

        #endregion public method/s

        #endregion method/s
    }
}
