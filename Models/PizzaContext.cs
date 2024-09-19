using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza_Site.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<PizzaUsers> PizzaStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = System.IO.Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName + "\\Database" ,"PizzaStore.db");
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
