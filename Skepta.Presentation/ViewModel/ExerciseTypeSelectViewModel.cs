using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class ExerciseTypeSelectViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string currenSelection = string.Empty;
        private Dictionary<string, int> selected;
        public ExerciseTypeSelectViewModel(SkeptaModel model)
        {
            this.model = model;
            selected = new Dictionary<string, int>
        {
            // the 0 value equates to a text exercise and the 1 is the exercise with a spoken aid
            {"0", 5 },
            {"1", 5 }
        };
        }

        public ICommand Select => new BaseCommand<string>((text) => SelectCmd(text));
        public int[] Selected { get => selected.Values.ToArray(); }

        // Method is responsible for regulating the border thickness when a specific item is selected
        private void SelectCmd(string text)
        {
            // if statement is used to reset item border back to 5 if another item is selected
            if (!string.IsNullOrEmpty(currenSelection))
            {
                selected[currenSelection] = 5;
            }
            // currentSelection is assigned a value
            currenSelection = text;
            // the selected item is given a thickness of 15
            selected[currenSelection] = 15;
            NotifyPropertyChanged(nameof(Selected));
            // this statement defines which type of exercise is selected
            if (currenSelection == "0")
            {
                model.IsSpeechExercise = false;
            }
            else
            {
                model.IsSpeechExercise = true;
            }
        }

    }
}
