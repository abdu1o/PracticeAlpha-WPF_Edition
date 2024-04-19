using PracticeAlpha_WPF_Edition.SoundControl;
using PracticeAlpha_WPF_Edition;
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
        private SoundController buttonSound;

        public MainWindow()
        {
            InitializeComponent();

            
            //MusicController.Initialize("Sounds\\mainMenu.mp3");
            //MusicController.Play();
            //MusicController.SetVolume(0.5);

            //buttonSound = new SoundController("Sounds\\button_click.mp3");
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
            //buttonSound.PlayAsync();
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
            //buttonSound.PlayAsync();

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

            //var multiplayMenu = new MultiplayMenu();
            //Application.Current.MainWindow = multiplayMenu;

            //this.Close();
            //multiplayMenu.Show();
        }
        //--=========================Click Play===========================--

        //--=========================Score===========================--
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 10;
            win.Effect = objBlur;
        }

        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        private void ClickScore(object sender, RoutedEventArgs e)
        {
            //buttonSound.PlayAsync();
            string connectionString = "Data Source=Player.db;Version=3;";
            List<String> arr = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT Login.Name, Score.Points FROM Score LEFT JOIN Login ON Score.Player_ID = Login.ID";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = reader.GetString(0);
                            string Score = Convert.ToString(reader.GetValue(1));
                            arr.Add(Name + "\t\t" + Score);
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
            //buttonSound.PlayAsync();
            ClearEffect(this);
        }

        private void ScoreClose(object sender, RoutedEventArgs e)
        {
            ScorePopUp.IsOpen = false;
        }

        //--=========================Score===========================--

    }
}
