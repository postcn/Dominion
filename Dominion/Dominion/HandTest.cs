using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [TestFixture()]
    class HandTest
    {
        [Test()]
        public void testInintializes()
        {
            Hand test = new Hand();
            Assert.NotNull(test);
        }

        /// <summary>
        /// Tests the ability to draw a card from a deck into a hand
        /// </summary>
        [Test()]
        public void testDraw()
        {
            Hand test = new Hand();
            Deck d = new Deck();
            Assert.AreEqual(0, test.getHand().Count);
            test.draw(d);
            Assert.AreEqual(1, test.getHand().Count);
            while (d.cardsLeft() > 0)
            {
                test.draw(d);
            }
            Assert.AreEqual(10, test.getHand().Count);
            Assert.IsFalse(test.draw(d));
            Assert.AreEqual(10, test.getHand().Count);
        }

        /// <summary>
        /// Tests that the contains function works properly to find whether a card is in the hand
        /// </summary>
        [Test()]
        public void testContains()
        {
            Hand test = new Hand();
            Card estate = new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2);
            Card copper = new Card(1, 1, 0, 0, 0, 0, 0, "Copper", "1 Currency", 0);
            Card other = new Card(0, 0, 0, 0, 0, 0, 0, "NULL", "NULL", 0);
            List<Card> smallDeck = new List<Card>();
            smallDeck.Add(estate);
            smallDeck.Add(copper);
            Deck toDraw = new Deck(smallDeck);
            Assert.IsFalse(test.contains(estate));
            test.draw(toDraw);
            Assert.IsTrue(test.contains(estate));
            Assert.IsFalse(test.contains(copper));
            test.draw(toDraw);
            Assert.IsTrue(test.contains(copper));
            Assert.IsFalse(test.contains(other));
        }

        /// <summary>
        /// Tests whether the get currency function works properly for drawing cards and changing values.
        /// </summary>
        [Test()]
        public void testGetCurrency()
        {
            Hand test = new Hand();
            Deck d = new Deck();//default with seven coppers in front.
            Assert.AreEqual(0, test.getCurrency());
            test.draw(d);
            Assert.AreEqual(1, test.getCurrency());
            for (int i = 0; i < 6; i++)
            {
                test.draw(d);
            }
            Assert.AreEqual(7, test.getCurrency());
            test.draw(d);
            Assert.AreEqual(7, test.getCurrency());
        }

        /// <summary>
        /// Tests whether remove works properly if we try to remove a card when there are no cards in the hand
        /// </summary>
        [Test()]
        public void testRemoveCardNotInHand()
        {
            Hand test = new Hand();
            Deck d = new Deck();
            test.draw(d);
            Card copper = CardMother.Copper();
            Assert.AreEqual(copper, test.remove(copper));
            Assert.IsNull(test.remove(copper));
            test.draw(d);
            Card estate = CardMother.Estate();
            Assert.IsNull(test.remove(estate));
        }

        /// <summary>
        /// Tests whether the discard works properly to put it in the discard of the deck;
        /// </summary>
        [Test()]
        public void testDiscard()
        {
            Hand test = new Hand();
            Deck d = new Deck();
            test.draw(d);
            Assert.AreEqual(1, test.getHand().Count);
            test.discard(test.getHand()[0], d);
            Assert.AreEqual(0, test.getHand().Count);
            Assert.AreEqual(1, d.getInDiscard().Count);
            Assert.IsFalse(test.discard(new Card(0, 0, 0, 0, 0, 0, 0, "Null", "Null", 0), d));
            Assert.AreEqual(1, d.getInDiscard().Count);
        }

        [Test()]
        public void testSize()
        {
            Hand test = new Hand();
            Deck d = new Deck();
            Assert.AreEqual(0,test.size());
            test.draw(d);
            Assert.AreEqual(1, test.size());
        }
    }
}
