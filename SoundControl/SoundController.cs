using System;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    public class SoundController
    {
        private IWavePlayer player;
        private AudioFileReader audioFileReader;
        private string sound;

        public SoundController(string sound)
        {
            this.sound = sound;
            InitializeAudio();
        }

        private void InitializeAudio()
        {
            player = new WaveOutEvent();
            player.PlaybackStopped += (sender, args) =>
            {
                player.Stop();
                audioFileReader.Position = 0;
            };

            audioFileReader = new AudioFileReader(this.sound);
            player.Init(audioFileReader);
        }

        public async Task PlayAsync()
        {
            await Task.Run(() => player.Play());
        }

        public void Stop()
        {
            player.Stop();
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                player.Volume = (float)volume;
            }
        }
    }
}
