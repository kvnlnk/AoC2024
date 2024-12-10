using AoC2024.Common;

namespace AoC2024.Day10;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;
    private int _counter = 0;

    private const string Trail = "0123456789";

    private readonly Direction[] _directions =
    [
        new() { Row = -1, Column = 0 }, // North
        new() { Row = 0, Column = 1 }, // East
        new() { Row = 1, Column = 0 }, // South
        new() { Row = 0, Column = -1 }, // West
    ];

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
                // Find possible beginning of the trail
                if ((char)_grid.GetValue(i, j)! != Trail[0]) continue;

                if (!CheckDirection(1, i, j)) continue;
            }
        }

        return _counter.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }

    private bool CheckDirection(int nextLetter, int i, int j)
    {
        // Found word
        if (nextLetter == Trail.Length)
        {
            _counter++;
            return true;
        }
        var found = false;
        // Check next char
        foreach (var direction in _directions)
        {
            if (Trail[nextLetter] == '4')
            {
                Console.WriteLine("asd");
            }
            if (CheckBounds(i + direction.Row, j + direction.Column) &&
                Trail[nextLetter] == _grid[i + direction.Row, j + direction.Column])
            {
                found = CheckDirection(nextLetter + 1, i + direction.Row, j + direction.Column);
            }
        }

        return found;
    }


    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i < _grid.GetLength(0) && j >= 0 && j < _grid.GetLength(1);
    }
}