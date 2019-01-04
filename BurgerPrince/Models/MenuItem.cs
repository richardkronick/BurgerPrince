using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurgerPrince.Models
{
    public class MenuItem
    {
        [Key]
        public int ID { get; set; }

        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public int Inventory { get; set; }
        public int InitialInventory { get; set; }
        public string Category { get; set; }
    }
}