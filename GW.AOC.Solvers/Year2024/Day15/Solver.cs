using System.Drawing;
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

        Console.WriteLine(GetSum(map, Symbol.BigO));

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var (map, input) = ReadData();

        map = Transform(map);

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
                    if (currentDirection.X == 0)
                    {
                        var nextValidPoint = nextPoint.GetNext(currentDirection);
                        while (map.IsValidPoint(nextValidPoint))
                        {
                            if (map.ContainsItem(Symbol.Sharp, nextValidPoint))
                            {
                                break;
                            }

                            if (map.ContainsItem(Symbol.SquareBracketOpen, nextValidPoint)
                                || map.ContainsItem(Symbol.SquareBracketClose, nextValidPoint))
                            {
                                nextValidPoint = nextValidPoint.GetNext(currentDirection);
                            }
                            else
                            {
                                if (map.ContainsItem(Symbol.SquareBracketOpen, nextPoint))
                                {
                                    map[nextValidPoint.X][nextValidPoint.Y] = Symbol.SquareBracketClose;
                                }
                                else
                                {
                                    map[nextValidPoint.X][nextValidPoint.Y] = Symbol.SquareBracketOpen;
                                }

                                map[nextPoint.X][nextPoint.Y] = Symbol.Dot;

                                var tempPoint = nextPoint.GetNext(currentDirection);
                                while (tempPoint != nextValidPoint)
                                {
                                    if (map.ContainsItem(Symbol.SquareBracketOpen, tempPoint))
                                    {
                                        map[tempPoint.X][tempPoint.Y] = Symbol.SquareBracketClose;
                                    }
                                    else
                                    {
                                        map[tempPoint.X][tempPoint.Y] = Symbol.SquareBracketOpen;
                                    }

                                    tempPoint = tempPoint.GetNext(currentDirection);
                                }

                                startPoint = nextPoint;
                                break;
                            }
                        }
                    }
                    else
                    {
                        var originalNextPoint = nextPoint;

                        if (map[nextPoint.X][nextPoint.Y] == Symbol.SquareBracketClose)
                        {
                            nextPoint.Y--;
                        }

                        var stack = new Stack<Point>();
                        stack.Push(nextPoint);

                        var queue = new Queue<Point>();
                        queue.Enqueue(nextPoint);

                        var isValid = true;

                        while (queue.Count > 0)
                        {
                            var point = queue.Dequeue();
                            var nextPointRight = point.GetNext(currentDirection);
                            var nextPointLeft = (point with { Y = point.Y + 1 }).GetNext(currentDirection);
                            if (!map.IsValidPoint(nextPointRight)
                                || !map.IsValidPoint(nextPointLeft)
                                || map[nextPointRight.X][nextPointRight.Y] == Symbol.Sharp
                                || map[nextPointLeft.X][nextPointLeft.Y] == Symbol.Sharp)
                            {
                                isValid = false;
                                break;
                            }

                            if (map[nextPointRight.X][nextPointRight.Y] == Symbol.Dot
                                && map[nextPointLeft.X][nextPointLeft.Y] == Symbol.Dot)
                            {
                                continue;
                            }

                            if (map[nextPointRight.X][nextPointRight.Y] == Symbol.SquareBracketOpen)
                            {
                                queue.Enqueue(nextPointRight);
                                stack.Push(nextPointRight);
                            }
                            else if (map[nextPointRight.X][nextPointRight.Y] == Symbol.SquareBracketClose)
                            {
                                queue.Enqueue(nextPointRight with { Y = point.Y - 1 });
                                stack.Push(nextPointRight with { Y = point.Y - 1 });
                            }

                            if (map[nextPointLeft.X][nextPointLeft.Y] == Symbol.SquareBracketOpen)
                            {
                                queue.Enqueue(nextPointLeft);
                                stack.Push(nextPointLeft);
                            }
                        }

                        if (isValid)
                        {
                            while (stack.Count > 0)
                            {
                                var point = stack.Pop();
                                map[point.X][point.Y] = Symbol.Dot;
                                map[point.X][point.Y + 1] = Symbol.Dot;

                                var nextValidPoint = point.GetNext(currentDirection);
                                map[nextValidPoint.X][nextValidPoint.Y] = Symbol.SquareBracketOpen;
                                map[nextValidPoint.X][nextValidPoint.Y + 1] = Symbol.SquareBracketClose;
                            }

                            startPoint = originalNextPoint;
                        }
                    }
                }
            }
        }

        Console.WriteLine(GetSum(map, Symbol.SquareBracketOpen));

        return Task.CompletedTask;
    }

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

    private static long GetSum(List<List<char>> map, char symbol)
    {
        var result = 0L;

        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[0].Count; j++)
            {
                if (map[i][j] == symbol)
                {
                    result += (i * 100) + j;
                }
            }
        }

        return result;
    }

    private static List<List<char>> Transform(List<List<char>> map)
    {
        var result = new List<List<char>>();

        foreach (var row in map)
        {
            var newMapRow = new List<char>();
            for (var j = 0; j < map[0].Count; j++)
            {
                switch (row[j])
                {
                    case Symbol.Sharp:
                        newMapRow.AddRange([Symbol.Sharp, Symbol.Sharp]);
                        break;
                    case Symbol.BigO:
                        newMapRow.AddRange([Symbol.SquareBracketOpen, Symbol.SquareBracketClose]);
                        break;
                    case Symbol.Dot:
                        newMapRow.AddRange([Symbol.Dot, Symbol.Dot]);
                        break;
                    case Symbol.At:
                        newMapRow.AddRange([Symbol.At, Symbol.Dot]);
                        break;
                }
            }

            result.Add(newMapRow);
        }

        return result;
    }
}
