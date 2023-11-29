using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
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
            {"0", 5 },
            {"1", 5 }
        };
        }

        public ICommand Select => new BaseCommand<string>((text) => SelectCmd(text));
        public int[] Selected { get => selected.Values.ToArray(); }


        private void SelectCmd(string text)
        {
            if (!string.IsNullOrEmpty(currenSelection))
            {
                selected[currenSelection] = 5;
            }
            currenSelection = text;
            selected[currenSelection] = 15;
            NotifyPropertyChanged(nameof(Selected));
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
