namespace GW.AOC.Contracts.Services;

public interface IPuzzleDataReader
{
    string ReadAllText(string inputFilePath);

    List<List<int>> ReadIntLists(string inputFilePath);

    List<List<char>> ReadCharMatrix(string inputFilePath);
}
