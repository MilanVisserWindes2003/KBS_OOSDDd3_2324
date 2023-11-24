using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class RegistreerViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string errorText = "";
        private string password = "";
        private string passwordConfirm = "";

        public RegistreerViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public ICommand Registreer => new BaseCommand(RegistreerCmd, AllowRegistreer);
        public ICommand Back => new BaseCommand(() => RequestPrev = true);

        public string Username { get; set; } = string.Empty;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ErrorText = "";
                NotifyPropertyChanged(nameof(Registreer));
            }
        }

        public string PasswordConfirm
        {
            get => passwordConfirm;
            set
            {
                passwordConfirm = value;
                ErrorText = "";
                NotifyPropertyChanged(nameof(Registreer));
            }
        }
        public string ErrorText
        {
            get => errorText;
            set
            {
                errorText = value;
                NotifyPropertyChanged(nameof(ErrorText));
            }
        }

        public override void OpenPage()
        {
            ErrorText = string.Empty;
        }

        public bool AllowRegistreer() => !string.IsNullOrWhiteSpace(password) && password.Equals(passwordConfirm);
        private void RegistreerCmd()
        {
            (bool pass, string message) = model.CheckRegister(Username, password, passwordConfirm);

            ErrorText = message;
            if (pass)
            {
                RequestPrev = true;
            }
        }
    }
}
