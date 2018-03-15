using NAudio.Wave;

public class AudioSourceManager
{
    WaveOut waveOutDevice;

    public AudioSourceManager()
    {
        waveOutDevice = new WaveOut();
        waveOutDevice.Volume = .5f;
    }

    public void AddSound(string url)
    {
        AudioFileReader audioFileReader = new AudioFileReader(@url);
        waveOutDevice.Init(audioFileReader);
        Play();
    }

    public void Play()
    {
        waveOutDevice.Play();
    }

    public void Stop()
    {
        waveOutDevice.Stop();
    }

    public float Volume
    {
        set
        {
            if (value < 0)
                value = 0;
            if (value > 1)
                value = 1;
            waveOutDevice.Volume = value;
        }
        get { return waveOutDevice.Volume; }
    }
}
