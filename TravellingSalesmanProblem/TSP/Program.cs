using System;
using System.IO;
using Domain.Common;
using Domain.GeneticAlgorithm;

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

            // testing select one 
            //var fitness = new double[]
            //{
            //    0.1, // 0
            //    0.2, // 1
            //    0.1, // 2
            //    0.2, // 3
            //    0.4  // 4
            //};

            //var rand = new Random();
            //foreach (var t in fitness)
            //{
            //    var index = 0;
            //    var r = rand.NextDouble();
            //    while (r > 0)
            //    {
            //        r -= fitness[index];
            //        index += 1;
            //    }

            //    index -= 1;

            //    Console.WriteLine(index);
            //}

            //var x = new[] { 1, 2, 3, 4, 5, 6, 7 };
            //for (int i = 0; i < 200; i++)
            //{
            //    int s1;
            //    int s2;

            //    RandomProvider.Default.RandomSubArrayIndexes(0, x.Length - 1, out s1, out s2);
            //    Console.WriteLine(s1 + " " + s2);
            //}

            //var genome1 = new[] { 8, 4, 7, 3, 6, 2, 5, 1, 9, 0 };
            //var father = new Chromosome<int>(genome1);

            //var genome2 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //var mother = new Chromosome<int>(genome2);

            //var offspring = test(father, mother);
            //Console.WriteLine(offspring.ToString());



            Console.ReadLine();
        }

        private static Chromosome<int> test(Chromosome<int> father, Chromosome<int> mother)
        {
            // TODO -> Check if not equal, should this be done by the selection method itself ??? (Same repeated genes)

            int startCutoffPoint = 7;
            int endCutoffPoint = 9;

            var offspring = new Chromosome<int>(father.GenomeLength);
            offspring.CopyGenes(father, startCutoffPoint, endCutoffPoint);

            var difference = (endCutoffPoint - startCutoffPoint) + 1;

            var counter = 0;
            for (var i = 0; i < mother.GenomeLength; i++)
            {
                if (counter == startCutoffPoint)
                {
                    counter += difference;
                }

                var motherGene = mother.Genome[i];
                if (!offspring.ContainsGene(motherGene))
                {
                    offspring.AddGene(motherGene, counter);
                    counter++;
                }
            }

            return offspring;
        }
    }
}
