// <copyright file="MatchTest.cs">Copyright ©  2016</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Sport.Tennis;

namespace Sport.Tennis.Tests
{
    /// <summary>This class contains parameterized unit tests for Match</summary>
    [PexClass(typeof(Match))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    public partial class MatchTest
    {
        /// <summary>Test stub for OnMatchWon(Team)</summary>
        [PexMethod]
        internal void OnMatchWonTest([PexAssumeUnderTest]Match target, Team teamSetWon)
        {
            target.OnMatchWon(teamSetWon);
            // TODO: add assertions to method MatchTest.OnMatchWonTest(Match, Team)
        }
    }
}
