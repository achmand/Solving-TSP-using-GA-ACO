using System;
using System.Linq;
using EvolutionaryComputation.TspProblem;

namespace EvolutionaryComputation.AntColonyOptimization.Common
{
    public sealed class TspAntColonyOptimization
    {
        #region properties 

        private Random Random { get; set; }

        /// <summary>
        /// The current <see cref="ACOOptions"/> used in the ACO. 
        /// </summary>
        private ACOOptions AcoOptions { get; set; }

        private Ant[] Ants { get; set; }

        private double[][] Pheromones { get; set; }

        /// <summary>
        /// The current iteration.
        /// </summary>
        private int CurrentIteration { get; set; }

        /// <summary>
        /// The TSP instance which contains the cities and information about the tsp problem . 
        /// </summary>
        private readonly TspInstance _tspInstance;

        #endregion properties 

        #region constructor/s 

        /// <summary>
        /// Default constructor. 
        /// </summary>
        /// <param name="acoOptions">The <see cref="ACOOptions"/> used in the ACO.</param>
        /// <param name="tspInstance"></param>
        public TspAntColonyOptimization(ACOOptions acoOptions, TspInstance tspInstance)
        {
            Random = new Random(0);

            AcoOptions = acoOptions;
            _tspInstance = tspInstance;
        }

        #endregion constructor/s

        #region public methods 

        public void Compute()
        {
            // initializes the ant colony 
            InitializeAnts();

            // initializes the pheromone information which is stored in a jagged array
            // since the cities start from 1 and our pheromone indexes start at 0,
            // we need to make sure that pheromone at index 0, 2 refers to city 1 to city 3 
            InitializePheromones();
            
            // gets the ant with the best path after initialization (after setting random trails)
            var antWithShortestPath = FindAntWithShortestPath();
            var shortestPathDistance = antWithShortestPath.PathDistance;

            while (CurrentIteration < 1000)
            {
                // update ant paths based on pheromones (this is not only based on pheromones it has a probabilistic element to it )
                UpdateAntPaths();
                UpdatePheromone();
                
                var currentAntWithShortestPath = FindAntWithShortestPath();
                var currentShortestPathDistance = currentAntWithShortestPath.PathDistance;
                if (currentShortestPathDistance < shortestPathDistance)
                {
                    shortestPathDistance = currentShortestPathDistance;
                    antWithShortestPath = currentAntWithShortestPath;
                    Console.WriteLine($"A new shorter distance was found at iteration {CurrentIteration} with length {shortestPathDistance}");
                }

                CurrentIteration++;
            }

            Console.WriteLine($"Best path found is {string.Join(",", antWithShortestPath.Path)}, with a distance of {shortestPathDistance}");
            Console.WriteLine($"Best path found is {string.Join(",", antWithShortestPath.Path.OrderBy(c => c))}, with a distance of {shortestPathDistance}");
        }

        #endregion public methods 

        #region private methods 

        private void InitializeAnts()
        {
            var totalAnts = AcoOptions.TotalAnts;
            Ants = new Ant[totalAnts];

            // since we are returning to the initial city the trail must have a length of the total cities + 1
            var pathLength = _tspInstance.CitiesSet.Count + 1;

            // initialize a new path
            var sequentialPath = new int[pathLength];
            
            // initially set a sequential path 
            var index = 0;
            var cities = _tspInstance.CitiesSet;
            var enumerator = cities.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current.Key;
                // instead of index we could have used current - 1 but to make sure that it will always start from 0 we used this counter 
                sequentialPath[index] = current;
                index++;
            }

            // path must end at 1 
            sequentialPath[sequentialPath.Length - 1] = 1;

            // initially we want to set random trails/paths to the ants
            for (int i = 0; i < totalAnts; i++)
            {
                // clone sequential path so we could pass it to the GetRandomPath function and get a random path
                var tmpPath = (int[])sequentialPath.Clone();
                var ant = new Ant(tmpPath);

                // turns the sequential path that was passed into the constructor to a rando path/trail
                ant.SetRandomPath(Random);

                // calculates the total distance of the ant's path/trail
                CalculatePathDistance(ant);
                Ants[i] = ant;

                //Console.WriteLine(string.Join(",", ant.Path.OrderBy(s => s)));
                //Console.WriteLine(string.Join(",", ant.Path));
            }
        }

