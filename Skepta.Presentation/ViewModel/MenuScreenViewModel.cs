using System.Windows.Input;
using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;

namespace Skepta.Presentation.ViewModel
{
    internal class MenuScreenViewModel : ViewModelBase
    {
        private SkeptaModel model;

            // commands to navigate to different pages
        public ICommand Excersize => new BaseCommand(ExcersizeCmd);
        public ICommand History => new BaseCommand(HistoryCmd);
        public ICommand Settings => new BaseCommand(SettingsCmd);
        public ICommand Logout => new BaseCommand(LogoutCmd);
        public ICommand Exit => new BaseCommand(ExitCmd);
        public MenuScreenViewModel(SkeptaModel model)
        {
            this.model = model;
        }
        // navigates to the exercise selection screen
        public void ExcersizeCmd()
        {
            RequestPage = PageId.Exercise;
        }
        // navigates to the history screen
        public void HistoryCmd()
        {
            RequestPage = PageId.Geschiedenis;
        }
        // navigates to settings page
        public void SettingsCmd()
        {
            RequestPage = PageId.Settings;
        }
        // logs the user out and navigates to the login screen
        public void LogoutCmd()
        {
            model.Username = string.Empty;
            RequestPage = PageId.Login;
        }
        // shuts down application
        public void ExitCmd()
        {
            System.Environment.Exit(0);
        }
    }
}