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
        Boolean trashForGain;
        Boolean gainedCorrectly;
        Boolean trashedCorrectly;
        String message;

        public StatusObject(Boolean played)
        {
            this.played = played;
            this.trashForGain = false;
            this.gainedCorrectly = false;
            this.trashedCorrectly = false;
            this.message = "No Message";
        }

        public void setPlayed(Boolean val)
        {
            this.played = val;
        }

        public Boolean wasPlayedProperly()
        {
            return this.played;
        }

        public void setTrashForGain(Boolean val)
        {
            this.trashForGain = val;
        }

        public Boolean trashForGainCheck()
        {
            return this.trashForGain;
        }

        public void setGainedProperly(Boolean val)
        {
            this.gainedCorrectly = val;
        }

        public Boolean getGainedProperly()
        {
            return this.gainedCorrectly;
        }

        public void setTrashedCorrectly(Boolean val)
        {
            this.trashedCorrectly = val;
        }

        public Boolean wasTrashedCorrectly()
        {
            return this.trashedCorrectly;
        }

        public void setMessage(String message)
        {
            this.message = message;
        }

        public String getMessage()
        {
            return this.message;
        }
    }
}
