using System.Drawing;

namespace GW.AOC.Contracts.Models;

public static class PointDirection
{
    public static readonly Point TopLeft = new(-1, -1);
    public static readonly Point Top = new(-1, 0);
    public static readonly Point TopRight = new(-1, 1);
    public static readonly Point Right = new(0, 1);
    public static readonly Point BottomRight = new(1, 1);
    public static readonly Point Bottom = new(1, 0);
    public static readonly Point BottomLeft = new(1, -1);
    public static readonly Point Left = new(0, -1);

    public static readonly List<Point> All =
    [
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    ];

    public static readonly List<Point> Main =
    [
        Top,
        Left,
        Right,
        Bottom
    ];
}
