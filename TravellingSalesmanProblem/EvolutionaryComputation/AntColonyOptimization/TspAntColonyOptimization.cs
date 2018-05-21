using System;
using EvolutionaryComputation.AntColonyOptimization.Common;
using EvolutionaryComputation.TspProblem;

namespace EvolutionaryComputation.AntColonyOptimization
{
    public class TspAntColonyOptimization
    {
        //#region properties 

        //protected Random Random { get; private set; }

        ///// <summary>
        ///// The current <see cref="ACOOptions"/> used in the ACO. 
        ///// </summary>
        //protected readonly ACOOptions AcoOptions;

        //private Ant[] Ants;

        //#endregion properties

        //#region constructor/s

        ///// <summary>
        ///// Default constructor.
        ///// </summary>
        //public TspAntColonyOptimization()
        //{
        //    Random = new Random(0);
        //}

        //#endregion constructor/s

        //#region private methods 

        //private void InitializeAnts(int totalAnts, int citiesLength)
        //{
        //    Ants = new Ant[totalAnts];
        //    for (int i = 0; i < totalAnts; i++)
        //    {
        //        var start = Random.Next(0, citiesLength); // TODO -> Make sure that we dont select starting index 
        //        var randomTrail = GetRandomTrail(start, citiesLength);
        //        var ant = new Ant(randomTrail);
        //        Ants[i] = ant;
        //    }
        //}

        //private int[] GetRandomTrail(int start, int citiesLength)
        //{
        //    var antTrail = new int[citiesLength];

        //    // first set up a sequential ant trail 
        //    for (int i = 0; i < citiesLength; i++)
        //    {
        //        antTrail[i] = i;
        //    }

        //    // Fisher-yates shuffle to get a random trail (randomizes the order of the cities in the trail)
        //    for (int i = 0; i < citiesLength; i++)
        //    {
        //        var randomIndex = Random.Next(i, citiesLength);
        //        var tempCity = antTrail[randomIndex];
        //        antTrail[randomIndex] = antTrail[i];
        //        antTrail[i] = tempCity;
        //    }

        //    // set back starting city at index 0 
        //    var startingIndex = GetIndexInTrail(antTrail, start);
        //    var city = antTrail[0];
        //    antTrail[0] = antTrail[startingIndex];
        //    antTrail[startingIndex] = city;

        //    return antTrail;
        //}

        //private int GetIndexInTrail(int[] trail, int target)
        //{
        //    for (int i = 0; i < trail.Length; i++)
        //    {
        //        if (trail[i] == target)
        //        {
        //            return i;
        //        }
        //    }

        //    throw new Exception("Target not found in IndexOfTarget");
        //}

        //private double[][] InitializePheromones(int citiesLength)
        //{
        //    var pheromones = new double[citiesLength][];
        //    for (int i = 0; i < citiesLength; i++)
        //    {
        //        pheromones[i] = new double[citiesLength];
        //    }

        //    // all values are set to an arbitrary value of 0.01 
        //    for (int i = 0; i < pheromones.Length; i++)
        //    {
        //        for (int j = 0; j < pheromones[i].Length; j++)
        //        {
        //            pheromones[i][j] = 0.01;
        //        }
        //    }

        //    return pheromones;
        //}

        //#endregion private methods

        #region properties 

        private Random Random { get; set; }

        private ACOOptions AcoOptions { get; set; }

        /// <summary>
        /// The current iteration.
        /// </summary>
        private int CurrentIteration { get; set; }

        private Ant[] Ants { get; set; }

        private double[][] Pheromones { get; set; }

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

        /// <summary>
        /// Computes the TSP instance using Ant Colony Optimization. 
        /// </summary>
        public void Compute()
        {
            // initializes the ant colony 
            InitializeAnts();

            // initializes the pheromone information which is stored in a jagged array
            InitializePheromones(); 

            // gets the ant with the best path after initialization (after setting random trails)
            var antWithShortestPath = FindAntWithShortestPath();

            Console.WriteLine(antWithShortestPath.PathDistance);

            while (CurrentIteration < 10000)
            {
                UpdateAntPaths();
                CurrentIteration ++;
            }
        }

