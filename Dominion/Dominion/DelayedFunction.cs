using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [Serializable]
    public class DelayedFunction
    {
        Player p;
        int funcNum;

        public DelayedFunction(Player p, int functionNumber)
        {
            this.p = p;
            this.funcNum = functionNumber;
        }

        public StatusObject performAction()
        {
            StatusObject retVal = new StatusObject(false);
            switch (this.funcNum)
            {
                case 0: //draw a card
                    CardFunctions.draw(p, 1);
                    break;
                case 1: //Militia
                    retVal.setMilitiaPlayed(true);
                    retVal.setContinueWithDelayedFunctions(true);
                    retVal.setMessage(Internationalizer.getMessage("NeedMilitia"));
                    break;
            }
            return retVal;
        }
    }
}
