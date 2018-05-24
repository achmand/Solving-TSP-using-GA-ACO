using CommandLine;

namespace TSP
{
    /// <summary>
    /// Holds the parsed arguments.
    /// </summary>
    public sealed class TspOptions
    {
        #region properties 

        /// <summary>
        /// The path for the TSP instance file. 
        /// </summary>
        [Option('p', "path", Required = true, HelpText = "The TSP instance path. The file format must be .tsp and this program only works with symmetric TSP problems." +
                                                         "You can download Symmetric TSP Instance from http://elib.zib.de/pub/mp-testdata/tsp/tsplib/tsp/index.html " +
                                                         "and problems with Edge_Weight_Type of EUC_2D can only be computed by this program.")]
        public string TspInstancePath { get; set; }

        /// <summary>
        /// The algorithm which will be used to solve the tsp instance. 
        /// </summary>
        [Option('a', "algorithm", Required = true, HelpText = "The algorithm which will be used to solve the TSP problem. Options are GeneticAlgorithm, AntColonyOptimization.")]
        public string Algorithm { get; set; }

        #region algorithm options

        /// <summary>
        /// (ACO & GA Options) The total number of population. 
        /// </summary>
        [Option('P', "totalpopulation", Required = true, HelpText = "The total population, if using GA this means the total population of chromosomes , if ACO the total ants.")]
        public int Population { get; set; }

        /// <summary>
        /// The algorithm stopping criteria type. 
        /// </summary>
        [Option('s', "stoppingcriteria", Required = true, HelpText = "The algorithm stopping criteria. Options are TimeBased, SpecifiedIterations.")]
        public string StoppingCriteriaType { get; set; }

        /// <summary>
        /// The algorithm stopping criteria time in minutes. 
        /// </summary>
        [Option('t', "timeminutes", Required = false, HelpText = "The algorithm stopping criteria time in minutes (minutes passed to stop). This is required if TimeBased is selected.")]
        public double StoppingCriteriaTimeMinutes { get; set; }

        /// <summary>
        /// The algorithm stopping criteria maximum iterations. 
        /// </summary>
        [Option('i', "iterations", Required = false, HelpText = "The algorithm stopping criteria maximum iteration (iterations reached to stop). This is required if SpecifiedIterations is selected.")]
        public int StoppingCriteriaIteration { get; set; }

        #region ACO options

        /// <summary>
        /// (ACO Options) Alpha in ACO options. 
        /// </summary>
        [Option('A', "alpha", Required = false, HelpText = "(ACO) This will affect the importance of pheromone in the optimization process (Edge selection).")]
        public int Alpha { get; set; }

        /// <summary>
        /// (ACO Options) Beta in ACO options. 
        /// </summary>
        [Option('B', "Beta", Required = false, HelpText = "(ACO) This will affect the importance of desirability/distance in the optimization process (Edge selection).")]
        public int Beta { get; set; }

        /// <summary>
        /// (ACO Options) Rho in ACO options. 
        /// </summary>
        [Option('R', "rho", Required = false, HelpText = "(ACO) Evaporation Constant: Affects how much pheromone is removed in between steps in the process (Pheromone update).")]
        public double Rho { get; set; }

        /// <summary>
        /// (ACO Options) Q in ACO options. 
        /// </summary>
        [Option('Q', "acoq", Required = false, HelpText = "(ACO) Affects how much pheromone is deposited in between steps in the process (Pheromone update).")]
        public double Q { get; set; }

        #endregion ACO options 

        #region GA Options 

        /// <summary>
        /// (GA Options) The selection operator type. 
        /// </summary>
        [Option('N', "selectionop", Required = false, HelpText = "(GA) The selection operator type. Options are RWS.")]
        public string SelectionOperator { get; set; }

        /// <summary>
        /// (GA Options) The crossover operator type. 
        /// </summary>
        [Option('C', "crossoverop", Required = false, HelpText = "(GA) The crossover operator type. Options are Cycle, OrderOne.")]
        public string CrossoverOperator { get; set; }

        /// <summary>
        /// (GA Options) The mutation operator type. 
        /// </summary>
        [Option('M', "mutationop", Required = false, HelpText = "(GA) The mutation operator type. Options are SingleSwap.")]
        public string MutationOperator { get; set; }

        /// <summary>
        /// (GA Options) The mutation rate. 
        /// </summary>
        [Option('O', "mutationrate", Required = false, HelpText = "(GA) The mutation rate. Not all mutation operators use the mutation rate.")]
        public double MutationRate { get; set; }

        /// <summary>
        /// (GA Options) The elite rate. 
        /// </summary>
        [Option('E', "eliterate", Required = false, HelpText = "(GA) The elite rate. If set to 0 elitism is not used.")]
        public double ElitismRate { get; set; }

        #endregion GA Options

        #endregion algorithm options 

        #endregion properties
    }
}