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
using System.Windows.Shapes;

namespace BattleShips.UI
{
    public partial class PlayWindow : Window
    {
        private Customs.AiBehav aiBehav = new Customs.AiBehav();
        private Customs.ManualPlacer manualPlacer = new Customs.ManualPlacer();

        private Customs.GameSave gameSave = new();
        private Customs.Coordinate[] p1Ships = new Customs.Coordinate[12], p2Ships = new Customs.Coordinate[12],
            manualCords = new Customs.Coordinate[2];
        private bool aiShipsCreated, p1ShipsCreated;
        private string currentPlayer, startingPlayer;
        private int manualChoosen;

        public PlayWindow()
        {
            InitializeComponent();
            TopLabel.Content = "Coose game mode!";
            PvAIB.Visibility = Visibility.Visible;
            PvPB.Visibility = Visibility.Visible;
            aiShipsCreated = false;
            p1ShipsCreated = false;
            currentPlayer = "p1";
            startingPlayer = "";
            manualChoosen = 0;
        }

        private void PlayW_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            mainWindow.Show();  
        }

        private void P1SetupClick(object sender, RoutedEventArgs e)
        {
            if (!p1ShipsCreated && manualChoosen<2)
            {
                Button? button = sender as Button;
                string[] cordsS = button.Content.ToString().Split('_');
                Customs.Coordinate manCord = new();
                manCord.R = Int32.Parse(cordsS[0]);
                manCord.C = Int32.Parse(cordsS[1]);
                manualCords[manualChoosen] = manCord;
                manualChoosen++;
                button.Background = new SolidColorBrush(Colors.Gray);
                if (manualChoosen == 2)
                {
                    PlaceCarrier();
                }
            }
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
            if (!aiShipsCreated)
            {
                Button? button = sender as Button;
                var coordinates = button.Content.ToString();
                button.Background = new SolidColorBrush(Colors.Black);
            }
        }

