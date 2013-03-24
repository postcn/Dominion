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

        Game(int numPlayers)
        {
            players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(new Player());
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
            return null;
        }

        private void setupBuyables()
        {
            //Instantiate must have cards;

        }
    }
}
