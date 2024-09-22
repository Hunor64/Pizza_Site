using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Site.Models
{
    //Pizza user properties
    public class PizzaUsers
    {
        [Key]
        public string? User_Name { get; set; }
        public string? User_Password { get; set; }
        public string? User_Email { get; set; }
        public string? User_MobileNumber { get; set; }
        public string? User_Address { get; set; }
        public Boolean? Is_Admin { get; set; }
    }
}
