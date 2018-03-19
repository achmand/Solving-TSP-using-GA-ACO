/* 
* <author>Dylan Vassallo</author>
* <date>16/03/2018</date>
*/

/* NOTES:
    - The reason why the chromosome is composed of a generic array is because some may encode the cities 
    as binary too (which can be an array of characters or 1's & 0's). And this could also be used whenever 
    a city is refered to by a letter rather than a number. 
    https://pdfs.semanticscholar.org/5101/c81247d581ca211b9cfb65a5430a381b2383.pdf

    - Different encoding schemes used in GA not necessarily for TSP only. 
    For TSP permutation encoding is commonly used. 
    http://www.garph.co.uk/ijarie/mar2013/1.pdf
*/

using System;
using System.Collections.Generic;

namespace Domain.GeneticAlgorithm
{
    // TODO-> Do I need an interface IFitness ??
    // TODO -> Implement IComparable 

    public enum EncodingType
    {
        Permutation,
        Binary
    }

    public sealed class Chromosome<T>
    {
        public double Fitness { private set; get; }

        private HashSet<T> _genomeSet;

        public T[] Genome { get; }

        public int GenomeLength => Genome.Length;

        // TODO -> Add equality comparer ??
        public Chromosome(T[] genome)
        {
            Genome = genome;
            _genomeSet = new HashSet<T>(genome);
        }

        public Chromosome(int genomeLength)
        {
            Genome = new T[genomeLength];
            _genomeSet = new HashSet<T>();
        }

        public void AddGene(T gene, int index)
        {
            Genome[index] = gene;
            _genomeSet.Add(gene);
        }

        public void CopyGenes(Chromosome<T> source, int sourceStart, int sourceEnd)
        {
            for (int i = sourceStart; i <= sourceEnd; i++)
            {
                var sourceGene = source.Genome[i];
                Genome[i] = sourceGene;

                _genomeSet.Add(sourceGene);
            }
        }

        public bool ContainsGene(T gene)
        {
            return _genomeSet.Contains(gene);
        }

        public override string ToString()
        {
            var genomeString = string.Join(",", Genome);
            return $"Genome: {genomeString}";
        }

    }
}
