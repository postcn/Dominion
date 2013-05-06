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
        Boolean deckDiscardCorrectly;
        Boolean minedCorrectly;
        Boolean discardMultipleToDrawSame;
        Boolean discardedAndDrawn;
        Boolean trashCards;
        Boolean discardDeck;
        Boolean trashACopperForCurrency;
        Boolean copperTrashedSuccessfullyForBonusCurrency;
        Boolean mineTreasure;
        Boolean militiaPlayed;
        Boolean continueWithDelayed;
        Boolean canSpy;
        Boolean spiedSuccessfully;
        Boolean trashThiefedCards;
        Boolean keepTrashedThiefedCards;
        String message;

        public StatusObject(Boolean played)
        {
            this.played = played;
            this.trashForGain = false;
            this.gainedCorrectly = false;
            this.trashedCorrectly = false;
            this.message = "";
            this.discardMultipleToDrawSame = false;
            this.discardedAndDrawn = false;
            this.trashACopperForCurrency = false;
            this.copperTrashedSuccessfullyForBonusCurrency = false;
            this.trashCards = false;
            this.mineTreasure = false;
            this.minedCorrectly = false;
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

        public void setDiscardCardsToDrawSameNumber(Boolean val)
        {
            this.discardMultipleToDrawSame = val;
        }

        public Boolean needToDiscardCardsFromHandToDrawSameNumber()
        {
            return this.discardMultipleToDrawSame;
        }

        public void setDiscardedAndDrawn(Boolean val)
        {
            this.discardedAndDrawn = val;
        }

        public Boolean wasDiscardedAndDrawnSuccessfully()
        {
            return this.discardedAndDrawn;
        }

        public void setTrashACopperForCurrency(Boolean val)
        {
            this.trashACopperForCurrency = val;
        }

        public Boolean needToTrashCoppersForCurrency()
        {
            return this.trashACopperForCurrency;
        }

        public void setCopperTrashedForCurrency(Boolean val)
        {
            this.copperTrashedSuccessfullyForBonusCurrency = val;
        }

        public Boolean wasCopperTrashedSuccessfullyForBonus()
        {
            return this.copperTrashedSuccessfullyForBonusCurrency;
        }

        public void setTrashCards(Boolean val)
        {
            this.trashCards = val;
        }

        public Boolean needToTrashCards()
        {
            return this.trashCards;
        }

        public void setDiscardDeck(Boolean val)
        {
            this.discardDeck = val;
        }

        public Boolean needToDisardDeck()
        {
            return this.discardDeck;
        }

        public void setDeckDiscardedCorrectly(Boolean val)
        {
            this.deckDiscardCorrectly = val;
        }

        public Boolean wasDeckDiscardedCorrectly()
        {
            return this.deckDiscardCorrectly;
        }

        public void setMinedCorrectly(Boolean val)
        {
           this.minedCorrectly = val;
        }

        public Boolean wasMinedCorrectly()
        {
            return this.minedCorrectly;
        }

        public void setMineTreasure(Boolean val)
        {
            this.mineTreasure = val;
        }

        public Boolean needToMine()
        {
            return this.mineTreasure;
        }

        public void setMilitiaPlayed(Boolean val)
        {
            this.militiaPlayed = val;
        }

        public Boolean wasMilitiaPlayed()
        {
            return this.militiaPlayed;
        }

        public void setContinueWithDelayedFunctions(Boolean val)
        {
            this.continueWithDelayed = val;
        }

        public Boolean needToContinueWithDelayedFunctions()
        {
            return this.continueWithDelayed;
        }

        public void setCanSpy(Boolean val)
        {
            this.canSpy = val;
        }

        public Boolean canSpyOnDeck()
        {
            return this.canSpy;
        }

        public void setSpiedSuccessfully(Boolean val)
        {
            this.spiedSuccessfully = val;
        }

        public Boolean playerSpiedSuccessfully()
        {
            return this.spiedSuccessfully;
        }

        public void setSelectTrashFromThief(Boolean val)
        {
            this.trashThiefedCards = val;
        }

        public void setKeepTrashedFromThief(Boolean val)
        {
            this.keepTrashedThiefedCards = val;
        }

        public Boolean needToKeepThief()
        {
            return this.keepTrashedThiefedCards;
        }

        public Boolean selectTrashFromThief()
        {
            return this.trashThiefedCards;
        }
    }
}
