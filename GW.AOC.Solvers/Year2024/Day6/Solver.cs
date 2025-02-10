using System.Drawing;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day6;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var visitedPoints = GetVisitedPoints(data);

        Console.WriteLine(visitedPoints.Count);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var visitedPoints = GetVisitedPoints(data);

        var result = 0L;

        foreach (var visitedPoint in visitedPoints.Skip(1))
        {
            var startPoint = visitedPoints.First();
            var currentDirection = PointDirection.Top;
            var newlyVisitedPoints = new HashSet<(Point, Point)> { (startPoint, currentDirection) };

            while (true)
            {
                var nextPoint = startPoint.GetNext(currentDirection);
                if (!data.IsValidPoint(nextPoint))
                {
                    break;
                }

                if ((data.ContainsItem(Symbol.Dot, nextPoint) || data.ContainsItem(Symbol.Caret, nextPoint))
                    && nextPoint != visitedPoint)
                {
                    startPoint = nextPoint;

                    var newlyVisitedPoint = (startPoint, currentDirection);

                    if (newlyVisitedPoints.Add(newlyVisitedPoint))
                    {
                        continue;
                    }

                    result++;
                    break;
                }

                if (data.ContainsItem(Symbol.Sharp, nextPoint) || nextPoint == visitedPoint)
                {
                    currentDirection = currentDirection.GetNextMainDirection();
                }
            }
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private static HashSet<Point> GetVisitedPoints(List<List<char>> matrix)
    {
        var startPoint = matrix.GetItemCoordinates(Symbol.Caret);

        var currentDirection = PointDirection.Top;
        var visitedPoints = new HashSet<Point> { startPoint };

        while (true)
        {
            var nextPoint = startPoint.GetNext(currentDirection);
            if (!matrix.IsValidPoint(nextPoint))
            {
                break;
            }

            if (matrix.ContainsItem(Symbol.Dot, nextPoint) || matrix.ContainsItem(Symbol.Caret, nextPoint))
            {
                startPoint = nextPoint;
                visitedPoints.Add(startPoint);
            }
            else if (matrix.ContainsItem(Symbol.Sharp, nextPoint))
            {
                currentDirection = currentDirection.GetNextMainDirection();
            }
        }

        return visitedPoints;
    }
}
