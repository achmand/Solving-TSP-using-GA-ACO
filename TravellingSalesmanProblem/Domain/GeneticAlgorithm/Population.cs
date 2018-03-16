namespace Domain.GeneticAlgorithm
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>16/03/2018</date>
    */

    /// <summary>
    /// A generic implementation for a population used in GA, which holds a subset of solution for the current generation. 
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Chromosome{T}"/> used in the population.</typeparam>
    public sealed class Population<T>
    {
        #region properties & fields 

        /// <summary>
        /// Holds all the chromosomes for the current population (candidate solution). 
        /// </summary>
        private Chromosome<T>[] Chromosomes { get; set; }

        //public int Generation { get; set; }

        /// <summary>
        /// Gets the population size. 
        /// </summary>
        public int PopulationSize { get; }

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
    }
}
