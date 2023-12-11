using System.Windows.Input;
using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;

namespace Skepta.Presentation.ViewModel
{
    internal class MenuScreenViewModel : ViewModelBase
    {
        private SkeptaModel model;

        public ICommand Excersize => new BaseCommand(ExcersizeCmd);
        public ICommand History => new BaseCommand(HistoryCmd);
        public ICommand Settings => new BaseCommand(SettingsCmd);
        public ICommand Logout => new BaseCommand(LogoutCmd);
        public ICommand Exit => new BaseCommand(ExitCmd);
        public MenuScreenViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public void ExcersizeCmd()
        {
            RequestPage = PageId.Exercise;
        }

        public void HistoryCmd()
        {
            RequestPage = PageId.Geschiedenis;
        }

        public void SettingsCmd()
        {
            RequestPage = PageId.Settings;
        }

        public void LogoutCmd()
        {
            RequestPage = PageId.Login;
        }

        public void ExitCmd()
        {
            System.Environment.Exit(0);
        }
    }
}