using System;
using CommandLine;
using EvolutionaryComputation.AntColonyOptimization;
using EvolutionaryComputation.AntColonyOptimization.Common;
using EvolutionaryComputation.EvolutionaryComputation;
using EvolutionaryComputation.GeneticAlgorithm;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection;
using EvolutionaryComputation.Utilities;

namespace TSP
{
    // TODO -> Implement different ways of distance functions 

    /* TODO -> Future work (Not for the assignment but for future work)
       -> Implement other evolutionary computation algorithms such as Particle swarm optimization, Swarm Intelligence, Artificial Bee Colony Algorithm, etc.. 
       -> Using floats instead of double could save up memory. 
       -> Use object pooling to save for optimization (GC).

       Genetic Algorithm
            -> Some code refactoring is needed between the GeneticAlgorithm base class and the TSPGeneticAlgorithm to insure it would be easy to implement GA for other problems. 
            -> Research and implement entropy in GA. 
        Ant Colony 
            -> Make use of generics to be able to use ACO for different problems. Similiar to GA's implementation.
    */

    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            const string testingPath = "C:/Users/Nalyd/Desktop/Current Projects/Github/Solving-TSP-using-GA-ACO/TravellingSalesmanProblem/TSP/TSPInstances/berlin52.tsp";
            var tspInstance = new TspInstance(testingPath);

            var stoppingCriteriaOptions = new StoppingCriteriaOptions
            {
                StoppingCriteriaType = StoppingCriteriaType.SpecifiedIterations,
                MaximumIterations = 500
            };

            var gaOptions = new GAOptions(1000, EncodingType.Permutation, SelectionType.Rws, CrossoverType.Cycle, MutationType.SingleSwap, 0.3, 0.3, stoppingCriteriaOptions);
            var tspGeneticAlgorithm = new TspGeneticAlgorithm(gaOptions, tspInstance);
            tspGeneticAlgorithm.Compute();


            //var acoOptions = new ACOOptions(4, 3, 2, 0.01, 2.00, stoppingCriteriaOptions);
            //var aco = new TspAntColonyOptimization(acoOptions, tspInstance);
            //aco.Compute();

            Console.ReadLine();

#else
            Parser.Default.ParseArguments<TspOptions>(args).WithParsed(SolveTsp);
#endif
        }

        /// <summary>
        /// Solves the TSP problem with the arguments passed from the console. 
        /// </summary>
        /// <param name="options">The parsed arguments as <see cref="TspOptions"/> .</param>
        private static void SolveTsp(TspOptions options)
        {
            var tspInstancePath = options.TspInstancePath;
            var tspInstance = new TspInstance(tspInstancePath);

            var algorithmValue = options.Algorithm;
            EvolutionaryComputationType algorithm;

            if (Enum.TryParse(algorithmValue, true, out algorithm))
            {
                var stoppingCriteriaValue = options.StoppingCriteriaType;
                StoppingCriteriaType stoppingCriteriaType;

                if (Enum.TryParse(stoppingCriteriaValue, true, out stoppingCriteriaType))
                {
                    var stoppingCriteriaOptions = new StoppingCriteriaOptions
                    {
                        StoppingCriteriaType = stoppingCriteriaType
                    };

                    switch (stoppingCriteriaType)
                    {
                        case StoppingCriteriaType.TimeBased:
                            stoppingCriteriaOptions.MinutesPassed = options.StoppingCriteriaTimeMinutes;
                            break;
                        case StoppingCriteriaType.SpecifiedIterations:
                            stoppingCriteriaOptions.MaximumIterations = options.StoppingCriteriaIteration;
                            break;
                    }

                    var population = options.Population;
                    switch (algorithm)
                    {
                        // using GA to solve TSP
                        case EvolutionaryComputationType.GeneticAlgorithm:

                            var selectionOperatorValue = options.SelectionOperator;
                            SelectionType selectionType;
                            if (!Enum.TryParse(selectionOperatorValue, true, out selectionType))
                            {
                                selectionType = SelectionType.Rws;
                            }

                            var crossoverOperatorType = options.CrossoverOperator;
                            CrossoverType crossoverType;
                            if (!Enum.TryParse(crossoverOperatorType, true, out crossoverType))
                            {
                                crossoverType = CrossoverType.Cycle;
                            }

                            var mutationOperatorType = options.MutationOperator;
                            MutationType mutationType;
                            if (!Enum.TryParse(mutationOperatorType, true, out mutationType))
                            {
                                mutationType = MutationType.SingleSwap;
                            }

                            var mutationRate = options.MutationRate;
                            var elitisimRate = options.ElitismRate;

                            var genticAlgorithmOptions = new GAOptions(population, EncodingType.Permutation, selectionType, crossoverType, mutationType, mutationRate, 
                                elitisimRate, stoppingCriteriaOptions);
                            var tspGeneticAlgorithm = new TspGeneticAlgorithm(genticAlgorithmOptions, tspInstance);

                            tspGeneticAlgorithm.Compute();
                            break;

                        // using ACO to solve TSP
                        case EvolutionaryComputationType.AntColonyOptimization:
                            var alpha = options.Alpha;
                            var beta = options.Beta;
                            var rho = options.Rho;
                            var q = options.Q;

                            var antColonyOptimizationOptions = new ACOOptions(population, alpha, beta, rho, q, stoppingCriteriaOptions);
                            var tspAntColonyOptimization = new TspAntColonyOptimization(antColonyOptimizationOptions,
                                tspInstance);

                            tspAntColonyOptimization.Compute();
                            break;
                    }
                }
                else
                {
                    throw new Exception("Stopping Criteria specified does not exist. Use --help for more information.");
                }
            }
            else
            {
                throw new Exception("Algorithm specified does not exist. Use --help for more information.");
            }

            Console.ReadLine();
        }
    }
}
