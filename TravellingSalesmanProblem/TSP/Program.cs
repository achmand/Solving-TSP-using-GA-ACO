﻿using System;
using EvolutionaryComputation.AntColonyOptimization.Common;
using EvolutionaryComputation.TspProblem;

namespace TSP
{
    // TODO -> GA: 
    // TODO -> Implement different ways of distance functions 
    // TODO -> Research entropy in GA
    // TODO -> Using floats instead of double could save up memory !!

    /* TODO -> Future work (Not for the assignment but for future work)
           
    
       Genetic Algorithm
            ->
        Ant Colony 
            -> Make use of generics to be able to use ACO for different problems. Similiar to GA's implementation.
    */

    class Program
    {
        // TODO -> Inputs from arguments 

        static void Main(string[] args)
        {

            const string testingPath = "C:/Users/Nalyd/Desktop/Current Projects/Github/Solving-TSP-using-GA-ACO/TravellingSalesmanProblem/TSP/TSPInstances/berlin52.tsp";
            var tspInstance = new TspInstance(testingPath);

            //var gaOptions = new GAOptions(1000, EncodingType.Permutation, SelectionType.Rws, CrossoverType.Cycle, MutationType.SingleSwap, 0.3, 0.3, IterationThreshold.SpecifiedGenerations);
            //var tspGeneticAlgorithm = new TspGeneticAlgorithm(gaOptions, tspInstance);
            //tspGeneticAlgorithm.Compute();

            var acoOptions = new ACOOptions(37, 3, 2, 0.01, 2.00);
            var aco = new TspAntColonyOptimization(acoOptions, tspInstance);
            aco.Compute();
            Console.ReadLine();
        }
    }
}
