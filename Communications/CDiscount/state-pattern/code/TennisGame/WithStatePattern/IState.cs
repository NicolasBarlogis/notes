namespace TennisGame.WithStatePattern
{
    public interface IState
    {
        string Score();
        IState WonPoint(IPlayer winner);
    }
}
