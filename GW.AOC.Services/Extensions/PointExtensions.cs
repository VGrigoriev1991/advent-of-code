using System.Drawing;

namespace GW.AOC.Services.Extensions;

public static class PointExtensions
{
    public static Point GetNext(this Point source, Point direction) => new(source.X + direction.X, source.Y + direction.Y);
}