        private void UpdateAntPaths()
        {
            for (int i = 0; i < Ants.Length; i++)
            {
                var updatedTrail = UpdateTrail();
                var ant = Ants[i];

                ant.SetNewPath(updatedTrail);
                CalculatePathDistance(ant);
            }
        }

        private int[] UpdateTrail()
        {
            var citiesLength = _tspInstance.CitiesLength;

            var updatedPath = new int[citiesLength + 1];
            updatedPath[0] = 1;
            updatedPath[citiesLength] = 1;

            // the index of this array refers to the city make sure that index 0 refers to city 1 and so on
            var vistedCities = new bool[citiesLength];
            vistedCities[0] = true;

            for (int i = 0; i < citiesLength - 1; i++)
            {
                var currentCity = updatedPath[i];
                var nextCity = GetNextCity(currentCity, vistedCities);
                updatedPath[i + 1] = nextCity;

                // using -1 since index 0 refers to city 1 and so on  
                vistedCities[nextCity - 1] = true;
            }

            //Console.WriteLine(string.Join(",", updatedPath.OrderBy(s => s )));
            return updatedPath;
        }

        private int GetNextCity(int targetCity, bool[] vistedCity)
        {
            var probabilities = MoveCityProbabilities(targetCity, vistedCity);
            var cumulativeProbs = new double[probabilities.Length + 1];

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProbs[i + 1] = cumulativeProbs[i] + probabilities[i];
            }

            var randomValue = Random.NextDouble();
            for (int i = 0; i < cumulativeProbs.Length - 1; i++)
            {
                if (randomValue >= cumulativeProbs[i] && randomValue < cumulativeProbs[i + 1])
                {
                    return i + 1;
                }
            }

