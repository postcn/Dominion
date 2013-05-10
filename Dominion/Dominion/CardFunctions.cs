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
            p.setOtherPlayerList();
            foreach (Player play in p.getOtherPlayers())
            {
                if (!play.getHand().hasDefenseCard())
                {
                    play.getDeck().discard(g.getBuyables()[6].buyOne());//will always be the curse for the game setup.
                }
                else
                {
                    p.getGame().addToGameMessage(play.getName() + Internationalizer.getMessage("Defended"));
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

        public static void bureaucratAction(Player p)
        {
            p.getDeck().addCardToFront(p.getGame().getBuyables()[1].buyOne());
            p.setOtherPlayerList();
            foreach (Player other in p.getOtherPlayers())
            {
                if (!other.getHand().hasDefenseCard())
                {
                    Card c = other.getHand().getFirstVictoryCard();
                    if (c == null)
                    {
                        p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("RevealNoVict"));
                    }
                    else
                    {
                        other.getDeck().addCardToFront(other.getHand().remove(c));
                        p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("BureaucratMsg1") + c.getName() + Internationalizer.getMessage("BureaucratMsg2"));
                    }
                }
                else
                {
                    p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("Defended"));
                }
            }
        }

        public static void mineATreasure(Player P, StatusObject o)
        {
            o.setMineTreasure(true);
            P.setTrashesNeeded(P.getTrashesNeeded() + 1);
        }

        public static void militiaAction(Player p)
        {
            p.setOtherPlayerList();
            foreach (Player other in p.getOtherPlayers())
            {
                if (!other.getHand().hasDefenseCard())
                {
                    other.addDelayedFunction(new DelayedFunction(other, 1));
                }
                else
                {
                    p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("Defended"));
                }
            }
        }

        public static void councilRoomAction(Player p)
        {
            p.setOtherPlayerList();
            foreach (Player other in p.getOtherPlayers())
            {
                other.addDelayedFunction(new DelayedFunction(other, 0));
            }
        }

        public static void spyFunction(Player p, StatusObject o)
        {
            o.setCanSpy(true);
            p.setOtherPlayerList();
        }

        public static void thiefAction(Player p, StatusObject o)
        {
            p.clearThiefList();
            p.setOtherPlayerList();
            o.setSelectTrashFromThief(true);
            foreach (Player other in p.getOtherPlayers())
            {
                List<Card> cards = new List<Card>();
                if (!other.getHand().hasDefenseCard())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Card c = other.getDeck().draw();
                        if (c.getType() == 1)
                        {
                            cards.Add(c);
                        }
                        else
                        {
                            other.getDeck().discard(c);
                        }
                        p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("ThiefMsg1") + c.getName() + Internationalizer.getMessage("ThiefMsg2"));
                    }
                }
                else
                {
                    p.getGame().addToGameMessage(other.getName() + Internationalizer.getMessage("Defended"));
                }
                p.getThiefList().Add(cards);
            }
        }
    }
}