        private void P2AttackClick(object sender, RoutedEventArgs e)
        {
            if (!aiShipsCreated)
            {
                Button? button = sender as Button;
                var coordinates = button.Content.ToString();
                Button? enemyB = (Button)P2Own.FindName("P1Field_" + coordinates);
                enemyB.Background = new SolidColorBrush(Colors.Black);
            }
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
                ChooseStarterAi();
            }
        }

        private void SaveP2_Click(object sender, RoutedEventArgs e)
        {
            gameSave.player2 = P2NameTB.Text;
            P2NameTB.Visibility = Visibility.Hidden;
            P2SaveB.Visibility = Visibility.Hidden;
            ChooseStarterPvP();
        }

        private void ChooseStarterAi()
        {
            p2Ships = aiBehav.GenerateShips();
            SetAiShips();
            var rand = new Random();
            int num = rand.Next(0, 2);
            if (num == 0)
            {
                MessageBox.Show("You start the game!");
                TopLabel.Content = gameSave.player1+" coose ship\nplacement mode!";
                AutoSetupB.Visibility = Visibility.Visible;
                ManualSetupB.Visibility = Visibility.Visible;
                currentPlayer = "p1";
                startingPlayer = "p1";
            }
            else
            {
                MessageBox.Show("The Ai starts the game!");
                TopLabel.Content = gameSave.player1 + " coose ship\nplacement mode!";
                AutoSetupB.Visibility = Visibility.Visible;
                ManualSetupB.Visibility = Visibility.Visible;
                currentPlayer = "p1";
                startingPlayer = "p2";
            }
        }

        private void ChooseStarterPvP()
        {
            var rand = new Random();
            int num = rand.Next(0, 2);
            if (num == 0)
            {
                MessageBox.Show(gameSave.player1 + " starts the game!");
                TopLabel.Content = gameSave.player1 + " coose ship\nplacement mode!";
                AutoSetupB.Visibility = Visibility.Visible;
                ManualSetupB.Visibility = Visibility.Visible;
                currentPlayer = "p1";
                startingPlayer = "p1";
            }
            else
            {
                MessageBox.Show(gameSave.player2 + " starts the game!");
                TopLabel.Content = gameSave.player2 + " coose ship\nplacement mode!";
                AutoSetupB.Visibility = Visibility.Visible;
                ManualSetupB.Visibility = Visibility.Visible;
                currentPlayer = "p2";
                startingPlayer = "p2";
            }
        }

        private void SetAiShips()
        {
            for (int i = 0; i < p2Ships.Length; i++)
            {
                Button button = (Button)P2Own.FindName("P2Field_" + p2Ships[i].R+"_"+p2Ships[i].C);
                button.Background = new SolidColorBrush(Colors.Black);
            }
            aiShipsCreated = true;
        }

        private void SetP1Ships()
        {
            for (int i = 0; i < 4; i++)
            {
                Button button = (Button)P1Own.FindName("P1Field_" + p1Ships[i].R + "_" + p1Ships[i].C);
                button.Background = new SolidColorBrush(Colors.Black);
            }
            p1ShipsCreated = true;
        }

        private void AutoSetup_Click(object sender, RoutedEventArgs e)
        {
            switch(currentPlayer)
            {
                case "p1":
                    p1Ships = aiBehav.GenerateShips();
                    SetP1Ships();
                    if(gameSave.gameMode == "PvAi")
                    {
                        ShipSetupReset();
                        P1Fog.Visibility = Visibility.Hidden;
                    }else if(startingPlayer == "p2"){
                        ShipSetupReset();
                        P2Fog.Visibility = Visibility.Hidden;
                    }else if(startingPlayer == "p1")
                    {
                        currentPlayer = "p2";
                        TopLabel.Content = gameSave.player2 + "coose ship placement mode!";
                    }
                    break;
                case "p2":
                    p2Ships = aiBehav.GenerateShips();
                    SetAiShips();
                    if(startingPlayer == "p2")
                    {
                        currentPlayer = "p1";
                        TopLabel.Content = gameSave.player1 + "coose ship placement mode!";
                    }else if (startingPlayer == "p1")
                    {
                        ShipSetupReset();
                        P1Fog.Visibility = Visibility.Hidden;
                    }
                    break;
            }
        }

        private void ManualSetup_Click(object sender, RoutedEventArgs e)
        {
            switch (currentPlayer)
            {
                case "p1":
                    ShipSetupReset();
                    TopLabel.Content = "Place Carrier(4)\n(first 2 places)";
                    P1Fog.Visibility = Visibility.Hidden;
                    break;
                case "p2":
                    ShipSetupReset();
                    TopLabel.Content = "Place Carrier(4)\n(first 2 places)";
                    P2Fog.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void ShipSetupReset()
        {
            TopLabel.Content = "Round 1";
            AutoSetupB.Visibility = Visibility.Hidden;
            ManualSetupB.Visibility = Visibility.Hidden;
        }

        private void PlaceCarrier()
        {
            Customs.Coordinate[] carrCords = new Customs.Coordinate[4];
            if (manualPlacer.CarrPlaceCheck(manualCords[0], manualCords[1]))
            {
                if (currentPlayer == "p1")
                {
                    carrCords = manualPlacer.Carrier(manualCords[0], manualCords[1]);
                    if(!carrCords[0].Equals(new Customs.Coordinate()))
                    {
                        p1Ships[0] = carrCords[0];
                        p1Ships[1] = carrCords[1];
                        p1Ships[2] = carrCords[2];
                        p1Ships[3] = carrCords[3];
                    }
                    SetP1Ships();
                }
                else
                {
                    carrCords = manualPlacer.Carrier(manualCords[0], manualCords[1]);
                    if (!carrCords[0].Equals(new Customs.Coordinate()))
                    {
                        p2Ships[0] = carrCords[0];
                        p2Ships[1] = carrCords[1];
                        p2Ships[2] = carrCords[2];
                        p2Ships[3] = carrCords[3];
                    }
                    SetAiShips();
                }
            }
            else
            {
                MessageBox.Show("Wrong coordinates!");
            }
        }

        private void PlaceDestr1()
        {

        }

        private void PlaceDestr2()
        {

        }

        private void PlaceHunter()
        {

        }
        
        private void ManualSetupFinish()
        {
            switch (currentPlayer)
            {
                case "p1":
                    SetP1Ships();
                    if (gameSave.gameMode == "PvAi")
                    {
                        ShipSetupReset();
                        P1Fog.Visibility = Visibility.Hidden;
                    }
                    else if (startingPlayer == "p2")
                    {
                        ShipSetupReset();
                        P2Fog.Visibility = Visibility.Hidden;
                    }
                    else if (startingPlayer == "p1")
                    {
                        currentPlayer = "p2";
                        TopLabel.Content = gameSave.player2 + "coose ship placement mode!";
                    }
                    break;
                case "p2":
                    SetAiShips();
                    if (startingPlayer == "p2")
                    {
                        currentPlayer = "p1";
                        TopLabel.Content = gameSave.player1 + "coose ship placement mode!";
                    }
                    else if (startingPlayer == "p1")
                    {
                        ShipSetupReset();
                        P1Fog.Visibility = Visibility.Hidden;
                    }
                    break;
            }
        }

    }
}
