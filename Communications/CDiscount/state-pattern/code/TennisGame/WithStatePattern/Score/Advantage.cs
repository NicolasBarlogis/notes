namespace TennisGame.WithStatePattern.Score;

public class Advantage : IState
{
    private readonly IPlayer _player;

    public Advantage(IPlayer player)
    {
        _player = player;
    }

    public string Score()
    {
        return $"Advantage {_player.Name}";
    }

    public IState WonPoint(IPlayer winner)
    {
        if (winner.Equals(_player))
            return new Win(winner);

        return new Deuce();
    }
}
