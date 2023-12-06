using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    public class SoundController
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private string sound;

        public SoundController(string sound)
        {
            this.sound = sound;
            mediaPlayer.MediaEnded += SoundByClick;
            mediaPlayer.Open(new Uri(this.sound, UriKind.Relative));
        }

        public void Play()
        {
            mediaPlayer.Play();
        }

        public void Stop()
        {
            mediaPlayer.Stop();
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                mediaPlayer.Volume = volume;
            }
        }

        private void SoundByClick(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Position = TimeSpan.Zero;
        }
    }
}