        #endregion public methods 

        #region private methods 

        /// <summary>
        /// Initialize the <see cref="Ant"/> array used in the ACO.
        /// </summary>
        private void InitializeAnts()
        {
            var totalAnts = AcoOptions.TotalAnts;
            Ants = new Ant[totalAnts];

            // since we are returning to the initial city the trail must have a length of the total cities + 1
            var pathLength = _tspInstance.CitiesSet.Count + 1;
            // initialize a new path
            var sequentialPath = new int[pathLength];

            // initially set a sequential path
            for (int i = 0; i < pathLength - 1; i++)
            {
                // we use i + 1 since cities start with 1 not 0
                sequentialPath[i] = i + 1;
            }

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
            }

            // TODO -> REMOVE THIS ONLY USED FOR TESTING PURPOSES 
            // Best solution for berlin52  just to make sure  
            var antA = new Ant(new[] { 1, 49, 32, 45, 19, 41, 8, 9, 10, 43, 33, 51, 11, 52, 14, 13, 47, 26, 27, 28, 12, 25, 4, 6, 15, 5, 24, 48, 38, 37, 40, 39, 36, 35, 34, 44, 46, 16, 29, 50, 20, 23, 30, 2, 7, 42, 21, 17, 3, 18, 31, 22, 1 });
            CalculatePathDistance(antA);
            Console.WriteLine(antA.PathDistance);
            for (int i = 0; i < totalAnts; i++)
            {
                var ant = Ants[i];
                Console.WriteLine(string.Join(",", ant.Path) + " " + ant.PathDistance);
            }
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

        private void UpdateAntPaths()
        {
            for (int i = 0; i < Ants.Length; i++)
            {
                var updatedTrail = UpdateTrail();
                Ants[i].SetNewPath(updatedTrail);
            }
        }

        private int[] UpdateTrail()
        {
            var citiesLength = _tspInstance.CitiesLength;
            var updatedTrail = new int[citiesLength + 1];

            var visitedCities = new bool[citiesLength];
            updatedTrail[0] = 1;
            updatedTrail[updatedTrail.Length - 1] = 1;

            for (int i = 0; i < citiesLength - 2; i++)
            {
                
            }


            return updatedTrail;
        }

        private int GetNextCity()
        {

            return 0;
        }

        private double[] MoveCityProbabilities()
        {
            var citiesLength = _tspInstance.CitiesLength;

            // an array called taueta which holds the value of: pheromone ^ alpha * (1 / distane ^ beta)
            var taueta = new double[citiesLength];

            return null;
        }

