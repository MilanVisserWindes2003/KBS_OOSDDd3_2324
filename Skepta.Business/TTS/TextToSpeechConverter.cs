using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Buisness.TTS;

public enum SpeedValue
{
    Slow = -2,
    Normal = 0,
    Fast = 2
}

public enum PlayMode
{
    Stopped,
    Playing,
    Paused
}
public class TextToSpeechConverter
{
    private readonly SpeechSynthesizer synthesizer;
    public TextToSpeechConverter()
    {
        synthesizer = new SpeechSynthesizer();
        synthesizer.SetOutputToDefaultAudioDevice();
        synthesizer.SelectVoice("Microsoft Frank");
        synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
    }

    private void Synthesizer_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
    {
        PlayMode = PlayMode.Stopped;
    }

    public IList<SpeedValue> SpeedValues { get; private set; }
    public SpeedValue SpeedValue { get; set; } = SpeedValue.Normal;
    public PlayMode PlayMode { get; private set; } = PlayMode.Stopped;

    public bool PlayText(string text)
    {
        if (PlayMode != PlayMode.Stopped)
        {
            return false;
        }

        synthesizer.Rate = (int)SpeedValue;
        _ = synthesizer.SpeakAsync(text);
        return true;
    }

    public void Pause()
    {
        if (PlayMode == PlayMode.Playing)
        {
            synthesizer.Pause();
            PlayMode = PlayMode.Paused;
        }
    }
    public void Resume()
    {
        if (PlayMode == PlayMode.Paused)
        {
            synthesizer.Resume();
            PlayMode = PlayMode.Playing;
        }
    }

    public void Stop()
    {
        if (PlayMode != PlayMode.Stopped)
        {
            synthesizer.Pause();
            PlayMode = PlayMode.Stopped;
        }
    }

    private void SetupSpeedValues()
    {
        List<SpeedValue> speedValues = new List<SpeedValue>();
        foreach (SpeedValue value in Enum.GetValues(typeof(SpeedValue)))
        {
            speedValues.Add(value);
        }
        SpeedValues = speedValues;
    }
}
