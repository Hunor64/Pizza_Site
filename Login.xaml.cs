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
    public partial class Login : Window
    {
        #region Reads Db and Login click logic
        //Reads the Database
        public void ReadDatabase()
        {
            using (var newContext = new PizzaContext())
            {
                #region Variables
                //Users list filled from the database
                var users = newContext.PizzaStore.ToList();

                //Username
                string username = txtUsername.Text;

                //Password
                string password = txtPassword.Password;

                //Checks if the login is successful
                bool loginSucsess;
                #endregion

                #region Custom MessageBox logic
                //Custom messagebox pop-up logic
                if (users.FindIndex(x => x.User_Name.ToString() == username) == -1)
                {
                    //Creates new custom message box element
                    CustomMessageBox LoginErr = new CustomMessageBox("Username not Found!", "Try Again", "Register" );
                    bool? result = LoginErr.ShowDialog();

                    //Decide which option was selected
                    if (result == true)
                    {
                        string userChoice = LoginErr.Result;

                        //You stay on the login page
                        if (userChoice == "Try Again")
                        {
                            return;
                        }
                        //Redirects you to the register page
                        else if (userChoice == "Register")
                        {
                            this.Close();
                            Register registerWindow = new Register();
                            registerWindow.ShowDialog();
                        }
                    }
                }
                #endregion

                #region Login validation
                //Checks if there is possible to log in with those data's
                foreach (var user in users)
                {
                    //Finds if the username exists in the database
                    if (users.FindIndex(x => x.User_Name.ToString() == username) != -1)
                    {
                        //Checks if the password is correct
                        if (users.FindIndex(x => x.User_Password.ToString() == password) != -1)
                        {
                            //Logins if success
                            this.Close();
                            loginSucsess = true;
                            if (loginSucsess)
                            {
                                loginSucsess = true;
                                this.Close();
                            }
                        }

                        else
                        {
                            //Shows error if not
                            MessageBox.Show("Password is incorrect");
                            return;
                        }
                    }
                }

                #endregion
            }
        }
        #endregion

        #region Login, NoAccRegister, Login_Click, txtPassword_Keydown functions
        public Login()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void NoAccRegister(object sender, MouseButtonEventArgs e)
        {
            //Redirect you to the register page if no account is registered
            this.Close();
            Register registerWindow = new();
            registerWindow.ShowDialog();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Login Validation and custom messagebox logic
            ReadDatabase();
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //Password secure
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }
        #endregion

      
        private void txbClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #region Custom title bar clicks
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        private void btnOpenPizzaAdding(object sender, RoutedEventArgs e)
        {
            PizzaAdding newPizzaAdding = new PizzaAdding();
            newPizzaAdding.ShowDialog();
        }
    }
}
