using System;

namespace EvolutionaryComputation.AntColonyOptimization.Common
{
    public class Ant
    {
        #region properties 

        // TODO -> This should be private
        public int[] Path { get; private set; }

        public double PathDistance { get; set; }

        #endregion properties 

        #region constructor/s 

        public Ant(int[] path)
        {
            Path = path;
        }

        #endregion constructor/s 

        #region public methods 

        /// <summary>
        /// Set a random trail/path. 
        /// </summary>
        /// <param name="random"></param>
        public void SetRandomPath(Random random)
        {
            var pathLength = Path.Length;

            // using Fisher-Yates algorithm to randomize the cities in a trail/path
            for (int i = 0; i < pathLength - 2; i++)
            {
                var randomIndex = random.Next(i, pathLength - 1);
                var tmpCity = Path[randomIndex];

                Path[randomIndex] = Path[i];
                Path[i] = tmpCity;
            }

            // since we did a random shuffle using Fisher-Yates we need to make sure that we start at the City with ID 1
            var startIndex = FindIndexInPath(1);
            var currentStartCity = Path[0];

            // swapping the current start index to always start trail at 1
            Path[0] = Path[startIndex];
            Path[startIndex] = currentStartCity;
        }

        /// <summary>
        /// Sets a new path. 
        /// </summary>
        /// <param name="path">The new path which will be set.</param>
        public void SetNewPath(int[] path)
        {
            Path = path;
        }

        /// <summary>
        /// Check if both cities are adjacent to each other in the current path. 
        /// </summary>
        /// <param name="cityA">The city from which will be checked for adjacency.</param>
        /// <param name="cityB">The city to which will be checked for adjacency.</param>
        /// <returns>True if both cities are adjacent to each other, and false otherwise.</returns>
        public bool CheckCitiesAdjacency(int cityA, int cityB)
        {
            if (cityA == cityB) // a city cannot be adjacent to itself 
            {
                return false;
            }

            var cityAIndex = FindIndexInPath(cityA);
            var cityBIndex = FindIndexInPath(cityB);

            if (cityAIndex == cityBIndex + 1)
            {
                return true;
            }
            if (cityBIndex == cityAIndex + 1)
            {
                return false;
            }

            var lastIndex = Path.Length - 2;
            if (cityAIndex == 0 && cityBIndex == lastIndex)
            {
                return true;
            }

            if (cityBIndex == 0 && cityAIndex == lastIndex)
            {
                return true;
            }

            return false;
        }

        #endregion public methods 

        #region private methods

        private int FindIndexInPath(int targetCity)
        {
            for (int i = 0; i < Path.Length; i++)
            {
                if (Path[i] == targetCity)
                {
                    return i;
                }
            }

            throw new Exception("Target not found in the path/trail");
        }

        #endregion private methods 
    }
}
