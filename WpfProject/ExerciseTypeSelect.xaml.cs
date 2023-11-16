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
    public partial class ExerciseTypeSelect : Page
    {
        Business.Business business;
        public ExerciseTypeSelect(Business.Business business)
        {
            InitializeComponent();
            ImageViewer1.Source = new BitmapImage(new Uri("font.png", UriKind.Relative));
            ImageViewer2.Source = new BitmapImage(new Uri("speak.png", UriKind.Relative));
            this.business = business;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login(business));
        }
    }
}
