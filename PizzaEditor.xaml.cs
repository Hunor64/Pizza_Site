using Pizza_Site.Models;
using System.Linq;
using System.Windows;

namespace Pizza_Site
{
    public partial class PizzaEditor : Window
    {
        private Pizza _pizza;

        public PizzaEditor(Pizza pizza)
        {
            InitializeComponent();
            _pizza = pizza;

            // Töltsük be a meglévő adatokat
            txtPizzaName.Text = pizza.Name;
            txtIngredients.Text = pizza.Ingredients;
            txtPrice.Text = pizza.Price.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Frissítsük a pizza adatait
            _pizza.Name = txtPizzaName.Text;
            _pizza.Ingredients = txtIngredients.Text;
            _pizza.Price = (int)decimal.Parse(txtPrice.Text);

            // Mentés az adatbázisba
            using (var context = new PizzaContext())
            {
                var pizzaInDb = context.PizzasDescription.FirstOrDefault(p => p.PizzaName == _pizza.Name);
                if (pizzaInDb != null)
                {
                    pizzaInDb.PizzaName = _pizza.Name;
                    pizzaInDb.Ingredients = _pizza.Ingredients;
                    pizzaInDb.Price = _pizza.Price;
                    context.SaveChanges();
                }
            }

            // Bezárás
            this.DialogResult = true;
            this.Close();
        }
    }
}
