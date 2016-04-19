namespace Ixcys.Tennis
{
    public abstract class AbstractScore
    {
        protected int scoreA;
        protected int scoreB;
        public abstract void UpdateGameScore(ref int playerToAddScore, ref int opponentScore);

        public void AchieveScore(string team)
        {
            switch (team)
            {
                case "A":
                    UpdateGameScore(ref scoreA, ref scoreB);
                    break;
                case "B":
                    UpdateGameScore(ref scoreB, ref scoreA);
                    break;
                default:
                    break;
            }
        }

        protected void reinitScores()
        {
            this.scoreA = 0;
            this.scoreB = 0;
        }
    }

    public class ScoreMatch : AbstractScore
    {

        private const string MatchA = "matchA";
        private const string MatchB = "matchB";


        public string MatchScore
        {
            get { return MatchScores[scoreB][scoreA]; }
        }

        private static readonly string[][] MatchScores = new[]
           {
                //any ending lines should get catched by engine to trigger according events
                new[] {"0-0",   "1-0",  "2-0",  "3-0",  MatchA},
                new[] {"0-1",   "1-1",  "2-1",  "3-1",  MatchA},
                new[] {"0-2",   "1-2",  "2-2",  "3-2",  MatchA},
                new[] {"0-3",   "1-3",  "2-3",  ""},
                new[] {MatchB,  MatchB, MatchB,  },
            };

        public override void UpdateGameScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (MatchScore != MatchA && MatchScore != MatchB)
            {
                playerToAddScore++;
            }


            //trigger event here
            if (MatchScore == MatchA)
            {

            }
            else if (MatchScore == MatchB)
            {

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
                new[] {"0-0",   "1-0",  "2-0",  "3-0",  "4-0",  "5-0",  "6-0",  SetA},
                new[] {"0-1",   "1-1",  "2-1",  "3-1",  "4-1",  "5-1",  "6-1",  SetA},
                new[] {"0-2",   "1-2",  "2-2",  "3-2",  "4-2",  "5-2",  "6-2",  SetA},
                new[] {"0-3",   "1-3",  "2-3",  "3-3",  "4-3",  "5-3",  "6-3",  SetA},
                new[] {"0-4",   "1-4",  "2-4",  "3-4",  "4-4",  "5-4",  "6-4",  SetA},
                new[] {"0-5",   "1-5",  "2-5",  "3-5",  "4-5",  "5-5",  "6-5",  "7-5", SetA},
                new[] {"0-6",   "1-6",  "2-6",  "3-6",  "4-6",  "5-6",  TieBreak,  SetA},
                new[] { SetB,   SetB,   SetB,   SetB,   SetB,   "5-7",  SetB, },
                new[] { "",     "",     "",     "",     "",     SetB,   "" }
            };

        public override void UpdateGameScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (SetScore != SetA && SetScore != SetB)
            {
                playerToAddScore++;
            }


            //trigger event here
            if (SetScore == SetA)
            {
                this.Set.OnSetWon("A");
                this.reinitScores();
            }
            else if (SetScore == SetB)
            {
                this.Set.OnSetWon("B");
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


        public override void UpdateGameScore(ref int playerToAddScore, ref int opponentScore)
        {
            if (GameScore == AdvantageA || GameScore == AdvantageB)
            {
                opponentScore--;
            }
            else if (GameScore != GameA && GameScore != GameB)
            {
                playerToAddScore++;
            }

            //register point to game

            //trigger event here
            if (GameScore == GameA)
            {
                this.Game.OnGameWon("A");
                this.reinitScores();
            }
            else if (GameScore == GameB)
            {
                this.Game.OnGameWon("B");
                this.reinitScores();
            }
        }

    }
}
