using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcys.Tennis
{
    public class Score
    {
        private int scoreA;
        private int scoreB;

        private const string GameA = "gameA";
        private const string GameB = "gameB";
        private const string AdvantageA = "advantageA";
        private const string AdvantageB = "advantageB";
        private const string SetA = "setA";
        private const string SetB = "setB";

        private static readonly string[][] GameScore = new[]
            {
                new[] {"love",  "15:0",     "30:0",     "40:0",     GameA},
                new[] {"0:15",  "15:15",    "30:15",    "40:15",    GameA},
                new[] {"0:30",  "15:30",    "30:30",    "40:30",    GameA},
                new[] {"0:40",  "15:40",    "30:40",    "deuce",    AdvantageA},
                new[] { GameB,   GameB,     GameB,      AdvantageB}
            };



        public void AchievesScore(Player player)
        {
            switch (player)
            {
                case Player.PlayerA:
                    UpdateGameScore(ref scoreA, ref scoreB);
                    break;
                case Player.PlayerB:
                    UpdateGameScore(ref scoreB, ref scoreA);
                    break;
            }
        }
        private void UpdateGameScore(ref int playerToAddScore, ref int opponentScore)
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

        private static readonly string[][] SetScores = new[]
           {
                new[] {"0-0",  "1-0",     "2-0",     "3-0",     "4-0", "5-0", SetA},
                new[] {"0-1",  "1-1",    "2-1",    "3-1",     "4-1", "5-1", "6-1", SetA},
                new[] {"0-2",  "1-2",    "2-2",    "3-2",     "4-2", "5-2", "6-0"},
                new[] {"0-3",  "1-3",    "2-3",    "3-3",     "4-3", "5-3", "6-0"},
                new[] {"0-4",  "1-4",    "2-4",    "3-4",     "4-4", "5-4", "6-0"},
                new[] {"0-5",  "1-5",    "2-5",    "3-5",     "4-5", "5-5", "6-0"},
                new[] {"0-6",  "1-6",    "2-6",    "3-6",     "4-6", "5-6", "6-0"},
                new[] { GameB,   GameB,     GameB,      AdvantageB}
            };

    }
}
