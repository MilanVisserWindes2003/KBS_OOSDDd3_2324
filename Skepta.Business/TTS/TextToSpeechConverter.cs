using System.Speech.Synthesis;

namespace Buisness.TTS;

public enum SpeedValue
{
    Sloom = -2,
    Normaal = 0,
    Snel = 2
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
    private int volume = 5;
    public TextToSpeechConverter()
    {
        synthesizer = new SpeechSynthesizer();
        Setup();
        synthesizer.SetOutputToDefaultAudioDevice();
        SetVoice("Nederlands");
        synthesizer.Volume = volume;
        synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
    }

    private void Synthesizer_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
    {
        PlayMode = PlayMode.Stopped;
    }

    public IList<SpeedValue> SpeedValues { get; private set; }
    public SpeedValue SpeedValue { get; set; } = SpeedValue.Normaal;
    public PlayMode PlayMode { get; private set; } = PlayMode.Stopped;
    public List<string> Voices { get; } = new List<string>();
    public int Volume { 
        get
        {
            return volume;
        }
        set
        {
            SetVolume(value);
        }
    }
    
    private Dictionary<string, string> LanguageOptions { get; } = new Dictionary<string, string>
{
    { "Nederlands", "Microsoft Frank" },
    { "Belgisch", "Microsoft Bart" }
};

    public string Voice {
        get => voice;
        set => SetVoice(value);
    }

    //Plays the the given text
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

    //Pauses the playing text if there is a text playing
    public void Pause()
    {
        if (PlayMode == PlayMode.Playing)
        {
            synthesizer.Pause();
            PlayMode = PlayMode.Paused;
        }
    }

    //Resumes the playing text after the text has been paused
    public void Resume()
    {
        if (PlayMode == PlayMode.Paused)
        {
            synthesizer.Resume();
            PlayMode = PlayMode.Playing;
        }
    }

    //Stops the playing text
    public void Stop()
    {
        if (PlayMode != PlayMode.Stopped)
        {
            synthesizer.SpeakAsyncCancelAll();
            PlayMode = PlayMode.Stopped;
        }
    }

    //Changes the voice to the given voice
    public void SetVoice(string voice)
    {
        if (!LanguageOptions.TryGetValue(voice, out string value))
        {
            return;
        }
        string Speaker = LanguageOptions[voice];
        if (CheckVoiceExists(Speaker))
        {
            synthesizer.SelectVoice(Speaker);
            this.voice = voice;
        }

    }

    //Sets The volume to the given volume *10, volume has to be between 0 and 10
    public void SetVolume(int volume)
    {
        if (volume < 0)
        {
            this.volume = 0;
            synthesizer.Volume = 0;
            return;
        }
        if (volume > 10)
        {
            this.volume = 10;
            synthesizer.Volume = 100;
            return;
        }

        synthesizer.Volume = volume*10;
        this.volume = volume;
    }

    //Checks if the given voice exists on your computer
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

    //Adds the used voices to Voices
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
