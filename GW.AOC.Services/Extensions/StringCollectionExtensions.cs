namespace GW.AOC.Services.Extensions;

public static class StringCollectionExtensions
{
    public static List<List<int>> ToIntLists(this IEnumerable<string> lines, string delimiter) => lines
        .Select(x => x.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
        .ToList();
}
