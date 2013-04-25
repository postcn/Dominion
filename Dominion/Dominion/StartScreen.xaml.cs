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
            languages = new List<MenuItem>();
            languages.Add(English);
            languages.Add(Something_Lame);
            nameBox = new List<TextBox>();
            nameBox.Add(NameBox1);
            nameBox.Add(NameBox2);
            nameBox.Add(NameBox3);
            nameBox.Add(NameBox4);
        }
        List<MenuItem> languages;
        List<TextBox> nameBox;
        MainWindow main;
        int numValue;
        private void ConfirmNames(object sender, RoutedEventArgs e) {
            Game mygame = new Game(numValue);
            List<Player> players = mygame.getPlayers();
            for (int i = 0; i < numValue; i++) {
                players[i].setName(nameBox[i].Text);
            }
            main = new MainWindow(mygame);
            PrepScreen Prep = new PrepScreen(players[0].getName(), main);
            Prep.Show();
            Close();
        }
        private void RadioCheck(Object sender, RoutedEventArgs e) {
            UnEnableText();
            RadioButton obj = (RadioButton)sender;
            String num = obj.Content.ToString().Substring(0, 1);
            Int32.TryParse(num, out numValue);
            //1.)
            if (numValue == 1) {
                Game mygame = new Game(1);
                mygame.getPlayers()[0].setName("Admin");
                main = new MainWindow(mygame);
                main.Show();
                Close();
                return;
            }
            //
            EnableText(numValue);
        }
        private void EnableText(int num) {
            for (int i = 0; i < num; i++) {
                nameBox[i].Focusable = true;
                nameBox[i].Visibility = Visibility.Visible;
                nameBox[i].IsEnabled = true;
            }
            NameBox1.Focus();
            HilightBox(NameBox1);
        }
        private void UnEnableText() {
            for (int i = 2; i < nameBox.Count; i++) {
                nameBox[i].Focusable = false;
                nameBox[i].Visibility = Visibility.Hidden;
                nameBox[i].IsEnabled = false;
            }
        }
        private void NameBoxFocus(object sender, RoutedEventArgs e) {
            HilightBox((TextBox)sender);
        }
        private void HilightBox(TextBox obj) {
            obj.SelectionStart = 0;
            obj.SelectionLength = obj.Text.Length;
        }
        private void ChangeLanguage(object sender, RoutedEventArgs e) {
            UncheckLanguages();
            MenuItem obj = (MenuItem)sender;
            obj.IsChecked = true;
        }
        private void UncheckLanguages() {
            for (int i = 0; i < languages.Count; i++) {
                languages[i].IsChecked = false;
            }
        }
    }

}
