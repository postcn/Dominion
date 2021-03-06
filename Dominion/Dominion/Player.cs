﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [Serializable]
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

        List<Player> otherPlayers;

        public Queue<DelayedFunction> functionsToCall;

        List<List<Card>> thiefList;
        List<Card> thiefTrashed;

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
            myDeck.reshuffle();
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
            this.otherPlayers = new List<Player>();
            this.functionsToCall = new Queue<DelayedFunction>();
            this.thiefList = new List<List<Card>>();
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

        private int calculateCardCount()
        {
            return this.myDeck.size() + this.myHand.size();
        }

        public void setVictoryPts()
        {
            int tempPts = 0;
            int cardCount = this.calculateCardCount();
            tempPts += countVictoryPoints(myDeck.getInDeck());
            tempPts += countVictoryPoints(myDeck.getInDiscard());
            tempPts += countVictoryPoints(myHand.getHand());
            tempPts += countGardens() * (cardCount / 10);
            victoryPts = tempPts;
        }

        private int countVictoryPoints(List<Card> cards)
        {
            int vic = 0;
            foreach (Card c in cards)
            {
                if (c.getType() == 0)
                {
                    vic += c.getVictoryPoints();
                }
            }
            return vic;
        }

        private int countGardens()
        {
            int gardens = 0;
            foreach (Card c in this.myHand.getHand())
            {
                if (c.Equals(CardMother.Gardens()))
                {
                    gardens++;
                }
            }
            foreach (Card c in this.myDeck.getInDeck())
            {
                if (c.Equals(CardMother.Gardens()))
                {
                    gardens++;
                }
            }
            foreach (Card c in this.myDeck.getInDiscard())
            {
                if (c.Equals(CardMother.Gardens()))
                {
                    gardens++;
                }
            }
            return gardens;
        }

        public int getCurrency()
        {
            int inPlayed = 0;
            for (int i = 0; i < this.played.Count; i++)
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
                if (this.game != null)
                {
                    this.game.addToGameMessage(this.name + Internationalizer.getMessage("Bought") + aStack.getCard().getName());
                }
                return true;
            }
            return false;
        }

        public StatusObject play(Card aCard)
        {
            StatusObject retVal = new StatusObject(false);
            if (this.myHand.contains(aCard) && aCard.getPlayable() && this.actionsLeft > 0)
            {
                this.actionsLeft--;
                this.played.Add(this.myHand.remove(aCard));
                this.timesToPlayLeft = this.timesToPlayNextCard;
                this.timesPlayed.Add(this.timesToPlayLeft);
                this.timesToPlayNextCard = 1; // we just set it to use up those plays.
                this.lastPlayedCard = aCard;
                if (this.game != null)
                {
                    this.game.addToGameMessage(this.name + Internationalizer.getMessage("Played") + aCard.getName());
                }
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
                            retVal.setMessage(Internationalizer.getMessage("DrawMsg"));
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            break;
                        case 2:
                            //Draw and Add Actions.
                            retVal.setMessage(Internationalizer.getMessage("DrawActionMsg"));
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 3:
                            //Draw and Add and Buy
                            retVal.setMessage(Internationalizer.getMessage("DrawActionBuyMsg"));
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 4:
                            //Add buy
                            retVal.setMessage(Internationalizer.getMessage("BuyMsg"));
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 5:
                            //Add actions and draw
                            retVal.setMessage(Internationalizer.getMessage("DrawActionMsg"));
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 6:
                            //Add actions and buy
                            retVal.setMessage(Internationalizer.getMessage("ActionBuyMsg"));
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 7:
                            //add cards and gain curses.
                            retVal.setMessage(Internationalizer.getMessage("WitchMsg"));
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.gainCurses(this);
                            break;
                        case 8:
                            //Remodel a card, trash and gain
                            retVal.setMessage(Internationalizer.getMessage("RemodelMsg"));
                            CardFunctions.gainCardRemodel(this, retVal);
                            break;
                        case 9:
                            //Feast, trash and gain
                            retVal.setMessage(Internationalizer.getMessage("FeastMsg"));
                            CardFunctions.gainCardFeast(this, retVal);
                            break;
                        case 10:
                            //Workshop, gain card worth 4
                            retVal.setMessage(Internationalizer.getMessage("WorkshopMsg"));
                            CardFunctions.gainCardWorkshop(this, retVal);
                            break;
                        case 11:
                            //Throne Room. Double the number of next plays.
                            retVal.setMessage(Internationalizer.getMessage("ThroneRoomMsg"));
                            CardFunctions.doubleNextPlay(this, this.timesToPlayLeft + 1);
                            this.timesToPlayLeft = 0;
                            CardFunctions.actionAdd(this, aCard.getActions());
                            break;
                        case 12:
                            //Cellar
                            retVal.setMessage(Internationalizer.getMessage("CellarMsg"));
                            CardFunctions.actionAdd(this, aCard.getActions());
                            CardFunctions.setupDiscardCardsToDrawSameNumber(this, retVal);
                            break;
                        case 13:
                            //MoneyLender
                            retVal.setMessage(Internationalizer.getMessage("MoneylenderMsg"));
                            CardFunctions.addNeededTrashes(this, retVal);
                            break;
                        case 14:
                            //Chapel
                            retVal.setMessage(Internationalizer.getMessage("ChapelMsg"));
                            CardFunctions.trashUptoFourCards(this, retVal);
                            break;
                        case 15:
                            //Chancellor
                            retVal.setMessage(Internationalizer.getMessage("ChancellorMsg"));
                            CardFunctions.discardDeckChancellor(this, retVal);
                            break;
                        case 16:
                            //Militia
                            retVal.setMessage(Internationalizer.getMessage("MilitiaMsg"));
                            if (this.timesToPlayLeft == 0)
                            {
                                CardFunctions.militiaAction(this);
                            }
                            break;
                        case 17:
                            //Bureaucrat
                            retVal.setMessage(Internationalizer.getMessage("BureaucratMsg3"));
                            CardFunctions.bureaucratAction(this);
                            break;
                        case 18:
                            //Mine
                            retVal.setMessage(Internationalizer.getMessage("MineMsg"));
                            CardFunctions.mineATreasure(this, retVal);
                            break;
                        case 19:
                            //Council Room;
                            retVal.setMessage(Internationalizer.getMessage("CouncilRoomMsg"));
                            CardFunctions.councilRoomAction(this);
                            CardFunctions.draw(this, aCard.getAdditionalDraws());
                            CardFunctions.buyAdd(this, aCard.getBuy());
                            break;
                        case 20:
                            //Spy;
                            retVal.setMessage(Internationalizer.getMessage("SpyMsg"));
                            CardFunctions.spyFunction(this, retVal);
                            CardFunctions.draw(this, 1);
                            CardFunctions.actionAdd(this, 1);
                            break;
                        case 21:
                            //Thief
                            retVal.setMessage(Internationalizer.getMessage("ThiefMsg"));
                            CardFunctions.thiefAction(this, retVal);
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
            if (this.myHand.getHand().Count == 0)
            {
                o.setTrashedCorrectly(true);
                this.currencyForGainBonus = 0;
                this.gainsLeft = 0;
            }
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
                o.setMessage(Internationalizer.getMessage("StackEmpty"));
                return o;
            }
            if (!this.gain)
            {
                o.setMessage(Internationalizer.getMessage("NotGain"));
                return o;
            }
            if (this.gainsLeft <= 0)
            {
                o.setMessage(Internationalizer.getMessage("NoGainsLeft"));
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
                        o.setMessage(Internationalizer.getMessage("WasRemodel"));
                        o.setTrashForGain(true);
                    }
                    else
                    {
                        o.setMessage(Internationalizer.getMessage("GainsLeft"));
                        o.setTrashedCorrectly(true);
                    }
                }
                o.setGainedProperly(true);
            }
            else
            {
                o.setMessage(Internationalizer.getMessage("NotEnoughCur") + this.currencyForGain);
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

            StatusObject returner = allCardsInHand(cards, retVal);
            if (returner != null)
            {
                return returner;
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

        private StatusObject allCardsInHand(List<Card> cards, StatusObject retVal)
        {
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
                    retVal.setMessage(Internationalizer.getMessage("MissingCards1") + c.getName() + Internationalizer.getMessage("MissingCards2"));
                    return retVal;
                }
            }

            return null;
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
                retVal.setMessage(Internationalizer.getMessage("MoreThanFour"));
                return retVal;
            }

            StatusObject returner = allCardsInHand(cards, retVal);
            if (returner != null)
            {
                return returner;
            }

            //Trash cards
            foreach (Card c in cards)
            {
                this.getHand().remove(c);//trash not discard for the chapel
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
            if (this.trashesNeeded <= 0 || !this.myHand.contains(CardMother.Copper()))
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

        public void setOtherPlayerList()
        {
            if (this.game != null)
            {
                this.otherPlayers = new List<Player>();
                foreach (Player p in this.game.getPlayers())
                {
                    if (!p.Equals(this))
                    {
                        this.otherPlayers.Add(p);
                    }
                }
            }
        }

        public List<Player> getOtherPlayers()
        {
            return this.otherPlayers;
        }

        public StatusObject mineATreasureCard(Card c)
        {
            StatusObject retVal = new StatusObject(false);
            if (c == null)
            {
                return retVal;
            }
            if (this.trashesNeeded <= 0 || (!this.myHand.contains(CardMother.Copper()) && !this.myHand.contains(CardMother.Silver())))
            {
                retVal.setMinedCorrectly(true);
                this.trashesNeeded = 0;
                return retVal;
            }
            if (c.Equals(CardMother.Copper()) || c.Equals(CardMother.Silver()))
            {
                if (c.Equals(CardMother.Copper()))
                {
                    this.getHand().getHand().Remove(c);
                    this.getHand().getHand().Add(CardMother.Silver());
                    retVal.setMinedCorrectly(true);
                    this.trashesNeeded--;
                }

                else
                {
                    this.getHand().getHand().Remove(c);
                    this.getHand().getHand().Add(CardMother.Gold());
                    retVal.setMinedCorrectly(true);
                    this.trashesNeeded--;
                }
                if (this.trashesNeeded > 0)
                {
                    retVal.setMinedCorrectly(false);
                    retVal.setMineTreasure(true);
                }
                return retVal;
            }
            else
            {
                return retVal;
            }


        }

        /// <summary>
        /// The trashing effect for the militia card. Needs the lastObject so we know whether to continue with more functions or not afterwards
        /// in the delayed function calls.
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="lastObject"></param>
        /// <returns></returns>
        public StatusObject militiaDiscardEffect(List<Card> cards)
        {
            StatusObject lastObject = new StatusObject(false);
            lastObject.setMilitiaPlayed(true);
            lastObject.setContinueWithDelayedFunctions(true);
            if (this.myHand.size() <= 3)
            {
                lastObject.setMilitiaPlayed(false);
                return lastObject;
            }
            List<Card> handCopy = new List<Card>();
            foreach (Card c in this.myHand.getHand())
            {
                handCopy.Add(c);
            }

            foreach (Card c in cards)
            {
                if (!handCopy.Remove(c))
                {
                    lastObject.setMessage(Internationalizer.getMessage("MissingCards1") + c.getName() + Internationalizer.getMessage("MissingCards2"));
                    return lastObject;
                }
            }

            if (handCopy.Count != 3)
            {
                lastObject.setMessage(Internationalizer.getMessage("IncorrectForMilitia"));
                return lastObject;
            }

            //All the cards were in the hand and there were the correct number
            foreach (Card c in cards)
            {
                this.myHand.discard(c, this.myDeck);
            }
            lastObject.setMilitiaPlayed(false);//no longer need the militia action.
            return lastObject;
        }

        public StatusObject callDelayedFunctions()
        {
            StatusObject stat = new StatusObject(false);
            while (this.functionsToCall.Count > 0)
            {
                DelayedFunction func = this.functionsToCall.Dequeue();
                stat = func.performAction();
                if (stat.wasMilitiaPlayed())
                {
                    break;
                }
            }
            return stat;
        }

        public void addDelayedFunction(DelayedFunction func)
        {
            this.functionsToCall.Enqueue(func);
        }

        public List<Card> spyOnDecks()
        {
            List<Card> spied = new List<Card>();
            spied.Add(this.myDeck.peekAtTopCard());
            for (int i = 0; i < this.otherPlayers.Count; i++)
            {
                Player p = this.otherPlayers[i];
                if (!p.getHand().hasDefenseCard())
                {
                    spied.Add(p.getDeck().peekAtTopCard());
                }
                else
                {
                    p.getGame().addToGameMessage(p.getName() + Internationalizer.getMessage("Defended"));
                    spied.Add(null);
                }
            }
            return spied;
        }

        public StatusObject keepOrDiscardSpiedCards(List<Card> discard)
        {
            StatusObject o = new StatusObject(false);
            for (int i = 0; i < discard.Count; i++)
            {
                Player p = PlayerGetPlayerInList(i);
                if (discard[i] != null && !p.getDeck().peekAtTopCard().Equals(discard[i]))
                {
                    return o;
                }
            }
            for (int i = 0; i < discard.Count; i++)
            {
                Player p = PlayerGetPlayerInList(i);
                if (discard[i] != null && p.getDeck().peekAtTopCard().Equals(discard[i]))
                {
                    p.getDeck().discard(p.getDeck().draw());
                }
            }
            o.setSpiedSuccessfully(true);
            return o;
        }

        private Player PlayerGetPlayerInList(int i)
        {
            Player p;
            if (i == 0)
            {
                p = this;
            }
            else
            {
                p = this.otherPlayers[i - 1];
            }
            return p;
        }

        public void clearThiefList()
        {
            this.thiefList = new List<List<Card>>();
        }

        public List<List<Card>> getThiefList()
        {
            return this.thiefList;
        }

        /// <summary>
        /// Cards are indexed by position, must be one from every player that had a card in their list.
        /// If their list was empty, pass a null in that spot.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public StatusObject validateThiefStolenCards(List<Card> cards)
        {
            StatusObject ret = new StatusObject(false);
            this.thiefTrashed = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                List<Card> stolen = this.thiefList[i];
                if (cards[i] == null && stolen.Count == 0)
                {
                    continue;
                }
                else if (cards[i] == null)
                {
                    ret.setSelectTrashFromThief(true);
                    this.thiefTrashed = new List<Card>();
                    return ret;
                }
                else
                {
                    if (stolen.Contains(cards[i]))
                    {
                        this.thiefTrashed.Add(cards[i]);
                    }
                    else
                    {
                        ret.setSelectTrashFromThief(true);
                        this.thiefTrashed = new List<Card>();
                        return ret;
                    }
                }
            }

            //if it gets here they have selected one card from each list, or there were no money cards in part of the list and null was passed in
            int pos = 0;
            foreach (Card c in this.thiefTrashed)
            {
                List<Card> stolen = this.thiefList[pos];
                while (!stolen.Contains(c))
                {
                    pos++;
                    stolen = this.thiefList[pos];
                }
                stolen.Remove(c);
                foreach (Card cc in this.thiefList[pos])
                {
                    this.otherPlayers[pos].getDeck().discard(cc);
                }
            }
            ret.setKeepTrashedFromThief(true);
            ret.setMessage(Internationalizer.getMessage("ThiefMsg3"));
            return ret;
        }

        public List<Card> getPossibleCardsToKeepFromThief()
        {
            return this.thiefTrashed;
        }

        public StatusObject keepCardsFromThief(List<Card> cards)
        {
            StatusObject ret = new StatusObject(false);
            List<Card> thiefCopy = new List<Card>();
            foreach (Card c in this.thiefTrashed)
            {
                thiefCopy.Add(c);
            }

            foreach (Card c in cards)
            {
                if (!thiefCopy.Remove(c))
                {
                    ret.setKeepTrashedFromThief(true);
                    return ret;
                }
            }

            //gets here all were in the theifTrashedList
            foreach (Card c in cards)
            {
                this.getDeck().discard(c);
            }

            this.thiefTrashed = new List<Card>();
            return ret;
        }
    }
}
