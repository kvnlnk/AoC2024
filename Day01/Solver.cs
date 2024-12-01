using AoC2024.Common;

namespace AoC2024.Day01;

public class Solver : ISolver
{
    private List<string> _list;
    private List<List<int>> _listOfLists;

    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }

    private void SeparateList()
    {
        _listOfLists = new List<List<int>> { new(), new() };
        foreach (var line in _list)
        {
            var splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            _listOfLists[0].Add(Convert.ToInt32(splitLine[0]));
            _listOfLists[1].Add(Convert.ToInt32(splitLine[1]));
        }
    }

    public string GetPartOneSolution()
    {
        var distance = 0;

        _listOfLists[0].Sort();
        _listOfLists[1].Sort();

        for (var i = 0; i < _listOfLists[0].Count; i++)
        {
            distance += Math.Abs(_listOfLists[0][i] - _listOfLists[1][i]);
        }

        return distance.ToString();
    }


    public string GetPartTwoSolution()
    {
        var score = 0;
        var amountCounter = 0;

        // Create and parse first list as SortedSet to get unique numbers
        var sortedSet = new SortedSet<int>(_listOfLists[0]);

        foreach (var leftNumber in sortedSet)
        {
            amountCounter += _listOfLists[1].Count(rightNumber => leftNumber == rightNumber);
            score += amountCounter * leftNumber;
            amountCounter = 0;
        }

        return score.ToString();
    }
}