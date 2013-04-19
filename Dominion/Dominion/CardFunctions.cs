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

        public static void gainCardRemodel(Player p, StatusObject o)
        {
            p.setGain(true);
            p.setCurrencyForGainBonus(2);
            o.setTrashForGain(true);
        }

        public static void gainCardFeast(Player p, StatusObject o)
        {
            p.setGain(true);
            //You gain a card worth 5 which is 1 more than cost of feast.
            p.setCurrencyForGain(5);
            p.getPlayed().Remove(CardMother.Feast());
            o.setTrashedCorrectly(true);
        }
        public static void gainCardWorkshop(Player p, StatusObject o)
        {
            p.setGain(true);
            //you gain a card worth 4
            p.setCurrencyForGain(4);
            o.setTrashedCorrectly(true);
        }
    }
}
