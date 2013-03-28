using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class DeckTest
    {
        Deck d;
        [SetUp()]
        public void setupFunc()
        {
            this.d = new Deck();
        }
        
        //Test that we can properly initialize a deck.
        //If this doesn't pass, then we can't actually do anything else in file.
        [Test()]
        public void testDeckInitializes() 
        {
            Assert.NotNull(d);
        }

        //Tests that we can get the size of a deck properly
        //Straightforward return so I'm not going to test it more than once.
        [Test()]
        public void testGetSize()
        {
            Assert.AreEqual(10, d.cardsLeft());
            Deck de = new Deck(new List<Card>());
            Assert.AreEqual(0, de.cardsLeft());
        }

        //Tests if we can draw a card from the deck;
        //Boundary case for lower boundary, only drawing one card
        [Test()]
        public void testDraw()
        {
            Card drawn = d.draw();
            Assert.NotNull(drawn);
            Assert.AreEqual(9, d.cardsLeft());
        }

        //Tests if we can draw all the cards in a deck.
        //Boundary case for upper boundary.
        [Test()]
        public void testDrawAll()
        {
            for (int i = 0; i < 11; i++)
            {
                d.draw();
            }
            Assert.AreEqual(0, d.cardsLeft());
        }

        //This test works with randomness so it may fail
        //sometimes without actually being a failure.
        //This is just to ensure that we get sufficient differences
        //when we reshuffle a deck.
        [Test()]
        public void testShuffle()
        {
            for (int i = 0; i < 100; i++)
            {
                String nonShuffled = getString(d.getInDeck());
                Deck.Shuffle<Card>(d.getInDeck());
                Assert.AreNotEqual(nonShuffled, getString(d.getInDeck()));
            }
        }

        //Used only with the above test to make sure the shuffle is working
        private String getString(List<Card> c)
        {
            String val = "";
            foreach (Card ca in c)
            {
                val += ca.getType() + ca.getCash();
            }
            return val;
        }

        //Tests the discard of all the cards in the deck
        //This is the upper boundary case.
        [Test()]
        public void testDiscardAll()
        {
            for (int i = 0; i < 10; i++)
            {
                Card c = d.draw();
                d.discard(c);
            }
            Assert.AreEqual(10, d.getInDiscard().Count);
        }

        //Discards a random number of cards, and makes sure that many cards show up in the discard pile.
        //The random number of cards always is less than the cards left in the deck
        //This represents the average case
        [Test()]
        public void testDiscardRandom()
        {
            Random rg = new Random();
            int num = rg.Next(d.cardsLeft());
            for (int i = 0; i < num; i++)
            {
                Card c = d.draw();
                d.discard(c);
            }
            Assert.AreEqual(num, d.getInDiscard().Count);
        }

        //Test which determines whether or not the automatic reshuffle after discard is working.
        //Low case, only does it once.
        [Test()]
        public void testDrawAndDiscardAllAutomaticShuffle()
        {
            int size = d.cardsLeft();
            for (int i = 0; i < size; i++)
            {
                Card c = d.draw();
                d.discard(c);
            }
            //this draw should reshuffle the deck.
            d.draw();
            Assert.AreEqual(size-1, d.cardsLeft());
        }
            
    }
}
