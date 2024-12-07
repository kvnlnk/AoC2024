using AoC2024.Common;

namespace AoC2024.Day04;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;

    private readonly Direction[] _directionsPart1 =
    [
        new() { Row = -1, Column = 0 }, // North
        new() { Row = -1, Column = 1 }, // North-East
        new() { Row = 0, Column = 1 }, // East
        new() { Row = 1, Column = 1 }, // South-East
        new() { Row = 1, Column = 0 }, // South
        new() { Row = 1, Column = -1 }, // South-West
        new() { Row = 0, Column = -1 }, // West
        new() { Row = -1, Column = -1 } // North-West
    ];

    private readonly Direction[] _directionsPart2 =
    [
        new() { Row = -1, Column = -1 }, // North-West
        new() { Row = 1, Column = 1 }, // South-East
        new() { Row = -1, Column = 1 }, // North-East
        new() { Row = 1, Column = -1 } // South-West
        
    ];

    private readonly List<char[]> _pattern =
    [
        new[] { 'M', 'S', 'S', 'M' },
        new[] { 'M', 'S', 'M', 'S' },
        new[] { 'S', 'M', 'M', 'S' },
        new[] { 'S', 'M', 'S', 'M' }
    ];

    private const string Word = "XMAS";


    public Solver(List<string> list)
    {
        _list = list;
        _grid = new char[list.Count, list[0].Length];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                _grid.SetValue(_list[i][j], i, j);
            }
        }
    }


    public string GetPartOneSolution()
    {
        var counter = 0;
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                // Find possible beginning of the word
                if ((char)_grid.GetValue(i, j)! != Word[0]) continue;
                // Check all directions from beginning of the word
                foreach (var direction in _directionsPart1)
                {
                    if (!CheckDirection(1, i, j, direction)) continue;
                    counter++;
                }
            }
        }

        return counter.ToString();
    }


    public string GetPartTwoSolution()
    {
        var counter = 0;
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                if ((char)_grid.GetValue(i, j)! != 'A') continue;
                if (CheckDirection(i, j))
                {
                    counter++;
                }
            }
        }

        return counter.ToString();
    }
    
    
    private bool CheckDirection(int nextLetter, int i, int j, Direction direction)
    {
        // Found word
        if (nextLetter == Word.Length) return true;
        
        // Check next char
        if (CheckBounds(i + direction.Row, j + direction.Column) &&
            Word[nextLetter] == _grid[i + direction.Row, j + direction.Column])
        {
            return CheckDirection(nextLetter + 1, i + direction.Row, j + direction.Column, direction);
        }

        return false;
    }

    private bool CheckDirection(int i, int j)
    {
        // Check bounds for each direction from starting point
        if (_directionsPart2.Any(direction => !CheckBounds(i + direction.Row, j+direction.Column)))
        {
            return false;
        }
        
        // Checks if any pattern fully matches the values in the grid based on the set directions
        return _pattern.Any(pattern => pattern.Select((value, index) => new { value, index }).
            All(c => c.value == _grid[i + _directionsPart2[c.index].Row, j + _directionsPart2[c.index].Column]));
    }


    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i < _grid.GetLength(0) && j >= 0 && j < _grid.GetLength(1);
    }
}