using System.Drawing;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day4;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var results = new Dictionary<char, List<PointsHolder>>
        {
            { 'X', [] },
            { 'M', [] },
            { 'A', [] },
            { 'S', [] }
        };

        for (var i = 0; i < data.Count; i++)
        {
            for (var j = 0; j < data[0].Count; j++)
            {
                if (data[i][j] == 'X')
                {
                    results['X'].Add(new PointsHolder { Points = [new Point(i, j)] });
                }
            }
        }

        var points = results['X'];

        var directions = new List<Point>
        {
            new(-1, -1),
            new(-1, 0),
            new(-1, 1),
            new(0, -1),
            new(0, 1),
            new(1, -1),
            new(1, 0),
            new(1, 1)
        };

        foreach (var item in results.Where(x => x.Key != 'X'))
        {
            foreach (var point in points)
            {
                var lastPoint = point.Points.Last();

                if (point.Direction == null)
                {
                    foreach (var direction in directions)
                    {
                        var newPoint = new Point(lastPoint.X + direction.X, lastPoint.Y + direction.Y);
                        if (!IsLetter(data, item.Key, newPoint))
                        {
                            continue;
                        }

                        var newOption = new PointsHolder
                        {
                            Points = [..point.Points, newPoint],
                            Direction = direction
                        };
                        item.Value.Add(newOption);
                    }
                }
                else
                {
                    var newPoint = new Point(
                        lastPoint.X + point.Direction!.Value.X,
                        lastPoint.Y + point.Direction!.Value.Y);
                    if (!IsLetter(data, item.Key, newPoint))
                    {
                        continue;
                    }

                    var newOption = new PointsHolder
                    {
                        Points = [..point.Points, newPoint],
                        Direction = point.Direction
                    };
                    item.Value.Add(newOption);
                }
            }

            points = item.Value;
        }

        Console.WriteLine(results['S'].Count);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static bool IsLetter(List<List<char>> array, char letter, Point point)
    {
        if (point is { X: >= 0, Y: >= 0 } && point.X < array.Count && point.Y < array[0].Count)
        {
            return array[point.X][point.Y] == letter;
        }

        return false;
    }
}

file class PointsHolder
{
    public Point? Direction { get; set; }

    public List<Point> Points { get; set; } = [];
}
