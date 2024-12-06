using System;
using System.Collections.Generic;
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
        var sum = 0;
        var processMul = true;

        foreach (Match match in Regex.Matches(string.Join(string.Empty, _list), FullPattern))
        {
            var value = match.Groups[0].Value;
            switch (value)
            {
                case "don't()":
                    processMul = false;
                    continue;
                case "do()":
                    processMul = true;
                    continue;
            }

            if (processMul)
            {
                sum += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
            }
        }

        return sum.ToString();
    }
}