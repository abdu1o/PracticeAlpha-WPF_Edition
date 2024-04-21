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

namespace PracticeAlpha_WPF_Edition
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginPopUp.IsOpen = true;
            //Music.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\mainMenu.mp3");
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
            //Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");
            await Task.Delay(300);
            this.Close();
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
        //--=========================Button Events========================--

        //--=========================Click Play===========================--
        private void ClickPlay(object sender, RoutedEventArgs e)
        {
            //Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            var levelSelector = new LevelSelector();
            Application.Current.MainWindow = levelSelector;

            this.Close();
            levelSelector.Show();
        }
        //--=========================Click Play===========================--

        //--=========================Click Multiplay===========================--
        private void ClickMultiplay(object sender, RoutedEventArgs e)
        {
            //buttonSound.PlayAsync();
            //Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            var level1_Multiplayer = new Level1_Multiplayer();
            Application.Current.MainWindow = level1_Multiplayer;

            this.Close();
            level1_Multiplayer.Show();
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
            string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
            
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

           // string connectionString = "Data Source=C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
            List<String> arr = new List<string>();
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
                            string Name = reader.GetString(0);
                            string Points = reader.GetString(1);
                            string Time = reader.GetString(2);
                            arr.Add(Name + "\t\t" + Points + "\t\t" + Time);
                        }
                        ScoreList.ItemsSource = arr;
                    }
                }
                connection.Close();
                ScorePopUp.IsOpen = true;
                ApplyEffect(this);
            }
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

        //--=========================Score===========================--

        //--=========================Login===========================--

        private void LoginSend(object sender, RoutedEventArgs e)
        {
            if (LoginName.Text.Length > 0)
            {
                int PID = -1;
                string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
                // string connectionString = "Data Source=C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $"SELECT ID FROM Login WHERE Name = '{LoginName.Text}'";
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
                }
            }
        }

        private void CloseLogin(object sender, EventArgs e)
        {

        }

        //--=========================Login===========================--

    }
}
