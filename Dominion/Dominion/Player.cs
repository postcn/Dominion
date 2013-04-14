using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Player
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

        public Player(int id)
        {
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
    }
}
