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
            string ConnectionString = @"Data Source="+ System.Environment.CurrentDirectory + "\\PizzaStore.db";


            sqlite_conn = new SQLiteConnection(ConnectionString);
            sqlite_conn.Open();
            string cmdString = "INSERT INTO PizzaStore (User_Name, User_Password, User_Email, User_MobileNumber, User_Address,Is_Admin) Values('asd1','asd2','asdasd@asd.asd','123456789','dasdasdas',true)";
            sqlite_cmd = new SQLiteCommand(cmdString, sqlite_conn);
            cmdString = "INSERT INTO PizzaStore (User_Name, User_Password, User_Email, User_MobileNumber, User_Address,Is_Admin) Values('asd3','asd4','asdasd@asd.asd','123456789','dasdasdas',true)";
            sqlite_cmd = new SQLiteCommand(cmdString, sqlite_conn);
            sqlite_datareader = sqlite_cmd.ExecuteReader();
        }
    }
}
