using Pizza_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pizza_Site
{
    public partial class PizzaEditor : Window
    {
        private Pizza _pizza;
        private List<string> selectedIngredients = new List<string>();

        public PizzaEditor(Pizza pizza)
        {
            InitializeComponent();
            _pizza = pizza;

            txtPizzaname.Text = _pizza.Name;
            txtPrice.Text = _pizza.Price.ToString();

            string imageName = _pizza.ImagePath.Split('/').Last();
            txtImage.Text = imageName.Split('.')[0];

            string fileExtension = imageName.Split('.').Last().ToLower();
            SetExtensionInComboBox(fileExtension);

            selectedIngredients = _pizza.Ingredients.Split(',').Select(i => i.Trim()).ToList();
            foreach (var ingredient in selectedIngredients)
            {
                AddIngredientToGrid(ingredient);
            }
        }

        private void SetExtensionInComboBox(string fileExtension)
        {
            foreach (ComboBoxItem item in cbExtensions.Items)
            {
                if (item.Content.ToString().Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    cbExtensions.SelectedItem = item;
                    return;
                }
            }
            cbExtensions.SelectedIndex = 0;
        }

        private void SavePizza_Click(object sender, RoutedEventArgs e)
        {
            _pizza.Name = txtPizzaname.Text;
            _pizza.Ingredients = string.Join(", ", selectedIngredients);
            _pizza.Price = int.Parse(txtPrice.Text);

            if (cbExtensions.SelectedItem is ComboBoxItem selectedExtensionItem)
            {
                string selectedExtension = selectedExtensionItem.Content.ToString().ToLower();
                _pizza.ImagePath = $"{txtImage.Text.ToLower()}.{selectedExtension}";
            }

            using (var context = new PizzaContext())
            {
                var pizzaInDb = context.PizzasDescription.FirstOrDefault(p => p.PizzaName == _pizza.Name);
                if (pizzaInDb != null)
                {
                    pizzaInDb.PizzaName = _pizza.Name;
                    pizzaInDb.Ingredients = _pizza.Ingredients;
                    pizzaInDb.Price = _pizza.Price;
                    pizzaInDb.ImagePath = _pizza.ImagePath;
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Pizza updated successfully!");
            this.Close();
        }

        private void cbIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbIngredients.SelectedItem is ComboBoxItem selectedItem)
            {
                string ingredient = selectedItem.Content.ToString();

                if (!selectedIngredients.Contains(ingredient))
                {
                    selectedIngredients.Add(ingredient);
                    AddIngredientToGrid(ingredient);
                    cbIngredients.Items.Remove(selectedItem);
                }
            }
        }

        private void AddIngredientToGrid(string ingredient)
        {
            TextBlock ingredientText = new TextBlock
            {
                Text = ingredient,
                Margin = new Thickness(0, 5, 0, 5),
                Cursor = Cursors.Hand,
                Background = Brushes.LightGray
            };
            ingredientText.MouseLeftButtonDown += (s, e) => RemoveIngredientFromGrid(ingredient, ingredientText);

            int rowCount = gridSelectedIngredients.RowDefinitions.Count;
            gridSelectedIngredients.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Grid.SetRow(ingredientText, rowCount);
            gridSelectedIngredients.Children.Add(ingredientText);
        }

        private void RemoveIngredientFromGrid(string ingredient, TextBlock ingredientText)
        {
            selectedIngredients.Remove(ingredient);
            gridSelectedIngredients.Children.Remove(ingredientText);

            ComboBoxItem newItem = new ComboBoxItem { Content = ingredient };
            cbIngredients.Items.Add(newItem);
        }

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
            else
                WindowState = WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
