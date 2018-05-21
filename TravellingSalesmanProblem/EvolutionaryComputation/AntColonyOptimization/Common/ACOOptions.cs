namespace EvolutionaryComputation.AntColonyOptimization.Common
{
    /// <summary>
    /// Options uses in the ant colony optimization. The variable names are named this way to be consistent with the original description of ACO.
    /// </summary>
    public sealed class ACOOptions
    {
        #region properties 

        /// <summary>
        /// The total number of ants used in the ACO. 
        /// </summary>
        public int TotalAnts { get; }
        
        /// <summary>
        /// Pheromone influence.
        /// </summary>
        public int Alpha { get; }

        /// <summary>
        /// Local node influence. 
        /// </summary>
        public int Beta { get; }

        /// <summary>
        /// Pheromone evaportation coefficient.
        /// </summary>
        public double Rho { get; }

        /// <summary>
        /// Pheromone deposit factor. 
        /// </summary>
        public double Q { get; }

        #endregion properties 

        #region constructor/s 

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public ACOOptions()
        {
            TotalAnts = 4; 
            Alpha = 3;
            Beta = 2;
            Rho = .01;
            Q = 2.0; 
        }

        /// <summary>
        /// Constructor with parameters. 
        /// </summary>
        /// <param name="totalAnts">The total number of ants used in the ACO.</param>
        /// <param name="alpha">Pheromone influence.</param>
        /// <param name="beta">Local node influence.</param>
        /// <param name="rho">Pheromone evaportation coefficient.</param>
        /// <param name="q">Pheromone deposit factor.</param>
        public ACOOptions(int totalAnts,int alpha, int beta, double rho, double q)
        {
            TotalAnts = totalAnts;
            Alpha = alpha;
            Beta = beta;
            Rho = rho;
            Q = q;
        }

        #endregion constructor/s
    }
}
