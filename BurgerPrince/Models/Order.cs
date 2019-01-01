using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurgerPrince.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal OrderSubtotal { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderTotal { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}