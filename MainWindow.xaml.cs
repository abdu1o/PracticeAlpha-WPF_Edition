using PracticeAlpha_WPF_Edition.SoundControl;
using PracticeAlpha_WPF_Edition;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

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
            buttonSound.PlayAsync();

            //var multiplayMenu = new MultiplayMenu();
            //Application.Current.MainWindow = multiplayMenu;

            //this.Close();
            //multiplayMenu.Show();
        }
        //--=========================Click Play===========================--
    }
}
