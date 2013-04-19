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
        List<Card> played;
        int buysLeft;
        int currencyAvailable;
        int actionsLeft;
        String name;
        Game game;
        Boolean gain;
        int currencyForGain;
        int currencyForGainBonus;

        public Player(int id)
        {
            this.gain = false;
            this.currencyForGain = 0;
            this.currencyForGainBonus = 0;
            this.id = id;
            myDeck = new Deck();
            myHand = new Hand();
            for (int i = 0; i < 5; i++)
            {
                myHand.draw(myDeck);
            }
            played = new List<Card>();
            victoryPts = 3;
            this.buysLeft = 1;
            this.currencyAvailable = 0;
            this.actionsLeft = 1;
            this.name = null;
            this.game = null;
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
            foreach (Card c in this.played)
            {
                inPlayed += c.getCash();
            }
            inPlayed += this.myHand.getCurrency();
            this.currencyAvailable = inPlayed;
            return inPlayed;
        }

        public int getCurrencyValue()
        {
            return this.currencyAvailable;
        }

        public Boolean buy(CardStack aStack)
        {
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
                    this.currencyForGainBonus = 0;
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
                return o;
            }
            if (!this.gain)
            {
                return o;
            }
            if (this.currencyForGain >= cs.getCard().getCost())
            {
                this.getDeck().discard(cs.buyOne());
                this.currencyForGain = 0;
                this.gain = false;
                o.setGainedProperly(true);
            }
            return o;
        }
    }
}
