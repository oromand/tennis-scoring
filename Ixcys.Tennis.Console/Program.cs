using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
