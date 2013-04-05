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
    }
}
