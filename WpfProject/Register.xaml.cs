
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
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        Business.businessMethode business;
        public Register()
        {
            InitializeComponent();
            business = new Business.businessMethode();
        }
        private void registreer_Button(object sender, RoutedEventArgs e)
        {
            if (!(logintxt.Text.Length <= 20 && wachtwoordtxt.Password.Length <= 20 && wachtwoordtxt2.Password == wachtwoordtxt.Password))
            {
                return;
            }
           
            if (string.IsNullOrWhiteSpace(logintxt.Text))
            {
                MessageBox.Show("Gebruikersnaam is verplicht");
                return;
            }

            if (string.IsNullOrWhiteSpace(wachtwoordtxt.Password))
            {
                MessageBox.Show("Wachtwoord is verplicht");
                return;
            } 
            if (string.IsNullOrWhiteSpace(wachtwoordtxt2.Password))
            {
                MessageBox.Show("Herhaal je Wachtwoord");
                return;
            } 
            if(business.CheckRegister(logintxt.Text, wachtwoordtxt.Password, wachtwoordtxt2.Password)){
                MessageBox.Show("Je account is aangemaakt.");
                NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
            }
            else 
            {
                MessageBox.Show("Account is niet aangemaakt.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
