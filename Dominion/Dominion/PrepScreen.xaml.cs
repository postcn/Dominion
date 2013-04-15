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
    /// Interaction logic for PrepScreen.xaml
    /// </summary>
    public partial class PrepScreen : Window {
        public PrepScreen(String player, MainWindow main) {
            InitializeComponent();
            this.ReadyButton.Content = player + " Is Ready";
            this.ReadyButton.Focus();
            this.main = main;
            this.ReadyLabel.Content = player + " Get Ready";
            this.Title = " Prep Screen";
        }
        MainWindow main;

        private void Ready(object sender, RoutedEventArgs e) {
            main.Show();
            Close();
        }
    }
}
