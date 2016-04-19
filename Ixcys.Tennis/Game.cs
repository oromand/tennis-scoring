using System;

namespace Ixcys.Tennis
{
    public class GameEvent : EventArgs
    {
        public String Team { get; set; }
    }

    public class Game
    {

        public event EventHandler<GameEvent> GameWon;

        public Player Server { get; set; }

        public ScoreGame ScoreGame { get; private set; }

        public Game()
        {
            this.ScoreGame = new ScoreGame(this);
        }

        /// <summary>
        /// notifies the match
        /// </summary>
        public virtual void OnGameWon(String team)
        {
            EventHandler<GameEvent> handler = GameWon;
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
