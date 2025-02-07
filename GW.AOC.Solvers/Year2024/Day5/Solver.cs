using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day5;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var (before, after) = GetOrderedSequences(data);
        var sequences = GetSequences(data);

        var result = GetMiddleValidSequenceNumbersSum(sequences, before, after, out _);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllLines(PuzzleDataFilePath);

        var (before, after) = GetOrderedSequences(data);
        var sequences = GetSequences(data);

        GetMiddleValidSequenceNumbersSum(sequences, before, after, out var incorrectNumbers);

        var result = 0L;

        foreach (var seq in incorrectNumbers)
        {
            var newSequence = new List<int>();
            while (seq.Count > 0)
            {
                for (var i = 0; i < seq.Count; i++)
                {
                    var sBefore = newSequence.ToArray();
                    var sAfter = seq.Skip(i + 1).Union(seq.Take(i)).ToArray();

                    if (sBefore.Length != 0 && (!before.ContainsKey(seq[i]) || sBefore.Any(x => !before[seq[i]].Contains(x))))
                    {
                        continue;
                    }

                    if (i != seq.Count - 1 && (!after.ContainsKey(seq[i]) || sAfter.Any(x => !after[seq[i]].Contains(x))))
                    {
                        continue;
                    }

                    newSequence.Add(seq[i]);
                    seq.Remove(seq[i]);
                    break;
                }
            }

            result += newSequence[newSequence.Count / 2];
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private static long GetMiddleValidSequenceNumbersSum(
        List<List<int>> sequences,
        Dictionary<int, HashSet<int>> beforeNumbersSequence,
        Dictionary<int, HashSet<int>> afterNumbersSequence,
        out List<List<int>> incorrectNumbers)
    {
        incorrectNumbers = [];

        var result = 0L;

        foreach (var sequence in sequences)
        {
            for (var i = 0; i < sequence.Count; i++)
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

                if (i != sequence.Count - 1
                    && (!afterNumbersSequence.ContainsKey(sequence[i]) || sAfter.Any(x => !afterNumbersSequence[sequence[i]].Contains(x))))
                {
                    incorrectNumbers.Add(sequence.ToList());
                    break;
                }

                if (i == sequence.Count - 1)
                {
                    result += sequence[sequence.Count / 2];
                }
            }
        }

        return result;
    }

    private static (Dictionary<int, HashSet<int>>, Dictionary<int, HashSet<int>>) GetOrderedSequences(IEnumerable<string> lines)
    {
        var before = new Dictionary<int, HashSet<int>>();
        var after = new Dictionary<int, HashSet<int>>();

        foreach (var pair in lines.Where(x => x.Contains(Delimiter.VerticalBar)).ToIntLists(Delimiter.VerticalBar))
        {
            if (!before.TryGetValue(pair[1], out var valueBefore))
            {
                valueBefore = [];
                before.Add(pair[1], valueBefore);
            }

            if (!after.TryGetValue(pair[0], out var valueAfter))
            {
                valueAfter = [];
                after.Add(pair[0], valueAfter);
            }

            valueBefore.Add(pair[0]);
            valueAfter.Add(pair[1]);
        }

        return (before, after);
    }

    private static List<List<int>> GetSequences(IEnumerable<string> lines) =>
        lines.Where(x => x.Contains(Delimiter.Comma)).ToIntLists(Delimiter.Comma);
}
