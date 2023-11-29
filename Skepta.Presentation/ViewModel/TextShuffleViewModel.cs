using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class TextShuffleViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string randomText;

        public TextShuffleViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public ICommand Shuffle => new BaseCommand(() => RandomText = model.ObtainRandomText());

        public ICommand Accept => new BaseCommand(() =>
        {
            RequestNext = true;
            model.WordCounting(RandomText);
        });


        public string RandomText
        {
            get => randomText;
            set
            {
                randomText = value;
                NotifyPropertyChanged(nameof(RandomText));
            }
        }

        public override void OpenPage()
        {
            RandomText = model.ObtainRandomText();
        }
    }
}
