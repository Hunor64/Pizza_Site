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
        #region User Registration
        //Setting which datas are we getting from the database
        public string RegisterUser(string userName, string userPassword, string userEmail, string userMobileNumber, string userAddress, bool is_Admin)
        {
            try
            {
                //Getting data from the database
                using (var context = new PizzaContext())
                {
                    //Passing data to a new user
                    var newUser = new PizzaUsers { User_Name = userName, User_Password = userPassword, User_Email = userEmail, User_MobileNumber = userMobileNumber,User_Address = userAddress, Is_Admin = is_Admin};

                    //Adding new user to our list
                    context.PizzaStore.Add(newUser);

                    //Saving the changes
                    context.SaveChanges();

                    //If successful we will get a notification
                    return "Registration successful!";
                }
            }
            //Exception handling
            catch (DbUpdateException ex)
            {
                //Checks if user is already exists in database
                if (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19)
                {
                    //If it exist it throws that we already have a username like that
                    return $"Registration failed: Username '{userName}' is already taken.";
                }
                throw;
            }
            catch (Exception ex)
            {
                //Any other exception
                return $"Registration failed: {ex.Message}";
            }
        }
        #endregion
    }
}
