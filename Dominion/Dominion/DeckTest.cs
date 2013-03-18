using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class DeckTest
    {
        Deck d;
        [SetUp()]
        public void setupFunc()
        {
            this.d = new Deck();
        }
        [Test()]
        public void testDeckInitializes() 
        {
            Assert.NotNull(d);
        }

        [Test()]
        public void testGetSize()
        {
            Assert.AreEqual(10, d.cardsLeft());
            Deck de = new Deck(new List<Card>());
            Assert.AreEqual(0, de.cardsLeft());
        }

        [Test()]
        public void testDraw()
        {
            Card drawn = d.draw();
            Assert.NotNull(drawn);
            Assert.AreEqual(9, d.cardsLeft());
        }
        [Test()]
        public void testDrawAll()
        {
            for (int i = 0; i < 11; i++)
            {
                d.draw();
            }
            Assert.AreEqual(0, d.cardsLeft());
        }
    }
}
