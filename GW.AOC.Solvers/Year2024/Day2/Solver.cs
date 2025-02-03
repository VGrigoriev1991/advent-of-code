using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day2;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadIntLists(PuzzleDataFilePath);

        var sum = data.LongCount(IsValid);

        Console.WriteLine(sum);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadIntLists(PuzzleDataFilePath);

        var sum = 0L;

        foreach (var numbers in data)
        {
            if (IsValid(numbers))
            {
                sum++;
            }
            else
            {
                for (var i = 0; i < numbers.Count; i++)
                {
                    var subSequence = new List<int>();
                    subSequence.AddRange(numbers.Take(i));
                    subSequence.AddRange(numbers.Skip(i + 1));

                    if (!IsValid(subSequence))
                    {
                        continue;
                    }

                    sum++;
                    break;
                }
            }
        }

        Console.WriteLine(sum);

        return Task.CompletedTask;
    }

    private static bool IsValid(List<int> numbers)
    {
        var result = false;
        bool? direction = null;

        for (var i = 0; i < numbers.Count - 1; i++)
        {
            var difference = numbers[i + 1] - numbers[i];
            direction ??= difference < 0 ? true : difference > 0 ? false : null;

            if (direction is null)
            {
                break;
            }

            var isValid = (direction is true && difference is >= -3 and <= -1) || (direction is false && difference is >= 1 and <= 3);

            if (isValid)
            {
                if (i == numbers.Count - 2)
                {
                    result = true;
                }

                continue;
            }

            break;
        }

        return result;
    }
}
