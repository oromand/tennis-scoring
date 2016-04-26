using System;
using System.Collections.Generic;

namespace Sport.Tennis
{
    public interface IGame
    {
        List<Point> Points { get; }
        ScoreGame ScoreGame { get; }
        Player Server { get; set; }
        ISet Set { get; set; }

        event EventHandler<GameEvent> GameWonHandler;

        void OnGameWon(Team team);
    }
}