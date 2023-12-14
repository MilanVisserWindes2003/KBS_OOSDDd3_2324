using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.ViewModel
{
    public class TextShuffleViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string randomText;
        public TextShuffleViewModel(SkeptaModel model)
        {
            this.model = model;
            model.PropertyChanged += Button_PropertyChanged;
        }

        public ICommand Shuffle => new BaseCommand(() => RandomTextShuffle = model.ObtainRandomText(), VerderAllowed);

        public bool IsPersonalized
        {
            get { return model.IsPersonalized; }
        }
        public string RandomTextShuffle
        {
            get => randomText;
            set
            {
                randomText = value;
                model.RandomText = value;
                NotifyPropertyChanged(nameof(RandomTextShuffle));
            }
        }

        public override void OpenPage()
        {
            RandomTextShuffle = model.ObtainRandomText();
        }

        private void Button_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(model.TextDifficulty))
            {
                RandomTextShuffle = model.ObtainRandomText();
                NotifyPropertyChanged(nameof(Shuffle));
            }
            if (e.PropertyName == nameof(model.TextLength))
            {
                RandomTextShuffle = model.ObtainRandomText();
                NotifyPropertyChanged(nameof(Shuffle));
            }
        }


        private bool VerderAllowed()
        {
            if (model.TextLength == 0 || model.TextDifficulty == null)
            {
                return false;
            }
            return true;
        }
    }
}
