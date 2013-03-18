using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Deck
    {
        List<Card> inDeck;
        List<Card> inDiscard;

        //Constructor which creates a new deck, initialized for the beginning of a dominion game.
        public Deck()
        {
            this.inDeck = new List<Card>();
            for (int i = 0; i < 7; i++)
            {
                //7 copper cards,
                this.inDeck.Add(new Card(1, 1, 0, 0, 0, 0, 0));
            }
            for (int i = 0; i < 3; i++)
            {
                //3 estate victory cards
                this.inDeck.Add(new Card(0, 0, 0, 0, 1, 0, 0));
            }
            this.inDiscard = new List<Card>();
        }

        //Constructor most likely used for testing
        public Deck(List<Card> cards)
        {
            this.inDeck = cards;
        }

        public int cardsLeft()
        {
            return this.inDeck.Count;
        }

        public Card draw()
        {
            if (inDeck.Count > 0)
            {
                Card drawn = inDeck[0];
                inDeck.Remove(drawn);
                return drawn;
            }
            return null;
        }
    }
}
