using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcys.Tennis
{
    public class Game
    {

        public event EventHandler GameWon;

        public Player Server { get; set; }

        public void AchievePoint()
        {
            
        }

        /// <summary>
        /// notifies the match
        /// </summary>
        protected virtual void OnGameWon(EventArgs e)
        {
            EventHandler handler = GameWon;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
