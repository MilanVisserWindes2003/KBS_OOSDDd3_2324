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
    private string voice;
    public TextToSpeechConverter()
    {
        
        synthesizer = new SpeechSynthesizer();
        Setup();
        synthesizer.SetOutputToDefaultAudioDevice();
        SetVoice("Nederlands");
        synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
    }

    private void Synthesizer_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
    {
        PlayMode = PlayMode.Stopped;
    }

    public IList<SpeedValue> SpeedValues { get; private set; }
    public SpeedValue SpeedValue { get; set; } = SpeedValue.Normal;
    public PlayMode PlayMode { get; private set; } = PlayMode.Stopped;
    public List<string> Voices { get;} = new List<string>();
    private Dictionary<string, string> LanguageOptions { get; } = new Dictionary<string, string>
{
    { "Nederlands", "Microsoft Frank" },
    { "Belgisch", "Microsoft Bart" }
    // Add more languages and voices as needed
};

    public string Voice {
        get => voice;
        set => SetVoice(value);
    }


    public bool PlayText(string text)
    {
        if (PlayMode != PlayMode.Stopped)
        {
            return false;
        }
        synthesizer.Rate = (int)SpeedValue;
        _ = synthesizer.SpeakAsync(text);
        PlayMode = PlayMode.Playing;
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
            synthesizer.SpeakAsyncCancelAll();
            PlayMode = PlayMode.Stopped;
        }
    }

    public void SetVoice(string voice)
    {
        string Speaker = LanguageOptions[voice];
        if (CheckVoiceExists(Speaker)) 
        {
            synthesizer.SelectVoice(Speaker);
            voice = Speaker;
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

    private bool CheckVoiceExists (string voice)
    {
        foreach (InstalledVoice voices in synthesizer.GetInstalledVoices())
        {
            VoiceInfo info = voices.VoiceInfo;
            if (info.Name.Equals(voice))
            {
                return true;
            }
        }
        return false;
    }

    private void Setup() 
    {
        foreach (InstalledVoice voices in synthesizer.GetInstalledVoices())
        {
            VoiceInfo info = voices.VoiceInfo;
            string Name = info.Name;
            if (Name == "Microsoft Frank" || Name == "Microsoft Bart")
            {
                var keyValuePair = LanguageOptions.FirstOrDefault(x => x.Value == Name);
                if (!Voices.Contains(keyValuePair.Key)) 
                {
                    Voices.Add(keyValuePair.Key);
                }               
            }
        }
    }
}
