using System.Collections.Generic;

namespace Ixcys.Tennis.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Team teamA = new Team();
            teamA.Players = new List<Player>()
                {
                    new Player()
                    {
                        Team = teamA,
                        Name = "playerA"
                    }
                };
            Team teamB = new Team();
            teamA.Players = new List<Player>()
                {
                    new Player()
                    {
                        Team = teamB,
                        Name = "playerB"
                    }
                };

            Match m = new Match(3)
            {
                MatchName = "Tournament ABC",
                TeamA = teamA,
                TeamB = teamB
            };

            System.Console.WriteLine("match has started, enter team name (A or B):");
            while (true)
            {
                string team = System.Console.ReadLine();
                if (team != "a" && team != "b" && team != "A" && team != "B")
                {
                    System.Console.WriteLine("Equipe invalide, doit-être A ou B");
                }
                else
                {
                    m.TeamScores(team.ToUpper());
                    System.Console.WriteLine("score in game is " + m.CurrentSet.CurrentGame.ScoreGame.GameScore);
                }
            }
        }
    }
}
