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
        [Test()]
        public void testDeckInitializes() 
        {
            Assert.NotNull(d);
        }

        [Test()]
        public void testGetSize()
        {
            Assert.AreEqual(10, d.cardsLeft());
            Deck de = new Deck(new List<Card>());
            Assert.AreEqual(0, de.cardsLeft());
        }

        [Test()]
        public void testDraw()
        {
            Card drawn = d.draw();
            Assert.NotNull(drawn);
            Assert.AreEqual(9, d.cardsLeft());
        }

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
