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
            p.setGainsLeft(p.getGainsLeft() + 1);
            p.setCurrencyForGainBonus(2);
            o.setTrashForGain(true);
        }

        public static void gainCardFeast(Player p, StatusObject o)
        {
            p.setGain(true);
            p.setGainsLeft(p.getGainsLeft() + 1);
            //You gain a card worth 5 which is 1 more than cost of feast.
            p.setCurrencyForGain(5);
            p.getPlayed().Remove(CardMother.Feast());
            o.setTrashedCorrectly(true);
        }
        public static void gainCardWorkshop(Player p, StatusObject o)
        {
            p.setGain(true);
            p.setGainsLeft(p.getGainsLeft() + 1);
            //you gain a card worth 4
            p.setCurrencyForGain(4);
            o.setTrashedCorrectly(true);
        }

        public static void doubleNextPlay(Player p,int plays)
        {
            p.setPlayMultipleTimes(true);
            p.setTimesToPlayNextCard(plays * 2);
        }

        public static void setupDiscardCardsToDrawSameNumber(Player p, StatusObject o)
        {
            o.setDiscardCardsToDrawSameNumber(true);
        }

        public static void addNeededTrashes(Player p, StatusObject o)
        {
            p.setTrashesNeeded(p.getTrashesNeeded() + 1);
            p.setTrashCurrencyBonus(3);
            o.setTrashACopperForCurrency(true);
        }

        public static void trashUptoFourCards(Player p, StatusObject o)
        {
            o.setTrashCards(true);
            p.setPossibleTrashes(p.getPossibleTrashes() + 4);
        }

        public static void discardDeckChancellor(Player p, StatusObject o)
        {
            o.setDiscardDeck(true);
        }
    }
}
