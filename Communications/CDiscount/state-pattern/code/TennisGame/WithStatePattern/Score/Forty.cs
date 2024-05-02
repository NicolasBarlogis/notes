using TennisGame.Player;

namespace TennisGame.WithStatePattern.Score;

public record Forty(IPlayer _player, Point _otherPlayerScore) : IState
{
    private readonly IPlayer _player = _player;
    private readonly Point _otherPlayerScore = _otherPlayerScore;

    public string Score()
    {
        return _player is Server
            ? $"{Point.Forty.ToString()}-{_otherPlayerScore.ToString()}"
            : $"{_otherPlayerScore.ToString()}-{Point.Forty.ToString()}";
    }

    public IState WonPoint(IPlayer winner)
    {
        if (winner.Equals(_player)) return new Win(_player);

        if (_otherPlayerScore == Point.Thirty) return new Deuce();

        return new Forty(_player, _otherPlayerScore + 1);
    }
}
