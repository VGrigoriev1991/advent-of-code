using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day15;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var (map, input) = ReadData();

        var startPoint = map.GetItemCoordinates(Symbol.At, Symbol.Dot);

        foreach (var inputOption in input)
        {
            foreach (var option in inputOption)
            {
                var currentDirection = PointDirection.ArrowDirections[option];
                var nextPoint = startPoint.GetNext(currentDirection);

                if (!map.IsValidPoint(nextPoint) || map.ContainsItem(Symbol.Sharp, nextPoint))
                {
                    continue;
                }

                if (map.ContainsItem(Symbol.Dot, nextPoint))
                {
                    startPoint = nextPoint;
                }
                else
                {
                    var nextValidPoint = nextPoint.GetNext(currentDirection);
                    while (map.IsValidPoint(nextValidPoint))
                    {
                        if (map.ContainsItem(Symbol.Sharp, nextValidPoint))
                        {
                            break;
                        }

                        if (map.ContainsItem(Symbol.BigO, nextValidPoint))
                        {
                            nextValidPoint = nextValidPoint.GetNext(currentDirection);
                        }
                        else
                        {
                            map[nextValidPoint.X][nextValidPoint.Y] = Symbol.BigO;
                            map[nextPoint.X][nextPoint.Y] = Symbol.Dot;
                            startPoint = nextPoint;
                            break;
                        }
                    }
                }
            }
        }

        Console.WriteLine(GetSum(map));

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private (List<List<char>>, List<string>) ReadData()
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var map = new List<List<char>>();
        var input = new List<string>();

        var isArray = true;

        foreach (var line in data)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isArray = false;
                continue;
            }

            if (isArray)
            {
                map.Add(line.ToCharArray().ToList());
            }
            else
            {
                input.Add(line);
            }
        }

        return (map, input);
    }

    private static long GetSum(List<List<char>> field)
    {
        var result = 0L;

        for (var i = 0; i < field.Count; i++)
        {
            for (var j = 0; j < field[0].Count; j++)
            {
                if (field[i][j] == Symbol.BigO)
                {
                    result += (i * 100) + j;
                }
            }
        }

        return result;
    }
}
