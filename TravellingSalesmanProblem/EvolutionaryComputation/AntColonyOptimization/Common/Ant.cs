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
            for (int i = 0; i < pathLength - 1; i++)
            {
                var randomIndex = random.Next(i, pathLength - 1);
                var tmpCity = Path[randomIndex];

                Path[randomIndex] = Path[i];
                Path[i] = tmpCity;
            }

            // since we did a random shuffle using Fisher-Yates we need to make sure that we start at the City with ID 1
            var startIndex = FindIndexInPath(Path, 1);
            var currentStartCity = Path[0];

            // swapping the current start index to always start trail at 1
            Path[0] = Path[startIndex];
            Path[startIndex] = currentStartCity;

            // set the last city to go back to 1 
            Path[pathLength - 1] = 1;
        }

        public void SetNewPath(int[] path)
        {
            Path = path;
        }

        #endregion public methods 

        #region private methods

        private int FindIndexInPath(int[] path, int targetCity)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == targetCity)
                {
                    return i;
                }
            }

            throw new Exception("Target not found in the path/trail");
        }

        #endregion private methods 
    }
}
