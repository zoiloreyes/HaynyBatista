using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Introduzca un valor mayor a {1}")]
        public decimal Monto { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }


        public int FormaPagoID { get; set; }
        public virtual FormaPago FormaPago { get; set; }

        public int CarritoID { get; set; }
        public virtual Carrito Carrito { get; set; }
    }
}