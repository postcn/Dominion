﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class StatusObjectTest
    {
        StatusObject o;

        [SetUp()]
        public void setUp()
        {
            this.o = new StatusObject(false);
        }

        [Test()]
        public void testWasPlayed()
        {
            Assert.IsFalse(o.wasPlayedProperly());
            o.setPlayed(true);
            Assert.IsTrue(o.wasPlayedProperly());
        }

        [Test()]
        public void testTrashForGain()
        {
            Assert.IsFalse(o.trashForGainCheck());
            o.setTrashForGain(true);
            Assert.IsTrue(o.trashForGainCheck());
        }

        [Test()]
        public void testGainedProperly()
        {
            Assert.IsFalse(o.getGainedProperly());
            o.setGainedProperly(true);
            Assert.IsTrue(o.getGainedProperly());
        }

        [Test()]
        public void testTrashedProperly()
        {
            Assert.IsFalse(o.wasTrashedCorrectly());
            o.setTrashedCorrectly(true);
            Assert.IsTrue(o.wasTrashedCorrectly());
        }

        [Test()]
        public void testMessage()
        {
            o.setMessage("Jeff likes candy.");
            Assert.AreEqual("Jeff likes candy.", o.getMessage());
        }

        [Test()]
        public void testDiscardCardsToDrawSameNumber()
        {
            Assert.IsFalse(o.needToDiscardCardsFromHandToDrawSameNumber());
            o.setDiscardCardsToDrawSameNumber(true);
            Assert.IsTrue(o.needToDiscardCardsFromHandToDrawSameNumber());
        }

        [Test()]
        public void testDiscardedAndDrawnSuccessfully()
        {
            Assert.IsFalse(o.wasDiscardedAndDrawnSuccessfully());
            o.setDiscardedAndDrawn(true);
            Assert.IsTrue(o.wasDiscardedAndDrawnSuccessfully());
        }

        [Test()]
        public void testTrashCoppersForCurrency()
        {
            Assert.IsFalse(o.needToTrashCoppersForCurrency());
            o.setTrashACopperForCurrency(true);
            Assert.IsTrue(o.needToTrashCoppersForCurrency());
        }

        [Test()]
        public void testCopperWasTrashedForBonus()
        {
            Assert.IsFalse(o.wasCopperTrashedSuccessfullyForBonus());
            o.setCopperTrashedForCurrency(true);
            Assert.IsTrue(o.wasCopperTrashedSuccessfullyForBonus());
        }

        [Test()]
        public void testMilitiaPlayed()
        {
            Assert.IsFalse(o.wasMilitiaPlayed());
            o.setMilitiaPlayed(true);
            Assert.IsTrue(o.wasMilitiaPlayed());
        }

        [Test()]
        public void testDelayedContinue()
        {
            Assert.IsFalse(o.needToContinueWithDelayedFunctions());
            o.setContinueWithDelayedFunctions(true);
            Assert.IsTrue(o.needToContinueWithDelayedFunctions());
        }
    }
}
