using System;
using FluentAssertions;
using TennisGame.Player;
using TennisGame.WithStatePattern;
using Xunit;
using static TennisGame.Tests.GameWithStatePatternBuilder;

namespace TennisGame.Tests
{
    public class TennisScoreWithStatePatternShould
    {
        private readonly Server _federer = new Server("Federer");
        private readonly Receiver _nadal = new Receiver("Nadal");

        [Fact]
        public void Not_accept_player_not_in_game()
        {
            var game = new TennisScore(_federer, _nadal);

            var point = () => game.WonPoint(new Receiver("Djokovic"));

            point.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Be_love_all_when_game_begins()
        {
            var game = new TennisScore(_federer, _nadal);

            game.Score().Should().Be("Love-All");
        }

        [Fact]
        public void Be_fifteen_love_when_server_scores_first_point()
        {
            var game = AGame(between: _federer, @and: _nadal).Build();

            game.WonPoint(_federer);

            game.Score().Should().Be("Fifteen-Love");
        }

        [Fact]
        public void Be_love_fifteen_when_receiver_scores_one_point()
        {
            var game = new TennisScore(_federer, _nadal);

            game.WonPoint(_nadal);
            game.Score().Should().Be("Love-Fifteen");
        }

        [Fact]
        public void Be_fifteen_all_when_receiver_and_server_scores_one_point()
        {
            var game = new TennisScore(_federer, _nadal);

            game.WonPoint(_nadal);
            game.WonPoint(_federer);

            game.Score().Should().Be("Fifteen-All");
        }

        [Fact]
        public void Be_with_thirty_love_when_server_scores_two_first_points()
        {
            var game = new TennisScore(_federer, _nadal);

            game.WonPoint(_federer);
            game.WonPoint(_federer);

            game.Score().Should().Be("Thirty-Love");
        }

        [Fact]
        public void Should_announce_score_with_Forty_love()
        {
            var game = new TennisScore(_federer, _nadal);

            game.WonPoint(_federer);
            game.WonPoint(_federer);
            game.WonPoint(_federer);

            game.Score().Should().Be("Forty-Love");
        }

        [Fact(DisplayName = "3-2 + receiver scores => Deuce")]
        public void Should_announce_score_with_deuce_by_receiver()
        {
            var game = AGame(between: _federer, @and: _nadal)
                .WithServerScoring(3).WithReceiverScoring(2).Build();

            game.WonPoint(_nadal);

            game.Score().Should().Be("Deuce");
        }

        [Fact(DisplayName = "2-3 + server scores => Deuce")]
        public void Should_announce_score_with_deuce_by_server()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 2)
                .WithReceiverScoring(times: 3)
                .Build();

            game.WonPoint(_federer);

            game.Score().Should().Be("Deuce");
        }

        [Fact(DisplayName = "Deuce + server scores => Advantage")]
        public void Be_advantage_after_deuce()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 3)
                .WithReceiverScoring(times: 3)
                .Build();

            game.WonPoint(_federer);

            game.Score().Should().Be("Advantage Federer");
        }

        [Fact(DisplayName = "Advantage player + player scores => Win")]
        public void Be_game_when_player_having_advantage_scores()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 3)
                .WithReceiverScoring(times: 3)
                .Build();

            game.WonPoint(_federer);
            game.WonPoint(_federer);

            game.Score().Should().Be("Game for Federer");
        }

        [Fact(DisplayName = "Advantage player + other player scores => Deuce")]
        public void Be_deuce_when_player_having_advantage_loose_point()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 3)
                .WithReceiverScoring(times: 3)
                .Build();

            game.WonPoint(_federer);
            game.WonPoint(_nadal);

            game.Score().Should().Be("Deuce");
        }

        [Fact]
        public void Be_game_for_player_a_player_scoring_4_times_with_2_points_more_than_the_opponent()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 3)
                .Build();

            game.WonPoint(_federer);

            game.Score().Should().Be("Game for Federer");
        }

        [Fact]
        public void Be_game_when_a_player_scores_2_times_after_deuce()
        {
            var game = AGame(_federer, _nadal)
                .WithServerScoring(times: 3)
                .WithReceiverScoring(3)
                .Build();

            game.WonPoint(_nadal);
            game.WonPoint(_nadal);

            game.Score().Should().Be("Game for Nadal");
        }
    }
}
