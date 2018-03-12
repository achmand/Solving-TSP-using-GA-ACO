namespace Domain.Common
{
    /* 
     * <author>Dylan Vassallo</author>
     * <date>12/03/2018</date>
    */

    /* NOTES
        - For more information on GA visit: https://en.wikipedia.org/wiki/Genetic_algorithm
        - For more information on ACO visit: https://en.wikipedia.org/wiki/Ant_colony_optimization_algorithms
     */

    /// <summary>
    /// Types of solutions used to solve the TSP (Travelling Salesman Problem). 
    /// </summary>
    public enum TspSolution
    {
        /// <summary>
        /// Genetic Algorithm solution. 
        /// </summary>
        Ga,
        /// <summary>
        /// Ant colony optimization solution. 
        /// </summary>
        Aco
    }
}
