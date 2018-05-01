﻿using System;
using System.Linq;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover
{
    // this will work for permutation encoding 

    public sealed class OrderOne<T> : CrossoverOperator<T>
    {
        #region properties

        /// <summary>
        /// The crossover operator type for this specific selection implementation (Order1).
        /// </summary>
        public override CrossoverType CrossoverType => CrossoverType.OrderOne;

        #endregion properties

        #region constructor/s 

        public OrderOne(Random random) : base(random) { }

        #endregion constructors 

        #region private methods

        protected override Chromosome<T> _Crossover(Chromosome<T> fatherChromosome, Chromosome<T> motherChromosome)
        {
            if (fatherChromosome.GenomeLength != motherChromosome.GenomeLength)
            {
                throw new Exception("Both parent chromosomes must have the same genome length.");
            }

            var cutoffA = 0;
            var cutoffB = 0;
            var genomeLength = fatherChromosome.GenomeLength;

            // get two random cutoff points (cutoffA must always be smaller and not equal to cutoffB)
            while (cutoffA >= cutoffB)
            {
                cutoffA = Random.Next(0, genomeLength);
                cutoffB = Random.Next(0, genomeLength);
            }

            var childChromosome = new Chromosome<T>(genomeLength); // TODO -> use object pooling !!

            // copying the random set between the two points to the child 
            for (int i = cutoffA; i < cutoffB; i++)
            {
                childChromosome.Genome[i] = fatherChromosome.Genome[i];
            }

            // make a copy of the second parent
            var motherChromosomeCopy = new T[genomeLength];
            var motherGenome = motherChromosome.Genome;
            motherGenome.CopyTo(motherChromosomeCopy, 0); // TODO -> Is this really needed can I do something with modulo instead of having another array 

            // shift the mother array to the right (number of empty slots on the right)
            for (int i = 0; i < genomeLength; i++)
            {
                motherChromosomeCopy[(i + (genomeLength - cutoffB)) % genomeLength] = motherGenome[i];
            }

            // adds the ones which are not found from mother to child, using modulo to start from the second cutoff point 
            var currentCounter = cutoffB;
            for (int i = 0; i < genomeLength; i++)
            {
                var tmpGene = motherChromosomeCopy[i];
                if (!childChromosome.Genome.Contains(tmpGene))
                {
                    childChromosome.Genome[currentCounter % genomeLength] = tmpGene;
                    currentCounter++;
                }
            }

            return childChromosome;
        }

        #endregion private method
    }
}
