using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcys.Tennis
{
    public class Set
    {
        public event EventHandler SetWon;

        public List<Game> Games { get; set; }

        public Game CurrentGame { get; set; }

        public Team WinningTeam { get; set; }
        public Set()
        {
            this.CurrentGame = new Game();
            CurrentGame.GameWon += CurrentGame_GameWon;
        }

        private void CurrentGame_GameWon(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnSetWon(EventArgs e)
        {
            EventHandler handler = SetWon;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
