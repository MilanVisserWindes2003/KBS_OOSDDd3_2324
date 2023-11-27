using Skepta.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Skepta.Presentation.View
{
    /// <summary>
    /// Interaction logic for LoginVieww.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox box)
            {
                vm.Password = box.Password.ToString();
            }
        }
    }
}
