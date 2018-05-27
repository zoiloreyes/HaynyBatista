using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class ItemCompra
    {
        public int ItemCompraID { get; set; }
        public int CompraID { get; set; }
        public virtual Compra Compra { get; set; }
        public int ProductoID { get; set; }
        public virtual Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }

    }
}