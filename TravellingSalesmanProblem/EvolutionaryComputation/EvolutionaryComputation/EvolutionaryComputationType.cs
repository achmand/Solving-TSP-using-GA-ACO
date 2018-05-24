namespace EvolutionaryComputation.EvolutionaryComputation
{
    /* NOTES:
    - For more information on evolutionary computation algorithms visit:
    https://en.wikipedia.org/wiki/Evolutionary_computation
    */

    /// <summary>
    /// Different types of algorithms which fall under the family of  evolutionary computation, 
    /// which are mostly inspired by biological evolution.
    /// </summary>
    public enum EvolutionaryComputationType
    {
        /// <summary>
        /// Genertic Algorithm. This algorithm reflects the process of natural selection where the 
        /// fittest individuals are selected for reproduction in order to produce offspring of the next generation.
        /// </summary>
        GeneticAlgorithm,
        /// <summary>
        /// Ant Colony Optimization. This algorithms is a probabilistic technique for solving computational problems
        /// which can be reduced to finding good paths through graphs.
        /// </summary>
        AntColonyOptimization
    }
}
