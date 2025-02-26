using System.Drawing;

namespace GW.AOC.Services.Extensions;

public static class MatrixExtensions
{
    public static bool ContainsItem<T>(this List<List<T>> source, T value, Point point)
    {
        if (point is { X: >= 0, Y: >= 0 } && point.X < source.Count && point.Y < source[0].Count)
        {
            return source[point.X][point.Y]!.Equals(value);
        }

        return false;
    }

    public static bool IsValidPoint<T>(this List<List<T>> source, Point point) =>
        point is { X: >= 0, Y: >= 0 } && point.X < source.Count && point.Y < source[0].Count;

    public static List<Point> GetAllItemCoordinates<T>(this List<List<T>> source, T value)
    {
        var result = new List<Point>();

        for (var i = 0; i < source.Count; i++)
        {
            for (var j = 0; j < source[0].Count; j++)
            {
                if (source[i][j]!.Equals(value))
                {
                    result.Add(new Point(i, j));
                }
            }
        }

        return result;
    }

    public static Point GetItemCoordinates<T>(this List<List<T>> source, T value)
    {
        var result = new Point();

        for (var i = 0; i < source.Count; i++)
        {
            for (var j = 0; j < source[0].Count; j++)
            {
                if (!source[i][j]!.Equals(value))
                {
                    continue;
                }

                result.X = i;
                result.Y = j;
            }
        }

        return result;
    }

    public static Point GetItemCoordinates<T>(this List<List<T>> source, T value, T newValue)
    {
        var result = source.GetItemCoordinates(value);

        source[result.X][result.Y] = newValue;

        return result;
    }

    public static Point? GetOppositeItemCoordinates<T>(this List<List<T>> source, T value)
    {
        for (var i = 0; i < source.Count; i++)
        {
            for (var j = 0; j < source[0].Count; j++)
            {
                if (source[i][j]!.Equals(value))
                {
                    continue;
                }

                return new Point(i, j);
            }
        }

        return default;
    }

    public static void ReplaceItems<T>(this List<List<T>> source, T sourceValue, T destinationValue)
    {
        foreach (var rowIndex in source)
        {
            for (var columnIndex = 0; columnIndex < source[0].Count; columnIndex++)
            {
                if (rowIndex[columnIndex]!.Equals(sourceValue))
                {
                    rowIndex[columnIndex] = destinationValue;
                }
            }
        }
    }
}
