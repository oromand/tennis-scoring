// <copyright file="MatchTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sport.Tennis;

namespace Sport.Tennis.Tests
{
    [TestClass]
    [PexClass(typeof(Match))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MatchTest
    {

        [PexMethod]
        public void OnSetWon([PexAssumeUnderTest]Match target, Team winningTeam)
        {
            target.OnSetWon(winningTeam);
            // TODO: add assertions to method MatchTest.OnSetWon(Match, Team)
        }
    }
}
