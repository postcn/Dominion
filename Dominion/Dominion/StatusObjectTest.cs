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
    }
}