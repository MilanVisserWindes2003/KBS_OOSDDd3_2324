using Skepta.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Skepta.Presentation.View
{

    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        //Gets the password from the password box
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox box)
            {
                vm.Password = box.Password.ToString();
            }
        }
    }
}
