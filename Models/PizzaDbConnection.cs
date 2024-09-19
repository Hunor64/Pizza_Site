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
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PizzaStore.db");
            string connectionString = $"Data Source={dbPath}";

            sqlite_conn = new SQLiteConnection(connectionString);
            sqlite_conn.Open();
        }
    }
}
