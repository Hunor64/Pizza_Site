using Microsoft.EntityFrameworkCore;
using Pizza_Site.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class Admin_Panel : Window
    {
        public ObservableCollection<Pizza> Pizzas { get; set; }
        public Admin_Panel()
        {
            InitializeComponent();
            LoadPizzasFromDatabase();
            DataContext = this;
        }

        private void LoadPizzasFromDatabase()
        {
            using (var newContext = new PizzaContext())
            {
                //Creating an itemsource list to our grid and pushing the datas from the database to it
                List<Pizza> pizzaList = new List<Pizza>();

                //Creating a list from our PizzaDescription table
                var pizzas = newContext.PizzasDescription.ToList();

                //Giving values to the elements to our list 
                foreach (var pizza in pizzas)
                {
                    pizzaList.Add(new Pizza { Name = pizza.PizzaName, ImagePath = $"pack://application:,,,/Pizza_Site;component/Images/{pizza.ImagePath.ToLower()}", Ingredients = pizza.Ingredients, Price = pizza.Price }); ;
                }


                //Setting the main grid's itemsource to our appended list
                PizzaListView.ItemsSource = pizzaList;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Custom title bar clicks
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PizzaAdding newPizzaAdding = new PizzaAdding();
            newPizzaAdding.ShowDialog();
        }
        public void LoadPizzaListFromDb()
        {
            //Creating a new db context
            using (var newContext = new PizzaContext())
            {
                //Creating an itemsource list to our grid and pushing the datas from the database to it
                List<Pizza> pizzaList = new List<Pizza>();

                //Creating a list from our PizzaDescription table
                var pizzas = newContext.PizzasDescription.ToList();

                //Giving values to the elements to our list 
                foreach (var pizza in pizzas)
                {
                    pizzaList.Add(new Pizza { Name = pizza.PizzaName, ImagePath = $"pack://application:,,,/Pizza_Site;component/Images/{pizza.ImagePath.ToLower()}", Ingredients = pizza.Ingredients, Price = pizza.Price });
                }


                //Setting the main grid's itemsource to our appended list
                PizzaListView.ItemsSource = pizzaList;
                
            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //using (var context = new PizzaContext())
            //{
            //    //Passing data to a new user
            //    var newUser = new PizzaUsers { User_Name = userName, User_Password = userPassword, User_Email = userEmail, User_MobileNumber = userMobileNumber, User_Address = userAddress, Is_Admin = is_Admin };

            //    //Adding new user to our list
            //    context.PizzaStore.Add(newUser);

            //    //Saving the changes
            //    context.SaveChanges();
            //}
        }
    }
}
