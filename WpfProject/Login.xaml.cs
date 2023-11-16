
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private const string ConnectionDatabase = "Server=localhost;Database=lerentypen;User ID=root;Passord=;";

        public Login()
        {
            InitializeComponent();
        }

        private void login_click(object sender, RoutedEventArgs e)
        {
            string username = logintxt.Text;
            string password = wachtwoordtxt.Text;

            //string loginResult = ConnectionDatabase.Login(username, password);

            //MessageBox.Show(loginResult); // Show the result in a MessageBox for example
        }

        private void registreer_click(object sender, RoutedEventArgs e)
        {
            string username = logintxt.Text;
            string password = wachtwoordtxt.Text;

            //string registerResult = ConnectionDatabase.Register(username, password);

            //MessageBox.Show(registerResult); // Show the result in a MessageBox for example
        }
    }
}
