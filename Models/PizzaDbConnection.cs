using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.Windows;
using System.IO;

namespace Pizza_Site.Models
{
    internal class PizzaDbConnection
    {
        public SQLiteConnection sqlite_conn;
        public SQLiteCommand sqlite_cmd;
        public SQLiteDataReader sqlite_datareader;

        public PizzaDbConnection()
        {
            MessageBox.Show(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName + "\\Database", "PizzaStore.db");
            string dbPath = System.IO.Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName + "\\Database", "PizzaStore.db");
            string connectionString = $"Data Source={dbPath}";

            sqlite_conn = new SQLiteConnection(connectionString);
            sqlite_conn.Open();
        }
    }
}
