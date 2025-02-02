using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day1;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadIntList(PuzzleDataFilePath);

        var left = data.Select(x => x.Item1).Order().ToList();
        var right = data.Select(x => x.Item2).Order().ToList();

        var sum = 0L;

        for (var i = 0; i < left.Count; i++)
        {
            sum += long.Abs(left[i] - right[i]);
        }

        Console.WriteLine(sum);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadIntList(PuzzleDataFilePath);

        var left = data.Select(x => x.Item1).Order().ToList();
        var right = data.Select(x => x.Item2).Order().ToList();

        var dictionary = left.ToLookup(x => x).ToDictionary(x => x.Key, x => new Item(x.Count(), 0));

        foreach (var item in right)
        {
            if (dictionary.TryGetValue(item, out var value))
            {
                value.Count++;
            }
        }

        var sum = dictionary.Sum(x => x.Key * x.Value.ParentCount * x.Value.Count);

        Console.WriteLine(sum);

        return Task.CompletedTask;
    }
}

file record Item(int ParentCount, int Count)
{
    public int Count { get; set; } = Count;
};
