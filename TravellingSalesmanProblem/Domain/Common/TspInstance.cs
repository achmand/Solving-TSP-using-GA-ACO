using System.Collections.Generic;
using System.Text;

/* 
 * <author>Dylan Vassallo</author>
 * <date>12/03/2018</date>
*/

namespace Domain.Common
{
    /// <summary>
    /// Types of solutions used to solve the TSP (Travelling Salesman Problem). 
    /// </summary>
    public enum TspAlgorithm
    {
        /* NOTES:
            - For more information on GA visit: https://en.wikipedia.org/wiki/Genetic_algorithm
            - For more information on ACO visit: https://en.wikipedia.org/wiki/Ant_colony_optimization_algorithms
        */

        /// <summary>
        /// Genetic Algorithm solution. 
        /// </summary>
        Ga,
        /// <summary>
        /// Ant colony optimization solution. 
        /// </summary>
        Aco
    }

    //http://comopt.ifi.uni-heidelberg.de/software/TSPLIB95/tsp95.pdf
    public sealed class TspInstance
    {
        #region properties 

        public string TourName { get; set; }

        public string Type { get; set; }

        public string Comment { get; set; }

        public int Dimensions { get; set; }

        public string EdgeWeightType { get; set; }

        public Dictionary<int, Vector2> Cities { get; set; }

        private StringBuilder _stringBuilder;

        #endregion properties 

        #region constructor/s 

        public TspInstance()
        {
            Cities = new Dictionary<int, Vector2>(Dimensions, Integer32EqualityComparer.Default);
        }

        #endregion constructor/s 

        #region public methods 

        public string CitiesToString()
        {
            if (_stringBuilder == null)
            {
                _stringBuilder = new StringBuilder();
            }

            _stringBuilder.Clear();
            foreach (var city in Cities)
            {
                _stringBuilder.Append($"City: {city.Key} {city.Value.X} {city.Value.Y} \n");
            }

            return _stringBuilder.ToString();
        }

        #endregion public methods 
    }
}
