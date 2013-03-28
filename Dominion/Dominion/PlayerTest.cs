using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dominion
{
    [TestFixture()]
    class PlayerTest
    {
        Player p;

        [SetUp()]
        public void setUp()
        {
            this.p = new Player(0);
        }
    }
}
