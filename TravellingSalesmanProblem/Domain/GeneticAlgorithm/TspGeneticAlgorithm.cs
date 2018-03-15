namespace Domain.GeneticAlgorithm
{
    // testing commit //
    public class TspGeneticAlgorithm
    {
        #region properties 

        public int Generation { get; private set; }

        private float MutationRate { get; }

        private int Elitism { get; }

        #endregion properties 

        #region constructor/s

        public TspGeneticAlgorithm()
        {
            Generation = 0;
            MutationRate = 0.3f;
            Elitism = 6; 
        }

        public TspGeneticAlgorithm(float mutationRate, int elitisim)
        {
            Generation = 0;
            MutationRate = mutationRate;
            Elitism = elitisim;
        }

        #endregion constructor/s

        public void Test()
        {
            Generation++;
            var x = MutationRate * 2;
        }
    }
}
