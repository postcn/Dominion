using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    class Game
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
            this.buyables.Add(new CardStack(120, new Card(1, 1, 0, 0, 0, 0, 0, "Copper", "1 Currency Value", 0)));
            this.buyables.Add(new CardStack(120, new Card(1, 2, 0, 0, 0, 0, 0, "Silver", "2 Currency Value", 3)));
            this.buyables.Add(new CardStack(120, new Card(1, 3, 0, 0, 0, 0, 0, "Gold", "3 Currency Value", 6)));
            //Instantiate victory cards
            int numInStack = this.calculateSupplyForVictory();
            this.buyables.Add(new CardStack(numInStack, new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2)));
            this.buyables.Add(new CardStack(numInStack, new Card(0, 0, 0, 0, 3, 0, 0, "Duchy", "3 Victory Points", 5)));
            this.buyables.Add(new CardStack(numInStack, new Card(0, 0, 0, 0, 6, 0, 0, "Province", "6 Victory Points", 8)));
            //TODO: get cards out of possible list.
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
    }
}
