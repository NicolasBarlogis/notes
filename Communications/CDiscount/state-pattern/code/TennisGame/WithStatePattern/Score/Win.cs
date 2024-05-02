namespace TennisGame.WithStatePattern.Score;

public record Win(IPlayer _player) : IState
{
    private readonly IPlayer _player = _player;

    public string Score()
    {
        return $"Game for {_player.Name}";
    }

    public IState WonPoint(IPlayer winner)
    {
        return this;
    }
}
