using System.Drawing;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day14;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    private const int SizeX = 101;
    private const int SizeY = 103;

    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var robots = ReadData();

        const int movesCount = 100;

        const int middleX = SizeX / 2;
        const int middleY = SizeY / 2;

        var finalPositions = GetNextRobotPositions(robots, movesCount);

        var counts = new int[4];
        foreach (var finalPosition in finalPositions)
        {
            if (finalPosition.X == middleX || finalPosition.Y == middleY)
            {
                continue;
            }

            if (finalPosition is { X: < middleX, Y: < middleY })
            {
                counts[0]++;
            }
            else if (finalPosition is { X: < middleX, Y: > middleY })
            {
                counts[1]++;
            }
            else if (finalPosition is { X: > middleX, Y: < middleY })
            {
                counts[2]++;
            }
            else
            {
                counts[3]++;
            }
        }

        var result = counts.Aggregate(1, (current, count) => current * count);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var robots = ReadData();

        var movesCount = 100;

        while (true)
        {
            movesCount++;

            var finalPositions = GetNextRobotPositions(robots, movesCount);

            var count = finalPositions.ToLookup(x => x).Count;
            if (count != robots.Count)
            {
                continue;
            }

            Console.WriteLine(movesCount);
            break;
        }

        return Task.CompletedTask;
    }

    private List<Robot> ReadData()
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var result = data.Select(
                x =>
                {
                    var lineParts = x.ToParts(Delimiter.Space);
                    var position = lineParts[0].Replace("p=", string.Empty).ToParts(Delimiter.Comma);
                    var move = lineParts[1].Replace("v=", string.Empty).ToParts(Delimiter.Comma);

                    return
                        new Robot
                        {
                            Position = new Point(int.Parse(position[0]), int.Parse(position[1])),
                            Move = new Point(int.Parse(move[0]), int.Parse(move[1]))
                        };
                })
            .ToList();

        return result;
    }

    private static List<Point> GetNextRobotPositions(List<Robot> robots, int movesCount)
    {
        List<Point> result = [];

        foreach (var robot in robots)
        {
            var finalPositionX = (robot.Position.X + (robot.Move.X * movesCount)) % SizeX;
            var finalPositionY = (robot.Position.Y + (robot.Move.Y * movesCount)) % SizeY;

            var finalPosition = new Point(
                finalPositionX >= 0 ? finalPositionX : SizeX + finalPositionX,
                finalPositionY >= 0 ? finalPositionY : SizeY + finalPositionY);

            result.Add(finalPosition);
        }

        return result;
    }
}

internal record Robot
{
    public Point Position { get; set; }

    public Point Move { get; set; }
}
