using Pizza_Site.Models;
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
    /// Interaction logic for PizzaAdding.xaml
    /// </summary>
    public partial class PizzaAdding : Window
    {
        PizzaAddingService pizzaAddingService = new PizzaAddingService();
        bool addingSuccess;
        List<string> pizzas = new();
        public PizzaAdding()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        private void AddPizza_Click(object sender, RoutedEventArgs e)
        {
            #region Pizza Variables
            string pizzaName = txtPizzaname.Text;
            string ingredients = txtIngredients.Text;
            string priceText = txtPrice.Text;
            string image = $"{txtImage.Text.ToLower()}.{cbExtensions.Text.ToLower()}";
            #endregion

            #region Pizza Validation
            if (string.IsNullOrEmpty(pizzaName))
            {
                MessageBox.Show("Error: Not a valid pizza name!");
                return;
            }
            if (string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Error: Don't leave ingredients field empty! ");
                return;
            }
            if (!int.TryParse(txtPrice.Text, out int outPrice) || outPrice <= 0)
            {
                MessageBox.Show("Please enter a valid price greater than zero.");
                return;
            }
            if (string.IsNullOrEmpty(txtImage.Text))
            {
                MessageBox.Show("Error: Don't leave Image field empty! ");
                return;
            }
            #endregion

            int price = int.Parse(txtPrice.Text);
            #region Pizza adding
            string addingPizzaResult = pizzaAddingService.AddingPizza(pizzaName, ingredients, price, image);

            if (addingPizzaResult == "New pizza added!")
            {
                addingSuccess = true;
                pizzas.Add(pizzaName);
                MessageBox.Show("Adding successful!");
                this.Close();
            }
            else if (addingPizzaResult == $"The {pizzaName} was already added")
            {
                this.Close();
                CustomMessageBox failedAdding = new CustomMessageBox($"Adding failed: The {pizzaName} was already added! \nDo you want to add a different one?", "Yes", "No");
                bool? result = failedAdding.ShowDialog();

                if (result == true)
                {
                    string userChoice = failedAdding.Result;
                    if (userChoice == "Yes")
                    {
                        PizzaAdding newPizzaAdding = new PizzaAdding();
                        newPizzaAdding.ShowDialog();
                    }
                    else if (userChoice == "No")
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(addingPizzaResult);
            }
            #endregion

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
            //Application.Current.Shutdown();
        }
        #endregion
    }
}
