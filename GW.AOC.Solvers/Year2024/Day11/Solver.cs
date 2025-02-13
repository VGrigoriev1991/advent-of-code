using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day11;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var result = GetStonesCount(25);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var result = GetStonesCount(75);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private long GetStonesCount(int blinkingCount)
    {
        var data = puzzleDataReader.ReadAllLineParts(PuzzleDataFilePath, Delimiter.Space);

        var dictionary = data[0].ToLookup(x => x).ToDictionary(x => x.Key, x => (long)x.Count());

        var result = 0L;

        for (var i = 0; i < blinkingCount; i++)
        {
            var workingDictionary = new Dictionary<string, long>();
            foreach (var item in dictionary)
            {
                if (item.Key == "0")
                {
                    workingDictionary.TryAdd("1", 0);
                    workingDictionary["1"] += item.Value;
                }
                else if (item.Key.Length % 2 == 0)
                {
                    var firstPart = item.Key[..(item.Key.Length / 2)];
                    var secondPart = long.Parse(item.Key[(item.Key.Length / 2)..]).ToString();

                    workingDictionary.TryAdd(firstPart, 0);
                    workingDictionary.TryAdd(secondPart, 0);

                    workingDictionary[firstPart] += item.Value;
                    workingDictionary[secondPart] += item.Value;
                }
                else
                {
                    var digit = (long.Parse(item.Key) * 2024).ToString();
                    workingDictionary.TryAdd(digit, 0);
                    workingDictionary[digit] += item.Value;
                }
            }

            dictionary = workingDictionary;

            result = workingDictionary.Sum(x => x.Value);
        }

        return result;
    }
}
