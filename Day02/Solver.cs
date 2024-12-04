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

        foreach (var list in _listOfLists)
        {
            if (IsSafe(list))
            {
                safeCounter++;
            }
        }

        return safeCounter.ToString();
    }

    public string GetPartTwoSolution()
    {
        var safeCounter = 0;

        foreach (var list in _listOfLists)
        {
            var valid = true;
            var copyOfList = new List<int>(list);
            // Check first two numbers to determine if they are increasing or decreasing
            if (list[0] < list[1]) // increase
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] <= list[i + 1] && list[i + 1] - list[i] is >= 1 and <= 3) continue; // valid case

                    if (TryToMakeSafe(copyOfList, list))
                    {
                        safeCounter++;
                    }

                    valid = false;
                    break;
                }
            }
            else if (list[0] > list[1]) // decrease
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] >= list[i + 1] && (list[i] - list[i + 1] is >= 1 and <= 3)) continue; // valid case

                    if (TryToMakeSafe(copyOfList, list))
                    {
                        safeCounter++;
                    }

                    valid = false;
                    break;
                }
            }
            else // equal numbers at the beginning
            {
                if (TryToMakeSafe(copyOfList, list))
                {
                    safeCounter++;
                }

                valid = false;
            }

            // Valid if no list needed to be fixed
            if (valid)
            {
                safeCounter++;
            }
        }

        return safeCounter.ToString();
    }


    private static bool IsSafe(List<int> list)
    {
        var valid = true;
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
                return true;
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
                return true;
            }
        }

        return false;
    }


    private static bool TryToMakeSafe(List<int> copyOfList, List<int> list)
    {
        for (var j = 0; j < list.Count; j++)
        {
            copyOfList.RemoveAt(j);
            if (IsSafe(copyOfList))
            {
                return true;
            }

            copyOfList.Clear();
            copyOfList.AddRange(list);
        }

        return false;
    }
}