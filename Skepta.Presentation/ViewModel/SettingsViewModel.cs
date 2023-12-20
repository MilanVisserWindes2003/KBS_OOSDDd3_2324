using Skepta.Business;
using Buisness.TTS;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataAccess;

namespace Skepta.Presentation.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private double volume = 0.5;
        public SettingsViewModel(SkeptaModel model)
        {
            this.model = model;
            InitializeLanguageOptions();
        }
        public ICommand HeadVolume => new BaseCommand(VolumeChangeCmd);
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
        public double Volume
        {
            get => volume;
            set
            {
                if (volume != value)
                {
                    volume = value;
                    NotifyPropertyChanged(nameof(Volume));
                    // Stel het volume in
                    //model.TTSConverter.
                }
            }
        }
        private void VolumeChangeCmd()
        {
            throw new NotImplementedException();
        }

        private void InitializeLanguageOptions()
        {
            LanguageOptions = model.TTSConverter.Voices.ToArray();
        }
        private void PersonalisedExerciseCmd()
        {
            throw new NotImplementedException();
        }
        private void ChangePasswordCmd()
        {
            RequestPage = PageId.ChangePassword;
        }
        private void RemoveAccountCmd()
        {
            string loggedInUsername = model.Username; // Gebruik de huidige ingelogde gebruikersnaam
            DataAccess.DataAccess dataAccess = new DataAccess.DataAccess();

            MessageBoxResult result = MessageBox.Show("Weet je zeker dat je het account wilt verwijderen?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool success = dataAccess.RemoveAccount(loggedInUsername);
                if (success)
                {
                    // Account succesvol verwijderd & uitlogacties
                    loggedInUsername = null;
                    RequestPage = PageId.Login;
                }
            }
            else
            {
                // Account verwijderen is mislukt
                MessageBox.Show("Het verwijderen van het account is mislukt.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackSetCmd()
        {
            RequestPage = PageId.MenuScreen;
        }
    }
}
