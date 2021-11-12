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
using System.Windows.Shapes;

namespace BattleShips.UI
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {

        //private GroupBox p1Own, p2Own, p1Enemy, p2Enemy;

        public PlayWindow()
        {
            InitializeComponent();
            /*p1Own = (GroupBox)this.FindName("P1Own");
            p2Own = (GroupBox)this.FindName("P2Own");
            p1Enemy = (GroupBox)this.FindName("P1Enemy");
            p2Enemy = (GroupBox)this.FindName("P2Enemy");
            P2E1_1.Background = new SolidColorBrush(Colors.Black);*/

        }

        private void PlayW_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            mainWindow.Show();  
        }

        private void P2SetupClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString(); //.Split('\u002C');
            //MessageBox.Show(coordinates);
            button.Background = new SolidColorBrush(Colors.Black);

        }

        private void P2AttackClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString(); //.Split('\u002C');
            //MessageBox.Show(coordinates);
            button.Background = new SolidColorBrush(Colors.Black);

        }
    }
}
