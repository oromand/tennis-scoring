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
        /// <summary>Test stub for .ctor(WinningSet)</summary>
        [PexMethod]
        public Match ConstructorTest(WinningSet nbWinningSets)
        {
            Match target = new Match(nbWinningSets);
            return target;
            // TODO: add assertions to method MatchTest.ConstructorTest(WinningSet)
        }
    }
}
