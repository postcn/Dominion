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

        [SetUp()]
        public void setUp()
        {
            this.p = new Player(0);
        }

        //[Test()]
        //public void TestgetHand()
        //{
        //    Deck myTestDeck = new Deck();
        //    Hand myTestHand = new Hand();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        myTestHand.draw(myTestDeck);
        //    }
        //    Assert.AreSame(myTestHand, p.getHand());
        //}

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
    }
}
