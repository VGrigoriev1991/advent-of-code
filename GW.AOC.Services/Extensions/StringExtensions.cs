namespace GW.AOC.Services.Extensions;

public static class StringExtensions
{
    public static string[] ToParts(this string source, string delimiter) =>
        source.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToArray();

    public static int[] ToIntArray(this string source, string delimiter) =>
        source.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
}
