using Pizza_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pizza_Site
{
    public partial class Admin_Panel : Window
    {
        public Admin_Panel()
        {
            InitializeComponent();
            LoadPizzaListFromDb();
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

        private void AddNewPizza(object sender, RoutedEventArgs e)
        {
            PizzaAdding newPizzaAdding = new PizzaAdding();
            newPizzaAdding.ShowDialog();
        }

        public void LoadPizzaListFromDb()
        {
            using (var newContext = new PizzaContext())
            {
                List<Pizza> pizzaList = new List<Pizza>();

                var pizzas = newContext.PizzasDescription.ToList();

                foreach (var pizza in pizzas)
                {
                    pizzaList.Add(new Pizza
                    {
                        Name = pizza.PizzaName,
                        ImagePath = $"pack://application:,,,/Pizza_Site;component/Images/{pizza.ImagePath.ToLower()}",
                        Ingredients = pizza.Ingredients,
                        Price = pizza.Price
                    });
                }

                lsbPizzaElemek.ItemsSource = pizzaList;
            }
        }

        private void EditPizza_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Pizza selectedPizza = editButton.Tag as Pizza;

            PizzaEditor editorWindow = new PizzaEditor(selectedPizza);

            bool? result = editorWindow.ShowDialog();

            if (result.HasValue)
            {
                LoadPizzaListFromDb();
            }
        }

        private void lsbPizzaElemek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var selectedPizza = lsbPizzaElemek.SelectedItem as Pizza;

                if (selectedPizza != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Biztos ki akarod törölni a {selectedPizza.Name} pizzát??", "Törlés konfirmáció", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (var context = new PizzaContext())
                        {
                            var pizzaToDelete = context.PizzasDescription.FirstOrDefault(p => p.PizzaName == selectedPizza.Name);

                            if (pizzaToDelete != null)
                            {
                                context.PizzasDescription.Remove(pizzaToDelete);
                                context.SaveChanges();
                            }
                        }

                        LoadPizzaListFromDb();
                    }
                }
            }
        }
    }
}
