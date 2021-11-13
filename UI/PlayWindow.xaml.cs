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
    public partial class PlayWindow : Window
    {

        private Customs.GameSave gameSave = new();
        private Customs.Coordinate[] p1Ships = new Customs.Coordinate[12], p2Ships = new Customs.Coordinate[12];
        private Customs.AiBehav aiBehav = new Customs.AiBehav();

        public PlayWindow()
        {
            InitializeComponent();
            TopLabel.Content = "Coose game mode!";
            PvAIB.Visibility = Visibility.Visible;
            PvPB.Visibility = Visibility.Visible;
        }

        private void PlayW_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            mainWindow.Show();  
        }

        private void P1SetupClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString(); //.Split('\u002C');
            button.Background = new SolidColorBrush(Colors.Black);

        }

        private void P1AttackClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString();
            Button? enemyB = (Button)P2Own.FindName("P2Field_"+coordinates);
            enemyB.Background = new SolidColorBrush(Colors.Black);
        }

        private void P2SetupClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString(); //.Split('\u002C');
            button.Background = new SolidColorBrush(Colors.Black);
        }

        private void P2AttackClick(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            var coordinates = button.Content.ToString();
            Button? enemyB = (Button)P2Own.FindName("P1Field_" + coordinates);
            enemyB.Background = new SolidColorBrush(Colors.Black);
        }

        private void GameStart()
        {
            PvAIB.Visibility = Visibility.Collapsed;
            PvPB.Visibility = Visibility.Collapsed;
        }

        private void PvAIB_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
            gameSave.gameMode = "PvAi";
            gameSave.player2 = "Ai";
            P1NameTB.Visibility = Visibility.Visible;
            P1SaveB.Visibility = Visibility.Visible;
        }

        private void PvPB_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
            gameSave.gameMode = "PvP";
            P1NameTB.Visibility = Visibility.Visible;
            P1SaveB.Visibility = Visibility.Visible;
        }

        private void SaveP1_Click(object sender, RoutedEventArgs e)
        {
            gameSave.player1 = P1NameTB.Text;
            P1NameTB.Visibility = Visibility.Hidden;
            P1SaveB.Visibility = Visibility.Hidden;
            if (gameSave.gameMode == "PvP")
            {
                P2NameTB.Visibility = Visibility.Visible;
                P2SaveB.Visibility = Visibility.Visible;
            }
            else
            {
                P1Fog.Visibility = Visibility.Hidden;
                ChooseStarer();
            }
        }

        private void ChooseStarer()
        {
            var rand = new Random();
            int num = rand.Next(0, 2);
            if(num == 0)
            {
                MessageBox.Show("You start the game!");
                PlayerSetup();
            }
            else
            {
                MessageBox.Show("The Ai starts the game!");
                p2Ships = aiBehav.GenerateShips();
            }
        }

        private void PlayerSetup()
        {
            throw new NotImplementedException();
        }

        private void SaveP2_Click(object sender, RoutedEventArgs e)
        {
            gameSave.player2 = P1NameTB.Text;
            P2NameTB.Visibility = Visibility.Hidden;
            P2SaveB.Visibility = Visibility.Hidden;
            P1Fog.Visibility = Visibility.Hidden;
        }


        


    }
}
