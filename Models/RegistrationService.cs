using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Pizza_Site.Models
{
    public class RegistrationService
    {
        public string RegisterUser(string userName, string userPassword, string userEmail, string userMobileNumber, string userAddress, bool is_Admin)
        {
            try
            {
                using (var context = new PizzaContext())
                {
                    var newUser = new PizzaUsers { User_Name = userName, User_Password = userPassword, User_Email = userEmail, User_MobileNumber = userMobileNumber,User_Address = userAddress, Is_Admin = is_Admin};

                    context.PizzaStore.Add(newUser);

                    context.SaveChanges();

                    return "Registration successful!";
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqliteException sqliteException &&
                    sqliteException.SqliteErrorCode == 19)
                {
                    return $"Registration failed: Username '{userName}' is already taken.";
                }
                throw;
            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }
    }
}
