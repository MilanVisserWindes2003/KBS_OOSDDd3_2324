using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

public class ExercisePageViewModel : ViewModelBase
{
    private SkeptaModel model;
    private bool difficultySelected = false;
    private bool textLengthSelected = false;
    private bool exerciseSelected = false;
    public ExercisePageViewModel(SkeptaModel model)
    {
        this.model = model;
        model.PropertyChanged += Model_PropertyChanged;
        DifficultySelect = new TextDifficultySelectViewModel(model);
        LenghtSelect = new TextLenghtSelectViewModel(model);
        ExerciseTypeSelect = new ExerciseTypeSelectViewModel(model);
    }

    public ICommand Verder => new BaseCommand(VerderCmd, VerderAllowed);

    public TextDifficultySelectViewModel DifficultySelect { get; set; }
    public TextLenghtSelectViewModel LenghtSelect { get; set;}
    public ExerciseTypeSelectViewModel ExerciseTypeSelect { get; set;}

    private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(model.TextDifficulty))
        {
            difficultySelected = true;
            NotifyPropertyChanged(nameof(Verder));
        }
        if (e.PropertyName == nameof(model.TextLength))
        {
            textLengthSelected = true;
            NotifyPropertyChanged(nameof(Verder));
        }
        if (e.PropertyName == nameof(model.IsSpeechExercise))
        {
            exerciseSelected = true;
            NotifyPropertyChanged(nameof(Verder));
        }
    }

    private void VerderCmd()
    {

    }

    private bool VerderAllowed() => difficultySelected && textLengthSelected;
}