        /// <summary>
        /// Calculate path distance for the <see cref="Ant"/> passed as a parameter. 
        /// </summary>
        /// <param name="ant">The <see cref="Ant"/> passed to calculate distance of the current path.</param>
        private void CalculatePathDistance(Ant ant)
        {
            var cities = _tspInstance.CitiesSet;
            var path = ant.Path;

            var totalDistance = 0.0; 
            for (int i = 0; i < path.Length - 1; i++)
            {
                var cityIndexA = path[i];
                var cityIndexB = path[i + 1];

                var cityA = cities[cityIndexA];
                var cityB = cities[cityIndexB];

                var distance = cityA.CalcMagnitude(cityB);
                totalDistance += distance;
            }

            ant.PathDistance = totalDistance;
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

        #endregion private methods 

        public void DoOptimization()
        {
            try
            {
                Console.WriteLine("\nBegin Ant Colony Optimization demo\n");

                int numCities = 60;
                int numAnts = 4;
                int maxTime = 1000;

                Console.WriteLine("Number cities in problem = " + numCities);

                Console.WriteLine("\nNumber ants = " + numAnts);
                Console.WriteLine("Maximum time = " + maxTime);

                Console.WriteLine("\nAlpha (pheromone influence) = " + AcoOptions.Alpha);
                Console.WriteLine("Beta (local node influence) = " + AcoOptions.Beta);
                Console.WriteLine("Rho (pheromone evaporation coefficient) = " + AcoOptions.Rho.ToString("F2"));
                Console.WriteLine("Q (pheromone deposit factor) = " + AcoOptions.Q.ToString("F2"));

                Console.WriteLine("\nInitialing dummy graph distances");
                int[][] dists = MakeGraphDistances(numCities);

                Console.WriteLine("\nInitialing ants to random trails\n");
                int[][] ants = InitAnts(numAnts, numCities);
                // initialize ants to random trails
                ShowAnts(ants, dists);

                int[] bestTrail = BestTrail(ants, dists);
                // determine the best initial trail
                double bestLength = Length(bestTrail, dists);
                // the length of the best trail

                Console.Write("\nBest initial trail length: " + bestLength.ToString("F1") + "\n");
                //Display(bestTrail);

                Console.WriteLine("\nInitializing pheromones on trails");
                double[][] pheromones = InitPheromones(numCities);

                int time = 0;
                Console.WriteLine("\nEntering UpdateAnts - UpdatePheromones loop\n");
                while (time < maxTime)
                {
                    UpdateAnts(ants, pheromones, dists);
                    UpdatePheromones(pheromones, ants, dists);

                    int[] currBestTrail = BestTrail(ants, dists);
                    double currBestLength = Length(currBestTrail, dists);
                    if (currBestLength < bestLength)
                    {
                        bestLength = currBestLength;
                        bestTrail = currBestTrail;
                        Console.WriteLine("New best length of " + bestLength.ToString("F1") + " found at time " + time);
                    }
                    time += 1;
                }

                Console.WriteLine("\nTime complete");

                Console.WriteLine("\nBest trail found:");
                Display(bestTrail);
                Console.WriteLine("\nLength of best trail found: " + bestLength.ToString("F1"));

                Console.WriteLine("\nEnd Ant Colony Optimization demo\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        // Main

        // --------------------------------------------------------------------------------------------

        private int[][] InitAnts(int numAnts, int numCities)
        {
            int[][] ants = new int[numAnts][];
            for (int k = 0; k <= numAnts - 1; k++)
            {
                int start = Random.Next(0, numCities);
                ants[k] = RandomTrail(start, numCities);
            }
            return ants;
        }

        private int[] RandomTrail(int start, int numCities)
        {
            // helper for InitAnts
            int[] trail = new int[numCities];

            // sequential
            for (int i = 0; i <= numCities - 1; i++)
            {
                trail[i] = i;
            }

            // Fisher-Yates shuffle
            for (int i = 0; i <= numCities - 1; i++)
            {
                int r = Random.Next(i, numCities);
                int tmp = trail[r];
                trail[r] = trail[i];
                trail[i] = tmp;
            }

            int idx = IndexOfTarget(trail, start);
            // put start at [0]
            int temp = trail[0];
            trail[0] = trail[idx];
            trail[idx] = temp;

            return trail;
        }

        private static int IndexOfTarget(int[] trail, int target)
        {
            // helper for RandomTrail
            for (int i = 0; i <= trail.Length - 1; i++)
            {
                if (trail[i] == target)
                {
                    return i;
                }
            }
            throw new Exception("Target not found in IndexOfTarget");
        }

        private double Length(int[] trail, int[][] dists)
        {
            // total length of a trail
            double result = 0.0;
            for (int i = 0; i <= trail.Length - 2; i++)
            {
                result += Distance(trail[i], trail[i + 1], dists);
            }
            return result;
        }

        // -------------------------------------------------------------------------------------------- 

        private int[] BestTrail(int[][] ants, int[][] dists)
        {
            // best trail has shortest total length
            double bestLength = Length(ants[0], dists);
            int idxBestLength = 0;
            for (int k = 1; k <= ants.Length - 1; k++)
            {
                double len = Length(ants[k], dists);
                if (len < bestLength)
                {
                    bestLength = len;
                    idxBestLength = k;
                }
            }
            int numCities = ants[0].Length;
            //INSTANT VB NOTE: The local variable bestTrail was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
            int[] bestTrail_Renamed = new int[numCities];
            ants[idxBestLength].CopyTo(bestTrail_Renamed, 0);
            return bestTrail_Renamed;
        }

        // --------------------------------------------------------------------------------------------

        private double[][] InitPheromones(int numCities)
        {
            double[][] pheromones = new double[numCities][];
            for (int i = 0; i <= numCities - 1; i++)
            {
                pheromones[i] = new double[numCities];
            }
            for (int i = 0; i <= pheromones.Length - 1; i++)
            {
                for (int j = 0; j <= pheromones[i].Length - 1; j++)
                {
                    pheromones[i][j] = 0.01;
                    // otherwise first call to UpdateAnts -> BuiuldTrail -> NextNode -> MoveProbs => all 0.0 => throws
                }
            }
            return pheromones;
        }

        // --------------------------------------------------------------------------------------------

        private void UpdateAnts(int[][] ants, double[][] pheromones, int[][] dists)
        {
            int numCities = pheromones.Length;
            for (int k = 0; k <= ants.Length - 1; k++)
            {
                int start = Random.Next(0, numCities);
                int[] newTrail = BuildTrail(k, start, pheromones, dists);
                ants[k] = newTrail;
            }
        }

        private int[] BuildTrail(int k, int start, double[][] pheromones, int[][] dists)
        {
            int numCities = pheromones.Length;
            int[] trail = new int[numCities];
            bool[] visited = new bool[numCities];
            trail[0] = start;
            visited[start] = true;
            for (int i = 0; i <= numCities - 2; i++)
            {
                int cityX = trail[i];
                int next = NextCity(k, cityX, visited, pheromones, dists);
                trail[i + 1] = next;
                visited[next] = true;
            }
            return trail;
        }

        private int NextCity(int k, int cityX, bool[] visited, double[][] pheromones, int[][] dists)
        {
            // for ant k (with visited[]), at nodeX, what is next node in trail?
            double[] probs = MoveProbs(k, cityX, visited, pheromones, dists);

            double[] cumul = new double[probs.Length + 1];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                cumul[i + 1] = cumul[i] + probs[i];
                // consider setting cumul[cuml.Length-1] to 1.00
            }

            double p = Random.NextDouble();

            for (int i = 0; i <= cumul.Length - 2; i++)
            {
                if (p >= cumul[i] && p < cumul[i + 1])
                {
                    return i;
                }
            }
            throw new Exception("Failure to return valid city in NextCity");
        }

        private double[] MoveProbs(int k, int cityX, bool[] visited, double[][] pheromones, int[][] dists)
        {
            // for ant k, located at nodeX, with visited[], return the prob of moving to each city
            int numCities = pheromones.Length;
            double[] taueta = new double[numCities];
            // inclues cityX and visited cities
            double sum = 0.0;
            // sum of all tauetas
            // i is the adjacent city
            for (int i = 0; i <= taueta.Length - 1; i++)
            {
                if (i == cityX)
                {
                    taueta[i] = 0.0;
                    // prob of moving to self is 0
                }
                else if (visited[i] == true)
                {
                    taueta[i] = 0.0;
                    // prob of moving to a visited city is 0
                }
                else
                {
                    taueta[i] = Math.Pow(pheromones[cityX][i], AcoOptions.Alpha) * Math.Pow((1.0 / Distance(cityX, i, dists)), AcoOptions.Beta);
                    // could be huge when pheromone[][] is big
                    if (taueta[i] < 0.0001)
                    {
                        taueta[i] = 0.0001;
                    }
                    else if (taueta[i] > (double.MaxValue / (numCities * 100)))
                    {
                        taueta[i] = double.MaxValue / (numCities * 100);
                    }
                }
                sum += taueta[i];
            }

            double[] probs = new double[numCities];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                probs[i] = taueta[i] / sum;
                // big trouble if sum = 0.0
            }
            return probs;
        }

        // --------------------------------------------------------------------------------------------

        private void UpdatePheromones(double[][] pheromones, int[][] ants, int[][] dists)
        {
            for (int i = 0; i <= pheromones.Length - 1; i++)
            {
                for (int j = i + 1; j <= pheromones[i].Length - 1; j++)
                {
                    for (int k = 0; k <= ants.Length - 1; k++)
                    {
                        double length = Length(ants[k], dists);
                        // length of ant k trail
                        double decrease = (1.0 - AcoOptions.Rho) * pheromones[i][j];
                        double increase = 0.0;
                        if (EdgeInTrail(i, j, ants[k]) == true)
                        {
                            increase = (AcoOptions.Q / length);
                        }

                        pheromones[i][j] = decrease + increase;

                        if (pheromones[i][j] < 0.0001)
                        {
                            pheromones[i][j] = 0.0001;
                        }
                        else if (pheromones[i][j] > 100000.0)
                        {
                            pheromones[i][j] = 100000.0;
                        }

                        pheromones[j][i] = pheromones[i][j];
                    }
                }
            }
        }

        private bool EdgeInTrail(int cityX, int cityY, int[] trail)
        {
            // are cityX and cityY adjacent to each other in trail[]?
            int lastIndex = trail.Length - 1;
            int idx = IndexOfTarget(trail, cityX);

            if (idx == 0 && trail[1] == cityY)
            {
                return true;
            }
            else if (idx == 0 && trail[lastIndex] == cityY)
            {
                return true;
            }
            else if (idx == 0)
            {
                return false;
            }
            else if (idx == lastIndex && trail[lastIndex - 1] == cityY)
            {
                return true;
            }
            else if (idx == lastIndex && trail[0] == cityY)
            {
                return true;
            }
            else if (idx == lastIndex)
            {
                return false;
            }
            else if (trail[idx - 1] == cityY)
            {
                return true;
            }
            else if (trail[idx + 1] == cityY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // --------------------------------------------------------------------------------------------

        private int[][] MakeGraphDistances(int numCities)
        {
            int[][] dists = new int[numCities][];
            for (int i = 0; i <= dists.Length - 1; i++)
            {
                dists[i] = new int[numCities];
            }
            for (int i = 0; i <= numCities - 1; i++)
            {
                for (int j = i + 1; j <= numCities - 1; j++)
                {
                    int d = Random.Next(1, 9);
                    // [1,8]
                    dists[i][j] = d;
                    dists[j][i] = d;
                }
            }
            return dists;
        }

        private double Distance(int cityX, int cityY, int[][] dists)
        {
            return dists[cityX][cityY];
        }

        // --------------------------------------------------------------------------------------------

        private void Display(int[] trail)
        {
            for (int i = 0; i <= trail.Length - 1; i++)
            {
                Console.Write(trail[i] + " ");
                if (i > 0 && i % 20 == 0)
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
        }


        private void ShowAnts(int[][] ants, int[][] dists)
        {
            for (int i = 0; i <= ants.Length - 1; i++)
            {
                Console.Write(i + ": [ ");

                for (int j = 0; j <= 3; j++)
                {
                    Console.Write(ants[i][j] + " ");
                }

                Console.Write(". . . ");

                for (int j = ants[i].Length - 4; j <= ants[i].Length - 1; j++)
                {
                    Console.Write(ants[i][j] + " ");
                }

                Console.Write("] len = ");
                double len = Length(ants[i], dists);
                Console.Write(len.ToString("F1"));
                Console.WriteLine("");
            }
        }

        private void Display(double[][] pheromones)
        {
            for (int i = 0; i <= pheromones.Length - 1; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j <= pheromones[i].Length - 1; j++)
                {
                    Console.Write(pheromones[i][j].ToString("F4").PadLeft(8) + " ");
                }
                Console.WriteLine("");
            }

        }
    }
}
