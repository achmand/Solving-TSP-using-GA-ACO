using System;
using System.Collections.Generic;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover
{
    /* NOTES:
    - For more information on cycle crossover visit:
    http://www.rubicite.com/Tutorials/GeneticAlgorithms/CrossoverOperators/CycleCrossoverOperator.aspx

    This crossover operator will work for permutation encoding
    */

    public sealed class CycleCrossover<T> : CrossoverOperator<T>
    {
        #region properties 

        /// <summary>
        /// The crossover operator type for this specific selection implementation (Cycle).
        /// </summary>
        public override CrossoverType CrossoverType => CrossoverType.Cycle;

        /// <summary>
        /// A dictionary used for fast lookups of a parent's index, given a value. 
        /// </summary>
        private Dictionary<T, int> _lookupDictionary;

        #endregion properties 

        #region constructor/s 

        public CycleCrossover(Random random, Dictionary<T, int> lookupDictionary) : base(random)
        {
            _lookupDictionary = lookupDictionary;
        }

        #endregion constructor/s 

        #region private methods 

        protected override Chromosome<T> _Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome)
        {
            _lookupDictionary.Clear();

            throw new NotImplementedException();
        }
        
        #endregion private methods
    }
}
