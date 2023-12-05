using Buisness.TTS;
using Skepta.Business;
using Skepta.Business.Util;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.ViewModel;

public class TextToSpeechViewModel : ViewModelBase
{

    private readonly SkeptaModel model;
    private readonly Stopwatch stopwatch = new Stopwatch();
    private DateTime lastRenderTime;

    private StringBuilder userInput = new StringBuilder();
    private string inputText = string.Empty;
    private int aantalTekens;
    private string elapsedTimeText;

    private string randomText;


    public TextToSpeechViewModel(SkeptaModel model)
    {
        this.model = model;
        stopwatch = new Stopwatch();
        SpeedOptions = Enum.GetValues<SpeedValue>();
        LanguageOptions = model.TTSConverter.Voices.ToArray();
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }
    public ICommand Speak => new BaseCommand(SpeakCmd, () => model.TTSConverter.PlayMode == PlayMode.Stopped);

    public ICommand Pause => new BaseCommand(PauseCmd, () => model.TTSConverter.PlayMode != PlayMode.Stopped);

    public string PauzeText
    {
        get => model.TTSConverter.PlayMode == PlayMode.Playing ? "Pauze" : model.TTSConverter.PlayMode == PlayMode.Paused ? "Resume" : "-";
    }

    public string RandomText
    {
        get => randomText;
        private set { randomText = value; }
    }
    public string InputText
    {
        get => inputText;
        set
        {
            inputText = value;
        }
    }
    public string ElapsedTimeText
    {
        get => elapsedTimeText;
        set
        {
            if (elapsedTimeText != value)
            {
                elapsedTimeText = value;
                NotifyPropertyChanged(nameof(ElapsedTimeText));
            }
        }
    }
    public double ElapsedSeconds
    {
        get => stopwatch.Elapsed.TotalSeconds;
        set
        {
            NotifyPropertyChanged(nameof(ElapsedSeconds));
        }
    }

    public Key Key { get; set; }

    private void CompositionTarget_Rendering(object sender, EventArgs e)
    {
        double elapsedMilliseconds = (DateTime.Now - lastRenderTime).TotalMilliseconds;

        if (elapsedMilliseconds >= 100)
        {
            RefreshButtons();
            ElapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            model.ElapsedTime = stopwatch.Elapsed;

            lastRenderTime = DateTime.Now;
        }
    }

    public override void OpenPage()
    {
        RandomText = model.RandomText;
        StartTimer();
    }
    private async void StartTimer()
    {
        await Task.Delay(3000);
        // Start de spraaksynthese en stopwatch na 3 seconden 
        SpeakCmd();
        RefreshButtons();
        stopwatch.Restart();
    }

    public override void KeyPressed(Key key)
    {
        if (key == Key.LeftShift || key == Key.RightShift)
        {
            return;
        }

        if (key == Key.Back)
        {
            if (userInput.Length > 0)
            {
                userInput.Length -= 1;
                UpdateUserInputDisplay();
            }
        }
        else if (key == Key.Space)
        {
            userInput.Append(" ");
            UpdateUserInputDisplay();
        }
        else if (IsPrintableKey(key))
        {
            string input = GetPrintableCharacter(key, Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
            userInput.Append(input);
            UpdateUserInputDisplay();
        }
        else if (key == Key.Enter || key == Key.Return)
        {
            UpdateUserInputDisplay();
        }
    }


    public string GetPrintableCharacter(Key key, bool isShiftPressed)
    {
        string keyString = key.ToString();

        if (key == Key.OemQuotes)
        {
            return isShiftPressed ? "\"" : "'";
        }
        else if (key == Key.OemQuestion)
        {
            return isShiftPressed ? "?" : "/";
        }
        else if (key == Key.OemPeriod)
        {
            return isShiftPressed ? ">" : ".";
        }
        else if (key == Key.OemComma)
        {
            return isShiftPressed ? "<" : ",";
        }
        else if (key >= Key.D0 && key <= Key.D9)
        {
            return (key - Key.D0).ToString();
        }

        return isShiftPressed ? keyString.ToUpper() : keyString.ToLower();
    }



    public SpeedValue SelectedSpeedOption
    {
        get => model.TTSConverter.SpeedValue;
        set => model.TTSConverter.SpeedValue = value;
    }
    public SpeedValue[] SpeedOptions { get; set; }
    public string[] LanguageOptions { get; set; }
    public string SelectedLanguage
    {
        get => model.TTSConverter.Voice;
        set => model.TTSConverter.SetVoice(value);
    }

    private void SpeakCmd()
    {
        model.TTSConverter.PlayText(RandomText);
        RefreshButtons();
    }
    private void PauseCmd()
    {
        if (model.TTSConverter.PlayMode == PlayMode.Playing)
        {
            model.TTSConverter.Pause(); // Pauzeer het afspelen
        }
        else
        {
            model.TTSConverter.Resume(); // Hervat de tekst-naar-spraak als het gepauzeerd is 
        }
        RefreshButtons();
    }
    private bool IsPrintableKey(Key key)
    {

        return (key >= Key.A && key <= Key.Z) ||
               (key >= Key.D0 && key <= Key.D9) ||
               (key >= Key.NumPad0 && key <= Key.NumPad9) ||
               key == Key.OemQuotes || // Double and single quotation marks
               key == Key.OemQuestion || // Question mark
               key == Key.OemPeriod || // Period (.)
               key == Key.OemComma || // Comma (,)
               key == Key.Oem1 || // Exclamation mark (!) - US keyboard layout
               key == Key.Oem2 || // Slash (/) - specific to some keyboards
               key == Key.Oem3 || // Grave accent (`) - specific to some keyboards
               key == Key.OemPlus || // Plus sign (+)
               key == Key.OemMinus || // Minus sign (-)
               key == Key.OemOpenBrackets || // Opening square bracket ([)
               key == Key.OemCloseBrackets; // Closing square bracket (])
    }
    private void UpdateUserInputDisplay()
    {
        InputText = userInput.ToString();
        NotifyPropertyChanged(nameof(InputText));

        if (RandomText.Equals(InputText))
        {
            stopwatch.Stop();
            TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.Elapsed.TotalSeconds);
            ElapsedTimeText = $"{timeSpan:mm\\:ss},{timeSpan:ff}";
            MessageBox.Show($"Exercise completed in {ElapsedTimeText}", "Exercise Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            RequestPage = PageId.Resultaat;
        }
    }

    private void RefreshButtons()
    {
        NotifyPropertyChanged(nameof(PauzeText));
        NotifyPropertyChanged(nameof(Speak));
        NotifyPropertyChanged(nameof(Pause));
    }

}
