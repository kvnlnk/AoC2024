using System.Threading.Channels;
using AoC2024.Common;

namespace AoC2024.Day06;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;
    private readonly HashSet<Position> _distinctPositions = [];

    private readonly Direction[] _directions =
    [
        new() { Row = -1, Column = 0 }, // North
        new() { Row = 0, Column = 1 }, // East
        new() { Row = 1, Column = 0 }, // South
        new() { Row = 0, Column = -1 }, // West
    ];

    private Guard _guard;


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
                if (_list[i][j] == '^')
                {
                    _guard = new Guard
                    {
                        Position = new Position { Row = i, Column = j },
                        Direction = _directions[0] // Facing north first
                    };
                }
            }
        }
    }

    public string GetPartOneSolution()
    {
        var directionIndex = 0;

        while (CheckBounds(_guard.Position.Row, _guard.Position.Column))
        {
            // Check if # is blocking the way for the guard
            if ((char)_grid.GetValue(_guard.Position.Row + _directions[directionIndex].Row,
                    _guard.Position.Column + _directions[directionIndex].Column)! != '#')
            {
                // Add position to hashset to count distinct positions later
                _distinctPositions.Add(_guard.Position);

                // Update position of the guard
                _guard.Position.Row += _directions[directionIndex].Row;
                _guard.Position.Column += _directions[directionIndex].Column;
            }
            else
            {
                // Change direction if # is blocking the way
                directionIndex = (directionIndex + 1) % 4;
            }
        }

        return (_distinctPositions.Count + 1).ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }

    private bool CheckBounds(int i, int j)
    {
        return i > 0 && i < _grid.GetLength(0) - 1 && j > 0 && j < _grid.GetLength(1) - 1;
    }
}

internal class Guard
{
    public required Position Position { get; set; }
    public required Direction Direction { get; set; }
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