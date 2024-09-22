using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    public class PizzasDescription
    {
        [Key] public string? PizzaName { get; set; }
        public string? Ingredients { get; set; }
        public int Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
