using TennisGame.Player;

namespace TennisGame.WithStatePattern.Score;

public record Points : IState
{
    private readonly Dictionary<Type, Point> _currentScore = new();

    private Points(Point serverScore, Point receiverScore)
    {
        _currentScore[typeof(Server)] =  serverScore;
        _currentScore[typeof(Receiver)] = receiverScore;
    }

    public Points() : this(Point.Love, Point.Love)
    { }

    public IState WonPoint(IPlayer winner)
    {
        return winner switch
        {
            _ when _currentScore[winner.GetType()] == Point.Thirty => new Forty(winner, _currentScore[Opponent(winner.GetType())]),
            Server _ => new Points(ScoreFor(typeof(Server)) + 1, _currentScore[typeof(Receiver)]),
            _ => new Points(serverScore: _currentScore[typeof(Server)], _currentScore[typeof(Receiver)] + 1)
        };
    }
    public string Score()
    {
        return _currentScore[typeof(Receiver)].Equals(_currentScore[typeof(Server)])
            ? $"{_currentScore[typeof(Server)].ToString()}-All"
            : $"{_currentScore[typeof(Server)].ToString()}-{_currentScore[typeof(Receiver)].ToString()}";
    }

    private Point ScoreFor(Type playerType) => _currentScore[playerType];

    private static Type Opponent(Type type) => type == typeof(Receiver) ? typeof(Server) : typeof(Receiver);
}
