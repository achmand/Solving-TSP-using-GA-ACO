using System;
using System.Collections.Generic;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.Utilities;

//TODO -> I think I can optimize this crossover operator a bit more if I have time (try to remove list, and it's contains method...)
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
        /// A dictionary used for fast lookups O(1) of a parent's index, given a value.
        /// </summary>
        private readonly Dictionary<T, int> _lookupDictionary;

        /// <summary>
        /// A hash set used to check O(1) if an index is already in a cycle. 
        /// </summary>
        private readonly HashSet<int> _cycleIndexSet;
        
        /// <summary>
        /// The list which hold different cycles found between the two parents (chromosomes).
        /// </summary>
        private readonly List<List<int>> _cycles; // TODO -> I dont like the idea of using list of lists
        
        #endregion properties 

        #region constructor/s 

        public CycleCrossover(Random random /*, IEqualityComparer<T> equalityComparer*/) : base(random)
        {
            _cycles = new List<List<int>>();
            _lookupDictionary = new Dictionary<T, int>(/*equalityComparer*/); // since I am using generics I can't specify the Equality Comparer
            _cycleIndexSet = new HashSet<int>(Integer32EqualityComparer.Default);
        }

        #endregion constructor/s 

        #region private methods 

        // TODO -> return more than one child always!!!
        protected override Chromosome<T> _Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome)
        {
            _cycles.Clear();
            _cycleIndexSet.Clear();
            _lookupDictionary.Clear();
            
            var genomeLength = fatherChromosome.GenomeLength;
            var childChromosomeA = new Chromosome<T>(genomeLength); // TODO -> use object pooling !!
            var childChromosomeB = new Chromosome<T>(genomeLength); // TODO -> use object pooling !!

            var fatherGenome = fatherChromosome.GetGenome();
            var motherGenome = motherChromosome.GetGenome();

            // add father genes to lookup dictionary 
            for (int i = 0; i < genomeLength; i++)
            {
                _lookupDictionary.Add(fatherGenome[i], i);
            }
           
            // find cycles between both parents
            for (int i = 0; i < genomeLength; i++)
            {
                if (!_cycleIndexSet.Contains(i))
                {
                    var newCycle = new List<int>();
                    CreateCycle(motherGenome, i, newCycle);
                    _cycles.Add(newCycle);
                }
            }

            // filling in the offsprings using alternate cycles
            for (int i = 0; i < _cycles.Count; i++)
            {
                var currentCycle = _cycles[i];
                var alternate = i % 2 == 0; // using modulo 2 to alternate the cycles between the 2 offsprings

                var chromosomeA = alternate ? childChromosomeA : childChromosomeB; 
                var chromosomeB = alternate ? childChromosomeB : childChromosomeA;

                CopyAlternateCycle(currentCycle, fatherGenome, motherGenome, chromosomeA, chromosomeB);
            }

            return childChromosomeA;
        }

        private void CreateCycle(IReadOnlyList<T> motherGenome, int geneIndex, ICollection<int> currentCycle)
        {
            while (true)
            {
                if (!currentCycle.Contains(geneIndex)) // TODO -> I dont like the idea of using the contains of a list since im passing a list ...
                {
                    currentCycle.Add(geneIndex); // add gene index to cycle
                    _cycleIndexSet.Add(geneIndex); // add gene index to cycle index set since it has been visted
                    var correspondingGeneIndex = _lookupDictionary[motherGenome[geneIndex]]; // get the corresponding index for the value found in the mother genome

                    if (correspondingGeneIndex != geneIndex) // if both values are in different positions continue to loop
                    {
                        geneIndex = correspondingGeneIndex;
                        continue;
                    }
                }

                break; // break once it returns to the initial index
            }
        }

        private static void CopyAlternateCycle(IReadOnlyList<int> currentCycle, IReadOnlyList<T> fatherGenome, IReadOnlyList<T> motherGenome, Chromosome<T> childChromosomeA, Chromosome<T> childChromosomeB)
        {
            for (int j = 0; j < currentCycle.Count; j++)
            {
                var currentCycleIndex = currentCycle[j];
                childChromosomeA.InsertGene(currentCycleIndex, fatherGenome[currentCycleIndex]);
                childChromosomeB.InsertGene(currentCycleIndex, motherGenome[currentCycleIndex]);
            }
        }

        #endregion private methods
    }
}
