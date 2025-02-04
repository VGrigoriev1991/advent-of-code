using System.Text.RegularExpressions;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day3;

public partial class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllText(PuzzleDataFilePath);

        var count = GetMatchesCount(data);

        Console.WriteLine(count);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllText(PuzzleDataFilePath);

        var indexDont = data.IndexOf("don't()", StringComparison.InvariantCulture);

        while (indexDont >= 0)
        {
            if (indexDont > 0)
            {
                var indexDo = data[(indexDont + 7)..].IndexOf("do()", StringComparison.InvariantCulture);
                if (indexDo > 0)
                {
                    data = data[..indexDont] + data[(indexDont + 7 + indexDo + 4)..];
                }
                else
                {
                    data = data[..indexDont];
                }
            }

            indexDont = data.IndexOf("don't()", StringComparison.InvariantCulture);
        }

        var count = GetMatchesCount(data);

        Console.WriteLine(count);

        return Task.CompletedTask;
    }

    private static long GetMatchesCount(string source)
    {
        var reg = SourceRegex();

        var dataMatch = reg.Match(source);

        var sum = 0L;

        while (!string.IsNullOrWhiteSpace(dataMatch.Value))
        {
            var numbers = dataMatch.Value.Replace("mul(", string.Empty)
                .Replace(")", string.Empty)
                .Split(Delimiter.Comma, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            sum += int.Parse(numbers[0]) * int.Parse(numbers[1]);
            dataMatch = dataMatch.NextMatch();
        }

        return sum;
    }

    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    private static partial Regex SourceRegex();
}
