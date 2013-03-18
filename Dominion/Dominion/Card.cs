using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Cards
    {
        //Instance Variables
        int type;
        int cash;
        int buy;
        int actions;
        int vict;
        int draw;
        int funcNum;

        //Constructor
        Cards(int type, int cash, int buy, int actions, int vict, int draw, int funcNum){
            this.type = type;
            this.cash = cash;
            this.buy = buy;
            this.actions = actions;
            this.vict = vict;
            this.draw = draw;
            this.funcNum = funcNum;
        }
    }
}
