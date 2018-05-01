namespace EvolutionaryComputation.GeneticAlgorithm.Common
{
    public sealed class Chromosome<T>
    {
        #region properties 

        public T[] Genome { get; }

        public int GenomeLength => Genome.Length;

        public double Fitness { set; get; }

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
    }
}
