using Pizza_Site.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizza_Site
{
    public partial class MainWindow : Window
    {
        private List<Pizza> pizzas;
        private ObservableCollection<CartItem> cart;


        string userName = null;
        public void InitUser()
        {
            if (userName == null)
            {
                Login loginWindow = new();
                loginWindow.ShowDialog();

            }
        }


        public MainWindow()
        {
            InitUser();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PizzaDbConnection connection = new PizzaDbConnection();
            InitializeComponent();
            LoadPizzaList();
            cart = new ObservableCollection<CartItem>();
            CartListView.ItemsSource = cart;
        }

        public class Pizza
        {
            public string Name { get; set; }
            public string Ingredients { get; set; }
            public int Price { get; set; }
            public string ImagePath { get; set; }
        }

        public class CartItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public int UnitPrice { get; set; }
            public int TotalPrice => Quantity * UnitPrice;
        }

        private void LoadPizzaList()
        {
            pizzas = new List<Pizza>
            {
                new Pizza { Name = "Margherita", Ingredients = "Paradicsomszósz, Mozzarella", Price = 1200, ImagePath = "images/margherita.jpg" },
                new Pizza { Name = "Pepperoni", Ingredients = "Paradicsomszósz, Mozzarella, Pepperoni", Price = 1400, ImagePath = "images/pepperoni.jpg" },
                new Pizza { Name = "Hawaii", Ingredients = "Paradicsomszósz, Mozzarella, Sonka, Ananász", Price = 1500, ImagePath = "images/hawaii.jpg" },
                new Pizza { Name = "BBQ Csirke", Ingredients = "BBQ Szósz, Mozzarella, Csirke", Price = 1600, ImagePath = "images/bbq_csirke.jpg" },
                new Pizza { Name = "Vegetáriánus", Ingredients = "Paradicsomszósz, Mozzarella, Zöldségek", Price = 1300, ImagePath = "images/vegetarian.jpg" }
            };

            PizzaListView.ItemsSource = pizzas;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Button orderButton = sender as Button;
            if (orderButton == null) return;

            Pizza selectedPizza = orderButton.Tag as Pizza;
            if (selectedPizza == null)
            {
                MessageBox.Show("Nem sikerült a pizza kiválasztása.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StackPanel parent = orderButton.Parent as StackPanel;
            if (parent == null) return;

            if (parent.Children[0] is TextBox quantityTextBox)
            {
                int quantity;
                if (!int.TryParse(quantityTextBox.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Érvényes mennyiséget adjon meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingCartItem = cart.FirstOrDefault(item => item.Name == selectedPizza.Name);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    CartItem cartItem = new CartItem
                    {
                        Name = selectedPizza.Name,
                        Quantity = quantity,
                        UnitPrice = selectedPizza.Price
                    };
                    cart.Add(cartItem);
                }

                CartItemCount.Text = cart.Sum(item => item.Quantity).ToString();
            }
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            CartPanel.Visibility = Visibility.Visible;
            UpdateTotalPrice();
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            CartPanel.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = sender as Button;
            if (removeButton == null) return;

            CartItem cartItem = removeButton.Tag as CartItem;
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                UpdateTotalPrice();
                CartItemCount.Text = cart.Sum(item => item.Quantity).ToString();
            }
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            CartItem cartItem = textBox.DataContext as CartItem;
            if (cartItem != null && int.TryParse(textBox.Text, out int newQuantity) && newQuantity >= 0)
            {
                cartItem.Quantity = newQuantity;
                UpdateTotalPrice();
                CartItemCount.Text = cart.Sum(item => item.Quantity).ToString();
            }
        }

        private void FinalizeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Any())
            {
                MessageBox.Show("Sikeres rendelés!", "Rendelés", MessageBoxButton.OK, MessageBoxImage.Information);
                cart.Clear();
                CartItemCount.Text = "0";
                UpdateTotalPrice();
            }
            else
            {
                MessageBox.Show("A kosár üres!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateTotalPrice()
        {
            int totalPrice = 0;
            foreach (var item in cart)
            {
                totalPrice += item.TotalPrice;
            }
            TotalPriceTextBlock.Text = $"Összesen: {totalPrice} Ft";
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
            else WindowState = WindowState.Maximized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}