using GW.AOC.Contracts.Models;

namespace GW.AOC.Contracts.Services;

public interface IPuzzleDataReader
{
    string ReadAllText(string inputFilePath);

    List<string> ReadAllLines(string inputFilePath);

    List<List<int>> ReadIntMatrix(string inputFilePath, string delimiter = Delimiter.Space);

    List<List<string>> ReadAllLineParts(string inputFilePath, string delimiter);

    List<List<char>> ReadCharMatrix(string inputFilePath);
}
