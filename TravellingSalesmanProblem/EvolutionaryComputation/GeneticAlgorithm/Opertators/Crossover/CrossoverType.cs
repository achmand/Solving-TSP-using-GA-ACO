namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover
{
    /* NOTES:
    - For more information on crossover methods visit:
    https://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)

    - For some examples of different crossover methods used in GA visit:
    http://ictactjournals.in/paper/IJSC_V6_I1_paper_4_pp_1083_1092.pdf
    */

    /// <summary>
    /// Different types of crossover methods used in a genetic algorithm GA.
    /// </summary>
    public enum CrossoverType
    {
        /// <summary>
        /// No crossover method at all. 
        /// </summary>
        None = 0,
        /// <summary>
        /// Partially-mapped crossover operator (PMX).
        /// </summary>
        Pmx = 1,
        /// <summary>
        /// Order 1 crossover operator. 
        /// </summary>
        OrderOne = 2,
        /// <summary>
        /// Cycle crossover operator. 
        /// </summary>
        Cycle,
    }
}
