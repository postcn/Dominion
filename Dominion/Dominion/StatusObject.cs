using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class StatusObject
    {
        Boolean played;

        public StatusObject(Boolean played)
        {
            this.played = played;
        }

        public void setPlayed(Boolean val)
        {
            this.played = val;
        }

        public Boolean wasPlayedProperly()
        {
            return this.played;
        }
    }
}
