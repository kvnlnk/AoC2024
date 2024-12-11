using AoC2024.Common;

namespace AoC2024.Day09;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<string> _diskMap = [];

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
        var blockList = GetIndividualBlocks();
        CompactIndividualBlocks(blockList);

        // Calculate checksum using blocklist after being compacted
        for (var i = 0; i < blockList.Count; i++)
        {
            if (blockList[i] != ".")
            {
                checksum += i * Convert.ToInt64(blockList[i]);
            }
        }

        return checksum.ToString();
    }

    
    public string GetPartTwoSolution()
    {
        long checksum = 0;
        var blockList = GetIndividualBlocks();
        CompactWholeBlocks(blockList);

        // Calculate checksum using blocklist after being compacted
        for (var i = 0; i < blockList.Count; i++)
        {
            if (blockList[i] != ".")
            {
                checksum += i * Convert.ToInt64(blockList[i]);
            }
        }

        return checksum.ToString();
    }


    private List<string> GetIndividualBlocks()
    {
        var id = 0;
        var blockList = new List<string>();
        for (var i = 0; i < _diskMap.Count; i += 2)
        {
            for (var j = 0; j < Convert.ToInt32(_diskMap[i]); j++)
            {
                blockList.Add(id.ToString());
            }

            if (Convert.ToInt32(_diskMap[i]) == 0)
            {
                blockList.Add(" ");
            }

            if (i + 1 < _diskMap.Count)
            {
                for (var j = 0; j < Convert.ToInt32(_diskMap[i + 1]); j++)
                {
                    blockList.Add(".");
                }
            }

            id++;
        }

        return blockList;
    }

    
    private static void CompactIndividualBlocks(List<string> blockList)
    {
        var counter = blockList.Count - 1;
        for (var i = 0; i < blockList.Count; i++)
        {
            if (blockList[i] == ".")
            {
                // Find next number beginning from the end of the list
                while (blockList[counter] == ".")
                {
                    counter--;
                }

                // Swap elements 
                if (counter > i)
                {
                    (blockList[i], blockList[counter]) = (blockList[counter], blockList[i]);
                    continue;
                }

                break;
            }
        }
    }


    private static void CompactWholeBlocks(List<string> blockList)
    {
        var counterEnd = blockList.Count - 1;
        int fileSize;
        var freeSpaceList = FindFreeSpace(blockList);

        while (counterEnd >= 0)
        {
            // Get file id from the end of the list
            var fileId = blockList[counterEnd];
            if (fileId == ".")
            {
                counterEnd--;
                continue;
            }

            // Find file size
            fileSize = 0;
            while (counterEnd >= 0 && blockList[counterEnd] == fileId)
            {
                counterEnd--;
                fileSize++;
            }

            // Find first space that fits 
            var freeSpace = freeSpaceList.FirstOrDefault(space => space[1] >= fileSize && space[0] < counterEnd);

            // Skip if there is not enough free space for the file
            if (freeSpace == null) continue;

            // Move whole file into free space
            for (var i = 0; i < fileSize; i++)
            {
                (blockList[freeSpace[0] + i], blockList[counterEnd + 1 + i]) =
                    (blockList[counterEnd + 1 + i], blockList[freeSpace[0] + i]);
            }

            // Remove the used free space from the list
            freeSpaceList.Remove(freeSpace);

            // Add new free space if the file was not using it all 
            if (freeSpace[1] > fileSize)
            {
                freeSpaceList.Add([freeSpace[0] + fileSize, freeSpace[1] - fileSize]);
                // Order list after adding new free space
                freeSpaceList = freeSpaceList.OrderBy(space => space[0]).ToList();
            }
        }
    }


    private static List<int[]> FindFreeSpace(List<string> blockList)
    {
        var freeSpaceList = new List<int[]>();

        var index = 0;
        while (index < blockList.Count)
        {
            if (blockList[index] == ".")
            {
                var freeSpaceStart = index;
                var freeSpaceSize = 0;

                // Continue until there is no consecutive free space left
                while (blockList[index] == "." && index < blockList.Count)
                {
                    freeSpaceSize++;
                    index++;
                }

                freeSpaceList.Add([freeSpaceStart, freeSpaceSize]);
            }
            else
            {
                index++;
            }
        }

        return freeSpaceList;
    }
}