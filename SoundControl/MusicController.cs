using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    static class Music
    {
        public static void Play(string path)
        {
            MusicController.Initialize(path);
            MusicController.Play();
            MusicController.SetVolume(0.5);
        }

        public static void Play(string path, double volume)
        {
            MusicController.Initialize(path);
            MusicController.Play();
            MusicController.SetVolume(volume);
        }
    }

    public static class MusicController
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static string music;

        static MusicController()
        {
            mediaPlayer.MediaEnded += MainMenuLoop;
        }

        public static void Initialize(string musicPath)
        {
            music = musicPath;
            mediaPlayer.Open(new Uri(music, UriKind.Relative));
        }

        public static void Play()
        {
            mediaPlayer.Play();
        }

        public static void Stop()
        {
            mediaPlayer.Stop();
        }

        public static void SetVolume(double volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                mediaPlayer.Volume = volume;
            }
        }

        private static void MainMenuLoop(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
