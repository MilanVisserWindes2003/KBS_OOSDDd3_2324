using System.Windows.Input;
using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;

namespace Skepta.Presentation.ViewModel
{
    internal class MenuScreenViewModel : ViewModelBase
    {
        private SkeptaModel model;

        public ICommand Excersize => new BaseCommand(ExcersizeCmd);
        public ICommand Logout => new BaseCommand(LogoutCmd);
        public MenuScreenViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public void ExcersizeCmd()
        {
            RequestPage = PageId.Exercise;
        }

        public void LogoutCmd()
        {
            RequestPage = PageId.Login;
        }
    }
}