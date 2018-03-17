using System;
using Domain.GeneticAlgorithm.CrossoverMethods;
using Domain.GeneticAlgorithm.SelectionMethods;

namespace Domain.GeneticAlgorithm
{
    // TODO -> Make a generic implementation for GA
    // TODO -> Proper comments 
    // TODO -> Check access modifiers
    // TODO -> Implement Different Selection methods 
    // TODO -> Charbonneau's genetic algorithm
    // TODO -> Add pooling for chromsomes 

    public class TspGeneticAlgorithm<T>
    {
        #region properties & fields 

        private int Generation { get; set; }

        private Population<T> Population { get; }

        private ISelectionMethod<T> _selectionMethod;

        private IChrossoverMethod<T> _crossoverMethod; 

        /// <summary>
        /// The selection method used in the GA. 
        /// </summary>
        private SelectionMethodType SelectionMethodType { get; set; }

        private readonly GeneticAlgorithmOptions<T> _geneticAlgorithmOptions;

        #endregion properties & fields  

        #region constructor/s

        public TspGeneticAlgorithm()
        {
            _geneticAlgorithmOptions = new GeneticAlgorithmOptions<T>();
            SetSelectionMethod(_geneticAlgorithmOptions.SelectionMethod);
            Generation = 0;
        }

        public TspGeneticAlgorithm(GeneticAlgorithmOptions<T> geneticAlgorithmOptions)
        {
            _geneticAlgorithmOptions = geneticAlgorithmOptions;
            SetSelectionMethod(_geneticAlgorithmOptions.SelectionMethod);
            Generation = 0;

            Population = new Population<T>(geneticAlgorithmOptions.PopulationSize);
        }

        #endregion constructor/s

        #region methods 

        #region public methods 

        public void EvolvePopulation()
        {
            //CandidateSelection();
        }

        #endregion public methods

        #region private methods 

        private Chromosome<T> CandidateSelection()
        {
            return _selectionMethod.PopulationSelection(Population);
        }

        // TODO -> Move this to base class or something !!!! 

        /// <summary>
        /// Sets the selection method according to the selection method type selected.
        /// </summary>
        private void SetSelectionMethod(SelectionMethodType selectionMethodType)
        {
            if (selectionMethodType == SelectionMethodType.None)
            {
                throw new Exception("Selection method cannot be none when setting the method.");
            }

            SelectionMethodType = selectionMethodType;
            switch (SelectionMethodType)
            {
                case SelectionMethodType.Rws:
                    _selectionMethod = new RouletteWheel<T>();
                    break;
            }
        }

        // TODO -> Implement set crossover method shit 

        #endregion private methods 

        #endregion methods  
    }
}
