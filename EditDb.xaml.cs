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
    /// Interaction logic for EditDb.xaml
    /// </summary>
    public partial class EditDb : Window
    {
        public EditDb()
        {
            InitializeComponent();
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
            {
                WindowState = WindowState.Normal;
                Register.GetWindow(this).Width = 350;
                Register.GetWindow(this).Height = 450;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                Register.GetWindow(this).Width = System.Windows.SystemParameters.WorkArea.Width;
                Register.GetWindow(this).Height = System.Windows.SystemParameters.WorkArea.Height;
            };
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        private void SavePizza_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
