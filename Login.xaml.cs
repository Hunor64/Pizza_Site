using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Shapes;

namespace Pizza_Site
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bool loginSucsess;
        List<string> users = new();
        public Login()
        {
            InitializeComponent();
            users.Add("admin");
        }

        private void NoAccRegister(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Register registerWindow = new();
            registerWindow.ShowDialog();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            users.FindIndex(x => x == username);
            if (users.FindIndex(x => x == username) != -1)
            {
                if (users[users.FindIndex(x => x == username)] == password)
                {
                    loginSucsess = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password is incorrect");
                }
            }
            else
            {
                MessageBox.Show("Username is not found");
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }
    }
}
