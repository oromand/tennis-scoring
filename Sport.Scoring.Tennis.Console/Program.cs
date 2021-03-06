﻿using System.Collections.Generic;

namespace Sport.Tennis.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Team teamA = new TeamA("A");
            teamA.Players = new List<Player>()
                {
                    new Player()
                    {
                        Team = teamA,
                        Name = "playerA"
                    }
                };
            Team teamB = new TeamB("B");
            teamA.Players = new List<Player>()
                {
                    new Player()
                    {
                        Team = teamB,
                        Name = "playerB"
                    }
                };

            Match m = new Match(WinningSet.BEST_OF_FIVE)
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
                    System.Console.Write("score in game is " + m.CurrentSet.CurrentGame.ScoreGame.GameScore);
                    if(m.CurrentSet.TieBreak != null)
                    {
                        System.Console.WriteLine("[" + m.CurrentSet.TieBreak.ScoreTieBreak.TieScore +"]");
                    }
                    else
                    {
                        System.Console.WriteLine("");
                    }
                    System.Console.WriteLine("score games in set is " + m.CurrentSet.ScoreSet.SetScore);
                    System.Console.WriteLine("Sets are " + m.ScoreMatch.MatchScore);
                }
            }
        }
    }
}
