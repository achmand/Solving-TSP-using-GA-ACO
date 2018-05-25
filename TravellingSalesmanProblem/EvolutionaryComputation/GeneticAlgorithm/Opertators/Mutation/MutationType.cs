/* NOTES:
- For more information on mutation methods visit:
https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)

- For some examples of different mutation methods used in GA visit:
https://www.tutorialspoint.com/genetic_algorithms/genetic_algorithms_mutation.htm
 */

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation
{
    /// <summary>
    /// Different types of mutation methods used in a genetic algorithm GA. 
    /// </summary>
    public enum MutationType
    {
        /// <summary>
        /// No mutation method at all. 
        /// </summary>
        None,
        /// <summary>
        /// Single swap mutation operator. 
        /// </summary>
        SingleSwap,
        /// <summary>
        /// Inversion mutation operator.
        /// </summary>
        InversionMutation,
    }
}
