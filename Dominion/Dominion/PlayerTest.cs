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
        Game game;

        [SetUp()]
        public void setUp()
        {
            this.p = new Player(0);
            p1 = new Player(1);
            p2 = new Player(2);
            this.game = new Game(4);
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
        /// tests buying a card and it being a victory point, with a game
        /// </summary>
        [Test()]
        public void testbuyandvictorywithgame() {
            CardStack s = new CardStack(5, new Card(0, 0, 0, 0, 2, 0, 0, "Test", "Null", 0));
            Player p = game.getCurrentPlayer();
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
            Assert.IsFalse(p.play(new Card(0,0,0,0,0,0,0,"NULL","NULL",0)).wasPlayedProperly());
        }

        /// <summary>
        /// Checks that play is false because we are trying to play a non-action card
        /// </summary>
        [Test()]
        public void testPlayNotAction()
        {
            Assert.IsFalse(p.play(CardMother.Copper()).wasPlayedProperly());
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
            p.getTimesPlayed().Add(1);
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
            Assert.AreEqual(0,p.getPlayed().Count);
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
            Assert.IsFalse(p.play(new Card(2, 0, 0, 0, 0, 0, 0, "NULL ACTION", "NULL ACTION", 0)).wasPlayedProperly());
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
            Assert.IsTrue(p.play(CardMother.Market()).wasPlayedProperly());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(2, p.getBuysLeft());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(6, p.getHand().getHand().Count);
            p.getHand().getHand().Add(new Card(2, 0, 1, 2, 0, 0, 3, "Null Card for Test", "Null Card for Test", 0));
            Assert.IsTrue(p.play(new Card(2, 0, 1, 2, 0, 0, 3, "Null Card for Test", "Null Card for Test", 0)).wasPlayedProperly());
            Assert.AreEqual(3, p.getBuysLeft());
            Assert.AreEqual(2, p.getActionsLeft());
        }
        //Tests that function four works which just adds a buy.
        [Test()]
        public void testFunctionFour()
        {
            p.getHand().getHand().Add(CardMother.Woodcutter());
            Assert.IsTrue(p.play(CardMother.Woodcutter()).wasPlayedProperly());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(2, p.getBuysLeft());
            Assert.AreEqual(0, p.getActionsLeft());
            Assert.AreEqual(5, p.getHand().getHand().Count);
        }

        //Tests that function five works which adds a card and two actions.
        [Test()]
        public void testFunctionFive()
        {
            p.getHand().getHand().Add(CardMother.Village());
            Assert.IsTrue(p.play(CardMother.Village()).wasPlayedProperly());
            Assert.AreEqual(6, p.getCurrency());
            Assert.AreEqual(1, p.getBuysLeft());
            Assert.AreEqual(2, p.getActionsLeft());
            Assert.AreEqual(6, p.getHand().getHand().Count);
        }

        //Tests that function six works which adds a card and two actions.
        [Test()]
        public void testFunctionSix()
        {
            p.getHand().getHand().Add(CardMother.Festival());
            Assert.IsTrue(p.play(CardMother.Festival()).wasPlayedProperly());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(2, p.getBuysLeft());
            Assert.AreEqual(2, p.getActionsLeft());
            Assert.AreEqual(5, p.getHand().getHand().Count);
        }

        [Test()]
        public void testFunctionSeven()
        {
            Game g = new Game(2);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            p.getHand().getHand().Add(CardMother.Witch());
            Assert.IsTrue(p.play(CardMother.Witch()).wasPlayedProperly());
            Assert.AreEqual(7,p.getHand().size());
            p.setVictoryPts();
            p2.setVictoryPts();
            Assert.AreEqual(3, p.getVictoryPts());
            Assert.AreEqual(2, p2.getVictoryPts());
        }

        [Test()]
        public void testSetAndGetName()
        {
            Assert.IsNull(p.getName());
            String name = "Jim Bob Schuler";
            p.setName(name);
            Assert.AreEqual(name, p.getName());
        }

        [Test()]
        public void testEqualFailID()
        {
            Player two = new Player(2);
            Assert.IsFalse(two.Equals(p));
        }

        [Test()]
        public void testEqualFailNonPlayer()
        {
            Game g = new Game(0);
            Assert.IsFalse(p.Equals(g));
        }

        [Test()]
        public void testEqualFailStringName()
        {
            p.setName("George");
            Player two = new Player(0);
            two.setName("Fred");
            Assert.IsFalse(p.Equals(two));
        }

        [Test()]
        public void testEqualsPass()
        {
            String name = "Paige";
            Player two = new Player(0);
            Assert.IsTrue(two.Equals(p));
            p.setName(name);
            two.setName(name);
            Assert.IsTrue(p.Equals(two));
        }

        [Test()]
        public void testGetAndSetGain()
        {
            Assert.IsFalse(p.getGain());
            p.setGain(true);
            Assert.IsTrue(p.getGain());
        }

        [Test()]
        public void testGetAndSetBonus()
        {
            Assert.AreEqual(0, p.getCurrencyForGainBonus());
            p.setCurrencyForGainBonus(2);
            Assert.AreEqual(2, p.getCurrencyForGainBonus());
        }

        [Test()]
        public void testTrashForGain()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(1);
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.AreEqual(1, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            Assert.AreEqual(4, p.getHand().size());
        }

        [Test()]
        public void testGainFailNotInHand()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(1);
            Assert.IsFalse(p.trashForGain(CardMother.Estate()).wasTrashedCorrectly());
            Assert.AreEqual(0, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            Assert.AreEqual(5, p.getHand().size());
        }

        [Test()]
        public void testGainFailNoCardsInStack()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(2);
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.AreEqual(2, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            CardStack copper = new CardStack(0, CardMother.Copper());
            Assert.IsFalse(p.gainCard(copper).getGainedProperly());
        }

        [Test()]
        public void testGainFailNotEnoughMoney()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(2);
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.AreEqual(2, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            CardStack silver = new CardStack(1, CardMother.Silver());
            Assert.IsFalse(p.gainCard(silver).getGainedProperly());
        }

        [Test()]
        public void testGainFailNotGainInPlayer()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(2);
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.AreEqual(2, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            p.setGain(false);
            CardStack copper = new CardStack(1, CardMother.Copper());
            Assert.IsFalse(p.gainCard(copper).getGainedProperly());
        }

        [Test()]
        public void testGainFailNoGainsLeft()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            CardStack copper = new CardStack(1, CardMother.Copper());
            Assert.IsFalse(p.gainCard(copper).getGainedProperly());
        }

        [Test()]
        public void testGainPass()
        {
            Assert.IsFalse(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            p.setGain(true);
            p.setCurrencyForGainBonus(2);
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.AreEqual(2, p.getCurrencyForGain());
            Assert.IsTrue(p.getGain());
            p.setGainsLeft(1);
            CardStack estate = new CardStack(1, CardMother.Estate());
            Assert.IsTrue(p.gainCard(estate).getGainedProperly());
            Assert.AreEqual(CardMother.Estate(),p.getDeck().getInDiscard()[0]);
            Assert.IsFalse(p.getGain());
            Assert.AreEqual(0, p.getCurrencyForGain());
        }

        [Test()]
        public void testRemodelSequence()
        {
            p.getHand().getHand().Add(CardMother.Remodel());
            Assert.IsTrue(p.play(CardMother.Remodel()).wasPlayedProperly());
            Assert.AreEqual(1, p.getGainsLeft());
            CardStack estate = new CardStack(1, CardMother.Estate());
            Assert.IsTrue(p.trashForGain(CardMother.Copper()).wasTrashedCorrectly());
            Assert.IsTrue(p.gainCard(estate).getGainedProperly());
            Assert.AreEqual(CardMother.Estate(),p.getDeck().getInDiscard()[0]);
            Assert.AreEqual(0, p.getGainsLeft());
            Assert.IsFalse(p.getGain());
        }

        [Test()]
        public void testRemodelSequenceWorthMore()
        {
            p.getHand().getHand().Add(CardMother.Remodel());
            p.getHand().getHand().Add(CardMother.Remodel());
            Assert.IsTrue(p.play(CardMother.Remodel()).wasPlayedProperly());
            CardStack gold = new CardStack(1, CardMother.Gold());
            Assert.IsTrue(p.trashForGain(CardMother.Remodel()).wasTrashedCorrectly());
            Assert.IsTrue(p.gainCard(gold).getGainedProperly());
            Assert.AreEqual(CardMother.Gold(), p.getDeck().getInDiscard()[0]);
        }

        [Test()]
        public void testPlayingFeast()
        {
            p.getHand().getHand().Add(CardMother.Feast());
            Assert.IsTrue(p.play(CardMother.Feast()).wasPlayedProperly());
            CardStack duchy = new CardStack(1, CardMother.Duchy());
            Assert.IsTrue(p.gainCard(duchy).getGainedProperly());
            Assert.AreEqual(CardMother.Duchy(), p.getDeck().getInDiscard()[0]);
            Assert.IsFalse(p.getPlayed().Contains(CardMother.Feast()));
        }

        [Test()]
        public void testPlayingWorkshop()
        {
            p.getHand().getHand().Add(CardMother.Workshop());
            Assert.IsTrue(p.play(CardMother.Workshop()).wasPlayedProperly());
            CardStack feast = new CardStack(1, CardMother.Feast());
            Assert.IsTrue(p.getGain());
            Assert.IsTrue(p.gainCard(feast).getGainedProperly());
            Assert.AreEqual(CardMother.Feast(), p.getDeck().getInDiscard()[0]);
        }

        [Test()]
        public void testSetAndGetPlayMultipleTimes()
        {
            Assert.IsFalse(p.getPlayMultipleTimes());
            p.setPlayMultipleTimes(true);
            Assert.IsTrue(p.getPlayMultipleTimes());
        }

        [Test()]
        public void testSetAndGetPlaysOfThisCardLeft()
        {
            //should start off with one play.
            Assert.AreEqual(1, p.getPlaysOfNextCardLeft());
            p.setTimesToPlayNextCard(2);
            Assert.AreEqual(2, p.getPlaysOfNextCardLeft());
        }

        [Test()]
        public void testThroneRoomByItself()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            Assert.IsTrue(p.play(CardMother.ThroneRoom()).wasPlayedProperly());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(2, p.getPlaysOfNextCardLeft());
        }

        [Test()]
        public void testThroneRoomAndVillage()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Village());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Village());
            Assert.AreEqual(4, p.getActionsLeft());
            Assert.AreEqual(7, p.getHand().size());
        }

        [Test()]
        public void testThroneRoomAndSmithy()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Smithy());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Smithy());
            Assert.AreEqual(0, p.getActionsLeft());
            Assert.AreEqual(10, p.getHand().size());//draw whole deck
        }

        [Test()]
        public void testFourThroneRoomAndWoodcutter()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Woodcutter());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Woodcutter());
            Assert.AreEqual(0, p.getActionsLeft());
            Assert.AreEqual(17, p.getBuysLeft());
            Assert.AreEqual(37, p.getCurrency());
        }

        [Test()]
        public void testPlayMultipleThroneRoomInRow()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(2, p.getPlaysOfNextCardLeft());
            p.play(CardMother.ThroneRoom());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(4, p.getPlaysOfNextCardLeft());
            p.play(CardMother.ThroneRoom());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(8, p.getPlaysOfNextCardLeft());
            p.play(CardMother.ThroneRoom());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(16, p.getPlaysOfNextCardLeft());
        }

        [Test()]
        public void testThroneRoomAndRemodel()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Remodel());
            Assert.IsTrue(p.play(CardMother.ThroneRoom()).wasPlayedProperly());
            Assert.AreEqual(1, p.getActionsLeft());
            Assert.AreEqual(2, p.getPlaysOfNextCardLeft());

            StatusObject o = p.play(CardMother.Remodel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.trashForGainCheck());
            Assert.AreEqual(2,p.getGainsLeft());

            CardStack estate = new CardStack(2, CardMother.Estate());

            o = p.trashForGain(CardMother.Copper());
            Assert.IsTrue(o.wasTrashedCorrectly());
            o = p.gainCard(estate);
            Assert.AreEqual(1, p.getGainsLeft());
            Assert.IsTrue(o.trashForGainCheck());//still have gains left
            //so signal to trashForGain from remodel

            o = p.trashForGain(CardMother.Copper());
            Assert.IsTrue(o.wasTrashedCorrectly());
            o = p.gainCard(estate);
            //Console.Write(o.getMessage());
            Assert.IsTrue(o.getGainedProperly());
            Assert.IsFalse(o.wasTrashedCorrectly());
            Assert.IsFalse(o.trashForGainCheck());
        }

        [Test()]
        public void testRemodelNoCardsInHand()
        {
            p.getHand().getHand().Clear();
            p.getHand().getHand().Add(CardMother.Remodel());
            p.play(CardMother.Remodel());
            Assert.AreEqual(1, p.getGainsLeft());
            StatusObject o = p.trashForGain(null);
            Assert.IsTrue(o.wasTrashedCorrectly());
            Assert.AreEqual(0, p.getGainsLeft());
        }

        [Test()]
        public void testLastPlayedCard()
        {
            p.getHand().getHand().Add(CardMother.Village());
            p.getHand().getHand().Add(CardMother.Smithy());
            p.getHand().getHand().Add(CardMother.Remodel());

            Assert.IsNull(p.getLastPlayedCard());

            p.play(CardMother.Village());
            Assert.AreEqual(CardMother.Village(), p.getLastPlayedCard());

            p.play(CardMother.Smithy());
            Assert.AreEqual(CardMother.Smithy(), p.getLastPlayedCard());

            p.play(CardMother.Remodel());
            Assert.AreEqual(CardMother.Remodel(), p.getLastPlayedCard());
        }

        [Test()]
        public void testThroneRoomAndFeast()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Feast());

            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Feast());

            Assert.AreEqual(2, p.getGainsLeft());

            CardStack lab = new CardStack(2, CardMother.Laboratory());

            StatusObject o = p.gainCard(lab);
            Assert.IsTrue(o.getGainedProperly());
            Assert.IsTrue(o.wasTrashedCorrectly());

            o = p.gainCard(lab);
            Assert.IsTrue(o.getGainedProperly());
            Assert.IsFalse(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testThroneRoomAndWorkshop()
        {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Workshop());

            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Workshop());

            Assert.AreEqual(2, p.getGainsLeft());

            CardStack silver = new CardStack(2, CardMother.Silver());

            StatusObject o = p.gainCard(silver);

            Assert.IsTrue(o.getGainedProperly());
            Assert.IsTrue(o.wasTrashedCorrectly());

            CardStack gold = new CardStack(1, CardMother.Gold());

            o = p.gainCard(gold);
            Assert.IsFalse(o.getGainedProperly());

            o = p.gainCard(silver);
            Assert.IsTrue(o.getGainedProperly());
            Assert.IsFalse(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testDiscardCardAndDrawSameAmountZeroCards()
        {
            List<Card> cards = new List<Card>();
            StatusObject o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsTrue(o.wasDiscardedAndDrawnSuccessfully());
        }

        [Test()]
        public void testDiscardAndDrawSameAmountSuccess()
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(CardMother.Copper());
            }

            StatusObject o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsTrue(o.wasDiscardedAndDrawnSuccessfully());
            Assert.AreEqual(CardMother.Estate(), p.getHand().getHand()[4]);//drew the rest of the deck and this should be an estate
        }

        [Test()]
        public void testDiscardAndDrawSameAmountFailCardNotInHand()
        {
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Feast());

            StatusObject o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsFalse(o.wasDiscardedAndDrawnSuccessfully());
        }

        [Test()]
        public void testDiscardAndDrawFailTooManyOfOneCard()
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(CardMother.Copper());
            }

            StatusObject o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsTrue(o.wasDiscardedAndDrawnSuccessfully());
            Assert.AreEqual(CardMother.Estate(), p.getHand().getHand()[4]);//drew the rest of the deck and this should be an estate
            o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsFalse(o.wasDiscardedAndDrawnSuccessfully());
            Assert.AreEqual(CardMother.Estate(), p.getHand().getHand()[4]);//the hand should not change.
            Assert.AreEqual(5, p.getHand().size());
        }

        [Test()]
        public void testPlayCellar()
        {
            p.getHand().getHand().Add(CardMother.Cellar());
            StatusObject o = p.play(CardMother.Cellar());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToDiscardCardsFromHandToDrawSameNumber());

            List<Card> cards = new List<Card>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(CardMother.Copper());
            }

            o = p.discardCardsAndDrawSameAmount(cards);
            Assert.IsTrue(o.wasDiscardedAndDrawnSuccessfully());
            Assert.AreEqual(CardMother.Estate(), p.getHand().getHand()[4]);
        }

        [Test()]
        public void testGetAndSetTrashesLeft()
        {
            Assert.AreEqual(0, p.getTrashesNeeded());
            p.setTrashesNeeded(1);
            Assert.AreEqual(1, p.getTrashesNeeded());
            p.setTrashesNeeded(p.getTrashesNeeded() + 1);
            Assert.AreEqual(2, p.getTrashesNeeded());
        }

        [Test()]
        public void testTrashACopperForCurrencyBonusFailNotACopper()
        {
            p.setTrashesNeeded(1);
            StatusObject o = p.trashACopperForCurrencyBonus(CardMother.Feast());
            Assert.IsFalse(o.wasCopperTrashedSuccessfullyForBonus());
        }

        [Test()]
        public void testTrashACopperNull() {
            p.setTrashesNeeded(1);
            StatusObject o = p.trashACopperForCurrencyBonus(null);
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
        }

        [Test()]
        public void testTrashACopperForCurrencyBonusSucceedNoTrashesNeeded()
        {
            Assert.AreEqual(0, p.getTrashesNeeded());
            StatusObject o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
            Assert.AreEqual(5, p.getHand().size());
            Assert.AreEqual(5, p.getCurrency());
        }

        [Test()]
        public void testTrashACopperForCurrencyBonusFailNoCopperInHand()
        {
            for (int i = 0; i < 5; i++)
            {
                p.getHand().discard(CardMother.Copper(), p.getDeck());
            }
            p.getHand().getHand().Add(CardMother.Silver());
            p.getHand().getHand().Add(CardMother.Feast());
            p.setTrashesNeeded(1);
            StatusObject o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
            Assert.AreEqual(2, p.getCurrency());
        }

        [Test()]
        public void testTrashACopperForCurrencyBonusSucceed()
        {
            p.setTrashesNeeded(1);
            p.setTrashCurrencyBonus(3);
            StatusObject o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
            Assert.AreEqual(3, p.getTrashCurrencyBonus());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(4, p.getHand().size());
            Assert.IsTrue(p.getDeck().getInDiscard().Count == 0);
        }

        [Test()]
        public void testPlayMoneyLender()
        {
            p.getHand().getHand().Add(CardMother.Moneylender());
            StatusObject o = p.play(CardMother.Moneylender());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCoppersForCurrency());
            o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(0, p.getTrashesNeeded());
            Assert.AreEqual(3, p.getTrashCurrencyBonus());
            Assert.AreEqual(4, p.getHand().getHand().Count);
        }

        [Test()]
        public void testPlayThroneRoomeMoneyLender() {
            p.getHand().getHand().Add(CardMother.Moneylender());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            StatusObject o = p.play(CardMother.ThroneRoom());
            o = p.play(CardMother.Moneylender());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCoppersForCurrency());
            o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.needToTrashCoppersForCurrency());
            o = p.trashACopperForCurrencyBonus(CardMother.Copper());
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
            Assert.AreEqual(9, p.getCurrency());
            Assert.AreEqual(0, p.getTrashesNeeded());
            Assert.AreEqual(3, p.getTrashCurrencyBonus());
            Assert.AreEqual(3, p.getHand().getHand().Count);
        }

        [Test()]
        public void testPlayChapel()
        {
            p.getHand().getHand().Add(CardMother.Chapel());
            StatusObject o = p.play(CardMother.Chapel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCards());
            Assert.AreEqual(4, p.getPossibleTrashes());
            o = p.trashCards(new List<Card>());
            Assert.IsTrue(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testPlayChapelFailTooManyCards()
        {
            p.getHand().getHand().Add(CardMother.Chapel());
            StatusObject o = p.play(CardMother.Chapel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCards());
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(CardMother.Copper());
            }
            o = p.trashCards(cards);
            Assert.IsFalse(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testPlayChapelFailCardNotInHand()
        {
            p.getHand().getHand().Add(CardMother.Chapel());
            StatusObject o = p.play(CardMother.Chapel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCards());
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Estate());
            o = p.trashCards(cards);
            Assert.IsFalse(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testPlayChapelSuccessful()
        {
            p.getHand().getHand().Add(CardMother.Chapel());
            StatusObject o = p.play(CardMother.Chapel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCards());
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                cards.Add(CardMother.Copper());
            }
            o = p.trashCards(cards);
            Assert.IsTrue(o.wasTrashedCorrectly());
            Assert.AreEqual(1, p.getHand().size());
        }

        [Test()]
        public void testPlayThroneRoomAndChapel()
        {
            p.getHand().getHand().Add(CardMother.Chapel());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            StatusObject o = p.play(CardMother.Chapel());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToTrashCards());
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(CardMother.Copper());
            }
            o = p.trashCards(cards);
            Assert.IsTrue(o.wasTrashedCorrectly());
            Assert.AreEqual(0, p.getHand().size());
            Assert.AreEqual(0, p.getPossibleTrashes());
        }

        [Test()]
        public void testPlayChancellor()
        {
            p.getHand().getHand().Add(CardMother.Chancellor());
            StatusObject o = p.play(CardMother.Chancellor());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToDisardDeck());
            o = p.discardDeck(true);
            Assert.IsTrue(o.wasDeckDiscardedCorrectly());
            Assert.AreEqual(0, p.getDeck().getInDeck().Count);
            Assert.AreEqual(7, p.getCurrency());
        }

        [Test()]
        public void testPlayChancellorFalse() {
            p.getHand().getHand().Add(CardMother.Chancellor());
            StatusObject o = p.play(CardMother.Chancellor());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToDisardDeck());
            o = p.discardDeck(false);
            Assert.IsTrue(o.wasDeckDiscardedCorrectly());
            Assert.AreEqual(5, p.getDeck().getInDeck().Count);
            Assert.AreEqual(7, p.getCurrency());
        }

        [Test()]
        public void testPlayThroneRoomAndChancellor()
        {
            p.getHand().getHand().Add(CardMother.Chancellor());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.play(CardMother.ThroneRoom());
            StatusObject o = p.play(CardMother.Chancellor());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToDisardDeck());
            o = p.discardDeck(true);
            Assert.IsTrue(o.wasDeckDiscardedCorrectly());
            Assert.AreEqual(0, p.getDeck().getInDeck().Count);
            Assert.AreEqual(9, p.getCurrency());
        }

        [Test()]
        public void testSetOtherPlayersNoGameSet()
        {
            p.setOtherPlayerList();
            List<Player> list = p.getOtherPlayers();
            Assert.AreEqual(0, list.Count);
        }

        [Test()]
        public void testSetOtherPlayersInAGame()
        {
            foreach (Player p in this.game.getPlayers())
            {
                p.setOtherPlayerList();
                Assert.AreEqual(3, p.getOtherPlayers().Count);
                Assert.IsFalse(p.getOtherPlayers().Contains(p));
            }
        }

        [Test()]
        public void testPlayBureaucrat()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            p.getHand().getHand().Add(CardMother.Bureaucrat());
            g.getPlayers()[1].getHand().getHand().Add(CardMother.Duchy());
            p.play(CardMother.Bureaucrat());
            Assert.AreEqual(CardMother.Silver(), p.getDeck().getInDeck()[0]);
            p = g.nextTurnPlayer();
            Assert.AreEqual(CardMother.Duchy(), p.getDeck().getInDeck()[0]);
            p = g.nextTurnPlayer();
            Assert.AreEqual(CardMother.Copper(), p.getDeck().getInDeck()[0]);
            Console.Write(g.getGameStatus());
        }

        [Test()]
        public void testGardens()
        {
            p.getHand().getHand().Add(CardMother.Gardens());
            p.getDeck().getInDeck().Add(CardMother.Gardens());
            p.getDeck().getInDiscard().Add(CardMother.Gardens());
            //three of them so should add three victory points for every ten cards rounded down.
            p.setVictoryPts();
            Assert.AreEqual(6, p.getVictoryPts());
            for (int i = 0; i < 10; i++)
            {
                p.getDeck().getInDeck().Add(CardMother.Copper());
            }
            p.setVictoryPts();
            Assert.AreEqual(9, p.getVictoryPts());
            for (int i = 0; i < 5; i++)
            {
                p.getDeck().getInDeck().Add(CardMother.Copper());
            }
            p.setVictoryPts();
            Assert.AreEqual(9, p.getVictoryPts());
        }

        [Test()]
        public void testPlayMine()
        {
            p.getHand().getHand().Add(CardMother.Mine());
            StatusObject o = p.play(CardMother.Mine());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToMine());
            o = p.mineATreasureCard(CardMother.Copper());
            Assert.IsTrue(o.wasMinedCorrectly());
            Assert.AreEqual(6, p.getCurrency());
        }

        [Test()]
        public void testPlayMineNull() {
            p.getHand().getHand().Add(CardMother.Mine());
            StatusObject o = p.play(CardMother.Mine());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToMine());
            o = p.mineATreasureCard(null);
            Assert.IsFalse(o.wasMinedCorrectly());
            Assert.AreEqual(5, p.getCurrency());
        }

        [Test()]
        public void testPlayMineNotCopperOrSilver() {
            p.getHand().getHand().Add(CardMother.Mine());
            StatusObject o = p.play(CardMother.Mine());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToMine());
            o = p.mineATreasureCard(CardMother.Feast());
            Assert.IsFalse(o.wasMinedCorrectly());
            Assert.AreEqual(5, p.getCurrency());
        }

        [Test()]
        public void testPlayMineSilver() {
            p.getHand().getHand().Add(CardMother.Mine());
            p.getHand().getHand().Add(CardMother.Silver());
            StatusObject o = p.play(CardMother.Mine());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.IsTrue(o.needToMine());
            o = p.mineATreasureCard(CardMother.Silver());
            Assert.IsTrue(o.wasMinedCorrectly());
            Assert.AreEqual(8, p.getCurrency());
        }

        [Test()]
        public void testPlayThroneRoomMine() {
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            p.getHand().getHand().Add(CardMother.Mine());
            p.play(CardMother.ThroneRoom());
            p.play(CardMother.Mine());
            Assert.AreEqual(2, p.getTrashesNeeded());
            StatusObject o = p.mineATreasureCard(CardMother.Copper());
            Assert.IsTrue(o.needToMine());
            Assert.AreEqual(1, p.getTrashesNeeded());
            p.mineATreasureCard(CardMother.Silver());
            Assert.AreEqual(7, p.getCurrency());
            Assert.AreEqual(0, p.getTrashesNeeded());
        }

        [Test()]
        public void testPlayMineNoSilverOrCopper()
        {
            p.getHand().getHand().Clear();
            p.getHand().getHand().Add(CardMother.Mine());
            p.getHand().getHand().Add(CardMother.Feast());
            p.play(CardMother.Mine());
            StatusObject o = p.mineATreasureCard(CardMother.Feast());
            Assert.IsTrue(o.wasMinedCorrectly());
        }

        [Test()]
        public void testMilitiaEffectLessThanThreeInHand()
        {
            StatusObject o = new StatusObject(false);
            o.setMilitiaPlayed(true);
            p.getHand().remove(CardMother.Copper());
            p.getHand().remove(CardMother.Copper());
            o = p.militiaDiscardEffect(new List<Card>());
            Assert.IsFalse(o.wasMilitiaPlayed());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testMilitiaEffectCardNotInHand()
        {
            StatusObject o = new StatusObject(false);
            o.setMilitiaPlayed(true);
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Feast());
            p.militiaDiscardEffect(cards);
            Assert.IsTrue(o.wasMilitiaPlayed());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testMilitiaEffectCardNotEnoughCards()
        {
            StatusObject o = new StatusObject(false);
            o.setMilitiaPlayed(true);
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            p.militiaDiscardEffect(cards);
            Assert.IsTrue(o.wasMilitiaPlayed());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testMilitiaEffectSuccess()
        {
            StatusObject o = new StatusObject(false);
            o.setMilitiaPlayed(true);
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Copper());
            o = p.militiaDiscardEffect(cards);
            Assert.IsFalse(o.wasMilitiaPlayed());
            Assert.AreEqual(2, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testMilitiaEffectTooManyCards()
        {
            StatusObject o = new StatusObject(false);
            o.setMilitiaPlayed(true);
            List<Card> cards = new List<Card>();
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Copper());
            o = p.militiaDiscardEffect(cards);
            Assert.IsTrue(o.wasMilitiaPlayed());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testPlayMilitiaSetup()
        {
            Game g = new Game(2);
            Player p = g.getCurrentPlayer();
            p.getHand().getHand().Add(CardMother.Militia());
            StatusObject o = p.play(CardMother.Militia());
            Assert.IsTrue(o.wasPlayedProperly());
            Assert.AreEqual(7, p.getCurrency());
            p = g.nextTurnPlayer();
            Assert.AreEqual(1, p.functionsToCall.Count);
            o = p.callDelayedFunctions();
            Assert.IsTrue(o.wasMilitiaPlayed());
            Assert.IsTrue(o.needToContinueWithDelayedFunctions());
            List<Card> cards = new List<Card>();
            o = p.militiaDiscardEffect(cards);
            Assert.IsTrue(o.wasMilitiaPlayed());
            cards.Add(CardMother.Copper());
            cards.Add(CardMother.Copper());
            o = p.militiaDiscardEffect(cards);
            Assert.IsFalse(o.wasMilitiaPlayed());
            Assert.IsTrue(o.needToContinueWithDelayedFunctions());
            o = p.callDelayedFunctions();
            Assert.IsFalse(o.needToContinueWithDelayedFunctions());
        }

        [Test()]
        public void testPlayThroneRoomAndMilitia()
        {
            Game g = new Game(2);
            Player p = g.getCurrentPlayer();
            p.getHand().getHand().Add(CardMother.Militia());
            p.getHand().getHand().Add(CardMother.ThroneRoom());
            StatusObject o = p.play(CardMother.ThroneRoom());
            o = p.play(CardMother.Militia());
            Assert.AreEqual(9, p.getCurrency());
            p = g.nextTurnPlayer();
            Assert.AreEqual(1, p.functionsToCall.Count);
        }

        [Test()]
        public void testPlayCouncilRoom()
        {
            Game g = new Game(2);
            Player p = g.getCurrentPlayer();
            p.getHand().getHand().Add(CardMother.CouncilRoom());
            p.play(CardMother.CouncilRoom());
            Assert.AreEqual(9, p.getHand().size());
            Assert.AreEqual(2, p.getBuysLeft());
            p = g.nextTurnPlayer();
            p.callDelayedFunctions();
            Assert.AreEqual(6, p.getHand().size());
        }

        [Test()]
        public void testPlaySpy()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Duchy());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.ThroneRoom());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Militia());
            p.getHand().getHand().Add(CardMother.Spy());
            StatusObject o = p.play(CardMother.Spy());
            Assert.IsTrue(o.canSpyOnDeck());
            List<Card> cards = p.spyOnDecks();
            o = p.keepOrDiscardSpiedCards(cards);
            Assert.IsTrue(o.playerSpiedSuccessfully());
            Assert.AreEqual(CardMother.Feast(), g.getPlayers()[0].getDeck().getInDiscard()[0]);
            Assert.AreEqual(CardMother.Duchy(), g.getPlayers()[1].getDeck().getInDiscard()[0]);
            Assert.AreEqual(CardMother.ThroneRoom(), g.getPlayers()[2].getDeck().getInDiscard()[0]);
            Assert.AreEqual(CardMother.Militia(), g.getPlayers()[3].getDeck().getInDiscard()[0]);
        }

        [Test()]
        public void testPlaySpyWithNull()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Duchy());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.ThroneRoom());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Militia());
            p.getHand().getHand().Add(CardMother.Spy());
            StatusObject o = p.play(CardMother.Spy());
            Assert.IsTrue(o.canSpyOnDeck());
            List<Card> cards = p.spyOnDecks();
            cards[1] = null;
            o = p.keepOrDiscardSpiedCards(cards);
            Assert.IsTrue(o.playerSpiedSuccessfully());
            Assert.AreEqual(CardMother.Feast(), g.getPlayers()[0].getDeck().getInDiscard()[0]);
            Assert.AreEqual(CardMother.Duchy(), g.getPlayers()[1].getDeck().peekAtTopCard());
            Assert.AreEqual(CardMother.ThroneRoom(), g.getPlayers()[2].getDeck().getInDiscard()[0]);
            Assert.AreEqual(CardMother.Militia(), g.getPlayers()[3].getDeck().getInDiscard()[0]);
        }

        [Test()]
        public void testPlaySpyFailureBadCard()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[0].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Duchy());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.ThroneRoom());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Militia());
            p.getHand().getHand().Add(CardMother.Spy());
            StatusObject o = p.play(CardMother.Spy());
            Assert.IsTrue(o.canSpyOnDeck());
            List<Card> cards = p.spyOnDecks();
            cards[3] = CardMother.Moneylender();
            o = p.keepOrDiscardSpiedCards(cards);
            Assert.IsFalse(o.playerSpiedSuccessfully());
            Assert.AreEqual(CardMother.Feast(), g.getPlayers()[0].getDeck().peekAtTopCard());
            Assert.AreEqual(CardMother.Duchy(), g.getPlayers()[1].getDeck().peekAtTopCard());
            Assert.AreEqual(CardMother.ThroneRoom(), g.getPlayers()[2].getDeck().peekAtTopCard());
            Assert.AreEqual(CardMother.Militia(), g.getPlayers()[3].getDeck().peekAtTopCard());
        }

        [Test()]
        public void testPlayThiefSuccess()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Gold());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Silver());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            p.getHand().getHand().Add(CardMother.Thief());
            StatusObject o = p.play(CardMother.Thief());
            Assert.IsTrue(o.selectTrashFromThief());
            List<List<Card>> cards = p.getThiefList();
            Assert.AreEqual(2, cards[0].Count);
            Assert.AreEqual(1, cards[1].Count);
            Assert.AreEqual(0, cards[2].Count);
            List<Card> steal = new List<Card>();
            steal.Add(CardMother.Gold());
            steal.Add(CardMother.Copper());
            steal.Add(null);
            o = p.validateThiefStolenCards(steal);
            Assert.IsTrue(o.needToKeepThief());
            Assert.AreEqual(CardMother.Silver(), g.getPlayers()[1].getDeck().getInDiscard()[0]);
            List<Card> keep = new List<Card>();
            keep.Add(CardMother.Gold());
            o = p.keepCardsFromThief(keep);
            Assert.AreEqual(1, p.getDeck().getInDiscard().Count);
            Assert.AreEqual(CardMother.Gold(), p.getDeck().getInDiscard()[0]);
        }

        [Test()]
        public void testPlayThiefNullWhenHasStolenCards()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Gold());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Silver());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            p.getHand().getHand().Add(CardMother.Thief());
            StatusObject o = p.play(CardMother.Thief());
            Assert.IsTrue(o.selectTrashFromThief());
            List<List<Card>> cards = p.getThiefList();
            Assert.AreEqual(2, cards[0].Count);
            Assert.AreEqual(1, cards[1].Count);
            Assert.AreEqual(0, cards[2].Count);
            List<Card> steal = new List<Card>();
            steal.Add(CardMother.Gold());
            steal.Add(null);
            steal.Add(null);
            o = p.validateThiefStolenCards(steal);
            Assert.IsTrue(o.selectTrashFromThief());
            Assert.AreEqual(0, p.getPossibleCardsToKeepFromThief().Count);
        }

        [Test()]
        public void testPlayThiefValidateCardNotStolen()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Gold());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Silver());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            p.getHand().getHand().Add(CardMother.Thief());
            StatusObject o = p.play(CardMother.Thief());
            Assert.IsTrue(o.selectTrashFromThief());
            List<List<Card>> cards = p.getThiefList();
            Assert.AreEqual(2, cards[0].Count);
            Assert.AreEqual(1, cards[1].Count);
            Assert.AreEqual(0, cards[2].Count);
            List<Card> steal = new List<Card>();
            steal.Add(CardMother.Gold());
            steal.Add(CardMother.Silver());
            steal.Add(null);
            o = p.validateThiefStolenCards(steal);
            Assert.IsTrue(o.selectTrashFromThief());
            Assert.AreEqual(0, p.getPossibleCardsToKeepFromThief().Count);
        }

        [Test()]
        public void testPlayThiefAndTryToKeepCardNotInTrashed()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Gold());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Silver());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            p.getHand().getHand().Add(CardMother.Thief());
            StatusObject o = p.play(CardMother.Thief());
            Assert.IsTrue(o.selectTrashFromThief());
            List<List<Card>> cards = p.getThiefList();
            Assert.AreEqual(2, cards[0].Count);
            Assert.AreEqual(1, cards[1].Count);
            Assert.AreEqual(0, cards[2].Count);
            List<Card> steal = new List<Card>();
            steal.Add(CardMother.Gold());
            steal.Add(CardMother.Copper());
            steal.Add(null);
            o = p.validateThiefStolenCards(steal);
            Assert.IsTrue(o.needToKeepThief());
            Assert.AreEqual(CardMother.Silver(), g.getPlayers()[1].getDeck().getInDiscard()[0]);
            List<Card> keep = new List<Card>();
            keep.Add(CardMother.Gold());
            keep.Add(CardMother.Gold());
            Assert.AreEqual(2, p.getPossibleCardsToKeepFromThief().Count);
            o = p.keepCardsFromThief(keep);
            Assert.IsTrue(o.needToKeepThief());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testPlayThiefAndTryToKeepNull()
        {
            Game g = new Game(4);
            Player p = g.getCurrentPlayer();
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Gold());
            g.getPlayers()[1].getDeck().addCardToFront(CardMother.Silver());
            g.getPlayers()[2].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            g.getPlayers()[3].getDeck().addCardToFront(CardMother.Feast());
            p.getHand().getHand().Add(CardMother.Thief());
            StatusObject o = p.play(CardMother.Thief());
            Assert.IsTrue(o.selectTrashFromThief());
            List<List<Card>> cards = p.getThiefList();
            Assert.AreEqual(2, cards[0].Count);
            Assert.AreEqual(1, cards[1].Count);
            Assert.AreEqual(0, cards[2].Count);
            List<Card> steal = new List<Card>();
            steal.Add(CardMother.Gold());
            steal.Add(CardMother.Copper());
            steal.Add(null);
            o = p.validateThiefStolenCards(steal);
            Assert.IsTrue(o.needToKeepThief());
            Assert.AreEqual(CardMother.Silver(), g.getPlayers()[1].getDeck().getInDiscard()[0]);
            List<Card> keep = new List<Card>();
            keep.Add(CardMother.Gold());
            keep.Add(null);
            o = p.keepCardsFromThief(keep);
            Assert.IsTrue(o.needToKeepThief());
            Assert.AreEqual(0, p.getDeck().getInDiscard().Count);
        }

        [Test()]
        public void testPlayMoat()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            p.getHand().getHand().Add(CardMother.Moat());
            p.play(CardMother.Moat());
            Assert.AreEqual(7, p.getHand().size());
        }

        [Test()]
        public void testPlayMoatAndWitch()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            Player p3 = g.getPlayers()[2];
            p2.getHand().getHand().Add(CardMother.Moat());
            p.getHand().getHand().Add(CardMother.Witch());
            p.play(CardMother.Witch());
            p2.setVictoryPts();
            p3.setVictoryPts();
            Assert.AreEqual(2, p3.getVictoryPts());
            Assert.AreEqual(3, p2.getVictoryPts());
        }

        [Test()]
        public void testPlayMoatAndBureaucrat()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            Player p3 = g.getPlayers()[2];
            p2.getHand().getHand().Add(CardMother.Moat());
            p.getHand().getHand().Add(CardMother.Bureaucrat());
            p2.getHand().getHand().Add(CardMother.Estate());
            p3.getHand().getHand().Add(CardMother.Estate());
            p.play(CardMother.Bureaucrat());
            Assert.AreEqual(CardMother.Copper(), p2.getDeck().peekAtTopCard());
            Assert.AreEqual(CardMother.Estate(), p3.getDeck().peekAtTopCard());
        }

        [Test()]
        public void testPlayMoatAndMilitia()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            Player p3 = g.getPlayers()[2];
            p2.getHand().getHand().Add(CardMother.Moat());
            p.getHand().getHand().Add(CardMother.Militia());
            p.play(CardMother.Militia());
            Assert.AreEqual(0, p2.functionsToCall.Count);
            Assert.AreEqual(1, p3.functionsToCall.Count);
        }

        [Test()]
        public void testPlayMoatAndThief()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            Player p3 = g.getPlayers()[2];
            p2.getHand().getHand().Add(CardMother.Moat());
            p.getHand().getHand().Add(CardMother.Thief());
            p.play(CardMother.Thief());
            List<List<Card>> thiefed = p.getThiefList();
            Assert.AreEqual(0, thiefed[0].Count);
            Assert.AreEqual(2, thiefed[1].Count);
        }

        [Test()]
        public void testPlayMoatAndSpy()
        {
            Game g = new Game(3);
            Player p = g.getCurrentPlayer();
            Player p2 = g.getPlayers()[1];
            Player p3 = g.getPlayers()[2];
            p2.getHand().getHand().Add(CardMother.Moat());
            p.getHand().getHand().Add(CardMother.Spy());
            p.play(CardMother.Spy());
            List<Card> cards = p.spyOnDecks();
            p.keepOrDiscardSpiedCards(cards);
            Assert.AreEqual(0, p2.getDeck().getInDiscard().Count);
            Assert.AreEqual(1, p3.getDeck().getInDiscard().Count);
            Assert.AreEqual(1, p.getDeck().getInDiscard().Count);
        }
    }
}
