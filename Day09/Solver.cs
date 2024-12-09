using AoC2024.Common;

namespace AoC2024.Day09;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<string> _diskMap = [];
    private readonly List<string> _blockList = [];
    private readonly List<string> _compactedBlocks = [];
    
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
        Console.WriteLine(string.Join(", ", _blockList));
        Console.WriteLine();
        CompactIndividualBlocks();
        Console.WriteLine(string.Join(", ", _compactedBlocks));
        var compactedBlocksAsString = string.Join("", _compactedBlocks);

        for (var i = 0; i < _compactedBlocks.Count; i++)
        {
            checksum += i * Convert.ToInt64(_compactedBlocks[i]);
        }
        /*
        for (var i = 0; i < compactedBlocksAsString.Length; i++)
        {
            if (compactedBlocksAsString[i].ToString() != " ")
            {
                Console.WriteLine("Rechnung: " + i * Convert.ToInt64(compactedBlocksAsString[i].ToString()));
                checksum += (i * Convert.ToInt64(compactedBlocksAsString[i].ToString()));
                Console.WriteLine("Checksum: " + checksum);
            }
        }
        */
        return checksum.ToString();
    }

    private void GetIndividualBlocks()
    {
        var id = 0;
        for (var i = 0; i < _diskMap.Count; i += 2)
        {
            // id soll nur hoch gehen, wenn auch zahlen eingefügt wurden nd nicht auch wenn punkt eingefügt wurden sind, muss noch angepasst werden-
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
                while (_blockList[_blockList.Count-1] == ".")
                {
                    _blockList.RemoveAt(_blockList.Count-1);
                }
                _compactedBlocks.Add(_blockList[_blockList.Count-1]);

                _blockList.RemoveAt(_blockList.Count-1);
            }
            else
            {
                _compactedBlocks.Add(_blockList[i]);
            }
            //Console.WriteLine(string.Join(", ", _compactedBlocks));
        }

        Console.WriteLine("help");
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}