using System;
using System.Collections.Generic;

namespace Ixcys.Tennis
{

    public enum WinningSet : int
    {
        BEST_OF_THREE = 2,
        BEST_OF_FIVE = 3
    };

    public class Set
    {
        //public event EventHandler<MatchEvent> MatchWonHandler;
        public event EventHandler<SetEvent> SetWonHandler;
        //public event EventHandler<GameEvent> GameWonHandler;

        public List<Game> Games { get; set; }

        public Game CurrentGame { get; set; }

        public Match Match { get; set; }

        public Team WinningTeam { get; set; }

        public ScoreSet ScoreSet { get; private set; }
        public Set(Match match)
        {
            this.CurrentGame = new Game(this);
            //CurrentGame.GameWonHandler += OnGameWon;
            this.ScoreSet = new ScoreSet(this);

            this.Match = match;
            this.Games = new List<Game>();
            //this.SetWonHandler += this.Match.ScoreMatch.OnSetWonHandler;
        }

        public void OnSetWon(Team setWonTeam)
        {
            //if set won
            EventHandler<SetEvent> handler = SetWonHandler;
            if (handler != null)
            {
                SetEvent setEvent = new SetEvent()
                {
                    Team = setWonTeam,
                };
                handler(this, setEvent);
            }
        }

    }
}
