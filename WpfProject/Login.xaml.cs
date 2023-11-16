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
    public partial class Login : Page
    {
        Business.Business business;
        public Login(Business.Business business)
        {
            InitializeComponent();
            this.business = business;

        }

        private void login_click(object sender, RoutedEventArgs e)
        {
            string username = logintxt.Text;
            string password = wachtwoordtxt.Password;
            if (business.CheckLogin(username, password))
            {
                NavigationService.Navigate(new TextDifficultySelect(business));
            }
            else
            {
                MessageBox.Show("Combinatie van gebruikersnaam en wachtwoord bestaat niet!");
            }
        }

        private void registreer_click(object sender, RoutedEventArgs e)
        {   
            NavigationService.Navigate(new Register(business));
        }
    }
}