using Microsoft.SqlServer.Server;
using PracticeAlpha_WPF_Edition.SoundControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        private bool pause;
        private Type level = null;

        public YouDead(bool isPause, Type level)
        {
            pause = isPause;
            this.level = level;

            InitializeComponent();

            if (isPause)
            {
                mainLabel.Content = "Pause";
                TryAgain.Content = "Continue";
            }

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
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            if (pause)
            {
                YouDead pauseWindow = Application.Current.Windows.OfType<YouDead>().FirstOrDefault();
                pauseWindow.Close();
            }
            else
            {
                object new_level = Activator.CreateInstance(level);
                
                if (new_level is Window)
                {
                    ((Window)new_level).Show();
                }

                CurrentLevelClose();
            }
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            MainWindow menu = new MainWindow();
            menu.Show();

            CurrentLevelClose();
        }

        private void YouDead_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!pause)
            {
                CurrentLevelClose();
            }
        }

        private void CurrentLevelClose()
        {
            Window oldLevel = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.GetType() == level);
            if (oldLevel != null)
            {
                oldLevel.Close();
            }
        }
    }
}
