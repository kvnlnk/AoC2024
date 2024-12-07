using System.ComponentModel.DataAnnotations;
using AoC2024.Common;

namespace AoC2024.Day07;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<List<long>> _equationInputs = [];
    private readonly List<List<string>> _listOfOperatorListsPart1 = [];
    private readonly List<List<string>> _listOfOperatorListsPart2 = [];

    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }

    private void SeparateList()
    {
        foreach (var line in _list)
        {
            _equationInputs.Add([
                ..Array.ConvertAll(line.Split([':', ' '],
                    StringSplitOptions.RemoveEmptyEntries), long.Parse)
            ]);
        }
    }

    public string GetPartOneSolution()
    {
        long sum = 0;
        long result = 0;

        for (var index = 0; index < _equationInputs.Count; index++)
        {
            var equation = _equationInputs[index];
            GetAllOperatorCombinationsPart1(equation.Count - 2);
            var operatorList = _listOfOperatorListsPart1[index];

            foreach (var operators in operatorList)
            {
                var copyOfEquation = new List<long>(equation);
                foreach (var op in operators)
                {
                    result = op switch
                    {
                        '*' => copyOfEquation[1] * copyOfEquation[2],
                        '+' => copyOfEquation[1] + copyOfEquation[2],
                        _ => result
                    };
                    copyOfEquation.RemoveAt(1);
                    copyOfEquation[1] = result;
                }

                if (result == equation[0])
                {
                    sum += equation[0];
                    result = 0;
                    break;
                }

                result = 0;
            }
        }

        return sum.ToString();
    }


    public string GetPartTwoSolution()
    {
        long sum = 0;
        long result = 0;

        for (var index = 0; index < _equationInputs.Count; index++)
        {
            var equation = _equationInputs[index];
            GetAllOperatorCombinationsPart2(equation.Count - 2);
            var operatorList = _listOfOperatorListsPart2[index];

            foreach (var operators in operatorList)
            {
                var copyOfEquation = new List<long>(equation);
                foreach (var op in operators)
                {
                    result = op switch
                    {
                        '*' => copyOfEquation[1] * copyOfEquation[2],
                        '+' => copyOfEquation[1] + copyOfEquation[2],
                        '|' => Convert.ToInt64(
                            Convert.ToString(copyOfEquation[1]) + Convert.ToString(copyOfEquation[2])),
                        _ => result
                    };
                    copyOfEquation.RemoveAt(1);
                    copyOfEquation[1] = result;
                }

                if (result == equation[0])
                {
                    sum += equation[0];
                    result = 0;
                    break;
                }

                result = 0;
            }
        }

        return sum.ToString();
    }


    private void GetAllOperatorCombinationsPart1(int bit)
    {
        List<string> operatorCombinations = [];

        // Generate all possible combination of operants using binary numbers
        for (var i = 0; i < Math.Pow(2, bit); i++)
        {
            // Convert i to base 2 to get binary number 
            var binaryString = Convert.ToString(i, 2).PadLeft(bit, '+')
                .Replace('0', '+').Replace('1', '*');
            operatorCombinations.Add(binaryString);
        }

        _listOfOperatorListsPart1.Add(operatorCombinations);
    }


    private void GetAllOperatorCombinationsPart2(int amountOfOperators)
    {
        List<string> combinations = [];
        List<string> operators = ["+", "*", "|"];
        // Generate all possible combination using recursion :(
        _listOfOperatorListsPart2.Add(GenerateCombinations(operators, amountOfOperators, combinations, []));
    }


    private List<string> GenerateCombinations(List<string> operators, int amountOfOperators,
        List<string> combinations, List<string> result)
    {
        // All operators for one combination generated
        if (amountOfOperators == 0)
        {
            result.Add(string.Join("", combinations));
            return result;
        }

        for (var i = 0; i < operators.Count; i++)
        {
            combinations.Add(operators[i]);
            GenerateCombinations(operators, amountOfOperators - 1, combinations, result);
            // Backtracking 
            combinations.RemoveAt(combinations.Count - 1);
        }

        return result;
    }
}