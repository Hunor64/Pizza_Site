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
using System.Windows.Shapes;

namespace Pizza_Site
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        List<string> users = new();
        bool registerSusess;

        public Register()
        {
            InitializeComponent();
            users.Add("admin");
        }
        private void Alreadyegistered(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Login loginWindow = new();
            loginWindow.ShowDialog();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            users.FindIndex(x => x == username);
            if (users.FindIndex(x => x == username) == -1)
            {
                if (password.Length > 5)
                {
                    registerSusess = true;
                    users.Add(username);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password is too short");
                }
            }
            else
            {
                MessageBox.Show("Username already found");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Register_Click(sender, e);
            }
        }
    }
}
