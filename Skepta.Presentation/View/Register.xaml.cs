
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business;
using Skepta.Presentation.ViewModel;

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
