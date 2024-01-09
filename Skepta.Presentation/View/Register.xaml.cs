using Skepta.Presentation.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Skepta.Presentation.View;

public partial class Register : UserControl
{
    public Register()
    {
        InitializeComponent();
    }

    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegistreerViewModel vm && sender is PasswordBox box)
        {
            vm.Password = box.Password.ToString();
        }
    }

    private void CheckPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegistreerViewModel vm && sender is PasswordBox box)
        {
            vm.PasswordConfirm = box.Password.ToString();
        }
    }
}
