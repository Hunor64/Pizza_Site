using Pizza_Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

namespace Pizza_Site
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bool loginSucsess;
        public void ReadDatabase()
        {
            using (var newContext = new PizzaContext())
            {
                var users = newContext.PizzaStore.ToList();


                foreach (var user in users)
                {
                    string username = txtUsername.Text;
                    string password = txtPassword.Password;
                    users.FindIndex(x => x.User_Name.ToString() == username);
                    if (users.FindIndex(x => x.User_Name.ToString() == username) != -1)
                    {
                        if (users.FindIndex(x => x.User_Password.ToString() == password) != -1)
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
            }
        }

        
        public Login()
        {
            InitializeComponent();
        }

        private void NoAccRegister(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Register registerWindow = new();
            registerWindow.ShowDialog();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            ReadDatabase();

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
