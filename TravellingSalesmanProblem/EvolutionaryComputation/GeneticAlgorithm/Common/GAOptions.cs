using EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection;

namespace EvolutionaryComputation.GeneticAlgorithm.Common
{
    /// <summary>
    /// The options used in the genetic algorithm. 
    /// </summary>
    public sealed class GAOptions
    {
        #region properties

        /// <summary>
        /// The population size used by the GA's population.
        /// </summary>
        public int PopulationSize { get; }

        /// <summary>
        /// The encoding type used in the chromosomes. 
        /// </summary>
        public EncodingType EncodingType { get; set; }

        /// <summary>
        /// The selection type used in the GA's selection stage. 
        /// </summary>
        public SelectionType SelectionType { get; set; }

        /// <summary>
        /// The crossover type used in the GA's crossover stage. 
        /// </summary>
        public CrossoverType CrossoverType { get; set; }

        /// <summary>
        /// The mutation type used in the GA's mutation stage. 
        /// </summary>
        public MutationType MutationType { get; set; }

        /// <summary>
        /// The mutation rate used in the GA.
        /// </summary>
        public double MutationRate { get; }

        /// <summary>
        /// How many elite chromsomes (percentage from populaton) should be kept unchanged for the next iteration. Must be a value from 0 to 1. 
        /// If the value is set to 1 non of he population will change, try to keep this value low. 
        /// </summary>
        public double Elitism { get; }

        /// <summary>
        /// The threshold which stops the GA from evolving.
        /// </summary>
        public IterationThreshold IterationThreshold { get; set; } // TODO -> Class should be used here 

        #endregion properties

        #region constructor/s

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GAOptions()
        {
            PopulationSize = 100;
            EncodingType = EncodingType.Permutation;
            SelectionType = SelectionType.Rws;
            CrossoverType = CrossoverType.Pmx;
            MutationType = MutationType.SingleSwap;
            MutationRate = 0.3f;
            Elitism = 0;

            IterationThreshold = IterationThreshold.SpecifiedGenerations;
        }

        /// <summary>
        /// Constructor with parameters. 
        /// </summary>
        /// <param name="populationSize">The population size.</param>
        /// <param name="encodingType">The encoding type used in the chromosome.</param>
        /// <param name="selectionType">The selection type.</param>
        /// <param name="crossoverType">The crossover type.</param>
        /// <param name="mutationType">The mutation type.</param>
        /// <param name="mutationRate">The mutation rate.</param>
        /// <param name="elitisim">The elistism rate.</param>
        /// <param name="iterationThreshold">The iteration threshold which stops the GA.</param>
        public GAOptions(int populationSize, EncodingType encodingType, SelectionType selectionType, CrossoverType crossoverType, MutationType mutationType, double mutationRate, double elitisim, IterationThreshold iterationThreshold)
        {
            PopulationSize = populationSize;
            EncodingType = encodingType;
            SelectionType = selectionType;
            CrossoverType = crossoverType;
            MutationType = mutationType;
            MutationRate = mutationRate;
            Elitism = elitisim;

            IterationThreshold = iterationThreshold;
        }

        #endregion constructor/s 
    }
}
