using PracticeAlpha_WPF_Edition.Levels;
using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace PracticeAlpha_WPF_Edition
{
    public partial class LevelSelector : Window
    {
        private SoundController buttonSound;
        private List<string> _levelsPicture = new List<string>();
        private int _countLevel;
        private int _currentLevel;

        public LevelSelector()
        {
            _countLevel = 3;
            _currentLevel = 1;

            InitializeComponent();
            InitializeLevelsPicture();

            MouseEnter += Button_MouseEnter;
            MouseLeave += Button_MouseLeave;

            MouseEnter += Arrow_MouseEnter;
            MouseLeave += Arrow_MouseLeave;

            CheckEnable();

        }

        private void InitializeLevelsPicture()
        {
            _levelsPicture.Add("Resources/LevelsPicture/Level1.png");
            _levelsPicture.Add("Resources/LevelsPicture/Level2.png");
            _levelsPicture.Add("Resources/LevelsPicture/Level3.png");
        }


        private void CheckEnable() // Kostili
        {       
            if (_countLevel <= 1)
            {
                NextButton.IsEnabled = false;
                PreviousButton.IsEnabled = false;
            }

            if (_currentLevel <= 1)
            {
                PreviousButton.IsEnabled = false;
            }
        }

        // --============ Buttons ============--
        
        private void NextLevel(object sender, RoutedEventArgs e)
        {
            buttonSound = new SoundController("Sounds\\button_click.mp3");
            buttonSound.PlayAsync();

            if (_currentLevel + 1 >= _countLevel)
            {
                var button = (Button)sender;
                button.IsEnabled = false;
            }

            PreviousButton.IsEnabled = true;

            _currentLevel++;
          

            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(_levelsPicture[_currentLevel - 1], UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            LevelImage.Source = bitmapImage;
            LevelText.Text = "STAGE " + _currentLevel.ToString();
        }

        private void PreviousLevel(object sender, RoutedEventArgs e)
        {
            buttonSound = new SoundController("Sounds\\button_click.mp3");
            buttonSound.PlayAsync();


            if (_currentLevel - 1 <= 1)
            {
                var button = (Button)sender;
                button.IsEnabled = false;
            }

            NextButton.IsEnabled = true;

            _currentLevel--;

            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(_levelsPicture[_currentLevel - 1], UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            LevelImage.Source = bitmapImage;
            LevelText.Text = "STAGE " + _currentLevel.ToString();
        }

        private void Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            LevelText.Foreground = new SolidColorBrush(Colors.Red);
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, RoutedEventArgs e)
        {
            LevelText.Foreground = new SolidColorBrush(Colors.White);
            Mouse.OverrideCursor = null;
        }

        private void Arrow_MouseEnter(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Arrow_MouseLeave(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void ChooseLevel(object sender, RoutedEventArgs e)
        {
            buttonSound = new SoundController("Sounds\\button_click.mp3");
            buttonSound.PlayAsync();

            var level1 = new Level1();
            Application.Current.MainWindow = level1;

            this.Close();
            level1.Show();
        }


        // --============= Exit ==============--

        private void Window_Exit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) 
            {
                var mainWindow = new MainWindow();
                App.Current.MainWindow = mainWindow;
                this.Close();
                mainWindow.Show();
            }
        }

        // --============= Exit ==============--

    }
}
