using AoC2024.Common;

namespace AoC2024.Day10;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;
    private int _counterPart1;
    private int _counterPart2;

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
                var positionSet = new HashSet<Position>();
                if (!CheckDirection(1, i, j, positionSet)) continue;
            }
        }

        return _counterPart1.ToString();
    }

    public string GetPartTwoSolution()
    {

        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                // Find possible beginning of the trail
                if ((char)_grid.GetValue(i, j)! != Trail[0]) continue;
                if (!CheckDirection(1, i, j)) continue;
            }
        }

        return _counterPart2.ToString();
    }

    private bool CheckDirection(int nextNumber, int i, int j, HashSet<Position> positionSet)
    {
        // Found hiking trail
        if (nextNumber == Trail.Length)
        {
            // Check if highest position was already reached
            if (positionSet.Contains(new Position { Row = i, Column = j })) return true;
            _counterPart1++;
            positionSet.Add(new Position { Row = i, Column = j });

            return true;
        }
 
        // Recursion to check every direction of the hiking trail
        foreach (var direction in _directions)
        {
            if (CheckBounds(i + direction.Row, j + direction.Column) &&
                Trail[nextNumber] == _grid[i + direction.Row, j + direction.Column])
            {
                CheckDirection(nextNumber + 1, i + direction.Row, j + direction.Column,positionSet);
            }
        }

        return false;
    }

    private bool CheckDirection(int nextNumber, int i, int j)
    {
        // Found hiking trail
        if (nextNumber == Trail.Length)
        {
            _counterPart2++;
            return true;
        }
 
        // Recursion to check every direction of the hiking trail
        foreach (var direction in _directions)
        {
            if (CheckBounds(i + direction.Row, j + direction.Column) &&
                Trail[nextNumber] == _grid[i + direction.Row, j + direction.Column])
            {
                CheckDirection(nextNumber + 1, i + direction.Row, j + direction.Column);
            }
        }

        return false;
    }

    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i < _grid.GetLength(0) && j >= 0 && j < _grid.GetLength(1);
    }


    internal class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Position position)
            {
                return false;
            }

            return Row.Equals(position.Row) && Column.Equals(position.Column);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}