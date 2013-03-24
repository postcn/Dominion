using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class CardStackTest
    {
        CardStack stackDefault;
        CardStack stackFive;
        CardStack stackOne;

        [SetUp()]
        public void setUp()
        {
            this.stackDefault = new CardStack(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2));
            this.stackFive = new CardStack(5, new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2));
            this.stackOne = new CardStack(1, new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2));
        }

        [Test()]
        public void testInitializes()
        {
            Assert.NotNull(this.stackOne);
            Assert.NotNull(this.stackDefault);
            Assert.NotNull(this.stackFive);
        }

        [Test()]
        public void testGetRemainingNoDraw()
        {
            Assert.AreEqual(10, this.stackDefault.cardsRemaining());
            Assert.AreEqual(5, this.stackFive.cardsRemaining());
            Assert.AreEqual(1, this.stackOne.cardsRemaining());
        }

        [Test()]
        public void testBuyOneCard()
        {
            Card drawn = this.stackOne.buyOne();
            Assert.AreEqual(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2), drawn);
            Assert.AreEqual(0, this.stackOne.cardsRemaining());
            drawn = this.stackFive.buyOne();
            Assert.AreEqual(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2), drawn);
            Assert.AreEqual(4, this.stackFive.cardsRemaining());
            drawn = this.stackDefault.buyOne();
            Assert.AreEqual(new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2), drawn);
            Assert.AreEqual(9, this.stackDefault.cardsRemaining());
        }

        [Test()]
        public void testEmpty()
        {
            CardStack s = new CardStack(0, new Card(0, 0, 0, 0, 0, 0, 0, "Null", "Null", 0));
            Assert.True(s.isEmpty());
            s = new CardStack(1, new Card(0, 0, 0, 0, 0, 0, 0, "Null", "Null", 0));
            Assert.False(s.isEmpty());
        }

        [Test()]
        public void drawAll()
        {
            int rem = this.stackDefault.cardsRemaining();
            Card test = null;
            for (int i = 0; i < rem; i++)
            {
                test = this.stackDefault.buyOne();
            }
            Assert.True(this.stackDefault.isEmpty());
            Assert.NotNull(test);
            rem = this.stackOne.cardsRemaining();
            for (int i = 0; i < rem; i++)
            {
                test = this.stackOne.buyOne();
            }
            Assert.True(this.stackOne.isEmpty());
            Assert.NotNull(test);
            rem = this.stackFive.cardsRemaining();
            for (int i = 0; i < rem; i++)
            {
                test = this.stackFive.buyOne();
            }
            Assert.True(this.stackFive.isEmpty());
            Assert.NotNull(test);
        }

        [Test()]
        public void drawMoreThanAll()
        {
            int rem = this.stackDefault.cardsRemaining();
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, null, null, 0);
            for (int i = 0; i <= rem; i++)
            {
                test = this.stackDefault.buyOne();
            }
            Assert.True(this.stackDefault.isEmpty());
            Assert.Null(test);
            rem = this.stackOne.cardsRemaining();
            for (int i = 0; i <= rem; i++)
            {
                test = this.stackOne.buyOne();
            }
            Assert.True(this.stackOne.isEmpty());
            Assert.Null(test);
            rem = this.stackFive.cardsRemaining();
            for (int i = 0; i <= rem; i++)
            {
                test = this.stackFive.buyOne();
            }
            Assert.True(this.stackFive.isEmpty());
            Assert.Null(test);
        }
    }
}
