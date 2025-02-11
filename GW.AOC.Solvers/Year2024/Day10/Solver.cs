using System.Drawing;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day10;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var paths = GetPaths();

        Console.WriteLine(paths.Select(x => x.Value.Distinct().Count()).Sum());

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private Dictionary<Point, List<Point>> GetPaths()
    {
        var data = puzzleDataReader.ReadIntMatrix(PuzzleDataFilePath, string.Empty);

        var ninePoints = data.GetAllItemCoordinates(9);

        var paths = ninePoints.ToDictionary(x => x, x => new List<Point> { x });

        for (var i = 1; i <= 9; i++)
        {
            foreach (var path in paths)
            {
                var nextPoints = new List<Point>();
                foreach (var point in path.Value)
                {
                    nextPoints.AddRange(
                        PointDirection.Main.Select(direction => point.GetNext(direction))
                            .Where(
                                nextPoint => data.IsValidPoint(nextPoint) && data[nextPoint.X][nextPoint.Y] == data[point.X][point.Y] - 1));
                }

                path.Value.Clear();
                path.Value.AddRange(nextPoints);
            }
        }

        return paths;
    }
}
