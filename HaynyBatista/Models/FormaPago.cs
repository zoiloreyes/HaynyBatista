using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaynyBatista.Models
{
    public class FormaPago
    {
        [Key]
        public int FormaPagoID { get; set; }

        [Required]
        public string Nombre { get; set; }


        public virtual ICollection<Cita> Citas { get; set; }
    }
}