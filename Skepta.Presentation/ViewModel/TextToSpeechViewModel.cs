using Buisness.TTS;
using Skepta.Business;
using Skepta.Business.Util;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.ViewModel;

public class TextToSpeechViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly SkeptaModel model; 
    private readonly Stopwatch stopwatch = new Stopwatch();
    private DateTime lastRenderTime;

    private StringBuilder userInput = new StringBuilder();
    private string inputText = string.Empty;
    private int aantalTekens;
    private int aantalWoorden;

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public double ElapsedSeconds
    {
        get => stopwatch.Elapsed.TotalSeconds;
        set
        {
            NotifyPropertyChanged(nameof(ElapsedSeconds));
        }
    }
    public TextToSpeechViewModel(SkeptaModel model)
    {
        this.model = model;
        stopwatch = new Stopwatch();

        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    public string RandomText { get; set; } = string.Empty;
    public string InputText
    {
        get => inputText;
        set
        {
            inputText = value;
            AantalWoorden = inputText.Split(' ').Length;
            aantalTekens = inputText.Length;
        }
    }

    public int AantalWoorden
    {
        get => aantalWoorden;
        set
        {
            aantalWoorden = value;
            NotifyPropertyChanged(nameof(AantalWoorden));
        }
    }

    public Key Key { get; set; }

    private void CompositionTarget_Rendering(object sender, EventArgs e)
    {
        double elapsedMilliseconds = (DateTime.Now - lastRenderTime).TotalMilliseconds;

        if (elapsedMilliseconds >= 16)
        {
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
    private void StartTimer()
    {
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
            aantalWoorden++;
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

    private void UpdateUserInputDisplay()
    {
        InputText = userInput.ToString();
        NotifyPropertyChanged(nameof(InputText));

        if (RandomText.Equals(InputText))
        {
            stopwatch.Stop();
            MessageBox.Show($"Exercise completed in {model.ElapsedTime.TotalSeconds:F2} seconds.", "Exercise Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            RequestPage = PageId.Resultaat;
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

    public ICommand Speak => new BaseCommand(SpeakCmd, () => !string.IsNullOrEmpty(Text));
    public SpeedValue SelectedSpeedOption
    {
        get => model.TTSConverter.SpeedValue;
        set => model.TTSConverter.SpeedValue = value;
    }
    public SpeedValue[] SpeedOptions { get; set; }

    public string Text { 
        get => RandomText; 
    }

    private void SpeakCmd()
    {
        model.TTSConverter.PlayText(Text);
    }
}
