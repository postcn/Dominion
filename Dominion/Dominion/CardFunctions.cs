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

        public static void actionAdd(Player p, int add)
        {
            p.addActions(add);
        }

        public static void buyAdd(Player p, int buys)
        {
            p.addBuys(buys);
        }

        public static void gainCurses(Player p)
        {
            Game g = p.getGame();
            foreach (Player play in g.getPlayers())
            {
                if (!p.Equals(play))
                {
                    play.getDeck().discard(g.getBuyables()[6].buyOne());//will always be the curse for the game setup.
                }
            }
        }
    }
}
