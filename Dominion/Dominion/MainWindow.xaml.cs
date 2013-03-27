﻿using System;
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
            count++;
            if(firstCard.getCash()==1){
                image.UriSource = new Uri("Copper.jpg", UriKind.RelativeOrAbsolute);
            }
            else{
                image.UriSource = new Uri("Estate.jpg", UriKind.RelativeOrAbsolute);
                countE++;
            }
            image.EndInit();
            Hand_Card.Source = image;
            //
            EstateButtomImage.Source = image;
            //
            String county;
            county=countE+"/" +count;
            Counter.Content = county;

            //            Card yo = new Card(1, 4, 0, 6, 2, 8, 9);
            //          Description.Content = yo.getActions();
        }

        private void Reset_Click(object sender, RoutedEventArgs e){
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("Blank.jpg", UriKind.RelativeOrAbsolute);
            image.EndInit();
            Hand_Card.Source = image;
            //
           /* BitmapImage image1 = new BitmapImage();
            image1.BeginInit();
            image1.UriSource = new Uri("Estate.jpg", UriKind.RelativeOrAbsolute);
            image1.EndInit();
          //  EstateButtomImage.Source = image1;
            //*/
            Description.Content = "Labez";
            DescriptionLabel.Content = "Nothing Yet";
            player1Deck.reshuffle();
        }
        private void CopperImage_Click(object sender, RoutedEventArgs e){
            DescriptionLabel.Content = "Copper Selected";
        }
        private void EstateImage_Click(object sender, RoutedEventArgs e){
            DescriptionLabel.Content = "Estate Selected";
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(EstateButtomImage.Source.ToString(), UriKind.RelativeOrAbsolute);
            image.EndInit();
            Hand_Card.Source = image;
        }
        //http://stackoverflow.com/questions/4151380/wpf-image-control-with-click-event
        //http://stackoverflow.com/questions/5090435/image-button-on-visual-studio-2010
    }
}
