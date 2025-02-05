using System.Drawing;

namespace GW.AOC.Services.Extensions;

public static class CharMatrixExtensions
{
    public static bool ContainsItem(this List<List<char>> source, char symbol, Point point)
    {
        if (point is { X: >= 0, Y: >= 0 } && point.X < source.Count && point.Y < source[0].Count)
        {
            return source[point.X][point.Y] == symbol;
        }

        return false;
    }
}
