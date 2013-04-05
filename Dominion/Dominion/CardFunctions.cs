using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class CardFunctions
    {
        public static void draw(Player p, int draws)
        {
            for (int i = 0; i < draws; i++)
            {
                p.getHand().draw(p.getDeck());
            }
        }
    }
}
