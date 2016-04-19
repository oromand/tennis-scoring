using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcys.Tennis
{
    public abstract class Team
    {
        public List<Player> Players { get; set; }

        public String Name { get; set; }

        public Team(String name)
        {
            this.Name = name;
        }
    }

    public class TeamA: Team {
        public TeamA(string name): base(name)
        {

        }
    }

    public class TeamB : Team
    {
        public TeamB(string name) : base(name)
        {

        }
    }
}
