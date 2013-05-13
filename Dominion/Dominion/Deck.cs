using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [Serializable]
    public class Deck
    {
        List<Card> inDeck;
        List<Card> inDiscard;

        //Constructor which creates a new deck, initialized for the beginning of a dominion game.
        /// <summary>
        /// Creates a new deck object, set up for the beginning of a dominion game with seven coppers and three estates.
        /// </summary>
        public Deck()
        {
            this.inDeck = new List<Card>();
            for (int i = 0; i < 7; i++)
            {
                //7 copper cards,
                this.inDeck.Add(CardMother.Copper());
            }
            for (int i = 0; i < 3; i++)
            {
                //3 estate victory cards
                this.inDeck.Add(CardMother.Estate());
            }
            this.inDiscard = new List<Card>();
        }

        //Constructor most likely used for testing, or possibly for loading saved games.
        /// <summary>
        /// Builds a deck from a specific list of cards.
        /// </summary>
        /// <param name="cards"> list of cards to build deck from.</param>
        public Deck(List<Card> cards)
        {
            this.inDeck = cards;
        }

        /// <summary>
        /// Returns the number of cards left in the deck, not counting those in the discard. 
        /// This can be used to show the player how many cards are left in their deck at any time.
        /// </summary>
        /// <returns>The number of cards in the deck</returns>
        public int cardsLeft()
        {
            return this.inDeck.Count;
        }

        public int size() {
            return this.inDeck.Count+this.inDiscard.Count;
        }

        /// <summary>
        /// Method which draws a card from the deck. If the deck has no cards in it, it will automatically reshuffle the cards in the discard pile back into the deck.
        /// </summary>
        /// <returns>A card taken from the top of the deck. Null if there are no cards in either the discard pile or in the deck.</returns>
        public Card draw()
        {
            Card c = this.peekAtTopCard();
            this.inDeck.Remove(c);
            return c;
        }

        /// <summary>
        /// Adds the specified card to the discard pile.
        /// </summary>
        /// <param name="c"> Card to be added to discard pile.</param>
        public void discard(Card c)
        {
            this.inDiscard.Add(c);
        }

        /// <summary>
        /// Function which shuffles a generic list using a Fisher-Yates shuffle
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="list">The list to be shuffled in place </param>
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

        /// <summary>
        /// Gets the cards currently drawable in deck.
        /// </summary>
        /// <returns>The List of Cards in the deck</returns>
        public List<Card> getInDeck()
        {
            return this.inDeck;
        }

        /// <summary>
        /// Gets the cards in the discard pile.
        /// </summary>
        /// <returns>A list of Cards from the discard pile.</returns>
        public List<Card> getInDiscard()
        {
            return this.inDiscard;
        }

        /// <summary>
        /// Adds the discard pile back into the deck, and reshuffles it using the shuffle function.
        /// </summary>
        public void reshuffle()
        {
            this.inDeck.AddRange(this.inDiscard);
            this.inDiscard.Clear();
            Shuffle<Card>(this.inDeck);
        }

        public Boolean isEmpty()
        {
            return (this.inDeck.Count == 0 && this.inDiscard.Count == 0);
        }

        public void addCardToFront(Card toFront)
        {
            List<Card> cardCopy = new List<Card>();
            foreach (Card c in this.inDeck)
            {
                cardCopy.Add(c);
            }

            this.inDeck = new List<Card>();
            this.inDeck.Add(toFront);
            this.inDeck.AddRange(cardCopy);
        }

        public Card peekAtTopCard()
        {
            if (inDeck.Count <= 0)
            {
                this.reshuffle();
            }
            if (inDeck.Count > 0)
            {
                Card drawn = inDeck[0];
                return drawn;
            }
            return null;
        }
    }
}
