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

        public MainWindow()
        {
            InitializeComponent();

            menuMusic = new MusicController("Resources\\Sounds\\mainMenu.mp3");
            menuMusic.Play();
        }


        //--=========================Button Events========================--
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button)sender).Effect = new DropShadowEffect
            {
                Color = Colors.Red,
                Direction = 320,
                ShadowDepth = 5,
                BlurRadius = 10
            };
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button)sender).Effect = new DropShadowEffect
            {
                Color = Colors.Transparent,
                Direction = 320,
                ShadowDepth = 0,
                BlurRadius = 0
            };
        }
        //--=========================Button Events========================--

    }
}
