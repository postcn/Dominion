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

namespace Dominion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
        }
        Deck player1Deck = new Deck();
        int count = 0;
        int countE = 0;


        private void Shuffle_Click(object sender, RoutedEventArgs e){
            count = 0;
            countE = 0;
            Counter.Content = "Estate vs total  (drawn)";
            player1Deck.reshuffle();
            DescriptionLabel.Content = "Top card's Currency Value:";
            Description.Content = player1Deck.getInDeck()[0].getCash();
        }

        private void Draw_Click(object sender, RoutedEventArgs e){
            Card firstCard;
            firstCard = player1Deck.draw();
            player1Deck.discard(firstCard);
            DescriptionLabel.Content = "Cards Left in Deck:";
            Description.Content = player1Deck.cardsLeft();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            //Description.Content = firstCard.ToString();
            count++;
            if(firstCard.getCash()==1){
                image.UriSource = new Uri("Copper.jpg", UriKind.Relative);       
            }
            else{
                image.UriSource = new Uri("Estate.jpg", UriKind.Relative);
                countE++;
            }
            image.EndInit();
            Hand_Card.Source = image;
            String county;
            county=countE+"/" +count;
            Counter.Content = county;

            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e){
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("Blank.jpg", UriKind.Relative);
            image.EndInit();
            Hand_Card.Source = image;
            Description.Content = "Labez";
            DescriptionLabel.Content = "Nothing Yet";
            player1Deck.reshuffle();
        }

    }
}
