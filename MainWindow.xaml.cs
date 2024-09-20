using Pizza_Site.Models;
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
        string userName = null;
        public MainWindow()
        {
            InitUser();
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PizzaDbConnection connection = new PizzaDbConnection();
        }
        public void InitUser()
        {
            if (userName == null)
            {
                Login loginWindow = new();
                loginWindow.ShowDialog();
                
            }
        }
    }
}