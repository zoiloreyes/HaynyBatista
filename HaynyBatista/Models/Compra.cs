using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class Compra
    {
        public int CompraID { get; set; }
        public int IdUsuario { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Sector { get; set; }
        public string ReferenciaPaypal { get; set; }
        public string DireccionPaypal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual List<ItemCompra> ItemsCompra { get; set; }
    }
}