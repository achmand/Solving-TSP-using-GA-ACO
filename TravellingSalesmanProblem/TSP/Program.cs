/*
  https://www.springer.com/gp/book/9783642072857 (Find slides)
*/

using System;
using System.IO;
using EvolutionaryComputation.GeneticAlgorithm.Common;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Crossover;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Mutation;
using EvolutionaryComputation.GeneticAlgorithm.Opertators.Selection;
using EvolutionaryComputation.TspProblem;
using EvolutionaryComputation.Utilities;

namespace TSP
{
    // TODO -> GA: 

    // TODO -> Implement different ways of distance functions 
    // TODO -> Research entropy in GA
    // TODO -> Check comments 
    // TODO -> Using floats instead of double could save up memory !!

    class Program
    {
        static void Main(string[] args)
        {
            var test = false;
            var tspCities = new TspInstance();

            //var t = 0;
            // TODO -> Properly get inputs from TSP files 
            foreach (var line in File.ReadLines("C:/Users/Nalyd/Desktop/Current Projects/Github/Solving-TSP-using-GA-ACO/TravellingSalesmanProblem/TSP/berlin52.tsp"))
            {
                if (line == "EOF")
                {
                    test = false;
                }

                if (test)
                {
                    var details = line.Split(null);
                    tspCities.CitiesSet.Add(Convert.ToInt32(details[0]), new Vector2(float.Parse(details[1]), float.Parse(details[2])));

                    //t++;

                    //if (t >= 4)
                    //{
                    //    break;
                    //}
                }

                if (line == "NODE_COORD_SECTION")
                {
                    test = true;
                }
            }

            //Console.WriteLine(tspCities.CitiesToString());

            var gaOptions = new GAOptions(1000, EncodingType.Permutation, SelectionType.Rws, CrossoverType.Cycle, MutationType.SingleSwap, 0.3, 0.5, IterationThreshold.SpecifiedGenerations);
            var tspGeneticAlgorithm = new TspGeneticAlgorithm(gaOptions, tspCities);
            tspGeneticAlgorithm.Evolve();
            // var chromosome = new Chromosome<int>(new []{49,32,45,19,41,8,9,10,43,33,51,11,52,14,13,47,26,27,28,12,25,4,6,15,5,24,48,38,37,40,39,36,35,34,44,46,16,29,50,20,23,30,2,7,42,21,17,3,18,31,22 });

            //var chromsome = new Chromosome<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            //var chromsomeB = new Chromosome<int>(new[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 });
            //var single = new OrderOne<int>(new Random());

            //var child = single.Crossover(chromsome, chromsomeB);
            //Console.WriteLine(string.Join(",", child.Genome));
            //var cycleTest = new CycleCrossover<int>(new Random());
            //var chromsome = new Chromosome<int>(new[] { 8, 4, 7, 3, 6, 2, 5, 1, 9, 0 });
            //var chromsomeB = new Chromosome<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            //cycleTest.Crossover(chromsome, chromsomeB);

            //chromsome = new Chromosome<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            //chromsomeB = new Chromosome<int>(new[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 });

            //Console.WriteLine("");
            //cycleTest.Crossover(chromsome, chromsomeB);

            Console.ReadLine();


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

            //var test = new Vector2(565, 575);
            //var test3 = new Vector2(25, 185);

            //var x = test.CalcSqrMagnitude(test3);
            //Console.WriteLine(x);

            //var tspInstance = new TspInstance();

        }

        //private static Chromosome<int> test(Chromosome<int> father, Chromosome<int> mother)
        //{
        //    // TODO -> Check if not equal, should this be done by the selection method itself ??? (Same repeated genes)

        //    int startCutoffPoint = 7;
        //    int endCutoffPoint = 9;

        //    var offspring = new Chromosome<int>(father.GenomeLength);
        //    offspring.CopyGenes(father, startCutoffPoint, endCutoffPoint);

        //    var difference = (endCutoffPoint - startCutoffPoint) + 1;

        //    var counter = 0;
        //    for (var i = 0; i < mother.GenomeLength; i++)
        //    {
        //        if (counter == startCutoffPoint)
        //        {
        //            counter += difference;
        //        }

        //        var motherGene = mother.Genome[i];
        //        if (!offspring.ContainsGene(motherGene))
        //        {
        //            offspring.AddGene(motherGene, counter);
        //            counter++;
        //        }
        //    }

        //    return offspring;
        //}
    }
}
