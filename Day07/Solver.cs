using AoC2024.Common;

namespace AoC2024.Day07;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<List<long>> _equationInputs = [];
    private readonly List<List<string>> _listOfOperatorLists = [];

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
                ..Array.ConvertAll(line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries), long.Parse)
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
            GetAllOperatorCombinationsForList(equation.Count - 2);
            var operatorList = _listOfOperatorLists[index];

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


    private void GetAllOperatorCombinationsForList(int bit)
    {
        List<string> operatorCombination = [];

        // Generate all possible combination of operants using binary numbers
        for (var i = 0; i < Math.Pow(2, bit); i++)
        {
            // Convert i to base 2 to get binary number 
            var binaryString = Convert.ToString(i, 2).PadLeft(bit, '+').Replace('0', '+').Replace('1', '*');
            operatorCombination.Add(binaryString);
        }

        _listOfOperatorLists.Add(operatorCombination);
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}