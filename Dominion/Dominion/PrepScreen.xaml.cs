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
            local =main.loc;
            Internationalizer.setLocale(local);
            this.ReadyButton.Content = player + " "+ Internationalizer.getMessage("IsReady");
            this.ReadyButton.Focus();
            this.main = main;
            this.ReadyLabel.Content = player + " "+ Internationalizer.getMessage("GetReady");
            this.Title = Internationalizer.getMessage("Prep");
            
        }
        MainWindow main;
        Locale local;
        private void Ready(object sender, RoutedEventArgs e) {
            main.Show();
            Close();
        }
    }
}
