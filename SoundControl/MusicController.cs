using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    public class MusicController
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private string music;

        public MusicController(string music)
        {
            this.music = music;
            mediaPlayer.MediaEnded += MainMenuLoop;
            mediaPlayer.Open(new Uri(this.music, UriKind.Relative));
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

        private void MainMenuLoop(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
