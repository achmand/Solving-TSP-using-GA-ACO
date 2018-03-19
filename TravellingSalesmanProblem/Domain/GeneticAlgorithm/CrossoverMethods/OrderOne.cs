using Domain.Common;
using Domain.GeneticAlgorithm.SelectionMethods;

namespace Domain.GeneticAlgorithm.CrossoverMethods
{
    public class OrderOne<T> : IChrossoverMethod<T>
    {
        #region properties & fields 

        /// <summary>
        /// The crossover operator for this specific crossover implementation (Order 1).
        /// </summary>
        public CrossoverOperator CrossoverOperator => CrossoverOperator.OrderOne;

        #endregion

        #region methods 

        #region public methods 

        public Chromosome<T> Crossover(Population<T> population, ISelectionMethod<T> selectionMethod)
        {
            var father = selectionMethod.PopulationSelection(population);
            var mother = selectionMethod.PopulationSelection(population);

            // TODO -> Check if not equal, should this be done by the selection method itself ??? (Same repeated genes)

            int startCutoffPoint;
            int endCutoffPoint;
            RandomProvider.Default.RandomSubArrayIndexes(0, father.GenomeLength - 1, out startCutoffPoint, out endCutoffPoint);

            var offspring = new Chromosome<T>(father.GenomeLength);
            offspring.CopyGenes(father, startCutoffPoint, endCutoffPoint);

            var difference = (endCutoffPoint - startCutoffPoint) + 1;

            var counter = 0;
            for (var i = 0; i < mother.GenomeLength; i++)
            {
                if (counter == startCutoffPoint)
                {
                    counter += difference;
                }

                var motherGene = mother.Genome[i];
                if (!offspring.ContainsGene(motherGene))
                {
                    offspring.AddGene(motherGene, counter);
                    counter++;
                }
            }

            return offspring;
        }

        #endregion public methods 

        #endregion methods
    }
}
