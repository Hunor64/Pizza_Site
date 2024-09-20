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
    public class PizzaAddingService
    {
        //Setting which datas are we getting from the database
        public string AddingPizza(string pizzaname, string ingredients, int price, string imagePath) 
        {
            try
            {
                //Getting data from the database
                using (var context = new PizzaContext())
                {
                    //Passing data to the new pizza
                    var newPizza = new PizzasDescription {PizzaName = pizzaname, Ingredients = ingredients, Price = price, ImagePath = imagePath };

                    //Adding new pizza to our list
                    context.PizzasDescription.Add(newPizza);

                    //Saving the changes
                    context.SaveChanges();

                    //If successful we will get a notification
                    return "New pizza added!";
                }
            }
            //Exception handling
            catch (DbUpdateException dbEx)
            {
                //Checks if the pizza is already exists in database
                if (dbEx.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19)
                {
                    //If it exist it throws that we already have a username like that
                    return $"The {pizzaname} was already added";
                }
                throw;
            }
            catch(Exception nEx) 
            {
                //Any other exception
                return $"Error: {nEx.Message}";
            }
        }
    }
}
