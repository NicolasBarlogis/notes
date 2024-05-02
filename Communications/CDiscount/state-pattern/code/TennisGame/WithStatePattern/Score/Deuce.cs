namespace TennisGame.WithStatePattern.Score
{
    public class Deuce : IState
    {
        public string Score()
        {
            return "Deuce";
        }

        public IState WonPoint(IPlayer winner)
        {
            return new Advantage(winner);
        }
    }
}
