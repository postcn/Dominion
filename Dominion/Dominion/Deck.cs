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
            if (inDeck.Count <= 0)
            {
                this.reshuffle();
            }
            if (inDeck.Count > 0)
            {
                Card drawn = inDeck[0];
                inDeck.Remove(drawn);
                return drawn;
            }
            return null;
        }

        public void discard(Card c)
        {
            this.inDiscard.Add(c);
        }

        public static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /**
         * This will get a list of the cards currently in the deck
         */
        public List<Card> getInDeck()
        {
            return this.inDeck;
        }

        public List<Card> getInDiscard()
        {
            return this.inDiscard;
        }

        /**
         * Adds the discard back into the deck and shuffles it.
         */ 
        public void reshuffle()
        {
            this.inDeck.AddRange(this.inDiscard);
            this.inDiscard.Clear();
            Shuffle<Card>(this.inDeck);
        }
    }
}
