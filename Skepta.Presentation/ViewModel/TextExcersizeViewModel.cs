﻿using Business;
using Skepta.Business;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Skepta.Presentation.ViewModel;

public class TextExcersizeViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly SkeptaModel model;
    private StringBuilder userInput = new StringBuilder();
    private string inputText = string.Empty;
    private int aantalTekens;
    private int aantalWoorden;
   
    private readonly Stopwatch stopwatch = new Stopwatch();
    private readonly DispatcherTimer timer;

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public double ElapsedSeconds
    {
        get => stopwatch.Elapsed.TotalSeconds;
        // Notify the UI when ElapsedSeconds changes
        set
        {
            NotifyPropertyChanged(nameof(ElapsedSeconds));
        }
    }

    public TextExcersizeViewModel(SkeptaModel model)
    {
        this.model = model;
        stopwatch = new Stopwatch();

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    
    private void Timer_Tick(object sender, EventArgs e)
    {
        ElapsedSeconds = stopwatch.Elapsed.TotalSeconds;
        model.ElapsedTime = stopwatch.Elapsed;
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
            MessageBox.Show($"Exercise completed in {model.ElapsedTime.TotalSeconds:F3} seconds.", "Exercise Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            RequestPage = PageId.Resultaat;
            model.aantalWoordenPerMinuut();
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
