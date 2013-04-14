using System;
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
    }
}
