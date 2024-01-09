using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private double sliderValue;
        public SettingsViewModel(SkeptaModel model)
        {
            this.model = model;
            InitializeLanguageOptions();
        }

        // checks if the desired exercise is adapted to the users skill set or not
        public bool IsPersonalized 
        {   get
            {
                return model.IsPersonalized;
            } 
            set
            {
                model.IsPersonalized = !model.IsPersonalized;
                NotifyPropertyChanged(nameof(IsPersonalized));
            }
        }

        // button commands to execute different operations
        public ICommand PersonalisedExercise => new BaseCommand(PersonalisedExerciseCmd);
        public ICommand WijzigWW => new BaseCommand(ChangePasswordCmd);
        public ICommand VerwijderAC => new BaseCommand(RemoveAccountCmd);
        public ICommand BackFromSet => new BaseCommand(BackSetCmd);

        public string[] LanguageOptions { get; private set; }
        public string SelectedLanguage
        {
            get => model.TTSConverter.Voice;
            set => model.TTSConverter.SetVoice(value);
        }
        public int SliderValue
        {
            get => model.TTSConverter.Volume;
            set
            {
                if (sliderValue != value)
                {
                    model.TTSConverter.Volume = value;
                    NotifyPropertyChanged(nameof(sliderValue));
                }
            }
        }
        

        // displays dropdown of different language options available
        private void InitializeLanguageOptions()
        {
            LanguageOptions = model.TTSConverter.Voices.ToArray();
        }

        // sets value of IsPersonalized to the value that it currently does not have
        private void PersonalisedExerciseCmd()
        {
            model.IsPersonalized = !model.IsPersonalized;
        }

        // opens the page to change user password
        private void ChangePasswordCmd()
        {
            RequestPage = PageId.ChangePassword;
        }
        
        // removes the users account and navigates them back to the login screen
        private void RemoveAccountCmd()
        {
            string loggedInUsername = model.Username; // Use the current users username 
            DataAccess.DataAccess dataAccess = new DataAccess.DataAccess();

            MessageBoxResult result = MessageBox.Show("Weet je zeker dat je het account wilt verwijderen?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool success = dataAccess.RemoveAccount(loggedInUsername);
                if (success)
                {
                    // Account successfully removed and logout action instigated
                    loggedInUsername = null;
                    RequestPage = PageId.Login;
                }
            }
            else
            {
                // Account removal failed
                MessageBox.Show("Het verwijderen van het account is mislukt.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // navigate back to the menu screen
        private void BackSetCmd()
        {
            RequestPage = PageId.MenuScreen;
        }
    }
}
