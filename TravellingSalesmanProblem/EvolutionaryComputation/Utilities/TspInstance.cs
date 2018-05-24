using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvolutionaryComputation.Utilities
{
    /* NOTES:
    - TSP Instances taken from
    All instances used for TSP are SYMMETRIC 
    http://elib.zib.de/pub/mp-testdata/tsp/tsplib/tsplib.html
    */

    /// <summary>
    /// The edge weight types which could be processed. 
    /// </summary>
    public enum EdgeWeightType
    {
        /// <summary>
        /// Euclidean 2 dimensional space. 
        /// </summary>
        Euc2D,
        Geo
    }

    /// <summary>
    /// The TSP instances which holds meta information and the cities.
    /// </summary>
    public sealed class TspInstance
    {
        #region properties 

        /// <summary>
        /// The name of the TSP instance. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Any additional comments about the TSP instance. 
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The total dimensions.
        /// </summary>
        public int Dimensions { get; set; }

        /// <summary>
        /// The edge weight type.
        /// </summary>
        public EdgeWeightType EdgeWeightType { get; set; }

        /// <summary>
        /// The collection of cities. 
        /// </summary>
        public Dictionary<int, Vector2> CitiesSet { get; set; }

        /// <summary>
        /// The cities collection length.
        /// </summary>
        public int CitiesLength => CitiesSet.Count;

        /// <summary>
        /// String builder used to parse the TSP instance. 
        /// </summary>
        private StringBuilder _stringBuilder;

        /// <summary>
        /// The allowed edge weight types. 
        /// </summary>
        private readonly string[] _allowedEdgeWeightType = { "EUC_2D" };

        #endregion

        #region constructor/s 

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public TspInstance()
        {
            CitiesSet = new Dictionary<int, Vector2>(Dimensions, Integer32EqualityComparer.Default);
        }

        /// <summary>
        /// Constructor with parameters. 
        /// </summary>
        /// <param name="filePath">The file path which holds the .tsp instance.</param>
        public TspInstance(string filePath)
        {
            ParseTspInstance(filePath);
        }

        #endregion constructor/s

        #region public methods 

        /// <summary>
        /// Parses a string into a <see cref="TspInstance"/>. 
        /// </summary>
        /// <param name="filePath">The file path for the .tsp file.</param>
        public void ParseTspInstance(string filePath)
        {
            try
            {
                var fileFormat = Path.GetExtension(filePath);
                if (fileFormat != ".tsp")
                {
                    throw new Exception("File must be .tsp format.");
                }

                var fileLines = File.ReadAllLines(filePath);
                for (int i = 0; i < fileLines.Length; i++)
                {
                    var line = fileLines[i];
                    var splitLine = line.Split(':');

                    if (splitLine.Length > 1)
                    {
                        var name = splitLine[0].Trim();
                        var value = splitLine[1].Trim();

                        if (name == "NAME")
                        {
                            Name = value;
                        }

                        if (name == "COMMENT")
                        {
                            Comment = value;
                        }

                        if (name == "DIMENSION")
                        {
                            int dimension;
                            var dimensionParse = int.TryParse(value, out dimension);
                            if (dimensionParse)
                            {
                                Dimensions = dimension;
                            }
                        }

                        if (name == "EDGE_WEIGHT_TYPE")
                        {
                            var isEdgeTypeAllowed = false;
                            for (int j = 0; j < _allowedEdgeWeightType.Length; j++)
                            {
                                if (_allowedEdgeWeightType[j] == value)
                                {
                                    isEdgeTypeAllowed = true;
                                    break;
                                }
                            }

                            if (!isEdgeTypeAllowed)
                            {
                                throw new Exception("Only EUC_2D and GEO Edge weight types are allowed.");
                            }

                            switch (value)
                            {
                                case "EUC_2D":
                                    EdgeWeightType = EdgeWeightType.Euc2D;
                                    break;
                                case "GEO":
                                    EdgeWeightType = EdgeWeightType.Geo;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (splitLine[0] == "NODE_COORD_SECTION")
                        {
                            for (int j = i + 1; j < fileLines.Length; j++)
                            {
                                var coordinateLine = fileLines[j];
                                if (coordinateLine == "EOF")
                                {
                                    break;
                                }

                                var coordinatesArray = coordinateLine.Split(' ');

                                var cityId = Convert.ToInt32(coordinatesArray[0]);

                                var coordinateX = Convert.ToDouble(coordinatesArray[1]);
                                var coordinateY = Convert.ToDouble(coordinatesArray[2]);
                                var vector = new Vector2(coordinateX, coordinateY);

                                if (CitiesSet == null)
                                {
                                    CitiesSet = new Dictionary<int, Vector2>(Dimensions, Integer32EqualityComparer.Default);
                                }

                                CitiesSet.Add(cityId, vector);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong when reading the file. {ex.Message}");
            }
        }

        /// <summary>
        /// The TSP instance information as a string.
        /// </summary>
        /// <returns>The information about the current TSP instance.</returns>
        public override string ToString()
        {
            if (_stringBuilder == null)
            {
                _stringBuilder = new StringBuilder();
            }

            _stringBuilder.Clear();

            _stringBuilder.Append($"Name: {Name} \n");
            _stringBuilder.Append($"Comment: {Comment} \n");
            _stringBuilder.Append($"Dimension: {Dimensions} \n");
            _stringBuilder.Append($"Edge Weight Type: {EdgeWeightType} \n");
            _stringBuilder.Append("\nCities\n*********\n");
            foreach (var city in CitiesSet)
            {
                _stringBuilder.Append($"City: {city.Key} {city.Value.X} {city.Value.Y} \n");
            }

            return _stringBuilder.ToString();
        }

        #endregion
    }
}
