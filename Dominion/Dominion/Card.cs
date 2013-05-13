using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [Serializable]
    public class Card
    {
        /*
         * For type, 0 is a victory, 1 is a currency, and 2 is an action, 3 is an action-attack, and 4 is an action-reaction
         */
        //Instance Variables
        int type;
        int cash;
        int buy;
        int actions;
        int vict;
        int draw;
        /*
         * For funcNum the following are defined
         * 1: drawing only. Such as the smithy card.
         * 2: is drawing and action, such as the laboratory card
         * 3: is drawing and action and buys, such as the market card
         * 4: is extra currency and buys, woodcutter
         * 5: is extra actions and cards, village
         * 6: is extra actions, buys, and currency, Festival
         * 7: is witch, curse gain and draw
         * 8: is Remodel
         */
        int funcNum;
        string cardName;
        string desc;
        int cost;

        public long equalValidator;

        //Constructor
        public Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum,string cardName,string desc,int cost){
            this.equalValidator = 0;

            this.type = type;
            this.equalValidator += type;
            this.cash = cash;
            this.equalValidator += cash * 100;
            this.buy = buy;
            this.equalValidator += buy * 10000;
            this.actions = actions;
            this.equalValidator += actions * 1000000;
            this.vict = vict;
            this.equalValidator += vict * 100000000;
            this.draw = draw;
            this.equalValidator += draw * 10000000000;
            this.cost = cost;
            this.equalValidator += cost * 1000000000000;
            this.cardName = cardName;
            this.equalValidator += cardName.GetHashCode();
            this.desc = desc;
            this.equalValidator += desc.GetHashCode();
            this.funcNum = funcNum;
            this.equalValidator += funcNum * 100000000000000;
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

        override
        public bool Equals(Object other)
        {
            if (other.GetType() != this.GetType())
            {
                return false;
            }
            Card otherCard = (Card)other;
            /* return ((this.actions == otherCard.getActions()) && (this.desc == otherCard.getDescription()) && (this.cardName == otherCard.getName()) &&
                 (this.type == otherCard.getType()) && (this.funcNum == otherCard.getFunctionNumber()) && (this.cost == otherCard.getCost()) &&
                 (this.cash == otherCard.getCash()) && (this.buy == otherCard.getBuy()) && (this.vict == otherCard.getVictoryPoints()) &&
                 (this.draw == otherCard.getAdditionalDraws()));*/
            return this.equalValidator == otherCard.equalValidator;
        }

        public bool getPlayable()
        {
            return this.type == 2 || this.type == 3 || this.type == 4;
        }

        public String InternationlizedTypeString()
        {
            return Internationalizer.getMessage("TypeString" + this.type);
        }
    }
}
