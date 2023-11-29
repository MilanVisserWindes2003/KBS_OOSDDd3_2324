﻿using Buisness.TTS;
using Skepta.Business;
using Skepta.Business.Util;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

public class TextToSpeechViewModel : ViewModelBase
{
    private readonly SkeptaModel model;   
    public TextToSpeechViewModel(SkeptaModel model)
    {
        this.model = model;
        SpeedOptions = Enum.GetValues<SpeedValue>();

    }

    public ICommand Speak => new BaseCommand(SpeakCmd, () => !string.IsNullOrEmpty(RandomText));
    public SpeedValue SelectedSpeedOption
    {
        get => model.TTSConverter.SpeedValue;
        set => model.TTSConverter.SpeedValue = value;
    }
    public SpeedValue[] SpeedOptions { get; set; }

    public string RandomText { 
        get => model.RandomText; 
    }

    private void SpeakCmd()
    {
        model.TTSConverter.PlayText(RandomText);
    }
}
