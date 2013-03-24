using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

//This comment tests that Nathan can edit this .cs file.

namespace Dominion
{   
    [TestFixture()]
    class CardTest
    {
        [Test()]
        public void testInintializes() 
        {
            Card test = new Card(0,1,0,0,0,0,0,"Copper","Single valued Currency card",0);
            Assert.NotNull(test);
        }

        [Test()]
        public void testGetType()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0,"Null Card","Null Card",0);
            Assert.AreEqual(0, test.getType());
            test = new Card(1, 0, 0, 0, 0, 0, 0,"Null Card","Null Card",0);
            Assert.AreEqual(1, test.getType());
            test = new Card(99, 0, 0, 0, 0, 0, 0,"Null Card","Null Card",0);
            Assert.AreEqual(99, test.getType());
        }

        [Test()]
        public void testGetCash()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getCash());
            test = new Card(0, 1, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getCash());
            test = new Card(99, 99, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getType());
        }

        [Test()]
        public void testGetBuy()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getBuy());
            test = new Card(1, 0, 1, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getBuy());
            test = new Card(5, 0, 99, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getBuy());
        }

        [Test()]
        public void testGetActions()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getActions());
            test = new Card(1, 0, 0, 1, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getActions());
            test = new Card(3, 0, 0, 99, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getActions());
        }

        [Test()]
        public void testGetVictoryPoints()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getVictoryPoints());
            test = new Card(0, 0, 0, 0, 1, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getVictoryPoints());
            test = new Card(3, 0, 0, 0, 99, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getVictoryPoints());
        }

        [Test()]
        public void testGetDraws()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getAdditionalDraws());
            test = new Card(0, 0, 0, 0, 0, 1, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getAdditionalDraws());
            test = new Card(3, 0, 0, 0, 4, 99, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getAdditionalDraws());
        }

        [Test()]
        public void testFunctionNumber()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getFunctionNumber());
            test = new Card(0, 0, 0, 0, 1, 0, 1, "Null Card", "Null Card",0);
            Assert.AreEqual(1, test.getFunctionNumber());
            test = new Card(3, 0, 2, 0, 99, 4, 99, "Null Card", "Null Card",0);
            Assert.AreEqual(99, test.getFunctionNumber());
        }

        [Test()]
        public void testToString()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "Null Card",0);
            Assert.AreEqual("Card Name", test.toString());
        }

        [Test()]
        public void testGetName()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "Null Card",0);
            Assert.AreEqual("Card Name", test.getName());
        }

        [Test()]
        public void testGetDescription()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "This is the card description",0);
            Assert.AreEqual("This is the card description", test.getDescription());
        }

        [Test()]
        public void testGetCost()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Null Card", "Null Card",0);
            Assert.AreEqual(0, test.getCost());
            test = new Card(0, 0, 0, 0, 1, 0, 1, "Null Card", "Null Card",1);
            Assert.AreEqual(1, test.getCost());
            test = new Card(3, 0, 2, 0, 99, 4, 99, "Null Card", "Null Card",99);
            Assert.AreEqual(99, test.getCost());
        }
    }
}
