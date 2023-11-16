
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
    public partial class Register : Page
    {
        Business.Business business;
        public Register(Business.Business business)
        {
            InitializeComponent();
            this.business = business;
        }
        private void registreer_Button(object sender, RoutedEventArgs e)
        {
            string username = logintxt.Text.Trim();
            string password = wachtwoordtxt.Password.Trim();
            string passwordConfirm = wachtwoordtxt2.Password.Trim();
            (bool pass, string message) = business.CheckRegister(username, password, passwordConfirm);
            MessageBox.Show(message);
            if (pass)
            {
                NavigationService.Navigate(new Login(business));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login(business));
        }
    }
}
