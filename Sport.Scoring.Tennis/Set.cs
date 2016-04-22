using System;
using System.Collections.Generic;

namespace Sport.Tennis
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
        public event EventHandler<TieBreakEvent> TieBreakWonHandler;
        public event EventHandler<TeamScoredEvent> TeamScoredHandler;

        //public event EventHandler<GameEvent> GameWonHandler;

        public List<Game> Games { get; set; }

        public Game CurrentGame { get; set; }

        public Match Match { get; set; }

        public Team WinningTeam { get; set; }

        public ScoreSet ScoreSet { get; private set; }

        //can have a tie break if player go to 6-6
        public TieBreak TieBreak { get; set; }

        public Set(Match match)
        {
            this.CurrentGame = new Game(this);
            //CurrentGame.GameWonHandler += OnGameWon;
            this.ScoreSet = new ScoreSet(this);

            this.Match = match;
            this.Games = new List<Game>();
            //this.SetWonHandler += this.Match.ScoreMatch.OnSetWonHandler;
        }

        //protected void OnTeamScoredGame(object sender, TeamScoredEvent args)
        //{
        //    this.CurrentGame.ScoreGame.OnTeamScored(sender, args);
        //}

        //protected void OnTeamScoredTieBreak(object sender, TeamScoredEvent args)
        //{
        //    this.TieBreak.ScoreTieBreak.OnTeamScored(sender, args);
        //}

        public void OnTeamScored(object sender, TeamScoredEvent args)
        {

            EventHandler<TeamScoredEvent> handler = TeamScoredHandler;
            if (handler != null)
            {
                TeamScoredEvent setEvent = new TeamScoredEvent()
                {
                    Team = args.Team,
                };
                //would be game or tie break depending of game advancement
                handler(this, setEvent);
            }
            //if (this.TieBreak == null)
            //{
            //    this.OnTeamScoredGame(sender, args);
            //}
            //else
            //{
            //    this.OnTeamScoredTieBreak(sender, args);
            //}
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

        public void OnTieBreak()
        {
            this.TieBreak = new TieBreak(this);
            this.TieBreak.TieBreakWonHandler += this.ScoreSet.OnTieBreakWonHandler;
            this.TeamScoredHandler -= CurrentGame.ScoreGame.OnTeamScored;
            this.TeamScoredHandler += TieBreak.ScoreTieBreak.OnTeamScored;
        }
    }
}