using System;
using System.Collections.Generic;
using Domain.Common;

namespace TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testing = new Dictionary<int, Vector2>(10)
            //{
                
            //};

            var v1= new Vector2(0,0);
            var v2= new Vector2(0,7);

            var distance = v2 - v1;
            var sqrMag = distance.Magnitude;
            Console.WriteLine(sqrMag);
            Console.ReadLine();
        }
    }
}
