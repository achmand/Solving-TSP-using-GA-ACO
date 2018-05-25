namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection
{
    /* NOTES:
    - For more information on selection methods visit:
    https://en.wikipedia.org/wiki/Selection_(genetic_algorithm)

    - For some examples of different selection methods used in GA visit:
    https://www.researchgate.net/publication/259461147_Selection_Methods_for_Genetic_Algorithms
    */
    
    public enum SelectionType
    {
        /// <summary>
        /// No selection method at all. 
        /// </summary>
        None = 0,
        /// <summary>
        /// Roulette Wheel Selection (RWS) a.k.a 'Fitness Proportionate Selection'.
        /// </summary>
        Rws,
        /// <summary>
        /// Tournament selection (TOS). A variant of rank-based selection methods. 
        /// </summary>
        Tos, 
    }
}
