namespace Domain.GeneticAlgorithm
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>16/03/2018</date>
    */

    // TODO-> Do I need an interface IFitness ??
    // TODO -> Implement IComparable 

    public sealed class Chromosome<T>
    {
        public double Fitness { private set; get; }

        public T Genome { private set; get; }

        public Chromosome(T genome)
        {
            Genome = genome;
        }
    }
}
