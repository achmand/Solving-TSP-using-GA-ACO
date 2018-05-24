using System;
using EvolutionaryComputation.EvolutionaryComputation;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection;

namespace EvolutionaryComputation.GeneticAlgorithm
{
    public class GeneticAlgorithm<T> : EvolutionaryComputationAlgorithm
    {
        #region properties

        /// <summary>
        /// The evolutionary computation type for this specific concrete implemetation/algorithm (Genetic Algorithm).
        /// </summary>
        public override EvolutionaryComputationType EvolutionaryComputationType => EvolutionaryComputationType.GeneticAlgorithm;

        protected Random Random { get; private set; }

        /// <summary>
        /// The current <see cref="Population{T}"/> used in the GA. 
        /// </summary>
        protected Population<T> Population { get; private set; }

        /// <summary>
        /// The current generation.
        /// </summary>
        protected int Generation { get; set; }

        /// <summary>
        /// The current <see cref="GAOptions"/> used in the GA. 
        /// </summary>
        protected readonly GAOptions GaOptions;

        /// <summary>
        /// The <see cref="SelectionOperator{T}"/> used in the GA's selection stage. 
        /// </summary>
        protected SelectionOperator<T> SelectionOperator;

        /// <summary>
        /// The <see cref="CrossoverOperator{T}"/> used in the GA's crossover stage. 
        /// </summary>
        protected CrossoverOperator<T> CrossoverOperator;

        /// <summary>
        /// The <see cref="MutationOperator{T}"/> used in the GA's mutation stage. 
        /// </summary>
        protected MutationOperator<T> MutationOperator;

        protected bool UsingElitism => GaOptions.Elitism > 0;

        protected int NumberOfElite;

        /// <summary>
        /// The stopping criteria used to stop the iteration, once a condition is met. 
        /// </summary>
        protected IStoppingCriteria StoppingCriteria;

        #endregion properties 

        #region constructor/s

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GeneticAlgorithm()
        {
            GaOptions = new GAOptions();
            Initialize();
        }

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="gaOptions">The <see cref="GAOptions"/> used in the GA.</param>
        public GeneticAlgorithm(GAOptions gaOptions)
        {
            GaOptions = gaOptions;
            Initialize();
        }

        #endregion constructor/s

        #region public methods 

        #endregion public methods 

        #region private methods

        /// <summary>
        /// Initializes the GA object. 
        /// </summary>
        private void Initialize()
        {
            var populationSize = GaOptions.PopulationSize;
            Population = new Population<T>(populationSize);
            Random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF); // https://msdn.microsoft.com/en-us/library/ctssatww(v=vs.110).aspx
            Generation = 0;

            var elite = GaOptions.Elitism;
            NumberOfElite = (int)(populationSize * elite);
            ApplyOptions();
        }

        /// <summary>
        /// Sets the operators and other configuration used in the GA.
        /// </summary>
        private void ApplyOptions()
        {
            /*** setting the selection operator to the specified type ***/
            var selectionType = GaOptions.SelectionType;
            if (selectionType == SelectionType.None)
            {
                throw new Exception("Selection method cannot be none when setting the method.");
            }

            var populationSize = Population.PopulationSize;
            switch (selectionType)
            {
                case SelectionType.Rws:
                    SelectionOperator = new RouletteWheel<T>(populationSize, Random);
                    break;
            }
            /*** setting the selection operator to the specified type ***/

            /*** setting the crossover operator to the specified type ***/
            var crossoverType = GaOptions.CrossoverType;
            if (crossoverType == CrossoverType.None)
            {
                throw new Exception("Crossover method cannot be none when setting the method.");
            }

            switch (crossoverType)
            {
                case CrossoverType.OrderOne:
                    CrossoverOperator = new OrderOne<T>(Random);
                    break;
                case CrossoverType.Cycle:
                    CrossoverOperator = new CycleCrossover<T>(Random);
                    break;
            }
            /*** setting the crossover operator to the specified type ***/

            /*** setting the mutation operator to the specified type ***/
            var mutationType = GaOptions.MutationType;
            if (mutationType == MutationType.None)
            {
                throw new Exception("Mutation method cannot be none when setting the method.");
            }

            switch (mutationType)
            {
                case MutationType.SingleSwap:
                    MutationOperator = new SingleSwapMutation<T>(Random);
                    break;
            }
            /*** setting the mutation operator to the specified type ***/

            /*** setting the stopping criteria for the algorithm ***/
            var stoppingCriteriaOptions = GaOptions.StoppingCriteriaOptions;
            var stoppingCriteriaType = stoppingCriteriaOptions.StoppingCriteriaType;
            switch (stoppingCriteriaType)
            {
                case StoppingCriteriaType.TimeBased:
                    var minutesPassed = stoppingCriteriaOptions.MinutesPassed;
                    if (minutesPassed <= 0)
                    {
                        throw new Exception($"When using {stoppingCriteriaType}, the minutes passed must be larger than 0.");
                    }
                    StoppingCriteria = new TimeBaseStoppingCriteria(minutesPassed);
                    break;
                case StoppingCriteriaType.SpecifiedIterations:
                    var maximumIterations = stoppingCriteriaOptions.MaximumIterations;
                    if (maximumIterations <= 0)
                    {
                        throw new Exception($"When using {stoppingCriteriaType}, the max iterations must be larger than 0.");
                    }
                    StoppingCriteria = new IterationStoppingCriteria(maximumIterations);
                    break;
            }

            /*** setting the stopping criteria for the algorithm ***/
        }

        #endregion private methods


    }
}
