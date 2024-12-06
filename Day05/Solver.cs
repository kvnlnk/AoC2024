using AoC2024.Common;

namespace AoC2024.Day05;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<List<int>> _ruleList = [];
    private readonly List<List<int>> _listOfPages = [];
    private readonly List<List<int>> _listOfWrongPages = [];

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
        
        foreach (var page in _listOfPages)
        {
            var valid = true;
            for (var i = 0; i < page.Count; i++)
            {
                
                // Get all rules for the current page
                var pageRules = _ruleList.Where(rule => rule[0] == page[i]).ToList();


                for (var j = 0; j < page.Count; j++)
                {
                    foreach (var rule in pageRules)
                    {
                        if (page[j] == rule[1])
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
                sum += page[page.Count / 2];
            }
            else
            {
                _listOfWrongPages.Add(page);
            }
        }
        return sum.ToString();
    }
    
    public string GetPartTwoSolution()
    {
        var sum = 0;
        
        foreach (var pages in _listOfWrongPages)
        {
            for (var i = 0; i < pages.Count; i++)
            {
                
                // Get all rules for the current page
                var pageRules = _ruleList.Where(rule => rule[0] == pages[i]).ToList();
                
                // Iterate through all pages of a list to find possible rules in the list
                for (var j = 0; j < pages.Count; j++)
                {
                    foreach (var rule in pageRules)
                    {
                        // Find possible rule in the list
                        if (pages[j] == rule[1])
                        {
                            // If the index of the page (i) value is greater than the
                            // index of the rule position (j), the rule is broken
                            if (i > j)
                            {
                                SortPagesIfWrong(pages);
                            }
                        }
                    }
                }
            }
            sum += pages[pages.Count / 2];
        }
        return sum.ToString();
    }

    private void SortPagesIfWrong(List<int> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            // Get all rules for the current page
            var pageRules = _ruleList.Where(rule => rule[0] == list[i]).ToList();
            
            // Iterate through list and change indices if rule is broken
            for (var j = 0; j < list.Count; j++)
            {
                foreach (var rule in pageRules)
                {
                    if (list[j] == rule[1])
                    {
                        // If the index of the page value is greater than the index of the rule, the rule is "broken"
                        if (i > j)
                        {
                            var page = list[j];
                            list.RemoveAt(j);
                            list.Insert(i, page);
                        }
                    }
                }
            }
        }
    }
}