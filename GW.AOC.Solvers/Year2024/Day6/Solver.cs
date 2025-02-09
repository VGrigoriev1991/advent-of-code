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

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static HashSet<string> GetVisitedPoints(List<List<char>> matrix)
    {
        var startPoint = matrix.GetItemCoordinates(Symbol.Caret);

        var currentDirection = PointDirection.Top;
        var visitedPoints = new HashSet<string> { startPoint.ToString() };

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
                visitedPoints.Add(startPoint.ToString());
            }
            else if (matrix.ContainsItem(Symbol.Sharp, nextPoint))
            {
                currentDirection = currentDirection.GetNextMainDirection();
            }
        }

        return visitedPoints;
    }
}
