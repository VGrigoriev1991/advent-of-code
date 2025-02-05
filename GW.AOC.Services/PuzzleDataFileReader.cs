using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Services;

namespace GW.AOC.Services;

public class PuzzleDataFileReader : IPuzzleDataReader
{
    public string ReadAllText(string inputFilePath)
    {
        var sr = new StreamReader(inputFilePath);

        return sr.ReadToEnd();
    }

    public List<List<int>> ReadIntLists(string inputFilePath)
    {
        var allParts = ReadAllLineParts(inputFilePath, Delimiter.Space);

        return allParts
            .Select(x => x.Select(int.Parse).ToList())
            .ToList();
    }

    public List<List<char>> ReadCharMatrix(string inputFilePath)
    {
        var lines = ReadAllLines(inputFilePath);
        return lines.Select(x => x.ToCharArray().ToList()).ToList();
    }

    private static List<List<string>> ReadAllLineParts(string inputFilePath, string delimiter)
    {
        var lines = ReadAllLines(inputFilePath);
        return lines.Select(x => x.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
    }

    private static List<string> ReadAllLines(string inputFilePath)
    {
        var sr = new StreamReader(inputFilePath);

        var result = new List<string>();

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();

            result.Add(line ?? string.Empty);
        }

        return result;
    }
}
