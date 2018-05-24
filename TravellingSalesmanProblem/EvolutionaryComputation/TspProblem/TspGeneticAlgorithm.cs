using System;
using System.Linq;
using EvolutionaryComputation.GeneticAlgorithm;
using EvolutionaryComputation.GeneticAlgorithm.Common;

namespace EvolutionaryComputation.TspProblem
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Computes the TSP instance using Genetic Algorithm. 
        /// </summary>
        public void Compute()
        {
            // Would be nice to clear these console writelines stuff and use events, so I could display them from Program.cs

            Console.WriteLine("\nStart TSP Genetic Algorithm\n");
            CreateInitialPopulation();
            var bestIndex = Population.HighestFitnessIndex;
            var bestChromomsome = Population.Chromosomes[bestIndex]; 

            Console.WriteLine($"Best initial distance: {bestChromomsome.Distance} \n"); // since we are using  SqrMagnitude for faster computation we must convert to Magnitude to show real distance

            // stops when the stopping criteria is met
            while (!StoppingCriteria.IsCriteriaMet())
            {
                bestIndex = Population.HighestFitnessIndex;

                var currentBestChromomsome = Population.Chromosomes[bestIndex];
                if (currentBestChromomsome.Fitness > bestChromomsome.Fitness)
                {
                    bestChromomsome = currentBestChromomsome;
                    Console.WriteLine($"A new shorter distance was found at generation {Generation} with distance {bestChromomsome.Distance}");
                }

                CreateNextGeneration();
            }

            Console.WriteLine($"\nDistance for the most optimal tour: {bestChromomsome.Distance}");

            // since we dont have a reference to the starting and ending city [1] in the Genome we add these so we could display them,
            // as they are taken into consideration when calculating fitness and we always start and end at City with ID 1
            Console.WriteLine($"1,{string.Join(",", bestChromomsome.GetGenome())},1");
            Console.WriteLine("\nEnd TSP Genetic Algorithm Finished");
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

                // TODO -> return more than one child always!!!
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosome"></param>
        private void CalculateFitness(Chromosome<int> chromosome)
        {
            var citiesSet = _tspInstance.CitiesSet;

            var distance = 0d;
            var genome = chromosome.GetGenome();

            // origin is always the city with ID 1 
            var initialVector = citiesSet[1];

            // we need to calculate the distance from initial city to the first gene/city
            var firstGene = genome[0];
            var firstVector = citiesSet[firstGene];
            distance += (initialVector - firstVector).Magnitude;

            var genomeLength = genome.Length;

            // we need to calculate the distance from last city to initial city
            var lastGene = genome[genomeLength - 1];
            var lastVector = citiesSet[lastGene];
            distance += (initialVector - lastVector).Magnitude;

            for (int i = 0; i < genomeLength - 1; i++)
            {
                var geneA = genome[i];
                var geneB = genome[i + 1];

                var vectorA = citiesSet[geneA];
                var vectorB = citiesSet[geneB];

                distance += (vectorA - vectorB).Magnitude;
            }

            chromosome.Fitness = 1 / distance; // using inverse since the shortest is better
            chromosome.Distance = distance;
        }

        #endregion
    }
}
