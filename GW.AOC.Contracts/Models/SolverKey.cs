namespace GW.AOC.Contracts.Models;

public static class SolverKey
{
    public const string Year = "year";
    public const string Day = "day";

    public static string Get(ushort year, ushort day) => $"year_{year}_day_{day}";

    public static string GetFromNamespace(string? source)
    {
        source ??= string.Empty;

        var year = GetYearFromNamespace(source);
        var day = GetDayFromNamespace(source);

        return Get(year, day);
    }

    private static ushort GetYearFromNamespace(string source) => ushort.Parse(GetNamespacePart(source, Year));

    private static ushort GetDayFromNamespace(string source) => ushort.Parse(GetNamespacePart(source, Day));

    private static string GetNamespacePart(string source, string partKey, bool removeKey = true)
    {
        var namespaceParts = source.Split(".", StringSplitOptions.RemoveEmptyEntries).ToArray();
        var part = namespaceParts.FirstOrDefault(x => x.StartsWith(partKey)) ?? string.Empty;

        if (removeKey)
        {
            part = part.Replace(partKey, string.Empty);
        }

        return part;
    }
}
