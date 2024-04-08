using Microsoft.SqlServer.Server;
using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public partial class YouDead : Window
    {
        private SoundController buttonSound;

        public YouDead()
        {
            InitializeComponent();

            Closing += YouDead_Closing;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void TryAgain_Click(object sender, MouseButtonEventArgs e)
        {
            //buttonSound = new SoundController("Sounds\\button_click.mp3");
            //buttonSound.PlayAsync();

            Level1 new_level = new Level1();
            new_level.Show();

            Level1 old_level = Application.Current.Windows.OfType<Level1>().FirstOrDefault();
            old_level.Close();
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            //buttonSound = new SoundController("Sounds\\button_click.mp3");
            //buttonSound.PlayAsync();

            MainWindow menu = new MainWindow();
            menu.Show();

            Level1 old_level = Application.Current.Windows.OfType<Level1>().FirstOrDefault();
            old_level.Close();
        }

        private void YouDead_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Level1 old_level = Application.Current.Windows.OfType<Level1>().FirstOrDefault();
            old_level.Close();
        }
    }
}
