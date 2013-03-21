using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [TestFixture()]
    class HandTest
    {
        [Test()]
        public void testInintializes()
        {
            Hand test = new Hand();
            Assert.NotNull(test);
        }
    }
}
