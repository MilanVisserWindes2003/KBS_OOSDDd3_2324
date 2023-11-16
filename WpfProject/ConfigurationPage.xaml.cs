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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage : Page
    {
        public ConfigurationPage()
        {
           InitializeComponent();
        }

        private void ButtonText_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }

        private void ButtonAudio_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
