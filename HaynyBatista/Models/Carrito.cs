using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class Carrito
    {
        [Key]
        public int CarritoID { get; set; }

        public virtual ICollection<ArticuloCarrito> ArticulosCarrito { get; set; }
    }
}