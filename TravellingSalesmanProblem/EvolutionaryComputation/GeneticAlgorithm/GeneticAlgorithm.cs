using System;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection;

namespace EvolutionaryComputation.GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        #region properties

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
            Population = new Population<T>(GaOptions.PopulationSize);
            Generation = 0;
            SetOperators();
            Random = new Random();
        }

        /// <summary>
        /// Sets the operators used in the GA.
        /// </summary>
        private void SetOperators()
        {
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

            var crossoverType = GaOptions.CrossoverType;
            if (crossoverType == CrossoverType.None)
            {
                throw new Exception("Crossover method cannot be none when setting the method.");
            }

            switch (crossoverType)
            {
                //case SelectionType.Rws:
                //    SelectionOperator = new RouletteWheel<T>();
                //    break;
            }
        }

        #endregion private methods
    }
}
