using AoC2024.Common;

namespace AoC2024.Day06;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly char[,] _grid;
    
    private readonly Direction[] _directions =
    [
        new() { X = -1, Y = 0 }, // North
        new() { X = 0, Y = 1 }, // East
        new() { X = 1, Y = 0 }, // South
        new() { X = 0, Y = -1 }, // West
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
                        Position = new Position {X=i, Y=j},
                        Direction = _directions[0] // Facing north first
                    };

                }
            }
        }
    }
    public string GetPartOneSolution()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                Console.Write(_grid.GetValue(i, j));
            }

            Console.WriteLine();
        }

        while (true)
        {
            if (_guard.Direction.)
        }
        
        Console.WriteLine(_guard.Position.X  + " " + _guard.Position.Y);
        Console.WriteLine(_guard.Direction.X + " " + _guard.Direction.Y);
        return "";
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }
    
}

internal class Guard
{
    public required Position Position { get; set; }
    public required Direction Direction { get; set; }
}

internal class Position
{
    public int X { get; set; }
    public int Y { get; set; }
}
