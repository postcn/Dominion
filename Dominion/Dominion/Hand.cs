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
            yourHand = null;
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
        // Goes through your hand and calculates how much base currency you have in it.
        public Boolean getCurrency()
        {
            if (yourHand == null)
            {
                return false;
            }

            for (int i = 0; i < yourHand.Count; i++)
            {
                if (yourHand[i].getCash() != 0)
                    currencyInHand = currencyInHand + yourHand[i].getCash();
            }
            return true;
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
    }
}
