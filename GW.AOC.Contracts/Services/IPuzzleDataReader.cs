namespace GW.AOC.Contracts.Services;

public interface IPuzzleDataReader
{
    List<(int, int)> ReadIntList(string inputFilePath);
}
