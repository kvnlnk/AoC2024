using AoC2024.Common;

namespace AoC2024.Day05;

public class Solver : ISolver
{
    private List<string> _list;
    private List<List<int>> _ruleList = [];
    private List<List<int>> _listOfPages = [];

    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }

    private void SeparateList()
    {
        var addRule = true;
        foreach (var line in _list)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                addRule = false;
                continue;
            }
            
            if (addRule)
            {
                _ruleList.Add([..Array.ConvertAll(line.Split("|"), int.Parse)]);
            }
            else
            {
                _listOfPages.Add([..Array.ConvertAll(line.Split(","), int.Parse)]);
            }
        }
    }

    public string GetPartOneSolution()
    {
        throw new NotImplementedException();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}