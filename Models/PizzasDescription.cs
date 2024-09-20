using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    public class PizzasDescription
    {
        [Key]
        public int pizzaId { get; set; }
        public string? pizzaName { get; set; }
        public string[]? ingredients { get; set; }
        public int price { get; set; }
        public string? imagePath { get; set; }
    }
}
