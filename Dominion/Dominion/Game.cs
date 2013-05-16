using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dominion {
    [Serializable]
    public class Game {
        List<Player> players;
        int currentPlayer;
        int numPlayers;
        List<CardStack> buyables;
        List<Player> winningPlayers;
        String gameOverStatus = "";
        String gameStatus = "";
        int turnsSoFar;

        public Game(int numPlayers) {
            players = new List<Player>();
            for (int i = 0; i < numPlayers; i++) {
                players.Add(new Player(i));
            }
            winningPlayers = new List<Player>();
            this.currentPlayer = 0;
            this.numPlayers = numPlayers;
            this.turnsSoFar = 1;
            this.setupBuyables();
            this.initializePlayersToGame();
        }

        public int getTurnsPassed()
        {
            return this.turnsSoFar;
        }

        public int nextTurn() {
            this.addToGameMessage(this.getCurrentPlayer().getName() + Internationalizer.getMessage("NextTurn"));
            this.currentPlayer++;
            if (this.currentPlayer == this.numPlayers)
            {
                turnsSoFar++;
            }
            this.currentPlayer = currentPlayer % numPlayers;
            return this.currentPlayer;
        }

        public Player nextTurnPlayer() {
            this.nextTurn();
            return this.getCurrentPlayer();
        }

        public String nextPlayerName() {
            int playerInt = (this.currentPlayer + 1) % this.numPlayers;
            return this.players[playerInt].getName();
        }

        public Player getCurrentPlayer() {
            return this.players[this.currentPlayer];
        }

        public int getCurrentPlayerNumber() {
            return this.currentPlayer;
        }

        private void setupBuyables() {
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
           // this.buyables.Add(new CardStack(4, CardMother.Province()));


            //TODO find actual number
            this.buyables.Add(new CardStack(120, CardMother.Curse()));


            // this.buyables.Add(new CardStack(10, CardMother.Smithy()));
            this.buyables.Add(new CardStack(10, CardMother.Laboratory()));
            //this.buyables.Add(new CardStack(10, CardMother.Market()));
            // this.buyables.Add(new CardStack(10, CardMother.Festival()));
            //this.buyables.Add(new CardStack(10, CardMother.Village()));
            this.buyables.Add(new CardStack(10, CardMother.Moat()));
            // this.buyables.Add(new CardStack(10, CardMother.Cellar()));
            //this.buyables.Add(new CardStack(10, CardMother.Bureaucrat()));
            //this.buyables.Add(new CardStack(10, CardMother.Gardens()));
            this.buyables.Add(new CardStack(10, CardMother.Thief()));
            this.buyables.Add(new CardStack(10, CardMother.Spy()));
            //this.buyables.Add(new CardStack(10, CardMother.Witch()));
            //this.buyables.Add(new CardStack(10, CardMother.Mine()));
            this.buyables.Add(new CardStack(10, CardMother.Feast()));
            //this.buyables.Add(new CardStack(10, CardMother.Remodel()));
            //this.buyables.Add(new CardStack(10, CardMother.Moneylender()));
            this.buyables.Add(new CardStack(10, CardMother.Militia()));
            //this.buyables.Add(new CardStack(10, CardMother.Workshop()));
            this.buyables.Add(new CardStack(10, CardMother.Chancellor()));
            this.buyables.Add(new CardStack(10, CardMother.ThroneRoom()));
            // this.buyables.Add(new CardStack(10, CardMother.Woodcutter()));
            this.buyables.Add(new CardStack(10, CardMother.Chapel()));
            this.buyables.Add(new CardStack(10, CardMother.CouncilRoom()));
            //TODO: get cards out of possible list.
        }

        public List<CardStack> getBuyables() {
            return this.buyables;
        }

        public int calculateSupplyForVictory() {
            if (this.numPlayers < 3) {
                return 8;
            } else {
                return 12;
            }
        }

        public List<Player> getPlayers() {
            return this.players;
        }

        public void initializePlayersToGame() {
            foreach (Player p in this.players) {
                p.setGame(this);
            }
        }

        public Boolean isGameOver() {
            this.winningPlayers = new List<Player>();
            bool over = false;
            if (this.buyables[5].isEmpty()) //Provinces are gone.
            {
                over = true;
            }
            int pilesGone = 0;
            foreach (CardStack stack in this.buyables) {
                if (stack.isEmpty()) {
                    pilesGone++;
                }
            }
            if (pilesGone > 2) {
                over = true;
            }
            foreach (Player p in this.players) {
                p.setVictoryPts();
                if (this.winningPlayers.Count == 0) {
                    this.winningPlayers.Add(p);
                    continue;
                } else {
                    if (p.getVictoryPts() > this.winningPlayers[0].getVictoryPts()) {
                        this.winningPlayers = new List<Player>();
                        this.winningPlayers.Add(p);
                    } else if (p.getVictoryPts() == this.winningPlayers[0].getVictoryPts()) {
                        this.winningPlayers.Add(p);
                    }
                }
            }

            String status = "";
            if (this.winningPlayers.Count == 1) {
                status += Internationalizer.getMessage("Grats1") + winningPlayers[0].getName() + Internationalizer.getMessage("Grats2");
            } else {
                status += Internationalizer.getMessage("Tie");
            }

            status += "\n";

            foreach (Player p in this.players) {
                status += p.getName() + Internationalizer.getMessage("VictPts1") + p.getVictoryPts() + Internationalizer.getMessage("VictPts2");
            }

            this.gameOverStatus = status;

            return over;
        }

        public String getGameOverStatus() {
            return this.gameOverStatus;
        }

        public List<Player> getWinningPlayerList() {
            return this.winningPlayers;
        }

        public void addToGameMessage(String message) {
            this.gameStatus += message;
            this.gameStatus += "\n";
        }

        public String getGameStatus() {
            return this.gameStatus;
        }

        public void randomizeBuyables() {
            List<Int32> used = new List<Int32>();
            int size = CardMother.allBuyableCards().Count;
            Random rng = new Random();
            int val;
            for (int i = 0; i < 10; i++) {
                val = rng.Next() % size;
                while (used.Contains(val)) {
                    val = rng.Next() % size;
                }
                used.Add(val);
                this.buyables[i + 7] = new CardStack(10, CardMother.allBuyableCards()[val]);
            }
        }

        public static FileStream openFile()
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }
            else
            {
                return null;
            }
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
            return stream;
        }

        public void Save()
        {
            FileStream stream = Game.openFile();
            this.SaveFile(stream);
        }

        /// <summary>
        /// DONT USE PUBLIC FOR TESTING ONLY
        /// </summary>
        /// <param name="stream"></param>
        public void SaveFile(FileStream stream)
        {
            if (stream != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                formatter.Serialize(stream, Internationalizer.getLocale());
                stream.Close();
            }
        }

        public static Game Load()
        {
            FileStream stream = Game.openFile();
            return LoadFile(stream);
        }

        /// <summary>
        /// DONE USE PUBLIC FOR TESTING ONLY!
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Game LoadFile(FileStream stream)
        {
            if (stream != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Game g = (Game)formatter.Deserialize(stream);
                Locale l = (Locale)formatter.Deserialize(stream);
                Internationalizer.setLocale(l);
                stream.Close();
                return g;
            }
            else
            {
                return null;
            }
        }
    }
}
