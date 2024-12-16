using System.Runtime.Intrinsics.X86;
using AoC2024.Common;

namespace AoC2024.Day15;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<char> _moves = [];
    private readonly List<string> _map = [];
    private readonly char[,] _grid;
    private Position _freeAtPosition;
    private Robot _robot;


    private int _rows;
    private int _columns;

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
        SeparateList();
        _grid = new char[_rows, _columns];
        GenerateGrid();
    }

    private void SeparateList()
    {
        var addMoves = false;
        foreach (var line in _list)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                addMoves = true;
                continue;
            }

            if (addMoves)
            {
                foreach (var move in line)
                {
                    _moves.Add(Convert.ToChar(move));
                }

                Console.WriteLine(string.Join(",", _moves));
            }
            else
            {
                _map.Add(line);
                _rows++;
                _columns = line.Length;
            }
        }
    }


    private void GenerateGrid()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                _grid.SetValue(_map[i][j], i, j);
                if (_map[i][j] == '@')
                {
                    _robot = new Robot
                    {
                        Position = new Position
                        {
                            X = i,
                            Y = j
                        }
                    };
                }
            }
        }
    }

    public string GetPartOneSolution()
    {
        var sum = 0;
        
        foreach (var move in _moves)
        {
            var direction = GetDirection(move);
            
            // Skip to next iteration if # is blocking the way
            if ((char)_grid.GetValue(_robot.Position.Y + direction.Row,
                    _robot.Position.X + direction.Column)! == '#') continue;
            
            // Check for free space if O is blocking the way
            if ((char)_grid.GetValue(_robot.Position.Y + direction.Row,
                    _robot.Position.X + direction.Column)! == 'O')
            {
                if (CheckForFreeSpace(direction, _robot.Position.Y + 2 * direction.Row, _robot.Position.X + 2 * direction.Column))
                {
                    Console.WriteLine("Free");
                    _grid[_robot.Position.Y, _robot.Position.X] = '.';
                    _robot.Position.Y += direction.Row;
                    _robot.Position.X += direction.Column;
                    _grid[_robot.Position.Y, _robot.Position.X] = '@';
                    _grid[_freeAtPosition.Y, _freeAtPosition.X] = 'O';
                    
                }
            }
            else
            {
                // Move robot to next spot
                _grid[_robot.Position.Y, _robot.Position.X] = '.';
                _robot.Position.Y += direction.Row;
                _robot.Position.X += direction.Column;
                _grid[_robot.Position.Y, _robot.Position.X] = '@';
            }

        }
        
        // Get sum of all boxes' GPS coordinates
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j] == 'O')
                {
                    sum += i * 100 + j;
                }
            }
        }
        return sum.ToString();
    }

    
    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }


    private bool CheckForFreeSpace(Direction direction, int y, int x)
    {
        while (true)
        {
            if ((char)_grid.GetValue(y, x)! == '.')
            {
                _freeAtPosition = new Position
                {
                    X = x,
                    Y = y
                };
                return true;
            }

            if ((char)_grid.GetValue(y, x)! == '#')
            {
                return false;
            }
            
            if((char)_grid.GetValue(y, x)! == 'O')
            {
                y += direction.Row;
                x += + direction.Column;
            }
        }
    }

    
    private Direction GetDirection(char move)
    {
        return move switch
        {
            '^' => _directions[0],
            '>' => _directions[1],
            'v' => _directions[2],
            _ => _directions[3]
        };
    }
}

internal class Robot
{
    public required Position Position { get; set; }
}

internal class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Position position)
        {
            return false;
        }

        return X.Equals(position.X) && Y.Equals(position.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return "X: " + X + "   " + "Y: " + Y;
    }
}