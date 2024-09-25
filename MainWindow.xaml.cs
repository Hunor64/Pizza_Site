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

        #region Variables
        private List<Pizza> pizzas;
        private ObservableCollection<CartItem> cart;
        private string? _userName;
        private string? _password;
        private string? _email;
        private string? _phone;
        private string? _address;
        private bool? _isAdmin;
        #endregion


        #region Main and User
        //Getting user data from login
        public void SetUserDetails(string userName, string userAddress, string userEmail, string userMobile, string userPass, bool isAdmin)
        {
            _userName = userName;
            _password = userPass;
            _email = userEmail;
            _address = userAddress;
            _phone = userMobile;
            _isAdmin = isAdmin;
        }
        public void InitUser()
        {
            if (_userName == null)
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
            if (_isAdmin == true)
            {
                Admin_Panel panel = new();
                panel.ShowDialog();
            }
            InitializeComponent();
            LoadPizzaListFromDb();
            cart = new ObservableCollection<CartItem>();
            CartListView.ItemsSource = cart;
            this.Closing += MainWindow_Closed;
            if (_userName == null || _userName == "")
            {
                this.Close();
            }
        }
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // Reset all user-related variables
            SetUserDetails(null, null, null, null, null, false);
        }

        #endregion

        #region Loading Pizza list from database
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
                    pizzaList.Add(new Pizza { Name = pizza.PizzaName, ImagePath = $"pack://application:,,,/Pizza_Site;component/Images/{pizza.ImagePath.ToLower()}", Ingredients = pizza.Ingredients, Price = pizza.Price }); ;
                }


                //Setting the main grid's itemsource to our appended list
                PizzaListView.ItemsSource = pizzaList;
            }
        }


        #endregion
        #region Button clicks and Updates
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
                    MessageBox.Show("Felvéve a kosába!");

                }

                CartItemCount.Text = cart.Sum(item => item.Quantity).ToString();
            }
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            CartPanel.Visibility = Visibility.Visible;
            UpdateTotalPrice();
            MessageBox.Show($"2: Username:{_userName},\nPassword:{_password},\nAdmin:{_isAdmin}");
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

        #endregion

        #region Custom title bar clicks
        //For dragging the window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //Window minimizeing
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //Window maximizeing
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }
        //Window closing
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Close(); For closing only this window
            Application.Current.Shutdown(); //For closing the whole app
        }
        #endregion


    }

}