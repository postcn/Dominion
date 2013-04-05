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
            PlayNumText.SelectionStart = 0;
            PlayNumText.SelectionLength = PlayNumText.Text.Length;
            NameBox1.Visibility = Visibility.Hidden;
            NameBox2.Visibility = Visibility.Hidden;
            NameBox3.Visibility = Visibility.Hidden;
            NameBox4.Visibility = Visibility.Hidden;
        }

        int maxplayers = 4;
        Boolean ready = false;
        MainWindow main;
        List<string> playernames;
        /***********************
        maybe add player names
         **********************/
        private void ConfirmNames(object sender, RoutedEventArgs e) {
            if (!ready) {
                string input = PlayNumText.Text;
                int numValue = 0;
                bool result = Int32.TryParse(input, out numValue);
                if (true == result) {
                    if (numValue > maxplayers || numValue < 2) {
                        MessageBox.Show("There Must Be 2-" + maxplayers + "Players", "Input Error");
                        PlayNumText.Focus();
                        PlayNumText.SelectionStart = 0;
                        PlayNumText.SelectionLength = PlayNumText.Text.Length;
                    } else {
                        enableBox(NameBox1);
                        enableBox(NameBox2);
                        if (numValue > 2) {
                            enableBox(NameBox3);
                        }
                        if (numValue == 4) {
                            enableBox(NameBox4);
                        }
                        ConfirmButton.Content = "Start Game";
                        NameBox1.Focus();
                        NameBox1.SelectionStart = 0;
                        NameBox1.SelectionLength = NameBox1.Text.Length;
                        PlayNumText.IsTabStop = false;
                        ready = true;

                    }
                } else {
                    MessageBox.Show("'" + input + "' " + "is not a valid number of players", "Input Error");
                    PlayNumText.Focus();
                    PlayNumText.SelectionStart = 0;
                    PlayNumText.SelectionLength = PlayNumText.Text.Length;
                }
            } else {
                playernames.Add(NameBox1.Text);
                playernames.Add(NameBox2.Text);
                if (NameBox3.IsEnabled) {
                    playernames.Add(NameBox3.Text);
                }
                if (NameBox4.IsEnabled) {
                    playernames.Add(NameBox4.Text);
                }
                main = new MainWindow(playernames);
                PrepScreen Prep = new PrepScreen(playernames[0], main);
                Prep.Show();
                Close();
            }
        }

        private void ChangeNumber(object sender, RoutedEventArgs e) {
            NameBox1.Visibility = Visibility.Hidden;
            NameBox1.IsEnabled = false;
            NameBox2.Visibility = Visibility.Hidden;
            NameBox2.IsEnabled = false;
            NameBox3.Visibility = Visibility.Hidden;
            NameBox3.IsEnabled = false;
            NameBox4.Visibility = Visibility.Hidden;
            NameBox4.IsEnabled = false;
            playernames = new List<string>();
            ConfirmButton.Content = "Confirm";
            ready = false;
            PlayNumText.IsTabStop = true;
        }
        private void enableBox(TextBox Box) {
            Box.Focusable = true;
            Box.Visibility = Visibility.Visible;
            Box.IsEnabled = true;
        }

        private void NameBox1Focus(object sender, RoutedEventArgs e) {
            NameBox1.SelectionStart = 0;
            NameBox1.SelectionLength = NameBox1.Text.Length;
        }
        private void NameBox2Focus(object sender, RoutedEventArgs e) {
            NameBox2.SelectionStart = 0;
            NameBox2.SelectionLength = NameBox2.Text.Length;
        }
        private void NameBox3Focus(object sender, RoutedEventArgs e) {
            NameBox3.SelectionStart = 0;
            NameBox3.SelectionLength = NameBox3.Text.Length;
        }
        private void NameBox4Focus(object sender, RoutedEventArgs e) {
            NameBox4.SelectionStart = 0;
            NameBox4.SelectionLength = NameBox4.Text.Length;
        }
    }
}
