using Skepta.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Skepta.Presentation.View
{

    public partial class ChangePasswordView : UserControl
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        //Event handler to get the canged password.
        private void NewPasswordChangedEventHandler(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.NewPassword = passwordBox.Password;
            }
        }

        //Event handler to confitm if the password is correct
        private void ConfirmPasswordChangedEventHandler(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.ConfirmPassword = passwordBox.Password;
            }
        }
    }
}
