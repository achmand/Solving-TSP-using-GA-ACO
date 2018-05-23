namespace EvolutionaryComputation.AntColonyOptimization.Common
{
    /* NOTES:
        Edge selection: https://wikimedia.org/api/rest_v1/media/math/render/svg/87876b3e1033b60f992d33a181bee4e2d7b229ab
        Pheromone update: https://wikimedia.org/api/rest_v1/media/math/render/svg/da75f512c94f2b2737112bebbf97539f5f6928c0
    */

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
        /// This will affect the importance of pheromone in the optimization process (Edge selection).
        /// </summary>
        public int Alpha { get; }

        /// <summary>
        /// This will affect the importance of desirability/distance in the optimization process (Edge selection). 
        /// </summary>
        public int Beta { get; }

        /// <summary>
        /// Evaporation Constant: Affects how much pheromone is removed in between steps in the process (Pheromone update).
        /// </summary>
        public double Rho { get; }

        /// <summary>
        /// Affects how much pheromone is deposited in between steps in the process (Pheromone update).
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
