using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    internal class ChangePasswordViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private string newPassword;
        private string confirmPassword;
        public ChangePasswordViewModel(SkeptaModel model)
        {
            this.model = model;
        }
        public ICommand BackFromChange => new BaseCommand(BackChangeCmd);
        public ICommand ChangePass => new BaseCommand(ChangePasswordCmd);

        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                NotifyPropertyChanged(nameof(NewPassword));
            }
        }
        
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                NotifyPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private void ChangePasswordCmd()
        {
            string loggedInUsername = model.Username; // Use current logged in username
            DataAccess.DataAccess dataAccess = new DataAccess.DataAccess();

            string newPassword = NewPassword;
            string confirmPassword = ConfirmPassword;
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Het wachtwoord konmt niet overeen.", "Probeer het opniew", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool passwordChanged = dataAccess.ChangePassword(loggedInUsername, newPassword);
            if (passwordChanged)
            {
                // Password changed successful
                MessageBox.Show("Het wachtwoord is succesvol gewijzigd!", "Wachtwoord gewijzigd", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                // Password couldn't be changed
                MessageBox.Show("Het wachtwoord kon niet worden gewijzigd. Probeer het opnieuw.", "Fout bij wachtwoord wijzigen", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        // Navigate back to settings
        private void BackChangeCmd()
        {
            RequestPage = PageId.Settings;
        }
    }
}
