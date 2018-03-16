namespace Domain.GeneticAlgorithm
{
    // TODO -> Make a generic implementation for GA
    // TODO -> Proper comments 
    // TODO -> Check access modifiers
    // TODO -> Implement Different Selection methods 

    public class TspGeneticAlgorithm<T>
    {
        #region properties 

        public int Generation { get; private set; }

        private float MutationRate { get; }

        private int Elitism { get; }
        
        private Population<T> Population { get; set; }

        private int PopulationSize => Population.PopulationSize;

        /// <summary>
        /// Is the GA using elitism. 
        /// </summary>
        private bool UseElistim => Elitism > 0;

        #endregion properties 

        #region constructor/s

        public TspGeneticAlgorithm()
        {
            Generation = 0;
            MutationRate = 0.3f;
            Elitism = 6;
        }

        public TspGeneticAlgorithm(int populationSize, float mutationRate, int elitisim)
        {
            Generation = 0;
            MutationRate = mutationRate;
            Elitism = elitisim;

            Population = new Population<T>(populationSize);
        }

        #endregion constructor/s

        #region methods 

        #region private methods 

        #endregion private methods 

        #endregion methods  
    }
}
