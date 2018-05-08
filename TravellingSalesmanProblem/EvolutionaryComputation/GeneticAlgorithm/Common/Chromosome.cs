using System;

namespace EvolutionaryComputation.GeneticAlgorithm.Common
{
    public sealed class Chromosome<T>
    {
        #region properties 

        private T[] Genome { get; }

        public int GenomeLength => Genome.Length;

        public double Fitness { set; get; }

        public double Distance { set; get; }

        #endregion properties

        #region constructors 

        public Chromosome(T[] genome)
        {
            Genome = genome;
        }

        public Chromosome(int genomeLength)
        {
            Genome = new T[genomeLength];
        }

        #endregion constructor/s 

        #region public methods 

        /// <summary>
        /// Gets the genome. 
        /// </summary>
        /// <returns>The genome for the current chromosome.</returns>
        public T[] GetGenome()
        {
            return Genome;
        }

        /// <summary>
        /// Gets the gene at the specified index.
        /// </summary>
        /// <param name="index">The specified index to get the gene from.</param>
        /// <returns>The gene at the specified index.</returns>
        public T GetGene(int index)
        {
            if (index < 0 || index >= GenomeLength)
            {
                throw new ArgumentOutOfRangeException($"Gene at index {index} does not exist.");
            }

            return Genome[index];
        }

        /// <summary>
        /// Insert/Replaces gene at the specified index. 
        /// </summary>
        /// <param name="index">The specified index to insert/replace gene.</param>
        /// <param name="gene">The gene which will be inserted/replaced.</param>
        public void InsertGene(int index, T gene)
        {
            if (index < 0 || index >= GenomeLength)
            {
                throw new ArgumentOutOfRangeException($"Gene at index {index} does not exist.");
            }

            Genome[index] = gene;

            // TODO -> Should I set fitness to 0 since genome was changed ?
        }

        #endregion public methods 
    }
}
