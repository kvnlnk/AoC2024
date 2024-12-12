using System.Threading.Channels;
using AoC2024.Common;

namespace AoC2024.Day11;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<string> _stones = [];
    private readonly List<string> _stones2 = [];

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
            _stones2.Add(stone);
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
        // Cache already processed values
        var memoization = new Dictionary<string, List<string>>(); 

        // Store stone and count of stone occurrences
        var stones = new Dictionary<string, long>();
        foreach (var stone in _stones2)
        {
            stones.TryAdd(stone, 0);
            stones[stone]++;
        }

        for (var i = 0; i < 75; i++)
        {
            var newStones = new Dictionary<string, long>();

            foreach (var (originalStone, count) in stones)
            {
                // Try to find values of already processed stone to skip operations
                if (memoization.TryGetValue(originalStone, out var value))
                {
                    foreach (var stone in value)
                    {
                        newStones.TryAdd(stone, 0);
                        newStones[stone] += count;
                    }

                    continue;
                }

                if (originalStone == "0")
                {
                    newStones.TryAdd("1", 0);
                    newStones["1"] += count;
                    memoization[originalStone] = ["1"];
                }
                else if (originalStone.Length % 2 == 0)
                {
                    var firstHalf = originalStone[..(originalStone.Length / 2)];
                    var secondHalf = originalStone[(originalStone.Length / 2)..].TrimStart('0');
                    
                    if (secondHalf == string.Empty)
                    {
                        secondHalf = "0";
                    }

                    newStones.TryAdd(firstHalf, 0);
                    newStones[firstHalf] += count;

                    newStones.TryAdd(secondHalf, 0);
                    newStones[secondHalf] += count;

                    // Cache processed values for original stone
                    memoization[originalStone] = [firstHalf, secondHalf];
                }
                else
                {
                    var multipliedValue = (Convert.ToInt64(originalStone) * 2024).ToString();

                    newStones.TryAdd(multipliedValue, 0);
                    newStones[multipliedValue] += count;

                    memoization[originalStone] = [multipliedValue];
                }
            }

            stones = newStones;
        }

        return stones.Values.Sum().ToString();
    }

}