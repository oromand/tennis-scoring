using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Sport.Tennis;
using Xunit;

namespace Sport.Tennis.Tests
{
    public class ScoreSetTests
    {
        ScoreSet scoreSet;
        Mock<ISet> set;
        Mock<IGame> game;
        TeamA teamA = new TeamA("A");
        TeamB teamB = new TeamB("B");

        public void beforeTest()
        {
            this.set = new Mock<ISet>();

            this.game = new Mock<IGame>();
            //game.Setup(g => g.GameWonHandler).Returns(new System.Collections.Generic.List<Point>());

            set.Setup(s => s.CurrentGame).Returns(game.Object);

            this.scoreSet = new ScoreSet(set.Object);
        }

        [Fact]
        public void ScoreGameConstructor()
        {
            beforeTest();
            Assert.NotNull(scoreSet);
            Assert.Null(scoreSet.Set.TieBreak);
            Assert.True(scoreSet.ScoreA == 0);
            Assert.True(scoreSet.ScoreB == 0);

            Assert.True(scoreSet.SetScore == "0-0");    
        }

        [Theory]
        [InlineData("", "0-0")]
        [InlineData("A", "1-0")]
        [InlineData("B", "0-1")]
        [InlineData("AB", "1-1")]
        [InlineData("AAA", "3-0")]
        [InlineData("AAABBB", "3-3")]
        [InlineData("AAABBBAAA", ScoreSet.SetA)]
        [InlineData("ABABBBBAAAAA", ScoreSet.SetA)]
        [InlineData("ABABBBBAAABB", ScoreSet.SetA)]
        [InlineData("ABABABABABAB", ScoreSet.TieBreak)]
        public void UpdateScore(string hits, string expectedScoreGame)
        {
            beforeTest();
            foreach (var currentScore in hits)
            {
                Team hittingTeam = null;
                switch (currentScore)
                {
                    case 'A':
                        hittingTeam = teamA;
                        break;
                    case 'B':
                        hittingTeam = teamB;
                        break;
                }
                this.scoreSet.OnGameWonHandler(null, new GameEvent() { Team = hittingTeam });
            }

            Assert.Equal(expectedScoreGame, this.scoreSet.SetScore);
        }

       

    }
}