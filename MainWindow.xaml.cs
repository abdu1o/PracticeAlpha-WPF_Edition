using PracticeAlpha_WPF_Edition.Levels;
using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeAlpha_WPF_Edition
{
    public partial class MainWindow : Window
    {
        private MusicController menuMusic;
        private SoundController buttonSound;
        public double soundVol = 0.5;
        public double musicVol = 0.5;

        public MainWindow()
        {
            InitializeComponent();

            menuMusic = new MusicController("Music\\mainMenu.mp3"); //не меняй путь
            menuMusic.SetVolume(musicVol);
            menuMusic.Play();

            buttonSound = new SoundController("Sounds\\button_click.mp3");
            buttonSound.SetVolume(soundVol);
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
            buttonSound.PlayAsync();
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
            buttonSound.PlayAsync();
            menuMusic.Stop();

            var levelSelector = new LevelSelector();
            levelSelector.soundVol = soundVol;
            levelSelector.musicVol = musicVol;
            Application.Current.MainWindow = levelSelector;

            this.Close();
            levelSelector.Show();
        }

        //--=========================Click Play===========================--

        //--=========================Settings===========================--

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

        private void ClickSettings(object sender, RoutedEventArgs e)
        {
            buttonSound.PlayAsync();
            SettingsPopUp.IsOpen = true;
            ApplyEffect(this);
        }

        private void CloseSettings(object sender, EventArgs e)
        {
            buttonSound.PlayAsync();
            ClearEffect(this);
        }

        private void GenVolChng(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Mute.IsChecked == false)
            {
                musicVol = ((double)((MusVol.Value / 100) * (GenVol.Value / 100)));
                menuMusic.SetVolume(musicVol);
                soundVol = ((double)((SoundVol.Value / 100) * (GenVol.Value / 100)));
                buttonSound.SetVolume(soundVol);
            }
        }

        private void MusVolChng(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Mute.IsChecked == false)
            {
                musicVol = ((double)((MusVol.Value / 100) * (GenVol.Value / 100)));
                menuMusic.SetVolume(musicVol);
            }
        }

        private void SoundVolChng(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Mute.IsChecked == false)
            {
                soundVol = ((double)((SoundVol.Value / 100) * (GenVol.Value / 100)));
                buttonSound.SetVolume(soundVol);
                buttonSound.PlayAsync();
            }
        }

        private void MuteSet(object sender, RoutedEventArgs e)
        {
            musicVol = 0.0001;
            menuMusic.SetVolume(musicVol);
            soundVol = 0.0001;
            buttonSound.SetVolume(soundVol);
        }

        private void MuteUnSet(object sender, RoutedEventArgs e)
        {
            musicVol = ((double)((MusVol.Value / 100) * (GenVol.Value / 100)));
            menuMusic.SetVolume(musicVol);
            soundVol = ((double)((SoundVol.Value / 100) * (GenVol.Value / 100)));
            buttonSound.SetVolume(soundVol);
        }

        private void SettingsClose(object sender, RoutedEventArgs e)
        {
            SettingsPopUp.IsOpen = false;
        }

        //--=========================Settings===========================--


    }
}
