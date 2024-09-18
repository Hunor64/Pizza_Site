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
        public string ConnectionString = "Data Source=PizzaStore.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(ConnectionString);
    }
}
