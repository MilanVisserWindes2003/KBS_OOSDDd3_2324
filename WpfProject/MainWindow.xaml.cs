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

namespace WpfProject
{
    public partial class MainWindow : Window
    {
        Business.Business business;
        public MainWindow()
        {
            InitializeComponent();
            business = new Business.Business();

            MainFrame.NavigationService.Navigate(new Login(business));
        }       
    }
}
