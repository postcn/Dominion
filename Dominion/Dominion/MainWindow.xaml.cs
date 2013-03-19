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
    public partial class MainWindow : Window
    {
        public MainWindow(){
            InitializeComponent();
        }
        Deck player1Deck;
       

        private void Shuffle_Click(object sender, RoutedEventArgs e){
            player1Deck.reshuffle();
            Description.Content = player1Deck.getInDeck()[0].getCash();
        }

        private void MakeDeck_Click(object sender, RoutedEventArgs e){
            player1Deck = new Deck();
            Description.Content = "";
            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

        private void Draw_Click(object sender, RoutedEventArgs e){
            player1Deck.draw();
            Description.Content = player1Deck.cardsLeft();
            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

    }
}
