using System;

namespace Sport.Tennis
{
    public abstract class AbstractScore
    {
        #region Fields

        protected int scoreA;
        protected int scoreB;

        #endregion Fields

        #region Methods

        public int ScoreA
        {
            get
            {
                return scoreA;
            }
            private set { }
        }

        public int ScoreB
        {
            get
            {
                return scoreB;
            }
            private set { }
        }

        protected abstract void UpdateScore(ref int playerToAddScore, ref int opponentScore);

        protected void reinitScores()
        {
            this.scoreA = 0;
            this.scoreB = 0;
        }
        #endregion Methods
    }

    public class BestOfFiveScoreMatch : ScoreMatch
    {
        #region Fields

        private static readonly string[][] MatchScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  "2-0", MatchA},
                new[] {"0-1",   "1-1",  "2-1", MatchA},
                new[] {"0-2",   "1-2",  "2-2", MatchA},
                new[] { MatchB, MatchB, MatchB,  ""},
            };

        #endregion Fields

        #region Constructors

        public BestOfFiveScoreMatch(Match match) : base(match)
        {
        }

        #endregion Constructors

        #region Properties

        public override string MatchScore
        {
            get { return MatchScores[scoreB][scoreA]; }
        }

        #endregion Properties

        #region Methods

        public override void OnSetWonHandler(object sender, SetEvent e)
        {
            this.Match.OnSetWon(e.Team);

            Team teamSetWon = e.Team;
            if (teamSetWon is TeamA)
            {
                UpdateScore(ref scoreA, ref scoreB);
            }
            else if (teamSetWon is TeamB)
            {
                UpdateScore(ref scoreB, ref scoreA);
            }

            //trigger event here
            if (MatchScore == MatchA || MatchScore == MatchB)
            {
                this.Match.OnMatchWon(teamSetWon);
            }
        }

        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (MatchScore != MatchA && MatchScore != MatchB)
            {
                playerToAddScore++;
            }
        }

        #endregion Methods
    }

    public class BestOfThreeScoreMatch : ScoreMatch
    {
        #region Fields

        private static readonly string[][] MatchScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  MatchA},
                new[] {"0-1",   "1-1",  MatchA},
                new[] { MatchB, MatchB,  "2-2",  MatchA},
                new[] {"",      "",     MatchB,  ""},
            };

        #endregion Fields

        #region Constructors

        public BestOfThreeScoreMatch(Match match) : base(match)
        {
        }

        #endregion Constructors

        #region Properties

        public override string MatchScore
        {
            get { return MatchScores[scoreB][scoreA]; }
        }

        #endregion Properties

        #region Methods

        public override void OnSetWonHandler(object sender, SetEvent e)
        {
            this.Match.OnSetWon(e.Team);

            Team teamSetWon = e.Team;
            if (teamSetWon is TeamA)
            {
                UpdateScore(ref scoreA, ref scoreB);
            }
            else if (teamSetWon is TeamB)
            {
                UpdateScore(ref scoreB, ref scoreA);
            }

            //trigger event here
            if (MatchScore == MatchA || MatchScore == MatchB)
            {
                this.Match.OnMatchWon(teamSetWon);
            }
        }

        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (MatchScore != MatchA && MatchScore != MatchB)
            {
                playerToAddScore++;
            }
        }

        #endregion Methods
    }

    public class ScoreGame : AbstractScore
    {
        #region Fields

        public const string AdvantageA = "advantageA";
        public const string AdvantageB = "advantageB";
        public const string GameA = "gameA";
        public const string GameB = "gameB";

        private static readonly string[][] GameScores = new[]
            {
                new[] {"love",  "15:0",     "30:0",     "40:0",     GameA},
                new[] {"0:15",  "15:15",    "30:15",    "40:15",    GameA},
                new[] {"0:30",  "15:30",    "30:30",    "40:30",    GameA},
                new[] {"0:40",  "15:40",    "30:40",    "deuce",    AdvantageA},
                new[] { GameB,   GameB,     GameB,      AdvantageB}
            };

        #endregion Fields

        #region Constructors

        public ScoreGame(IGame Game)
        {
            this.Game = Game;
        }

        #endregion Constructors

        #region Properties

        public IGame Game { get; set; }

        public string GameScore
        {
            get { return GameScores[scoreB][scoreA]; }
        }

        #endregion Properties

        #region Methods

        public void OnTeamScored(object sender, TeamScoredEvent args)
        {
            Team teamScored = args.Team;
            if (teamScored is TeamA)
            {
                UpdateScore(ref scoreA, ref scoreB);
            }
            else if (teamScored is TeamB)
            {
                UpdateScore(ref scoreB, ref scoreA);
            } else { throw new Exception("Unknown team"); }

            //trigger event here
            //team who scored is certainly winning here
            if (GameScore == GameA || GameScore == GameB)
            {
                this.Game.OnGameWon(teamScored);
                this.reinitScores();
            }
        }

        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (GameScore == AdvantageA || GameScore == AdvantageB)
            {
                opponentScore--;
            }
            else if (GameScore != GameA && GameScore != GameB)
            {
                playerToAddScore++;
            }
            this.Game.Points.Add(new Point() { Value = this.GameScore });
        }

        #endregion Methods
    }

    public abstract class ScoreMatch : AbstractScore
    {
        #region Fields

        protected const string MatchA = "matchA";
        protected const string MatchB = "matchB";

        #endregion Fields

        #region Constructors

        public ScoreMatch(Match match)
        {
            this.Match = match;
        }

        #endregion Constructors

        #region Properties

        public Match Match { get; set; }
        public abstract string MatchScore { get; }

        #endregion Properties

        #region Methods

        public abstract void OnSetWonHandler(object sender, SetEvent e);

        #endregion Methods
    }

    public class ScoreSet : AbstractScore
    {
        #region Fields

        private const string SetA = "setA";
        private const string SetB = "setB";
        private const string TieBreak = "tieBreak";

        private static readonly string[][] SetScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  "2-0",  "3-0",  "4-0",  "5-0", SetA},
                new[] {"0-1",   "1-1",  "2-1",  "3-1",  "4-1",  "5-1", SetA},
                new[] {"0-2",   "1-2",  "2-2",  "3-2",  "4-2",  "5-2", SetA},
                new[] {"0-3",   "1-3",  "2-3",  "3-3",  "4-3",  "5-3", SetA},
                new[] {"0-4",   "1-4",  "2-4",  "3-4",  "4-4",  "5-4", SetA},
                new[] {"0-5",   "1-5",  "2-5",  "3-5",  "4-5",  "5-5",  "6-5", SetA},
                new[] { SetB,   SetB,   SetB,   SetB,   SetB,   "5-6",  TieBreak,  SetA},
                new[] { "",     "",     "",     "",     "",     SetB,  SetB }
            };

        #endregion Fields

        #region Constructors

        public ScoreSet(Set Set)
        {
            this.Set = Set;
        }

        #endregion Constructors

        #region Properties

        public Set Set { get; set; }

        public string SetScore
        {
            get { return SetScores[scoreB][scoreA]; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get notified when a game has been won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnGameWonHandler(object sender, GameEvent e)
        {
            this.Set.CurrentGame.GameWonHandler -= OnGameWonHandler;
            this.Set.TeamScoredHandler -= this.Set.CurrentGame.ScoreGame.OnTeamScored;
            _GameWon(e);
        }

        public void OnTieBreakWonHandler(object sender, TieBreakEvent e)
        {
            this.Set.TieBreak.TieBreakWonHandler -= this.OnTieBreakWonHandler;
            this.Set.TeamScoredHandler -= this.Set.TieBreak.ScoreTieBreak.OnTeamScored;
            _GameWon(e);
        }

        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (SetScore != SetA && SetScore != SetB)
            {
                playerToAddScore++;
            }
        }

        private void _GameWon(ScoreEvent e)
        {
            this.Set.Games.Add(Set.CurrentGame);
            this.Set.CurrentGame = new Game(Set);

            this.Set.CurrentGame.GameWonHandler += OnGameWonHandler;
            this.Set.TeamScoredHandler += this.Set.CurrentGame.ScoreGame.OnTeamScored;
            //this.Set.Match.TeamScoredHandler += Set.CurrentGame.ScoreGame.OnTeamScored;

            Team teamGameWon = e.Team;
            if (teamGameWon is TeamA)
            {
                UpdateScore(ref scoreA, ref scoreB);
            }
            else if (teamGameWon is TeamB)
            {
                UpdateScore(ref scoreB, ref scoreA);
            }

            //trigger event here
            //team who scored is certainly winning here
            if (SetScore == SetA || SetScore == SetB)
            {
                this.Set.OnSetWon(teamGameWon);
                this.reinitScores();
            }
            else if (SetScore == TieBreak)
            {
                this.Set.OnTieBreak();
            }
        }
        #endregion Methods
    }

    public class ScoreTieBreak : AbstractScore
    {
        #region Fields

        private const string ShouldContinue = "shouldContinue";
        private const string TieA = "tieBreakA";
        private const string TieB = "tieBreakB";
        private static readonly string[][] TieScores = new[]
                   {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  "2-0",  "3-0",  "4-0",  "5-0", "6-0", TieA},
                new[] {"0-1",   "1-1",  "2-1",  "3-1",  "4-1",  "5-1", "6-1", TieA },
                new[] {"0-2",   "1-2",  "2-2",  "3-2",  "4-2",  "5-2", "6-2", TieA },
                new[] {"0-3",   "1-3",  "2-3",  "3-3",  "4-3",  "5-3", "6-3", TieA },
                new[] {"0-4",   "1-4",  "2-4",  "3-4",  "4-4",  "5-4", "6-4", TieA },
                new[] {"0-5",   "1-5",  "2-5",  "3-5",  "4-5",  "5-5", "6-5", TieA},
                new[] {"0-6",   "1-6",  "2-6",  "3-6",  "4-6",  "5-6", ShouldContinue, ""},
                new[] { TieB,   TieB,   TieB,   TieB,   TieB,   TieB,   "" }
            };

        //tells if 6-6 has been reached
        //then first score having a difference of 2 would win
        private bool shouldContinue = false;
        #endregion Fields

        #region Properties

        public TieBreak TieBreak { get; private set; }

        public string TieScore
        {
            get
            {
                if (!shouldContinue)
                {
                    return TieScores[scoreB][scoreA];
                }
                else
                {
                    return scoreA.ToString() + ":" + scoreB.ToString();
                }

            }
        }
        #endregion Properties

        #region Methods

        /// <summary>
        /// Get notified when a game has been won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        public ScoreTieBreak(TieBreak TieBreak)
        {
            this.TieBreak = TieBreak;
        }

        public void OnTeamScored(object sender, TeamScoredEvent e)
        {
            
            Team teamScored = e.Team;
            if (teamScored is TeamA)
            {
                UpdateScore(ref scoreA, ref scoreB);
            }
            else if (teamScored is TeamB)
            {
                UpdateScore(ref scoreB, ref scoreA);
            }

            if (TieScore == TieA || TieScore == TieB || (shouldContinue && Math.Abs(scoreA - scoreB) >= 2))
            {
                this.TieBreak.OnTieBreakWon(teamScored);
            }
        }
        protected override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if(!shouldContinue)
            {
                if (TieScore != TieA && TieScore != TieB)
                {
                    playerToAddScore++;
                }

                if (TieScore == ShouldContinue)
                {
                    shouldContinue = true;
                }
            }
            else // went to 6-6, would notify GameWon upon a difference of 2
            {
                playerToAddScore++;
                
            }

        }

        #endregion Methods
    }
}