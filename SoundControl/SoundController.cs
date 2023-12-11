using System;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.SoundControl
{
    public class SoundController
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private string sound;

        public SoundController(string sound)
        {
            this.sound = sound;
            InitializeAudio();
        }

        private void InitializeAudio()
        {
            waveOutDevice = new WaveOutEvent();
            waveOutDevice.PlaybackStopped += (sender, args) =>
            {
                waveOutDevice.Stop();
                audioFileReader.Position = 0;
            };

            audioFileReader = new AudioFileReader(this.sound);
            waveOutDevice.Init(audioFileReader);
        }

        public async Task PlayAsync()
        {
            await Task.Run(() => waveOutDevice.Play());
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                waveOutDevice.Volume = (float)volume;
            }
        }
    }
}
