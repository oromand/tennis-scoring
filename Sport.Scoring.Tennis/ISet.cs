﻿using System;
using System.Collections.Generic;

namespace Sport.Tennis
{
    public interface ISet
    {
        IGame CurrentGame { get; set; }
        List<IGame> Games { get; set; }
        Match Match { get; set; }
        ScoreSet ScoreSet { get; }
        TieBreak TieBreak { get; set; }
        Team WinningTeam { get; set; }

        event EventHandler<SetEvent> SetWonHandler;
        event EventHandler<TeamScoredEvent> TeamScoredHandler;
        event EventHandler<TieBreakEvent> TieBreakWonHandler;

        void OnSetWon(Team setWonTeam);
        void OnTeamScored(object sender, TeamScoredEvent args);
        void OnTieBreak();
    }
}