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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeAlpha_WPF_Edition
{
    public partial class MainWindow : Window
    {
        private MusicController menuMusic;

        public MainWindow()
        {
            InitializeComponent();

            menuMusic = new MusicController("Music\\mainMenu.mp3");
            menuMusic.Play();

            musicVolume.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            menuMusic.SetVolume(musicVolume.Value);
        }
    }
}
