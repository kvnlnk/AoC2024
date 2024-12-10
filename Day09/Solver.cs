using AoC2024.Common;

namespace AoC2024.Day09;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<string> _diskMap = [];
    private readonly List<string> _blockList = [];
    
    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }
    
    private void SeparateList()
    {
        foreach (var number in _list.SelectMany(line => line))
        {
            _diskMap.Add(number.ToString());
        }
    }
    

    public string GetPartOneSolution()
    {
        long checksum = 0;
        GetIndividualBlocks();
        CompactIndividualBlocks();

        // Calculate checksum using blocklist after being compacted
        for (var i = 0; i < _blockList.Count; i++)
        {
            if (_blockList[i] != ".")
            {
                checksum += i * Convert.ToInt64(_blockList[i]);   
            }
        }
        return checksum.ToString();
    }

    private void GetIndividualBlocks()
    {
        var id = 0;
        for (var i = 0; i < _diskMap.Count; i += 2)
        {
            for (var j = 0; j < Convert.ToInt32(_diskMap[i]); j++)
            {
                _blockList.Add(id.ToString());
                
            }
            
            if (Convert.ToInt32(_diskMap[i]) == 0)
            {
                _blockList.Add(" ");
            }
            
            if (i + 1 < _diskMap.Count)
            {
                for (var j = 0; j < Convert.ToInt32(_diskMap[i+1]); j++)
                {
                    _blockList.Add(".");
                }
            }
            id++;
        }
    }


    private void CompactIndividualBlocks()
    {
        var counter = _blockList.Count - 1;
        for (var i = 0; i < _blockList.Count; i++)
        {
            if (_blockList[i] == ".")
            {
                // Find next number beginning from the end of the list
                while (_blockList[counter] == ".")
                {
                    counter--;
                }
                
                // Swap elements 
                if (counter > i)
                {
                    (_blockList[i], _blockList[counter]) = (_blockList[counter], _blockList[i]); 
                    continue;
                }
                break;
            }
        }
    }
    

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}