using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class GameTest
    {
        Game gameOnePlayer;
        Game gameTwoPlayer;
        Game gameThreePlayer;
        Game gameFourPlayer;

        [SetUp()]
        public void setUp()
        {
            this.gameOnePlayer = new Game(1);
            this.gameTwoPlayer = new Game(2);
            this.gameThreePlayer = new Game(3);
            this.gameFourPlayer = new Game(4);
        }

        /// <summary>
        /// Tests that games successfully initialize
        /// </summary>
        [Test()]
        public void testInitialization()
        {
            Assert.NotNull(this.gameOnePlayer);
            Assert.NotNull(this.gameTwoPlayer);
            Assert.NotNull(this.gameThreePlayer);
            Assert.NotNull(this.gameFourPlayer);
        }

        /// <summary>
        /// Tests the current player without taking any turns
        /// </summary>
        [Test()]
        public void testZeroPlayerTurn()
        {
            Assert.AreEqual(0, this.gameOnePlayer.getCurrentPlayerNumber());
            Assert.AreEqual(0, this.gameTwoPlayer.getCurrentPlayerNumber());
            Assert.AreEqual(0, this.gameThreePlayer.getCurrentPlayerNumber());
            Assert.AreEqual(0, this.gameFourPlayer.getCurrentPlayerNumber());
        }

        /// <summary>
        /// Tests the turn for the game with only one player, never changes player number
        /// </summary>
        [Test()]
        public void testTurnOnePlayer()
        {
            Assert.AreEqual(0, this.gameOnePlayer.getCurrentPlayerNumber());
            this.gameOnePlayer.nextTurn();
            Assert.AreEqual(0, this.gameOnePlayer.getCurrentPlayerNumber());
            this.gameOnePlayer.nextTurn();
            Assert.AreEqual(0, this.gameOnePlayer.getCurrentPlayerNumber());
        }

        /// <summary>
        /// tests the turn for the game with two players, should alternate player number between 0-1
        /// </summary>
        [Test()]
        public void testTurnTwoPlayer()
        {
            Assert.AreEqual(0, this.gameTwoPlayer.getCurrentPlayerNumber());
            this.gameTwoPlayer.nextTurn();
            Assert.AreEqual(1, this.gameTwoPlayer.getCurrentPlayerNumber());
            this.gameTwoPlayer.nextTurn();
            Assert.AreEqual(0, this.gameTwoPlayer.getCurrentPlayerNumber());
        }

        /// <summary>
        /// tests that we can take turns with three players, will oscillate 0 - 1 - 2
        /// </summary>
        [Test()]
        public void testTurnThreePlayer()
        {
            Assert.AreEqual(0, this.gameThreePlayer.getCurrentPlayerNumber());
            this.gameThreePlayer.nextTurn();
            Assert.AreEqual(1, this.gameThreePlayer.getCurrentPlayerNumber());
            this.gameThreePlayer.nextTurn();
            Assert.AreEqual(2, this.gameThreePlayer.getCurrentPlayerNumber());
            this.gameThreePlayer.nextTurn();
            Assert.AreEqual(0, this.gameThreePlayer.getCurrentPlayerNumber());
        }

        /// <summary>
        /// Tests that we can take turns with four players, will oscillate 0 - 1 - 2 - 3
        /// </summary>
        [Test()]
        public void testTurnFourPlayer()
        {
            Assert.AreEqual(0, this.gameFourPlayer.getCurrentPlayerNumber());
            this.gameFourPlayer.nextTurn();
            Assert.AreEqual(1, this.gameFourPlayer.getCurrentPlayerNumber());
            this.gameFourPlayer.nextTurn();
            Assert.AreEqual(2, this.gameFourPlayer.getCurrentPlayerNumber());
            this.gameFourPlayer.nextTurn();
            Assert.AreEqual(3, this.gameFourPlayer.getCurrentPlayerNumber());
            this.gameFourPlayer.nextTurn();
            Assert.AreEqual(0, this.gameFourPlayer.getCurrentPlayerNumber());
            this.gameFourPlayer.nextTurn();
            Assert.AreEqual(1, this.gameFourPlayer.getCurrentPlayerNumber());
        }

        /// <summary>
        /// Tests the calculation of the supply piles based on the number of players
        /// </summary>
        [Test()]
        public void testCalculateVictorySupply()
        {
            Assert.AreEqual(8, this.gameOnePlayer.calculateSupplyForVictory());
            Assert.AreEqual(8, this.gameTwoPlayer.calculateSupplyForVictory());
            Assert.AreEqual(12, this.gameThreePlayer.calculateSupplyForVictory());
            Assert.AreEqual(12, this.gameFourPlayer.calculateSupplyForVictory());
        }

        /// <summary>
        /// Tests the ability to call nextTurnPlayer with one player game
        /// </summary>
        [Test()]
        public void testNextTurnPlayerOnePlayer()
        {
            Assert.AreEqual(0, this.gameOnePlayer.nextTurnPlayer().getID());
        }

        /// <summary>
        /// Tests ability to use next turn player with two player game
        /// </summary>
        [Test()]
        public void testNextTurnPlayerTwoPlayer()
        {
            Assert.AreEqual(1, this.gameTwoPlayer.nextTurnPlayer().getID());
            Assert.AreEqual(0, this.gameTwoPlayer.nextTurnPlayer().getID());
            Assert.AreEqual(1, this.gameTwoPlayer.nextTurnPlayer().getID());
        }

        /// <summary>
        /// Tests that we can use next turn player with three player games
        /// </summary>
        [Test()]
        public void testNextTurnPlayerThreePlayer()
        {
            Assert.AreEqual(1, this.gameThreePlayer.nextTurnPlayer().getID());
            Assert.AreEqual(2, this.gameThreePlayer.nextTurnPlayer().getID());
            Assert.AreEqual(0, this.gameThreePlayer.nextTurnPlayer().getID());
            Assert.AreEqual(1, this.gameThreePlayer.nextTurnPlayer().getID());
        }

        /// <summary>
        /// Test that we can use next turn playe rwith the four player game
        /// </summary>
        [Test()]
        public void testNextTurnPlayerFourPlayer()
        {
            Assert.AreEqual(1, this.gameFourPlayer.nextTurnPlayer().getID());
            Assert.AreEqual(2, this.gameFourPlayer.nextTurnPlayer().getID());
            Assert.AreEqual(3, this.gameFourPlayer.nextTurnPlayer().getID());
            Assert.AreEqual(0, this.gameFourPlayer.nextTurnPlayer().getID());
        }

        /// <summary>
        /// Tests that we can use get player for all the games
        /// </summary>
        [Test()]
        public void testGetPlayer()
        {
            Assert.AreEqual(0, this.gameOnePlayer.getCurrentPlayer().getID());
            Assert.AreEqual(0, this.gameTwoPlayer.getCurrentPlayer().getID());
            this.gameTwoPlayer.nextTurn();
            Assert.AreEqual(1, this.gameTwoPlayer.getCurrentPlayer().getID());
            this.gameTwoPlayer.nextTurn();
            Assert.AreEqual(0, this.gameTwoPlayer.getCurrentPlayer().getID());
            Assert.AreEqual(0, this.gameThreePlayer.getCurrentPlayer().getID());
            this.gameThreePlayer.nextTurn();
            Assert.AreEqual(1, this.gameThreePlayer.getCurrentPlayer().getID());
            this.gameThreePlayer.nextTurn();
            Assert.AreEqual(2, this.gameThreePlayer.getCurrentPlayer().getID());
        }

        /// <summary>
        /// Tests the population of the buyable piles, this one has no cards other than the default that are in every game
        /// </summary>
        [Test()]
        public void testGetBuyablesDefaultCardsOnly()
        {
            //This test should fail once we add in new Action Cards.
            //This failure should remind me to update the test when we get more default cards.
            List<CardStack> buys = this.gameOnePlayer.getBuyables();
            Assert.AreEqual(6, buys.Count);
            List<String> names = new List<String>();
            names.Add("Copper");
            names.Add("Silver");
            names.Add("Gold");
            names.Add("Estate");
            names.Add("Duchy");
            names.Add("Province");
            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(names[i], buys[i].getCard().getName());
            }
        }
    }
}
