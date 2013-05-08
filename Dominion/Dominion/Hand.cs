using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class Hand
    {
        List<Card> yourHand;
        int currencyInHand;

        public Hand()
        {
            currencyInHand = 0;
            yourHand = new List<Card>();
        }

        public Boolean draw(Deck deck)
        {
            if (!deck.isEmpty())
            {
                yourHand.Add(deck.draw());
                return true;
            }
            return false;
        }
        public List<Card> getHand()
        {
            return yourHand;
        }
        public int size() {
            return yourHand.Count();
        }
        // Goes through your hand and calculates how much base currency you have in it.
        public int getCurrency()
        {
            currencyInHand = 0;
            for (int i = 0; i < yourHand.Count; i++)
            {
                if (yourHand[i].getType() == 1)
                    currencyInHand = currencyInHand + yourHand[i].getCash();
            }
            return this.currencyInHand;
        }

        public Boolean discard(Card picked, Deck deck)
        {
            if (!(yourHand.Contains(picked)))
            {
                return false;
            }
            else
            {
                yourHand.Remove(picked);
                deck.discard(picked);
                return true;
            }
        }

        public Boolean contains(Card toCheck)
        {
            foreach (Card c in this.yourHand)
            {
                if (c.Equals(toCheck))
                {
                    return true;
                }
            }
            return false;
        }

        public Card remove(Card aCard)
        {
            if (this.yourHand.Remove(aCard))
            {
                return aCard;
            }
            else
            {
                return null;
            }
        }

        public Card getFirstVictoryCard()
        {
            foreach (Card c in this.yourHand)
            {
                if (c.getType() == 0)
                {
                    return c;
                }
            }
            return null;
        }

        public Boolean hasDefenseCard()
        {
            foreach (Card c in this.yourHand)
            {
                if (c.getType() == 4)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
