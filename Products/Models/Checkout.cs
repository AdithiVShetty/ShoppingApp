using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Models
{
    public class Checkout
    {
        public List<CartItem> CartItems { get; set; }
        public float TotalAmount { get; set; }
        public string Address { get; set; }
    }
}