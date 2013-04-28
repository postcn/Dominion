using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class CardFunctionsTest
    {
        Player p;

        [SetUp()]
        public void setUp()
        {
            this.p = new Player(0);
        }

        /// <summary>
        /// Tests the draw function to see if the player can draw cards using the CardFunction
        /// </summary>
        [Test()]
        public void testDraw()
        {
            CardFunctions.draw(p, 0);
            Assert.AreEqual(5, p.getHand().getHand().Count);
            CardFunctions.draw(p, 1);
            Assert.AreEqual(6, p.getHand().getHand().Count);
            CardFunctions.draw(p, 4);//all the cards in the deck
            Assert.AreEqual(10, p.getHand().getHand().Count);
        }

        /// <summary>
        /// Tests that we can successfully add actions to the player
        /// </summary>
        [Test()]
        public void testAddActions()
        {
            CardFunctions.actionAdd(p, 1);
            Assert.AreEqual(2, p.getActionsLeft());
            CardFunctions.actionAdd(p, -1);
            Assert.AreEqual(1, p.getActionsLeft());
        }

        /// <summary>
        /// Tests that we can successfully add buys to the player
        /// </summary>
        [Test()]
        public void testAddBuys()
        {
            CardFunctions.buyAdd(p, 1);
            Assert.AreEqual(2, p.getBuysLeft());
        }

        /// <summary>
        /// Tests that we can cause all the other players to gain curses.
        /// </summary>
        [Test()]
        public void testGainCurses()
        {
            Game mini = new Game(1);
            Player p = mini.getCurrentPlayer();
            CardFunctions.gainCurses(p);
            p.setVictoryPts();
            Assert.AreEqual(3, p.getVictoryPts());

            Game big = new Game(4);
            p = big.getCurrentPlayer();
            CardFunctions.gainCurses(p);
            p.setVictoryPts();
            Assert.AreEqual(3, p.getVictoryPts());
            for (int i = 0; i < 3; i++)
            {
                p = big.nextTurnPlayer();
                p.setVictoryPts();
                Assert.AreEqual(2, p.getVictoryPts());
            }
        }

        [Test()]
        public void testGainCardRemodel()
        {
            StatusObject o = new StatusObject(false);
            CardFunctions.gainCardRemodel(p, o);
            Assert.IsTrue(p.getGain());
            Assert.IsTrue(o.trashForGainCheck());
            Assert.AreEqual(2, p.getCurrencyForGainBonus());
        }

        [Test()]
        public void testGainCardFeast()
        {
            StatusObject o = new StatusObject(false);
            CardFunctions.gainCardFeast(p, o);
            Assert.IsTrue(p.getGain());
            Assert.IsTrue(o.wasTrashedCorrectly());
            Assert.AreEqual(5, p.getCurrencyForGain());
        }

        [Test()]
        public void testGainCardWorkshop()
        {
            StatusObject o = new StatusObject(false);
            CardFunctions.gainCardWorkshop(p, o);
            Assert.IsTrue(p.getGain());
            Assert.IsTrue(o.wasTrashedCorrectly());
            Assert.AreEqual(4, p.getCurrencyForGain());
        }

        [Test()]
        public void testDoubleNextPlay()
        {
            CardFunctions.doubleNextPlay(p,1);
            Assert.IsTrue(p.getPlayMultipleTimes());
            Assert.AreEqual(2,p.getPlaysOfNextCardLeft());
        }

        [Test()]
        public void testGainCardChapel()
        {
            StatusObject o = new StatusObject(false);
            CardFunctions.trashUptoFourCards(p, o);
            Assert.IsTrue(o.needToTrashCards());
        }

        [Test()]
        public void testGainCardChancellor()
        {
            StatusObject o = new StatusObject(false);
            CardFunctions.discardDeckChancellor(p, o);
            Assert.IsTrue(o.needToDisardDeck());
        }

        [Test()]
        public void testBureaucratAction()
        {
            Game g = new Game(2);
            g.getPlayers()[1].getHand().getHand().Add(CardMother.Estate());
            CardFunctions.bureaucratAction(g.getPlayers()[0]);
            Assert.AreEqual(CardMother.Estate(), g.getPlayers()[1].getDeck().draw());
            Assert.AreEqual(CardMother.Silver(), g.getPlayers()[0].getDeck().draw());
        }

        [Test()]
        public void testBureaucratActionNoVictoryInOtherPlayer()
        {
            Game g = new Game(2);
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Feast());
            CardFunctions.bureaucratAction(g.getPlayers()[0]);
            Assert.AreEqual(CardMother.Feast(), g.getPlayers()[1].getDeck().draw());
            Assert.AreEqual(CardMother.Silver(), g.getPlayers()[0].getDeck().draw());
        }
    }
}
