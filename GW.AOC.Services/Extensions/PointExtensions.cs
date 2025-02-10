using System.Drawing;
using GW.AOC.Contracts.Models;

namespace GW.AOC.Services.Extensions;

public static class PointExtensions
{
    public static Point GetNext(this Point source, Point direction) => new(source.X + direction.X, source.Y + direction.Y);

    public static Point GetNextMainDirection(this Point direction)
    {
        if (direction == PointDirection.Top)
        {
            return PointDirection.Right;
        }

        if (direction == PointDirection.Right)
        {
            return PointDirection.Bottom;
        }

        return direction == PointDirection.Bottom ? PointDirection.Left : PointDirection.Top;
    }
}
