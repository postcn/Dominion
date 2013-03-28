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
        }
        public Hand getHand()
        {
            return this.myHand;
        }

        public Deck getDeck()
        {
            return this.myDeck;
        }

        public int getID()
        {
            return this.id;
        }

        public int getVictoryPts()
        {
            return victoryPts;
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
            if (!(aStack.isEmpty()) && (cost <= currency))
            {
                this.myDeck.discard(aStack.buyOne());
                this.buysLeft--;
                this.currencyAvailable -= aStack.getCard().getCost();
                return true;
            }
            return false;
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
        }
    }
}