            throw new Exception("Failure to return valid city in NextCity");
        }

        private double[] MoveCityProbabilities(int targetCity, bool[] vistedCities)
        {
            var citiesLength = _tspInstance.CitiesLength;

            // an array called taueta which holds the value of: pheromone ^ alpha * (1 / distane ^ beta)
            var taueta = new double[citiesLength];

            // hold the sum for all the taueta values 
            var tauetaSumation = 0.0;
            
            // summing all of the tauetas
            for (int i = 0; i < taueta.Length; i++)
            {
                var cityMapped = i + 1;
                if (cityMapped == targetCity)
                {
                    taueta[i] = 0.0;
                }

                // the probability of moving to a city which is already visted is 0
                else if (vistedCities[i])
                {
                    taueta[i] = 0.0;
                }

                // calculating taueta value (non visted city)
                else
                {
                    // pheromone ^ alpha
                    var alpha = AcoOptions.Alpha;
                    var pheromoneLevel = Pheromones[targetCity - 1][i];
                    var pheromoneAlpha = Math.Pow(pheromoneLevel, alpha);

                    // (1 / distane ^ beta)
                    var beta = AcoOptions.Beta;
                    var distance = 1.0 / CalculateCitiesDistance(targetCity, i + 1);
                    var distanceBeta = Math.Pow(distance, beta);

                    // pheromone ^ alpha * (1 / distane ^ beta)
                    var tauetaValue = pheromoneAlpha * distanceBeta;
                    taueta[i] = tauetaValue;

                    // TODO -> Check these out !!!
                    if (taueta[i] < 0.0001)
                    {
                        taueta[i] = 0.0001;
                    }
                    else if (taueta[i] > double.MaxValue / (citiesLength * 100))
                    {
                        taueta[i] = double.MaxValue / (citiesLength * 100);
                    }
                }

                tauetaSumation += taueta[i];
            }

            // after taueta values are computed we must divide each value with the taueta summation
            var probabilities = new double[citiesLength];
            for (int i = 0; i < probabilities.Length; i++)
            {
                probabilities[i] = taueta[i] / tauetaSumation;
            }

            return probabilities;
        }

        private Ant FindAntWithShortestPath()
        {
            var antWithShortestPathIndex = 0;
            var shortestDistance = Ants[antWithShortestPathIndex].PathDistance;

            for (int i = 0; i < Ants.Length; i++)
            {
                var tmpAnt = Ants[i];
                var tmpDistance = tmpAnt.PathDistance;

                if (tmpDistance < shortestDistance)
                {
                    shortestDistance = tmpDistance;
                    antWithShortestPathIndex = i;
                }
            }

            return Ants[antWithShortestPathIndex];
        }

        /// <summary>
        /// Initialize the pheromone information, which is stored in a jagged array.
        /// </summary>
        private void InitializePheromones()
        {
            var citiesLength = _tspInstance.CitiesLength;

            // initialize jagged array 
            Pheromones = new double[citiesLength][];

            // initialize the pheromone inner array
            for (int i = 0; i < Pheromones.Length; i++)
            {
                Pheromones[i] = new double[citiesLength];
            }

            // index i is the city from (inner array) 
            for (int i = 0; i < Pheromones.Length; i++)
            {
                // index j is the city to (outer array)
                for (int j = 0; j < Pheromones[i].Length; j++)
                {
                    // initially we set the pheromone level to an arbitrary small value .01 
                    Pheromones[i][j] = 0.01;
                }
            }
        }

        private void UpdatePheromone()
        {
            for (int i = 0; i < Pheromones.Length; i++)
            {
                for (int j = i + 1; j < Pheromones[i].Length ; j++)
                {
                    for (int k = 0; k < Ants.Length; k++)
                    {
                        var ant = Ants[k];
                        var pathDistance = ant.PathDistance;

                        var rho = AcoOptions.Rho;
                        var decrease = (1.0 - rho) * Pheromones[i][j];
                        var increase = 0.0;

                        var cityA = i + 1;
                        var cityB = j + 1;
                        if (ant.CheckCitiesAdjacency(cityA, cityB))
                        {
                            var q = AcoOptions.Q;
                            increase = (q / pathDistance);
                        }
                        
                        Pheromones[i][j] = decrease + increase;
                        if (Pheromones[i][j] < 0.0001)
                        {
                            Pheromones[i][j] = 0.0001;
                        }
                        else if (Pheromones[i][j] > 100000.0)
                        {
                            Pheromones[i][j] = 100000.0;
                        }

                        Pheromones[j][i] = Pheromones[i][j];
                    }
                }
            }
        }

        /// <summary>
        /// Calculate path distance for the <see cref="Ant"/> passed as a parameter. 
        /// </summary>
        /// <param name="ant">The <see cref="Ant"/> passed to calculate distance of the current path.</param>
        private void CalculatePathDistance(Ant ant)
        {
            //var cities = _tspInstance.CitiesSet;
            var path = ant.Path;

            var totalDistance = 0.0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                var cityIndexA = path[i];
                var cityIndexB = path[i + 1];

                //var cityA = cities[cityIndexA];
                //var cityB = cities[cityIndexB];

                //var distance = cityA.CalcMagnitude(cityB);

                var distance = CalculateCitiesDistance(cityIndexA, cityIndexB);
                totalDistance += distance;
            }

            ant.PathDistance = totalDistance;
        }

        private double CalculateCitiesDistance(int cityAIndex, int cityBIndex)
        {
            var cities = _tspInstance.CitiesSet;
            if (!cities.ContainsKey(cityAIndex))
            {
                throw new Exception($"City A with ID {cityAIndex} is not found in the cities collection.");
            }

            if (!cities.ContainsKey(cityBIndex))
            {
                throw new Exception($"City B with ID {cityBIndex} is not found in the cities collection.");
            }

            var cityA = cities[cityAIndex];
            var cityB = cities[cityBIndex];

            // TODO -> Add IDistanceCalculator 

            var distance = cityA - cityB;
            return distance.Magnitude;
        }

        #endregion private methods 
    }
}
