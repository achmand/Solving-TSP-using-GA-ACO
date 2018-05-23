using System.Collections.Generic;
using System.Text;
using EvolutionaryComputation.Utilities;

namespace EvolutionaryComputation.TspProblem
{
    /* NOTES:
    - TSP Instances taken from
    All instances used for TSP are SYMMETRIC 
    http://elib.zib.de/pub/mp-testdata/tsp/tsplib/tsplib.html
    */

    public sealed class TspInstance
    {
        #region properties 

        public int Dimensions { get; set; }

        public Dictionary<int, Vector2> CitiesSet { get; set; }

        public int CitiesLength => CitiesSet.Count;

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
