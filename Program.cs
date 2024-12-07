using AoC2024.Common;
using AoC2024.Day01;


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
        };

        foreach (var day in solvers.Keys)
        {
            Console.WriteLine($"--- Solving Day {day:D2} ---");
            var solver = solvers[day]();
            try
            {
                Console.WriteLine("Part 1: " + solver.GetPartOneSolution());
                Console.WriteLine("Part 2: " + solver.GetPartTwoSolution());
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Not fully implemented yet.");
                
            }
            
        }
    }
}