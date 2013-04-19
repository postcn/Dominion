using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class Game
    {
        List<Player> players;
        int currentPlayer;
        int numPlayers;
        List<CardStack> buyables;

        public Game(int numPlayers)
        {
            players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(new Player(i));
            }
            this.currentPlayer = 0;
            this.numPlayers = numPlayers;
            this.setupBuyables();
            this.initializePlayersToGame();
        }

        public int nextTurn()
        {
            this.currentPlayer++;
            this.currentPlayer = currentPlayer % numPlayers;
            return this.currentPlayer;
        }

        public Player nextTurnPlayer()
        {
            this.nextTurn();
            return this.getCurrentPlayer();
        }

        public Player getCurrentPlayer()
        {
            return this.players[this.currentPlayer];
        }

        public int getCurrentPlayerNumber()
        {
            return this.currentPlayer;
        }

        private void setupBuyables()
        {
            //Instantiate must have cards;

            //Instantiate Currency
            this.buyables = new List<CardStack>();
            this.buyables.Add(new CardStack(120, CardMother.Copper()));
            this.buyables.Add(new CardStack(120, CardMother.Silver()));
            this.buyables.Add(new CardStack(120, CardMother.Gold()));
            //Instantiate victory cards
            int numInStack = this.calculateSupplyForVictory();
            this.buyables.Add(new CardStack(numInStack, CardMother.Estate()));
            this.buyables.Add(new CardStack(numInStack, CardMother.Duchy()));
            this.buyables.Add(new CardStack(numInStack, CardMother.Province()));
            //TODO find actual number
            this.buyables.Add(new CardStack(120, CardMother.Curse()));


            this.buyables.Add(new CardStack(10, CardMother.Smithy()));
            this.buyables.Add(new CardStack(10, CardMother.Laboratory()));
            this.buyables.Add(new CardStack(10, CardMother.Market()));
            this.buyables.Add(new CardStack(10, CardMother.Festival()));
            this.buyables.Add(new CardStack(10, CardMother.Village()));
            this.buyables.Add(new CardStack(10, CardMother.Witch()));
            this.buyables.Add(new CardStack(10, CardMother.Feast()));
            this.buyables.Add(new CardStack(10, CardMother.Remodel()));
            this.buyables.Add(new CardStack(10, CardMother.Workshop()));
            this.buyables.Add(new CardStack(10, CardMother.Woodcutter()));
            //TODO: get cards out of possible list.
        }

        public List<CardStack> getBuyables() 
        {
            return this.buyables;
        }

        public int calculateSupplyForVictory()
        {
            if (this.numPlayers < 3)
            {
                return 8;
            }
            else 
            {
                return 12;
            }
        }

        public List<Player> getPlayers()
        {
            return this.players;
        }

        public void initializePlayersToGame()
        {
            foreach (Player p in this.players)
            {
                p.setGame(this);
            }
        }
    }
}
