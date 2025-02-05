using System.Drawing;

namespace GW.AOC.Contracts.Models;

public static class PointConstants
{
    public static readonly List<Point> AllDirections =
    [
        new(-1, -1),
        new(-1, 0),
        new(-1, 1),
        new(0, -1),
        new(0, 1),
        new(1, -1),
        new(1, 0),
        new(1, 1)
    ];
}
