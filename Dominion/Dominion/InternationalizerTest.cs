using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class InternationalizerTest
    {
        [Test()]
        public void testGetAString()
        {
            Assert.AreEqual("This is the test string!", Internationalizer.getMessage("TestString"));
        }
    }
}
