using System.Drawing;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day12;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var result = ProcessRegions(
            (currentResult, matrix) =>
            {
                var perimeter = CalculatePerimeter(matrix);
                currentResult += CalculateDotsCount(matrix) * perimeter;

                return currentResult;
            });

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var result = ProcessRegions(
            (currentResult, matrix) =>
            {
                var sides = CalculateSides(matrix);
                currentResult += CalculateDotsCount(matrix) * sides;

                return currentResult;
            });

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private long ProcessRegions(Func<long, List<List<char>>, long> calculateRegionParameter)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var result = 0L;

        var nextPoint = data.GetOppositeItemCoordinates(Symbol.Plus);
        while (nextPoint != null)
        {
            PlotRegion(data, nextPoint.Value);

            result = calculateRegionParameter(result, data);

            data.ReplaceItems(Symbol.Dot, Symbol.Plus);

            nextPoint = data.GetOppositeItemCoordinates(Symbol.Plus);
        }

        return result;
    }

    private static int CalculateDotsCount(List<List<char>> matrix) => matrix.Select(x => x.Count(i => i == Symbol.Dot)).Sum();

    private static void PlotRegion(List<List<char>> matrix, Point startPoint)
    {
        var queue = new Queue<Point>();
        queue.Enqueue(startPoint);

        var regionChar = matrix[startPoint.X][startPoint.Y];

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();
            matrix[point.X][point.Y] = Symbol.Dot;

            foreach (var direction in PointDirection.Main)
            {
                var newPoint = point.GetNext(direction);
                if (!matrix.IsValidPoint(newPoint) || matrix[newPoint.X][newPoint.Y] != regionChar)
                {
                    continue;
                }

                if (!queue.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                }
            }
        }
    }

    private static int CalculatePerimeter(List<List<char>> matrix)
    {
        var result = 0;

        for (var i = 0; i < matrix.Count; i++)
        {
            for (var j = 0; j < matrix[0].Count; j++)
            {
                if (matrix[i][j] != Symbol.Dot)
                {
                    continue;
                }

                foreach (var direction in PointDirection.Main)
                {
                    var newPoint = new Point(i + direction.X, j + direction.Y);
                    if (!matrix.IsValidPoint(newPoint)
                        || matrix[newPoint.X][newPoint.Y] != Symbol.Dot)
                    {
                        result++;
                    }
                }
            }
        }

        return result;
    }

    private static int CalculateSides(List<List<char>> matrix)
    {
        var sides = new List<Side>();

        for (var i = 0; i < matrix.Count; i++)
        {
            for (var j = 0; j < matrix[0].Count; j++)
            {
                if (matrix[i][j] != Symbol.Dot)
                {
                    continue;
                }

                foreach (var direction in PointDirection.Main)
                {
                    var newPoint = new Point(i + direction.X, j + direction.Y);
                    if ((!matrix.IsValidPoint(newPoint) || matrix[newPoint.X][newPoint.Y] != Symbol.Dot)
                        && !sides.Any(x => x.Direction == direction && x.AllPoints.Contains(new Point(i, j))))
                    {
                        var side = new Side
                        {
                            StartPoint = new Point(i, j),
                            Direction = direction,
                            AllPoints = [new Point(i, j)]
                        };

                        if (direction.Y == 0)
                        {
                            for (var k = j + 1; k < matrix[0].Count; k++)
                            {
                                var sidePoint = new Point(i + direction.X, k + direction.Y);
                                if (matrix[i][k] == Symbol.Dot
                                    && (!matrix.IsValidPoint(sidePoint) || matrix[sidePoint.X][sidePoint.Y] != Symbol.Dot))
                                {
                                    side.AllPoints.Add(new Point(i, k));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if (direction.X == 0)
                        {
                            for (var k = i + 1; k < matrix.Count; k++)
                            {
                                var sidePoint = new Point(k + direction.X, j + direction.Y);
                                if (matrix[k][j] == Symbol.Dot
                                    && (!matrix.IsValidPoint(sidePoint) || matrix[sidePoint.X][sidePoint.Y] != Symbol.Dot))
                                {
                                    side.AllPoints.Add(new Point(k, j));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        sides.Add(side);
                    }
                }
            }
        }

        return sides.Count;
    }
}

file class Side
{
    public Point StartPoint { get; set; }

    public Point Direction { get; set; }

    public List<Point> AllPoints { get; set; } = [];
}
