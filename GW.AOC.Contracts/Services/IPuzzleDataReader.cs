namespace GW.AOC.Contracts.Services;

public interface IPuzzleDataReader
{
    List<List<int>> ReadIntLists(string inputFilePath);
}
