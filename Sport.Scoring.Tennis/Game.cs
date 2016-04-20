using System;

namespace Ixcys.Tennis
{

    public class Game
    {
        public event EventHandler<GameEvent> GameWonHandler;

        public Player Server { get; set; }

        public ScoreGame ScoreGame { get; private set; }

        public Set Set { get; set; }

        public Game(Set set)
        {
            this.Set = set;
            //this.GameWonHandler += Set.ScoreSet.OnGameWonHandler;

            this.ScoreGame = new ScoreGame(this);
        }

        /// <summary>
        /// notifies the match
        /// </summary>
        public virtual void OnGameWon(Team team)
        {
            EventHandler<GameEvent> handler = GameWonHandler;
            if (handler != null)
            {
                GameEvent gameEvent = new GameEvent()
                {
                    Team = team
                };
                handler(this, gameEvent);
            }
        }
    }
}
