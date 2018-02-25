using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaynyBatista.Models
{
    public class TipoCita
    {
        [Key]
        public int IdTipoCita { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public double Costo { get; set; }


        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Imagen> Imagen { get; set; }
    }
}