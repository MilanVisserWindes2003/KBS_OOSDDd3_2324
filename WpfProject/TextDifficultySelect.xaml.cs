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
    public partial class TextDifficultySelect : Page
    {
        Business.Business business;
        public TextDifficultySelect(Business.Business business)
        {
           InitializeComponent();
           this.business = business;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TextLengthSelect(business));
        }
    }
}
