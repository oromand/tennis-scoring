using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcys.Tennis
{
    public abstract class ScoreEvent: EventArgs
    {
        public Team Team { get; set; }
    }

    public class TeamScoredEvent: ScoreEvent { }
    public class SetEvent : ScoreEvent { }
    public class GameEvent : ScoreEvent { }

}
