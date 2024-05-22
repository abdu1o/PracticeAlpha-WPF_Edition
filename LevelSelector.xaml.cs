using Microsoft.Win32;
using PracticeAlpha_WPF_Edition.Levels;
using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PracticeAlpha_WPF_Edition
{
    public partial class LevelSelector : Window
    {
        private List<string> _levelsPicture = new List<string>();
        private int _countLevel;
        private int _currentLevel;

        private string customMusic = " ";

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
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

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
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

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
            if(_currentLevel == 1)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

                if (customMusic == " ")
                {
                    var level1 = new Level1();
                    Application.Current.MainWindow = level1;

                    this.Close();
                    level1.Show();
                }
                else
                {
                    var level1 = new Level1(customMusic);
                    Application.Current.MainWindow = level1;

                    this.Close();
                    level1.Show();
                }
            }
            else if (_currentLevel == 2)
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

                if (customMusic == " ")
                {
                    var level2 = new Level2();
                    Application.Current.MainWindow = level2;

                    this.Close();
                    level2.Show();
                }
                else
                {
                    var level2 = new Level2(customMusic);
                    Application.Current.MainWindow = level2;

                    this.Close();
                    level2.Show();
                }
            }
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

        private void ButtonMusic_MouseEnter(object sender, MouseEventArgs e)
        {
            MusicButton.Foreground = new SolidColorBrush(Colors.Red);
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void ButtonMusic_MouseLeave(object sender, MouseEventArgs e)
        {
            MusicButton.Foreground = new SolidColorBrush(Colors.White);
            Mouse.OverrideCursor = null;
        }

        private void DefaultMusic_MouseEnter(object sender, MouseEventArgs e)
        {
            DefaultButton.Foreground = new SolidColorBrush(Colors.Red);
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void DefaultMusic_MouseLeave(object sender, MouseEventArgs e)
        {
            DefaultButton.Foreground = new SolidColorBrush(Colors.White);
            Mouse.OverrideCursor = null;
        }

        private void SelectMusic_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы MP3 (*.mp3)|*.mp3|Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                customMusic = openFileDialog.FileName;
            }
            InfoText.Text = "Current music: " + customMusic;
        }

        private void DefaultMusic_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            customMusic = " ";
            InfoText.Text = "Music restored to default";
        }

        private void DownloadFile(string url, string savePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, savePath);
                    MessageBox.Show($"File successfully downloaded");
                }
                catch (WebException ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}");
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;
            string savePath = "C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Resource packs\\";

            DownloadFile(url, savePath);
        }
    }
}
