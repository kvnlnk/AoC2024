using System.Numerics;
using AoC2024.Common;

namespace AoC2024.Day14;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<Robot> _robots = [];
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
                _grid.SetValue('0', i, j);
            }
        }
    }


    private void SeparateList()
    {
        foreach (var line in _list)
        {
            var splitLine = line.Split(["=", ",", " ", "p", "v"], StringSplitOptions.RemoveEmptyEntries);
            _robots.Add(new Robot
            {
                Position = new Position { X = Convert.ToInt32(splitLine[0]), Y = Convert.ToInt32(splitLine[1]) },
                Velocity = new Velocity { X = Convert.ToInt32(splitLine[2]), Y = Convert.ToInt32(splitLine[3]) }
            });
        }
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
            foreach (var robot in _robots)
            {
                // Remove old position
                if (_grid[robot.Position.Y, robot.Position.X] != '0')
                {
                    _grid[robot.Position.Y, robot.Position.X] = (char)(_grid[robot.Position.Y, robot.Position.X] - 1);
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
                _grid[robot.Position.Y, robot.Position.X] = (char)(_grid[robot.Position.Y, robot.Position.X] + 1);
            }
        }

        return GetSumOfRobotsInQuadrants().Aggregate((total, next) => total * next).ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
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
                sumQ1 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant two
        for (var i = 0; i < midRow; i++)
        {
            for (var j = midCol + 1; j < _grid.GetLength(1); j++)
            {
                sumQ2 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant three
        for (var i = midRow + 1; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1) / 2; j++)
            {
                sumQ3 += Convert.ToInt32(_grid[i, j] - '0');
            }
        }

        // Get sum of quadrant four
        for (var i = midRow + 1; i < _grid.GetLength(0); i++)
        {
            for (var j = midCol + 1; j < _grid.GetLength(1); j++)
            {
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