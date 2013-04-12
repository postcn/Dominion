using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dominion{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow(List<String> players){
            InitializeComponent();
            this.totalPlayers = players.Count();
            this.playerNames = players;
            Initialize();
        }
        static Game myGame;
        /*************************
        add tooltips and tab indecies and maybe tool tips based upon if button is enabled or not and which phase we are in
        *************************/
        int totalPlayers,playernum;
        Player player;
        List<string> playerNames;
        List<CardStack> stacks;
        string currentCard,lastCard, handCard;
        List<Image> victoryImage, currencyImage,handImage;
        List<Button> currencyButton,victoryButton,handButton;

        /*********************************************************
         add playerinit and change start_click to initializegame
         ********************************************************/
        private void Initialize() {
            myGame = new Game(totalPlayers);
            player = myGame.getCurrentPlayer();
            stacks = myGame.getBuyables();
            currentCard = "";
            lastCard = "";
            handCard = "";
            playernum = 0;

            victoryButton = new List<Button>();
            currencyButton = new List<Button>();
            currencyImage = new List<Image>();
            victoryImage = new List<Image>();
            //Confirm.IsEnabled = true;
         //   List<Image> victory = new List<Image>();
         //   List<Image> currency = new List<Image>();
            // List<Image> action = new List<Image>();
         //   List<Button> victoryButton = new List<Button>();
         //   List<Button> currencyButton = new List<Button>();
            //   List<Button> actionButton = new List<Button>();
            victoryImage.Add(VictoryImage1);
            victoryImage.Add(VictoryImage2);
            victoryImage.Add(VictoryImage3);
            victoryImage.Add(VictoryImage4);
            victoryButton.Add(VictoryButton1);
            victoryButton.Add(VictoryButton2);
            victoryButton.Add(VictoryButton3);
            victoryButton.Add(VictoryButton4);
            currencyImage.Add(CurrencyImage1);
            currencyImage.Add(CurrencyImage2);
            currencyImage.Add(CurrencyImage3);
            currencyButton.Add(CurrencyButton1);
            currencyButton.Add(CurrencyButton2);
            currencyButton.Add(CurrencyButton3);

            int size = stacks.Count();
            int i, victorys = 0, currencies = 0, actions = 0;
            for (i = 0; i < size; i++) {
                string name = stacks[i].getCard().getName() + ".jpg";
                if (stacks[i].getCard().getType() == 0) {
                    setPicture(name, victoryImage[victorys]);
                    victoryImage[victorys].IsEnabled = true;
                    victoryButton[victorys].IsEnabled = true;
                    victorys++;
                } else if (stacks[i].getCard().getType() == 1) {
                    setPicture(name, currencyImage[currencies]);
                    currencyImage[currencies].IsEnabled = true;
                    currencyButton[currencies].IsEnabled = true;
                    currencies++;
                } else {
                    //     action[actions].Source = image;
                    //    action[actions].IsEnabled = true;
                    //    actions++;
                }
            }
            handImage = new List<Image>();
            handButton = new List<Button>();
            int length = 5;
            handImage.Add(HandImage1);
            handImage.Add(HandImage2);
            handImage.Add(HandImage3);
            handImage.Add(HandImage4);
            handImage.Add(HandImage5);
            handButton.Add(HandButton1);
            handButton.Add(HandButton2);
            handButton.Add(HandButton3);
            handButton.Add(HandButton4);
            handButton.Add(HandButton5);
            Hand myHand = player.getHand();
            for (i = 0; i < length; i++) {
                handImage[i].IsEnabled = true;
                handButton[i].IsEnabled = true;
                string name = myHand.getHand()[i].toString() + ".jpg";
                setPicture(name, handImage[i]);
            }
            player.getCurrency();
        }
        private void playerCont() {
            //player.cleanUp();
            resetCards();
            currentCard = "";
            Description.Content = myGame.getCurrentPlayerNumber();
            List<Image> handImage = new List<Image>();
            int length = 5;
            handImage.Add(HandImage1);
            handImage.Add(HandImage2);
            handImage.Add(HandImage3);
            handImage.Add(HandImage4);
            handImage.Add(HandImage5);
            Hand myHand = player.getHand();
            for (int i = 0; i < length; i++) {
                string name = myHand.getHand()[i].toString() + ".jpg";
                setPicture(name, handImage[i]);
            }
            player.getCurrency();
        }
    
        private void Cleanup_Click(object sender, RoutedEventArgs e){
            this.Hide();

            /*if (playernum+1 == totalPlayers) {
                playernum = 0;
            }else{
                playernum++;
            }*/
            playernum = myGame.nextTurn();
            PrepScreen prep = new PrepScreen(playerNames[playernum],this);
            prep.Show();
            player = myGame.getCurrentPlayer();
            player.cleanUp();
            playerCont();
            //switch to next player
        }

        private void Reset_Click(object sender, RoutedEventArgs e){
            setPicture("blank.jpg", Hand_Card);
            CardDrawnLabel.Content = "Card Bought";
            Description.Content = "Labez";
            DescriptionLabel.Content = "Nothing Yet";
        }

        private void CurrencyImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, currencyButton, currencyImage, false);
        }
        private void VictoryImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, victoryButton, victoryImage, false);
        }
        private void HandImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, handButton, handImage, true);   
        }
        private void Image_Click(object sender,List<Button> buttons,List<Image> images,Boolean handcard) {
            Button obj = (Button) sender;
            for (int i = 0; i < buttons.Count(); i++) {
                if (buttons[i] == obj) {
                    currentCard = stripImageSource(images[i].Source.ToString());
                    hilightCard(images[i], handcard);
                }
            }
        }
    /*private void HandImage1_Click(object sender, RoutedEventArgs e){
            currentCard = stripImageSource(HandImage1.Source.ToString());
            handCard = stripImageSource(HandImage1.Source.ToString());
            hilightCard(HandImage1, true);
            Buy.IsEnabled = false;
        }*/

        private string stripImageSource(string str){
            int length = str.Count();
            
            return str.Substring(42, length - 46);
        }
        private void Buy_Click(object sender, RoutedEventArgs e){
            if (currentCard.Equals("") || handCard.Equals(currentCard)){
                Description.Content = "A Buyable Card isn't Selected";
                resetCards();
                return;
            }
            Boolean work = false;
            int i;
            int length = stacks.Count();
            for (i = 0; i < length; i++){
                if (stacks[i].getCard().toString().Equals(currentCard)){
                    work = player.buy(stacks[i]);
                }
            }

            if (!work){
                Description.Content = "buy failed " + player.getCurrencyValue();
            }else{
                Description.Content = "Buy Sucessful";
                string name = currentCard + ".jpg";
                setPicture(name, Hand_Card);
            }
            resetCards();
        }

       
        private void hilightCard(Image pic, Boolean isHandCard){
            resetCards();
            string card;
            Buy.IsEnabled = false;
            if (!currentCard.Contains("1")){
                card = currentCard + "1" + ".jpg";
                if (!isHandCard){
                    Buy.IsEnabled = true;
                }
            }else{
                card = currentCard.Substring(0, currentCard.Count() - 1) + ".jpg";
            }

            lastCard = card.Substring(0, card.Count() - 4);
            setPicture(card, pic);

        }
        private void resetCards(){
            if (lastCard != ""){
                Buy.IsEnabled = false;
                string card = lastCard;
                if (lastCard.Contains("1")){
                    card = lastCard.Substring(0, lastCard.Count() - 1);
                }
                card = card + ".jpg";
                List<Image> victory = new List<Image>();
                List<Image> currency = new List<Image>();
                List<Image> handImage = new List<Image>();
                // List<Image> action = new List<Image>();
                victory.Add(VictoryImage1);
                victory.Add(VictoryImage2);
                victory.Add(VictoryImage3);
                victory.Add(VictoryImage4);
                currency.Add(CurrencyImage1);
                currency.Add(CurrencyImage2);
                currency.Add(CurrencyImage3);
                handImage.Add(HandImage1);
                handImage.Add(HandImage2);
                handImage.Add(HandImage3);
                handImage.Add(HandImage4);
                handImage.Add(HandImage5);
                int i;
                for (i = 0; i < currency.Count(); i++){
                    if (stripImageSource(currency[i].Source.ToString()).Equals(lastCard)){
                        setPicture(card, currency[i]);
                        return;
                    }
                }
                for (i = 0; i < handImage.Count(); i++){
                    if (stripImageSource(handImage[i].Source.ToString()).Equals(lastCard)){
                        setPicture(card, handImage[i]);
                        return;
                    }
                }
                for (i = 0; i < victory.Count(); i++){
                    if (victory[i].IsEnabled == true){
                        if (stripImageSource(victory[i].Source.ToString()).Equals(lastCard)){
                            setPicture(card, victory[i]);
                            return;
                        }
                    }
                }
            }
        }
        private void setPicture(string str, Image pic){
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(str, UriKind.RelativeOrAbsolute);
            image.EndInit();
            pic.Source = image;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e){

        }
        //doens't check buys
        //shuffle cards well (like from start)
        //
        //http://stackoverflow.com/questions/4151380/wpf-image-control-with-click-event
        //http://stackoverflow.com/questions/5090435/image-button-on-visual-studio-2010
    }
}
