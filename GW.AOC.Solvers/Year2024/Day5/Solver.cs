using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day5;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var before = new Dictionary<int, HashSet<int>>();
        var after = new Dictionary<int, HashSet<int>>();

        var sequences = new List<int[]>();

        foreach (var line in data)
        {
            if (line.Contains(Delimiter.VerticalBar))
            {
                var numbers = line.Split(Delimiter.VerticalBar, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                if (!before.TryGetValue(numbers[1], out var valueBefore))
                {
                    valueBefore = [];
                    before.Add(numbers[1], valueBefore);
                }

                if (!after.TryGetValue(numbers[0], out var valueAfter))
                {
                    valueAfter = [];
                    after.Add(numbers[0], valueAfter);
                }

                valueBefore.Add(numbers[0]);
                valueAfter.Add(numbers[1]);

                continue;
            }

            if (line.Contains(Delimiter.Comma))
            {
                var sequence = line.Split(Delimiter.Comma, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                sequences.Add(sequence);
            }
        }

        var result = GetMiddleValidSequenceNumbersSum(sequences, before, after, out _);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static long GetMiddleValidSequenceNumbersSum(
        List<int[]> sequences,
        Dictionary<int, HashSet<int>> beforeNumbersSequence,
        Dictionary<int, HashSet<int>> afterNumbersSequence,
        out List<List<int>> incorrectNumbers)
    {
        incorrectNumbers = [];

        var result = 0L;

        foreach (var sequence in sequences)
        {
            for (var i = 0; i < sequence.Length; i++)
            {
                if (!beforeNumbersSequence.ContainsKey(sequence[i]) && !afterNumbersSequence.ContainsKey(sequence[i]))
                {
                    incorrectNumbers.Add(sequence.ToList());
                    break;
                }

                var sBefore = sequence.Take(i);
                var sAfter = sequence.Skip(i + 1);

                if (i != 0
                    && (!beforeNumbersSequence.ContainsKey(sequence[i])
                        || sBefore.Any(x => !beforeNumbersSequence[sequence[i]].Contains(x))))
                {
                    incorrectNumbers.Add(sequence.ToList());
                    break;
                }

                if (i != sequence.Length - 1
                    && (!afterNumbersSequence.ContainsKey(sequence[i]) || sAfter.Any(x => !afterNumbersSequence[sequence[i]].Contains(x))))
                {
                    incorrectNumbers.Add(sequence.ToList());
                    break;
                }

                if (i == sequence.Length - 1)
                {
                    result += sequence[sequence.Length / 2];
                }
            }
        }

        return result;
    }
}
