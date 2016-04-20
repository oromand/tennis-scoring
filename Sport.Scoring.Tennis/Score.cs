namespace Ixcys.Tennis
{
    public abstract class AbstractScore
    {
        protected int scoreA;
        protected int scoreB;
        public abstract void UpdateScore(ref int playerToAddScore, ref int opponentScore);

        protected void reinitScores()
        {
            this.scoreA = 0;
            this.scoreB = 0;
        }
    }

    public abstract class ScoreMatch : AbstractScore
    {
        protected const string MatchA = "matchA";
        protected const string MatchB = "matchB";

        public Match Match { get; set; }

        public ScoreMatch(Match match)
        {
            this.Match = match;
        }

        public abstract void OnSetWonHandler(object sender, SetEvent e);

        public abstract string MatchScore { get; }


    }

    public class BestOfFiveScoreMatch : ScoreMatch
    {


        private static readonly string[][] MatchScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  "2-0", MatchA},
                new[] {"0-1",   "1-1",  "2-1", MatchA},
                new[] {"0-2",   "1-2",  "2-2", MatchA},
                new[] { MatchB, MatchB, MatchB,  ""},
            };

        public BestOfFiveScoreMatch(Match match) : base(match)
        {
        }

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

        public override string MatchScore
        {
            get { return MatchScores[scoreB][scoreA]; }
        }

        public override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (MatchScore != MatchA && MatchScore != MatchB)
            {
                playerToAddScore++;
            }
        }


    }

    public class BestOfThreeScoreMatch : ScoreMatch
    {

        public BestOfThreeScoreMatch(Match match) : base(match)
        {
        }
        public override string MatchScore
        {
            get { return MatchScores[scoreB][scoreA]; }
        }

        public override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (MatchScore != MatchA && MatchScore != MatchB)
            {
                playerToAddScore++;
            }
        }

        private static readonly string[][] MatchScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  MatchA},
                new[] {"0-1",   "1-1",  MatchA},
                new[] { MatchB, MatchB,  "2-2",  MatchA},
                new[] {"",      "",     MatchB,  ""},
            };

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

    }


    public class ScoreSet : AbstractScore
    {

        private const string SetA = "setA";
        private const string SetB = "setB";
        private const string TieBreak = "tieBreak";


        public Set Set { get; set; }

        public ScoreSet(Set Set)
        {
            this.Set = Set;
        }

        public string SetScore
        {
            get { return SetScores[scoreB][scoreA]; }
        }

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

        public override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (SetScore != SetA && SetScore != SetB)
            {
                playerToAddScore++;
            }
        }

        /// <summary>
        /// Get notified when a game has been won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnGameWonHandler(object sender, GameEvent e)
        {
            this.Set.CurrentGame.GameWonHandler -= OnGameWonHandler;
            this.Set.Games.Add(Set.CurrentGame);
            this.Set.CurrentGame = new Game(Set);

            this.Set.CurrentGame.GameWonHandler += OnGameWonHandler;
            this.Set.Match.TeamScoredHandler += Set.CurrentGame.ScoreGame.OnTeamScored;

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

            }
        }

    }


    public class ScoreGame : AbstractScore
    {
        private const string GameA = "gameA";
        private const string GameB = "gameB";
        private const string AdvantageA = "advantageA";
        private const string AdvantageB = "advantageB";

        private static readonly string[][] GameScores = new[]
            {
                new[] {"love",  "15:0",     "30:0",     "40:0",     GameA},
                new[] {"0:15",  "15:15",    "30:15",    "40:15",    GameA},
                new[] {"0:30",  "15:30",    "30:30",    "40:30",    GameA},
                new[] {"0:40",  "15:40",    "30:40",    "deuce",    AdvantageA},
                new[] { GameB,   GameB,     GameB,      AdvantageB}
            };

        public Game Game { get; set; }

        public ScoreGame(Game Game)
        {
            this.Game = Game;
        }

        public string GameScore
        {
            get { return GameScores[scoreB][scoreA]; }
        }

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
            }

            //trigger event here
            //team who scored is certainly winning here
            if (GameScore == GameA || GameScore == GameB)
            {
                this.Game.OnGameWon(teamScored);
                this.reinitScores();
            }
        }

        public override void UpdateScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (GameScore == AdvantageA || GameScore == AdvantageB)
            {
                opponentScore--;
            }
            else if (GameScore != GameA && GameScore != GameB)
            {
                playerToAddScore++;
            }

        }


    }
}
