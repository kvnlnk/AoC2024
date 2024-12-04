using AoC2024.Common;

namespace AoC2024.Day04;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;

    private readonly Direction[] _directions =
    [
        new() { X = -1, Y = 0 }, // North
        new() { X = -1, Y = 1 }, // North-East
        new() { X = 0, Y = 1 }, // East
        new() { X = 1, Y = 1 }, // South-East
        new() { X = 1, Y = 0 }, // South
        new() { X = 1, Y = -1 }, // South-West
        new() { X = 0, Y = -1 }, // West
        new() { X = -1, Y = -1 } // North-West
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
                foreach (var direction in _directions)
                {
                    if (!CheckDirection(1, i, j, direction)) continue;
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
        if (CheckBounds(i + direction.X, j + direction.Y) &&
            Word[nextLetter] == _grid[i + direction.X, j + direction.Y])
        {
            return CheckDirection(nextLetter + 1, i + direction.X, j + direction.Y, direction);
        }

        return false;
    }

    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i < _grid.GetLength(0) && j >= 0 && j < _grid.GetLength(1);
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
}

internal class Direction()
{
    public int X { get; set; }
    public int Y { get; set; }
}