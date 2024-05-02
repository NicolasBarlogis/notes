using System;
using TennisGame.Player;
using TennisGame.WithConditionals;

namespace TennisGame.Tests;

public class GameWithConditionalsBuilder
{
    private readonly Server _server;
    private readonly Receiver _receiver;
    private int _serverScore;
    private int _receiverScore;

    private GameWithConditionalsBuilder(Server server, Receiver receiver)
    {
        _server = server;
        _receiver = receiver;
    }

    public static GameWithConditionalsBuilder AGame(Server between, Receiver @and)
    {
        return new GameWithConditionalsBuilder(between, and);
    }

    public TennisScore Build()
    {
        var game = new TennisScore(_server, _receiver);
        Repeat(_serverScore, () => game.WonPoint(_server));
        Repeat(_receiverScore, () => game.WonPoint(_receiver));
        return game;
    }

    private void Repeat(int times, Action action)
    {
        for (int i = 0; i < times; i++)
            action();
    }

    public GameWithConditionalsBuilder WithServerScoring(int times)
    {
        _serverScore = times;
        return this;
    }

    public GameWithConditionalsBuilder WithReceiverScoring(int times)
    {
        _receiverScore = times;
        return this;
    }
}
