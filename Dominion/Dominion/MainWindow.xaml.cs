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
        Deck player1Deck;
       

        private void Shuffle_Click(object sender, RoutedEventArgs e){
            player1Deck.reshuffle();
            DescriptionLabel.Content = "Top card's Currency Value:";
            Description.Content = player1Deck.getInDeck()[0].getCash();
        }

        private void MakeDeck_Click(object sender, RoutedEventArgs e){
            player1Deck = new Deck();
            DescriptionLabel.Content = "A new Deck has been initialized";
            Description.Content = "";
            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

        private void Draw_Click(object sender, RoutedEventArgs e){
            Card firstCard;
            firstCard = player1Deck.draw();
            player1Deck.discard(firstCard);
            DescriptionLabel.Content = "Cards Left in Deck:";
            Description.Content = player1Deck.cardsLeft();
            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

    }
}
