using AoC2024.Common;

namespace AoC2024.Day25;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<List<string>> _schematics = [];
    private readonly List<List<int>> _keys = [];
    private readonly List<List<int>> _locks = [];

    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }

    private void SeparateList()
    {
        // Extract all schematics
        for (var i = 0; i < _list.Count; i++)
        {
            var schematic = new List<string>();
            while (_list[i] != string.Empty)
            {
                schematic.Add(_list[i]);
                if (i >= _list.Count - 1)
                {
                    break;
                }

                i++;
            }

            _schematics.Add(schematic);
        }

        // Extract keys and locks
        foreach (var s in _schematics)
        {
            // Check first line to determine if schematic is a key or lock
            if (s[0] == ".....")
            {
                var key = new List<int>();
                for (var i = 0; i < s[0].Length; i++)
                {
                    var keyHeight = -1;
                    for (var j = 0; j < s.Count; j++)
                    {
                        if (s[j][i] == '.') continue;
                        keyHeight++;
                    }

                    key.Add(keyHeight);
                }

                _keys.Add(key);
            }
            else
            {
                var lock_ = new List<int>();
                for (var i = 0; i < s[0].Length; i++)
                {
                    var lockHeight = -1;
                    for (var j = 0; j < s.Count; j++)
                    {
                        if (s[j][i] == '.') continue;
                        lockHeight++;
                    }

                    lock_.Add(lockHeight);
                }

                _locks.Add(lock_);
            }
        }
    }

    public string GetPartOneSolution()
    {
        var pairCounter = 0;
        foreach (var lock_ in _locks)
        {
            var pair = true;
            foreach (var key in _keys)
            {
                if (lock_.Where((pin, pinIndex) => key[pinIndex] + pin > 5).Any())
                {
                    pair = false;
                }

                if (pair)
                {
                    pairCounter++;
                }

                pair = true;
            }
        }

        return pairCounter.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}