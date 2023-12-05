using Skepta.Business;

namespace Skepta.Presentation.ViewModel
{
    internal class MenuScreenViewModel : ViewModelBase
    {
        private SkeptaModel model;

        public MenuScreenViewModel(SkeptaModel model)
        {
            this.model = model;
        }
    }
}