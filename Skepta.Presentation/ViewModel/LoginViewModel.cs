using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

public class LoginViewModel : ViewModelBase
{
    private readonly SkeptaModel model;
    private string errorText = "";
    private string password = "";
    

    public LoginViewModel(SkeptaModel model)
    {
        this.model = model;
    }

    public ICommand Login => new BaseCommand(LoginCmd);
    public ICommand Register => new BaseCommand(() => RequestPage = PageId.Register);

    public string Username { get; set; } = string.Empty;
    public string Password
    {
        get => password;
        set
        {
            password = value;
            ErrorText = "";
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

    private void LoginCmd()
    {
        if (model.CheckLogin(Username, Password))
        {   
            model.Username = Username;
            //RequestPage = PageId.Exercise;
            //NavigationService.Navigate(new TextDifficultySelect(business));
             RequestPage = PageId.Geschiedenis;
        }
        else
        {
            ErrorText = "Combinatie van gebruikersnaam en wachtwoord bestaat niet!";
        }
    }
}
