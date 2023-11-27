using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class TextLenghtSelectViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;

        public TextLenghtSelectViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public ICommand Next => new BaseCommand<string>((length) => NextCmd(length));

        private void NextCmd(string length)
        {
            if (int.TryParse(length, out int value))
            {
                model.TextLength = value;
                RequestNext = true;
            }
        }
    }
}
