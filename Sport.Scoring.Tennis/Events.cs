using System;

namespace Sport.Tennis
{
    public class GameEvent : ScoreEvent { }

    public abstract class ScoreEvent : EventArgs
    {
        #region Properties

        public Team Team { get; set; }

        #endregion Properties
    }

    public class SetEvent : ScoreEvent { }

    public class TeamScoredEvent : ScoreEvent { }

    public class TieBreakEvent : ScoreEvent { }
}