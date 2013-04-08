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

        /// <summary>
        /// Tests the getter for the victory points
        /// </summary>
        [Test()]
        public void TestgetVictoryPts()
        {
            Assert.AreEqual(3, p.getVictoryPts());
        }

        /// <summary>
        /// Tests that we can get the possible victory points from all places that cards may be located.
        /// </summary>
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

        /// <summary>
        /// Tests the getter for the currency value
        /// </summary>
        [Test()]
        public void TestgetCurrencyValue()
        {
            Assert.AreEqual(0, p.getCurrencyValue());
        }

        /// <summary>
        /// Tests the getter for the id
        /// </summary>
        [Test()]
        public void testGetID()
        {
            Assert.AreEqual(0, p.getID());
            Assert.AreEqual(1, p1.getID());
            Assert.AreEqual(2, p2.getID());
        }

        /// <summary>
        /// tests buying a card and it being a victory point
        /// </summary>
        [Test()]
        public void testBuyAndVictory()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            p.buy(s);
            p.setVictoryPts();
            Assert.AreEqual(5, p.getVictoryPts());
        }

        /// <summary>
        /// Tets buying failure because there isn't enough currency in the hand.
        /// </summary>
        [Test()]
        public void testBuyFailureBecauseOfCurrency()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 10));
            Assert.IsFalse(p.buy(s));
        }

        /// <summary>
        /// Tests buying failure because player runs out of possible buys.
        /// </summary>
        [Test()]
        public void testFailMultipleBuys()
        {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsTrue(p.buy(s));
            Assert.IsFalse(p.buy(s));
        }

        /// <summary>
        /// Tests buying failure because there are no cards left in the card stack
        /// </summary>
        [Test()]
        public void testFailBuyEmptyStack()
        {
            CardStack s = new CardStack(0, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsFalse(p.buy(s));
            s = new CardStack(1, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Assert.IsTrue(p.buy(s));
            Assert.IsFalse(p.buy(s));
        }

        /// <summary>
        /// Tests the ability to add buys to the player
        /// </summary>
        [Test()]
        public void testAddBuys()
        {
            Assert.AreEqual(3, p.addBuys(2));
            Assert.AreEqual(7, p2.addBuys(6));
            Assert.AreEqual(0, p1.addBuys(-1));
        }

        /// <summary>
        /// Tests the adder and getter for the buys
        /// </summary>
        [Test()]
        public void testAddAndGetBuys()
        {
            Assert.AreEqual(3, p.addBuys(2));
            Assert.AreEqual(3, p.getBuysLeft());
        }

        /// <summary>
        /// Tests the add buys and then making multiple buys.
        /// </summary>
        [Test()]
        public void testAddBuysAndMultipleBuy()
        {
            p.addBuys(1);
            CardStack c = new CardStack(120,CardMother.Copper());
            Assert.IsTrue(p.buy(c));
            Assert.IsTrue(p.buy(c));
            Assert.IsFalse(p.buy(c));
        }

        /// <summary>
        /// Asserts that the cleanup discards the current hand and draws the next five cards
        /// </summary>
        [Test()]
        public void testCleanUp()
        {
            p.cleanUp();
            Assert.AreEqual(5, p.getDeck().getInDiscard().Count);
            Assert.AreEqual(0, p.getDeck().cardsLeft());
            Assert.AreEqual(5, p.getHand().getHand().Count);
        }

        /// <summary>
        /// Checks that play is false because card is not in hand.
        /// </summary>
        [Test()]
        public void testPlayCardNotInHand()
        {
            Assert.IsFalse(p.play(new Card(0,0,0,0,0,0,0,"NULL","NULL",0)));
        }

        /// <summary>
        /// Checks that play is false because we are trying to play a non-action card
        /// </summary>
        [Test()]
        public void testPlayNotAction()
        {
            Assert.IsFalse(p.play(CardMother.Copper()));
        }

        /// <summary>
        /// Checks that playing an action card will place it into the played list for the player
        /// </summary>
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

        /// <summary>
        /// Tests get currency when we have "played" an action card with bonus currency
        /// </summary>
        [Test()]
        public void testGetCurrency()
        {
            p.getPlayed().Add(new Card(2, 1, 0, 0, 0, 0, 0, "One Bonus Currency", "One Bonus Currency", 0));
            Assert.AreEqual(6, p.getCurrency());//the starter hand will always be five copper until shuffled.
        }

        /// <summary>
        /// Tests that a card in the played pile successfully gets shuffled into deck
        /// </summary>
        [Test()]
        public void testPlayACardAndCleanup()
        {
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.cleanUp();
            Assert.AreEqual(6, p.getDeck().getInDiscard().Count);
        }

        /// <summary>
        /// Tests the ability to play function one, drawing only, such as the smithy card.
        /// </summary>
        [Test()]
        public void testPlayFuncOne()
        {
            //test play a smithy
            p.getHand().getHand().Add(CardMother.Smithy());
            p.play(CardMother.Smithy());
            Assert.AreEqual(8, p.getHand().getHand().Count);
        }

        /// <summary>
        /// Tests the ability to get the actions remaining for a player
        /// </summary>
        [Test()]
        public void testGetActions()
        {
            Assert.AreEqual(1, p.getActionsLeft());
        }

        /// <summary>
        /// Tests that we can add and get actions properly
        /// </summary>
        [Test()]
        public void testAddAndGetActions()
        {
            Assert.AreEqual(2,p.addActions(1));
            Assert.AreEqual(2, p.getActionsLeft());
        }

        /// <summary>
        /// Tests that playing a card reduces the number of actions left
        /// </summary>
        [Test()]
        public void testPlayReducesActions()
        {
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            Assert.AreEqual(0, p.getActionsLeft());
            p.getHand().getHand().Add(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0));
            Assert.IsFalse(p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0)));
        }

        /// <summary>
        /// Tests that function two works, adding actions and cards
        /// </summary>
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

        /// <summary>
        /// Tests that function three works, drawing a card, adding an action, and a buy
        /// </summary>
        [Test()]
        public void testFunctionThree()
        {
            p.getHand().getHand().Add(CardMother.Market());
            Assert.IsTrue(p.play(CardMother.Market()));
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(2, p.getBuysLeft());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(6, p.getHand().getHand().Count);
            p.getHand().getHand().Add(new Card(2, 0, 1, 2, 0, 0, 3, "Null Card for Test", "Null Card for Test", 0));
            Assert.IsTrue(p.play(new Card(2, 0, 1, 2, 0, 0, 3, "Null Card for Test", "Null Card for Test", 0)));
            Assert.AreEqual(3, p.getBuysLeft());
            Assert.AreEqual(2, p.getActionsLeft());
        }
    }
}
