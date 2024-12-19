using AoC2024.Common;

namespace AoC2024.Day12;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly Field[,] _grid;
    private List<List<Field>> _connectedFields = [];
    private List<Field> _visitedFields = [];

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
        _grid = new Field[list.Count, list[0].Length];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                _grid.SetValue(new Field { Name = _list[i][j], Row = i, Column = j }, i, j);
            }
        }
    }

    public string GetPartOneSolution()
    {
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                var field = _grid[i, j];
                if (_visitedFields.Any(f => f.Equals(field))) continue;

                // Mark field as visited
                _visitedFields.Add(field);
                var area = new List<Field> { field };

                // Recursive Search to get other connected fields
                CheckDirections(field, area);
                _connectedFields.Add(area);
            }
        }

        return _connectedFields.Sum(connectedField =>
            connectedField.Count * CountAreaPerimeter(connectedField)).ToString();
    }

    public string GetPartTwoSolution()
    {
        var totalSum = 0;
        foreach (var connectedField in _connectedFields)
        {
            var corners = 0;
            foreach (var field in connectedField)
            {

                var sum = 0;
                if (field is { North: false, East: false })
                {
                    sum++;
                }

                if (field is { North: false, West: false })
                {
                    sum++;
                }

                if (field is { South: false, East: false })
                {
                    sum++;
                }

                if (field is { South: false, West: false })
                {
                    sum++;
                }

                var neighborFieldBottomRight = connectedField.Find(f => f.Row == field.Row + 1 && f.Column == field.Column + 1);
                var neighborFieldRight = connectedField.Find(f => f.Row == field.Row && f.Column == field.Column + 1);
                var neighborFieldBottom = connectedField.Find(f => f.Row == field.Row + 1 && f.Column == field.Column);
                var neighborFieldBottomLeft = connectedField.Find(f => f.Row == field.Row + 1 && f.Column == field.Column - 1);
                var neighborFieldLeft = connectedField.Find(f => f.Row == field.Row && f.Column == field.Column - 1);
                
                if (field is { South: false } && neighborFieldBottomRight is { West: false } &&
                    neighborFieldRight != null &&
                    field.Name.Equals(neighborFieldRight.Name))
                {
                    sum++;
                }

                if (field is { East: false } && neighborFieldBottomRight is { North: false } &&
                    neighborFieldBottom != null &&
                    field.Name.Equals(neighborFieldBottom.Name) )
                {
                    sum++;
                }
                
                if (field is { West: false } && neighborFieldBottomLeft is { North: false } &&
                    neighborFieldBottom != null &&
                    field.Name.Equals(neighborFieldBottom.Name))
                {
                    sum++;
                }

                if (field is { South: false } && neighborFieldBottomLeft is { East: false } &&
                    neighborFieldLeft != null &&
                    field.Name.Equals(neighborFieldLeft.Name))
                {
                    sum++;
                }

                corners += sum;
            }

            totalSum += corners * connectedField.Count;
        }

        return totalSum.ToString();
    }


    private void CheckDirections(Field field, List<Field> area)
    {
        if (CheckBounds(field.Row + _directions[0].Row, field.Column + _directions[0].Column))
        {
            var nextField = _grid[field.Row + _directions[0].Row, field.Column + _directions[0].Column];
            if (nextField.Name == field.Name)
            {
                if (!_visitedFields.Any(f => f.Equals(nextField)))
                {
                    area.Add(nextField);
                    _visitedFields.Add(nextField);
                    CheckDirections(nextField, area);
                }

                field.North = true;
                nextField.South = true;
            }
        }
        else
        {
            field.North = false;
        }

        if (CheckBounds(field.Row + _directions[1].Row, field.Column + _directions[1].Column))
        {
            var nextField = _grid[field.Row + _directions[1].Row, field.Column + _directions[1].Column];
            if (nextField.Name == field.Name)
            {
                if (!_visitedFields.Any(f => f.Equals(nextField)))
                {
                    area.Add(nextField);
                    _visitedFields.Add(nextField);

                    CheckDirections(nextField, area);
                }

                field.East = true;
                nextField.West = true;
            }
        }
        else
        {
            field.East = false;
        }

        if (CheckBounds(field.Row + _directions[2].Row, field.Column + _directions[2].Column))
        {
            var nextField = _grid[field.Row + _directions[2].Row, field.Column + _directions[2].Column];
            if (nextField.Name == field.Name)
            {
                if (!_visitedFields.Any(f => f.Equals(nextField)))
                {
                    area.Add(nextField);
                    _visitedFields.Add(nextField);

                    CheckDirections(nextField, area);
                }

                field.South = true;
                nextField.North = true;
            }
        }
        else
        {
            field.South = false;
        }

        if (CheckBounds(field.Row + _directions[3].Row, field.Column + _directions[3].Column))
        {
            var nextField = _grid[field.Row + _directions[3].Row, field.Column + _directions[3].Column];
            if (nextField.Name == field.Name)
            {
                if (!_visitedFields.Any(f => f.Equals(nextField)))
                {
                    area.Add(nextField);
                    _visitedFields.Add(nextField);

                    CheckDirections(nextField, area);
                }

                field.West = true;
                nextField.East = true;
            }
        }
        else
        {
            field.West = false;
        }
    }

    private static int CountAreaPerimeter(List<Field> connectedFields)
    {
        return connectedFields.Sum(field =>
            (field.North ? 0 : 1) +
            (field.East ? 0 : 1) +
            (field.South ? 0 : 1) +
            (field.West ? 0 : 1)
        );
    }


    private bool CheckBounds(int i, int j)
    {
        return i >= 0 && i <= _grid.GetLength(0) - 1 && j >= 0 && j <= _grid.GetLength(1) - 1;
    }
}

internal class Field
{
    // To indicate if similar fields are connected
    public char Name { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool North { get; set; }
    public bool East { get; set; }
    public bool South { get; set; }
    public bool West { get; set; }

    public override string ToString()
    {
        return Name.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Field field)
        {
            return false;
        }

        return Row.Equals(field.Row) && Column.Equals(field.Column);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }
}