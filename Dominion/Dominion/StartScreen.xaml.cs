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
    public partial class StartScreen : Window {
        public StartScreen() {
            InitializeComponent();
            PlayNumText.Focus();
            HilightBox(PlayNumText);
        }

        List<TextBox> nameBox;
        int maxplayers = 4;
        Boolean ready = false;
        MainWindow main;
        int numValue;

        private void ConfirmNames(object sender, RoutedEventArgs e) {
            if (!ready) {
                string input = PlayNumText.Text;
                numValue = 0;
                //for testing purposes
                if (input.Equals("Number of Players")) {
                    Game mygame = new Game(1);
                    mygame.getPlayers()[0].setName("Admin");
                    main = new MainWindow(mygame);
                    PrepScreen Prep = new PrepScreen("Admin", main);
                    Prep.Show();
                    Close();
                    return;
                }
                bool result = Int32.TryParse(input, out numValue);
                if (true == result) {
                    if (numValue > maxplayers || numValue < 2) {
                        MessageBox.Show("There Must Be 2-" + maxplayers + " Players", "Input Error");
                        numValue = 0;
                        PlayNumText.Focus();
                        HilightBox(PlayNumText);
                    } else {
                        nameBox = new List<TextBox>();
                        nameBox.Add(NameBox1);
                        nameBox.Add(NameBox2);
                        if (numValue > 2) {
                            nameBox.Add(NameBox3);
                        }
                        if (numValue == 4) {
                            nameBox.Add(NameBox4);
                        }
                        for (int i = 0; i < numValue; i++) {
                            enableBox(nameBox[i]);
                        }
                        ConfirmButton.Content = "Start Game";
                        NameBox1.Focus();
                        HilightBox(NameBox1);
                        PlayNumText.IsTabStop = false;
                        ready = true;

                    }
                } else {
                    MessageBox.Show("'" + input + "' " + "is not a valid number of players", "Input Error");
                    PlayNumText.Focus();
                    HilightBox(PlayNumText);
                }
            } else {
                Game mygame = new Game(numValue);
                List<Player> players = mygame.getPlayers();
                for (int i = 0; i < numValue; i++) {
                    players[i].setName(nameBox[i].Text);
                }
                main = new MainWindow(mygame);
                PrepScreen Prep = new PrepScreen(players[0].getName(),main);
                Prep.Show();
                Close();
            }
        }
        /*
         * maybe on lose focus change number of text boxes like territory wars would
         */
        private void ChangeNumber(object sender, RoutedEventArgs e) {
            for (int i = 0; i < numValue; i++) {
                nameBox[i].Visibility = Visibility.Hidden;
                nameBox[i].IsEnabled = false;
            }
            ConfirmButton.Content = "Confirm";
            ready = false;
            PlayNumText.IsTabStop = true;
        }
        private void enableBox(TextBox Box) {
            Box.Focusable = true;
            Box.Visibility = Visibility.Visible;
            Box.IsEnabled = true;
        }
        private void NameBoxFocus(object sender, RoutedEventArgs e) {
            HilightBox((TextBox) sender);
        }
        private void HilightBox(TextBox obj) {
            obj.SelectionStart = 0;
            obj.SelectionLength = obj.Text.Length;
        }
    }
}
