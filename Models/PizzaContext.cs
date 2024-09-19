using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<PizzaUsers> PizzaStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PizzaStore.db");
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
