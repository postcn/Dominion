using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class LocaleTest
    {
        [Test()]
        public void testInstantiate()
        {
            Locale l = new Locale("en", "US");
            Assert.IsNotNull(l);
        }

        [Test()]
        public void testGetExtension()
        {
            Locale l = new Locale("en", "US");
            Assert.AreEqual("_en_US", l.getExtension());
        }
    }
}
