using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    static class Sound
    {
        public static void Play(string path)
        {
            SoundController.Initialize(path);
            SoundController.Play();
            SoundController.SetVolume(0.5);
        }

        public static void Play(string path, double volume)
        {
            SoundController.Initialize(path);
            SoundController.Play();
            SoundController.SetVolume(volume);
        }
    }

    static class SoundController
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static string sound;

        public static void Initialize(string path)
        {
            sound = path;
            mediaPlayer.Open(new Uri(sound, UriKind.Relative));
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
    }
}
