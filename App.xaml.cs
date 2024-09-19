using Pizza_Site.Models;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Pizza_Site
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            UpdateDatabase();
        }

        private void UpdateDatabase()
        {
            try
            {
                using (var context = new PizzaContext())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database update failed: {ex.Message}");
            }
        }
    }

}
