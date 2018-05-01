using System.Collections.Generic;
using System.Text;
using EvolutionaryComputation.Utilities;

namespace EvolutionaryComputation.TspProblem
{
    /* NOTES:
    - For more information on selection TSP instance visit:
    //http://comopt.ifi.uni-heidelberg.de/software/TSPLIB95/tsp95.pdf
    */

    public sealed class TspInstance
    {
        #region properties 

        public int Dimensions { get; set; }

        public Dictionary<int, Vector2> CitiesSet { get; set; }

        private StringBuilder _stringBuilder;

        #endregion

        #region constructor/s 

        public TspInstance()
        {
            CitiesSet = new Dictionary<int, Vector2>(Dimensions, Integer32EqualityComparer.Default);
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
            foreach (var city in CitiesSet)
            {
                _stringBuilder.Append($"City: {city.Key} {city.Value.X} {city.Value.Y} \n");
            }

            return _stringBuilder.ToString();
        }

        #endregion
    }
}
