using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class TextLenghtSelectViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string currenSelection = string.Empty;
        private Dictionary<string, int> selected;

        public TextLenghtSelectViewModel(SkeptaModel model)
        {
            this.model = model;
            selected = new Dictionary<string, int>
        {
            {"1", 5 },
            {"2", 5 },
            {"3", 5 },
            {"4", 5 },
            {"5", 5 }
        };
        }

        public ICommand Select => new BaseCommand<string>((lengthText) => SelectCmd(lengthText));
        public int[] Selected { get => selected.Values.ToArray(); }

        //Highlights the selected text length and sets the rest to default. It also sets the length to the given value
        private void SelectCmd(string lengthText)
        {
            if (!string.IsNullOrEmpty(currenSelection))
            {
                selected[currenSelection] = 5;
            }
            currenSelection = lengthText;
            selected[currenSelection] = 15;
            NotifyPropertyChanged(nameof(Selected));

            model.TextLength = int.Parse(lengthText);
            RequestPage = PageId.TextShuffle;
        }
    }
}
