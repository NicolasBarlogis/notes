using System;
using TennisGame.Player;

namespace TennisGame.Tests;

public class GameWithStatePatternBuilder
{
    private readonly Server _server;
    private readonly Receiver _receiver;
    private int _serverScore;
    private int _receiverScore;

    private GameWithStatePatternBuilder(Server server, Receiver receiver)
    {
        _server = server;
        _receiver = receiver;
    }

    public static GameWithStatePatternBuilder AGame(Server between, Receiver @and)
    {
        return new GameWithStatePatternBuilder(between, and);
    }

    public WithStatePattern.TennisScore Build()
    {
        var game = new WithStatePattern.TennisScore(_server, _receiver);
        Repeat(_serverScore, () => game.WonPoint(_server));
        Repeat(_receiverScore, () => game.WonPoint(_receiver));
        return game;
    }

    private void Repeat(int times, Action action)
    {
        for (int i = 0; i < times; i++)
            action();
    }

    public GameWithStatePatternBuilder WithServerScoring(int times)
    {
        _serverScore = times;
        return this;
    }

    public GameWithStatePatternBuilder WithReceiverScoring(int times)
    {
        _receiverScore = times;
        return this;
    }
}
