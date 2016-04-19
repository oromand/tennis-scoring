﻿using System;
using System.Collections.Generic;

namespace Ixcys.Tennis
{
    public class MatchEvent : EventArgs
    {
        public Team WinningTeam { get; set; }
    }
    public class Match
    {
        public event EventHandler<MatchEvent> MatchWon;

        public Match(int nbWinningSets)
        {
            this.Sets = new List<Set>(nbWinningSets);
            this.CurrentSet = new Set();
            this.CurrentSet.SetWon += CurrentSet_SetWon;

            this.ScoreMatch = new ScoreMatch();

            this.MatchWon += Match_MatchWon;

            this.MatchStarted = false;
            this.MatchFinnished = false;
        }

        #region EVENT HANDLER


        private void Match_MatchWon(object sender, MatchEvent e)
        {
            this.MatchFinnished = true;
            Console.WriteLine("Match won by team " + e.WinningTeam);
        }

        /// <summary>
        /// receveived when current set is considered as won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentSet_SetWon(object sender, EventArgs e)
        {
            Console.WriteLine("Set won ");

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
                EventHandler<MatchEvent> handler = MatchWon;
                MatchEvent me = new MatchEvent();
                me.WinningTeam = TeamA;
                if (handler != null)
                {
                    handler(this, me);
                }
            }
            else
            {
                this.CurrentSet = new Set();
                this.CurrentSet.SetWon += CurrentSet_SetWon;
            }

        }
        #endregion


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

        #endregion

        #region METHODS

        public void TeamScores(string teamScore)
        {
            if (!MatchStarted)
            {
                MatchStarted = true;
                StartTime = new DateTime();
            }

            this.ScoreMatch.AchieveScore(teamScore);

        }

        public void GameWon(object sender, EventArgs e)
        {

        }
    }
    #endregion
}
