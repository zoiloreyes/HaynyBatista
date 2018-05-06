using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public Carrito Carrito { get; set; }
        public string ReturnUrl { get; set; }
    }
}