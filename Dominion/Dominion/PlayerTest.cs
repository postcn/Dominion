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
            //put a new victory card in the hand
            p.getHand().getHand().Add(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2));
            Assert.AreEqual(6, p.getHand().getHand().Count);
            p.setVictoryPts();

            Assert.AreEqual(6, p.getVictoryPts());
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
        public void testAddAndGetBuys()
        {
            Assert.AreEqual(3, p.addBuys(2));
            Assert.AreEqual(3, p.getBuysLeft());
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
            Assert.IsFalse(p.play(CardMother.Copper()));
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

        [Test()]
        public void testGetCurrency()
        {
            p.getPlayed().Add(new Card(2, 1, 0, 0, 0, 0, 0, "One Bonus Currency", "One Bonus Currency", 0));
            Assert.AreEqual(6, p.getCurrency());//the starter hand will always be five copper until shuffled.
        }

        [Test()]
        public void testPlayACardAndCleanup()
        {
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.cleanUp();
            Assert.AreEqual(6, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testPlayFuncOne()
        {
            //test play a smithy
            p.getHand().getHand().Add(CardMother.Smithy());
            p.play(CardMother.Smithy());
            Assert.AreEqual(8, p.getHand().getHand().Count);
        }

        [Test()]
        public void testGetActions()
        {
            Assert.AreEqual(1, p.getActionsLeft());
        }

        [Test()]
        public void testAddAndGetActions()
        {
            Assert.AreEqual(2,p.addActions(1));
            Assert.AreEqual(2, p.getActionsLeft());
        }

        [Test()]
        public void testPlayReducesActions()
        {
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            Assert.AreEqual(0, p.getActionsLeft());
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            Assert.IsFalse(p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0)));
        }

        [Test()]
        public void testFunctionTwo()
        {
            p.getHand().getHand().Add(CardMother.Laboratory());
            p.play(CardMother.Laboratory());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(7, p.getHand().getHand().Count);
            Card testMoreActions = new Card(2, 0, 0, 3, 0, 0, 2, "NULL TEST CARD", "NULL TEST CARD", 0);
            p.getHand().getHand().Add(testMoreActions);
            p.play(testMoreActions);
            Assert.AreEqual(3, p.getActionsLeft());
        }
    }
}
