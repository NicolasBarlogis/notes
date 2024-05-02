namespace TennisGame;

public enum Point
{
    Love, Fifteen, Thirty, Forty
}

public static class PointEnvy
{
    public static Point ToPoint(this int score)
    {
        return score switch
        {
            0 => Point.Love,
            1 => Point.Fifteen,
            2 => Point.Thirty,
            3 => Point.Forty,
            _ => throw new ArgumentOutOfRangeException(nameof(score), score, null)
        };
    }

    public static string ToString(this Point point)
    {
        return point switch
        {
            Point.Love => "Love",
            Point.Fifteen => "Fifteen",
            Point.Thirty => "Thirty",
            Point.Forty => "Forty",
            _ => throw new ArgumentOutOfRangeException(nameof(point), point, null)
        };
    }
}
