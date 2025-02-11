using System.Drawing;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day8;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadCharMatrix(PuzzleDataFilePath);

        var nodes = GetNodes(data);

        var antiNodes = new HashSet<Point>();

        foreach (var node in nodes)
        {
            for (var i = 0; i < node.Value.Count - 1; i++)
            {
                for (var j = i + 1; j < node.Value.Count; j++)
                {
                    var antiNodeTop = new Point(
                        node.Value[i].X - (node.Value[j].X - node.Value[i].X),
                        node.Value[i].Y - (node.Value[j].Y - node.Value[i].Y));
                    if (data.IsValidPoint(antiNodeTop))
                    {
                        antiNodes.Add(antiNodeTop);
                    }

                    var antiNodeBottom = new Point(
                        node.Value[j].X + (node.Value[j].X - node.Value[i].X),
                        node.Value[j].Y + (node.Value[j].Y - node.Value[i].Y));
                    if (data.IsValidPoint(antiNodeBottom))
                    {
                        antiNodes.Add(antiNodeBottom);
                    }
                }
            }
        }

        Console.WriteLine(antiNodes.Count);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static Dictionary<char, List<Point>> GetNodes(List<List<char>> matrix)
    {
        var result = new Dictionary<char, List<Point>>();

        for (var i = 0; i < matrix.Count; i++)
        {
            for (var j = 0; j < matrix[0].Count; j++)
            {
                if (matrix[i][j] == Symbol.Dot)
                {
                    continue;
                }

                if (!result.TryGetValue(matrix[i][j], out var value))
                {
                    value = [];
                    result.Add(matrix[i][j], value);
                }

                value.Add(new Point(i, j));
            }
        }

        return result;
    }
}
