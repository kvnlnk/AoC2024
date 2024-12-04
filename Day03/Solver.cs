using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using AoC2024.Common;

namespace AoC2024.Day03;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private const string PatternMul = @"mul\((\d{1,3})\,(\d{1,3})\)";
    private const string FullPattern = @"mul\((\d{1,3})\,(\d{1,3})\)|do\(\)|don't\(\)";


    public Solver(List<string> list)
    {
        _list = list;
    }

    public string GetPartOneSolution()
    {
        var sum = 0;

        foreach (Match match in Regex.Matches(string.Join(string.Empty, _list), PatternMul))
        {
            sum += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
        }

        return sum.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}