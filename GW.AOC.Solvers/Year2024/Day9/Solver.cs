using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;

namespace GW.AOC.Solvers.Year2024.Day9;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllText(PuzzleDataFilePath);

        var blocks = Convert(data);

        var spaceIndex = blocks.IndexOf(Delimiter.Dot);

        var result = 0L;

        for (var i = blocks.Count - 1; i > 0; i--)
        {
            if (spaceIndex >= i)
            {
                break;
            }

            if (blocks[i] == Delimiter.Dot)
            {
                continue;
            }

            var temp = blocks[i];
            blocks[i] = Delimiter.Dot;
            blocks[spaceIndex] = temp;

            spaceIndex = blocks.IndexOf(Delimiter.Dot);
        }

        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] == Delimiter.Dot)
            {
                break;
            }

            result += i * int.Parse(blocks[i]);
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var data = puzzleDataReader.ReadAllText(PuzzleDataFilePath);

        var blocks = Convert(data);

        var spaceIndex = blocks.IndexOf(Delimiter.Dot);

        var result = 0L;

        int? currentItem = null;
        var index = blocks.Count - 1;

        while (index > spaceIndex)
        {
            if (blocks[index] == ".")
            {
                index--;
                continue;
            }

            var firstItem = blocks[index];
            var firstItemNumber = int.Parse(firstItem);
            if (firstItemNumber >= currentItem)
            {
                index--;
                continue;
            }

            currentItem = firstItemNumber;

            var sequenceLength = 1;
            index--;
            while (blocks[index] == firstItem)
            {
                index--;
                sequenceLength++;
            }

            while (spaceIndex < index)
            {
                if (blocks.Skip(spaceIndex).Take(sequenceLength).All(x => x == Delimiter.Dot))
                {
                    for (var i = spaceIndex; i < spaceIndex + sequenceLength; i++)
                    {
                        blocks[i] = firstItem;
                    }

                    for (var i = index + 1; i < index + 1 + sequenceLength; i++)
                    {
                        blocks[i] = Delimiter.Dot;
                    }

                    break;
                }

                spaceIndex = blocks.IndexOf(Delimiter.Dot, spaceIndex + 1);
                if (spaceIndex <= 0)
                {
                    break;
                }
            }

            spaceIndex = blocks.IndexOf(Delimiter.Dot);
        }

        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] == ".")
            {
                continue;
            }

            result += i * int.Parse(blocks[i]);
        }

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private static List<string> Convert(string source)
    {
        var result = new List<string>();

        var index = 0;
        var isFile = true;

        foreach (var digit in source.Select(item => byte.Parse(item.ToString())))
        {
            result.AddRange(Enumerable.Range(0, digit).Select(_ => isFile ? index.ToString() : Delimiter.Dot));

            if (isFile)
            {
                index++;
            }

            isFile = !isFile;
        }

        return result;
    }
}
