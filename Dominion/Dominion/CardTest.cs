using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{   
    [TestFixture()]
    class CardTest
    {
        [Test()]
        public void testInintializes() 
        {
            Card test = new Card(0,1,0,0,0,0);
            Assert.NotNull(test);
        }
    }
}
