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
        public MainWindow(){
            InitializeComponent();
        }
        static Game myGame;
        Player player;
        List<CardStack> stacks;
        string currentCard;
        string handcard;
        //  Deck Deck = Player.getDeck();

        private void Cleanup_Click(object sender, RoutedEventArgs e){
            player.cleanUp();
            currentCard = "";
            List<Image> handImage = new List<Image>();
            List<Button> handButton = new List<Button>();
            int length=5;
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
            int i;
            Hand myHand=player.getHand();
            for (i = 0; i < length; i++){
                string name = myHand.getHand()[i].toString()+".jpg";
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(name, UriKind.RelativeOrAbsolute);
                image.EndInit();
                handImage[i].Source = image;
                
            }
            player.getCurrency();

        }

        private void Reset_Click(object sender, RoutedEventArgs e){
            BitmapImage image = new BitmapImage();
             image.BeginInit();
             image.UriSource = new Uri("Blank.jpg", UriKind.RelativeOrAbsolute);
             image.EndInit();
             Hand_Card.Source = image;
             CardDrawnLabel.Content = "Card Bought";
             Description.Content = "Labez";
             DescriptionLabel.Content = "Nothing Yet";
        }
        private void CurrencyImage1_Click(object sender, RoutedEventArgs e){
            currentCard = CurrencyImage1.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void CurrencyImage2_Click(object sender, RoutedEventArgs e){
            currentCard = CurrencyImage2.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void CurrencyImage3_Click(object sender, RoutedEventArgs e){
            currentCard = CurrencyImage3.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void VictoryImage1_Click(object sender, RoutedEventArgs e){
            currentCard = VictoryImage1.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void VictoryImage2_Click(object sender, RoutedEventArgs e){
            currentCard = VictoryImage2.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void VictoryImage3_Click(object sender, RoutedEventArgs e){
            currentCard = VictoryImage3.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void VictoryImage4_Click(object sender, RoutedEventArgs e){
            currentCard = VictoryImage4.Source.ToString();
            setCurrentCard(currentCard);
        }
        private void setCurrentCard(string str){
            int length = str.Count();
            currentCard = str.Substring(42, length - 46);
            Description.Content = currentCard;
        }
        private void Buy_Click(object sender, RoutedEventArgs e){
            if (currentCard.Equals("")){
                Description.Content = "no card selected";
            }
            Boolean work=false;
            int i;
            int length = stacks.Count();
            for(i=0;i<length;i++){
                if(stacks[i].getCard().toString().Equals(currentCard)){
                    work=player.buy(stacks[i]);
                }
            }

            if (!work){
                Description.Content = "buy failed " + player.getCurrencyValue();
            }
            else{
                string name = currentCard+".jpg";
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(name, UriKind.RelativeOrAbsolute);
                image.EndInit();
                Hand_Card.Source = image;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e){
            myGame = new Game(1);
            player = myGame.getCurrentPlayer();
            stacks = myGame.getBuyables();
            //Curr.IsEnabled = true;
            Reset.IsEnabled = true;
            Buy.IsEnabled = true;
            Cleanup.IsEnabled = true;
            currentCard = "";
            handcard = "";
            //Confirm.IsEnabled = true;
            List<Image> victory = new List<Image>();
            List<Image> currency = new List<Image>();
           // List<Image> action = new List<Image>();
            List<Button> victoryButton = new List<Button>();
            List<Button> currencyButton = new List<Button>();
         //   List<Button> actionButton = new List<Button>();
            victory.Add(VictoryImage1);
            victory.Add(VictoryImage2);
            victory.Add(VictoryImage3);
            victory.Add(VictoryImage4);
            victoryButton.Add(VictoryButton1);
            victoryButton.Add(VictoryButton2);
            victoryButton.Add(VictoryButton3);
            victoryButton.Add(VictoryButton4);
            currency.Add(CurrencyImage1);
            currency.Add(CurrencyImage2);
            currency.Add(CurrencyImage3);
            currencyButton.Add(CurrencyButton1);
            currencyButton.Add(CurrencyButton2);
            currencyButton.Add(CurrencyButton3);

            int size = stacks.Count();
            int i, victorys = 0, currencies = 0, actions = 0;
            for (i = 0; i < size; i++)
            {
                string name = stacks[i].getCard().getName() + ".jpg";
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(name, UriKind.RelativeOrAbsolute);
                image.EndInit();
                if (stacks[i].getCard().getType() == 0)
                {
                    victory[victorys].Source = image;
                    victory[victorys].IsEnabled = true;
                    victoryButton[victorys].IsEnabled = true;
                    victorys++;
                }
                else if (stacks[i].getCard().getType() == 1)
                {
                    currency[currencies].Source = image;
                    currency[currencies].IsEnabled = true;
                    currencyButton[currencies].IsEnabled = true;
                    currencies++;
                }
                else
                {
                    //     action[actions].Source = image;
                    //    action[actions].IsEnabled = true;
                    //    actions++;
                }
            }


                List<Image> handImage = new List<Image>();
                List<Button> handButton = new List<Button>();
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
                    BitmapImage image1 = new BitmapImage();
                    image1.BeginInit();
                    image1.UriSource = new Uri(name, UriKind.RelativeOrAbsolute);
                    image1.EndInit();
                    handImage[i].Source = image1;

                }
                player.getCurrency();
            
        }

        private void Curr_Click(object sender, RoutedEventArgs e){
            player.getCurrency();
        }
        //http://stackoverflow.com/questions/4151380/wpf-image-control-with-click-event
        //http://stackoverflow.com/questions/5090435/image-button-on-visual-studio-2010
    }
}
