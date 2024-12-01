using AoC2024.Day01;

class Program
{
    public static void Main()
    {
        Directory.SetCurrentDirectory(@"..\..\..\");
        var inputReader = new InputReader("01");
        var solver = new Solver(inputReader.ReadFile());
        
        
        Console.WriteLine("Solving Part 1:");
        Console.WriteLine(solver.GetPartOneSolution());

        Console.WriteLine("Solving Part 2:");
        Console.WriteLine(solver.GetPartTwoSolution());
    }
}