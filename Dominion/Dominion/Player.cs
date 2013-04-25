using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class Player
    {
        private int victoryPts;
        private int id;
        Deck myDeck;
        Hand myHand;
        List<int> timesPlayed;
        List<Card> played;
        int buysLeft;
        int currencyAvailable;
        int actionsLeft;
        String name;
        Game game;
        Boolean gain;
        int gainsLeft;
        int currencyForGain;
        int currencyForGainBonus;
        int bonusCurrencyForBuy;

        Boolean playMultipleTimes;
        int timesToPlayLeft;
        int timesToPlayNextCard;

        Card lastPlayedCard;

        int trashesNeeded;
        int trashCurrencyBonus;
        int possibleTrashes;


        public Player(int id)
        {
            this.gain = false;
            this.currencyForGain = 0;
            this.currencyForGainBonus = 0;
            this.gainsLeft = 0;
            this.playMultipleTimes = false;
            this.timesToPlayLeft = 1;
            this.timesToPlayNextCard = 1;
            this.id = id;
            myDeck = new Deck();
            myHand = new Hand();
            for (int i = 0; i < 5; i++)
            {
                myHand.draw(myDeck);
            }
            timesPlayed = new List<int>();
            played = new List<Card>();
            victoryPts = 3;
            this.buysLeft = 1;
            this.currencyAvailable = 0;
            this.actionsLeft = 1;
            this.name = null;
            this.game = null;
            this.lastPlayedCard = null;
            this.trashesNeeded = 0;
            this.trashCurrencyBonus = 0;
            this.bonusCurrencyForBuy = 0;
            this.possibleTrashes = 0;
        }
        public Hand getHand()
        {
            return this.myHand;
        }

        public Deck getDeck()
        {
            return this.myDeck;
        }

        public List<Card> getPlayed()
        {
            return this.played;
        }

        public List<int> getTimesPlayed()
        {
            return this.timesPlayed;
        }

        public void setDeck(Deck deck)
        {
            this.myDeck = deck;
        }

        public int getID()
        {
            return this.id;
        }

        public int getVictoryPts()
        {
            return victoryPts;
        }

        public int getBuysLeft()
        {
            return this.buysLeft;
        }

        public void setVictoryPts()
        {
            int tempPts = 0;
            List<Card> tempDeck = myDeck.getInDeck();
            for (int i = 0; i < tempDeck.Count; i++)
            {
                if (tempDeck[i].getType() == 0)
                {
                    tempPts = tempPts + tempDeck[i].getVictoryPoints();
                }
            }
            List<Card> tempDiscard = myDeck.getInDiscard();
            for (int i = 0; i < tempDiscard.Count; i++)
            {
                if (tempDiscard[i].getType() == 0)
                {
                    tempPts = tempPts + tempDiscard[i].getVictoryPoints();
                }
            }
            List<Card> tempHand = myHand.getHand();
            for (int i = 0; i < tempHand.Count; i++)
            {
                if (tempHand[i].getType() == 0)
                {
                    tempPts = tempPts + tempHand[i].getVictoryPoints();
                }
            }
            victoryPts = tempPts;
        }

        public int getCurrency()
        {
            int inPlayed = 0;
            for (int i=0; i<this.played.Count; i++)
            {
                inPlayed += this.played[i].getCash() * this.timesPlayed[i];
            }
            inPlayed += this.myHand.getCurrency();
            inPlayed += this.bonusCurrencyForBuy;
            this.currencyAvailable = inPlayed;
            return inPlayed;
        }

        public int getCurrencyValue()
        {
            return this.currencyAvailable;
        }

        public Boolean buy(CardStack aStack)
        {
            this.bonusCurrencyForBuy = 0;//reset it at the buy. This allows the bonus to be kept through multiple getCurrency calls
            int cost = aStack.getCard().getCost();
            int currency = this.currencyAvailable;
            if (!(aStack.isEmpty()) && (cost <= currency) && (this.buysLeft > 0))
            {
                this.myDeck.discard(aStack.buyOne());
                this.buysLeft--;
                this.currencyAvailable -= aStack.getCard().getCost();
                return true;
            }
            return false;
        }

        public StatusObject play(Card aCard)
        {
            StatusObject retVal = new StatusObject(false);
            if (this.myHand.contains(aCard) && aCard.getType() == 2 && this.actionsLeft > 0)
            {
                this.actionsLeft--;
                this.played.Add(this.myHand.remove(aCard));
                this.timesToPlayLeft = this.timesToPlayNextCard;
                this.timesPlayed.Add(this.timesToPlayLeft);
                this.timesToPlayNextCard = 1; // we just set it to use up those plays.
                this.lastPlayedCard = aCard;
                if (timesToPlayLeft > 1)
                {
                    this.playMultipleTimes = false;
                }
                while (this.timesToPlayLeft > 0)
                {
                    this.timesToPlayLeft--;
                    switch (aCard.getFunctionNumber())
                    {
                        case 0:
                            //No Action
                            break;
                        case 1:
                            //Draw only
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            break;
                        case 2:
                            //Draw and Add Actions.
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 3:
                            //Draw and Add and Buy
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 4:
                            //Add buy
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 5:
                            //Add actions and draw
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 6:
                            //Add actions and buy
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 7:
                            //add cards and gain curses.
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.gainCurses(this);
                            break;
                        case 8:
                            //Remodel a card, trash and gain
                            CardFunctions.gainCardRemodel(this, retVal);
                            break;
                        case 9:
                            //Feast, trash and gain
                            CardFunctions.gainCardFeast(this, retVal);
                            break;
                        case 10:
                            //Workshop, gain card worth 4
                            CardFunctions.gainCardWorkshop(this, retVal);
                            break;
                        case 11:
                            //Throne Room. Double the number of next plays.
                            CardFunctions.doubleNextPlay(this,this.timesToPlayLeft+1);
                            this.timesToPlayLeft = 0;
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 12:
                            //Cellar
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.setupDiscardCardsToDrawSameNumber(this, retVal);
                            break;
                        case 13:
                            //MoneyLender
                            CardFunctions.addNeededTrashes(this, retVal);
                            break;
                        case 14:
                            //Chapel
                            CardFunctions.trashUptoFourCards(this, retVal);
                            break;
                        case 15:
                            //Chancellor
                            CardFunctions.discardDeckChancellor(this, retVal);
                            break;
                    }
                }
                retVal.setPlayed(true);
            }
            return retVal;
        }

        public int addBuys(int toAdd)
        {
            this.buysLeft += toAdd;
            return this.buysLeft;
        }

        public int addActions(int toAdd)
        {
            this.actionsLeft += toAdd;
            return this.actionsLeft;
        }

        public int getActionsLeft()
        {
            return this.actionsLeft;
        }

        public void cleanUp()
        {
            foreach (Card c in myHand.getHand())
            {
                myDeck.discard(c);
            }
            foreach (Card c in this.played)
            {
                myDeck.discard(c);
            }
            myHand = new Hand();
            for (int i = 0; i < 5; i++)
            {
                myHand.draw(myDeck);
            }
            this.buysLeft = 1;
            this.actionsLeft = 1;
            this.played = new List<Card>();
            this.timesPlayed = new List<int>();
            this.gain = false;
            this.gainsLeft = 0;
            this.playMultipleTimes = false;
            this.timesToPlayLeft = 1;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return this.name;
        }

        public void setGame(Game game)
        {
            this.game = game;
        }

        public Game getGame()
        {
            return this.game;
        }

        override
        public bool Equals(Object other)
        {
            if (other.GetType() != this.GetType())
            {
                return false;
            }
            Player otherPlayer = (Player)other;
            return (otherPlayer.id == this.id && otherPlayer.name == this.name);
        }

        public void setCurrencyForGainBonus(int bonus)
        {
            this.currencyForGainBonus = bonus;
        }

        public void setCurrencyForGain(int currency)
        {
            this.currencyForGain = currency;
        }

        public int getCurrencyForGainBonus()
        {
            return this.currencyForGainBonus;
        }

        public int getCurrencyForGain()
        {
            return this.currencyForGain;
        }

        public Boolean getGain()
        {
            return this.gain;
        }

        public void setGain(Boolean val)
        {
            this.gain = val;
        }

        public int getGainsLeft()
        {
            return this.gainsLeft;
        }

        public void setGainsLeft(int val)
        {
            this.gainsLeft = val;
        }

        public StatusObject trashForGain(Card c)
        {
            StatusObject o = new StatusObject(false);
            if (this.gain)
            {
                //check if card is in hand or if its feast (which has already been played)
                if (this.myHand.contains(c))
                {
                    this.myHand.remove(c);//don't put it anywhere so trashed
                    this.currencyForGain = c.getCost() + this.currencyForGainBonus;
                    if (this.gainsLeft <= 0)
                    {
                        this.currencyForGainBonus = 0;
                    }
                    o.setTrashedCorrectly(true);
                }
            }
            return o;
        }

        public StatusObject gainCard(CardStack cs)
        {
            StatusObject o = new StatusObject(false);
            if (cs.isEmpty())
            {
                o.setMessage("Card Stack was Empty");
                return o;
            }
            if (!this.gain)
            {
                o.setMessage("Not gain in player");
                return o;
            }
            if (this.gainsLeft <= 0)
            {
                o.setMessage("No gains left in player");
                return o;
            }
            if (this.currencyForGain >= cs.getCard().getCost())
            {
                this.getDeck().discard(cs.buyOne());
                this.gainsLeft--;
                if (this.gainsLeft == 0)
                {
                    this.currencyForGain = 0;
                    this.gain = false;
                }
                else
                {
                    if (this.lastPlayedCard.Equals(CardMother.Remodel()))
                    {
                        o.setMessage("Was a remodel. Setting it to trash the next card.");
                        o.setTrashForGain(true);
                    }
                    else
                    {
                        o.setMessage("Still have gains left, setting to gain again.");
                        o.setTrashedCorrectly(true);
                    }
                }
                o.setGainedProperly(true);
            }
            else
            {
                o.setMessage("Not enough currency for gain. " + this.currencyForGain);
            }
            return o;
        }

        public Boolean getPlayMultipleTimes()
        {
            return this.playMultipleTimes;
        }

        public void setPlayMultipleTimes(Boolean val)
        {
            this.playMultipleTimes = val;
        }

        public int getPlaysOfNextCardLeft()
        {
            return this.timesToPlayNextCard;
        }

        public void setTimesToPlayNextCard(int val)
        {
            this.timesToPlayNextCard = val;
        }

        public Card getLastPlayedCard()
        {
            return this.lastPlayedCard;
        }

        public StatusObject discardCardsAndDrawSameAmount(List<Card> cards)
        {
            StatusObject retVal = new StatusObject(false);
            //check if there are actually no cards in the list. Skip the rest of the code  if there aren't any.

            if (cards.Count == 0)
            {
                retVal.setDiscardedAndDrawn(true);
                return retVal;
            }

            List<Card> handCopy = new List<Card>();
            //make a copy of the hand so that we can check if all the cards in the list are in the hand
            foreach (Card c in this.getHand().getHand())
            {
                handCopy.Add(c);
            }

            foreach (Card c in cards)
            {
                if (!handCopy.Remove(c))
                {
                    retVal.setMessage("Missing one or more " + c.getName() + " from hand.");
                    return retVal;
                }
            }

            //if it gets to here all the cards are in the hand. Proceed to remove them and draw the same number.
            //now we can modify the player's actual hand
            Hand h = this.getHand();
            Deck d = this.getDeck();
            foreach (Card c in cards)
            {
                h.discard(c, d);
            }

            //now we discarded all of the cards, draw the number that we need
            CardFunctions.draw(this, cards.Count);
            retVal.setDiscardedAndDrawn(true);

            return retVal;
        }

        public StatusObject trashCards(List<Card> cards)
        {
            StatusObject retVal = new StatusObject(false);
            //If none then didnt want to discard any
            if (cards.Count == 0)
            {
                retVal.setTrashedCorrectly(true);
                return retVal;
            }

            if (cards.Count > this.possibleTrashes)
            {
                retVal.setMessage("More than 4 cards selected!");
                return retVal;
            }

            List<Card> handCopy = new List<Card>();

            foreach (Card c in this.getHand().getHand())
            {
                handCopy.Add(c);
            }
            //Check that cards are in the hand
            foreach (Card c in cards)
            {
                if (!handCopy.Remove(c))
                {
                    retVal.setMessage("Missing one or more " + c.getName() + " from hand.");
                    return retVal;
                }
            }
            //Trash cards
            Hand h = this.getHand();
            Deck d = this.getDeck();
            foreach (Card c in cards)
            {
                h.remove(c);//trash not discard for the chapel
            }

            this.possibleTrashes = 0;
            retVal.setTrashedCorrectly(true);

            return retVal;
        }

        public StatusObject discardDeck(Boolean discard)
        {
            StatusObject retVal = new StatusObject(false);

            if (discard == false)
            {
                retVal.setDeckDiscardedCorrectly(true);
                return retVal;
            }
            int size = this.getDeck().getInDeck().Count;
            Deck d = this.getDeck();
            //put deck into discard
            for (int i = 0; i < size; i++)
            {
                d.getInDiscard().Add(d.getInDeck().ElementAt(0));
                d.getInDeck().RemoveAt(0);
            }
            retVal.setDeckDiscardedCorrectly(true);
            return retVal;
        }

        //Should only be used when dealing with the moneylender
        public int getTrashesNeeded()
        {
            return this.trashesNeeded;
        }

        public void setTrashesNeeded(int val)
        {
            this.trashesNeeded = val;
        }

        public void setTrashCurrencyBonus(int val)
        {
            this.trashCurrencyBonus = val;
        }

        public int getTrashCurrencyBonus()
        {
            return this.trashCurrencyBonus;
        }

        //Passing in null counts as it being false, don't want to trash because they have the option
        public StatusObject trashACopperForCurrencyBonus(Card aCard)
        {
            StatusObject retVal = new StatusObject(false);
            if (this.trashesNeeded <= 0)
            {
                this.trashesNeeded = 0;
                retVal.setCopperTrashedForCurrency(true);
                return retVal;
            }
            if (aCard == null)
            {
                retVal.setCopperTrashedForCurrency(true);
                return retVal;
            }
            if (!aCard.Equals(CardMother.Copper()))
            {
                return retVal;
            }

            if (!this.getHand().contains(aCard))
            {
                return retVal;
            }

            this.getHand().remove(aCard);
            this.bonusCurrencyForBuy += this.trashCurrencyBonus;
            this.trashesNeeded--;
            if (trashesNeeded == 0)
            {
                retVal.setCopperTrashedForCurrency(true);
            }
            else
            {
                retVal.setTrashACopperForCurrency(true);
            }
            return retVal;
        }

        public void setPossibleTrashes(int val)
        {
            this.possibleTrashes = val;
        }

        public int getPossibleTrashes()
        {
            return this.possibleTrashes;
        }
    }
}
