using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    public class PizzaStorage
    {
        [Key] public string? ingredientName { get; set; }
        public int ingredientAmount { get; set; }
        public bool isAvailable { get; set; }
    }
}
