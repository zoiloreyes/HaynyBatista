using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class ArticuloCarrito
    {
        [Key]
        public int ArticuloCarritoID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Valor debe ser mayor a {0}")]
        public int Cantidad { get; set; }

        public int ProductoID { get; set; }
        public virtual Producto Producto { get; set; }
    }
}