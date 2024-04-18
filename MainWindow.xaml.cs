﻿using PracticeAlpha_WPF_Edition.SoundControl;
using PracticeAlpha_WPF_Edition.Levels;
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
        public MainWindow()
        {
            InitializeComponent();

            //norm puti ne rabotayut!!!!!!!!!!!!!!!!!!
            Music.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\mainMenu.mp3");
            //norm puti ne rabotayut!!!!!!!!!!!!!!!!!!
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
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");
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
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            var levelSelector = new LevelSelector();
            Application.Current.MainWindow = levelSelector;

            this.Close();    
            levelSelector.Show();
        }
        //--=========================Click Play===========================--

        //--=========================Click Multiplay===========================--
        private void ClickMultiplay(object sender, RoutedEventArgs e)
        {
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\button_click.mp3");

            var level1_Multiplayer = new Level1_Multiplayer();
            Application.Current.MainWindow = level1_Multiplayer;

            this.Close();
            level1_Multiplayer.Show();
        }
        //--=========================Click Play===========================--
    }
}
