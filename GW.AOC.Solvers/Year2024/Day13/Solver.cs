using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day13;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var strategies = ReadStrategies();

        var result = 0L;

        foreach (var strategy in strategies)
        {
            var buttonACount = 0;

            var buttonBCount = strategy.PrizeX / strategy.ButtonBX;

            while (buttonACount <= 100)
            {
                var tempX = (buttonBCount * strategy.ButtonBX) + (buttonACount * strategy.ButtonAX);
                var tempY = (buttonBCount * strategy.ButtonBY) + (buttonACount * strategy.ButtonAY);

                if (tempX == strategy.PrizeX && tempY == strategy.PrizeY)
                {
                    result += (buttonACount * 3) + buttonBCount;
                    break;
                }

                if (tempX < strategy.PrizeX || tempY < strategy.PrizeY)
                {
                    buttonACount++;
                }
                else if (buttonBCount > 0)
                {
                    buttonBCount--;
                }
                else
                {
                    break;
                }
            }
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var strategies = ReadStrategies();

        var result = 0L;

        foreach (var strategy in strategies)
        {
            strategy.PrizeX += 10000000000000;
            strategy.PrizeY += 10000000000000;

            var aCount =
                ((strategy.PrizeX * strategy.ButtonBY) - (strategy.PrizeY * strategy.ButtonBX))
                / ((strategy.ButtonAX * strategy.ButtonBY) - (strategy.ButtonAY * strategy.ButtonBX));
            var bCount = (strategy.PrizeX - (strategy.ButtonAX * aCount)) / strategy.ButtonBX;

            var resX = (strategy.ButtonAX * aCount) + (strategy.ButtonBX * bCount);
            var resY = (strategy.ButtonAY * aCount) + (strategy.ButtonBY * bCount);

            if (resY == strategy.PrizeY && resX == strategy.PrizeX)
            {
                result += (aCount * 3) + bCount;
            }
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private List<ButtonsStrategy> ReadStrategies()
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var strategies = new List<ButtonsStrategy>();

        for (var i = 0; i < data.Count - 2; i += 4)
        {
            var strategy = new ButtonsStrategy();

            var parts = data[i].Replace("Button A: ", string.Empty).Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            strategy.ButtonAX = long.Parse(parts[0].Replace("X+", string.Empty));
            strategy.ButtonAY = long.Parse(parts[1].Replace("Y+", string.Empty));

            parts = data[i + 1].Replace("Button B: ", string.Empty).Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            strategy.ButtonBX = long.Parse(parts[0].Replace("X+", string.Empty));
            strategy.ButtonBY = long.Parse(parts[1].Replace("Y+", string.Empty));

            parts = data[i + 2].Replace("Prize: ", string.Empty).Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            strategy.PrizeX = long.Parse(parts[0].Replace("X=", string.Empty));
            strategy.PrizeY = long.Parse(parts[1].Replace("Y=", string.Empty));

            strategies.Add(strategy);
        }

        return strategies;
    }
}

sealed record ButtonsStrategy
{
    public long ButtonAX { get; set; }
    public long ButtonAY { get; set; }

    public long ButtonBX { get; set; }
    public long ButtonBY { get; set; }

    public long PrizeX { get; set; }
    public long PrizeY { get; set; }
}
