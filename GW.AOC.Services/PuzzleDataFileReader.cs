using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;

namespace GW.AOC.Services;

public class PuzzleDataFileReader : IPuzzleDataReader
{
    public List<List<int>> ReadIntLists(string inputFilePath)
    {
        var allParts = ReadAllLineParts(inputFilePath, Delimiter.Space);

        return allParts
            .Select(x => x.Select(int.Parse).ToList())
            .ToList();
    }

    private static List<List<string>> ReadAllLineParts(string inputFilePath, string delimiter)
    {
        var sr = new StreamReader(inputFilePath);

        var result = new List<List<string>>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();

            var parts = line!.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

            result.Add(parts);
        }

        return result;
    }
}
