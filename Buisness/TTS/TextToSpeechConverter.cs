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
internal class TextToSpeechConverter
{
    private readonly SpeechSynthesizer synthesizer;
    public TextToSpeechConverter()
    {
        synthesizer = new SpeechSynthesizer();
        synthesizer.SetOutputToDefaultAudioDevice();
        synthesizer.SelectVoice("Microsoft Frank");
    }

    public List<string> ToSpeech() { }
}
