using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

public class ExercisePageViewModel : ViewModelBase
{
    private SkeptaModel model;
    private bool difficultySelected = false;
    private bool textLengthSelected = false;
    private bool exerciseSelected = false;
    private bool textSelected = false;
    public ExercisePageViewModel(SkeptaModel model)
    {
        this.model = model;
        model.PropertyChanged += Model_PropertyChanged;
        DifficultySelect = new TextDifficultySelectViewModel(model);
        LenghtSelect = new TextLenghtSelectViewModel(model);
        ExerciseTypeSelect = new ExerciseTypeSelectViewModel(model);
        TextSelect = new TextShuffleViewModel(model);
    }

    public ICommand Verder => new BaseCommand(VerderCmd, VerderAllowed);
    public ICommand Back => new BaseCommand(() => RequestPage = PageId.MenuScreen);

    public TextDifficultySelectViewModel DifficultySelect { get; set; }
    public TextLenghtSelectViewModel LenghtSelect { get; set;}
    public ExerciseTypeSelectViewModel ExerciseTypeSelect { get; set;}
    public TextShuffleViewModel TextSelect {  get; set;}

    private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Method checks if all required elements are selected to allow progression towards the application
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
        if (e.PropertyName == nameof(model.RandomText))
        {
            textSelected = true;
            NotifyPropertyChanged(nameof(Verder));
        }
    }
    // Different options for the Verder command either navigating to a text exercise or an exercise where the user is assisted by a spoken text
    private void VerderCmd()
    {
        if (model.IsSpeechExercise)
        {
            RequestPage = PageId.TextToSpeech;
        }
        RequestPage = PageId.TextExcersize;
    }
    private bool VerderAllowed() => difficultySelected && textLengthSelected && exerciseSelected && textSelected;

}
