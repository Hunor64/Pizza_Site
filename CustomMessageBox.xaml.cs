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
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        #region Input result data
        //Getting the result
        public string Result { get; private set; }

        //Passing data to the pop-up window
        public CustomMessageBox(string message, string button1Text, string button2Text)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
            btnLogin.Content = button1Text;
            btnRegister.Content = button2Text;
        }
        #endregion

        #region Button clicks
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Result = "Login";
            this.DialogResult = true;
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Result = "Register";
            this.DialogResult = true;
            this.Close();
        }
        #endregion
    }
}

