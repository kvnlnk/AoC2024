using System;
using System.Collections.Generic;
using System.Linq;
using AoC2024.Common;

namespace AoC2024.Day05;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<List<int>> _ruleList = [];
    private readonly List<List<int>> _listOfPages = [];

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
        var sum = 0;
        
        // try with first list of pages
        for (var p = 0; p < _listOfPages.Count; p++)
        {

            var valid = true;
            for (var i = 0; i < _listOfPages[p].Count; i++)
            {
                
                // Get all rules for the current page
                var pageRules = _ruleList.Where(rule => rule[0] == _listOfPages[p][i]).ToList();


                for (var j = 0; j < _listOfPages[p].Count; j++)
                {
                    foreach (var rule in pageRules)
                    {
                        if (_listOfPages[p][j] == rule[1])
                        {

                            // Index of page > Index of rule
                            if (i > j)
                            {
                                valid = false;
                            }
                        }
                    }
                }
            }
            if (valid)
            {
                sum += _listOfPages[p][_listOfPages[p].Count / 2];
            }

        }

        return sum.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}