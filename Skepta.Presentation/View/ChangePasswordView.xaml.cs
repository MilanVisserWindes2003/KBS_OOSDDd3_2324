using Skepta.Presentation.ViewModel;
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

namespace Skepta.Presentation.View
{
    /// <summary>
    /// Interaction logic for ChangePasswordView.xaml
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistreerViewModel vm && sender is PasswordBox box)
            {
                vm.Password = box.Password.ToString();
            }
        }

        private void CheckPasswordChange(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistreerViewModel vm && sender is PasswordBox box)
            {
                vm.PasswordConfirm = box.Password.ToString();
            }
        }
    }
}
