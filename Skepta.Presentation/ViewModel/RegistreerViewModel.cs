using Org.BouncyCastle.Asn1.Cms;
using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class RegistreerViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        private string errorText = "";
        private string password = "";
        private string passwordConfirm = "";
        private string registrationStatus = "";

        public RegistreerViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public ICommand Registreer => new BaseCommand(RegistreerCmd, AllowRegistreer);
        public ICommand Login => new BaseCommand(() => RequestPage = PageId.Login);

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

        public string RegistrationStatus
        {
            get => registrationStatus;
            set
            {
                registrationStatus = value;
                NotifyPropertyChanged(nameof(RegistrationStatus));
            }
        }

        public override void OpenPage()
        {
            ErrorText = string.Empty;
            RegistrationStatus = string.Empty;
        }
        // is used to discern if credentials have been filled out
        public bool AllowRegistreer() => !string.IsNullOrWhiteSpace(password) && password.Equals(passwordConfirm);
        // checks if user registration is allowed and if this is the case the user is forwarded to the login screen
        private async void RegistreerCmd()
        {
            (bool pass, string message) = model.CheckRegister(Username, password, passwordConfirm);

            if (pass)
            {
                RegistrationStatus = "Succesvol geregistreerd! U wordt doorverwezen naar de login-pagina...";
                await Task.Delay(2000);
                Login.Execute(null);
            }
            else
            {
                ErrorText = message;
            }
        }
    }
}
