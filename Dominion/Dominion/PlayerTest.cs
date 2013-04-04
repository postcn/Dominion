using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class PlayerTest
    {
        Player p;
        Player p1;
        Player p2;

        [SetUp()]
        public void setUp()
        {
            this.p = new Player(0);
            p1 = new Player(1);
            p2 = new Player(2);
        }

        [Test()]
        public void TestgetVictoryPts()
        {
            Assert.AreEqual(3, p.getVictoryPts());
        }

        [Test()]
        public void TestsetVictoryPts()
        {
            Deck temp = p.getDeck();
            //add two cards worth one victory point to the deck.
            temp.getInDeck().Add(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2));
            temp.getInDiscard().Add(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2));
            p.setVictoryPts();

            Assert.AreEqual(5, p.getVictoryPts());
        }

        [Test()]
        public void TestgetCurrencyValue()
        {
            Assert.AreEqual(0, p.getCurrencyValue());
        }

        [Test()]
        public void testGetID()
        {
            Assert.AreEqual(0, p.getID());
            Assert.AreEqual(1, p1.getID());
            Assert.AreEqual(2, p2.getID());
        }

        [Test()]
        public void testBuyAndVictory()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            p.buy(s);
            p.setVictoryPts();
            Assert.AreEqual(5, p.getVictoryPts());
        }

        [Test()]
        public void testBuyFailureBecauseOfCurrency()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 10));
            Assert.IsFalse(p.buy(s));
        }

        [Test()]
        public void testFailMultipleBuys()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsTrue(p.buy(s));
            Assert.IsFalse(p.buy(s));
        }

        [Test()]
        public void testFailBuyEmptyStack()
        {
            CardStack s = new CardStack(0, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsFalse(p.buy(s));
            s = new CardStack(1, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsTrue(p.buy(s));
            Assert.IsFalse(p.buy(s));
        }

        [Test()]
        public void testAddBuys()
        {
            Assert.AreEqual(3, p.addBuys(2));
            Assert.AreEqual(7, p2.addBuys(6));
            Assert.AreEqual(0, p1.addBuys(-1));
        }

        [Test()]
        public void testCleanUp()
        {
            p.cleanUp();
            Assert.AreEqual(5, p.getDeck().getInDiscard().Count);
            Assert.AreEqual(0, p.getDeck().cardsLeft());
        }

        [Test()]
        public void testPlayCardNotInHand()
        {
            Assert.IsFalse(p.play(new Card(0,0,0,0,0,0,0,"NULL","NULL",0)));
        }

        [Test()]
        public void testPlayNotAction()
        {
            Assert.IsFalse(p.play(new Card(1, 1, 0, 0, 0, 0, 0, "Copper", "1 Currency", 0)));
        }

        [Test()]
        public void testPlayActionGoesIntoPlayed()
        {
            List<Card> toBuildFrom = new List<Card>();
            for (int i = 0; i < 10; i++)
            {
                toBuildFrom.Add(new Card(2, 0, 0, 0, 0, 0, 0, "Null Action", "Null Action", 0));
            }
            Deck d = new Deck(toBuildFrom);
            p.setDeck(d);
            p.getHand().draw(p.getDeck());
            p.play(new Card(2, 0, 0, 0, 0, 0, 0, "Null Action", "Null Action", 0));
            Assert.AreEqual(1, p.getPlayed().Count);
        }
    }
}
