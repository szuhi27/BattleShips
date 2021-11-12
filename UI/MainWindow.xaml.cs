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

namespace BattleShips
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayB_Click(object sender, RoutedEventArgs e)
        {
            UI.PlayWindow playWindow = new UI.PlayWindow();
            this.Visibility = Visibility.Hidden;
            playWindow.Show();
        }

        private void HSB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("highscores Clicked");
        }
    }
}
