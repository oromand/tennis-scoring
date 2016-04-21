using System;

namespace Sport.Tennis
{
    public class TieBreak
    {
        public event EventHandler<TieBreakEvent> TieBreakWonHandler;

        public ScoreTieBreak ScoreTieBreak { get; private set; }

        public Set Set { get; set; }

        public TieBreak(Set set)
        {
            this.Set = set;

            this.ScoreTieBreak = new ScoreTieBreak(this);
        }

        public virtual void OnTieBreakWon(Team team)
        {
            EventHandler<TieBreakEvent> handler = TieBreakWonHandler;
            if (handler != null)
            {
                TieBreakEvent gameEvent = new TieBreakEvent()
                {
                    Team = team
                };
                handler(this, gameEvent);
            }
        }
    }
}