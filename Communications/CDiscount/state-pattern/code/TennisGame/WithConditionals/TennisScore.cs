using TennisGame.Player;

namespace TennisGame.WithConditionals;

public class TennisScore
{
    private readonly Server _server;
    private int _serverScore;
    private readonly Receiver _receiver;
    private int _receiverScore;

    public TennisScore(Server server, Receiver receiver)
    {
        _server = server;
        _receiver = receiver;
    }

    public void WonPoint(IPlayer player)
    {
        if (_server.Equals(player))
            _serverScore++;
        else if (_receiver.Equals(player))
            _receiverScore++;

        throw new InvalidOperationException();
    }

    public string Score()
    {
        if (_serverScore >= (int)Point.Forty && _receiverScore >= (int)Point.Forty)
        {
            if (_serverScore.Equals(_receiverScore))
                return "Deuce";
            if (PointDifference() == 1)
                return $"Advantage {LeadingPlayer()}";
            return $"Game for {LeadingPlayer()}";
        }

        if (PointDifference() >= 2 && MaxScore() == 4)
            return $"Game for {LeadingPlayer()}";

        if (_serverScore.Equals(_receiverScore))
            return $"{_serverScore.ToPoint()}-All";

        return $"{_serverScore.ToPoint()}-{_receiverScore.ToPoint()}";
    }

    private int MaxScore() => Math.Max(_serverScore, _receiverScore);

    private int PointDifference() => Math.Abs(_serverScore - _receiverScore);

    private string LeadingPlayer() => _serverScore > _receiverScore ? _server.Name : _receiver.Name;
}
