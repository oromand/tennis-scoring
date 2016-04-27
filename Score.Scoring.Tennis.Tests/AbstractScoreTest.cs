using System;
using Xunit;
using Sport.Tennis;
using Moq;

namespace Score.Scoring.Tennis.Tests
{
    /// <summary>
    /// Purpose is to test reinitScore
    /// </summary>
    class SubAbstractScoreTest : AbstractScore
    {
        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            throw new NotImplementedException();
        }

        public void incrementScoreA()
        {
            this.scoreA++;
        }

        public void incrementScoreB()
        {
            this.scoreB++;
        }

        public void reinintScoreWrapper()
        {
            this.reinitScores();
        }
    }

    public class AbstractScoreTest
    {

        SubAbstractScoreTest instanceUnderTest;

        public void beforeTest()
        {
            this.instanceUnderTest = new SubAbstractScoreTest();
        }

        [Fact]
        public void ReinitScoreWhenDefaults()
        {
            beforeTest();
            Assert.NotNull(this.instanceUnderTest);
            Assert.True(this.instanceUnderTest.ScoreA == 0);
            Assert.True(this.instanceUnderTest.ScoreB == 0);
        }

        [Fact]
        public void ReinitScoreWhenIncrementScoreA()
        {
            beforeTest();
            instanceUnderTest.incrementScoreA();
            Assert.True(this.instanceUnderTest.ScoreA == 1);
            Assert.True(this.instanceUnderTest.ScoreB == 0);

            instanceUnderTest.reinintScoreWrapper();

            Assert.True(this.instanceUnderTest.ScoreA == 0);
            Assert.True(this.instanceUnderTest.ScoreB == 0);
        }

        [Fact]
        public void ReinitScoreWhenIncrementScoreB()
        {
            beforeTest();
            instanceUnderTest.incrementScoreB();
            Assert.True(this.instanceUnderTest.ScoreA == 0);
            Assert.True(this.instanceUnderTest.ScoreB == 1);

            instanceUnderTest.reinintScoreWrapper();

            Assert.True(this.instanceUnderTest.ScoreA == 0);
            Assert.True(this.instanceUnderTest.ScoreB == 0);
        }

        [Fact]
        public void ReinitScoreWhenIncrementScoreAB()
        {
            beforeTest();
            instanceUnderTest.incrementScoreA();
            instanceUnderTest.incrementScoreB();
            Assert.True(this.instanceUnderTest.ScoreA == 1);
            Assert.True(this.instanceUnderTest.ScoreB == 1);

            instanceUnderTest.reinintScoreWrapper();

            Assert.True(this.instanceUnderTest.ScoreA == 0);
            Assert.True(this.instanceUnderTest.ScoreB == 0);
        }
    }
}
