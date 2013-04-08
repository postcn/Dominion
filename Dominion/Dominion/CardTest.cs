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
        /// <summary>
        /// Tests that a card successfully initializes
        /// </summary>
        [Test()]
        public void testInintializes() 
        {
            Card test = new Card(0,1,0,0,0,0,0,"Copper","Single valued Currency card",0);
            Assert.NotNull(test);
        }

        /// <summary>
        /// Tests that we can get the type of a card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the monetary value from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the buy value from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the action value from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the victory point value from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the draw value from the card succesfully
        /// </summary>
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

        /// <summary>
        /// Tests that we can get the function number value from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests the toString function for the card, same functionality as get name.
        /// </summary>
        [Test()]
        public void testToString()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "Null Card",0);
            Assert.AreEqual("Card Name", test.toString());
        }

        /// <summary>
        /// Tests the get name function for the card, same functionality as toString
        /// </summary>
        [Test()]
        public void testGetName()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "Null Card",0);
            Assert.AreEqual("Card Name", test.getName());
        }

        /// <summary>
        /// Tests that we can get the description from the card successfully
        /// </summary>
        [Test()]
        public void testGetDescription()
        {
            Card test = new Card(0, 0, 0, 0, 0, 0, 0, "Card Name", "This is the card description",0);
            Assert.AreEqual("This is the card description", test.getDescription());
        }

        /// <summary>
        /// Tests that we can get cost from the card successfully
        /// </summary>
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

        /// <summary>
        /// Tests that the equals of the card works properly
        /// </summary>
        [Test()]
        public void testEquals()
        {
            Card test = new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2);
            Card estate = new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2);
            Object generic = new Object();
            Assert.True(test.Equals(estate));
            Assert.False(test.Equals(generic));
            Card oneoff = new Card(1, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2);
            oneoff = new Card(0, 1, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 1, 0, 1, 0, 0, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 1, 1, 0, 0, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 0, 0, 0, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 1, 1, 0, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 1, 0, 1, "Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 1, 0, 0, "Not Estate", "Single Victory Point", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Changed Description", 2);
            Assert.False(test.Equals(oneoff));
            oneoff = new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "Single Victory Point", 0);
            Assert.False(test.Equals(oneoff));
        }
    }
}
