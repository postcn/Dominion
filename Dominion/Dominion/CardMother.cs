using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class CardMother
    {
        public static Card Smithy()
        {
            return new Card(2, 0, 0, 0, 0, 3, 1, "Smithy", "+3 Cards", 4);
        }

        public static Card Estate()
        {
            return new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2);
        }

        public static Card Duchy()
        {
            return new Card(0, 0, 0, 0, 3, 0, 0, "Duchy", "3 Victory Points", 5);
        }

        public static Card Province()
        {
            return new Card(0, 0, 0, 0, 6, 0, 0, "Province", "6 Victory Points", 8);
        }

        public static Card Copper()
        {
            return new Card(1, 1, 0, 0, 0, 0, 0, "Copper", "1 Currency Value", 0);
        }

        public static Card Silver()
        {
            return new Card(1, 2, 0, 0, 0, 0, 0, "Silver", "2 Currency Value", 3);
        }

        public static Card Gold()
        {
            return new Card(1, 3, 0, 0, 0, 0, 0, "Gold", "3 Currency Value", 6);
        }

        public static Card Laboratory()
        {
            return new Card(2, 0, 0, 1, 0, 2, 2, "Laboratory", "+2 Cards +1 Action", 5);
        }

        public static Card Market()
        {
            return new Card(2, 1, 1, 1, 0, 1, 3, "Market", "+1 Card +1 Action +1 Buy +1 Currency", 5);
        }
    }
}
