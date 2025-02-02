using GW.AOC.Contracts.Models;

namespace GW.AOC.Solvers.Models;

public static class SolverKey
{
    public const string Year = "Year";
    public const string Day = "Day";

    public static string Get(ushort year, ushort day) => $"{Year}_{year}_{Day}_{day}";

    public static string GetFromNamespace(string? source)
    {
        source ??= string.Empty;

        var year = GetYearFromNamespace(source);
        var day = GetDayFromNamespace(source);

        return Get(year, day);
    }

    public static string GetNamespacePart(string source, string partKey, bool removeKey = true)
    {
        var namespaceParts = source.Split(Delimiter.Dot, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var part = namespaceParts.FirstOrDefault(x => x.StartsWith(partKey, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;

        if (removeKey)
        {
            part = part.Replace(partKey, string.Empty);
        }

        return part;
    }

    private static ushort GetYearFromNamespace(string source) => ushort.Parse(GetNamespacePart(source, Year));

    private static ushort GetDayFromNamespace(string source) => ushort.Parse(GetNamespacePart(source, Day));
}
