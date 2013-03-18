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
        int action;
        int vict;
        int draw;
        int funcNum;

        //Constructor
        Card(int type, int cash, int buy, int action, int vict, int draw, int funcNum)
        {
            this.type = type;
            this.cash = cash;
            this.buy = buy;
            this.action = action;
            this.vict = vict;
            this.draw = draw;
            this.funcNum = funcNum;

        }
    }
}
