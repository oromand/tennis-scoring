using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Sport.Tennis;
using Xunit;

namespace Sport.Tennis.Tests
{
    public class ScoreGameTests
    {
        ScoreGame scoreGame;
        Mock<IGame> game;

        public void beforeTest()
        {
            this.game = new Mock<IGame>();
            game.Setup(g => g.Points).Returns(new System.Collections.Generic.List<Point>());

            this.scoreGame = new ScoreGame(game.Object);
        }

        [Fact]
        public void ScoreGameConstructor()
        {
            beforeTest();
            Assert.NotNull(scoreGame);
            Assert.NotNull(scoreGame.Game.Points);
            Assert.True(scoreGame.ScoreA == 0);
            Assert.True(scoreGame.ScoreB == 0);

            Assert.True(scoreGame.GameScore == "love");    
        }

        [Theory]
        [InlineData("", "love")]
        [InlineData("A", "15:0")]
        [InlineData("B", "0:15")]
        [InlineData("AB", "15:15")]
        [InlineData("AAA", "40:0")]
        [InlineData("AAABBB", "deuce")]
        [InlineData("AAABBBA", ScoreGame.AdvantageA)]
        [InlineData("AAABBBAB", "deuce")]
        [InlineData("AAABBBB", ScoreGame.AdvantageB)]
        public void UpdateScore(string hits, string expectedScoreGame)
        {
            beforeTest();
            foreach (var currentScore in hits)
            {
                Team hittingTeam = null;
                switch (currentScore)
                {
                    case 'A':
                        hittingTeam = new TeamA("A");
                        break;
                    case 'B':
                        hittingTeam = new TeamB("B");
                        break;
                }
                this.scoreGame.OnTeamScored(null, new TeamScoredEvent() { Team = hittingTeam });
            }

            Assert.Equal(expectedScoreGame, this.scoreGame.GameScore);
        }

        [Theory]
        [InlineData("AAABBBAA", ScoreGame.GameB)]
        public void UpdateScoreWhenGameWonTeamA(string hits, string expectedScoreGame)
        {
            beforeTest();
            TeamA teamA = new TeamA("A");
            TeamB teamB = new TeamB("B");
            Team hittingTeam = null;
            this.game.Setup(g => g.OnGameWon(teamA)).Verifiable("OnGameWon wasn't called");
            foreach (var currentScore in hits)
            {
                switch (currentScore)
                {
                    case 'A':
                        hittingTeam = teamA;
                        break;
                    case 'B':
                        hittingTeam = teamB;
                        break;
                }
                this.scoreGame.OnTeamScored(null, new TeamScoredEvent() { Team = hittingTeam });
            }

            game.Verify();
            //Assert.Equal(expectedScoreGame, this.scoreGame.GameScore);
        }

        [Theory]
        [InlineData("AAABBBBB", ScoreGame.GameB)]
        public void UpdateScoreWhenGameWonTeamB(string hits, string expectedScoreGame)
        {
            beforeTest();
            TeamA teamA = new TeamA("A");
            TeamB teamB = new TeamB("B");
            Team hittingTeam = null;
            this.game.Setup(g => g.OnGameWon(teamB)).Verifiable("OnGameWon wasn't called");
            foreach (var currentScore in hits)
            {
                switch (currentScore)
                {
                    case 'A':
                        hittingTeam = teamA;
                        break;
                    case 'B':
                        hittingTeam = teamB;
                        break;
                }
                this.scoreGame.OnTeamScored(null, new TeamScoredEvent() { Team = hittingTeam });
            }

            game.Verify();
            //Assert.Equal(expectedScoreGame, this.scoreGame.GameScore);
        }

    }
}