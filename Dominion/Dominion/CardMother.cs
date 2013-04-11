﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    /*
         * For funcNum the following are defined
         * 1: drawing only. Such as the smithy card.
         * 2: is drawing and action, such as the laboratory card
         * 3: is drawing and action and buys, such as the market card
         */
    //Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum,string cardName,string desc,int cost)
    class CardMother
    {
        public static Card Festival()
        {
            return new Card(2, 2, 1, 2, 0, 0, 6, "Festival", "+2 Actions +1 Buy +2 Currency", 5);
        }

        public static Card Village()
        {
            return new Card(2, 0, 0, 2, 0, 1, 5, "Village", "+1 Card +2 Actions", 3);
        }

        public static Card Woodcutter()
        {
            return new Card(2, 2, 1, 0, 0, 0, 4, "Woodcutter", "+1 Buy +2 Coins", 3);
        }

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
