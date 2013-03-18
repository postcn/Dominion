using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Card
    {
        /*
         * For type, 0 is a victory, 1 is a currency, and 2 is an action
         */
        //Instance Variables
        int type;
        int cash;
        int buy;
        int actions;
        int vict;
        int draw;
        int funcNum;

        //Constructor
        public Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum){
            this.type = type;
            this.cash = cash;
            this.buy = buy;
            this.actions = actions;
            this.vict = vict;
            this.draw = draw;
            this.funcNum = funcNum;
        }

        public int getType()
        {
            return this.type;
        }

        public int getCash()
        {
            return this.cash;
        }

        public int getBuy()
        {
            return this.buy;
        }

        public int getActions()
        {
            return this.actions;
        }

        public int getVictoryPoints()
        {
            return this.vict;
        }

        public int getAdditionalDraws()
        {
            return this.draw;
        }

        public int getFunctionNumber()
        {
            return this.funcNum;
        }
    }
}
