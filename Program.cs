﻿using System.Diagnostics;
using AoC2024.Common;
using AoC2024.Day01;

namespace AoC2024;

class Program
{
    public static void Main()
    {
        Directory.SetCurrentDirectory(@"..\..\..\");

        var solvers = new Dictionary<int, Func<ISolver>>
        {
            { 1, () => new AoC2024.Day01.Solver(new InputReader("01").ReadFile()) },
            { 2, () => new AoC2024.Day02.Solver(new InputReader("02").ReadFile()) },
            { 3, () => new AoC2024.Day03.Solver(new InputReader("03").ReadFile()) },
            { 4, () => new AoC2024.Day04.Solver(new InputReader("04").ReadFile()) },
            { 5, () => new AoC2024.Day05.Solver(new InputReader("05").ReadFile()) },
            { 6, () => new AoC2024.Day06.Solver(new InputReader("06").ReadFile()) },
            //{ 7, () => new AoC2024.Day07.Solver(new InputReader("07").ReadFile()) },
            
            { 9, () => new AoC2024.Day09.Solver(new InputReader("09").ReadFile()) },
            { 10, () => new AoC2024.Day10.Solver(new InputReader("10").ReadFile()) },
        };

        foreach (var day in solvers.Keys)
        {
            Console.WriteLine($"--- Solving Day {day:D2} ---");
            var solver = solvers[day]();
            try
            {
                var stopwatch = Stopwatch.StartNew();
                Console.WriteLine("Part 1: " + solver.GetPartOneSolution());
                stopwatch.Stop();
                Console.WriteLine($"Execution time Part 1: {stopwatch.ElapsedMilliseconds} ms");
                
                var stopwatch2 = Stopwatch.StartNew();
                Console.WriteLine("Part 2: " + solver.GetPartTwoSolution());
                stopwatch.Stop();
                Console.WriteLine($"Execution time Part 2: {stopwatch2.ElapsedMilliseconds} ms");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Not fully implemented yet.");
                
            }
            
        }
    }
}