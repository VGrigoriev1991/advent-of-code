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

    public static bool IsValidPoint(this List<List<char>> source, Point point) =>
        point is { X: >= 0, Y: >= 0 } && point.X < source.Count && point.Y < source[0].Count;

    public static List<Point> GetAllItemCoordinates(this List<List<char>> source, char symbol)
    {
        var result = new List<Point>();

        for (var i = 0; i < source.Count; i++)
        {
            for (var j = 0; j < source[0].Count; j++)
            {
                if (source[i][j] == symbol)
                {
                    result.Add(new Point(i, j));
                }
            }
        }

        return result;
    }

    public static Point GetItemCoordinates(this List<List<char>> source, char symbol)
    {
        var result = new Point();

        for (var i = 0; i < source.Count; i++)
        {
            for (var j = 0; j < source[0].Count; j++)
            {
                if (source[i][j] == symbol)
                {
                    result.X = i;
                    result.Y = j;
                }
            }
        }

        return result;
    }
}
