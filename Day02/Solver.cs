using System.ComponentModel;
using System.Security.AccessControl;
using AoC2024.Common;

namespace AoC2024.Day02;

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
        _listOfLists = [];

        foreach (var line in _list)
        {
            var splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            // Cast string array to int array and add it to the list
            _listOfLists.Add(Array.ConvertAll(splitLine, int.Parse).ToList());
        }
    }

    public string GetPartOneSolution()
    {
        var safeCounter = 0;
        var valid = true;

        foreach (var list in _listOfLists)
        {
            // Check first two numbers to determine if they are increasing or decreasing
            if (list[0] < list[1]) // increase
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] > list[i + 1] || (list[i + 1] - list[i] is < 1 or > 3)) // invalid case
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    safeCounter++;
                }
            }
            else if (list[0] > list[1]) // decrease
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] < list[i + 1] || (list[i] - list[i + 1] is < 1 or > 3)) // invalid case
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    safeCounter++;
                }
            }

            valid = true;
        }
        
        return safeCounter.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}