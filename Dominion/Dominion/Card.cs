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
        string cardName;
        string desc;
        int cost;

        //Constructor
        public Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum,string cardName,string desc,int cost){
            this.type = type;
            this.cash = cash;
            this.buy = buy;
            this.actions = actions;
            this.vict = vict;
            this.draw = draw;
            this.funcNum = funcNum;
            this.cardName = cardName;
            this.desc = desc;
            this.cost = cost;
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

        public string toString()
        {
            return this.cardName;
        }

        public string getDescription()
        {
            return this.desc;
        }

        public string getName()
        {
            return this.cardName;
        }

        public int getCost()
        {
            return this.cost;
        }
    }
}
