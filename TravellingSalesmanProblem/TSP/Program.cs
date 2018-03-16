using System;
using System.IO;
using Domain.Common;

namespace TSP
{
    // TODO -> Implement different ways of distance functions 
    // TODO -> Research entropy in GA
    // TODO -> Check comments 

    class Program
    {
        static void Main(string[] args)
        {
            //var test = false;
            //var tspCities = new TspInstance();

            //// TODO -> Properly get inputs from TSP files 
            //foreach (var line in File.ReadLines("C:/Users/Nalyd/Desktop/Current Projects/Github/Solving-TSP-using-GA-ACO/TravellingSalesmanProblem/TSP/berlin52.tsp"))
            //{
            //    if (line == "EOF")
            //    {
            //        test = false;
            //    }

            //    if (test)
            //    {
            //        var details = line.Split(null);
            //        tspCities.Cities.Add(Convert.ToInt32(details[0]), new Vector2(float.Parse(details[1]), float.Parse(details[2])));
            //    }

            //    if (line == "NODE_COORD_SECTION")
            //    {
            //        test = true;
            //    }
            //}

            //Console.WriteLine(tspCities.CitiesToString());
            //Console.WriteLine(Helpers.Map(25f, 0f, 50f, 0f, 1f));
            Console.ReadLine();
        }
    }
}
