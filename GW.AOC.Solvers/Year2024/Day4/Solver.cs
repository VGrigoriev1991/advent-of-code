using System.Drawing;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

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

        results['X'].AddRange(data.GetAllItemCoordinates('X').Select(x => new PointsHolder { Points = [x] }));

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
                        if (!data.ContainsItem(item.Key, newPoint))
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
                    if (!data.ContainsItem(item.Key, newPoint))
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

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var aPoints = data.GetAllItemCoordinates('A');

        var sum = 0L;

        foreach (var aPoint in aPoints)
        {
            var isX =
                (data.ContainsItem('M', new Point(aPoint.X - 1, aPoint.Y - 1))
                 && data.ContainsItem('S', new Point(aPoint.X + 1, aPoint.Y + 1)))
                || (data.ContainsItem('S', new Point(aPoint.X - 1, aPoint.Y - 1))
                    && data.ContainsItem('M', new Point(aPoint.X + 1, aPoint.Y + 1)));
            if (!isX)
            {
                continue;
            }

            isX = (data.ContainsItem('M', new Point(aPoint.X - 1, aPoint.Y + 1))
                   && data.ContainsItem('S', new Point(aPoint.X + 1, aPoint.Y - 1)))
                  || (data.ContainsItem('S', new Point(aPoint.X - 1, aPoint.Y + 1))
                      && data.ContainsItem('M', new Point(aPoint.X + 1, aPoint.Y - 1)));
            if (isX)
            {
                sum++;
            }
        }

        Console.WriteLine(sum);

        return Task.CompletedTask;
    }
}

file class PointsHolder
{
    public Point? Direction { get; set; }

    public List<Point> Points { get; set; } = [];
}
