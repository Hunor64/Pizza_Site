using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
    }
}
