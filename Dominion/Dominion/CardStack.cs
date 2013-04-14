using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    /// <summary>
    /// Class which is designed for piles of cards to buy.
    /// </summary>
    public class CardStack
    {
       Card card;
        int remaining;

        /// <summary>
        /// Initialize a new stack with a specified card and number of cards in the deck.
        /// </summary>
        /// <param name="remaining"></param>
        /// <param name="toInitialize"></param>
        public CardStack(int remaining,Card toInitialize)
        {
            this.remaining = remaining;
            this.card = toInitialize;
        }

        /// <summary>
        /// Constructor for the default size of a card stack with a specified card.
        /// The default size of stack is 10 for every card in game except for the victory and currency.
        /// </summary>
        /// <param name="toIniitialize"></param>
        public CardStack(Card toIniitialize)
        {
            this.remaining = 10;
            this.card = toIniitialize;
        }

        public Card buyOne()
        {
            if (this.remaining > 0)
            {
                this.remaining--;
                return this.card;
            }
            //no cards left in deck;
            return null;
        }

        public Boolean isEmpty()
        {
            return (this.remaining < 1);
        }

        public int cardsRemaining()
        {
            return this.remaining;
        }

        public Card getCard()
        {
            return this.card;
        }
    }
}
