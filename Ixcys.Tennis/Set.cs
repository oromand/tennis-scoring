using System;
using System.Collections.Generic;

namespace Ixcys.Tennis
{
    public class SetEvent : EventArgs
    {
        public String Team { get; set; }
    }

    public class Set
    {
        public event EventHandler<SetEvent> SetWon;

        public List<Game> Games { get; set; }

        public Game CurrentGame { get; set; }

        public Team WinningTeam { get; set; }

        public ScoreSet ScoreSet { get; private set; }
        public Set()
        {
            this.CurrentGame = new Game();
            CurrentGame.GameWon += CurrentGame_GameWon;

            this.ScoreSet = new ScoreSet(this);
        }

        private void CurrentGame_GameWon(object sender, GameEvent e)
        {
            this.ScoreSet.AchieveScore(e.Team);
        }

        public virtual void OnSetWon(String team)
        {
            EventHandler<SetEvent> handler = SetWon;
            if (handler != null)
            {
                SetEvent setEvent = new SetEvent()
                {
                    Team = team,
                };
                handler(this, setEvent);
            }
        }
    }
}
