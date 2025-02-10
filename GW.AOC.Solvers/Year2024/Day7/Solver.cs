using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;
using GW.AOC.Contracts.Solvers;
using GW.AOC.Services.Extensions;

namespace GW.AOC.Solvers.Year2024.Day7;

public class Solver(IPuzzleDataReader puzzleDataReader) : SolverBase, ISolver
{
    public Task SolvePartOneAsync(CancellationToken cancellationToken)
    {
        var result = CalculateTotalCalibrationResult(2);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    public Task SolvePartTwoAsync(CancellationToken cancellationToken)
    {
        var result = CalculateTotalCalibrationResult(3);

        Console.WriteLine(result);

        return Task.CompletedTask;
    }

    private long CalculateTotalCalibrationResult(int combinationsCount)
    {
        var data = puzzleDataReader.ReadAllLineParts(PuzzleDataFilePath, Delimiter.Semicolon);

        var sequences = data.Select(dataItem => (long.Parse(dataItem[0]), dataItem[1].ToIntArray(Delimiter.Space))).ToList();

        var result = 0L;

        foreach (var sequence in sequences)
        {
            var count = Math.Pow(combinationsCount, sequence.Item2.Length - 1);
            var combination = Enumerable.Repeat(0, sequence.Item2.Length - 1).ToArray();

            for (var i = 0; i < count; i++)
            {
                var tempResult = GetResult(sequence.Item2, combination);
                if (tempResult == sequence.Item1)
                {
                    result += sequence.Item1;
                    break;
                }

                combination = GenerateNextCombination(combination, combinationsCount - 1);
            }
        }

        return result;
    }

    private static long GetResult(int[] numbers, int[] operatorsCombination)
    {
        long result = numbers.First();

        for (var i = 1; i < numbers.Length; i++)
        {
            switch (operatorsCombination[i - 1])
            {
                case 0:
                    result += numbers[i];
                    break;
                case 1:
                    result *= numbers[i];
                    break;
                default:
                    result = long.Parse($"{result}{numbers[i]}");
                    break;
            }
        }

        return result;
    }

    private static int[] GenerateNextCombination(int[] currentCombination, int max)
    {
        for (var i = 0; i < currentCombination.Length; i++)
        {
            if (currentCombination[i] < max)
            {
                currentCombination[i] += 1;
                break;
            }

            currentCombination[i] = 0;
        }

        return currentCombination;
    }
}
