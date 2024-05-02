using TennisGame.Player;
using TennisGame.WithStatePattern.Score;

namespace TennisGame.WithStatePattern
{
    public class TennisScore
    {
        private readonly Server _server;
        private readonly Receiver _receiver;

        private IState _state;

        public TennisScore(Server server, Receiver receiver)
        {
            _server = server;
            _receiver = receiver;
            _state = new Points();

        }

        public string Score()
        {
            return _state.Score();
        }

        public void WonPoint(IPlayer player)
        {
            if (!(player == _server || player == _receiver))
                throw new InvalidOperationException();

            _state = _state.WonPoint(player);
        }
    }
}
