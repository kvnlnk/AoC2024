using System.Numerics;
using AoC2024.Common;

namespace AoC2024.Day14;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<Robot> _robots = [];
    private List<Robot> _robots2 = [];
    private readonly char[,] _grid;

    public Solver(List<string> list)
    {
        _list = list;
        _grid = new char[103, 101];
        GenerateGrid();
        SeparateList();
    }


    private void GenerateGrid()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                _grid.SetValue('.', i, j);
            }
        }
    }


    private void SeparateList()
    {
        foreach (var splitLine in _list.Select(line => line.Split(["=", ",", " ", "p", "v"], StringSplitOptions.RemoveEmptyEntries)))
        {
            _robots.Add(new Robot
            {
                Position = new Position { X = Convert.ToInt32(splitLine[0]), Y = Convert.ToInt32(splitLine[1]) },
                Velocity = new Velocity { X = Convert.ToInt32(splitLine[2]), Y = Convert.ToInt32(splitLine[3]) }
            });
        }

        // Create copy of list
        _robots2 = _robots.Select(robot => new Robot
        {
            Position = new Position
            {
                X = robot.Position.X,
                Y = robot.Position.Y
            },
            Velocity = new Velocity
            {
                X = robot.Velocity.X,
                Y = robot.Velocity.Y
            }
        }).ToList();
    }

    
    public string GetPartOneSolution()
    {
        // Set initial state of the robots
        foreach (var robot in _robots)
        {
            _grid[robot.Position.Y, robot.Position.X] = '1';
        }

        for (var i = 0; i < 100; i++)
        {
            MoveRobots(_robots);
        }

        return GetSumOfRobotsInQuadrants().Aggregate((total, next) => total * next).ToString();
    }

    
    public string GetPartTwoSolution()
    {
        var found = false;
        var iterations = 0;

        // Set initial state of the robots
        foreach (var robot in _robots2)
        {
            _grid[robot.Position.Y, robot.Position.X] = '1';
        }

        while (!found)
        {
            MoveRobots(_robots2);
            for (var a = 0; a < _grid.GetLength(0); a++)
            {
                var counter = 0;
                for (var b = 0; b < _grid.GetLength(1); b++)
                {
                    if (_grid[a, b] == '1')
                    {
                        counter++;
                        if (counter == 8)
                        {
                            found = true;
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            iterations++;
        }

        return iterations.ToString();
    }


    private void MoveRobots(List<Robot> robots)
    {
        foreach (var robot in robots)
        {
            // Remove old position
            if (_grid[robot.Position.Y, robot.Position.X] != '0' &&
                _grid[robot.Position.Y, robot.Position.X] != '.')
            {
                _grid[robot.Position.Y, robot.Position.X] = (char)(_grid[robot.Position.Y, robot.Position.X] - 1);
            }

            if (_grid[robot.Position.Y, robot.Position.X] == '0')
            {
                _grid[robot.Position.Y, robot.Position.X] = '.';
            }


            // Set new position
            if (!CheckBounds(robot.Position.Y + robot.Velocity.Y, robot.Position.X + robot.Velocity.X))
            {
                var yDistance = robot.Position.Y + robot.Velocity.Y;
                var xDistance = robot.Position.X + robot.Velocity.X;
                if (yDistance < 0)
                {
                    robot.Position.Y = _grid.GetLength(0) + yDistance;
                }
                else if (yDistance > _grid.GetLength(0) - 1)
                {
                    robot.Position.Y = -(_grid.GetLength(0) - yDistance);
                }
                else
                {
                    robot.Position.Y += robot.Velocity.Y;
                }

                if (xDistance < 0)
                {
                    robot.Position.X = _grid.GetLength(1) + xDistance;
                }
                else if (xDistance > _grid.GetLength(1) - 1)
                {
                    robot.Position.X = -(_grid.GetLength(1) - xDistance);
                }
                else
                {
                    robot.Position.X += robot.Velocity.X;
                }
            }
            else
            {
                robot.Position.Y += robot.Velocity.Y;
                robot.Position.X += robot.Velocity.X;
            }

            // Update number of robots on position

            if (_grid[robot.Position.Y, robot.Position.X] == '.')
            {
                _grid[robot.Position.Y, robot.Position.X] = '1';
            }
            else
            {
                _grid[robot.Position.Y, robot.Position.X] = (char)(_grid[robot.Position.Y, robot.Position.X] + 1);
            }
        }
    }

    
    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i < _grid.GetLength(0) && j >= 0 && j < _grid.GetLength(1);
    }

    
    
    private List<int> GetSumOfRobotsInQuadrants()
    {
        var sumQ1 = 0;
        var sumQ2 = 0;
        var sumQ3 = 0;
        var sumQ4 = 0;
        var midRow = _grid.GetLength(0) / 2;
        var midCol = _grid.GetLength(1) / 2;

        // Get sum of quadrant one
        for (var i = 0; i < midRow; i++)
        {
            for (var j = 0; j < midCol; j++)
            {
                if (_grid[i, j] == '.') continue;
                sumQ1 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant two
        for (var i = 0; i < midRow; i++)
        {
            for (var j = midCol + 1; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j] == '.') continue;
                sumQ2 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant three
        for (var i = midRow + 1; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1) / 2; j++)
            {
                if (_grid[i, j] == '.') continue;
                sumQ3 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant four
        for (var i = midRow + 1; i < _grid.GetLength(0); i++)
        {
            for (var j = midCol + 1; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j] == '.') continue;
                sumQ4 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        return [sumQ1, sumQ2, sumQ3, sumQ4];
    }
}


internal class Robot
{
    public required Position Position { get; set; }
    public required Velocity Velocity { get; set; }
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

internal class Velocity
{
    public int X { get; set; }
    public int Y { get; set; }
}