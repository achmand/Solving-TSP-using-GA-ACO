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

        public T[] Genome { private set; get; }

        public Chromosome(T[] genome)
        {
            Genome = genome;
        }
    }
}
