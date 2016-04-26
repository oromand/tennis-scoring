﻿using Xunit;
using Sport.Tennis;

namespace Score.Scoring.Tennis.Tests
{
    public class SetTest
    {
        [Fact]
        public void TestMethod1()
        {
            Match m = new Match(WinningSet.BEST_OF_FIVE);
            Set instanceUndeTest = new Set(m);

            Assert.NotNull(instanceUndeTest);
            Assert.NotNull(instanceUndeTest.ScoreSet);
            Assert.NotNull(instanceUndeTest.Games);

        }
    }
}
