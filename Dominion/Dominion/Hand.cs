using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Hand
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
            if (deck == null)
            {
                return false;
            }
            else
            {
                yourHand.Add(deck.draw());
                return true;
            }
        }
        public List<Card> getHand()
        {
            return yourHand;
        }

        // Goes through your hand and calculates how much base currency you have in it.
        public int getCurrency()
        {
            if (yourHand == null)
            {
                return 0;
            }

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
            if (!yourHand.Contains(picked))
            {
                return false;
            }

            yourHand.Remove(picked);
            deck.discard(picked);
            return true;
        }

        public Boolean contains(Card toCheck)
        {
            if (this.yourHand == null)
            {
                return false;
            }
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
    }
}
