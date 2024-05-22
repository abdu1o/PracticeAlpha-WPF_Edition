using PracticeAlpha_WPF_Edition.SoundControl;
using PracticeAlpha_WPF_Edition.Levels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Threading;
using System.Data.SQLite;
using System.Collections.Generic;
using PracticeAlpha_WPF_Edition.PlayerDataSave;
using System.ComponentModel;
using System.Windows.Data;

namespace PracticeAlpha_WPF_Edition
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if(PlayerInfo.isFirstStart) 
            {
                LoginPopUp.IsOpen = true;
                ApplyEffect(this);
            }

            Music.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\mainMenu.mp3", 0.07);
        }

        //--=========================Button Events========================--
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = (Button)sender;
            Mouse.OverrideCursor = Cursors.Hand;

            button.Effect = new DropShadowEffect
            {
                Direction = 270,
                Color = Colors.Red,
                ShadowDepth = 5,
                BlurRadius = Math.Max(button.RenderSize.Width, button.RenderSize.Height) / 10
            };
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;

            ((Button)sender).Effect = new DropShadowEffect
            {
                Color = Color.FromArgb(0, 99, 0, 0),
                ShadowDepth = 0,
                BlurRadius = 0
            };
        }

        private async void CloseClick(object sender, MouseButtonEventArgs e)
        {
            if (LoginPopUp.IsOpen == false)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3", 0.2);
                await Task.Delay(300);
                this.Close();
            }
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ToolTip = null;
        }

        private async void HideClick(object sender, MouseButtonEventArgs e)
        {
            if (LoginPopUp.IsOpen == false)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3", 0.2);
                await Task.Delay(300);
                this.WindowState = WindowState.Minimized;
            }
        }

        //--=========================Button Events========================--

        //--=========================Click Play===========================--
        private void ClickPlay(object sender, RoutedEventArgs e)
        {
            if (LoginPopUp.IsOpen == false)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3", 0.2);

                var levelSelector = new LevelSelector();
                Application.Current.MainWindow = levelSelector;

                this.Close();
                levelSelector.Show();
            }
        }
        //--=========================Click Play===========================--

        //--=========================Click Multiplay===========================--
        private void ClickMultiplay(object sender, RoutedEventArgs e)
        {
            if (LoginPopUp.IsOpen == false)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3", 0.2);

                var level1_Multiplayer = new Level1_Multiplayer();
                Application.Current.MainWindow = level1_Multiplayer;

                this.Close();
                level1_Multiplayer.Show();
            }
        }
        //--=========================Click Play===========================--

        //--=========================Score===========================--
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 15;
            win.Effect = objBlur;
        }

        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        private void ClickScore(object sender, RoutedEventArgs e)
        {
            if (LoginPopUp.IsOpen == false)
            {
                //string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";

                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

                string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;"; //"Data Source=C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
                List<DBItem> arr = new List<DBItem>();
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT Login.Name, Score.Points, Score.Time FROM Score LEFT JOIN Login ON Score.Player_ID = Login.ID";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DBItem item = new DBItem();
                                string Name = reader.GetString(0);
                                string Points = reader.GetString(1);
                                string Time = reader.GetString(2);
                                item.Name = Name;
                                item.Points = Points;
                                item.Time = Time;
                                arr.Add(item);
                            }
                            ScoreList.ItemsSource = arr;
                            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ScoreList.ItemsSource);
                            view.SortDescriptions.Add(new SortDescription("Points", ListSortDirection.Descending));
                        }
                    }
                    connection.Close();
                    ScorePopUp.IsOpen = true;
                    ApplyEffect(this);
                }
            }
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ScoreList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
        }

        private void SortByPoint(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ScoreList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Points", ListSortDirection.Descending));
        }

        private void SortByTime(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ScoreList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Descending));
        }

        private void CloseScore(object sender, EventArgs e)
        {
            //Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");
            //buttonSound.PlayAsync();
            ClearEffect(this);
        }

        private void ScoreClose(object sender, RoutedEventArgs e)
        {
            ScorePopUp.IsOpen = false;
        }

        private void LightThemeEnable(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sb = new SolidColorBrush(Colors.White);
            ScoreList.Background = sb;
            ScoreTheme.Background = sb;
            NameC.Background = sb;
            PointsC.Background = sb;
            TimeC.Background = sb;
            sb = new SolidColorBrush(Colors.Black);
            ScoreList.Foreground = sb;
            NameC.Foreground = sb;
            PointsC.Foreground = sb;
            TimeC.Foreground = sb;
        }

        private void LightThemeDisable(object sender, RoutedEventArgs e)
        {
            Color color = new Color(); color.R = 18; color.G = 18; color.B = 18;
            SolidColorBrush sb = new SolidColorBrush(color);
            ScoreList.Background = sb;
            ScoreTheme.Background = sb;
            NameC.Background = sb;
            PointsC.Background = sb;
            TimeC.Background = sb;
            sb = new SolidColorBrush(Colors.DarkRed);
            ScoreList.Foreground = sb;
            NameC.Foreground = sb;
            PointsC.Foreground = sb;
            TimeC.Foreground = sb;
        }

        //--=========================Score===========================--

        //--=========================Login===========================--

        private void LoginSend(object sender, RoutedEventArgs e)
        {
            if (LoginName.Text.Length > 0)
            {
                int PID = -1;
                //string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";

                //"Data Source=C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
                string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $"SELECT ID FROM Login WHERE Name = '{LoginName.Text}'";

                        PlayerInfo.Name = LoginName.Text;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PID = reader.GetInt32(0);
                            }
                        }
                        if (PID == -1)
                        {
                            command.CommandText = $"INSERT INTO Login (Name) VALUES ('{LoginName.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"SELECT ID FROM Login WHERE Name = '{LoginName.Text}'";
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    PID = reader.GetInt32(0);
                                }
                            }
                            command.CommandText = $"INSERT INTO Score (Player_ID, Points, Time) VALUES ({PID}, 0, 0)";
                            command.ExecuteNonQuery();
                            LoginPopUp.IsOpen = false;
                        }
                        else
                        {
                            LoginPopUp.IsOpen = false;
                        }
                    }
                    connection.Close();
                    PlayerInfo.isFirstStart = false;
                }
            }
        }

        private void CloseLogin(object sender, EventArgs e)
        {
            ClearEffect(this);
        }


        //--=========================Login===========================--

    }
}
