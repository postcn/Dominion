﻿using System;
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
    }
}
