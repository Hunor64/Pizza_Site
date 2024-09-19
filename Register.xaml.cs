﻿using Pizza_Site.Models;
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
    public partial class Register : Window
    {
        #region Variables
        List<string> users = new();
        bool registerSuccess;
        RegistrationService registrationService = new RegistrationService();
        #endregion
        public Register()
        {
            InitializeComponent();
        }
        private void AlreadyRegistered(object sender, MouseButtonEventArgs e)
        {
            //Already registered button click (see Login.xaml)
            this.Close();
            Login loginWindow = new Login();
            loginWindow.ShowDialog();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            #region User Variables
            //Pass values to the user variables
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string email = txtEmail.Text;
            string mobileNumber = txtMobileNumber.Text;
            string adress = txtAddress.Text;
            bool isAdmin = false;
            #endregion
            #region User Validation
            //Username validation
            var upper = username.Any(char.IsUpper);
            var numeric = username.Any(char.IsDigit);
            if (!(upper && numeric))
            {
                MessageBox.Show("Error: Not a valid username! It must contain at least 1 uppercase letter and 1 number!");
                return;
            }

            //Password validation
            upper = password.Any(char.IsUpper);
            numeric = password.Any(char.IsDigit);
            if (password.Length < 5 || !(upper && numeric))
            {
                MessageBox.Show("Error: Password is too short! It must be at least 5 characters and must contain a number and an upper case letter!");
                return;
            }

            //Email validation
            if (!(email.Contains('@')) ||!(email.Contains('.')))
            {
                MessageBox.Show("Error: Not a valid E-mail address! It must contain '@' and '.'!");
                return;
            }

            //Mobile number validation
            if (!(mobileNumber.Length == 11))
            {
                MessageBox.Show("Error: Not a valid mobile number! It must be EXACTLY 11 Characters!");
                return;
            }
            var isNumeric = mobileNumber.All(char.IsDigit);
            if (!isNumeric)
            {
                MessageBox.Show("Error: Not a valid mobile number! It must contain only numbers!");
                return;
            }
            #endregion
            #region Register Validation
            string registrationResult = registrationService.RegisterUser(username, password, email, mobileNumber, adress ,isAdmin);

            if (registrationResult == "Registration successful!")
            {
                registerSuccess = true;
                users.Add(username);
                MessageBox.Show("Registration successful!");
                this.Close();
                Login loginWindow = new Login();
                loginWindow.ShowDialog();
            }
            else if (registrationResult == $"Registration failed: Username '{username}' is already taken.")
            {
                MessageBox.Show($"Registration failed: Username '{username}' is already taken.");
                this.Close();
                CustomMessageBox failedRegister = new CustomMessageBox("Registration Failed! Do you want to Log in or you want to create another account?", "Log in", "Register another account");
                bool? result = failedRegister.ShowDialog();

                if (result == true)
                {
                    string userChoice = failedRegister.Result;
                    if (userChoice == "Login")
                    {
                        this.Close();
                        Login loginWindow = new Login();
                        loginWindow.ShowDialog();
                    }
                    else if (userChoice == "Register")
                    {
                        this.Close();
                        Register registerWindow = new Register();
                        registerWindow.ShowDialog();
                    }
                }

            }
            else
            {
                MessageBox.Show(registrationResult);
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Register_Click(sender, e);
            }
        #endregion
        }
    }
}
