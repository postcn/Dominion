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

namespace Dominion {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow(Game game) {
            InitializeComponent();
            myGame = game;
            Initialize();
        }
        Game myGame;
        /*************************
        add tooltips and tab indecies and maybe tool tips based upon if button is enabled or not and which phase we are in
        *************************/
        Player player;
        List<CardStack> stacks;
        string currentCard, lastCard, handCard,phase;
        List<Image> victoryImage, currencyImage, handImage, actionImage;
        List<Button> currencyButton, victoryButton, handButton, actionButton;

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            //StatusObject status = ;
            int length = stacks.Count();
            for (int i = 0; i < length; i++) {
                if (stacks[i].getCard().toString().Equals(currentCard)) {
                  //  status = player.play(stacks[i]);
                }
            }

        }
        private void PlayerCont() {
            ResetUnknownHilightedCards();
            currentCard = "";
            // player.getHand().draw(player.getDeck());
            int length = player.getHand().size();
            int panelsize = 400 + (length - 5) * 80;
            stackpan.Width = panelsize;
            Hand myHand = player.getHand();
            for (int i = 0; i < length; i++) {
                string name = myHand.getHand()[i].toString() + ".jpg";
                SetPicture(name, handImage[i]);
            }
            int actions = player.getActionsLeft();
            int currency = player.getCurrency();
            int buys = player.getBuysLeft();
        }

        private void Cleanup_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            player = myGame.nextTurnPlayer();
            PrepScreen prep = new PrepScreen(player.getName(), this);
            prep.Show();
            player = myGame.getCurrentPlayer();
            SetPicture("blank.jpg", Selected_Card);
            player.cleanUp();
            phase = "buy";
            PlayerCont();
        }
        private void EndPhase_Click(object sender, RoutedEventArgs e) {
            //set tool tips based on phase
            Buy.IsEnabled = false;
            phase = "action";
            ResetUnknownHilightedCards();
            SetPicture("blank.jpg", Selected_Card);
        }

        private void Reset_Click(object sender, RoutedEventArgs e) {
            SetPicture("blank.jpg", Hand_Card);
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
        private void ActionImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, actionButton, actionImage, false);
        }
        private void Image_Click(object sender, List<Button> buttons, List<Image> images, Boolean handcard) {
            Button obj = (Button)sender;
            for (int i = 0; i < buttons.Count(); i++) {
                if (buttons[i] == obj) {
                    currentCard = StripImageSource(images[i].Source.ToString());
                    HilightCard(images[i], handcard);
                }
            }
        }
        /*private void HandImage1_Click(object sender, RoutedEventArgs e){
                currentCard = stripImageSource(HandImage1.Source.ToString());
                handCard = stripImageSource(HandImage1.Source.ToString());
                hilightCard(HandImage1, true);
                Buy.IsEnabled = false;
            }*/

        private string StripImageSource(string str) {
            int length = str.Count();
            return str.Substring(42, length - 46);
        }
        private void Buy_Click(object sender, RoutedEventArgs e) {
            /* if (currentCard.Equals("") || handCard.Equals(currentCard)){
                 Description.Content = "A Buyable Card isn't Selected";
                 ResetCards();
                 return;
             }*/
            Boolean work = false;
            int length = stacks.Count();
            for (int i = 0; i < length; i++) {
                if (stacks[i].getCard().toString().Equals(currentCard)) {
                    work = player.buy(stacks[i]);
                    //set a variable here then only reset this card 
                }
            }

            if (!work) {
                Description.Content = "buy failed " + player.getCurrencyValue();
            } else {
                Description.Content = "Buy Sucessful";
                string name = currentCard + ".jpg";
                SetPicture(name, Hand_Card);
            }
            ResetUnknownHilightedCards();
            //reset only hilighted one? as determined by the for loop?
        }


        private void HilightCard(Image pic, Boolean isHandCard) {
            Boolean work=false;
            ResetUnknownHilightedCards();
            string card;
            Buy.IsEnabled = false;
            Confirm.IsEnabled = false;
            string selected="blank";
            if (!currentCard.Contains("1")) {
                 selected = currentCard;
                card = currentCard + "1" + ".jpg";
                if (!isHandCard&&phase.Equals("buy")) {
                    work = true;
                    Buy.IsEnabled = true;
                }
                if(isHandCard&&phase.Equals("action")){
                    work = true;
                    Confirm.IsEnabled=true;
                }
            } else {
                card = currentCard.Substring(0, currentCard.Count() - 1) + ".jpg";
                SetPicture("blank.jpg", Selected_Card);
            }

            lastCard = card.Substring(0, card.Count() - 4);
            if (work) {
                SetPicture(card, pic);
                SetPicture(selected + ".jpg", Selected_Card);
            }
        }

        private void SetPicture(string str, Image pic) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(str, UriKind.RelativeOrAbsolute);
            image.EndInit();
            pic.Source = image;
        }
        private void ResetUnknownHilightedCards() {
            if (lastCard != "") {
                Buy.IsEnabled = false;
                Confirm.IsEnabled = false;
                string card = lastCard;
                if (lastCard.Contains("1")) {
                    card = lastCard.Substring(0, lastCard.Count() - 1);
                }
                card = card + ".jpg";
                int i;
                for (i = 0; i < currencyImage.Count(); i++) {
                    if (StripImageSource(currencyImage[i].Source.ToString()).Equals(lastCard)) {
                        SetPicture(card, currencyImage[i]);
                    }
                }
                for (i = 0; i < victoryImage.Count(); i++) {
                    if (StripImageSource(victoryImage[i].Source.ToString()).Equals(lastCard)) {
                        SetPicture(card, victoryImage[i]);
                    }
                }
                for (i = 0; i < actionImage.Count(); i++) {
                    if (StripImageSource(actionImage[i].Source.ToString()).Equals(lastCard)) {
                        SetPicture(card, actionImage[i]);
                    }
                }
                for (i = 0; i < handImage.Count(); i++) {
                    if (StripImageSource(handImage[i].Source.ToString()).Equals(lastCard)) {
                        SetPicture(card, handImage[i]);
                    }
                }
            }
        }
        /*********************************************************
         add playerinit and change start_click to initializegame
         ********************************************************/
        private void Initialize() {
            player = myGame.getCurrentPlayer();
            stacks = myGame.getBuyables();
            currentCard = "";
            lastCard = "";
            handCard = "";
            phase = "buy";
            InitializeButtonImages();

            //Confirm.IsEnabled = true;
            int size = stacks.Count();
            int i, victorys = 0, currencies = 0, actions = 0;
            for (i = 0; i < size; i++) {
                string name = stacks[i].getCard().getName() + ".jpg";
                if (stacks[i].getCard().getType() == 0) {
                    SetPicture(name, victoryImage[victorys]);
                    victoryButton[victorys].IsEnabled = true;
                    victorys++;
                } else if (stacks[i].getCard().getType() == 1) {
                    SetPicture(name, currencyImage[currencies]);
                    currencyButton[currencies].IsEnabled = true;
                    currencies++;
                } else {
                    SetPicture(name, actionImage[actions]);
                    actionButton[actions].IsEnabled = true;
                    actions++;
                }
            }

            int handsize = 5;
            Hand myHand = player.getHand();
            for (i = 0; i < handsize; i++) {
                string name = myHand.getHand()[i].toString() + ".jpg";
                SetPicture(name, handImage[i]);
            }
            player.getCurrency();
        }
        private void InitializeButtonImages() {
            victoryButton = new List<Button>();
            currencyButton = new List<Button>();
            actionButton = new List<Button>();
            currencyImage = new List<Image>();
            victoryImage = new List<Image>();
            actionImage = new List<Image>();
            handImage = new List<Image>();
            handButton = new List<Button>();
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
            actionImage.Add(ActionImage1);
            actionImage.Add(ActionImage2);
            actionImage.Add(ActionImage3);
            actionImage.Add(ActionImage4);
            actionImage.Add(ActionImage5);
            actionImage.Add(ActionImage6);
            actionImage.Add(ActionImage7);
            actionImage.Add(ActionImage8);
            actionImage.Add(ActionImage9);
            actionImage.Add(ActionImage10);
            actionButton.Add(ActionButton1);
            actionButton.Add(ActionButton2);
            actionButton.Add(ActionButton3);
            actionButton.Add(ActionButton4);
            actionButton.Add(ActionButton5);
            actionButton.Add(ActionButton6);
            actionButton.Add(ActionButton7);
            actionButton.Add(ActionButton8);
            actionButton.Add(ActionButton9);
            actionButton.Add(ActionButton10);
            handImage.Add(HandImage1);
            handImage.Add(HandImage2);
            handImage.Add(HandImage3);
            handImage.Add(HandImage4);
            handImage.Add(HandImage5);
            handImage.Add(HandImage6);
            handImage.Add(HandImage7);
            handImage.Add(HandImage8);
            handImage.Add(HandImage9);
            handImage.Add(HandImage10);
            handImage.Add(HandImage11);
            handImage.Add(HandImage12);
            handImage.Add(HandImage13);
            handImage.Add(HandImage14);
            handImage.Add(HandImage15);
            handImage.Add(HandImage16);
            handImage.Add(HandImage17);
            handImage.Add(HandImage18);
            handImage.Add(HandImage19);
            handImage.Add(HandImage20);
            handImage.Add(HandImage11);
            handImage.Add(HandImage12);
            handImage.Add(HandImage13);
            handImage.Add(HandImage14);
            handImage.Add(HandImage15);
            handImage.Add(HandImage16);
            handImage.Add(HandImage17);
            handImage.Add(HandImage18);
            handImage.Add(HandImage19);
            handImage.Add(HandImage20);
            handImage.Add(HandImage21);
            handImage.Add(HandImage22);
            handImage.Add(HandImage23);
            handImage.Add(HandImage24);
            handImage.Add(HandImage25);
            handImage.Add(HandImage26);
            handImage.Add(HandImage27);
            handImage.Add(HandImage28);
            handImage.Add(HandImage29);
            handImage.Add(HandImage30);
            handImage.Add(HandImage31);
            handImage.Add(HandImage32);
            handImage.Add(HandImage33);
            handImage.Add(HandImage34);
            handImage.Add(HandImage35);
            handImage.Add(HandImage36);
            handImage.Add(HandImage37);
            handImage.Add(HandImage38);
            handImage.Add(HandImage39);
            handImage.Add(HandImage40);
            handImage.Add(HandImage41);
            handImage.Add(HandImage42);
            handImage.Add(HandImage43);
            handImage.Add(HandImage44);
            handImage.Add(HandImage45);
            handImage.Add(HandImage46);
            handImage.Add(HandImage47);
            handImage.Add(HandImage48);
            handImage.Add(HandImage49);
            handImage.Add(HandImage50);
            handButton.Add(HandButton1);
            handButton.Add(HandButton2);
            handButton.Add(HandButton3);
            handButton.Add(HandButton4);
            handButton.Add(HandButton5);
            handButton.Add(HandButton6);
            handButton.Add(HandButton7);
            handButton.Add(HandButton8);
            handButton.Add(HandButton9);
            handButton.Add(HandButton10);
            handButton.Add(HandButton11);
            handButton.Add(HandButton12);
            handButton.Add(HandButton13);
            handButton.Add(HandButton14);
            handButton.Add(HandButton15);
            handButton.Add(HandButton16);
            handButton.Add(HandButton17);
            handButton.Add(HandButton18);
            handButton.Add(HandButton19);
            handButton.Add(HandButton20);
            handButton.Add(HandButton10);
            handButton.Add(HandButton11);
            handButton.Add(HandButton12);
            handButton.Add(HandButton13);
            handButton.Add(HandButton14);
            handButton.Add(HandButton15);
            handButton.Add(HandButton16);
            handButton.Add(HandButton17);
            handButton.Add(HandButton18);
            handButton.Add(HandButton19);
            handButton.Add(HandButton20);
            handButton.Add(HandButton20);
            handButton.Add(HandButton21);
            handButton.Add(HandButton22);
            handButton.Add(HandButton23);
            handButton.Add(HandButton24);
            handButton.Add(HandButton25);
            handButton.Add(HandButton26);
            handButton.Add(HandButton27);
            handButton.Add(HandButton28);
            handButton.Add(HandButton29);
            handButton.Add(HandButton30);
            handButton.Add(HandButton31);
            handButton.Add(HandButton32);
            handButton.Add(HandButton33);
            handButton.Add(HandButton34);
            handButton.Add(HandButton35);
            handButton.Add(HandButton36);
            handButton.Add(HandButton37);
            handButton.Add(HandButton38);
            handButton.Add(HandButton39);
            handButton.Add(HandButton40);
            handButton.Add(HandButton41);
            handButton.Add(HandButton42);
            handButton.Add(HandButton43);
            handButton.Add(HandButton44);
            handButton.Add(HandButton45);
            handButton.Add(HandButton46);
            handButton.Add(HandButton47);
            handButton.Add(HandButton48);
            handButton.Add(HandButton49);
            handButton.Add(HandButton50);
        }
        //shuffle cards well (like from start)
        //
        //http://stackoverflow.com/questions/4151380/wpf-image-control-with-click-event
        //http://stackoverflow.com/questions/5090435/image-button-on-visual-studio-2010
    }
}
