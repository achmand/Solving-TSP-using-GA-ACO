using System;
using System.Linq;
using EvolutionaryComputation.GeneticAlgorithm;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.TspProblem
{
    public sealed class TspGeneticAlgorithm : GeneticAlgorithm<int>
    {
        #region properties

        /// <summary>
        /// The TSP instance which contains the cities and information about the tsp problem . 
        /// </summary>
        private readonly TspInstance _tspInstance;

        #endregion properties 

        #region constructor/s 

        /// <summary>
        /// Constructor with params. 
        /// </summary>
        /// <param name="gaOptions"></param>
        /// <param name="tspInstance"></param>
        public TspGeneticAlgorithm(GAOptions gaOptions, TspInstance tspInstance) : base(gaOptions)
        {
            _tspInstance = tspInstance;
        }

        #endregion constructor/s 

        #region public methods 

        public void Evolve()
        {
            CreateInitialPopulation();
            while (Generation < 30000)
            {
                var bestIndex = Population.HighestFitnessIndex;
                var bestChromomsome = Population.Chromosomes[bestIndex];
                Console.WriteLine(bestChromomsome.Distance);
                CreateNextGeneration();
            }

            //var bestIndex = Population.HighestFitnessIndex;
            //var bestChromomsome = Population.Chromosomes[bestIndex];

            Console.WriteLine("Finished");
        }

        #endregion public methods 

        #region private methods 

        // TODO -> Find different ways to create initial population 
        // TODO-> always add in next gen
        private void CreateInitialPopulation()
        {
            var citiesSet = _tspInstance.CitiesSet;
            if (citiesSet == null)
            {
                throw new Exception("Cities cannot be null");
            }

            if (citiesSet.Count < 1)
            {
                throw new Exception("Cities must at least have a length of 3.");
            }

            // copying cities into an array starting from index 1 
            var cities = citiesSet.Keys.ToArray();  // TODO -> Should I have an array in tsp instance instead ???  to avoid to array ?
            var elements = new int[cities.Length - 1];
            for (int i = 1; i < cities.Length; i++)
            {
                elements[i - 1] = cities[i];
            }

            for (int i = 0; i < Population.PopulationSize; i++)
            {
                // fisher-yates-shuffle O(n)
                for (int j = elements.Length - 1; j > 0; j--)
                {
                    var swapIndex = Random.Next(j + 1);
                    var tmp = elements[j];
                    elements[j] = elements[swapIndex];
                    elements[swapIndex] = tmp;
                }

                // adding to population
                var chromosome = new Chromosome<int>(elements);
                CalculateFitness(chromosome);

                Population.AddChromosome(i, chromosome);
            }

            // prepare for next evolution
            Population.SetNextGeneration();
            SelectionOperator.SetNextGeneration();
        }

        private void CreateNextGeneration()
        {
            var startingIndex = UsingElitism ? NumberOfElite : 0;
            if (UsingElitism)
            {
                Population.SortByFitness();
                for (int i = 0; i < NumberOfElite; i++)
                {
                    Population.AddChromosome(i, Population.Chromosomes[i]);
                }

                startingIndex = NumberOfElite;
            }

            var populationSize = Population.PopulationSize;
            for (int i = startingIndex; i < populationSize; i++)
            {
                // TODO -> Should I make sure that both parents are different ??

                // selection operation
                var fatherChromosome = SelectionOperator.PopulationSelection(Population);
                var motherChromosome = SelectionOperator.PopulationSelection(Population);

                // crossover operation
                var childChromosome = CrossoverOperator.Crossover(fatherChromosome, motherChromosome); // TODO -> some crossovers can return more than one child 

                // mutate operation 
                MutationOperator.Mutate(childChromosome);

                // add child chromsome to the next generation population
                CalculateFitness(childChromosome);
                Population.AddChromosome(i, childChromosome);
            }

            // prepare for next evolution
            Population.SetNextGeneration();
            SelectionOperator.SetNextGeneration();

            Generation++;
        }

        // TODO -> This must be used as a fitness calculator
        private void CalculateFitness(Chromosome<int> chromosome)
        {
            var citiesSet = _tspInstance.CitiesSet;

            var distance = 0d;
            var genome = chromosome.Genome;

            var initialVector = citiesSet[1];

            var firstGene = genome[0];
            var firstVector = citiesSet[firstGene];
            distance += initialVector.CalcMagnitude(firstVector);

            var genomeLength = genome.Length;

            var lastGene = genome[genomeLength - 1];
            var lastVector = citiesSet[lastGene];
            distance += initialVector.CalcMagnitude(lastVector);

            for (int i = 0; i < genomeLength - 1; i++)
            {
                var geneA = genome[i];
                var geneB = genome[i + 1];

                var vectorA = citiesSet[geneA];
                var vectorB = citiesSet[geneB];

                distance += vectorA.CalcMagnitude(vectorB);
            }

            chromosome.Fitness = 1 / distance; // using inverse since the shortest is better
            chromosome.Distance = distance;
        }

        #endregion
    }
}
