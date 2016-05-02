using System;
using System.Collections.Generic;

namespace Sport.Tennis
{
    public class MatchEvent : EventArgs
    {
        public Team WinningTeam { get; set; }
    }

    public class Match
    {
        //public event EventHandler<MatchEvent> MatchWonHandler;

        public event EventHandler<TeamScoredEvent> TeamScoredHandler;

        public Match(WinningSet nbWinningSets)
        {
            this.NbWinningSets = (int)nbWinningSets;
            this.Sets = new List<Set>((int)nbWinningSets);
            this.CurrentSet = new Set(this);

            switch (nbWinningSets)
            {
                case WinningSet.BEST_OF_THREE:
                    this.ScoreMatch = new BestOfThreeScoreMatch(this);
                    break;

                case WinningSet.BEST_OF_FIVE:
                    this.ScoreMatch = new BestOfFiveScoreMatch(this);
                    break;

                default:
                    break;
            }
            //dispatch event to current handling ScoreGame object
            this.TeamScoredHandler += this.CurrentSet.OnTeamScored;

            this.MatchStarted = false;
            this.MatchFinnished = false;

            //register events
            this.CurrentSet.TeamScoredHandler += CurrentSet.CurrentGame.ScoreGame.OnTeamScored;
            this.CurrentSet.CurrentGame.GameWonHandler += CurrentSet.ScoreSet.OnGameWonHandler;
            this.CurrentSet.SetWonHandler += ScoreMatch.OnSetWonHandler;
        }

        #region EVENT HANDLER

        internal void OnMatchWon(Team teamSetWon)
        {
            this.MatchFinnished = true;
            Console.WriteLine("Match won by team " + teamSetWon.Name);
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// receveived when current set is considered as won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSetWon(Team winningTeam)
        {
            Console.WriteLine("Set won ");

            this.CurrentSet.WinningTeam = winningTeam;
            this.CurrentSet.SetWonHandler -= ScoreMatch.OnSetWonHandler;
            this.Sets.Add(CurrentSet);
            int nbSetTeamA = 0, nbSetTeamB = 0;
            //TODO optimize this with linq
            foreach (Set set in Sets)
            {
                if (set.WinningTeam == TeamA)
                {
                    nbSetTeamA++;
                }
                else if (set.WinningTeam == TeamB)
                {
                    nbSetTeamB++;
                }
            }

            if (nbSetTeamA >= this.NbWinningSets || nbSetTeamB >= this.NbWinningSets)
            {
                this.OnMatchWon(winningTeam);
            }
            else
            {
                this.CurrentSet = new Set(this);
                this.CurrentSet.TeamScoredHandler += CurrentSet.CurrentGame.ScoreGame.OnTeamScored;
                this.TeamScoredHandler += CurrentSet.OnTeamScored;

                this.CurrentSet.SetWonHandler += ScoreMatch.OnSetWonHandler;
                this.CurrentSet.CurrentGame.GameWonHandler += CurrentSet.ScoreSet.OnGameWonHandler;
            }
        }

        #endregion EVENT HANDLER

        #region PROPERTIES

        public string MatchName { get; set; }

        public List<Set> Sets { get; private set; }

        /// <summary>
        /// Should be 3 or 5 depending of settings
        /// </summary>
        public int NbWinningSets { get; private set; }

        public Set CurrentSet { get; set; }

        public Player Server { get; set; }

        public ScoreMatch ScoreMatch { get; private set; }

        public Team TeamA { get; set; }

        public Team TeamB { get; set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Boolean MatchStarted { get; set; }
        public Boolean MatchFinnished { get; set; }

        #endregion PROPERTIES

        #region METHODS

        public void TeamScores(string teamScore)
        {
            if (!MatchStarted)
            {
                MatchStarted = true;
                StartTime = new DateTime();
            }

            EventHandler<TeamScoredEvent> handler = TeamScoredHandler;
            TeamScoredEvent teamScoredEvent = new TeamScoredEvent();

            switch (teamScore)
            {
                case "A":
                    teamScoredEvent.Team = TeamA;
                    break;

                case "B":
                    teamScoredEvent.Team = TeamB;
                    break;

                default: throw new Exception("Unknown team, should either be A or B");
            }
            if (handler != null)
            {
                handler(this, teamScoredEvent);
            }
        }
    }

    #endregion METHODS
}