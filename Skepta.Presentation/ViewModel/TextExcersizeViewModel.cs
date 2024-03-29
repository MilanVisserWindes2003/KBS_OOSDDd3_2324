﻿using Skepta.Business;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.ViewModel;

public class TextExcersizeViewModel : ViewModelBase
{
    private readonly SkeptaModel model;
    private StringBuilder userInput = new StringBuilder();
    public ToetsenbordViewModel toetsenbord { get; set; }

    private readonly Stopwatch stopwatch = new Stopwatch();
    private DateTime lastRenderTime;
    private string inputText = string.Empty;


    public double ElapsedSeconds
    {
        get => stopwatch.Elapsed.TotalSeconds;
        set
        {
            NotifyPropertyChanged(nameof(ElapsedSeconds));
        }
    }

    public TextExcersizeViewModel(SkeptaModel model)
    {
        this.model = model;
        stopwatch = new Stopwatch();
        toetsenbord = new ToetsenbordViewModel(model);

        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    
    // Method extracts the elapsed time
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
    private string elapsedTimeText;
    public string RandomText { get; set; } = string.Empty;
    public string InputText
    {
        get => inputText;
        set
        {
            inputText = value;
            NotifyPropertyChanged(nameof(InputText));
        }
    }

    public Key Key { get; set; }

    // Method clears any possible leftover user input and starts the timer
    public override void OpenPage()
    {
        RandomText = model.RandomText;
        InputText = string.Empty;
        userInput.Clear();
        StartTimer();
    }
    // The stopwatch is started
    private void StartTimer()
    {
        stopwatch.Restart();
    }
    // Method processes what key is pressed in order to decide what is or isn't to be added to the string that the user has typed
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
            model.ElapsedTime = stopwatch.Elapsed;
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
            return (key-Key.D0).ToString();
        }

        return isShiftPressed ? keyString.ToUpper() : keyString.ToLower();
    }
    // adds to the counter of mistakes
    public void addMistake(char mistake)
    {
        model.result.addMistake(mistake);
    }

    // method is responsible for checking if the key that is pressed is actually able to be displayed (a letter, number or another sign that has physical representation on the screen)
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
}
