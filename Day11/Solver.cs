using System.Threading.Channels;
using AoC2024.Common;

namespace AoC2024.Day11;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<string> _stones = [];
    
    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }

    private void SeparateList()
    {
        foreach (var stone in _list[0].Split(" "))
        {
            _stones.Add(stone);
        }
            
        Console.WriteLine(string.Join(",", _stones));
    }
    
    public string GetPartOneSolution()
    {
        for (var i = 0; i < 25; i++)
        {
            for (var j = 0; j < _stones.Count; j++)
            {
                if (_stones[j] == "0")
                {
                    _stones[j] = "1";
                }
                else if (_stones[j].Length % 2 == 0)
                {
                    var stone = _stones[j];
                    // Insert first half of the number into its old place
                    _stones[j] = _stones[j][..(_stones[j].Length / 2)];
                    
                    // Get substring and remove leading 0 and add it to next to the old numbr
                    var substring = stone.Substring(stone.Length / 2, stone.Length / 2).TrimStart(['0']);
                    _stones.Insert(j + 1, substring == string.Empty ? "0" : substring);
                    j++;
                }
                else
                {
                    // Multiply number with 2024 if no other case fits
                    _stones[j] = (Convert.ToInt64(_stones[j]) * 2024).ToString();
                }
            }
        }

        return _stones.Count.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}