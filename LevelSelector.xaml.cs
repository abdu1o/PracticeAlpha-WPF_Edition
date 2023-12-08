using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
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
            buttonSound = new SoundController("Sounds\\button_click.mp3");

            InitializeComponent();
            InitializeLevelsPicture();

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
            buttonSound.Play();

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
        }

        private void PreviousLevel(object sender, RoutedEventArgs e)
        {
            buttonSound.Play();


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
        }


        private void ChooseLevel(object sender, RoutedEventArgs e)
        {
            // Code...
        }
    }
}
