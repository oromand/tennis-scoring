using System;
using System.Collections.Generic;

namespace Sport.Tennis
{
    public class Game : IGame
    {
        public event EventHandler<GameEvent> GameWonHandler;

        public Player Server { get; set; }

        public ScoreGame ScoreGame { get; private set; }

        public List<Point> Points { get; private set; }

        public ISet Set { get; set; }

        public Game(ISet set)
        {
            this.Set = set;
            //this.GameWonHandler += Set.ScoreSet.OnGameWonHandler;
            this.Points = new List<Point>();
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